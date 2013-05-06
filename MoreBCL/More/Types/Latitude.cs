namespace More.Types
{
    using System;
    using System.ComponentModel;
    using More.Properties;

    /// <summary>
    /// Hemisphere enumeration.
    /// </summary>
    public enum LatitudeHemisphere
    {
        /// <summary>
        /// West hemisphere.
        /// </summary>
        North,

        /// <summary>
        /// East hemisphere.
        /// </summary>
        South,
    }

    /// <summary>
    /// Immutable value type for latitude based off of a <see cref="Angle" />.
    /// Defines units and adds conversion methods.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(LatitudeTypeConverter))]
    public struct Latitude : IComparable, IFormattable, IComparable<Latitude>, IEquatable<Latitude>
    {
        /// <summary>
        /// Maximum valid latitude.
        /// </summary>
        public static readonly Latitude MaxValue = FromDegrees(90);

        /// <summary>
        /// Minimum valid latitude.
        /// </summary>
        public static readonly Latitude MinValue = FromDegrees(-90);

        /// <summary>
        /// Positive hemisphere.
        /// </summary>
        private static LatitudeHemisphere positiveHemisphere = LatitudeHemisphere.North;

        /// <summary>
        /// Angle that represents Latitude.  The hemisphere is implicit from +/- sign of angle.
        /// </summary>
        private readonly Angle latitudeValue;

        /// <summary>
        /// Create Latitude from value.
        /// </summary>
        /// <param name="latitudeValue">Numeric value in degrees.</param>
        /// <exception cref="ArgumentException">Latitude must be between -90 and 90</exception>
        public Latitude(double latitudeValue)
        {
            if (latitudeValue < MinValue||
                latitudeValue > MaxValue)
            {
                throw new ArgumentException("Latitude must be between -90 and 90");
            }

            this.latitudeValue = latitudeValue;
        }

        /// <summary>
        /// Gets or sets the positive hemisphere.  By default and most standards this is north,
        /// but in some cultures it may be south.
        /// </summary>
        public static LatitudeHemisphere PositiveHemisphere
        {
            get { return positiveHemisphere; }
            set { positiveHemisphere = value; }
        }

        /// <summary>
        /// Gets the total degrees.
        /// </summary>
        public double TotalDegrees
        {
            get { return this.latitudeValue.TotalDegrees; }
        }

        /// <summary>
        /// Gets the degrees.
        /// </summary>
        public int Degrees
        {
            get { return this.latitudeValue.Degrees; }
        }

        /// <summary>
        /// Gets the hemisphere.
        /// </summary>
        /// <value>The hemisphere.</value>
        public LatitudeHemisphere Hemisphere
        {
            get
            {
                if (PositiveHemisphere == LatitudeHemisphere.North)
                {
                    return this.latitudeValue >= 0.0 ?
                        LatitudeHemisphere.North :
                        LatitudeHemisphere.South;
                }

                return this.latitudeValue >= 0.0 ?
                           LatitudeHemisphere.South :
                           LatitudeHemisphere.North;
            }
        }

        /// <summary>
        /// Gets the precise minutes.
        /// </summary>
        public double PreciseMinutes
        {
            get { return this.latitudeValue.PreciseMinutes; }
        }

        /// <summary>
        /// Gets the minutes.
        /// </summary>
        public int Minutes
        {
            get { return this.latitudeValue.Minutes; }
        }

        /// <summary>
        /// Gets the total minutes.
        /// </summary>
        public double TotalMinutes
        {
            get { return this.latitudeValue.TotalMinutes; }
        }

        /// <summary>
        /// Gets the precise seconds.
        /// </summary>
        public double PreciseSeconds
        {
            get { return this.latitudeValue.PreciseSeconds; }
        }

        /// <summary>
        /// Gets the seconds.
        /// </summary>
        public int Seconds
        {
            get { return this.latitudeValue.Seconds; }
        }

        /// <summary>
        /// Gets the total seconds.
        /// </summary>
        public double TotalSeconds
        {
            get { return this.latitudeValue.TotalSeconds; }
        }



        /// <summary>
        /// Performs an implicit conversion from 
        /// <see cref="System.Double"/> to <see cref="More.Types.Latitude"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Value out of bounds.</exception>
        public static implicit operator Latitude(double value)
        {
            return FromDegrees(value);
        }

        /// <summary>
        /// Performs an implicit conversion from 
        /// <see cref="More.Types.Latitude"/> to <see cref="System.Double"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator double(Latitude value)
        {
            return value.TotalDegrees;
        }

        /// <summary>
        /// Get Latitude.  Hemisphere is implicit from +/- sign of degrees.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <returns>Returns Latitude.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Value out of bounds.</exception>
        public static Latitude FromDegrees(double degrees)
        {
            return new Latitude(degrees);
        }

        /// <summary>
        /// Get Latitude.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <param name="hemisphere">The hemisphere.</param>
        /// <returns>Returns Latitude.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Value out of bounds.</exception>
        public static Latitude FromDegrees(double degrees, LatitudeHemisphere hemisphere)
        {
            if (degrees < 0.0)
            {
                throw new ArgumentException("degrees must be greater than 0");
            }

            return hemisphere == PositiveHemisphere ? degrees : -1.0 * degrees;
        }

        /// <summary>
        /// Get Latitude.  Hemisphere is implicit from +/- sign of degrees.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <param name="minutes">The minutes.</param>
        /// <returns>Returns Latitude.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Value out of bounds.</exception>
        public static Latitude FromDegreesMinutes(int degrees, double minutes)
        {
            return FromDegrees(degrees + (minutes / 60.0));
        }

        /// <summary>
        /// Froms the degrees minutes.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <param name="minutes">The minutes.</param>
        /// <param name="hemisphere">The hemisphere.</param>
        /// <returns>Returns Latitude.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Values out of bounds.</exception>
        public static Latitude FromDegreesMinutes(
            int degrees,
            double minutes,
            LatitudeHemisphere hemisphere)
        {
            return FromDegrees(degrees + (minutes / 60.0), hemisphere);
        }

        /// <summary>
        /// Froms the degrees minutes.  Hemisphere is implicit from +/- sign of degrees.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <param name="minutes">The minutes.</param>
        /// <param name="seconds">The seconds.</param>
        /// <returns>Returns Latitude.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Values out of bounds.</exception>
        public static Latitude FromDegreesMinutesSeconds(
            int degrees,
            int minutes,
            double seconds)
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
        /// <returns>Returns Latitude.</returns>
        /// <exception cref="ArgumentException">Values out of bounds.</exception>
        public static Latitude FromDegreesMinutesSeconds(
            int degrees,
            int minutes,
            double seconds,
            LatitudeHemisphere hemisphere)
        {
            return FromDegrees(degrees + (minutes / 60.0) + (seconds / 3600.0), hemisphere);
        }

        /// <summary>
        /// Parse a Latitude string, e.g. 54 (deg) 45' 23" or 54 (deg) 45.23' W.
        /// </summary>
        /// <param name="s">The string representation of a Latitude.</param>
        /// <returns>The result latitude.</returns>
        /// <exception cref="ArgumentNullException">An argument was null.</exception>
        /// <exception cref="FormatException">The text could not be parsed.</exception>
        public static Latitude Parse(string s)
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
        /// Tries to parse a Latitude string, e.g. 54 (deg) 45' 23" or 54 (deg) 45.23' W.
        /// </summary>
        /// <param name="s">The string representation of a Latitude.</param>
        /// <param name="result">The result latitude.</param>
        /// <returns>True if the parse was successful.</returns>
        public static bool TryParse(string s, out Latitude result)
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
            if (obj is Latitude)
            {
                return this.latitudeValue.CompareTo(((Latitude)obj).latitudeValue);
            }

            return this.latitudeValue.CompareTo(obj);
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
        public int CompareTo(Latitude other)
        {
            return this.latitudeValue.CompareTo((double)other);
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

            if (obj is Latitude)
            {
                Latitude temp = (Latitude)obj;

                if (temp.latitudeValue == this.latitudeValue)
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
        public bool Equals(Latitude other)
        {
            return other.latitudeValue.Equals(this.latitudeValue);
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
            return this.latitudeValue.GetHashCode();
        }

        /// <summary>
        /// Returns the string representation of the latitude.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> of the latitude.
        /// </returns>
        public override string ToString()
        {
            return this.latitudeValue.ToString();
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
            return this.latitudeValue.ToString(format, formatProvider);
        }
    }
}