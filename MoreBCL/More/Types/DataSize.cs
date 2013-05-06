namespace More.Types
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using More.Properties;

    /// <summary>
    /// Immutable value type for data storage size based off of a <see cref="System.Double" />.
    /// Defines units and adds conversion methods.  This class only includes
    /// sizes up to the terabyte level, it does not include petabytes, exabytes,
    /// zettabytes, or yottabytes.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(DataSizeTypeConverter))]
    public struct DataSize : IComparable, IFormattable, IComparable<DataSize>, IEquatable<DataSize>
    {
        /// <summary>
        /// Regex to parse data size strings.
        /// </summary>
        private static readonly Regex regex = new Regex(
            @"^\s*(\d+|\d+\.\d+)\s*(bytes|kB|MB|GB|TB|Kib|MiB|GiB|TiB)?\s*$",
            RegexOptions.IgnoreCase);

        /// <summary>
        /// DataSize in bytes.
        /// </summary>
        private double dataSizeValue;

        /// <summary>
        /// Bytes per kB.
        /// </summary>
        private const double BytesPerKilobyte = 1.0e3;

        /// <summary>
        /// Bytes per MB.
        /// </summary>
        private const double BytesPerMegabyte = 1.0e6;

        /// <summary>
        /// Bytes per GB.
        /// </summary>
        private const double BytesPerGigabyte = 1.0e9;

        /// <summary>
        /// Bytes per TB.
        /// </summary>
        private const double BytesPerTerabyte = 1.0e12;

        /// <summary>
        /// Bytes per KiB.
        /// </summary>
        private const double BytesPerKibibyte = 1024.0;

        /// <summary>
        /// Bytes per MiB.
        /// </summary>
        private const double BytesPerMebibyte = BytesPerKibibyte * 1024.0;

        /// <summary>
        /// Bytes per GiB.
        /// </summary>
        private const double BytesPerGibibyte = BytesPerMebibyte * 1024.0;

        /// <summary>
        /// Bytes per TiB.
        /// </summary>
        private const double BytesPerTebibyte = BytesPerGibibyte * 1024.0;

        /// <summary>
        /// Initializes a new instance of the DataSize struct.
        /// </summary>
        /// <param name="dataSizeValue">Numeric value in bytes.</param>
        public DataSize(double dataSizeValue)
        {
            this.dataSizeValue = dataSizeValue;
        }

        /// <summary>
        /// Gets value in bytes.
        /// </summary>
        public double Bytes
        {
            get { return this.dataSizeValue; }
        }

        /// <summary>
        /// Gets value in kilobytes.
        /// </summary>
        public double kB
        {
            get { return this.dataSizeValue / BytesPerKilobyte; }
        }

        /// <summary>
        /// Gets value in megabytes.
        /// </summary>
        public double MB
        {
            get { return this.dataSizeValue / BytesPerMegabyte; }
        }

        /// <summary>
        /// Gets value in gigabytes.
        /// </summary>
        public double GB
        {
            get { return this.dataSizeValue / BytesPerGigabyte; }
        }

        /// <summary>
        /// Gets value in terabytes.
        /// </summary>
        public double TB
        {
            get { return this.dataSizeValue / BytesPerTerabyte; }
        }

        /// <summary>
        /// Gets value in kibibytes.
        /// </summary>
        public double KiB
        {
            get { return this.dataSizeValue / BytesPerKibibyte; }
        }

        /// <summary>
        /// Gets value in mebibytes.
        /// </summary>
        public double MiB
        {
            get { return this.dataSizeValue / BytesPerMebibyte; }
        }

        /// <summary>
        /// Gets value in gibibytes.
        /// </summary>
        public double GiB
        {
            get { return this.dataSizeValue / BytesPerGibibyte; }
        }

        /// <summary>
        /// Gets value in tebibytes.
        /// </summary>
        public double TiB
        {
            get { return this.dataSizeValue / BytesPerTebibyte; }
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="value">Value of data size in bytes.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator DataSize(double value)
        {
            return new DataSize(value);
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="value">Value of data size in bytes.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator DataSize(float value)
        {
            return new DataSize(value);
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="value">Value of data size in bytes.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator DataSize(int value)
        {
            return new DataSize(value);
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="value">Value of data size in bytes.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator DataSize(long value)
        {
            return new DataSize(value);
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="value">Value of data size in bytes.</param>
        /// <returns>Returns casted value.</returns>
        public static explicit operator DataSize(decimal value)
        {
            return new DataSize((double)value);
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="size">Value of data size.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator double(DataSize size)
        {
            return size.Bytes;
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="size">Value of data size.</param>
        /// <returns>Returns casted value.</returns>
        public static explicit operator long(DataSize size)
        {
            return (long)size.Bytes;
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="size">Value of data size.</param>
        /// <returns>Returns casted value.</returns>
        public static explicit operator int(DataSize size)
        {
            return (int)size.Bytes;
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="size">Value of data size.</param>
        /// <returns>Returns casted value.</returns>
        public static explicit operator float(DataSize size)
        {
            return (float)size.Bytes;
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="size">Value of data size.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator decimal(DataSize size)
        {
            return (decimal)size.Bytes;
        }

        /// <summary>
        /// Not equal operator.
        /// </summary>
        /// <param name="size1">LH data size.</param>
        /// <param name="size2">RH data size.</param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator !=(DataSize size1, DataSize size2)
        {
            return size1.dataSizeValue != size2.dataSizeValue;
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        /// <param name="size1">LH data size.</param>
        /// <param name="size2">RH data size.</param>
        /// <returns>Returns arithmetic result.</returns>
        public static DataSize operator *(DataSize size1, DataSize size2)
        {
            return new DataSize(size1.dataSizeValue * size2.dataSizeValue);
        }

        /// <summary>
        /// Add operator.
        /// </summary>
        /// <param name="size1">LH data size.</param>
        /// <param name="size2">RH data size.</param>
        /// <returns>Returns arithmetic result.</returns>
        public static DataSize operator +(DataSize size1, DataSize size2)
        {
            return new DataSize(size1.dataSizeValue + size2.dataSizeValue);
        }

        /// <summary>
        /// Increment operator.
        /// </summary>
        /// <param name="size1">LH data size.</param>
        /// <returns>Returns arithmetic result.</returns>
        public static DataSize operator ++(DataSize size1)
        {
            return new DataSize(size1.dataSizeValue++);
        }

        /// <summary>
        /// Subtract operator.
        /// </summary>
        /// <param name="size1">LH data size.</param>
        /// <param name="size2">RH data size.</param>
        /// <returns>Returns arithmetic result.</returns>
        public static DataSize operator -(DataSize size1, DataSize size2)
        {
            return new DataSize(size1.dataSizeValue - size2.dataSizeValue);
        }

        /// <summary>
        /// Decrement operator.
        /// </summary>
        /// <param name="size1">LH data size.</param>
        /// <returns>Returns arithmetic result.</returns>
        public static DataSize operator --(DataSize size1)
        {
            return new DataSize(size1.dataSizeValue++);
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        /// <param name="size1">LH data size.</param>
        /// <param name="size2">RH data size.</param>
        /// <returns>Returns arithmetic result.</returns>
        public static DataSize operator /(DataSize size1, DataSize size2)
        {
            return new DataSize(size1.dataSizeValue / size2.dataSizeValue);
        }

        /// <summary>
        /// Less than operator.
        /// </summary>
        /// <param name="size1">LH data size.</param>
        /// <param name="size2">RH data size.</param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator <(DataSize size1, DataSize size2)
        {
            return size1.dataSizeValue < size2.dataSizeValue;
        }

        /// <summary>
        /// Less than or equal operator.
        /// </summary>
        /// <param name="size1">LH data size.</param>
        /// <param name="size2">RH data size.</param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator <=(DataSize size1, DataSize size2)
        {
            return size1.dataSizeValue <= size2.dataSizeValue;
        }

        /// <summary>
        /// Equal operator.
        /// </summary>
        /// <param name="size1">LH data size.</param>
        /// <param name="size2">RH data size.</param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator ==(DataSize size1, DataSize size2)
        {
            return size1.dataSizeValue == size2.dataSizeValue;
        }

        /// <summary>
        /// Greater than operator.
        /// </summary>
        /// <param name="size1">LH data size.</param>
        /// <param name="size2">RH data size.</param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator >(DataSize size1, DataSize size2)
        {
            return size1.dataSizeValue > size2.dataSizeValue;
        }

        /// <summary>
        /// Greater than or equal operator.
        /// </summary>
        /// <param name="size1">LH data size.</param>
        /// <param name="size2">RH data size.</param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator >=(DataSize size1, DataSize size2)
        {
            return size1.dataSizeValue >= size2.dataSizeValue;
        }

        /// <summary>
        /// Convert data size.
        /// </summary>
        /// <param name="value">Value of data size.</param>
        /// <returns>DataSize from converted value.</returns>
        public static DataSize FromBytes(double value)
        {
            return value;
        }

        /// <summary>
        /// Convert data size.
        /// </summary>
        /// <param name="value">Value of data size.</param>
        /// <returns>DataSize from converted value.</returns>
        public static DataSize FromkB(double value)
        {
            return FromBytes(value * BytesPerKilobyte);
        }

        /// <summary>
        /// Convert data size.
        /// </summary>
        /// <param name="value">Value of data size.</param>
        /// <returns>DataSize from converted value.</returns>
        public static DataSize FromMB(double value)
        {
            return FromBytes(value * BytesPerMegabyte);
        }

        /// <summary>
        /// Convert data size.
        /// </summary>
        /// <param name="value">Value of data size.</param>
        /// <returns>DataSize from converted value.</returns>
        public static DataSize FromGB(double value)
        {
            return FromBytes(value * BytesPerGigabyte);
        }

        /// <summary>
        /// Convert data size.
        /// </summary>
        /// <param name="value">Value of data size.</param>
        /// <returns>DataSize from converted value.</returns>
        public static DataSize FromTB(double value)
        {
            return FromBytes(value * BytesPerTerabyte);
        }

        /// <summary>
        /// Convert data size.
        /// </summary>
        /// <param name="value">Value of data size.</param>
        /// <returns>DataSize from converted value.</returns>
        public static DataSize FromKiB(double value)
        {
            return FromBytes(value * BytesPerKibibyte);
        }

        /// <summary>
        /// Convert data size.
        /// </summary>
        /// <param name="value">Value of data size.</param>
        /// <returns>DataSize from converted value.</returns>
        public static DataSize FromMiB(double value)
        {
            return FromBytes(value * BytesPerMebibyte);
        }

        /// <summary>
        /// Convert data size.
        /// </summary>
        /// <param name="value">Value of data size.</param>
        /// <returns>DataSize from converted value.</returns>
        public static DataSize FromGiB(double value)
        {
            return FromBytes(value * BytesPerGibibyte);
        }

        /// <summary>
        /// Convert data size.
        /// </summary>
        /// <param name="value">Value of data size.</param>
        /// <returns>DataSize from converted value.</returns>
        public static DataSize FromTiB(double value)
        {
            return FromBytes(value * BytesPerTebibyte);
        }

        /// <summary>
        /// Parse a data size string, e.g. 23 Mhz or 54.45 kHz.
        /// </summary>
        /// <param name="s">The string representation of a data size.</param>
        /// <returns>The result data size.</returns>
        /// <remarks>Currently the string is assumed to be in hertz.  However,
        /// the intention is to allow parsing at a later date with different
        /// units that would have to be specified by passing in a units designator.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Invalid string.</exception>
        /// <exception cref="ArgumentException">Returned by double.Parse</exception>
        /// <exception cref="FormatException">Returned by double.Parse</exception>
        /// <exception cref="OverflowException">Returned by double.Parse</exception>
        public static DataSize Parse(string s)
        {
            Match match = regex.Match(s);

            if (match.Success)
            {
                double number = double.Parse(match.Groups[1].Value);
                string units = match.Groups[2].Value.ToLower();

                switch (units)
                {
                    case "":
                    case "bytes":
                        return FromBytes(number);
                    case "kB":
                        return FromkB(number);
                    case "MB":
                        return FromMB(number);
                    case "GB":
                        return FromGB(number);
                    case "TB":
                        return FromTB(number);
                    case "KiB":
                        return FromKiB(number);
                    case "MiB":
                        return FromMiB(number);
                    case "GiB":
                        return FromGiB(number);
                    case "TiB":
                        return FromTiB(number);
                    default:
                        throw new FormatException("Unrecognized units specified for DataSize");
                }
            }

            throw new FormatException("Unrecognized format for DataSize");
        }

        /// <summary>
        /// Tries to parse a data size string, e.g. 23 MB or 54.45 KiB.
        /// </summary>
        /// <param name="s">The string representation of a data size.</param>
        /// <param name="result">The result data size.</param>
        /// <returns>True if the parse was successful.</returns>
        public static bool TryParse(string s, out DataSize result)
        {
            bool ok = false;

            try
            {
                result = Parse(s);
                ok = true;
            }
            catch
            {
                result = FromBytes(0.0);
            }

            return ok;
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns the computed value.</returns>
        public DataSize Add(DataSize value)
        {
            return this + value;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the 
        /// objects being compared. The return value has these meanings: Value 
        /// Meaning Less than zero This instance is less than obj. Zero This 
        /// instance is equal to obj. Greater than zero This instance is greater 
        /// than obj.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">obj is not the same type 
        /// as this instance. </exception>
        public int CompareTo(object obj)
        {
            if (obj is DataSize)
            {
                return this.dataSizeValue.CompareTo(((DataSize)obj).dataSizeValue);
            }

            return this.dataSizeValue.CompareTo(obj);
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the 
        /// objects being compared. The return value has the following meanings: 
        /// Value Meaning Less than zero This object is less than the other 
        /// parameter.Zero This object is equal to other. Greater than zero 
        /// This object is greater than other.
        /// </returns>
        public int CompareTo(DataSize other)
        {
            return this.dataSizeValue.CompareTo(other);
        }

        /// <summary>
        /// Decrements the value.
        /// </summary>
        /// <returns>Returns the computed value.</returns>
        public DataSize Decrement()
        {
            return this++;
        }

        /// <summary>
        /// Divides the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns the computed value.</returns>
        public DataSize Divide(DataSize value)
        {
            return this / value;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        /// true if obj and this instance are the same type and 
        /// represent the same value; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            bool equal = false;

            if (obj is DataSize)
            {
                var temp = (DataSize)obj;

                if (temp.dataSizeValue == this.dataSizeValue)
                {
                    equal = true;
                }
            }

            return equal;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(DataSize other)
        {
            return other.dataSizeValue.Equals(this.dataSizeValue);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code 
        /// for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return this.dataSizeValue.GetHashCode();
        }

        /// <summary>
        /// Increments the value.
        /// </summary>
        /// <returns>Returns the computed value.</returns>
        public DataSize Increment()
        {
            return this++;
        }

        /// <summary>
        /// Multiplies the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns the computed value.</returns>
        public DataSize Multiply(DataSize value)
        {
            return this * value;
        }

        /// <summary>
        /// Subtracts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns the computed value.</returns>
        public DataSize Subtract(DataSize value)
        {
            return this - value;
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> containing a 
        /// fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return this.dataSizeValue.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Formats the value of the current instance using the specified format.
        /// Formatting same as double except a units and label specifier may be used.  Prefix format with M, k, or G for MHz, kHz, or GHz
        /// units and postfix with l to show units label.
        /// </summary>
        /// <param name="format">The <see cref="T:System.String"></see> specifying the format to use.-or- null to use the default format defined for the type of the <see cref="T:System.IFormattable"></see> implementation.</param>
        /// <param name="formatProvider">The <see cref="T:System.IFormatProvider"></see> to use to format the value.-or- null to obtain the numeric format information from the current locale setting of the operating system.</param>
        /// <returns>
        /// A <see cref="T:System.String"></see> containing the value of the current instance in the specified format.
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                return this.dataSizeValue.ToString(format, formatProvider);
            }

            // The values accepted as units here are somewhat
            // arbitrary.  Upper case is used for MB, GB, etc. and
            // lower case is used for MiB, GiB, etc.
            Match match = Regex.Match(
                format, @"(K|M|G|T|k|m|g|t)?([0-9e\+\(\),\.\#]*)(l)?");

            if (match.Success)
            {
                string unitsFlag = match.Groups[1].Value;
                string numericFormat = match.Groups[2].Value;
                string labelFlag = match.Groups[3].Value;

                string output = string.Empty;
                string units = string.Empty;

                switch (unitsFlag)
                {
                    case "K":
                        output = this.kB.ToString(numericFormat, formatProvider);
                        units = Resources.KilobyteUnits;
                        break;
                    case "M":
                        output = this.MB.ToString(numericFormat, formatProvider);
                        units = Resources.MegabyteUnits;
                        break;
                    case "G":
                        output = this.GB.ToString(numericFormat, formatProvider);
                        units = Resources.GigabyteUnits;
                        break;
                    case "T":
                        output = this.TB.ToString(numericFormat, formatProvider);
                        units = Resources.TerabyteUnits;
                        break;
                    case "k":
                        output = this.KiB.ToString(numericFormat, formatProvider);
                        units = Resources.KibibyteUnits;
                        break;
                    case "m":
                        output = this.MiB.ToString(numericFormat, formatProvider);
                        units = Resources.MebibyteUnits;
                        break;
                    case "g":
                        output = this.GiB.ToString(numericFormat, formatProvider);
                        units = Resources.GibibyteUnits;
                        break;
                    case "t":
                        output = this.TiB.ToString(numericFormat, formatProvider);
                        units = Resources.TebibyteUnits;
                        break;
                    case "":
                        output = this.Bytes.ToString(numericFormat, formatProvider);
                        break;
                }

                if (labelFlag == "l")
                {
                    output += " " + units;
                }

                return output;
            }

            return this.dataSizeValue.ToString(format, formatProvider);
        }

        /// <summary>
        /// Formats the value of the current instance using the specified format.
        /// </summary>
        /// <param name="format">The <see cref="T:System.String"></see> specifying the format to use.-or- null to use the default format defined for the type of the <see cref="T:System.IFormattable"></see> implementation.</param>
        /// <returns>
        /// A <see cref="T:System.String"></see> containing the value of the current instance in the specified format.
        /// </returns>
        public string ToString(string format)
        {
            return this.ToString(format, null);
        }

        /// <summary>
        /// Formats the value of the current instance using the specified format.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="T:System.String"></see> containing the value of the current instance in the specified format.
        /// </returns>
        public string ToString(IFormatProvider provider)
        {
            return this.ToString(null, provider);
        }
    }
}