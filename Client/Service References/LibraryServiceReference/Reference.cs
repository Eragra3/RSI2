﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.LibraryServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LibraryServiceReference.ILibraryService")]
    public interface ILibraryService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILibraryService/Hello", ReplyAction="http://tempuri.org/ILibraryService/HelloResponse")]
        string Hello();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILibraryService/Hello", ReplyAction="http://tempuri.org/ILibraryService/HelloResponse")]
        System.Threading.Tasks.Task<string> HelloAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILibraryService/GetFile", ReplyAction="http://tempuri.org/ILibraryService/GetFileResponse")]
        string GetFile(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILibraryService/GetFile", ReplyAction="http://tempuri.org/ILibraryService/GetFileResponse")]
        System.Threading.Tasks.Task<string> GetFileAsync(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILibraryService/GetAllFileNames", ReplyAction="http://tempuri.org/ILibraryService/GetAllFileNamesResponse")]
        string GetAllFileNames();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILibraryService/GetAllFileNames", ReplyAction="http://tempuri.org/ILibraryService/GetAllFileNamesResponse")]
        System.Threading.Tasks.Task<string> GetAllFileNamesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILibraryService/GetLines", ReplyAction="http://tempuri.org/ILibraryService/GetLinesResponse")]
        string GetLines(string fileName, int lowerBoundary, int upperBoundary);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILibraryService/GetLines", ReplyAction="http://tempuri.org/ILibraryService/GetLinesResponse")]
        System.Threading.Tasks.Task<string> GetLinesAsync(string fileName, int lowerBoundary, int upperBoundary);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILibraryService/FileExists", ReplyAction="http://tempuri.org/ILibraryService/FileExistsResponse")]
        bool FileExists(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILibraryService/FileExists", ReplyAction="http://tempuri.org/ILibraryService/FileExistsResponse")]
        System.Threading.Tasks.Task<bool> FileExistsAsync(string fileName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILibraryServiceChannel : Client.LibraryServiceReference.ILibraryService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LibraryServiceClient : System.ServiceModel.ClientBase<Client.LibraryServiceReference.ILibraryService>, Client.LibraryServiceReference.ILibraryService {
        
        public LibraryServiceClient() {
        }
        
        public LibraryServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LibraryServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LibraryServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LibraryServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Hello() {
            return base.Channel.Hello();
        }
        
        public System.Threading.Tasks.Task<string> HelloAsync() {
            return base.Channel.HelloAsync();
        }
        
        public string GetFile(string fileName) {
            return base.Channel.GetFile(fileName);
        }
        
        public System.Threading.Tasks.Task<string> GetFileAsync(string fileName) {
            return base.Channel.GetFileAsync(fileName);
        }
        
        public string GetAllFileNames() {
            return base.Channel.GetAllFileNames();
        }
        
        public System.Threading.Tasks.Task<string> GetAllFileNamesAsync() {
            return base.Channel.GetAllFileNamesAsync();
        }
        
        public string GetLines(string fileName, int lowerBoundary, int upperBoundary) {
            return base.Channel.GetLines(fileName, lowerBoundary, upperBoundary);
        }
        
        public System.Threading.Tasks.Task<string> GetLinesAsync(string fileName, int lowerBoundary, int upperBoundary) {
            return base.Channel.GetLinesAsync(fileName, lowerBoundary, upperBoundary);
        }
        
        public bool FileExists(string fileName) {
            return base.Channel.FileExists(fileName);
        }
        
        public System.Threading.Tasks.Task<bool> FileExistsAsync(string fileName) {
            return base.Channel.FileExistsAsync(fileName);
        }
    }
}