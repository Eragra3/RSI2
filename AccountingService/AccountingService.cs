using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MathNet.Numerics.Statistics;

namespace Accounting
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccountingService" in both code and config file together.
    public class AccountingService : IAccountingService
    {
        public AccountingResult GetStandardDeviation(List<double> numbers)
        {
            if (numbers == null || !numbers.Any())
                return new AccountingResult
                {
                    Success = false,
                    Result = double.NaN,
                    ErrorMessage = "Nie przesłano żadnych danych"
                };

            return new AccountingResult
            {
                Result = numbers.StandardDeviation(),
                Success = true
            };
        }

        public AccountingResult GetMaximum(List<double> numbers)
        {
            if (numbers == null || !numbers.Any())
                return new AccountingResult
                {
                    Success = false,
                    Result = double.NaN,
                    ErrorMessage = "Nie przesłano żadnych danych"
                };

            return new AccountingResult
            {
                Result = numbers.Maximum(),
                Success = true
            };
        }
        public AccountingResult GetMinimum(List<double> numbers)
        {
            if (numbers == null || !numbers.Any())
                return new AccountingResult
                {
                    Success = false,
                    Result = double.NaN,
                    ErrorMessage = "Nie przesłano żadnych danych"
                };

            return new AccountingResult
            {
                Result = numbers.Minimum(),
                Success = true
            };
        }
        public AccountingResult GetAverage(List<double> numbers)
        {
            if (numbers == null || !numbers.Any())
                return new AccountingResult
                {
                    Success = false,
                    Result = double.NaN,
                    ErrorMessage = "Nie przesłano żadnych danych"
                };

            return new AccountingResult
            {
                Result = numbers.Average(),
                Success = true
            };
        }

        public string Hello()
        {
            return "Hello there!";
        }
    }
}
