#define callReq
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;

namespace CAVinCustHost
{
    class Program
    {
        static void Main(string[] args)
        {

            callMyServiceReq();

        }
        /// <summary>
        /// Call the service using the http req technique
        /// </summary>
        [Conditional("callReq")]
        private static void callMyServiceReq()
        {
            Console.WriteLine("In ServiceReq");
            Console.WriteLine("Call retrieve orders");
            string uri = "http://localhost/VPIOrderService/OrderService.svc/C00001";
            HttpWebRequest Ordersrequest = (HttpWebRequest)HttpWebRequest.Create(uri);
            Ordersrequest.Method = "GET";
            using (HttpWebResponse response = (HttpWebResponse)Ordersrequest.GetResponse())
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(response.GetResponseStream());
                XmlNodeList theOrders = doc.GetElementsByTagName("CustOrder");
                List<CustOrder> myorders = GetMyOrders(theOrders);
                foreach(CustOrder theOrder in myorders)
                {
                    Console.WriteLine("order = {0} line = {1} status = {2}",
                        theOrder.OrderNumber, theOrder.LineNumber, theOrder.StatusText);
                }
 
            }
            Console.WriteLine("");
            Console.WriteLine("Call retrieve orders");
            uri = "http://localhost/VPIOrderService/OrderService.svc/Order/C00001/10058164";
            HttpWebRequest Orderrequest = (HttpWebRequest)HttpWebRequest.Create(uri);
            Orderrequest.Method = "GET";
           
            using (HttpWebResponse response = (HttpWebResponse)Orderrequest.GetResponse())
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(response.GetResponseStream());
                //List<CustOrder> theOrders = GetMyPro(doc);
                XmlNodeList theOrders = doc.GetElementsByTagName("CustOrder");
                List<CustOrder> myorders = GetMyOrders(theOrders);
                foreach (CustOrder theOrder in myorders)
                {
                    Console.WriteLine("order = {0} line = {1} status = {2}", 
                        theOrder.OrderNumber, theOrder.LineNumber, theOrder.StatusText);
                }
            }
            Console.WriteLine("Leaving ServiceReq");
        }

        private static List<CustOrder> GetMyOrders(XmlNodeList theOrders)
        {
            List<CustOrder> theObjs = new List<CustOrder>();
            foreach (XmlNode node in theOrders)
            {
                CustOrder theObj = new CustOrder();
                           //Use the reflection to get all the properties of this class object and set those
                System.Reflection.PropertyInfo[] arrPropInfo = theObj.GetType().GetProperties();
                for (int i = 0; i < arrPropInfo.Length; i++)
                {
                    string xmlname = arrPropInfo[i].Name;
                    System.Reflection.PropertyInfo propInfo = arrPropInfo[i];
                    String xmlValue = node[xmlname].InnerText;
                    Object typeCastedValue = xmlValue;
                    switch (propInfo.PropertyType.Name)
                    {
                        case "Boolean":
                            typeCastedValue = xmlValue.Equals("true") ? true : false;
                            break;
                        case "Int32":
                            typeCastedValue = Convert.ToInt32(xmlValue);
                            break;
                        case "DateTime":
                            typeCastedValue = Convert.ToDateTime(xmlValue);
                            break;
                    }

                    propInfo.SetValue(theObj, typeCastedValue, null);
                }
                theObjs.Add(theObj);
                theObj = null;
            }
            return theObjs;
        }

    }
}
