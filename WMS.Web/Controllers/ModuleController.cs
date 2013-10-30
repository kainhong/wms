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
using System.Text.RegularExpressions;

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

        public QueryResult GetQueryData(int moduleId, string queryName, string condition)
        {
            DataQueryCollection lst = service.GetModuleQuery(moduleId);
            var query = lst.FirstOrDefault(c => c.Name == queryName);
            if (query == null)
                return null;
            using (var con = Unity.GetConnection())
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = query.SelectSQL;
                if(!string.IsNullOrEmpty(condition) )
                    cmd.CommandText += " WHERE " + condition;

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
                var script =  string.Join(Environment.NewLine,field.DisplayValue);
                script = string.Format(@"SELECT * FROM 
                    (
                        SELECT A.*, ROWNUM RN 
                        FROM ( {0} ) A 
                            WHERE ROWNUM <= 20
                    )
                    WHERE RN >= 0", script);

                using (var con = Unity.GetConnection())
                {
                    var cmd = con.CreateCommand();
                    cmd.CommandText = script;
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
        
        public QueryResult PostQuery(Query query)
        {
            DataQueryCollection lst = service.GetModuleQuery(query.ModuleId);
            var q = lst.FirstOrDefault(c => c.Name == query.DataQueryName);
            if (q == null)
                return null;
            var script = q.SelectSQL;
            Dictionary<string, object> parameters = null;// new Dictionary<string, object>()
            if (query.IsDynamic)
            {
                script = GetWhereStatement(query, q);
            }
            else
            {
                script = ParseScript(query, q, out parameters);
                
            }

            using (var con = Unity.GetConnection())
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = script;
                cmd.Connection = con;
                if (parameters.Count > 0)
                {
                    foreach (var key in parameters.Keys)
                    {
                        var p = cmd.CreateParameter();
                        p.ParameterName = ":" + key;
                        p.Value = parameters[key];
                        cmd.Parameters.Add(p);
                    }
                }
                con.Open();
                var rd = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(rd);

                var result = new QueryResult() { Total = dt.Rows.Count, Data = dt };

                return result;
            }
            
        }

        private string GetWhereStatement(Query query,DataQuery q)
        {
            var script = q.SelectSQL;
            List<string> conditions = new List<string>();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            foreach (var p in query.Parameters)
            {
                var field = q.Fields[p.FeildName];
                var val = p.Value;
                //if (string.IsNullOrEmpty(val))
                if( val == null)
                    continue;
                string format = field.DataType == DbType.String ? "{0} = '{1}'" : "{0} = {1}";
                conditions.Add(string.Format(format, p.FeildName, p.Value));                
            }
            var where = string.Join(" AND ", conditions.ToArray());
            if (!string.IsNullOrEmpty(where))
                script += " WHERE " + where;
            return script;
        }
         

        public ActionResult<bool> PostSaveData(int moduleid, string queryName,
            [ModelBinder(typeof(DataRecordModelBinder))]DataRecords datas)
        {

            return null;
        }

        Regex regex = new Regex(@"(?<name>@\b\w+\b)");
        private string ParseScript(Query query, DataQuery q, out Dictionary<string, object> @params)
        {
            var script = q.SelectSQL;
            var dict = new Dictionary<string, object>();
            var val = regex.Replace(script,new MatchEvaluator( m => {
                var name = m.Groups["name"].Value.TrimStart('@');
                var field = query.Parameters.FirstOrDefault(f => string.Compare(f.FeildName, name, true) == 0);
                dict[name] = field.Value;
                return ":" + name;
            }));
            @params = dict;

            return val;
        }
    }
}
