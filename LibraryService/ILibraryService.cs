using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Library
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILibraryService" in both code and config file together.
    [ServiceContract]
    public interface ILibraryService
    {
        [OperationContract]
        string Hello();

        [OperationContract]
        string GetFile(string fileName);

        [OperationContract]
        string GetAllFileNames();

        [OperationContract]
        string GetLines(string fileName, int lowerBoundary, int upperBoundary);

        [OperationContract]
        bool FileExists(string fileName);
    }
}
