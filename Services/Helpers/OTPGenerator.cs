using System.Security.Cryptography;

namespace theCoffeeroom.Services.Helpers
{
    public class OTPGenerator
    {
        public static string GenerateOTP(string secret)
        {
            var time = DateTime.UtcNow;
            var secondsSinceEpoch = (long)(time - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            var timeSlice = secondsSinceEpoch / 30; // Time slice of 30 seconds

            var key = Base32Encoding.ToBytes(secret);
            var data = new byte[8];
            for (var i = 8; i-- > 0; timeSlice >>= 8)
            {
                data[i] = (byte)timeSlice;
            }

            var hmac = new HMACSHA1(key);
            var hash = hmac.ComputeHash(data);

            var offset = hash[hash.Length - 1] & 0xF;
            var truncatedHash = (hash[offset] & 0x7F) << 24 | (hash[offset + 1] & 0xFF) << 16 | (hash[offset + 2] & 0xFF) << 8 | hash[offset + 3] & 0xFF;

            var otp = truncatedHash % 1000000;
            return otp.ToString("D6");
        }
    }

    public static class Base32Encoding
    {
        private const string Base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        public static byte[] ToBytes(string input)
        {
            var bits = input.ToUpper().ToCharArray()
                .Select(c => Convert.ToString(Base32Alphabet.IndexOf(c), 2).PadLeft(5, '0'))
                .Aggregate((a, b) => a + b)
                .PadRight((input.Length * 5 + 7) / 8 * 8, '0')
                .Select((c, i) => new { C = c, I = i })
                .GroupBy(x => x.I / 8)
                .Select(x => Convert.ToByte(x.Aggregate(0, (b, a) => (b << 1) + (a.C - '0'))))
                .ToArray();
            return bits;
        }
    }
}
