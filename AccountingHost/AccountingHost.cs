using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Accounting;

namespace AccountingHost
{
    class AccountingHost
    {
        static void Main(string[] args)
        {
            var baseAdress = new Uri("http://localhost:13371/AccountingService/");

            var host = new ServiceHost(typeof(AccountingService), baseAdress);

            try
            {
                host.AddServiceEndpoint(typeof(IAccountingService), new WSHttpBinding(), "AccountingService");

                var smb = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true
                };
                host.Description.Behaviors.Add(smb);

                host.Open();

                Console.WriteLine($"{nameof(AccountingService)} is now being hosted on 'Accounting'");
                Console.WriteLine("Press any key to stop service");
                Console.Read();

                host.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ooops, {e.Message}");
                Console.ReadLine();
            }
        }
    }
}
