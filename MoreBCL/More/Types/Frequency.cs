namespace More.Types
{
    using System;
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using More.Properties;

    /// <summary>
    /// Immutable value type for frequency based off of a <see cref="System.Double" />.
    /// Defines units and adds conversion methods.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(FrequencyTypeConverter))]
    public struct Frequency : IComparable, IFormattable, IComparable<Frequency>, IEquatable<Frequency>
    {
        /// <summary>
        /// Regex to parse frequency strings.
        /// </summary>
        private static readonly Regex regex = new Regex(
            @"^\s*(\d+|\d+\.\d+)\s*(Hz|kHz|MHz|GHz)?\s*$",
            RegexOptions.IgnoreCase);

        /// <summary>
        /// Frequency stored in Hz.
        /// </summary>
        private double frequencyValue;

        /// <summary>
        /// Initializes a new instance of the Frequency struct.
        /// </summary>
        /// <param name="frequencyValue">Numeric value in Hz.</param>
        public Frequency(double frequencyValue)
        {
            this.frequencyValue = frequencyValue;
        }

        /// <summary>
        /// Gets value in GHz.
        /// </summary>
        public double GHz
        {
            get { return this.frequencyValue / 1000000000; }
        }

        /// <summary>
        /// Gets value in Hz.
        /// </summary>
        public double Hz
        {
            get { return this.frequencyValue; }
        }

        /// <summary>
        /// Gets value in KHz.
        /// </summary>
        public double KHz
        {
            get { return this.frequencyValue / 1000; }
        }

        /// <summary>
        /// Gets value in MHz.
        /// </summary>
        public double MHz
        {
            get { return this.frequencyValue / 1000000; }
        }

        /// <summary>
        /// Gets inverse value or the period in seconds.
        /// </summary>
        public TimeSpan Period
        {
            get { return TimeSpan.FromSeconds(1.0 / this.frequencyValue); }
        }



        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="value">Value of frequency in Hz.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator Frequency(double value)
        {
            return new Frequency(value);
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="value">Value of frequency in Hz.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator Frequency(float value)
        {
            return new Frequency(value);
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="value">Value of frequency in Hz.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator Frequency(int value)
        {
            return new Frequency(value);
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="value">Value of frequency in Hz.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator Frequency(long value)
        {
            return new Frequency(value);
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="value">Value of frequency in Hz.</param>
        /// <returns>Returns casted value.</returns>
        public static explicit operator Frequency(decimal value)
        {
            return new Frequency((double)value);
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="freq">Value of </param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator double(Frequency freq)
        {
            return freq.Hz;
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="freq">Value of </param>
        /// <returns>Returns casted value.</returns>
        public static explicit operator long(Frequency freq)
        {
            return (long)freq.Hz;
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="freq">Value of </param>
        /// <returns>Returns casted value.</returns>
        public static explicit operator int(Frequency freq)
        {
            return (int)freq.Hz;
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="freq">Value of </param>
        /// <returns>Returns casted value.</returns>
        public static explicit operator float(Frequency freq)
        {
            return (float)freq.Hz;
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="freq">Value of </param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator decimal(Frequency freq)
        {
            return (decimal)freq.Hz;
        }

        /// <summary>
        /// Not equal operator.
        /// </summary>
        /// <param name="freq1">LH </param>
        /// <param name="freq2">RH </param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator !=(Frequency freq1, Frequency freq2)
        {
            return freq1.frequencyValue != freq2.frequencyValue;
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        /// <param name="freq1">LH </param>
        /// <param name="freq2">RH </param>
        /// <returns>Returns arithmetic result.</returns>
        public static Frequency operator *(Frequency freq1, Frequency freq2)
        {
            return new Frequency(freq1.frequencyValue * freq2.frequencyValue);
        }

        /// <summary>
        /// Add operator.
        /// </summary>
        /// <param name="freq1">LH </param>
        /// <param name="freq2">RH </param>
        /// <returns>Returns arithmetic result.</returns>
        public static Frequency operator +(Frequency freq1, Frequency freq2)
        {
            return new Frequency(freq1.frequencyValue + freq2.frequencyValue);
        }

        /// <summary>
        /// Increment operator.
        /// </summary>
        /// <param name="freq1">LH </param>
        /// <returns>Returns arithmetic result.</returns>
        public static Frequency operator ++(Frequency freq1)
        {
            return new Frequency(freq1.frequencyValue++);
        }

        /// <summary>
        /// Subtract operator.
        /// </summary>
        /// <param name="freq1">LH </param>
        /// <param name="freq2">RH </param>
        /// <returns>Returns arithmetic result.</returns>
        public static Frequency operator -(Frequency freq1, Frequency freq2)
        {
            return new Frequency(freq1.frequencyValue - freq2.frequencyValue);
        }

        /// <summary>
        /// Decrement operator.
        /// </summary>
        /// <param name="freq1">LH </param>
        /// <returns>Returns arithmetic result.</returns>
        public static Frequency operator --(Frequency freq1)
        {
            return new Frequency(freq1.frequencyValue++);
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        /// <param name="freq1">LH </param>
        /// <param name="freq2">RH </param>
        /// <returns>Returns arithmetic result.</returns>
        public static Frequency operator /(Frequency freq1, Frequency freq2)
        {
            return new Frequency(freq1.frequencyValue / freq2.frequencyValue);
        }

        /// <summary>
        /// Less than operator.
        /// </summary>
        /// <param name="freq1">LH </param>
        /// <param name="freq2">RH </param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator <(Frequency freq1, Frequency freq2)
        {
            return freq1.frequencyValue < freq2.frequencyValue;
        }

        /// <summary>
        /// Less than or equal operator.
        /// </summary>
        /// <param name="freq1">LH </param>
        /// <param name="freq2">RH </param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator <=(Frequency freq1, Frequency freq2)
        {
            return freq1.frequencyValue <= freq2.frequencyValue;
        }

        /// <summary>
        /// Equal operator.
        /// </summary>
        /// <param name="freq1">LH </param>
        /// <param name="freq2">RH </param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator ==(Frequency freq1, Frequency freq2)
        {
            return freq1.frequencyValue == freq2.frequencyValue;
        }

        /// <summary>
        /// Greater than operator.
        /// </summary>
        /// <param name="freq1">LH </param>
        /// <param name="freq2">RH </param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator >(Frequency freq1, Frequency freq2)
        {
            return freq1.frequencyValue > freq2.frequencyValue;
        }

        /// <summary>
        /// Greater than or equal operator.
        /// </summary>
        /// <param name="freq1">LH </param>
        /// <param name="freq2">RH </param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator >=(Frequency freq1, Frequency freq2)
        {
            return freq1.frequencyValue >= freq2.frequencyValue;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Frequency from converted value.</returns>
        public static Frequency FromGHz(double value)
        {
            return GHzToHz(value);
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Frequency from converted value.</returns>
        public static Frequency FromHz(double value)
        {
            return value;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Frequency from converted value.</returns>
        public static Frequency FromKHz(double value)
        {
            return KHzToHz(value);
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Frequency from converted value.</returns>
        public static Frequency FromMHz(double value)
        {
            return MHzToHz(value);
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static double GHzToHz(double value)
        {
            return value * 1000000000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static decimal GHzToHz(decimal value)
        {
            return value * 1000000000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static double GHzToKHz(double value)
        {
            return value * 1000000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static decimal GHzToKHz(decimal value)
        {
            return value * 1000000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static double GHzToMHz(double value)
        {
            return value * 1000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static decimal GHzToMHz(decimal value)
        {
            return value * 1000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static double HzToGHz(double value)
        {
            return value / 1000000000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static decimal HzToGHz(decimal value)
        {
            return value / 1000000000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static double HzToKHz(double value)
        {
            return value / 1000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static decimal HzToKHz(decimal value)
        {
            return value / 1000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static double HzToMHz(double value)
        {
            return value / 1000000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static decimal HzToMHz(decimal value)
        {
            return value / 1000000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static double KHzToGHz(double value)
        {
            return value / 1000000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static decimal KHzToGHz(decimal value)
        {
            return value / 1000000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static double KHzToHz(double value)
        {
            return value * 1000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static decimal KHzToHz(decimal value)
        {
            return value * 1000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static double KHzToMHz(double value)
        {
            return value / 1000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static decimal KHzToMHz(decimal value)
        {
            return value / 1000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static double MHzToGHz(double value)
        {
            return value / 1000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static decimal MHzToGHz(decimal value)
        {
            return value / 1000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static double MHzToHz(double value)
        {
            return value * 1000000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static decimal MHzToHz(decimal value)
        {
            return value * 1000000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static double MHzToKHz(double value)
        {
            return value * 1000;
        }

        /// <summary>
        /// Convert 
        /// </summary>
        /// <param name="value">Value of </param>
        /// <returns>Converted </returns>
        public static decimal MHzToKHz(decimal value)
        {
            return value * 1000;
        }

        /// <summary>
        /// Parse a frequency string, e.g. 23 Mhz or 54.45 kHz.
        /// </summary>
        /// <param name="s">The string representation of a </param>
        /// <returns>The result </returns>
        /// <remarks>Currently the string is assumed to be in hertz.  However,
        /// the intention is to allow parsing at a later date with different
        /// units that would have to be specified by passing in a units designator.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Invalid string.</exception>
        /// <exception cref="ArgumentException">Returned by double.Parse</exception>
        /// <exception cref="FormatException">Returned by double.Parse</exception>
        /// <exception cref="OverflowException">Returned by double.Parse</exception>
        public static Frequency Parse(string s)
        {
            Match match = regex.Match(s);

            if (match.Success)
            {
                double number = double.Parse(match.Groups[1].Value);
                string units = match.Groups[2].Value.ToLower();

                switch (units)
                {
                    case "hz":
                    case "":
                        return FromHz(number);
                    case "khz":
                        return FromKHz(number);
                    case "mhz":
                        return FromMHz(number);
                    case "ghz":
                        return FromGHz(number);
                    default:
                        throw new FormatException("Unrecognized units specified for Frequency");
                }
            }

            throw new FormatException("Unrecongized format for Frequency");
        }

        /// <summary>
        /// Tries to parse a frequency string, e.g. 23 Mhz or 54.45 kHz.
        /// </summary>
        /// <param name="s">The string representation of a </param>
        /// <param name="result">The result </param>
        /// <returns>True if the parse was successful.</returns>
        public static bool TryParse(string s, out Frequency result)
        {
            bool ok = false;

            try
            {
                result = Parse(s);
                ok = true;
            }
            catch
            {
                result = FromHz(0.0);
            }

            return ok;
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns the computed value.</returns>
        public Frequency Add(Frequency value)
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
            if (obj is Frequency)
            {
                return this.frequencyValue.CompareTo(((Frequency)obj).frequencyValue);
            }

            return this.frequencyValue.CompareTo(obj);
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
        public int CompareTo(Frequency other)
        {
            return this.frequencyValue.CompareTo((double)other);
        }

        /// <summary>
        /// Decrements the value.
        /// </summary>
        /// <returns>Returns the computed value.</returns>
        public Frequency Decrement()
        {
            return this++;
        }

        /// <summary>
        /// Divides the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns the computed value.</returns>
        public Frequency Divide(Frequency value)
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

            if (obj is Frequency)
            {
                Frequency temp = (Frequency)obj;

                if (temp.frequencyValue == this.frequencyValue)
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
        public bool Equals(Frequency other)
        {
            return other.frequencyValue.Equals(this.frequencyValue);
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
            return this.frequencyValue.GetHashCode();
        }

        /// <summary>
        /// Increments the value.
        /// </summary>
        /// <returns>Returns the computed value.</returns>
        public Frequency Increment()
        {
            return this++;
        }

        /// <summary>
        /// Multiplies the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns the computed value.</returns>
        public Frequency Multiply(Frequency value)
        {
            return this * value;
        }

        /// <summary>
        /// Subtracts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns the computed value.</returns>
        public Frequency Subtract(Frequency value)
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
            return this.frequencyValue.ToString();
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
                return this.frequencyValue.ToString(format, formatProvider);
            }
            else
            {
                // TODO: handle formatProvider and localization
                Match match = Regex.Match(format, @"(M|k|G)?([0-9e\+\(\),\.\#]*)(l)?");

                if (match.Success)
                {
                    string unitsFlag = match.Groups[1].Value;
                    string numericFormat = match.Groups[2].Value;
                    string labelFlag = match.Groups[3].Value;

                    string output = string.Empty;
                    string units = string.Empty;

                    switch (unitsFlag)
                    {
                        case "M":
                            output = this.MHz.ToString(numericFormat, formatProvider);
                            units = Resources.MegahertzUnits;
                            break;
                        case "k":
                            output = this.KHz.ToString(numericFormat, formatProvider);
                            units = Resources.KilohertzUnits;
                            break;
                        case "G":
                            output = this.GHz.ToString(numericFormat, formatProvider);
                            units = Resources.GigahertzUnits;
                            break;
                        case "":
                            output = this.Hz.ToString(numericFormat, formatProvider);
                            break;
                    }

                    if (labelFlag == "l")
                    {
                        output += " " + units;
                    }

                    return output;
                }
                else
                {
                    return this.frequencyValue.ToString(format, formatProvider);
                }
            }
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