using System.Security.Cryptography;
using System.Text;

namespace wan24
{
    public static partial class DecimalEncoder
    {
        /// <summary>
        /// Encode a positive integer decimal
        /// </summary>
        /// <param name="dec">Decimal</param>
        /// <param name="simple">Use the simple encoding map?</param>
        /// <returns>Encoded</returns>
        public static string EncodeDecimal(this decimal dec, bool simple) => dec.EncodeDecimal(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Encode a positive integer decimal
        /// </summary>
        /// <param name="dec">Decimal</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Encoded</returns>
        public static string EncodeDecimal(this decimal dec, DecimalEncodingMap map) => dec.EncodeDecimal(map.GetEncodingMap());

        /// <summary>
        /// Encode a positive integer decimal
        /// </summary>
        /// <param name="dec">Decimal</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Encoded</returns>
        public static string EncodeDecimal(this decimal dec, string map = MAP)
        {
            if (dec < 0 || decimal.Floor(dec) != dec) throw new ArgumentOutOfRangeException(nameof(dec));
            if (map.Length < 1) throw new ArgumentException("Encoding map is empty", nameof(map));
            StringBuilder sb = new();
            for (decimal mod; dec > 0; mod = dec % map.Length, sb.Append(map[(int)mod]), dec = (dec - mod) / map.Length) ;
            return sb.ToString();
        }

        /// <summary>
        /// Decode decimal
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="simple">Use the simple encoding map?</param>
        /// <returns>Decimal</returns>
        public static decimal ToDecodedDecimal(this string str, bool simple) => str.ToDecodedDecimal(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Decode decimal
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Decimal</returns>
        public static decimal ToDecodedDecimal(this string str, DecimalEncodingMap map) => str.ToDecodedDecimal(map.GetEncodingMap());

        /// <summary>
        /// Decode decimal
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Decimal</returns>
        public static decimal ToDecodedDecimal(this string str, string map = MAP)
        {
            decimal res = 0;
            for (int i = str.Length - 1, mapIndex; i > -1; res = res * map.Length + mapIndex, i--)
            {
                mapIndex = map.IndexOf(str[i]);
                if (mapIndex < 0) throw new InvalidDataException("Invalid encoding");
            }
            return res;
        }

        /// <summary>
        /// Create a random positive integer decimal and encode
        /// </summary>
        /// <param name="simple">Use the simple encoding map?</param>
        /// <returns>Random decimal and encoded</returns>
        public static (decimal, string) CreateRandomDecimal(bool simple) => CreateRandomDecimal(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Create a random positive integer decimal and encode
        /// </summary>
        /// <param name="map">Encoding map</param>
        /// <returns>Random decimal and encoded</returns>
        public static (decimal, string) CreateRandomDecimal(DecimalEncodingMap map) => CreateRandomDecimal(map.GetEncodingMap());

        /// <summary>
        /// Create a random positive integer decimal and encode
        /// </summary>
        /// <param name="map">Encoding map</param>
        /// <returns>Random decimal and encoded</returns>
        public static (decimal, string) CreateRandomDecimal(string map = MAP)
        {
            decimal res = new(DecimalRandomBits(), DecimalRandomBits(), DecimalRandomBits(), false, 0);
            return (res, res.EncodeDecimal(map));
        }

        /// <summary>
        /// Get decimal random bits
        /// </summary>
        /// <returns>Random bits</returns>
        private static int DecimalRandomBits() => (RandomNumberGenerator.GetInt32(0, 1 << 4) << 28) | RandomNumberGenerator.GetInt32(0, 1 << 28);
    }
}
