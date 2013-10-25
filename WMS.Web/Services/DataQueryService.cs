using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WMS.Web.Models;
using WMS.Web.Controllers;
using System.IO;

namespace WMS.Web.Services
{
    public class DataQueryService
    {
        internal static Dictionary<int, DataQueryCollection> queries = new Dictionary<int, DataQueryCollection>();

        public IEnumerable<Module> Get()
        {
            string script = "Select ModuleId,Caption From MySystem order by OrderNO";
            using (var con = Unity.GetConnection())
            {
                var cmd = con.CreateCommand();
                cmd.Connection = con;
                cmd.CommandText = script;
                con.Open();
                var rd = cmd.ExecuteReader();
                List<Module> lst = new List<Module>();
                while (rd.Read())
                {
                    var item = new Module();
                    item.Id = Convert.ToInt32(rd["ModuleId"]);
                    item.Caption = rd["Caption"].ToString();
                    lst.Add(item);
                }
                return lst;
            }
        }

        public DataQueryCollection GetModuleQuery(int id)
        {
            DataQueryCollection query;
            if (!queries.TryGetValue(id, out query))
            {

                string script = "select DesignerData from ModuleDesigner where ModuleID = ";

                using (var con = Unity.GetConnection())
                {
                    var cmd = con.CreateCommand();
                    cmd.Connection = con;
                    cmd.CommandText = script + id;
                    con.Open();
                    var buff = (byte[])cmd.ExecuteScalar();
                    var mem = Unity.DeCompress(new MemoryStream(buff));
                    var result = mem.XmlDeserialize<DataQueryCollection>();
                    var content = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                    queries[id] = query = result;
                }

                foreach (var item in query)
                    item.ModuleID = id.ToString();
                
            }
            return query;
        }
    }
}