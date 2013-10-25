using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Globalization;
using System.Runtime.Serialization;

namespace WMS.Web.Models
{

    /// <summary>
    /// 动态加载设置类
    /// </summary>
    [Serializable]
    [DataContract]
    public class RuntimeLoadSetting
    {
        public RuntimeLoadSetting()
        {
            this.PageSize = 50;
        }
        private int pageSize;
        
        public bool Enable { get; set; }

        public int PageSize
        {
            get { return pageSize; }
            set
            {
                if (value < 10)
                    throw new ArgumentOutOfRangeException("每页最小值不能少于10");
                pageSize = value;
            }
        }

        public string[] Sort { get; set; }
       
        public int PageIndex { get; set; }

       
        public int? RowCount { get; set; }
    }
     
}
