 

using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace WMS.Web.Models
{
	/// <summary>
	/// ����Դ����
	/// </summary>
	public class DataQueryCollection : List<DataQuery>
	{
		#region Open & Close
		
		#endregion

		#region Create Contorl Layout
		 
		#endregion
 

		/// <summary>
		/// ���DataQuery����DataQuery�������޸ģ�������HasDataModifiedDisableGrid���ͽ�������
		/// </summary>
        public bool HasDataModified
        {
            get;
            set;
        }
	}
}
