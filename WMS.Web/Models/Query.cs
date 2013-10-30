using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WMS.Web.Models
{
    [DataContract]
    public class Query
    {
        [DataMember(Name = "moduleId")]
        public int ModuleId { get; set; }

        [DataMember(Name = "dataQueryName")]
        public string DataQueryName { get; set; }

        [DataMember(Name = "parameters")]
        public List<Parameter> Parameters { get; set; }

        [DataMember(Name = "isDynamic")]
        public bool IsDynamic { get; set; }
    }

    [DataContract]
    public class Parameter
    {
        [DataMember(Name = "feildName")]
        public string FeildName { get; set; }

        [DataMember(Name = "value")]
        public object Value { get; set; }

        [DataMember(Name = "Operation")]
        public string Operation { get; set; }
    }
}