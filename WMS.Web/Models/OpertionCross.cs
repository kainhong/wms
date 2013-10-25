using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Runtime.Serialization;

namespace WMS.Web.Models
{
    [DataContract]
    public class OpertionCross
    {
        [DataMember]
        public string FieldName { get; set; }
        
        #region Cross
        EnumCrossCommonType crossCommonType = EnumCrossCommonType.Custom;
        //[D("常用交叉报表类型")]
        //[D("5.Cross")]
        //[DtValue(EnumCrossCommonType.Custom)]
        [DataMember]
        public EnumCrossCommonType CrossCommonType
        {
            get;
            set;
        }

        EnumDisplayValueType crossExprType = EnumDisplayValueType.dvtConstString;
        //[D("交叉报表显示标题类型 通常字符串和SQL语句")]
        //[D("5.Cross")]
        //[DtValue(EnumDisplayValueType.dvtConstString)]
        [DataMember]
        public EnumDisplayValueType CrossExprType
        {
            get { return crossExprType; }
            set { crossExprType = value; }
        }

        private string[] crossExpr;
        //[D("交叉报表显示标题表达式,设置数字列中数字对应含义(可以式常量和SQL语句),例如: 1=编辑 2=审核 或 select ID,Name from Table")]
        //[D("5.Cross")]
        [DataMember]
        public string[] CrossExpr
        {
            get { return crossExpr; }
            set { crossExpr = value; }
        }

        private string crossValueField = "";

        //[D("交叉报表值字段,当值字段为多个,请用分号(;)隔开.当计算字段引用交叉字段，交叉字段名称为  [FieldName]_?_? 如FieldName=Size 则名称为Size_0_0,Size_1_0")]
        //[D("5.Cross")]
        [DataMember]
        public string CrossValueField
        {
            get { return crossValueField; }
            set { crossValueField = value; }
        }

        bool isCrossSum = false;

        //[D("是否进行交叉合计,产生合计的字段名称为 SUM_ + [FieldName] + [序号]，序号为0-9")]
        //[D("5.Cross")]
        //[DtValue(false)]
        [DataMember]
        public bool CrossIsSum
        {
            get { return isCrossSum; }
            set { isCrossSum = value; }
        }

        string crossSumDisplayLabel = "合计";
        //[D("交叉合计显示标题")]
        //[D("5.Cross")]
        //[DtValue("")]
        [DataMember]
        public string CrossSumDisplayLabel
        {
            get { return crossSumDisplayLabel; }
            set { crossSumDisplayLabel = value; }
        }

        [Browsable(false)]
        public string SumFieldName
        {
            get
            {
                return "SUM_" + FieldName;
            }
        }

        bool crossSumPosition = true;
        //[D("交叉合计显示位置 true 显示在后面, false 显示在前面")]
        //[D("5.Cross")]
        //[DtValue(true)]
        [DataMember]
        public bool CrossSumPosition
        {
            get { return crossSumPosition; }
            set { crossSumPosition = value; }
        }

        bool crossFilterCondition = true;
        //[D("是不是生成交叉表的过虑条件")]
        //[D("5.Cross")]
        //[DtValue(true)]
        [DataMember]
        public bool CrossFilterCondition
        {
            get { return crossFilterCondition; }
            set { crossFilterCondition = value; }
        }
        

        [NonSerialized]
        private CrossColumnCollection crossColumns = new CrossColumnCollection();

        public CrossColumnCollection CrossColumns
        {
            get { return crossColumns; }
            set { crossColumns = value; }
        }

        public enum EnumCrossCommonType
        {
            Yard,
            Custom
        }

        #endregion
    }


    internal class OpertionCrossPropertyConverter : ExpandableObjectConverter
    {

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type t)
        {
            if (t == typeof(string))
            {

                return true;

            }

            return base.CanConvertFrom(context, t);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo info, object value)
        {

            if (value is string)
            {

                try
                {

                    string s = (string)value;

                    string[] valList = s.Split(',', ';');

                    if (valList.Length == 4)
                    {
                        var property = new OpertionCross();
                            

                        return property;
                    }
                }
                catch
                {
                    throw new ArgumentException("Can not convert '" + (string)value + "' to type SpinEditProperty");
                }

            }

            return base.ConvertFrom(context, info, value);

        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {

            if (destType == typeof(string) && value is OpertionCross)
            {

                var property = (OpertionCross)value;


                //return string.Format("{0},{1},{2},{3}", property.ShowButton, property.Increment, property.MaxValue, property.MinValue);

            }

            return base.ConvertTo(context, culture, value, destType);
        }
	
    }
}
