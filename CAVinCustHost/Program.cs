#define callReq
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using CAVinCustHost.CustOrders;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace CAVinCustHost
{
    class Program
    {
        static void Main(string[] args)
        {

            callMyServiceClient();
            callMyServiceReq();

        }
        /// <summary>
        /// Call the service using the client technique
        /// </summary>
        [Conditional("callClient")]
        private static void callMyServiceClient()
        {
            Console.WriteLine("In Client call");
            try
            {
                CustomerOrdersClient theProxy = new CustomerOrdersClient();

                Console.WriteLine("Test 1: list my orders");
                VPIOrder myOrders = theProxy.GetOrder("C00001", "12412355");
                Console.WriteLine("{0} {1}", myOrders.OrderNumber, myOrders.Status);
                //foreach (VPIOrder theOrder in myOrders)
                //{
                //    Console.WriteLine("{0} {1}", theOrder.OrderNumber, theOrder.Status);
                //}
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            Console.WriteLine("Leaving client call");
        }
        /// <summary>
        /// Call the service using the http req technique
        /// </summary>
        [Conditional("callReq")]
        private static void callMyServiceReq()
        {
            Console.WriteLine("In ServiceReq");
            Console.WriteLine("Call retrieve orders");
            string uri = "http://localhost/VinCustOrders/Service1.svc/C00001";
            HttpWebRequest Ordersrequest = (HttpWebRequest)HttpWebRequest.Create(uri);
            Ordersrequest.Method = "GET";
            using (HttpWebResponse response = (HttpWebResponse)Ordersrequest.GetResponse())
            {
                using(var responseStream = response.GetResponseStream())
                {
                    using(var reader = new StreamReader(responseStream))
                    {
                        //Here you will get response
                        Console.WriteLine(reader.ReadToEnd());
                    }

                }
            }
            Console.WriteLine("");
            Console.WriteLine("Call retrieve orders");
            uri = "http://localhost/VinCustOrders/Service1.svc/Order/C00001/120894326";
            HttpWebRequest Orderrequest = (HttpWebRequest)HttpWebRequest.Create(uri);
            Orderrequest.Method = "GET";
            using (HttpWebResponse response = (HttpWebResponse)Orderrequest.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        //Here you will get response
                        Console.WriteLine(reader.ReadToEnd());
                    }

                }
            }
            Console.WriteLine("Leaving ServiceReq");
        }
    }
}
