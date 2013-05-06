namespace More.Types
{
    using System;
    using System.ComponentModel;
    using More.Properties;

    /// <summary>
    /// Hemisphere enumeration.
    /// </summary>
    public enum LongitudeHemisphere
    {
        /// <summary>
        /// West hemisphere.
        /// </summary>
        West,

        /// <summary>
        /// East hemisphere.
        /// </summary>
        East,
    }

    /// <summary>
    /// Immutable value type for longitude based off of a <see cref="Angle" />.
    /// Defines units and adds conversion methods.
    /// </summary>
    /// <remarks>Valid longitudes run from -180 degrees to 180 degrees.
    /// Negative values are in the western hemisphere and positive values
    /// are in the eastern hemisphere.
    /// </remarks>
    [Serializable]
    [TypeConverter(typeof(LongitudeTypeConverter))]
    public struct Longitude : IComparable, IFormattable, IComparable<Longitude>, IEquatable<Longitude>
    {
        /// <summary>
        /// Maximum valid longitude
        /// </summary>
        public static readonly Longitude MaxValue = FromDegrees(180.0);

        /// <summary>
        /// Minimum valid longitude
        /// </summary>
        public static readonly Longitude MinValue = FromDegrees(-180.0);

        /// <summary>
        /// Positive hemisphere.
        /// </summary>
        private static LongitudeHemisphere positiveHemisphere = LongitudeHemisphere.East;

        /// <summary>
        /// Angle that represents longitude.  The hemisphere is implicit from +/- sign of angle.
        /// </summary>
        private readonly Angle longitudeValue;

        /// <summary>
        /// Create Longitude from value.
        /// </summary>
        /// <param name="longitudeValue">Numeric value in degrees.</param>
        /// <exception cref="ArgumentException">Latitude must be between -180 and 180</exception>
        public Longitude(double longitudeValue)
        {
            if (longitudeValue < MinValue || 
                longitudeValue > MaxValue)
            {
                throw new ArgumentException("Latitude must be between -180 and 180");
            }

            this.longitudeValue = longitudeValue;
        }

        /// <summary>
        /// Gets or sets the positive hemisphere.  By default and most standards this is east,
        /// but in some cultures it may be west.
        /// </summary>
        public static LongitudeHemisphere PositiveHemisphere
        {
            get { return positiveHemisphere; }
            set { positiveHemisphere = value; }
        }

        /// <summary>
        /// Gets the total degrees.
        /// </summary>
        public double TotalDegrees
        {
            get { return this.longitudeValue.TotalDegrees; }
        }

        /// <summary>
        /// Gets the degrees.
        /// </summary>
        public int Degrees
        {
            get { return this.longitudeValue.Degrees; }
        }

        /// <summary>
        /// Gets the hemisphere.
        /// </summary>
        public LongitudeHemisphere Hemisphere
        {
            get
            {
                if (PositiveHemisphere == LongitudeHemisphere.East)
                {
                    return this.longitudeValue >= 0.0 ?
                        LongitudeHemisphere.East :
                        LongitudeHemisphere.West;
                }
                else
                {
                    return this.longitudeValue >= 0.0 ?
                        LongitudeHemisphere.West :
                        LongitudeHemisphere.East;
                }
            }
        }

        /// <summary>
        /// Gets the precise minutes.
        /// </summary>
        public double PreciseMinutes
        {
            get { return this.longitudeValue.PreciseMinutes; }
        }

        /// <summary>
        /// Gets the minutes.
        /// </summary>
        public int Minutes
        {
            get { return this.longitudeValue.Minutes; }
        }

        /// <summary>
        /// Gets the total minutes.
        /// </summary>
        public double TotalMinutes
        {
            get { return this.longitudeValue.TotalMinutes; }
        }

        /// <summary>
        /// Gets the precise seconds.
        /// </summary>
        public double PreciseSeconds
        {
            get { return this.longitudeValue.PreciseSeconds; }
        }

        /// <summary>
        /// Gets the seconds.
        /// </summary>
        public int Seconds
        {
            get { return this.longitudeValue.Seconds; }
        }

        /// <summary>
        /// Gets the total seconds.
        /// </summary>
        public double TotalSeconds
        {
            get { return this.longitudeValue.TotalSeconds; }
        }
       
        /// <summary>
        /// Performs an implicit conversion from 
        /// <see cref="System.Double"/> to <see cref="More.Types.Longitude"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Longitude(double value)
        {
            return FromDegrees(value);
        }

        /// <summary>
        /// Performs an implicit conversion from 
        /// <see cref="More.Types.Longitude"/> to <see cref="System.Double"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator double(Longitude value)
        {
            return value.TotalDegrees;
        }

        /// <summary>
        /// Get longitude.  Hemisphere is implicit from +/- sign of degrees.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <returns>Returns longitude.</returns>
        /// <exception cref="ArgumentException">Values out of bounds.</exception>
        public static Longitude FromDegrees(double degrees)
        {
            return new Longitude(degrees);
        }

        /// <summary>
        /// Get longitude.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <param name="hemisphere">The hemisphere.</param>
        /// <returns>Returns longitude.</returns>
        /// <exception cref="ArgumentException">Values out of bounds.</exception>
        public static Longitude FromDegrees(double degrees, LongitudeHemisphere hemisphere)
        {
            if (degrees < 0.0)
            {
                throw new ArgumentException("degrees must be greater than 0");
            }

            return hemisphere == PositiveHemisphere ? degrees : -1.0 * degrees;
        }

        /// <summary>
        /// Get longitude.  Hemisphere is implicit from +/- sign of degrees.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <param name="minutes">The minutes.</param>
        /// <returns>Returns longitude.</returns>
        /// <exception cref="ArgumentException">Values out of bounds.</exception>
        public static Longitude FromDegreesMinutes(int degrees, double minutes)
        {
            return FromDegrees(degrees + (minutes / 60.0));
        }

        /// <summary>
        /// Froms the degrees minutes.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <param name="minutes">The minutes.</param>
        /// <param name="hemisphere">The hemisphere.</param>
        /// <returns>Returns longitude.</returns>
        /// <exception cref="ArgumentException">Values out of bounds.</exception>
        public static Longitude FromDegreesMinutes(int degrees, double minutes, LongitudeHemisphere hemisphere)
        {
            return FromDegrees(degrees + (minutes / 60.0), hemisphere);
        }

        /// <summary>
        /// Froms the degrees minutes.  Hemisphere is implicit from +/- sign of degrees.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <param name="minutes">The minutes.</param>
        /// <param name="seconds">The seconds.</param>
        /// <returns>Returns longitude.</returns>
        /// <exception cref="ArgumentException">Values out of bounds.</exception>
        public static Longitude FromDegreesMinutes(int degrees, int minutes, double seconds)
        {
            return FromDegrees(degrees + (minutes / 60.0) + (seconds / 3600.0));
        }

        /// <summary>
        /// Froms the degrees minutes seconds.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <param name="minutes">The minutes.</param>
        /// <param name="seconds">The seconds.</param>
        /// <param name="hemisphere">The hemisphere.</param>
        /// <returns>Returns longitude.</returns>
        /// <exception cref="ArgumentException">Values out of bounds.</exception>
        public static Longitude FromDegreesMinutesSeconds(int degrees, int minutes, double seconds, LongitudeHemisphere hemisphere)
        {
            return FromDegrees(degrees + (minutes / 60.0) + (seconds / 3600.0), hemisphere);
        }

        /// <summary>
        /// Parse a longitude string, e.g. 54 (deg) 45' 23" or 54 (deg) 45.23' W.
        /// </summary>
        /// <param name="s">The string representation of a longitude.</param>
        /// <returns>The result longitude.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        public static Longitude Parse(string s)
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
        /// Tries to parse a longitude string, e.g. 54 (deg) 45' 23" or 54 (deg) 45.23' W.
        /// </summary>
        /// <param name="s">The string representation of a longitude.</param>
        /// <param name="result">The result longitude.</param>
        /// <returns>True if the parse was successful.</returns>
        public static bool TryParse(string s, out Longitude result)
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
            if (obj is Longitude)
            {
                return this.longitudeValue.CompareTo(((Longitude)obj).longitudeValue);
            }

            return this.longitudeValue.CompareTo(obj);
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
        public int CompareTo(Longitude other)
        {
            return this.longitudeValue.CompareTo((double)other);
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

            if (obj is Longitude)
            {
                Longitude temp = (Longitude)obj;

                if (temp.longitudeValue == this.longitudeValue)
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
        public bool Equals(Longitude other)
        {
            return other.longitudeValue.Equals(this.longitudeValue);
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
            return this.longitudeValue.GetHashCode();
        }

        /// <summary>
        /// Checks to see if a longitude falls in a specified range.
        /// The range is checked in a circular fashion (for example, 170 is
        /// between 160 and -170.
        /// </summary>
        /// <param name="start">The initial longitude.</param>
        /// <param name="end">The final longitude</param>
        /// <returns>Returns true if the longitude is in the 
        /// specified range.</returns>
        public bool IsBetween(Longitude start, Longitude end)
        {
            bool between = false;

            if (start <= end)
            {
                between = (this >= start) && (this <= end);
            }
            else
            {
                if ((this >= start) && (this.longitudeValue <= MaxValue))
                {
                    between = true;
                }
                else if ((this.longitudeValue.TotalDegrees >= MinValue) && (this <= end))
                {
                    between = true;
                }
            }

            return between;
        }

        /// <summary>
        /// Returns the string representation of the longitude.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> of the longitude.
        /// </returns>
        public override string ToString()
        {
            return this.longitudeValue.ToString();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return this.longitudeValue.ToString(format, formatProvider);
        }
    }
}