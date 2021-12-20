using System;
using System.Linq;

namespace Application.Utils.RandomStringGenerator
{
    public static class RandomStringGenerator
    {
        public static string GeneratePasswordResetToken(int length)
        {
            var random = new Random();
            return new string(Enumerable
                .Repeat(ApplicationConstants.Alphabet, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}