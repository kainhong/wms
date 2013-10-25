using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WMS.Web.Controllers
{
    [DataContract]
    public class ActionResult<T>
    {
        public ActionResult()
        {
            this.Status = 200;
        }

        public ActionResult(T data):this()
        {
            this.Data = data;
        }

        [DataMember]
        public T Data { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public bool ErrorMessage { get; set; }
    }
}
