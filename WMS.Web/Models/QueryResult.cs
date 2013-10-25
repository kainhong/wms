using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WMS.Web.Models
{
    [DataContract]
    public class QueryResult
    {
        [DataMember(Name="rows")]
        [JsonConverter(typeof(DataTableConverter))]
        public DataTable Data { get; set; }

        [DataMember(Name="total")]
        public int Total { get; set; }
    }
}