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
        public static string EncodeDecimal(this ulong dec, bool simple) => dec.EncodeDecimal(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Encode a positive integer
        /// </summary>
        /// <param name="dec">Integer</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Encoded</returns>
        public static string EncodeDecimal(this ulong dec, DecimalEncodingMap map) => dec.EncodeDecimal(map.GetEncodingMap());

        /// <summary>
        /// Encode a positive integer
        /// </summary>
        /// <param name="dec">Integer</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Encoded</returns>
        public static string EncodeDecimal(this ulong dec, string map = MAP)
        {
            if (map.Length < 1) throw new ArgumentException("Encoding map is empty", nameof(map));
            StringBuilder sb = new();
            for (ulong mod, mapLen = (ulong)map.Length; dec > 0; mod = dec % mapLen, sb.Append(map[(int)mod]), dec = (dec - mod) / mapLen) ;
            return sb.ToString();
        }

        /// <summary>
        /// Decode integer
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="simple">Use the simple encoding map?</param>
        /// <returns>Integer</returns>
        public static ulong ToDecodedULong(this string str, bool simple) => str.ToDecodedULong(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Decode integer
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Integer</returns>
        public static ulong ToDecodedULong(this string str, DecimalEncodingMap map) => str.ToDecodedULong(map.GetEncodingMap());

        /// <summary>
        /// Decode integer
        /// </summary>
        /// <param name="str">Encoded</param>
        /// <param name="map">Encoding map</param>
        /// <returns>Integer</returns>
        public static ulong ToDecodedULong(this string str, string map = MAP)
        {
            ulong res = 0, mapLen = (ulong)map.Length;
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
        public static (ulong, string) CreateRandomULong(bool simple) => CreateRandomULong(simple ? SIMPLE_MAP : MAP);

        /// <summary>
        /// Create a random positive integer and encode
        /// </summary>
        /// <param name="map">Encoding map</param>
        /// <returns>Random integer and encoded</returns>
        public static (ulong, string) CreateRandomULong(DecimalEncodingMap map) => CreateRandomULong(map.GetEncodingMap());

        /// <summary>
        /// Create a random positive integer and encode
        /// </summary>
        /// <param name="map">Encoding map</param>
        /// <returns>Random integer and encoded</returns>
        public static (ulong, string) CreateRandomULong(string map = MAP)
        {
            ulong res = BitConverter.ToUInt64(RandomNumberGenerator.GetBytes(sizeof(ulong)));
            return (res, res.EncodeDecimal(map));
        }
    }
}
