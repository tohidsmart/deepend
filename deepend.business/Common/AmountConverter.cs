using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Math;

namespace deepend.business.Common
{
    public class AmountConverter
    {
        /// <summary>
        /// Converter Component class. It converts a number to its textual representation
        /// </summary>

        private readonly Dictionary<int, string> _numbers;
        private readonly List<string> _ranks;

        /// <summary>
        /// Initiate a Number converter
        /// </summary>
        public AmountConverter()
        {
            _numbers = InitNumbersCollection();
            _ranks = InitRankingCollection();
        }

        /// <summary>
        /// Converts a whole number to its textual representation in English.
        /// </summary>
        /// <param name="number">An integer holds a number to be converted</param>
        /// <returns>A string holds the textual representation of the given number</returns>
        public string Convert(ulong number)
        {
            if (number == 0)
                return "zero";

            short length = (short)Floor(Log10(number) + 1); // calculates the number of digits
            int thousandthLength = (int)Ceiling(length / 3d); // calculates number of thousandth
            ulong groupRemaining = number; // use to keep the remaining of the number after deducting the group

            List<int> groups = new List<int>(); // holds groups number

            for (int i = 0; i < thousandthLength; i++) // calculates each group value and adds to groups collection
            {
                short group = (short)(groupRemaining % 1000);
                groupRemaining /= 1000;

                groups.Add(group);
            }

            StringBuilder sb = new StringBuilder(); // holds the final result

            for (int i = --thousandthLength; i >= 0; i--) // loops through each group and converts it to text
            {
                if (groups[i] == 0) // ignores if number is 0
                    continue;

                if (i < thousandthLength && groups[i] > 0) // appends 'and' to link groups
                    sb.Append("and ");

                sb.Append(ConvertGroup(groups[i])); // converts the number to text
                sb.Append($" { _ranks[i] } "); // appends the group rank
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        /// Converts number's hundreth group to its textual representation in English.
        /// </summary>
        /// <param name="number">An integer between 0 to 999 holds a number to be converted</param>
        /// <returns>A string holds the textual representation of the given number</returns>
        /// <exception cref="ArgumentException">When the number is less than 0 or more than 999.</exception>
        protected string ConvertGroup(int number)
        {
            if (number == 0)
                return string.Empty;

            if (number > 999 || number < 0)
                throw new ArgumentException("A group should be positive and it cannot be more than 999");

            StringBuilder sb = new StringBuilder();

            int length = (int)Floor(Log10(number) + 1); // calculates the number of digits

            int digit = 0;
            int remaining = number;

            for (int i = length - 1; i >= 0; i--) // loops through each individual digit
            {
                digit = (int)(remaining / Pow(10, i)); // calculates the digit
                remaining = (int)(remaining % Pow(10, i)); // calculates the remaining

                if (digit == 0)
                    continue;

                switch (i)
                {
                    case 2: // hundredth
                        sb.Append($"{ NumberToString(digit) } hundred"); // appends hundred to the number
                        sb.Append($"{ (remaining > 0 ? " and " : string.Empty) }"); // if remaining is not zero appends 'and'

                        break;
                    case 1: // tenth
                        var num = digit * 10 + ((digit > 1) ? 0 : remaining); // checks if the remaining is below 20
                        sb.Append($"{ NumberToString(num) }");

                        if (digit == 1) // resets the remaining to 0 if it is less than 20 and ends the lookup 
                            remaining = 0;
                        else if (remaining > 0) // appends '-' for numbers more than 20 when first number is more than 0
                            sb.Append("-");

                        break;
                    case 0: // first
                        sb.Append($"{ _numbers.FirstOrDefault(n => n.Key == digit).Value }");

                        break;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts a number to its textual representation in English
        /// </summary>
        /// <param name="number">An integer holding a number to be converted</param>
        /// <returns>A string holds the textual representation of the given number</returns>
        private string NumberToString(int number) => $"{_numbers.FirstOrDefault(n => n.Key == number).Value}";

        /// <summary>
        /// Initializes a list of numbers and its textual representation in English
        /// </summary>
        /// <returns>A Dictionary of numbers and their textual representation in English</returns>
        private Dictionary<int, string> InitNumbersCollection() =>
            new Dictionary<int, string>()
            {
                [0] = string.Empty,
                [1] = "one",
                [2] = "two",
                [3] = "three",
                [4] = "four",
                [5] = "five",
                [6] = "six",
                [7] = "seven",
                [8] = "eight",
                [9] = "nine",
                [10] = "ten",
                [11] = "eleven",
                [12] = "twelve",
                [13] = "thirteen",
                [14] = "fourteen",
                [15] = "fifteen",
                [16] = "sixteen",
                [17] = "seventeen",
                [18] = "eighteen",
                [19] = "nineteen",
                [20] = "twenty",
                [30] = "thirty",
                [40] = "forty",
                [50] = "fifty",
                [60] = "sixty",
                [70] = "seventy",
                [80] = "eighty",
                [90] = "ninety"
            };

        /// <summary>
        /// Represents number ranks textual representation
        /// </summary>
        /// <returns>A list of number ranks</returns>
        private List<string> InitRankingCollection() =>
            new List<string>()
            {
                string.Empty,
                "thousand",
                "million",
                "billion",
                "trillion",
                "quadrillion",
                "quintillion"
            };

    }
}
