using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace deepend.business.Common
{
    public class AmountConverter2
    {

        public string Convert(string numberStrs)
        {
            numberStrs = Regex.Replace(numberStrs, "^[0]*", "");

            Dictionary<int, string> DicK = GetKDigits();


            StringBuilder stb = new StringBuilder();

            int length = numberStrs.Length;
            int extra = length % 3;
            List<string> numbers = new List<string>();

            numbers.Add(numberStrs.Substring(0, extra));

            for (int i = 0; extra + (i * 3) < length; i++)
            {
                numbers.Add(numberStrs.Substring(extra + (i * 3), 3));
            }
            numbers.RemoveAll(x => x == string.Empty);

            for (int i = numbers.Count; i > 0; i--)
            {
                stb.AppendFormat("{0} {1}", PrintNumbers(numbers[numbers.Count - i]), DicK[i]);
            }


            return stb.ToString();

        }

        public Dictionary<int, string> GetKDigits()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(0, "");
            dic.Add(1, "");
            dic.Add(2, "THOUSAND ");
            dic.Add(3, "MILLION ");
            //dic.Add(4, "billion ");
            //dic.Add(5, "trillion ");
            //dic.Add(6, "gazillion ");
            //dic.Add(7, "quadrillion ");
            return dic;
        }

        public string PrintNumbers(string numbers)
        {
            StringBuilder stb = new StringBuilder();

            Dictionary<string, string> firstDigit = GetSingleDigits();
            Dictionary<string, string> secondDigit = GetDoubleDigits();

            switch (numbers.Length)
            {
                case 3:
                    {
                        if (numbers[0] != '0')
                        {
                            stb.AppendFormat("{0} HUNDRED ", firstDigit[numbers.Substring(0, 1)]);
                        }
                        if (numbers[1] == '1')
                        {
                            stb.AppendFormat("{0}", firstDigit[numbers.Substring(1, 2)]);
                        }
                        else
                        {
                            stb.AppendFormat("{0} {1}", secondDigit[numbers.Substring(1, 1)], firstDigit[numbers.Substring(2, 1)]);
                        }
                    }
                    break;
                case 2:
                    {
                        if (numbers[0] == '1')
                        {
                            stb.AppendFormat("{0} ", firstDigit[numbers.Substring(0, 2)]);
                        }
                        else
                        {
                            stb.AppendFormat("{0} {1}", secondDigit[numbers.Substring(0, 1)], firstDigit[numbers.Substring(1, 1)]);
                        }
                    }
                    break;
                case 1:
                    {
                        stb.AppendFormat("{0} ", firstDigit[numbers.Substring(0, 1)]);
                    }
                    break;
            }

            return stb.ToString();
        }

        public string GetFractionStringRepresentation(string amount)
        {
            if (string.IsNullOrEmpty(amount)) return string.Empty;
            string result = CheckNumberLessThanThousand(amount, "", true);
            return result;
        }

        public string CheckNumberLessThanThousand(string numberStr, string postFix = "", bool readRight = false)
        {

            var singleDigit = GetSingleDigits();
            var doubleDigits = GetDoubleDigits();
            StringBuilder sb = new StringBuilder();
            int originalNumber = System.Convert.ToInt32(numberStr);

            if (numberStr.Length == 3)
            {
                int ser2 = originalNumber / 100;
                sb.Append($" {singleDigit[ser2.ToString()]} {postFix}");
                originalNumber = originalNumber - (ser2 * 100);
                if (originalNumber < 10 && originalNumber > 0)
                {
                    sb.Append($" {singleDigit[originalNumber.ToString()]}");
                    originalNumber = 0;
                }

                numberStr = originalNumber.ToString();
            }
            if (numberStr.Length < 3 && originalNumber > 0)
            {
                if (originalNumber < 20 && readRight)
                {
                    sb.Append($" {singleDigit[originalNumber.ToString()]}");
                }
                else
                {
                    int ser1 = originalNumber / 10;
                    if (ser1 > 0)
                    {
                        sb.Append($" {doubleDigits[ser1.ToString()]}");
                        originalNumber = originalNumber - (ser1 * 10);

                    }
                    if (originalNumber > 0)
                        sb.Append($" {singleDigit[originalNumber.ToString()]}");
                }
            }
            return sb.ToString();
        }

        public Dictionary<string, string> GetSingleDigits()
        {
            Dictionary<string, string> singleDigit = new Dictionary<string, string>();
            singleDigit.Add("0", "");
            singleDigit.Add("1", "ONE");
            singleDigit.Add("2", "TWO");
            singleDigit.Add("3", "THREE");
            singleDigit.Add("4", "FOUR");
            singleDigit.Add("5", "FIVE");
            singleDigit.Add("6", "SIX");
            singleDigit.Add("7", "SEVEN");
            singleDigit.Add("8", "EIGHT");
            singleDigit.Add("9", "NINE");
            singleDigit.Add("10", "TEN");
            singleDigit.Add("11", "ELEVEN");
            singleDigit.Add("12", "TWELVE");
            singleDigit.Add("13", "THIRTEEN");
            singleDigit.Add("14", "FOURTEEN");
            singleDigit.Add("15", "FIFTEEN");
            singleDigit.Add("16", "SIXTEEN");
            singleDigit.Add("17", "SEVENTEEN");
            singleDigit.Add("18", "EIGHTEEN");
            singleDigit.Add("19", "NINETEEN");
            return singleDigit;
        }

        public Dictionary<string, string> GetDoubleDigits()
        {
            Dictionary<string, string> textStrings = new Dictionary<string, string>();
            textStrings.Add("0", "");
            textStrings.Add("2", "TWENTY ");
            textStrings.Add("3", "THIRTY ");
            textStrings.Add("4", "FORTY ");
            textStrings.Add("5", "FIFTY ");
            textStrings.Add("6", "SIXTY ");
            textStrings.Add("7", "SEVENTY ");
            textStrings.Add("8", "EIGHTY ");
            textStrings.Add("9", "NINETY ");
            return textStrings;
        }

    }
}
