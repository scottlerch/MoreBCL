namespace More.Extensions
{
    using System;

    /// <summary>
    /// Extension methods for Double.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// Determine if two values are almost equal.
        /// </summary>
        /// <param name="double1">The double1.</param>
        /// <param name="double2">The double2.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is zero; otherwise, <c>false</c>.
        /// </returns>
        public static bool AlmostEquals(this double double1, double double2)
        {
            return (Math.Abs(double1 - double2) < double.Epsilon);
        }

        /// <summary>
        /// Determines whether the specified value is zero.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is zero; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsZero(this double value)
        {
            return (Math.Abs(value) < double.Epsilon);
        }
    }
}
