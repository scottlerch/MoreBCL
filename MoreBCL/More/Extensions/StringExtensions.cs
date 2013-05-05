namespace More.Extensions
{
    using System;

    /// <summary>
    /// String extension methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Helper method to validate null or empty arguments.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="ArgumentNullException">
        /// Argument cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Argument cannot be empty.
        /// </exception>
        public static void ThrowIfNullOrEmpty(this string value, string argumentName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }

            if (value == string.Empty)
            {
                throw new ArgumentException(argumentName);
            }
        }

        /// <summary>
        /// Helper method to validate null or empty arguments.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentNullException">
        /// Argument cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Argument cannot be empty.
        /// </exception>
        public static void ThrowIfNullOrEmpty(this string value)
        {
            value.ThrowIfNullOrEmpty(string.Empty);
        }

        /// <summary>
        /// Determines whether the string contains the specified string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The value.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <returns>
        ///   <c>true</c> if it contains the string; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            return source.IndexOf(value, comparisonType) >= 0;
        }

        /// <summary>
        /// Check string equality.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="value">The value.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <returns></returns>
        public static bool Equals(this string source, string value, StringComparison comparisonType)
        {
            return string.Compare(source, value, comparisonType) == 0;
        }

        /// <summary>
        /// Limits the string to be no longer than the specified max length.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="maxLength">Length of the max.</param>
        /// <returns></returns>
        public static string Limit(this string value, int maxLength)
        {
            return (string.IsNullOrEmpty(value) || value.Length <= maxLength) ? value : value.Substring(0, maxLength);
        }
    }
}
