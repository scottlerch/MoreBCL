﻿namespace More.Types
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    /// <summary>
    /// Longitude converter class.
    /// </summary>
    public class LongitudeTypeConverter : TypeConverter
    {
        /// <summary>
        /// Returns whether this converter can convert an object of the given 
        /// type to the type of this converter, using the specified context.
        /// </summary>
        /// <param name="context">An 
        /// <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that 
        /// provides a format context.</param>
        /// <param name="sourceType">A <see cref="T:System.Type"/> that 
        /// represents the type you want to convert from.</param>
        /// <returns>
        /// true if this converter can perform the conversion; otherwise, false.
        /// </returns>
        public override bool CanConvertFrom(
            ITypeDescriptorContext context,
            Type sourceType)
        {
            if (sourceType == typeof(string) ||
                sourceType == typeof(double) ||
                sourceType == typeof(float) ||
                sourceType == typeof(decimal) ||
                sourceType == typeof(short) ||
                sourceType == typeof(int) ||
                sourceType == typeof(long) ||
                sourceType == typeof(uint) ||
                sourceType == typeof(ulong) ||
                sourceType == typeof(ushort))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Converts the given object to the type of this converter, using the 
        /// specified context and culture information.
        /// </summary>
        /// <param name="context">An 
        /// <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that 
        /// provides a format context.</param>
        /// <param name="culture">The 
        /// <see cref="T:System.Globalization.CultureInfo"/> to use as the 
        /// current culture.</param>
        /// <param name="value">The <see cref="T:System.Object"/> to convert.
        /// </param>
        /// <returns>
        /// An <see cref="T:System.Object"/> that represents the converted value.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The conversion cannot be performed.
        /// </exception>
        public override object ConvertFrom(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value)
        {
            if (value is string)
            {
                return Longitude.Parse((string)value);
            }

            if (value is double ||
                value is float ||
                value is decimal ||
                value is short ||
                value is int ||
                value is long ||
                value is ushort ||
                value is uint ||
                value is ulong)
            {
                return Longitude.FromDegrees(Convert.ToDouble(value));
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Returns whether this converter can convert the object to the specified type, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="destinationType">A <see cref="T:System.Type"/> that represents the type you want to convert to.</param>
        /// <returns>
        /// true if this converter can perform the conversion; otherwise, false.
        /// </returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string) ||
                destinationType == typeof(double) ||
                destinationType == typeof(float) ||
                destinationType == typeof(decimal) ||
                destinationType == typeof(short) ||
                destinationType == typeof(int) ||
                destinationType == typeof(long) ||
                destinationType == typeof(uint) ||
                destinationType == typeof(ulong) ||
                destinationType == typeof(ushort))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the given value object to the specified type, using the
        /// specified context and culture information.
        /// </summary>
        /// <param name="context">An 
        /// <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that 
        /// provides a format context.</param>
        /// <param name="culture">A 
        /// <see cref="T:System.Globalization.CultureInfo"/>. If null is passed,
        /// the current culture is assumed.</param>
        /// <param name="value">The <see cref="T:System.Object"/> to convert.
        /// </param>
        /// <param name="destinationType">The <see cref="T:System.Type"/> to 
        /// convert the <paramref name="value"/> parameter to.</param>
        /// <returns>
        /// An <see cref="T:System.Object"/> that represents the converted value.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="destinationType"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The conversion cannot be performed.
        /// </exception>
        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destinationType)
        {
            if (value is Longitude)
            {
                if (destinationType == typeof(string))
                {
                    return ((Longitude)value).ToString();
                }

                if (destinationType == typeof(double))
                {
                    return Convert.ToDouble(((Longitude)value).TotalDegrees);
                }

                if (destinationType == typeof(float))
                {
                    return Convert.ToSingle(((Longitude)value).TotalDegrees);
                }

                if (destinationType == typeof(decimal))
                {
                    return Convert.ToDecimal(((Longitude)value).TotalDegrees);
                }

                if (destinationType == typeof(short))
                {
                    return Convert.ToInt16(((Longitude)value).TotalDegrees);
                }

                if (destinationType == typeof(int))
                {
                    return Convert.ToInt32(((Longitude)value).TotalDegrees);
                }

                if (destinationType == typeof(long))
                {
                    return Convert.ToInt64(((Longitude)value).TotalDegrees);
                }

                if (destinationType == typeof(uint))
                {
                    return Convert.ToUInt32(((Longitude)value).TotalDegrees);
                }

                if (destinationType == typeof(ulong))
                {
                    return Convert.ToUInt64(((Longitude)value).TotalDegrees);
                }

                if (destinationType == typeof(ushort))
                {
                    return Convert.ToUInt16(((Longitude)value).TotalDegrees);
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
