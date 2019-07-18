using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWCF
{
    class Program
    {
        static void Main(string[] args)
        {       //Create a URI to serve as the base address
            Uri httpUrl = new Uri("http://localhost:8090/CalculatorService/CalculatorService");

            //Create ServiceHost
            ServiceHost host = new ServiceHost(typeof(CalculatorService.CalculatorService), httpUrl);

            //Add a service endpoint
            host.AddServiceEndpoint(typeof(CalculatorService.ISimpleCalculator), new BasicHttpBinding(), "");

            //Enable metadata exchange
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);

            //Start the Service
            host.Open();
            Console.WriteLine("Service is host at " + DateTime.Now.ToString());
            Console.WriteLine("Host is running... Press  key to stop");
            Console.ReadLine();
        }
    }
}
