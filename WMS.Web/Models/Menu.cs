using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WMS.Web.Models
{
    [DataContract]
    public class Menu
    {
        public Menu()
        {
            Children = new List<Menu>();
            Attributes = new MenuAttributes();
        }

        [DataMember(Name = "attributes")]
        public MenuAttributes Attributes { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name="text")]
        public string Text { get; set; }

        

        [DataMember(Name="children")]
        public List<Menu> Children { get; set; }

        
    }

    [DataContract]
    public class MenuAttributes
    {
        [DataMember(Name = "parentId")]
        public int ParentId { get; set; }

        [DataMember(Name = "moduleId")]
        public int ModuleId { get; set; }

        [DataMember(Name = "systemId")]
        public int SystemId { get; set; }
    }
}