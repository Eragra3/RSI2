using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace LibraryHost
{
    class LibraryHost
    {
        static void Main(string[] args)
        {
            var baseAdress = new Uri("http://localhost:13372/LibraryService/");

            var host = new ServiceHost(typeof(LibraryService), baseAdress);

            try
            {
                host.AddServiceEndpoint(typeof(ILibraryService), new WSHttpBinding(), "LibraryService");

                var smb = new ServiceMetadataBehavior { HttpGetEnabled = true };
                host.Description.Behaviors.Add(smb);

                host.Open();

                Console.WriteLine($"{nameof(LibraryService)} is now being hosted on 'LibraryService'");
                Console.WriteLine("Press any key to stop service");
                Console.Read();

                host.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ooops, {e.Message}");
                Console.ReadLine();
                host.Abort();
            }
        }
    }
}
