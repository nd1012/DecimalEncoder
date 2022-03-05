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
        public static string Encode(this uint dec, bool simple) => dec.Encode(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Encode a positive integer
        /// </summary>
        /// <param name="dec">Integer</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Encoded</returns>
        public static string Encode(this uint dec, DecimalEncodingMap map) => dec.Encode(map.GetEncodingMap());

        /// <summary>
        /// Encode a positive integer
        /// </summary>
        /// <param name="dec">Integer</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Encoded</returns>
        public static string Encode(this uint dec, string map = MAP)
        {
            if (map.Length < 1) throw new ArgumentException("Encoding map is empty", nameof(map));
            StringBuilder sb = new();
            for (uint mod, mapLen = (uint)map.Length; dec > 0; mod = dec % mapLen, sb.Append(map[(int)mod]), dec = (dec - mod) / mapLen) ;
            return sb.ToString();
        }

        /// <summary>
        /// Decode integer
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="simple">Use the simple encoding map?</param>
        /// <returns>Integer</returns>
        public static uint ToDecodedUInteger(this string str, bool simple) => str.ToDecodedUInteger(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Decode integer
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Integer</returns>
        public static uint ToDecodedUInteger(this string str, DecimalEncodingMap map) => str.ToDecodedUInteger(map.GetEncodingMap());

        /// <summary>
        /// Decode integer
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Integer</returns>
        public static uint ToDecodedUInteger(this string str, string map = MAP)
        {
            uint res = 0, mapLen = (uint)map.Length;
            for (int i = str.Length - 1, mapIndex; i > -1; res = res * mapLen + (uint)mapIndex, i--)
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
        public static (uint, string) CreateRandomUInteger(bool simple) => CreateRandomUInteger(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Create a random positive integer and encode
        /// </summary>
        /// <param name="map">Encoding map</param>
        /// <returns>Random integer and encoded</returns>
        public static (uint, string) CreateRandomUInteger(DecimalEncodingMap map) => CreateRandomUInteger(map.GetEncodingMap());

        /// <summary>
        /// Create a random positive integer and encode
        /// </summary>
        /// <param name="map">Encoding map</param>
        /// <returns>Random integer and encoded</returns>
        public static (uint, string) CreateRandomUInteger(string map = MAP)
        {
            uint res = BitConverter.ToUInt32(RandomNumberGenerator.GetBytes(sizeof(uint)));
            return (res, res.Encode(map));
        }
    }
}
