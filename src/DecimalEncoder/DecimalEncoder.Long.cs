using System.Security.Cryptography;
using System.Text;

namespace wan24
{
    public static partial class DecimalEncoder
    {
        /// <summary>
        /// Encode a positive integer
        /// </summary>
        /// <param name="dec">Integer</param>
        /// <param name="simple">Use the simple encoding map?</param>
        /// <returns>Encoded</returns>
        public static string EncodeDecimal(this long dec, bool simple) => dec.EncodeDecimal(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Encode a positive integer
        /// </summary>
        /// <param name="dec">Integer</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Encoded</returns>
        public static string EncodeDecimal(this long dec, DecimalEncodingMap map) => dec.EncodeDecimal(map.GetEncodingMap());

        /// <summary>
        /// Encode a positive integer
        /// </summary>
        /// <param name="dec">Integer</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Encoded</returns>
        public static string EncodeDecimal(this long dec, string map = MAP)
        {
            if (dec < 0) throw new ArgumentOutOfRangeException(nameof(dec));
            if (map.Length < 1) throw new ArgumentException("Encoding map is empty", nameof(map));
            StringBuilder sb = new();
            for (long mod; dec > 0; mod = dec % map.Length, sb.Append(map[(int)mod]), dec = (dec - mod) / map.Length) ;
            return sb.ToString();
        }

        /// <summary>
        /// Decode integer
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="simple">Use the simple encoding map?</param>
        /// <returns>Integer</returns>
        public static long ToDecodedLong(this string str, bool simple) => str.ToDecodedLong(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Decode integer
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Integer</returns>
        public static long ToDecodedLong(this string str, DecimalEncodingMap map) => str.ToDecodedLong(map.GetEncodingMap());

        /// <summary>
        /// Decode integer
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Integer</returns>
        public static long ToDecodedLong(this string str, string map = MAP)
        {
            long res = 0;
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
        /// <returns>Random integer and encoded</returns>
        public static (long, string) CreateRandomLong(bool simple) => CreateRandomLong(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Create a random positive integer and encode
        /// </summary>
        /// <param name="map">Encoding map</param>
        /// <returns>Random integer and encoded</returns>
        public static (long, string) CreateRandomLong(DecimalEncodingMap map) => CreateRandomLong(map.GetEncodingMap());

        /// <summary>
        /// Create a random positive integer and encode
        /// </summary>
        /// <param name="map">Encoding map</param>
        /// <returns>Random integer and encoded</returns>
        public static (long, string) CreateRandomLong(string map = MAP)
        {
            long res = ((long)RandomNumberGenerator.GetInt32(0, int.MaxValue) << 32) | BitConverter.ToUInt32(RandomNumberGenerator.GetBytes(sizeof(uint)));
            return (res, res.EncodeDecimal(map));
        }
    }
}
