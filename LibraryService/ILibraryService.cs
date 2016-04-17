using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Library
{
    [ServiceContract]
    public interface ILibraryService
    {
        [OperationContract]
        string Hello();

        [OperationContract]
        LibraryTransaction<string> GetFile(string fileName);

        [OperationContract]
        LibraryTransaction<List<string>> GetAllFileNames();

        [OperationContract]
        LibraryTransaction<string> GetLines(string fileName, int lowerBoundary, int upperBoundary);

        [OperationContract]
        LibraryTransaction<bool> FileExists(string fileName);

    }

    [DataContract]
    public class LibraryTransaction<T>
    {
        [DataMember]
        public bool Success { get; set; }
        [DataMember]
        public T Result { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
