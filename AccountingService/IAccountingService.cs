using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Accounting
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAccountingService" in both code and config file together.
    [ServiceContract]
    public interface IAccountingService
    {
        [OperationContract]
        string Hello();

        [OperationContract]
        AccountingResult GetStandardDeviation(List<double> numbers);

        [OperationContract]
        AccountingResult GetMaximum(List<double> numbers);

        [OperationContract]
        AccountingResult GetMinimum(List<double> numbers);

        [OperationContract]
        AccountingResult GetAverage(List<double> numbers);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "AccountingService.ContractType".
    [DataContract]
    public class AccountingResult
    {
        [DataMember]
        public bool Success { get; set; }
        [DataMember]
        public double Result { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
