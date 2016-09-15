using System;
using System.Collections.Generic;

namespace SFA.DAS.Payments.AcceptanceTests.DataHelpers
{
    internal static class IdentifierGenerator
    {
        private static readonly char[] ValidAlpha = { 'A', 'B', 'C', 'E', 'F', 'G', 'H', 'K', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y', 'Z' };
        private static readonly char[] ValidNumeric = { '2', '3', '4', '5', '6', '7', '8', '9' };
        private static readonly Random Rdm = new Random();

        internal static string GenerateIdentifier(int requiredLength = 8, bool useAlpha = true, bool useNumeric = true)
        {
            var validCharacters = new List<char>();
            if (useAlpha)
            {
                validCharacters.AddRange(ValidAlpha);
            }
            if (useNumeric)
            {
                validCharacters.AddRange(ValidNumeric);
            }

            var identifier = new char[requiredLength];
            for (var i = 0; i < requiredLength; i++)
            {
                identifier[i] = validCharacters[Rdm.Next(0, validCharacters.Count - 1)];
            }
            return new string(identifier);
        }
    }
}
