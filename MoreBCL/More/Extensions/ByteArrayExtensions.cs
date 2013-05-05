namespace More.Extensions
{
    /// <summary>
    /// Extension methods for byte[].
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Fast managed byte array comparison by value.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <returns>true if arrays equal by value, false otherwise</returns>
        public static bool EqualsByValue(this byte[] first, byte[] second)
        {
            int length = first.Length;

            for (int i = 0; i < length; i++)
            {
                if ((first[i] ^ second[i]) > 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
