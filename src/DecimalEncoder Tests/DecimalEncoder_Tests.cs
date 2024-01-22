using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;

namespace wan24
{
    [TestClass]
    public class DecimalEncoder_Tests
    {
        [TestMethod]
        public void Decimal_Tests()
        {
            (decimal dec, string enc) = DecimalEncoder.CreateRandomDecimal(simple: true);
            Debug.WriteLine(dec);
            Debug.WriteLine($"{enc.Length,3}\t{DecimalEncodingMap.Simple,-13}\t{enc}");
            Assert.AreEqual(dec, enc.ToDecodedDecimal(simple: true));
            Assert.AreEqual(enc, dec.EncodeDecimal(simple: true));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => ((decimal)-1).EncodeDecimal());
            Assert.ThrowsException<ArgumentException>(() => ((decimal)0).EncodeDecimal(string.Empty));
            Assert.ThrowsException<InvalidDataException>(() => "|".ToDecodedDecimal());

            foreach (DecimalEncodingMap map in new DecimalEncodingMap[]
            { 
                DecimalEncodingMap.Normal, 
                DecimalEncodingMap.Extended, 
                DecimalEncodingMap.Safe,
                DecimalEncodingMap.SafeExtended
            })
            {
                enc = dec.EncodeDecimal(map);
                Debug.WriteLine($"{enc.Length,3}\t{map,-13}\t{enc}");
                Assert.AreEqual(dec, enc.ToDecodedDecimal(map));
                Assert.AreEqual(enc, dec.EncodeDecimal(map));
            }
        }

        [TestMethod]
        public void Integer_Tests()
        {
            (int dec, string enc) = DecimalEncoder.CreateRandomInteger(simple: true);
            Debug.WriteLine(dec);
            Debug.WriteLine($"{enc.Length,3}\t{DecimalEncodingMap.Simple,-13}\t{enc}");
            Assert.AreEqual(dec, enc.ToDecodedInteger(simple: true));
            Assert.AreEqual(enc, dec.EncodeDecimal(simple: true));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => (-1).EncodeDecimal());
            Assert.ThrowsException<ArgumentException>(() => 0.EncodeDecimal(string.Empty));
            Assert.ThrowsException<InvalidDataException>(() => "|".ToDecodedInteger());

            foreach (DecimalEncodingMap map in new DecimalEncodingMap[]
            {
                DecimalEncodingMap.Normal,
                DecimalEncodingMap.Extended,
                DecimalEncodingMap.Safe,
                DecimalEncodingMap.SafeExtended
            })
            {
                enc = dec.EncodeDecimal(map);
                Debug.WriteLine($"{enc.Length,3}\t{map,-13}\t{enc}");
                Assert.AreEqual(dec, enc.ToDecodedInteger(map));
                Assert.AreEqual(enc, dec.EncodeDecimal(map));
            }
        }

        [TestMethod]
        public void UInteger_Tests()
        {
            (uint dec, string enc) = DecimalEncoder.CreateRandomUInteger(simple: true);
            Debug.WriteLine(dec);
            Debug.WriteLine($"{enc.Length,3}\t{DecimalEncodingMap.Simple,-13}\t{enc}");
            Assert.AreEqual(dec, enc.ToDecodedUInteger(simple: true));
            Assert.AreEqual(enc, dec.EncodeDecimal(simple: true));
            Assert.ThrowsException<ArgumentException>(() => ((uint)0).EncodeDecimal(string.Empty));
            Assert.ThrowsException<InvalidDataException>(() => "|".ToDecodedUInteger());

            foreach (DecimalEncodingMap map in new DecimalEncodingMap[]
            {
                DecimalEncodingMap.Normal,
                DecimalEncodingMap.Extended,
                DecimalEncodingMap.Safe,
                DecimalEncodingMap.SafeExtended
            })
            {
                enc = dec.EncodeDecimal(map);
                Debug.WriteLine($"{enc.Length,3}\t{map,-13}\t{enc}");
                Assert.AreEqual(dec, enc.ToDecodedUInteger(map));
                Assert.AreEqual(enc, dec.EncodeDecimal(map));
            }
        }

        [TestMethod]
        public void BigInteger_Tests()
        {
            (BigInteger dec, string enc) = DecimalEncoder.CreateRandomBigInteger(simple: true);
            Debug.WriteLine(dec);
            Debug.WriteLine($"{enc.Length,3}\t{DecimalEncodingMap.Simple,-13}\t{enc}");
            Assert.AreEqual(dec, enc.ToDecodedBigInteger(simple: true));
            Assert.AreEqual(enc, dec.Encode(simple: true));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => ((BigInteger)(-1)).Encode());
            Assert.ThrowsException<ArgumentException>(() => ((BigInteger)0).Encode(string.Empty));
            Assert.ThrowsException<InvalidDataException>(() => "|".ToDecodedBigInteger());

            foreach (DecimalEncodingMap map in new DecimalEncodingMap[]
            {
                DecimalEncodingMap.Normal,
                DecimalEncodingMap.Extended,
                DecimalEncodingMap.Safe,
                DecimalEncodingMap.SafeExtended
            })
            {
                enc = dec.Encode(map);
                Debug.WriteLine($"{enc.Length,3}\t{map,-13}\t{enc}");
                Assert.AreEqual(dec, enc.ToDecodedBigInteger(map));
                Assert.AreEqual(enc, dec.Encode(map));
            }
        }

        [TestMethod]
        public void BigInteger_Bytes_Tests()
        {
            string enc = RandomNumberGenerator.GetBytes(20).EncodeInteger(simple: true);
            BigInteger dec = enc.ToDecodedBigInteger(simple: true);
            Debug.WriteLine(dec);
            Debug.WriteLine($"{enc.Length,3}\t{DecimalEncodingMap.Simple,-13}\t{enc}");
            Assert.AreEqual(dec, enc.ToDecodedBigInteger(simple: true));
            Assert.AreEqual(enc, dec.Encode(simple: true));
        }

        [TestMethod]
        public void Long_Tests()
        {
            (long dec, string enc) = DecimalEncoder.CreateRandomLong(simple: true);
            Debug.WriteLine(dec);
            Debug.WriteLine($"{enc.Length,3}\t{DecimalEncodingMap.Simple,-13}\t{enc}");
            Assert.AreEqual(dec, enc.ToDecodedLong(simple: true));
            Assert.AreEqual(enc, dec.EncodeDecimal(simple: true));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => ((long)-1).EncodeDecimal());
            Assert.ThrowsException<ArgumentException>(() => ((long)0).EncodeDecimal(string.Empty));
            Assert.ThrowsException<InvalidDataException>(() => "|".ToDecodedLong());

            foreach (DecimalEncodingMap map in new DecimalEncodingMap[]
            {
                DecimalEncodingMap.Normal,
                DecimalEncodingMap.Extended,
                DecimalEncodingMap.Safe,
                DecimalEncodingMap.SafeExtended
            })
            {
                enc = dec.EncodeDecimal(map);
                Debug.WriteLine($"{enc.Length,3}\t{map,-13}\t{enc}");
                Assert.AreEqual(dec, enc.ToDecodedLong(map));
                Assert.AreEqual(enc, dec.EncodeDecimal(map));
            }
        }

        [TestMethod]
        public void ULong_Tests()
        {
            (ulong dec, string enc) = DecimalEncoder.CreateRandomULong(simple: true);
            Debug.WriteLine(dec);
            Debug.WriteLine($"{enc.Length,3}\t{DecimalEncodingMap.Simple,-13}\t{enc}");
            Assert.AreEqual(dec, enc.ToDecodedULong(simple: true));
            Assert.AreEqual(enc, dec.EncodeDecimal(simple: true));
            Assert.ThrowsException<ArgumentException>(() => ((ulong)0).EncodeDecimal(string.Empty));
            Assert.ThrowsException<InvalidDataException>(() => "|".ToDecodedULong());

            foreach (DecimalEncodingMap map in new DecimalEncodingMap[]
            {
                DecimalEncodingMap.Normal,
                DecimalEncodingMap.Extended,
                DecimalEncodingMap.Safe,
                DecimalEncodingMap.SafeExtended
            })
            {
                enc = dec.EncodeDecimal(map);
                Debug.WriteLine($"{enc.Length,3}\t{map,-13}\t{enc}");
                Assert.AreEqual(dec, enc.ToDecodedULong(map));
                Assert.AreEqual(enc, dec.EncodeDecimal(map));
            }
        }
    }
}
