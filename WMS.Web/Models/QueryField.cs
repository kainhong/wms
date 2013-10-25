#region Using Define
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Data;
#endregion

namespace WMS.Web.Models
{
	/// <summary>
	/// 查询字段类
	/// </summary>
	[Serializable]
	public class QueryField : BaseField
	{
		#region IQueryColumn Members

		private SpinEditProperty fSpinEditProperty = new SpinEditProperty();

		//[D("SpinEdit(数字输入)控件属性，不是此类控件此属性不起作用")]
		//[DtValue(false)]
		//[D("3.View")]
		public SpinEditProperty SpinEditProperty
		{
			get { return fSpinEditProperty; }
			set { fSpinEditProperty = value; }
		}

		private bool fIsSearchCondition = false;
		//[D("是查询条件")]
		//[DtValue(false)]
		//[D("4.Search")]
		public bool IsSearchCondition
		{
			get { return fIsSearchCondition; }
			set { fIsSearchCondition = value; }
		}

		private string fConditionFieldName = string.Empty;
		//[D("查询条件字段名称")]
		//[D("4.Search")]
		public string ConditionFieldName
		{
			get
			{
				if (string.IsNullOrEmpty(fConditionFieldName))
				{
					fConditionFieldName = FieldName;
				}

				return fConditionFieldName;
			}
			set
			{
				fConditionFieldName = value;
			}
		}

		private short fConditionControlNumber = 1;

		//[D("查询控件数目")]
		//[D("4.Search")]
		//[DtValue((short)1)]
		public short ConditionControlNumber
		{
			get
			{
				return fConditionControlNumber;
			}
			set
			{
				fConditionControlNumber = value;
			}
		}

		private EnumQueryConditionDataType fConditionDataType = EnumQueryConditionDataType.cdtString;
		//[D("查询条件数据类型")]
		//[D("4.Search")]
		//[DtValue(EnumQueryConditionDataType.cdtString)]
		public EnumQueryConditionDataType ConditionDataType
		{
			get
			{
				return fConditionDataType;
			}
			set
			{
				fConditionDataType = value;
			}
		}

		private bool fIsRequire = false;
		//[D("是否必输条件")]
		//[DtValue(false)]
		//[D("4.Search")]
		public bool IsRequire
		{
			get
			{
				return fIsRequire;
			}
			set
			{
				fIsRequire = value;
			}
		}

		private bool fIsLikeSearch = false;

		//[D("是否使用Like")]
		//[DtValue(false)]
		//[D("4.Search")]
		public bool IsLikeSearch
		{
			get
			{
				return fIsLikeSearch;
			}
			set
			{
				fIsLikeSearch = value;
			}
		}

		private int fColumnSpan = 1;
		//[D("查询条件跨几列")]
		//[D("4.Search")]
		//[DtValue(1)]
		public int ColumnSpan
		{
			get
			{
				return fColumnSpan;
			}
			set
			{
				fColumnSpan = value;
			}
		}

        //[D("查询条件跨几列")]
        //[D("4.Search")]
        //[DtValue(false)]
        public bool Multible
        {
            get;
            set;
        }

        //[D("查询条件依赖条件")]
        //[D("4.Search")]
        public string ConditionFilter
        {
            get;
            set;
        }

		private bool fShowInPanel = true;
		//[D("查询条件是否显示在面板上")]
		//[D("4.Search")]
		//[DtValue(true)]
		public bool ShowInPanel
		{
			get
			{
				return fShowInPanel;
			}
			set
			{
				fShowInPanel = value;
			}
		}

		[NonSerialized]
		private ClauseType fClauseType = ClauseType.Equals;

		[Browsable(false)]
		[XmlIgnore]
		public ClauseType ClauseType
		{
			get
			{
				return fClauseType;
			}
			set
			{
				fClauseType = value;
			}
		}

		[NonSerialized]
		private string[] fConditionVar = null;

		[Browsable(false)]
		[XmlIgnore]
		public string[] ConditionVar
		{
			get
			{
				return fConditionVar;
			}
			set
			{
				fConditionVar = value;
			}
		}

        [Browsable(false)]
        public string WhereFormatSQL
        {
            get;
            set;
        }

		[Browsable(false)]
		[XmlIgnore]
		public string ErrorMsg
		{
			get
			{
				return "[" + Caption + "]" + ("必须输入!");
			}
		}
		#endregion






        
    }
}
