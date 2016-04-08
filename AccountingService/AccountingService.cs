using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MathNet.Numerics.Statistics;

namespace AccountingService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccountingService" in both code and config file together.
    public class AccountingService : IAccountingService
    {
        public double GetStandardDeviation(IEnumerable<double> numbers)
        {
            if (numbers == null || numbers.Count() == 0) return double.NaN;

            return numbers.StandardDeviation();
        }

        public double GetMaximum(IEnumerable<double> numbers)
        {
            if (numbers == null || numbers.Count() == 0) return double.NaN;

            return numbers.Maximum();
        }
        public double GetMinimum(IEnumerable<double> numbers)
        {
            if (numbers == null || numbers.Count() == 0) return double.NaN;

            return numbers.Minimum();
        }
        public double GetAverage(IEnumerable<double> numbers)
        {
            if (numbers == null || numbers.Count() == 0) return double.NaN;

            return numbers.Average();
        }
    }
}
