namespace More.IO
{
    using System;
    using System.IO;

    /// <summary>
    /// Give limited Seek capability to a Stream that doesn't support Seek.
    /// It will buffer the initial specified set of bytes so Seek and Position are supported
    /// while reading below that length.  Once reading exceeds the buffer length any
    /// further calls to Seek or Position setter will throw NotSupportedExceptions.
    /// This is useful for API's that require seeking in the beginning to read over header
    /// information then just sequentially read afterwards.  Specifically this was
    /// needed for use with streaming network data to SevenZipSharp.
    /// </summary>
    public class BufferedSeekReadStream : Stream
    {
        private readonly Stream stream;
        private readonly MemoryStream buffer;

        private long bufferedBytesCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferedSeekReadStream"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="bufferSize">Size of the buffer in bytes.</param>
        /// <exception cref="System.ArgumentNullException">stream</exception>
        /// <exception cref="System.ArgumentException">
        /// stream not readable
        /// </exception>
        public BufferedSeekReadStream(Stream stream, int bufferSize)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (!stream.CanRead)
            {
                throw new ArgumentException("stream not readable");
            }

            this.stream = stream;
            this.buffer = new MemoryStream(new byte[bufferSize], true);
        }

        /// <inheritdoc />
        public override bool CanRead
        {
            get { return true; }
        }

        /// <inheritdoc />
        public override bool CanSeek
        {
            get { return this.bufferedBytesCount < this.buffer.Length; }
        }

        /// <inheritdoc />
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <inheritdoc />
        public override void Flush()
        {
        }

        /// <inheritdoc />
        public override long Length
        {
            get { return this.stream.Length; }
        }

        /// <inheritdoc />
        public override long Position
        {
            get
            {
                return this.CanSeek ? this.buffer.Position : this.stream.Position;
            }
            set
            {
                if (this.CanSeek)
                {
                    this.Seek(value, SeekOrigin.Begin);
                }
                else
                {
                    throw new NotSupportedException("seek not supported after buffer size exceeded");
                }
            }
        }

        /// <inheritdoc />
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this.bufferedBytesCount >= this.buffer.Length)
            {
                return this.stream.Read(buffer, offset, count);
            }

            // Read from buffer
            var bytesRead = this.buffer.Read(buffer, offset, (int)Math.Min(count, this.bufferedBytesCount - this.buffer.Position));

            // If we read past buffer read from underlying stream
            if (bytesRead < count)
            {
                int bytesReadFromStream = this.stream.Read(buffer, offset + bytesRead, count - bytesRead);
                bytesRead += bytesReadFromStream;

                // Added data read from stream to end of buffer
                this.buffer.Position = this.bufferedBytesCount;
                var bytesWrittenToBuffer = (int)Math.Min(bytesReadFromStream, this.buffer.Length - this.buffer.Position);
                this.buffer.Write(buffer, offset, bytesWrittenToBuffer);

                this.bufferedBytesCount = this.buffer.Position;
            }

            return bytesRead;
        }

        /// <inheritdoc />
        public override long Seek(long offset, SeekOrigin origin)
        {
            if (origin == SeekOrigin.End)
            {
                if (offset == 0)
                {
                    return this.stream.Length;
                }

                throw new NotSupportedException("SeekOrigin.End not supported");
            }

            if (!this.CanSeek || 
                (
                    (origin != SeekOrigin.Begin || offset >= this.buffer.Length) &&
                    (origin != SeekOrigin.Current || (offset + this.buffer.Position) >= this.buffer.Length)
                ))
            {
                throw new NotSupportedException("seek not supported after buffer size exceeded");
            }

            var originalPosition = (int)this.buffer.Position;
            var position = (int)this.buffer.Seek(offset, origin);

            if (position > this.bufferedBytesCount)
            {
                var tempBuffer = new byte[position - this.bufferedBytesCount];
                var bytesRead = 0;

                while (bytesRead != tempBuffer.Length)
                {
                    int byteReadFromStream = this.stream.Read(tempBuffer, 0, tempBuffer.Length);

                    if (byteReadFromStream == 0)
                    {
                        throw new EndOfStreamException();
                    }

                    bytesRead += byteReadFromStream;
                }

                this.buffer.Position = originalPosition;
                this.buffer.Write(tempBuffer, 0, tempBuffer.Length);
                this.buffer.Position = position;
            }

            return position;
        }

        /// <inheritdoc />
        public override void SetLength(long value)
        {
            throw new NotSupportedException("write not supported");
        }

        /// <inheritdoc />
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("write not supported");
        }
    }
}
