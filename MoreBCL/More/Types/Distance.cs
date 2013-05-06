namespace More.Types
{
    using System;
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using More.Properties;

    /// <summary>
    /// Immutable value type for Distance based off of a <see cref="System.Double" />.
    /// Defines units and adds conversion methods.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(DistanceTypeConverter))]
    public struct Distance : IComparable, IFormattable, IComparable<Distance>, IEquatable<Distance>
    {
        /// <summary>
        /// Number of meters per foot (0.3048).
        /// </summary>
        private const double MetersPerFoot = MetersPerMile / 5280;

        /// <summary>
        /// Number of meters per mile.
        /// </summary>
        private const double MetersPerMile = 1609.344;

        /// <summary>
        /// Distance stored in meters.
        /// </summary>
        private double meters;

        /// <summary>
        /// Initializes a new instance of the Distance struct.
        /// </summary>
        /// <param name="value">Numeric value in meters.</param>
        public Distance(double value)
        {
            this.meters = value;
        }

        /// <summary>
        /// Gets value in feet.
        /// </summary>
        public double Feet
        {
            get { return this.meters / MetersPerFoot; }
        }

        /// <summary>
        /// Gets value in kilometers.
        /// </summary>
        public double Kilometers
        {
            get { return this.meters / 1000.0; }
        }

        /// <summary>
        /// Gets value in meters.
        /// </summary>
        public double Meters
        {
            get { return this.meters; }
        }

        /// <summary>
        /// Gets value in miles.
        /// </summary>
        public double Miles
        {
            get { return this.meters / MetersPerMile; }
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="value">Value of Distance in meters.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator Distance(double value)
        {
            return new Distance(value);
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="value">Value of Distance in meters.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator Distance(float value)
        {
            return new Distance(value);
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="value">Value of Distance in meters.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator Distance(int value)
        {
            return new Distance(value);
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="value">Value of Distance in meters.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator Distance(long value)
        {
            return new Distance(value);
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="value">Value of Distance in meters.</param>
        /// <returns>Returns casted value.</returns>
        public static explicit operator Distance(decimal value)
        {
            return new Distance((double)value);
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="distance">Value of Distance.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator double(Distance distance)
        {
            return distance.Meters;
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="distance">Value of Distance.</param>
        /// <returns>Returns casted value.</returns>
        public static explicit operator long(Distance distance)
        {
            return (long)distance.Meters;
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="distance">Value of Distance.</param>
        /// <returns>Returns casted value.</returns>
        public static explicit operator int(Distance distance)
        {
            return (int)distance.Meters;
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="distance">Value of Distance.</param>
        /// <returns>Returns casted value.</returns>
        public static explicit operator float(Distance distance)
        {
            return (float)distance.Meters;
        }

        /// <summary>
        /// Cast operator.
        /// </summary>
        /// <param name="distance">Value of Distance.</param>
        /// <returns>Returns casted value.</returns>
        public static implicit operator decimal(Distance distance)
        {
            return (decimal)distance.Meters;
        }

        /// <summary>
        /// Not equal operator.
        /// </summary>
        /// <param name="distance1">LH Distance.</param>
        /// <param name="distance2">RH Distance.</param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator !=(Distance distance1, Distance distance2)
        {
            return distance1.meters != distance2.meters;
        }

        /// <summary>
        /// Multiplication operator.
        /// </summary>
        /// <param name="distance1">LH Distance.</param>
        /// <param name="distance2">RH Distance.</param>
        /// <returns>Returns arithmetic result.</returns>
        public static Distance operator *(Distance distance1, Distance distance2)
        {
            return new Distance(distance1.meters * distance2.meters);
        }

        /// <summary>
        /// Add operator.
        /// </summary>
        /// <param name="distance1">LH Distance.</param>
        /// <param name="distance2">RH Distance.</param>
        /// <returns>Returns arithmetic result.</returns>
        public static Distance operator +(Distance distance1, Distance distance2)
        {
            return new Distance(distance1.meters + distance2.meters);
        }

        /// <summary>
        /// Increment operator.
        /// </summary>
        /// <param name="distance1">LH Distance.</param>
        /// <returns>Returns arithmetic result.</returns>
        public static Distance operator ++(Distance distance1)
        {
            return new Distance(distance1.meters++);
        }

        /// <summary>
        /// Subtract operator.
        /// </summary>
        /// <param name="distance1">LH Distance.</param>
        /// <param name="distance2">RH Distance.</param>
        /// <returns>Returns arithmetic result.</returns>
        public static Distance operator -(Distance distance1, Distance distance2)
        {
            return new Distance(distance1.meters - distance2.meters);
        }

        /// <summary>
        /// Decrement operator.
        /// </summary>
        /// <param name="distance1">LH Distance.</param>
        /// <returns>Returns arithmetic result.</returns>
        public static Distance operator --(Distance distance1)
        {
            return new Distance(distance1.meters++);
        }

        /// <summary>
        /// Division operator.
        /// </summary>
        /// <param name="distance1">LH Distance.</param>
        /// <param name="distance2">RH Distance.</param>
        /// <returns>Returns arithmetic result.</returns>
        public static Distance operator /(Distance distance1, Distance distance2)
        {
            return new Distance(distance1.meters / distance2.meters);
        }

        /// <summary>
        /// Less than operator.
        /// </summary>
        /// <param name="distance1">LH Distance.</param>
        /// <param name="distance2">RH Distance.</param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator <(Distance distance1, Distance distance2)
        {
            return distance1.meters < distance2.meters;
        }

        /// <summary>
        /// Less than or equal operator.
        /// </summary>
        /// <param name="distance1">LH Distance.</param>
        /// <param name="distance2">RH Distance.</param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator <=(Distance distance1, Distance distance2)
        {
            return distance1.meters <= distance2.meters;
        }

        /// <summary>
        /// Equal operator.
        /// </summary>
        /// <param name="distance1">LH Distance.</param>
        /// <param name="distance2">RH Distance.</param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator ==(Distance distance1, Distance distance2)
        {
            return distance1.meters == distance2.meters;
        }

        /// <summary>
        /// Greater than operator.
        /// </summary>
        /// <param name="distance1">LH Distance.</param>
        /// <param name="distance2">RH Distance.</param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator >(Distance distance1, Distance distance2)
        {
            return distance1.meters > distance2.meters;
        }

        /// <summary>
        /// Greater than or equal operator.
        /// </summary>
        /// <param name="distance1">LH Distance.</param>
        /// <param name="distance2">RH Distance.</param>
        /// <returns>Return true if condition met, otherwise false.</returns>
        public static bool operator >=(Distance distance1, Distance distance2)
        {
            return distance1.meters >= distance2.meters;
        }

        /// <summary>
        /// Convert Distance.
        /// </summary>
        /// <param name="value">Value of Distance.</param>
        /// <returns>Converted Distance.</returns>
        public static double FeetToMeters(double value)
        {
            return value * MetersPerFoot;
        }

        /// <summary>
        /// Convert Distance.
        /// </summary>
        /// <param name="value">Value of Distance.</param>
        /// <returns>Distance from converted value.</returns>
        public static Distance FromFeet(double value)
        {
            return Distance.FeetToMeters(value);
        }

        /// <summary>
        /// Convert Distance.
        /// </summary>
        /// <param name="value">Value of Distance.</param>
        /// <returns>Distance from converted value.</returns>
        public static Distance FromKilometers(double value)
        {
            return Distance.KilometersToMeters(value);
        }

        /// <summary>
        /// Convert Distance.
        /// </summary>
        /// <param name="value">Value of Distance.</param>
        /// <returns>Distance from converted value.</returns>
        public static Distance FromMeters(double value)
        {
            return value;
        }

        /// <summary>
        /// Convert Distance.
        /// </summary>
        /// <param name="value">Value of Distance.</param>
        /// <returns>Distance from converted value.</returns>
        public static Distance FromMiles(double value)
        {
            return Distance.MilesToMeters(value);
        }

        /// <summary>
        /// Convert Distance.
        /// </summary>
        /// <param name="value">Value of Distance.</param>
        /// <returns>Converted Distance.</returns>
        public static double KilometersToMeters(double value)
        {
            return value * 1000.0;
        }

        /// <summary>
        /// Convert Distance.
        /// </summary>
        /// <param name="value">Value of Distance.</param>
        /// <returns>Converted Distance.</returns>
        public static double MilesToMeters(double value)
        {
            return value * MetersPerMile;
        }

        /// <summary>
        /// Parse a Distance string, e.g. 23 km or 54.45 ft.
        /// </summary>
        /// <param name="s">The string representation of a Distance.</param>
        /// <returns>The result Distance.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        public static Distance Parse(string s)
        {
            try
            {
                // Currently parses the simplest form.  May be expanded later.
                return Distance.FromMeters(double.Parse(s));
            }
            catch
            {
                throw new FormatException(Resources.NumericValueRequired);
            }
        }

        /// <summary>
        /// Tries to parse a Distance string, e.g. 23 km or 54.45 ft.
        /// </summary>
        /// <param name="s">The string representation of a Distance.</param>
        /// <param name="result">The result Distance.</param>
        /// <returns>True if the parse was successful.</returns>
        public static bool TryParse(string s, out Distance result)
        {
            bool ok = false;

            try
            {
                result = Parse(s);
                ok = true;
            }
            catch
            {
                result = Distance.FromKilometers(0.0);
            }

            return ok;
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns the computed value.</returns>
        public Distance Add(Distance value)
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
            if (obj is Distance)
            {
                return this.meters.CompareTo(((Distance)obj).meters);
            }

            return this.meters.CompareTo(obj);
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
        public int CompareTo(Distance other)
        {
            return this.meters.CompareTo((double)other);
        }

        /// <summary>
        /// Decrements the value.
        /// </summary>
        /// <returns>Returns the computed value.</returns>
        public Distance Decrement()
        {
            return this++;
        }

        /// <summary>
        /// Divides the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns the computed value.</returns>
        public Distance Divide(Distance value)
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

            if (obj is Distance)
            {
                Distance temp = (Distance)obj;

                if (temp.meters == this.meters)
                {
                    equal = true;
                }
            }

            return equal;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object 
        /// of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; 
        /// otherwise, false.
        /// </returns>
        public bool Equals(Distance other)
        {
            return other.meters.Equals(this.meters);
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
            return this.meters.GetHashCode();
        }

        /// <summary>
        /// Increments the value.
        /// </summary>
        /// <returns>Returns the computed value.</returns>
        public Distance Increment()
        {
            return this++;
        }

        /// <summary>
        /// Multiplies the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns the computed value.</returns>
        public Distance Multiply(Distance value)
        {
            return this * value;
        }

        /// <summary>
        /// Subtracts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns the computed value.</returns>
        public Distance Subtract(Distance value)
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
            return this.meters.ToString();
        }

        /// <summary>
        /// Formats the value of the current instance using the specified format.
        /// Formatting same as double except a units and label specifier may
        /// be used.  Prefix format with m, k, f, M for meters, kilometers,
        /// feet, or mile units and postfix with l to show units label.
        /// </summary>
        /// <param name="format">The <see cref="T:System.String"></see> 
        /// specifying the format to use.-or- null to use the default format
        /// defined for the type of the
        /// <see cref="T:System.IFormattable"></see> implementation.</param>
        /// <param name="formatProvider">The <see cref="T:System.IFormatProvider"></see> to use to format the value.-or- null to obtain the numeric format information from the current locale setting of the operating system.</param>
        /// <returns>
        /// A <see cref="T:System.String"></see> containing the value of 
        /// the current instance in the specified format.
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                return this.meters.ToString(format, formatProvider);
            }
            else
            {
                // TODO: handle formatProvider and localization
                Match match = Regex.Match(format, @"(m|k|f|M)?([0-9e\+\(\),\.\#]*)(l)?");

                if (match.Success)
                {
                    string unitsFlag = match.Groups[1].Value;
                    string numericFormat = match.Groups[2].Value;
                    string labelFlag = match.Groups[3].Value;

                    string output = string.Empty;
                    string units = string.Empty;

                    switch (unitsFlag)
                    {
                        case "m":
                            output = this.Meters.ToString(
                                numericFormat,
                                formatProvider);
                            units = Resources.MeterUnits;
                            break;
                        case "k":
                            output = this.Kilometers.ToString(
                                numericFormat,
                                formatProvider);
                            units = Resources.KilometerUnits;
                            break;
                        case "f":
                            output = this.Feet.ToString(
                                numericFormat,
                                formatProvider);
                            units = Resources.FootUnits;
                            break;
                        case "M":
                            output = this.Miles.ToString(
                                numericFormat,
                                formatProvider);
                            units = Resources.MileUnits;
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
                    return this.meters.ToString(format, formatProvider);
                }
            }
        }

        /// <summary>
        /// Formats the value of the current instance using the specified format.
        /// </summary>
        /// <param name="format">The <see cref="T:System.String"></see> specifying
        /// the format to use.-or- null to use the default format defined for 
        /// the type of the <see cref="T:System.IFormattable"></see> 
        /// implementation.</param>
        /// <returns>
        /// A <see cref="T:System.String"></see> containing the value of the 
        /// current instance in the specified format.
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
        /// A <see cref="T:System.String"></see> containing the value of 
        /// the current instance in the specified format.
        /// </returns>
        public string ToString(IFormatProvider provider)
        {
            return this.ToString(null, provider);
        }
    }
}