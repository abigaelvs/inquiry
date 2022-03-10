using System;
using System.Text.Json.Serialization;

namespace InqService.Entity
{
    public class MsCustomer
    {
        public static string TableName = "ms_customer";
        public string Periode { get; set; }
        public string Oid { get; set; }
        public string CustName { get; set; }
        public string Gender { get; set; }
        public string GradingNasabah { get; set; }
        public string CustId { get; set; }
        public decimal CapacityLimit { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public string FlagCust { get; set; }
    }
}
