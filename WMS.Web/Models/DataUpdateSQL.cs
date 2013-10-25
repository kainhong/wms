using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.Web.Models
{
    [Serializable]
    public class DataUpdateSQL
    {
        

        // Constructors
        public DataUpdateSQL()
        {
            
        }

         

        // Instance Fields
        private string insertSQL;
        public string InsertSQL
        {
            get
            {
                return this.insertSQL;
            }
            set
            {
                this.insertSQL = value;
            }
        }

        private string updateSQL;
        public string UpdateSQL
        {
            get
            {
                return this.updateSQL;
            }
            set
            {
                this.updateSQL = value;
            }
        }

        private string deleteSQL;

        public string DeleteSQL
        {
            get
            {
                return this.deleteSQL;
            }
            set
            {
                this.deleteSQL = value;
            }
        }

        

        //private CmdParameterCollection updateSQLParams;
        //private CmdParameterCollection insertSQLParams;
        //private CmdParameterCollection deleteSQLParams;
    }
}