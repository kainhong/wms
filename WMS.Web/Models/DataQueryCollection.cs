 

using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace WMS.Web.Models
{
	/// <summary>
	/// 数据源集合
	/// </summary>
	public class DataQueryCollection : List<DataQuery>
	{
		#region Open & Close
		
		#endregion

		#region Create Contorl Layout
		 
		#endregion
 

		/// <summary>
		/// 如果DataQuery的子DataQuery有数据修改，并设置HasDataModifiedDisableGrid，就禁用网格
		/// </summary>
        public bool HasDataModified
        {
            get;
            set;
        }
	}
}
