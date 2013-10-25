using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Web.Models
{
    public class DataRecords
    {
        public DataRecords()
        {
            Modified = new List<DataRecord>();
            Deleted = new List<DataRecord>();
            Added = new List<DataRecord>();

        }

        public List<DataRecord> Modified { get; private set; }

        public List<DataRecord> Deleted { get; private set; }

        public List<DataRecord> Added { get; private set; }
    }

    public class DataRecord
    {
        Dictionary<string, object> datas = new Dictionary<string, object>();

        public DataRecord()
        {

        }

        public object this[string key]
        {
            get { return datas[key]; }
            set { datas[key] = value; }
        }

        public IEnumerable<string> Keys{
            get
            {
                return datas.Keys;
            }
        }
        //public Dictionary<string,object> Datas { get; set; }

        public int Status { get; set; }
    }
}