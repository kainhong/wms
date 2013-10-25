#region Copyright(C) 2010
/*
 * Copyright XD  Information System 2007
 * 
 * Authors:		xiangdong
 * Created:		2007.09.10
 * Description:
 *     数据字段类
 * 
 * * 2008.03.01 xiangdong 重新规划基类 版本升级为 2.0
 * * 2010.11.01 xiangdong 重构整理
 * * 2010.11.30 xiangdong 解决网格新增行、明细新增行，以及错误提示相关问题
 * + 2011.01.17 xiangdong 增加字段权限控制
 * 
 * Last Modified: 2011.01.17
 * Version:		  3.0
 */
#endregion

#region Using Define
using System;
using System.ComponentModel;

using System.Xml.Serialization;
using System.Runtime.Serialization;


#endregion

namespace WMS.Web.Models
{
    /// <summary>
    /// 数据字段类
    /// </summary>
    public class DataField : QueryField
    {
        #region Constructor
        public DataField()
        {
            this.OpertionCross = new OpertionCross();
            BandIndex = "0";
        }
        #endregion
        
        #region Data
        public override string FieldName
        {
            get
            {
                return base.FieldName;
            }
            set
            {
                base.FieldName = value;
                this.OpertionCross.FieldName = value;
            }
        }

        /// <summary>
        /// 是否只读 
        /// <summary>
        //[D("是否只读")]
        //[DtValue(false)]
        //[D("2.Data")]
        public bool ReadOnly
        {
            get;
            set;
        }

        //[D("是否主键,可以由多个字段组合")]
        //[DtValue(false)]
        //[D("2.Data")]
        public bool IsPrimaryKey
        {
            get;
            set;
        }

        private bool fIsUniqueField = false;

        //[D("是否唯一约束")]
        //[DtValue(false)]
        //[D("2.Data")]
        public bool IsUniqueField
        {
            get
            {
                return fIsUniqueField;
            }
            set
            {
                fIsUniqueField = value;
            }
        }

        public enum AutoIncrementFieldType
        {
            aitLocal,
            aitServer,
            None
        }

        AutoIncrementFieldType fAutoIncrementType = AutoIncrementFieldType.None;
        /// <summary>
        /// 是否自动增加字段
        /// </summary> 
        //[D("自动增加字段类型, aitLocal 本地自增字段, aitServer服务器自增字段")]
        //[DtValue(AutoIncrementFieldType.None)]
        //[D("2.Data")]
        public AutoIncrementFieldType AutoIncrementType
        {
            get
            {
                return fAutoIncrementType;
            }
            set
            {
                //if (DbTypeHelper.IsNumber(this.DataType) && fAutoIncrementType != value)
                {
                    fAutoIncrementType = value;
                }
            }
        }

        /// <summary>
        /// 服务器自增字段编号
        /// </summary>
        //[D("自动增加字段编号, 为 0 则为模块编号 ")]
        //[DtValue("0")]
        //[D("2.Data")]
        public string ServerAutoIncNO
        {
            get;
            set;
        }

        private bool fIsNewRowInherit = false;
        /// <summary>
        /// 新记录是否继承
        /// </summary>
        //[D("新记录是否继承")]
        //[DtValue(false)]
        //[D("2.Data")]
        public bool IsNewRowInherit
        {
            get
            {
                return fIsNewRowInherit;
            }
            set
            {
                fIsNewRowInherit = value;
            }
        }

        private bool fIsMasterTableUpdateField = false;
        //[D("是否主表更新字段")]
        //[DtValue(false)]
        //[D("2.Data")]
        public bool IsMasterTableUpdateField
        {
            get { return fIsMasterTableUpdateField; }
            set { fIsMasterTableUpdateField = value; }
        }

        private bool fOnlyInsertEdit = false;
        //[D("只有在新增状态可以进行编辑,但不能进行修改.")]
        //[DtValue(false)]
        //[D("2.Data")]
        public bool OnlyInsertEdit
        {
            get { return fOnlyInsertEdit; }
            set { fOnlyInsertEdit = value; }
        }

        private bool fAutoBindDetailControl = true;
        //[D("是否自动绑定明细控件")]
        //[DtValue(true)]
        //[D("2.Data")]
        public bool AutoBindDetailControl
        {
            get { return fAutoBindDetailControl; }
            set { fAutoBindDetailControl = value; }
        }

        private bool fDetailHasRecordCannotModify = false;
        //[D("明细表有记录时，此字段是否可以修改")]
        //[DtValue(true)]
        //[D("2.Data")]
        public bool DetailHasRecordCannotModify
        {
            get { return fDetailHasRecordCannotModify; }
            set { fDetailHasRecordCannotModify = value; }
        }
        #endregion

        #region View
        
        private bool fVisibleInDetail = false;

        /// <summary>
        /// 在面板中是否可见
        /// </summary>
        //[D("在面板中是否可见")]
        //[DtValue(false)]
        //[D("3.View")]
        public bool VisibleInDetail
        {
            get { return fVisibleInDetail; }
            set { fVisibleInDetail = value; }
        }


        private bool fVisibleInGrid = true;   //是否可见
        /// <summary>
        /// 在网格中是否可见
        /// </summary>
        //[D("在网格中是否可见")]
        //[DtValue(true)]
        //[D("3.View")]
        public bool VisibleInGrid
        {
            get
            {
                return fVisibleInGrid;
            }
            set
            {
                fVisibleInGrid = value;
            }
        }

        private bool fTreeColumnNoVisibleCreate = true;
        //[D("TreeList不可见的列也创建,用于保存数据")]
        //[DtValue(true)]
        //[D("3.View")]
        public bool TreeColumnNoVisibleCreate
        {
            get { return fTreeColumnNoVisibleCreate; }
            set { fTreeColumnNoVisibleCreate = value; }
        }
        
        /// <summary>
        /// 用户有字段权限?
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool? UserHasFieldRight
        {
            get;
            set;
        }

        //[D("交叉视图设置")]
        //[DtValue(true)]
        //[D("3.View")]
        public OpertionCross OpertionCross
        {
            get;
            set;
        }
        #endregion
        
        #region Quick Input
        private string[] fQuickInputSQL = null;
        /// <summary>
        /// 快速输入SQL
        /// </summary>
        //[D("快速输入Select语句，例如: select Name from XXTable where ID = @ID")]
        //[D("7.Quick Input")]
        public string[] QuickInputSQL
        {
            get { return fQuickInputSQL; }
            set
            {
                fQuickInputSQL = value;
            }
        }

        private bool fQuickInputShowError = true;
        //[D("快速输入记录为空时候，是否显示提示信息")]
        //[DtValue(true)]
        //[D("7.Quick Input")]
        public bool QuickInputShowError
        {
            get { return fQuickInputShowError; }
            set { fQuickInputShowError = value; }
        }

        private string fQuickInputEmptyError = "[{0}]资料没有找到!";
        //[D("快速输入记录为空时候，错误提示信息")]
        //[D("7.Quick Input")]
        //[DtValue("[{0}]资料没有找到!")]
        public string QuickInputEmptyError
        {
            get { return fQuickInputEmptyError; }
            set { fQuickInputEmptyError = value; }
        }

        private string[] fQuickInputUpdateField = null;
        //[D("快速输入更新字段，多字段用逗号隔开。如果为空，表示 QuickInputSQL 对应字段全部更新")]
        //[D("7.Quick Input")]
        public string[] QuickInputUpdateField
        {
            get { return fQuickInputUpdateField; }
            set { fQuickInputUpdateField = value; }
        }

        private bool fCheckExistRecord = false;
        //[D("检查记录是否存在!服务器实时检查,和 IsPrimaryKey 和 UniqueField 实现有区别，这里是服务器级别搜索比较.")]
        //[DtValue(false)]
        //[D("7.Quick Input")]
        public bool CheckExistRecord
        {
            get { return fCheckExistRecord; }
            set { fCheckExistRecord = value; }
        }

        private string fCheckExistTableName = "";
        //[D("检查记录的表,为空的时候使用DataQuery.TableName")]
        //[DtValue("")]
        //[D("7.Quick Input")]
        public string CheckExistTableName
        {
            get { return fCheckExistTableName; }
            set { fCheckExistTableName = value; }
        }

        private ExistShowErrorType fCheckExistShowErrorType = ExistShowErrorType.NotExistShow;
        //[D("检查记录时,是存在的时候显示错误,还是不存在的显示错误")]
        //[DtValue(ExistShowErrorType.NotExistShow)]
        //[D("7.Quick Input")]
        public ExistShowErrorType CheckExistShowErrorType
        {
            get { return fCheckExistShowErrorType; }
            set { fCheckExistShowErrorType = value; }
        }

        public enum ExistShowErrorType
        {
            ExistShow,
            NotExistShow
        }
        #endregion

        #region Linkage
        //[D("级联更新字段")]
        //[D("5.Combox")]
        public string FilterExpression { get; set; }

        public string BandIndex { get; set; }

        public int RowCount { get; set; }

        #endregion
    }

    public class QuickInputEventAgrs:EventArgs
    {
        public QuickInputEventAgrs ( )
	    {

        }

        /// <summary>
        /// 当前行的值，如果只有一个参数时，默认使用该值
        /// </summary>
        public object Value { get;  set; }

        /// <summary>
        /// 自定义脚本
        /// </summary>
        public string Script { get; set; }

        /// <summary>
        /// 如果不需要自动处理脚本，需要手动修改为True
        /// </summary>
        public bool Handled { get; set; }
    }
}
