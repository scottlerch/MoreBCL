namespace More.Extensions
{
    using System;

    /// <summary>
    /// Extension methods for reference types.
    /// </summary>
    /// <remarks>
    /// Currently, this just includes syntactic suger for certain cases.
    /// Maybe it's too broad, but it definitely shorten argument null
    /// checking in the beginning of methods.
    /// </remarks>
    public static class ReferenceExtensions
    {
        /// <summary>
        /// Helper method to validate null arguments.
        /// </summary>
        /// <typeparam name="T">Type of object to check.</typeparam>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentNullException">
        /// Argument cannot be null.
        /// </exception>
        public static void ThrowIfNull<T>(this T value) where T : class
        {
            ThrowIfNull(value, string.Empty);
        }

        /// <summary>
        /// Helper method to validate null arguments.
        /// </summary>
        /// <typeparam name="T">Type of object to check.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="ArgumentNullException">
        /// Argument cannot be null.
        /// </exception>
        public static void ThrowIfNull<T>(this T value, string argumentName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Determines whether the specified value is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is null; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNull<T>(this T value) where T : class
        {
            return value == null;
        }
    }
}
