namespace More.Types
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using More.Properties;

    /// <summary>
    /// Immutable value type for angle based off of a <see cref="System.Double" />.
    /// Defines units and adds conversion methods as well as
    /// handles arithmetic either constained from 0 to 360 or not.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(AngleTypeConverter))]
    public struct Angle : IComparable, IFormattable, IComparable<Angle>, IEquatable<Angle>
    {
        /// <summary>
        /// Degrees symbol.
        /// </summary>
        public const char DegreeSymbol = (char)176;

        /// <summary>
        /// Total degrees in a circle
        /// </summary>
        public const double DegressInACircle = 360.0;

        /// <summary>
        /// Maximum angle when degrees run from 0 to 360.
        /// </summary>
        public const double MaximumAngle = 360.0;

        /// <summary>
        /// Minimum angle when degrees run from 0 to 360.
        /// </summary>
        public const double MinimumAngle = 0.0;

        /// <summary>
        /// Angle stored in degrees.
        /// </summary>
        private double angleValue;

        /// <summary>
        /// Create angle from value.
        /// </summary>
        /// <param name="angleValue">Numeric value in degrees.</param>
        public Angle(double angleValue)
        {
            this.angleValue = angleValue;
        }

        /// <summary>
        /// Gets the degrees.
        /// </summary>
        public double TotalDegrees
        {
            get { return this.angleValue; }
        }

        /// <summary>
        /// Gets the total angle in radians.
        /// </summary>
        public double Radians
        {
            get { return this.angleValue * Math.PI / 180.0; }
        }

        /// <summary>
        /// Gets the total degrees.
        /// </summary>
        public int Degrees
        {
            get { return (int)Math.Round(this.TotalDegrees); }
        }

        /// <summary>
        /// Gets the precise minutes.
        /// </summary>
        public double PreciseMinutes
        {
            get { return Math.Abs((int)this.angleValue - this.angleValue) / (1.0 / 60.0); }
        }

        /// <summary>
        /// Gets the total minutes.
        /// </summary>
        public double TotalMinutes
        {
            get { return this.angleValue / (1.0 / 60.0); }
        }

        /// <summary>
        /// Gets the minutes.
        /// </summary>
        public int Minutes
        {
            get { return (int)Math.Round(this.PreciseMinutes); }
        }

        /// <summary>
        /// Gets the precise seconds.
        /// </summary>
        public double PreciseSeconds
        {
            get { return Math.Abs(this.Minutes - this.Minutes) / (1.0 / 60.0); }
        }

        /// <summary>
        /// Gets the seconds.
        /// </summary>
        public int Seconds
        {
            get { return (int)Math.Round(this.PreciseSeconds); }
        }

        /// <summary>
        /// Gets the total seconds.
        /// </summary>
        public double TotalSeconds
        {
            get { return this.TotalMinutes / (1.0 / 60.0); }
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Double"/> to 
        /// <see cref="More.Types.Angle"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Angle(double value)
        {
            return new Angle(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Single"/> to 
        /// <see cref="More.Types.Angle"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Angle(float value)
        {
            return new Angle(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Int32"/> to 
        /// <see cref="More.Types.Angle"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Angle(int value)
        {
            return new Angle(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Int64"/>
        /// to <see cref="More.Types.Angle"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Angle(long value)
        {
            return new Angle(value);
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="System.Decimal"/> 
        /// to <see cref="More.Types.Angle"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Angle(decimal value)
        {
            return new Angle((double)value);
        }

        /// <summary>
        /// Performs an implicit conversion from 
        /// <see cref="More.Types.Angle"/> to <see cref="System.Double"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator double(Angle value)
        {
            return value.TotalDegrees;
        }

        /// <summary>
        /// Performs an explicit conversion from 
        /// <see cref="More.Types.Angle"/> to <see cref="System.Int64"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator long(Angle value)
        {
            return (long)value.TotalDegrees;
        }

        /// <summary>
        /// Performs an explicit conversion from 
        /// <see cref="More.Types.Angle"/> to <see cref="System.Int32"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator int(Angle value)
        {
            return (int)value.TotalDegrees;
        }

        /// <summary>
        /// Performs an explicit conversion from 
        /// <see cref="More.Types.Angle"/> to <see cref="System.Single"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator float(Angle value)
        {
            return (float)value.TotalDegrees;
        }

        /// <summary>
        /// Performs an implicit conversion from 
        /// <see cref="More.Types.Angle"/> to <see cref="System.Decimal"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator decimal(Angle value)
        {
            return (decimal)value.TotalDegrees;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="leftHand">The left hand.</param>
        /// <param name="rightHand">The right hand.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Angle leftHand, Angle rightHand)
        {
            return leftHand.angleValue != rightHand.angleValue;
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="leftHand">The left hand.</param>
        /// <param name="rightHand">The right hand.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator *(Angle leftHand, Angle rightHand)
        {
            return new Angle(leftHand.angleValue * rightHand.angleValue);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="leftHand">The left hand.</param>
        /// <param name="rightHand">The right hand.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator +(Angle leftHand, Angle rightHand)
        {
            return new Angle(leftHand.angleValue + rightHand.angleValue);
        }

        /// <summary>
        /// Implements the operator ++.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator ++(Angle value)
        {
            return new Angle(value.angleValue++);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="leftHand">The left hand.</param>
        /// <param name="rightHand">The right hand.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator -(Angle leftHand, Angle rightHand)
        {
            return new Angle(leftHand.angleValue - rightHand.angleValue);
        }

        /// <summary>
        /// Implements the operator --.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator --(Angle value)
        {
            return new Angle(value.angleValue++);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="leftHand">The left hand.</param>
        /// <param name="rightHand">The right hand.</param>
        /// <returns>The result of the operator.</returns>
        public static Angle operator /(Angle leftHand, Angle rightHand)
        {
            return new Angle(leftHand.angleValue / rightHand.angleValue);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="leftHand">The left hand.</param>
        /// <param name="rightHand">The right hand.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(Angle leftHand, Angle rightHand)
        {
            return leftHand.angleValue < rightHand.angleValue;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="leftHand">The left hand.</param>
        /// <param name="rightHand">The right hand.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(Angle leftHand, Angle rightHand)
        {
            return leftHand.angleValue <= rightHand.angleValue;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="leftHand">The left hand.</param>
        /// <param name="rightHand">The right hand.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Angle leftHand, Angle rightHand)
        {
            return leftHand.angleValue == rightHand.angleValue;
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="leftHand">The left hand.</param>
        /// <param name="rightHand">The right hand.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(Angle leftHand, Angle rightHand)
        {
            return leftHand.angleValue > rightHand.angleValue;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="leftHand">The left hand.</param>
        /// <param name="rightHand">The right hand.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(Angle leftHand, Angle rightHand)
        {
            return leftHand.angleValue >= rightHand.angleValue;
        }

        /// <summary>
        /// Convert Angle.
        /// </summary>
        /// <param name="value">Value of Angle.</param>
        /// <returns>Angle from converted value.</returns>
        public static Angle FromDegrees(double value)
        {
            return value;
        }

        /// <summary>
        /// Convert Angle.
        /// </summary>
        /// <param name="value">Value of Angle.</param>
        /// <returns>Angle from converted value.</returns>
        public static Angle FromRadians(double value)
        {
            return value * 180.0 / Math.PI;
        }

        /// <summary>
        /// Tries to parse a angle string.
        /// </summary>
        /// <param name="s">The string representation of an angle.</param>
        /// <returns>The result angle.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        public static Angle Parse(string s)
        {
            try
            {
                // Currently parses the simplest form.  May be expanded later.
                return FromDegrees(double.Parse(s));
            }
            catch
            {
                throw new FormatException(Resources.NumericValueRequired);
            }
        }

        /// <summary>
        /// Tries to parse a angle string.
        /// </summary>
        /// <param name="s">The string representation of an angle.</param>
        /// <param name="result">The result angle.</param>
        /// <returns>True if the parse was successful.</returns>
        public static bool TryParse(string s, out Angle result)
        {
            bool ok = false;

            try
            {
                result = Parse(s);
                ok = true;
            }
            catch
            {
                result = FromDegrees(0.0);
            }

            return ok;
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
            if (obj is Angle)
            {
                return this.angleValue.CompareTo(((Angle)obj).angleValue);
            }

            return this.angleValue.CompareTo(obj);
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
        public int CompareTo(Angle other)
        {
            return this.angleValue.CompareTo(other);
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

            if (obj is Angle)
            {
                var temp = (Angle)obj;

                if (temp.angleValue == this.angleValue)
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
        public bool Equals(Angle other)
        {
            return other.angleValue.Equals(this.angleValue);
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
            return this.angleValue.GetHashCode();
        }

        /// <summary>
        /// Convert to the degrees, minutes, seconds string, e.g. 120 (deg) 45' 23.34"
        /// </summary>
        /// <returns>String representation.</returns>
        public string ToDegreesMinutesSeccondsString()
        {
            return string.Format(
                "{0}{1} {2}' {3:0.00}\"",
                this.Degrees,
                DegreeSymbol,
                this.Minutes,
                this.PreciseSeconds);
        }

        /// <summary>
        /// Convert to the degrees, minutes, seconds string, e.g. 120 (deg) 45.56'
        /// </summary>
        /// <returns>String representation.</returns>
        public string ToDegreesMinutesString()
        {
            return string.Format(
                "{0}{1} {2:0.00}'\"",
                this.Degrees,
                DegreeSymbol,
                this.PreciseMinutes);
        }

        /// <summary>
        /// Convert to the degrees, e.g. 120.123(deg)
        /// </summary>
        /// <returns>String representation.</returns>
        public string ToDegreesString()
        {
            return string.Format(
                "{0:0.000}{1}",
                this.TotalDegrees,
                DegreeSymbol);
        }

        /// <summary>
        /// Convert to radians string, e.g. 1.75 rad.
        /// </summary>
        /// <returns>String representation.</returns>
        public string ToRadiansString()
        {
            return string.Format("{0:0.00} rad", this.Radians);
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
            return this.angleValue.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convert numeric value to string representation.
        /// </summary>
        /// <param name="format">String format.</param>
        /// <param name="provider">Format provider.</param>
        /// <returns>String representation.</returns>
        public string ToString(string format, IFormatProvider provider)
        {
            return this.angleValue.ToString(format, provider);
        }

        /// <summary>
        /// Convert numeric value to string representation.
        /// </summary>
        /// <param name="provider">Format provider.</param>
        /// <returns>String representation.</returns>
        public string ToString(IFormatProvider provider)
        {
            return this.angleValue.ToString(provider);
        }

        /// <summary>
        /// Convert numeric value to string representation.
        /// </summary>
        /// <param name="format">String format.</param>
        /// <returns>String representation.</returns>
        public string ToString(string format)
        {
            return this.angleValue.ToString(format);
        }
    }
}