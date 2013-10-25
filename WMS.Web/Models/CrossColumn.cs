using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WMS.Web.Models
{
    [DataContract]
    public class CrossColumn
    {
        [DataMember]
        public string ColumnName { get; set; }
        [DataMember]
        public string ColumnValue { get; set; }
        [DataMember]
        public string ColumnLabel { get; set; }
        [DataMember]
        public string ColumnFieldName { get; set; }
        [DataMember]
        public bool MutilValue { get; set; }

        public CrossColumn()
        {

        }

        public CrossColumn(string colName, string colValue)
            : this(colName, string.Empty, colValue, string.Empty, false)
        {

        }

        public CrossColumn(string colName, string fldName, string colValue, string colLabel, bool mutilValue)
        {
            ColumnName = colName;
            ColumnFieldName = fldName;
            ColumnValue = colValue;
            ColumnLabel = colLabel;
            MutilValue = mutilValue;
        }
    }

    [DataContract]
    public class CrossColumnCollection : List<CrossColumn>
    {
        public string GetCrossColumnName(string val, string valFieldName)
        {
            foreach (CrossColumn col in this)
            {
                if (col.ColumnValue.Equals(val) && col.ColumnFieldName.Equals(valFieldName))
                    return col.ColumnName;
            }
            return "";
        }
    }

    [DataContract]
    public class CrossField
    {
        [DataMember]
        public string NameFieldName { get; set; }
        [DataMember]
        public string ValueFieldName { get; set; }
        [DataMember]
        public string DisplayLabel { get; set; }
        [DataMember]
        public bool MutilValue
        {
            get;
            set;
        }
        [DataMember]
        public string[] ValueFieldNameAry
        {
            get;
            set;
        }
        [DataMember]
        public string[] DisplayLabelAry
        {
            get;
            set;
        }

        [DataMember]
        public bool IsSum { get; set; }
        CrossColumnCollection crossColumns = new CrossColumnCollection();

        [DataMember]
        public CrossColumnCollection CrossColumns
        {
            get { return crossColumns; }
        }
        
        public CrossField(string name, string value, string label, bool isSum)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
            {
                throw new Exception("交叉表字段设置不正确,请检查 FieldName 和 crossValueField");
            }

            NameFieldName = name;
            ValueFieldName = value;
            DisplayLabel = label;
            IsSum = isSum;
        }

        
    }
}