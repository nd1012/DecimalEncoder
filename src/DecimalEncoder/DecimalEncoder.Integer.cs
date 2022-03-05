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
        public static string Encode(this int dec, bool simple) => dec.Encode(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Encode a positive integer
        /// </summary>
        /// <param name="dec">Integer</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Encoded</returns>
        public static string Encode(this int dec, DecimalEncodingMap map) => dec.Encode(map.GetEncodingMap());

        /// <summary>
        /// Encode a positive integer
        /// </summary>
        /// <param name="dec">Integer</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Encoded</returns>
        public static string Encode(this int dec, string map = MAP)
        {
            if (dec < 0) throw new ArgumentOutOfRangeException(nameof(dec));
            if (map.Length < 1) throw new ArgumentException("Encoding map is empty", nameof(map));
            StringBuilder sb = new();
            for (int mod; dec > 0; mod = dec % map.Length, sb.Append(map[mod]), dec = (dec - mod) / map.Length) ;
            return sb.ToString();
        }

        /// <summary>
        /// Decode integer
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="simple">Use the simple encoding map?</param>
        /// <returns>Integer</returns>
        public static int ToDecodedInteger(this string str, bool simple) => str.ToDecodedInteger(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Decode integer
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Integer</returns>
        public static int ToDecodedInteger(this string str, DecimalEncodingMap map) => str.ToDecodedInteger(map.GetEncodingMap());

        /// <summary>
        /// Decode integer
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Integer</returns>
        public static int ToDecodedInteger(this string str, string map = MAP)
        {
            int res = 0;
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
        public static (int, string) CreateRandomInteger(bool simple) => CreateRandomInteger(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Create a random positive integer and encode
        /// </summary>
        /// <param name="map">Encoding map</param>
        /// <returns>Random integer and encoded</returns>
        public static (int, string) CreateRandomInteger(DecimalEncodingMap map) => CreateRandomInteger(map.GetEncodingMap());

        /// <summary>
        /// Create a random positive integer and encode
        /// </summary>
        /// <param name="map">Encoding map</param>
        /// <returns>Random integer and encoded</returns>
        public static (int, string) CreateRandomInteger(string map = MAP)
        {
            int res = RandomNumberGenerator.GetInt32(0, int.MaxValue);
            return (res, res.Encode(map));
        }
    }
}
