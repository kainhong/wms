using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WMS.Web.Models
{
    [JsonConverter(typeof(DataTableConverter))]
    public class ListItem:DataTable
    {
         
    }
}