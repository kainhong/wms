using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Oracle.DataAccess.Client;
using WMS.Web.Models;
using System.IO;
using WMS.Web.Services;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web.Http.ModelBinding;

namespace WMS.Web.Controllers
{
    public class ModuleController : ApiController
    {

        DataQueryService service = new DataQueryService();
       
        public IEnumerable<Module> Get()
        {
            return service.Get();
        }

        /// <summary>
        /// get module dataquery list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataQueryCollection GetModuleQuery(int id)
        {
            return service.GetModuleQuery(id);
        }               

        public QueryResult GetQueryData(int moduleId,string queryName)
        {
            DataQueryCollection lst = service.GetModuleQuery(moduleId);
            var query = lst.FirstOrDefault(c => c.Name == queryName);
            if (query == null)
                return null;
            using (var con = Unity.GetConnection())
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = query.SelectSQL;
                cmd.Connection = con;
                con.Open();
                var rd = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(rd);

                var result = new QueryResult() { Total = dt.Rows.Count, Data = dt };

                return result;
            }
            
        }
                
        public ListItem GetFieldDataSource(int moduleId,string queryName,string fieldName)
        {
            DataQueryCollection lst = service.GetModuleQuery(moduleId);
            var query = lst.FirstOrDefault(c => c.Name == queryName);
            if (query == null)
                return null;
            var field = query.Fields[fieldName];
            if (field == null) 
                return null;
            if (field.DispValueType == EnumDisplayValueType.dvtSQL)
            {
                using (var con = Unity.GetConnection())
                {
                    var cmd = con.CreateCommand();
                    cmd.CommandText = string.Join(Environment.NewLine,field.DisplayValue);
                    cmd.Connection = con;
                    con.Open();
                    var rd = cmd.ExecuteReader();
                    var dt = new ListItem();
                    dt.Load(rd);
                    return dt;
                }
            }
            return null;
        }


        public QueryResult PostQuerySearch(Query query)
        {
            DataQueryCollection lst = service.GetModuleQuery(query.ModuleId);
            var q = lst.FirstOrDefault(c => c.Name == query.DataQueryName);
            if (q == null)
                return null;
            var script = q.SelectSQL;
            List<string> conditions = new List<string>();
            Dictionary<string,object> parameters = new Dictionary<string,object>();
            foreach (var p in query.Parameters)
            {
                var field = q.Fields[p.FeildName];
                var val = p.Value;
                if (string.IsNullOrEmpty(val))
                    continue;
                string format = field.DataType == DbType.String ? "{0} = '{1}'" : "{0} = {1}";
                conditions.Add(string.Format(format, p.FeildName, p.Value));
                //conditions.Add()
            }
            var where = string.Join(" AND ", conditions.ToArray());
            if( !string.IsNullOrEmpty(where))
                script += " WHERE " + where;


            using (var con = Unity.GetConnection())
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = script;
                cmd.Connection = con;
                con.Open();
                var rd = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(rd);

                var result = new QueryResult() { Total = dt.Rows.Count, Data = dt };

                return result;
            }
            
        }

        public ActionResult<bool> PostSaveData(int moduleid, string queryName,
            [ModelBinder(typeof(DataRecordModelBinder))]DataRecords datas)
        {

            return null;
        }
    }
}
