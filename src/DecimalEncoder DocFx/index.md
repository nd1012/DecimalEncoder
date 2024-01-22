# DecimalEncoder

This library encodes numeric values (and byte arrays) using a string encoding 
map. Supported numeric types are:

- `int` (32 bit signed positive integer)
- `uint` (32 bit unsigned integer)
- `long` (64 bit signed positive integer)
- `ulong` (64 bit unsigned integer)
- `decimal` (unsigned positive unscaled)
- `BigInteger` (any size unsigned integer)

Byte arrays will be converted to a `BigInteger` for encoding.

The encoding uses division for finding the encoding map index and compressing 
the result. The same will run backward for decoding using multiplication.

## How to get it

This library is available as 
[NuGet package](https://www.nuget.org/packages/DecimalEncoder/).

## Usage

```cs
// Encode a value
string encoded = RandomNumberGenerator.GetInt32().EncodeDecimal();
Console.WriteLine(encoded);

// Decode an encoded value
int decoded = encoded.ToDecodedInteger();
Console.WriteLine(decoded);

// Create a random value
(decoded, encoded) = DecimalEncoder.CreateRandomInteger();
Console.WriteLine(encoded);
Console.WriteLine(decoded);
```

## Encoding

Please note that encoding isn't encryption. This encoding is **WAY** more 
slower than base64 encoding, while the decoding is pretty fast, and the 
encoded value might be smaller than the original (compression instead of 
expansion).

I noticed that the length of the encoding map does effect the encoding speed 
for the different numeric types. For example, the built in encoding maps are 
fast for all types except decimal, where the encoding takes about 5 times 
longer (see the test runtimes). The encoding performance depends on the 
availability of optimized low level number division algorithms and the used 
divisor (the encoding maps length will be used), and it may also be different 
with different CPUs or operating systems.

### Encoding maps

There are several encoding maps predefined (accessable using the 
`DecimalEncodingMap` enumeration):

- `Normal`: decimals 0-9, letters a-z and A-Z and basic special characters
- `Simple`: decimals 0-9 and letters a-z and A-Z
- `Extended`: decimals 0-9, letters a-z and A-Z, more special characters and 
umlauts
- `Safe`: decimals 2-9 and letters a-z (without b, c, d, i, j, l, o, p, u, v) 
and A-Z (without C, I, O, Q, U, V)
- `SafeExtended`: decimals 2-9, letters a-z (without b, c, d, i, j, l, o, p, 
u, v) and A-Z (without C, I, O, Q, U, V) and some basic special characters

The "Safe" encoding maps are designed to avoid symbols that may be confounded 
with other similar looking symbols.

You're free to use any individual encoding map. An encoding map contains the 
symbol list to use, where every symbol needs to be unique.
