﻿namespace More.Types
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    /// <summary>
    /// Frequency converter class.
    /// </summary>
    public class FrequencyTypeConverter : TypeConverter
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
                return Frequency.Parse((string)value);
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
                return Frequency.FromHz(Convert.ToDouble(value));
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
            if (value is Frequency)
            {
                if (destinationType == typeof(string))
                {
                    return ((Frequency)value).ToString();
                }

                if (destinationType == typeof(double))
                {
                    return Convert.ToDouble(((Frequency)value).Hz);
                }

                if (destinationType == typeof(float))
                {
                    return Convert.ToSingle(((Frequency)value).Hz);
                }

                if (destinationType == typeof(decimal))
                {
                    return Convert.ToDecimal(((Frequency)value).Hz);
                }

                if (destinationType == typeof(short))
                {
                    return Convert.ToInt16(((Frequency)value).Hz);
                }

                if (destinationType == typeof(int))
                {
                    return Convert.ToInt32(((Frequency)value).Hz);
                }

                if (destinationType == typeof(long))
                {
                    return Convert.ToInt64(((Frequency)value).Hz);
                }

                if (destinationType == typeof(uint))
                {
                    return Convert.ToUInt32(((Frequency)value).Hz);
                }

                if (destinationType == typeof(ulong))
                {
                    return Convert.ToUInt64(((Frequency)value).Hz);
                }

                if (destinationType == typeof(ushort))
                {
                    return Convert.ToUInt16(((Frequency)value).Hz);
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
