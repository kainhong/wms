#region Copyright(C) 2007
/*
 * Copyright XD  Information System 2007
 * 
 * Authors:		xiangdong
 * Created:		2007.09.10
 * Description:
 *     数据字段集合类
 * 
 * * 2008.03.01 xiangdong 重新规划基类 版本升级为 2.0
 * 
 * Last Modified: 2008.03.13
 * Version:		  2.0
 */
#endregion


using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace WMS.Web.Models
{
	/// <summary>
	/// 数据字段集合类
	/// </summary>
	[Serializable]
	public class DataFieldCollection : List<DataField>
	{
        public DataField this[string field]
        {
            get
            {
                return this.FirstOrDefault(c => string.Compare(c.FieldName,field,true)==0);
            }
        }
	}
}
