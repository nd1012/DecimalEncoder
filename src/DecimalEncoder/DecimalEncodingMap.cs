namespace wan24
{
    /// <summary>
    /// Decimal encoding map
    /// </summary>
    public enum DecimalEncodingMap
    {
        /// <summary>
        /// Normal (decimals 0-9, letters a-z and A-Z and basic special characters)
        /// </summary>
        Normal,
        /// <summary>
        /// Simple (decimals 0-9 and letters a-z and A-Z)
        /// </summary>
        Simple,
        /// <summary>
        /// Extended (decimals 0-9, letters a-z and A-Z, more special characters and umlauts)
        /// </summary>
        Extended,
        /// <summary>
        /// Safe (decimals 2-9 and letters a-z (without b, c, d, i, j, l, o, p, u, v) and A-Z (without C, I, O, Q, U, V))
        /// </summary>
        Safe,
        /// <summary>
        /// Safe extended (decimals 2-9, letters a-z (without b, c, d, i, j, l, o, p, u, v) and A-Z (without C, I, O, Q, U, V) and some basic special characters)
        /// </summary>
        SafeExtended
    }
}
