using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WMS.Web.Models;

namespace WMS.Web.Controllers
{
    public class SystemController : ApiController
    {
        public IEnumerable<SystemInformation> Get()
        {
            string script = "Select SystemId,SystemName From MySystem order by OrderNO";
            using (var con = Unity.GetConnection())
            {
                var cmd = con.CreateCommand();
                cmd.Connection = con;
                cmd.CommandText = script;
                con.Open();
                var rd = cmd.ExecuteReader();
                List<SystemInformation> lst = new List<SystemInformation>();
                while (rd.Read())
                {
                    var item = new SystemInformation();
                    item.SystemName = rd["SystemName"].ToString();
                    item.SystemId = rd["SystemId"].ToString();
                    lst.Add(item);
                }
                return lst;
            }
        }

    }
}
