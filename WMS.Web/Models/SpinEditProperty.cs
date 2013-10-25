#region Copyright(C) 2007 - 2010
/*
 * Copyright XD 15Easy Information System  2007 - 2010
 * 
 * Authors:	xiangdong
 * Created:	2010.12.21	
 * Description:
 *     
 *      SpinEdit 属性
 * 
 * Last Modified: 2010.12.21	
 * Version:		  3.0
 */
#endregion

#region Using Define
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
#endregion

namespace WMS.Web.Models
{
	/// <summary>
	/// SpinEdit 属性
	/// </summary>
	[Serializable]
	public class SpinEditProperty
	{
		private bool fShowButton = false;
		//[D("数字控件增量是否显示按钮")]
		//[D("4.Search")]
		//[DtValue(false)]
		public bool ShowButton
		{
			get
			{
				return fShowButton;
			}
			set
			{
				fShowButton = value;
			}
		}

		private int fIncrement = 1;
		//[D("数字控件增量")]
		//[D("4.Search")]
		//[DtValue(1)]
		public int Increment
		{
			get
			{
				return fIncrement;
			}
			set
			{
				fIncrement = value;
			}
		}

		private int fMaxValue = 100;
		//[D("数字控件最大值")]
		//[D("4.Search")]
		//[DtValue(100)]
		public int MaxValue
		{
			get
			{
				return fMaxValue;
			}
			set
			{
				fMaxValue = value;
			}
		}

		private int fMinxValue = 0;
		//[D("数字控件最小值")]
		//[D("4.Search")]
		//[DtValue(0)]
		public int MinValue
		{
			get
			{
				return fMinxValue;
			}
			set
			{
				fMinxValue = value;
			}
		}
	}
     
}
