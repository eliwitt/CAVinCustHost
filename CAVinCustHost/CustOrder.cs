using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CAVinCustHost
{
    [CollectionDataContract(Name="ArrayOfCustOrder")]
    public class ArrayOfCustOrder : List<CustOrder>
    {
    }
    [DataContract]
    public class CustOrder
    {
        /// <summary>
        /// How manys days in the past to looks for orders
        /// </summary>
        [DataMember]
        public bool IsWebOrder { get; set; }
        [DataMember]
        public string ItemNumber { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string OrderNumber { get; set; }
        [DataMember]
        public string LineNumber { get; set; }
        [DataMember]
        public string ShippingMethod { get; set; }
        [DataMember]
        public string TrackingNumber { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string CsrName { get; set; }
        [DataMember]
        public string CsrEmail { get; set; }
        [DataMember]
        public string Advertiser { get; set; }
        [DataMember]
        public string Design { get; set; }
        [DataMember]
        public DateTime TargetShipDate { get; set; }
        [DataMember]
        public DateTime ActualShipDate { get; set; }
        [DataMember]
        public DateTime TargetDeliveryDate { get; set; }
        [DataMember]
        public DateTime ActualDeliveryDate { get; set; }
        [DataMember]
        public string ReceivedBy { get; set; }
        [DataMember]
        public string PoNumber { get; set; }
        [DataMember]
        public string StatusText { get; set; }
    }
}
