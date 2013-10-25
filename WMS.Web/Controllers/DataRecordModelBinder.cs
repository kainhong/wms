using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using System.IO;
using WMS.Web.Models;
using Newtonsoft.Json.Linq;


namespace WMS.Web.Controllers
{
    public class DataRecordModelBinder : System.Web.Http.ModelBinding.IModelBinder
    {

        //public object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        //{
        //    using (var stream = new StreamReader(controllerContext.RequestContext.HttpContext.Request.InputStream, System.Text.Encoding.UTF8))
        //    {
        //        var content = stream.ReadToEnd();
        //        var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
        //        return obj;
        //    }
        //}




        public bool BindModel(System.Web.Http.Controllers.HttpActionContext actionContext,
                    System.Web.Http.ModelBinding.ModelBindingContext bindingContext)
        {
            using( var stream = new StreamReader((actionContext.ControllerContext.Request.Properties["MS_HttpContext"] as System.Web.HttpContextWrapper).Request.InputStream))
            {
                var content = stream.ReadToEnd();
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(content) as JObject;
                if (obj == null)
                    return false;
                var datas = obj["data"];
                var result = new DataRecords();
                foreach (var status in datas.Where(c=>c.Type == JTokenType.Property).Cast<JProperty>())
                {
                    if (status.Name == "modified")
                        GetRecoders(status, result.Modified);
                    else if( status.Name == "added")
                        GetRecoders(status, result.Added);
                    else if (status.Name == "deleted")
                        GetRecoders(status,result.Deleted);
                }                
                bindingContext.Model = result;
                return true;
            } 
        }

        private void GetRecoders(JProperty jp,List<DataRecord> records)
        {
            if (!jp.HasValues)
                return;
            var jv = jp.Value;
            foreach (var data in jv)
            {
                var record = new DataRecord();
                foreach (var p in data)
                {
                    if (p.Type != JTokenType.Property)
                        continue;
                    var ps = p as JProperty;
                    object value = null;
                    if (ps.HasValues)
                        value = (ps.Value as JValue).Value;
                    record[ps.Name] = value;
                }
                records.Add(record);
            }
        }
    }
}