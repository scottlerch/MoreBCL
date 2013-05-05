namespace More.Extensions
{
    using System;

    /// <summary>
    /// Extension methods for Single.
    /// </summary>
    public static class SingleExtensions
    {
        /// <summary>
        /// Determine if two values are almost equal.
        /// </summary>
        /// <param name="float1">The float1.</param>
        /// <param name="float2">The float2.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is zero; otherwise, <c>false</c>.
        /// </returns>
        public static bool AlmostEquals(this float float1, float float2)
        {
            return (Math.Abs(float1 - float2) < float.Epsilon);
        }

        /// <summary>
        /// Determines whether the specified value is zero.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is zero; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsZero(this float value)
        {
            return (Math.Abs(value) < float.Epsilon);
        }
    }
}
