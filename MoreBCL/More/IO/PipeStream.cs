namespace More.IO
{
    using System;
    using System.IO;
    using System.Threading;

    /// <summary>
    /// PipeStream is a thread-safe read/write data stream for use between two threads in a producer/consumer type problem.
    /// Originally inspired by: http://www.codeproject.com/Articles/16011/PipeStream-a-Memory-Efficient-and-Thread-Safe-Stre
    /// </summary>
    public class PipeStream : Stream
    {
        /// <summary>
        /// Fixed size queue to efficiently enque and deque arrays of data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class BoundedQueue<T>
        {
            private readonly int size;
            private readonly T[] queueBuffer;

            private int startIndex;

            public BoundedQueue(int size)
            {
                this.queueBuffer = new T[size];
                this.size = size;
                this.startIndex = 0;
                this.Count = 0;
            }

            public int Count { get; private set; }

            public void Enqueue(T[] data, int offset, int length)
            {
                if (length + this.Count > this.size)
                {
                    throw new ArgumentException("length + Count exceeds the Size");
                }

                int nextIndex = (this.startIndex + this.Count) % this.size;

                if (nextIndex + length > this.size)
                {
                    int remainder = this.size - nextIndex;
                    int delta = length - remainder;

                    Array.Copy(data, offset, this.queueBuffer, nextIndex, remainder);
                    Array.Copy(data, offset + remainder, this.queueBuffer, 0, delta);
                }
                else
                {
                    Array.Copy(data, offset, this.queueBuffer, nextIndex, length);
                }

                this.Count += length;
            }

            public int Deque(T[] buffer, int offset, int length)
            {
                if (length > this.Count)
                {
                    length = this.Count;
                }

                int nextStartIndex = this.startIndex + length;

                if (nextStartIndex > this.size)
                {
                    var remainder = this.size - this.startIndex;
                    var delta = length - remainder;

                    Array.Copy(this.queueBuffer, this.startIndex, buffer, offset, remainder);
                    Array.Copy(this.queueBuffer, 0, buffer, offset + remainder, delta);
                }
                else
                {
                    Array.Copy(this.queueBuffer, this.startIndex, buffer, offset, length);
                }

                this.startIndex = nextStartIndex % this.size;
                this.Count -= length;

                return length;
            }

            public void Clear()
            {
                this.startIndex = 0;
                this.Count = 0;
            }
        }

        private const int Kb = 1024;

        private readonly BoundedQueue<byte> pipeBuffer;
        private bool shouldFlush;
        private bool blockLastRead;
        private long position;
        private long length;
        private readonly bool keepTrackOfReadPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="PipeStream"/> class.
        /// </summary>
        public PipeStream()
            : this(40 * Kb)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PipeStream" /> class.
        /// </summary>
        /// <param name="maxBufferLength">Length of the max buffer.</param>
        /// <param name="virtualLength">Virtual length of stream which is useful when caller needs total Length upfront.</param>
        /// <param name="keepTrackOfReadPosition">if set to <c>true</c> keep track of read position, otherwise Position will always be 0.</param>
        public PipeStream(int maxBufferLength, long virtualLength = 0, bool keepTrackOfReadPosition = true)
        {
            this.keepTrackOfReadPosition = keepTrackOfReadPosition;
            this.length = virtualLength;
            this.pipeBuffer = new BoundedQueue<byte>(maxBufferLength * 2);
            this.MaxBufferLength = maxBufferLength;
            this.blockLastRead = true;
        }

        /// <summary>
        /// Gets or sets the maximum number of bytes to store in the buffer.
        /// </summary>
        public long MaxBufferLength
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to block last read method before the buffer is empty.
        /// When true, Read() will block until it can fill the passed in buffer and count.
        /// When false, Read() will not block, returning all the available buffer data.
        /// </summary>
        /// <remarks>
        /// Setting to true will remove the possibility of ending a stream reader prematurely.
        /// </remarks>
        /// <value>
        /// <c>true</c> if block last read method before the buffer is empty; otherwise, <c>false</c>.
        /// </value>
        public bool BlockLastReadBuffer
        {
            get
            {
                return this.blockLastRead;
            }
            set
            {
                this.blockLastRead = value;

                // when turning off the block last read, signal Read() that it may now read the rest of the buffer.
                if (!this.blockLastRead)
                {
                    lock (this.pipeBuffer)
                    {
                        Monitor.Pulse(this.pipeBuffer);
                    }
                }
            }
        }

        /// <inheritdoc />
        public override void Flush()
        {
            this.shouldFlush = true;

            lock (this.pipeBuffer)
            {
                Monitor.Pulse(this.pipeBuffer);
            }
        }

        /// <inheritdoc />
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void SetLength(long value)
        {
            this.length = value;
        }

        /// <inheritdoc />
        public override int Read(byte[] buffer, int offset, int count)
        {
            this.ThrowIfInvalidReadArguments(buffer, offset, count);

            if (count == 0)
            {
                return 0;
            }

            var readLength = 0;

            lock (this.pipeBuffer)
            {
                while (!this.ReadAvailable(count))
                {
                    Monitor.Wait(this.pipeBuffer);
                }

                readLength = this.pipeBuffer.Deque(buffer, offset, count);

                Monitor.Pulse(this.pipeBuffer);
            }

            if (this.keepTrackOfReadPosition)
            {
                this.position += readLength;
            }

            return readLength;
        }

        private void ThrowIfInvalidReadArguments(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentException("Buffer is null");
            }

            if (offset + count > buffer.Length)
            {
                throw new ArgumentException("The sum of offset and count is greater than the buffer length. ");
            }

            if (offset < 0 || count < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "offset or count is negative.");
            }

            if (this.BlockLastReadBuffer && count >= this.MaxBufferLength)
            {
                throw new ArgumentException(string.Format("count({0}) > mMaxBufferLength({1})", count, this.MaxBufferLength));
            }
        }

        private bool ReadAvailable(int count)
        {
            return (this.pipeBuffer.Count >= count || shouldFlush) && (this.pipeBuffer.Count >= (count + 1) || !BlockLastReadBuffer);
        }

        /// <inheritdoc />
        public override void Write(byte[] buffer, int offset, int count)
        {
            this.ThrowIfInvalidWriteArguments(buffer, offset, count);

            lock (this.pipeBuffer)
            {
                // Wait until the buffer isn't full
                while (this.pipeBuffer.Count >= this.MaxBufferLength)
                {
                    Monitor.Wait(this.pipeBuffer);
                }

                // If it was flushed before it soon will be so turn this off
                shouldFlush = false;

                // Queue up the buffer data
                this.pipeBuffer.Enqueue(buffer, offset, count);

                // Signal that write has occured
                Monitor.Pulse(this.pipeBuffer);
            }
        }

        private void ThrowIfInvalidWriteArguments(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentException("Buffer is null");
            }

            if (offset + count > buffer.Length)
            {
                throw new ArgumentException("The sum of offset and count is greater than the buffer length. ");
            }

            if (offset < 0 || count < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "offset or count is negative.");
            }

            if (count == 0)
            {
                return;
            }
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            lock (this.pipeBuffer)
            {
                this.position = 0;
                this.pipeBuffer.Clear();
                this.shouldFlush = false;
            }
        }

        /// <inheritdoc />
        public override bool CanRead
        {
            get { return true; }
        }

        /// <inheritdoc />
        public override bool CanSeek
        {
            get { return false; }
        }

        /// <inheritdoc />
        public override bool CanWrite
        {
            get { return true; }
        }

        /// <inheritdoc />
        public override long Length
        {
            get { return this.length; }
        }

        /// <inheritdoc />
        public override long Position
        {
            get { return this.position; }
            set { throw new NotImplementedException(); }
        }
    }
}
