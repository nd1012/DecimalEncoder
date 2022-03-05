namespace wan24
{
    /// <summary>
    /// Decimal encoder
    /// </summary>
    public static partial class DecimalEncoder
    {
        /// <summary>
        /// Encoding map (decimals 0-9, letters a-z and A-Z and basic special characters)
        /// </summary>
        public const string MAP = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!§$%&/()=?+*#-_.:,;{}[]<>@";
        /// <summary>
        /// Simple encoding map (decimals 0-9 and letters a-z and A-Z)
        /// </summary>
        public const string SIMPLE_MAP = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>
        /// Extended encoding map (decimals 0-9, letters a-z and A-Z, more special characters and umlauts)
        /// </summary>
        public const string EXTENDED_MAP = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!§$%&/()=?+*#-_.:,;{}[]<>@\"'\\|^°´`~äöüÄÖÜßâêîôûÂÊÎÔÛáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙ";
        /// <summary>
        /// Safe encoding map (decimals 2-9 and letters a-z (without b, c, d, i, j, l, o, p, u, v) and A-Z (without C, I, O, Q, U, V))
        /// </summary>
        public const string SAFE_MAP = "23456789aefghkmnrstwxyzABDEFGHJKLMNPRSTWXYZ";
        /// <summary>
        /// Safe extended encoding map (decimals 2-9, letters a-z (without b, c, d, i, j, l, o, p, u, v) and A-Z (without C, I, O, Q, U, V) and some basic special characters)
        /// </summary>
        public const string SAFE_EXTENDED_MAP = "23456789aefghkmnrstwxyzABDEFGHJKLMNPRSTWXYZ%=?+#-<>@";

        /// <summary>
        /// Get the encoding map
        /// </summary>
        /// <param name="map">Encoding map</param>
        /// <returns>Encoding map</returns>
        public static string GetEncodingMap(this DecimalEncodingMap map) => map switch
        {
            DecimalEncodingMap.Normal => MAP,
            DecimalEncodingMap.Simple => SIMPLE_MAP,
            DecimalEncodingMap.Extended => EXTENDED_MAP,
            DecimalEncodingMap.Safe => SAFE_MAP,
            DecimalEncodingMap.SafeExtended => SAFE_EXTENDED_MAP,
            _ => throw new NotImplementedException($"Map {map} isn't implemented")
        };
    }
}
