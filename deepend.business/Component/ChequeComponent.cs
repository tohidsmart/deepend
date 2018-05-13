using deepend.business.Common;
using deepend.entity.Request;
using deepend.entity;
using deepend.business.Interface;
using deepend.entity.Response;
using System;
using System.Text;
using System.Text.RegularExpressions;
using static System.Math;

namespace deepend.business.Component
{
    public class ChequeComponent : IChequeComponent
    {
        public ChequeResponse Transform(ChequeResponse response)
        {

            response.ChequeAmountInLetter = ProcessNumber(response.Amount);
            return response;

        }

        public ChequeResponse Create(ChequeRequest request)
        {
            ValidatEntity(request);
            ChequeResponse response = new ChequeResponse
            {
                DateTime = DateTime.Now,
                PersonName = request.PersonName.ToUpper(),
                Amount = request.ChequeAmount.Value,
            };
            return response;
        }

        public void ValidatEntity(EntityBase entity)
        {
            if (!entity.Validate())
                throw new Common.ValidationException($"{entity.ErrorMessages}", entity.GetType().Name);
        }

        private string ProcessNumber(decimal number)
        {
            if (number < 0)
                throw new ArgumentException("The number should be positive.");
            AmountConverter converter = new AmountConverter();
            ulong integer = (ulong)Truncate(number); // extracts the integer part of the number
            ulong decimals = (ulong)((number - integer) * 100); // extracts the decimals of the number and converts it to integer


            string result =
                ($"{converter.Convert(integer)} dollar{(integer > 1 ? "s" : "")}" +
                 $"{(decimals > 0 ? $" and {converter.Convert(decimals)} cent{(decimals > 1 ? "s" : "")}" : "")}");

            return result;
        }
        
    }
}
