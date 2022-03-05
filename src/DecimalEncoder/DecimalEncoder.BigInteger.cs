using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace wan24
{
    public static partial class DecimalEncoder
    {
        /// <summary>
        /// Default big integer length in bytes
        /// </summary>
        public const int BIG_INT_LENGTH = 64;

        /// <summary>
        /// Encode a positive integer
        /// </summary>
        /// <param name="dec">Integer</param>
        /// <param name="simple">Use the simple encoding map?</param>
        /// <returns>Encoded</returns>
        public static string Encode(this BigInteger dec, bool simple) => dec.Encode(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Encode a positive integer
        /// </summary>
        /// <param name="dec">Integer</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Encoded</returns>
        public static string Encode(this BigInteger dec, DecimalEncodingMap map) => dec.Encode(map.GetEncodingMap());

        /// <summary>
        /// Encode a positive integer
        /// </summary>
        /// <param name="dec">Integer</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Encoded</returns>
        public static string Encode(this BigInteger dec, string map = MAP)
        {
            if (dec < 0) throw new ArgumentOutOfRangeException(nameof(dec));
            if (map.Length < 1) throw new ArgumentException("Encoding map is empty", nameof(map));
            StringBuilder sb = new();
            for (int mod; dec > 0; mod = (int)(dec % map.Length), sb.Append(map[mod]), dec = (dec - mod) / map.Length) ;
            return sb.ToString();
        }

        /// <summary>
        /// Encode a positive integer
        /// </summary>
        /// <param name="bytes">Unsigned integer bytes</param>
        /// <param name="simple">Use the simple encoding map?</param>
        /// <returns>Encoded</returns>
        public static string EncodeInteger(this byte[] bytes, bool simple) => ToBigInteger(bytes).Encode(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Encode a positive integer
        /// </summary>
        /// <param name="bytes">Unsigned integer bytes</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Encoded</returns>
        public static string EncodeInteger(this byte[] bytes, DecimalEncodingMap map) => ToBigInteger(bytes).Encode(map.GetEncodingMap());

        /// <summary>
        /// Encode a positive integer
        /// </summary>
        /// <param name="bytes">Unsigned integer bytes</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Encoded</returns>
        public static string EncodeInteger(this byte[] bytes, string map = MAP) => ToBigInteger(bytes).Encode(map);

        /// <summary>
        /// Decode integer
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="simple">Use the simple encoding map?</param>
        /// <returns>Integer</returns>
        public static BigInteger ToDecodedBigInteger(this string str, bool simple) => str.ToDecodedBigInteger(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Decode integer
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Integer</returns>
        public static BigInteger ToDecodedBigInteger(this string str, DecimalEncodingMap map) => str.ToDecodedBigInteger(map.GetEncodingMap());

        /// <summary>
        /// Decode integer
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Integer</returns>
        public static BigInteger ToDecodedBigInteger(this string str, string map = MAP)
        {
            BigInteger res = 0;
            for (int i = str.Length - 1, mapIndex; i > -1; res = res * map.Length + mapIndex, i--)
            {
                mapIndex = map.IndexOf(str[i]);
                if (mapIndex < 0) throw new InvalidDataException("Invalid encoding");
            }
            return res;
        }

        /// <summary>
        /// Create a random positive integer and encode
        /// </summary>
        /// <param name="simple">Use the simple encoding map?</param>
        /// <param name="len">Length in bytes</param>
        /// <returns>Random integer and encoded</returns>
        public static (BigInteger, string) CreateRandomBigInteger(bool simple, int len = BIG_INT_LENGTH) => CreateRandomBigInteger(simple ? SIMPLE_MAP : MAP, len);

        /// <summary>
        /// Create a random positive integer and encode
        /// </summary>
        /// <param name="map">Encoding map</param>
        /// <param name="len">Length in bytes</param>
        /// <returns>Random integer and encoded</returns>
        public static (BigInteger, string) CreateRandomBigInteger(DecimalEncodingMap map, int len = BIG_INT_LENGTH) => CreateRandomBigInteger(map.GetEncodingMap(), len);

        /// <summary>
        /// Create a random positive integer and encode
        /// </summary>
        /// <param name="map">Encoding map</param>
        /// <param name="len">Length in bytes</param>
        /// <returns>Random integer and encoded</returns>
        public static (BigInteger, string) CreateRandomBigInteger(string map = MAP, int len = BIG_INT_LENGTH)
        {
            if (len < 0) throw new ArgumentOutOfRangeException(nameof(len));
            BigInteger res = new(RandomNumberGenerator.GetBytes(len), isUnsigned: true);
            return (res, res.Encode(map));
        }

        /// <summary>
        /// Convert bytes to an unsigned big integer
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <returns>Big integer</returns>
        private static BigInteger ToBigInteger(byte[] bytes) => new BigInteger(bytes, isUnsigned: true);
    }
}
