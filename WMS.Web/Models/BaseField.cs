#region Copyright(C) 2010
/*
 * Copyright XD  Information System 2007
 * 
 * Authors:		xiangdong
 * Created:		2010.11.1
 * Description:
 *     基本字段
 * 
 * * 2010.11.1 xiangdong 重构
 * 
 * Last Modified: 2010.11.1
 * Version:		  3.0
 */
#endregion

#region Using Define
using System;
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;
#endregion

namespace WMS.Web.Models
{
	/// <summary>
	/// 基本字段
	/// </summary>
	public class BaseField
	{
		#region Information
		private string fName = "";

		/// <summary>
		/// 字段名称
		/// </summary>
		//[D("名称")]
		//[D("1.Info")]
		public string Name
		{
			get { return fName; }
			set { fName = value; }
		}

		private string fFieldName = "";
		/// <summary>
		/// 数据字段名称
		/// </summary>
		//[D("字段名称")]
		//[D("1.Info")]
		public virtual string FieldName
		{
			get
			{
				return fFieldName;
			}
			set
			{
				fFieldName = value;
			}
		}

		private string fCaption = ""; //显示标题
		/// <summary>
		/// 字段标题
		/// </summary>
		[Browsable(true)]
		//[D("字段标题")]
		//[D("1.Info")]
		public string Caption
		{
			get
			{
				return fCaption;
			}
			set
			{
				fCaption = value;
			}
		}
		#endregion

		#region DataQuery
		
 
		#endregion

		#region Data

		private DbType fDataType = DbType.String;
		/// <summary>
		/// 数据类型
		/// </summary> 
		//[D("数据类型")]
		//[DtValue(DbType.String)]
		//[D("2.Data")]
		public DbType DataType
		{
			get
			{
				return fDataType;
			}
			set
			{
				fDataType = value;
			}
		}

		private string fDefaultValue = ""; //缺省值 
		/// <summary>
		/// 缺省值
		/// </summary> 
		public string DefaultValue
		{
			get
			{
				return fDefaultValue;
			}
			set
			{
				fDefaultValue = value;
			}
		}



		EnumFieldType fFieldType = EnumFieldType.NormalField;
		/// <summary>
		/// 字段类型，正常字段还是计算字段
		/// </summary>
		//[D("字段类型")]
		//[DtValue(EnumFieldType.NormalField)]
		//[D("3.View")]
		public EnumFieldType FieldType
		{
			get
			{
				return fFieldType;
			}
			set
			{
				fFieldType = value;
			}
		}


		private string fExpression = "";
		/// <summary>
		/// 计算表达式
		/// </summary> 
		//[D("计算表达式")]
		//[DtValue("")]
		//[D("2.Data")]
		public string Expression
		{
			get
			{
				return fExpression;
			}
			set
			{
				fExpression = value;
			}
		}

		private bool fAllowDBNull = true; //是否允许空值
		/// <summary>
		/// 是否允许空值
		/// </summary>
		//[D("是否允许空值")]
		//[D("2.Data")]
		//[DtValue(true)]
		public bool AllowDBNull
		{
			get
			{
				return fAllowDBNull;
			}
			set
			{
				fAllowDBNull = value;
			}
		}
		#endregion

		#region View
		private int fDisplayWidth = 10; //显示宽度
		/// <summary>
		/// 显示宽度(字符数)
		/// </summary> 
		//[D("显示宽度(字符数)")]
		//[DtValue(10)]
		//[D("3.View")]
		public int DisplayWidth
		{
			get
			{
				return fDisplayWidth;
			}
			set
			{
				fDisplayWidth = value;
			}
		}

		private int fMaxWidth = 20; //字段最大宽度
		/// <summary>
		/// 最大宽度(字符数)
		/// </summary> 
		//[D("最大宽度(字符数)")]
		//[DtValue(20)]
		//[D("3.View")]
		public int MaxWidth
		{
			get
			{
				return fMaxWidth;
			}
			set
			{
				fMaxWidth = value;
			}
		}


		private string fToolTip = ""; //提示信息   
		/// <summary>
		/// 提示信息
		/// </summary>
		//[D("提示信息")]
		//[DtValue("")]
		//[D("3.View")]
		public string ToolTip
		{
			get
			{
				return fToolTip;
			}
			set
			{
				if (fToolTip == value)
					return;
				fToolTip = value;
			}
		}

		EnumDisplayType fDisplayType = EnumDisplayType.Text;
		/// <summary>
		/// 字段显示类型
		/// </summary>
		//[D("字段显示类型")]
		//[DtValue(EnumDisplayType.Text)]
		//[D("3.View")]
		public EnumDisplayType DisplayType
		{
			get
			{
				return fDisplayType;
			}
			set
			{
				fDisplayType = value;
			}
		}
		#endregion

		#region Format
		private string fFormatString = ""; //显示格式化字符串
		/// <summary>
		/// 显示格式化字符串，当FormatType=Numeric时：c?:金额; e?:科学记数; n?:数字; x?:十六进制(?代表位数)；当FormatType=DateTime时：d:短日期; D:长日期; t:短时间; T:长时间; f:短日期时间; F:长日期时间; g:普通短日期时间; G:普通长日期时间；其他格式有：0,#,.,yy,yyyy,MM,MMM,MMMM,dd,ddd,dddd,/,hh,mm,ss,tt,:
		/// </summary> 
		//[D("显示格式化字符串，当FormatType=Numeric时：c?:金额; e?:科学记数; n?:数字; x?:十六进制(?代表位数)；当FormatType=DateTime时：d:短日期; D:长日期; t:短时间; T:长时间; f:短日期时间; F:长日期时间; g:普通短日期时间; G:普通长日期时间；其他格式有：0,#,.,yy,yyyy,MM,MMM,MMMM,dd,ddd,dddd,/,hh,mm,ss,tt,:")]
		//[DtValue("")]
		//[D("6.Format")]
		public string FormatString
		{
			get
			{
				return fFormatString;
			}
			set
			{
				fFormatString = value;
			}
		}

		private FormatType fFormatType = FormatType.None; //显示格式化类型
		/// <summary>
		/// 显示格式化类型
		/// </summary>
		//[DtValue(FormatType.None)]
		//[D("显示格式化类型")]
		//[D("6.Format")]
		public FormatType FormatType
		{
			get
			{
				return fFormatType;
			}
			set
			{
				fFormatType = value;
			}
		}

		private string fEditMask = "";
		/// <summary>
		/// 编辑掩码,可用格式为:L,l,A,a,C,c,0,9,#，例如：(000)000-00-00
		/// </summary> 
		//[D("编辑掩码,可用格式为:L,l,A,a,C,c,0,9,#，例如：(000)000-00-00")]
		//[DtValue("")]
		//[D("6.Format")]
		public string EditMask
		{
			get
			{
				return fEditMask;
			}
			set
			{
				fEditMask = value;
			}
		}

		MaskType fMaskType = MaskType.None;
		//[D("编辑掩码类型")]
		//[DtValue(MaskType.None)]
		//[D("6.Format")]
		public MaskType MaskType
		{
			get { return fMaskType; }
			set { fMaskType = value; }
		}

		CharacterCasing fCharacterCasing = CharacterCasing.Normal;
		//[D("大小写转换方式")]
		//[DtValue(CharacterCasing.Normal)]
		//[D("6.Format")]
		public CharacterCasing CharacterCasing
		{
			get { return fCharacterCasing; }
			set { fCharacterCasing = value; }
		}
		#endregion

		#region Cell Merge

		private DefaultBoolean allowCellMerge = DefaultBoolean.Default;
		//[D("允许网格自动合并")]
		//[D("8.Merge")]
		//[DtValue(DefaultBoolean.Default)]
		public DefaultBoolean AllowCellMerge
		{
			get { return allowCellMerge; }
			set { allowCellMerge = value; }
		}

		//modify 2009.6.30 yanglei 添加列的合并属性
		private string cellMergeReferFieldName = "";
		//[D("网格合并参考字段，多个字段用分号隔开")]
		//[D("8.Merge")]
		public string CellMergeReferFieldName
		{
			get { return cellMergeReferFieldName; }
			set { cellMergeReferFieldName = value; }
		}
		#endregion

		#region Combox
		private string fDisplayMember = "Name";
		//[D("显示的字段(三种方式 1.字段名 2.字段名:标题 3.字段名:标题:字段显示宽度:是显示字段(默认False);显示多个字段用';'隔开。如源为常量，默认为Name )")]
		//[D("5.Combox")]
		//[DtValue("Name")]
		public string DisplayMember
		{
			get
			{
				return fDisplayMember;
			}
			set
			{
				fDisplayMember = value;
			}
		}

		private string fValueMember = "ID";
		//[D("值字段(当源为常量时,Int型使用ID,String使用Code)")]
		//[D("5.Combox")]
		//[DtValue("ID")]
		public string ValueMember
		{
			get
			{
				return fValueMember;
			}
			set
			{
				fValueMember = value;
			}
		}

        public string[] DisplayValue
        {
            get;
            set;
        }

        EnumDisplayValueType fDispValueType = EnumDisplayValueType.dvtConstString;

		//[D("下拉框类型")]
		//[D("5.Combox")]
        //[DtValue(EnumDisplayValueType.dvtConstString)]
        public EnumDisplayValueType DispValueType
		{
			get { return fDispValueType; }
			set
			{
				fDispValueType = value;
			}
		}

		private int fDropDownRows = 8;
		//[D("下拉行数")]
		//[D("5.Combox")]
		//[DtValue(8)]
		public int DropDownRows
		{
			get
			{
				return fDropDownRows;
			}
			set
			{
				fDropDownRows = value;
			}
		}


		bool fEnableComboBoxInput = false;
		//[D("下拉框允许输入,只对Combobox有效")]
		//[D("5.Combox")]
		//[DtValue(false)]
		public bool EnableComboBoxInput
		{
			get { return fEnableComboBoxInput; }
			set { fEnableComboBoxInput = value; }
		}

		#endregion

		#region Base Method Override
		public override string ToString()
		{
			string s = string.IsNullOrEmpty(fFieldName) ? Name : fFieldName;
			if (!string.IsNullOrEmpty(fCaption))
			{
				s = s.PadRight(25) + fCaption;
			}
			return s;
		}
		#endregion

		#region Method
		 
		#endregion
	}
}
