using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WMS.Web.Models;

namespace WMS.Web.Controllers
{
    public class MenuController : ApiController
    {
        public IEnumerable<Menu> Get()
        {
            string script = @"Select MenuId,SystemId,MenuCaption,PARENTMENUID,OrderNO 
                            From MYMENU order by SystemId,PARENTMENUID,OrderNO,MenuCaption";
            using (var con = Unity.GetConnection())
            {
                var cmd = con.CreateCommand();
                cmd.Connection = con;
                cmd.CommandText = script;
                con.Open();
                var rd = cmd.ExecuteReader();
                List<Menu> lst = new List<Menu>();
                while (rd.Read())
                {
                    var item = new Menu();
                    item.Id = Convert.ToInt32(rd["MenuId"]);
                    item.Attributes.SystemId = Convert.ToInt32(rd["SystemId"]);
                    item.Text = Convert.ToString(rd["MenuCaption"]);
                    item.Attributes.ParentId = Convert.ToInt32(rd["PARENTMENUID"]);
                    Menu parent = null;
                    if (item.Attributes.ParentId > 0)
                        parent = lst.FirstOrDefault(c => c.Id == item.Attributes.ParentId);

                    if (parent != null)
                        parent.Children.Add(item);
                    else
                        lst.Add(item);
                }
                return lst;
            }
        }

        public IEnumerable<Menu> Get(int id)
        {
            string script = @"Select B.ModuleId,A.Caption,B.SUBMENUID,B.MenuId,C.MenuCaption
                                From MYSUBMENU B
                                INNER JOIN MODULELIST A On B.ModuleId = A.ModuleId
                                Inner JOIN MYMENU C ON C.MenuId = B.MenuId
                                Where B.SYSTEMID = {0}
                                Order By B.MENUID,B.ORDERNO,A.CAPTION";

            using (var con = Unity.GetConnection())
            {
                var cmd = con.CreateCommand();
                cmd.Connection = con;
                cmd.CommandText = string.Format(script, id);
                con.Open();
                var rd = cmd.ExecuteReader();
                List<Menu> lst = new List<Menu>();
                while (rd.Read())
                {
                    var item = new Menu();
                    var parentid = Convert.ToInt32(rd["MenuId"]);
                    item.Id = Convert.ToInt32(rd["SUBMENUID"]);
                    item.Attributes.SystemId = id;
                    item.Text = rd["Caption"].ToString();
                    item.Attributes.ModuleId = Convert.ToInt32(rd["ModuleId"]);
                    var parent = lst.FirstOrDefault(c => c.Id == parentid);
                    if (parent == null)
                    {
                        parent = new Menu();
                        parent.Id = parentid;
                        parent.Text = rd["MenuCaption"].ToString();
                        parent.Attributes.SystemId = id;
                        lst.Add(parent);
                    }
                    parent.Children.Add(item);
                }
                return lst;
            }
        }
    }
}
