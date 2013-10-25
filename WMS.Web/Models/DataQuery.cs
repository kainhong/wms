#region Copyright(C) 2010
/*
 * Copyright XD  Information System  2007 - 2010
 * 
 * Authors:		xiangdong
 * Created:		2007.09.10
 * Description:
 *     数据源控件
 *     
 *     DataQuery 含 DataField 
 *     
 * * 2008.03.01 xiangdong   重新规划基类 版本升级为 2.0
 * * 2009.11.14 xiangdong   修复主表为空，从表有参数没有被替换的BUG
 * * 2009.12.24 zhoujingbo  主表无记录时，从表不允许新增
 * * 2010.11.01 xiangdong	全部重构
 * + 2011.01.18 xiangdong	增加数据权限
 * * 2013.06.06 tenglingbo  修改原因，当查询条件为空时，总是记录上次的条件所以查询不到记录 .方法名 ReOpen()
 * Last Modified: 2010.11.01
 * Version:		  3.0
 */
#endregion

#region Using Define
using System;
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;
using System.Runtime.Serialization;

#endregion

namespace WMS.Web.Models
{
    /// <summary>
    /// 数据查询类,仿Delphi的TQuery类  
    /// </summary>
    public class DataQuery
    {
        
        #region Constructor
        public DataQuery()
        {
            this.Fields = new DataFieldCollection();
        }
 
        #endregion

        #region Information
        /// <summary>
        /// DataQuery名称
        /// </summary>
        //[D("1.Info")]
        //[D("数据源名称")]
        public string Name
        {
            get;
            set;
        }


        public int OrderNO
        {
            get;
            set;
        }

        public bool AllowCellMerge
        {
            get;
            set;
        }

        #endregion

        #region Property
    

        

        /// <summary>
        /// 字段集合
        /// </summary>

        public DataFieldCollection Fields
        {
            get;
            set;
        }

       
 

        #endregion

        #region DataSource
        public string ModuleID
        {
            get;
            set;
        }

        
        private bool fShowDeleteRecord = true;
        //[D("2.Data")]
        //[D("显示删除记录")]
        //[DtValue(true)]
        public bool ShowDeleteRecord
        {
            get { return fShowDeleteRecord; }
            set { fShowDeleteRecord = value; }
        }

        private bool fParentAutoOpen = false;

        //[D("2.Data")]
        //[D("表由主表自动打开")]
        //[DtValue(false)]
        public bool ParentAutoOpen
        {
            get { return fParentAutoOpen; }
            set
            {
                fParentAutoOpen = value;

                if (fParentAutoOpen)
                {
                    fAutoOpen = false;
                }
            }
        }

        
        /// <summary>
        /// 是否可以插入记录
        /// </summary>
        //[D("2.Data")]
        //[D("是否允许新增")]
        //[DtValue(true)]
        public bool AllowInsert
        {
            get;
            set;
        }

        //[D("2.Data")]
        //[D("是否允许粘贴")]
        //[DtValue(true)]
        public bool AllowPaste { get; set; }

        /// <summary>
        /// 是否可以删除
        /// </summary>
         
        public bool AllowDelete
        {
            get;
            set;
        }

        
        public string TableName
        {
            get;
            set;
        }

        public string AliasTableName
        {
            get;
            set;
        }

      
        
        public string UniqueFieldList
        {
            get;
            set;
        }

        

        private bool fAutoOpen = true;

        /// <summary>
        /// 自动打开数据源
        /// </summary>    
        //[D("2.Data")]
        //[D("是否自动打开数据源,当为单据表设置为false")]
        //[DtValue(true)]
        public bool AutoOpen
        {
            get { return fAutoOpen; }
            set
            {
                fAutoOpen = value;

                if (fAutoOpen)
                {
                    fParentAutoOpen = false;
                }
            }
        }

        /// <summary>
        /// 数据源是否可以编辑，Editable = ! ReadOnly
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        public bool Editable
        {
            get
            {
                return !ReadOnly;
            }
            set
            {
                ReadOnly = !value;
            }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        //[D("2.Data")]
        //[D("数据源是否只读")]
        //[DtValue(false)]
        public bool ReadOnly
        {
            get;
            set;
        }

        //[D("2.Data")]
        //[D("有数据修改禁用网格")]
        //[DtValue(false)]
        public bool HasDataModifiedDisableGrid
        {
            get;
            set;
        }

        ////[D("网格多表头显示")]
        ////[D("1.Basic Information")]
        [Browsable(false)]
        [XmlIgnore]
        public bool GridMultiHead
        {
            get
            {
                return this.GridShowType == EnumGridShowType.showInBandGrid 
                        || GridShowType == EnumGridShowType.showInAdvBandGrid;
            }
          
        }

        /// <summary>
        /// SELECT 语句
        /// </summary>
        [Browsable(false)]
        public string SelectSQL { get; set; }

        /// <summary>
        /// 数据更新信息,UpdateSQL,InsertSQL,DeleteSQL
        /// <summary>
        [Browsable(false)]
        public DataUpdateSQL UpdateSQL { get; set; }

        private bool fHasRecordLog = false;
        /// <summary>
        /// 记录日志
        /// </summary>
        //[D("2.Data")]
        //[D("是否记录日志")]
        //[DtValue(false)]
        public bool HasRecordLog
        {
            get { return fHasRecordLog; }
            set { fHasRecordLog = value; }
        }


        //[D("2.Data")]
        //[D("级联字段名称。多个字段用分号隔开。")]
        public string CascadeDeleteFieldName
        {
            get;
            set;
        }

        //[D("2.Data")]
        //[D("级联删除表或删除前检查表是否存在记录，存在不能删除。多个表用分号隔开。")]
        public string CascadeDeleteTables
        {
            get;
            set;
        }

        EnumCascadeDeleteWay fCascadeDeleteWay = EnumCascadeDeleteWay.CascadeDelete;

        //[D("2.Data")]
        //[D("级联处理方式")]
        //[DtValue(EnumCascadeDeleteWay.CascadeDelete)]
        public EnumCascadeDeleteWay CascadeDeleteWay
        {
            get
            {
                return fCascadeDeleteWay;
            }
            set
            {
                fCascadeDeleteWay = value;
            }
        }

        public enum EnumCascadeDeleteWay
        {
            CascadeDelete,
            CheckHasRecordCannotDelete,
            ShowMessageAndCascadeDelete
        }
        

        #endregion

        #region Master DataSource
        string fRelationFields = null;
        /// <summary>
        /// 与主表关联的字段
        /// </summary>
        //[D("3.Relation")]
        //[D("主从表关联字段(如果ParentAutoOpen=true,忽略此字段设置) 设置方式为主关联字段,子关联字段 例如关联字段名一样sheetid,sheetid;empid,empid 也可简写成sheetid;empid")]
        public string RelationFields
        {
            get { return fRelationFields; }
            set { fRelationFields = value; }
        }

        string fMasterDataQuery = null;
        //[D("3.Relation")]
        //[D("主 DataQuery 名称")]
        public string MasterDataQuery
        {
            get { return fMasterDataQuery; }
            set { fMasterDataQuery = value; }
        }
        
        #endregion

        #region View
        private bool fShowOperateColor = true;
        /// <summary>
        /// 是否显示操作颜色
        /// </summary>
        //[D("4.View")]
        //[D("是否显示网格颜色")]
        //[DtValue(true)]
        public bool ShowOperateColor
        {
            get { return fShowOperateColor; }
            set { fShowOperateColor = value; }
        }

        EnumGridShowType fGridShowType = EnumGridShowType.showInNormalGrid;

        /// <summary>
        /// 显示网格或树
        /// </summary>
        //[D("4.View")]
        //[D("显示网格或树")]
        //[DtValue(EnumGridShowType.showInNormalGrid)]
        public EnumGridShowType GridShowType
        {
            get { return fGridShowType; }
            set
            {
                fGridShowType = value;
                //if (value == EnumGridShowType.showInBandGrid || value == EnumGridShowType.showInAdvBandGrid)
                //    this.GridMultiHead = true;
            }
        }

        public enum EnumGridShowType
        {
            showInNormalGrid,
            showInTreeList,
            showInBandGrid,
            showInAdvBandGrid,
            showInSizeBandGrid,
            None
        }

        private bool fAutoLayoutControl = true;
        //[D("4.View")]
        //[D("是否自动布局")]
        //[DtValue(true)]
        public bool AutoLayoutControl
        {
            get
            {
                return fAutoLayoutControl;
            }
            set
            {
                fAutoLayoutControl = value;
            }
        }

        //[D("4.View")]
        //[D("显示选择列")]
        //[DtValue(false)]
        public bool ShowCheckBox { get; set; }

        private string fFirstFocusControlFieldName = "";

        //[D("4.View")]
        //[D("第一次焦点控件字段名称")]
        //[DtValue("")]
        public string FirstFocusControlFieldName
        {
            get { return fFirstFocusControlFieldName; }
            set { fFirstFocusControlFieldName = value; }
        }

        //[D("5.View")]
        //[D("能进行拖动操作")]
        //[DtValue(false)]
        public bool CanDragDrop
        {
            get;
            set;
        }

         

        #endregion

        #region Tree Property
        private string fParentIDFieldName = "ParentID";
        //[D("5.Tree")]
        //[D("父字段名称")]
        //[DtValue("ParentID")]
        public string ParentIDFieldName
        {
            get { return fParentIDFieldName; }
            set
            {
                //解决DevExpress大小写问题{ 
                fParentIDFieldName = value;
            }
        }

        private string fIDFieldName = "ID";
        //[D("5.Tree")]
        //[D("ID段名称")]
        //[DtValue("ID")]
        public string IDFieldName
        {
            get { return fIDFieldName; }
            set
            {
                fIDFieldName = value;
            }
        }

        private bool fDynamicLoadTree = false;

        //[D("5.Tree")]
        //[D("动态加载")]
        //[DtValue(false)]
        public bool DynamicLoadTree
        {
            get { return fDynamicLoadTree; }
            set { fDynamicLoadTree = value; }
        }

        private bool fIsLevelSQL = false;
        //[D("5.Tree")]
        //[D("是否使用分级SQL;SelectSQL需要用分号隔开")]
        //[DtValue(false)]
        public bool IsLevelSQL
        {
            get { return fIsLevelSQL; }
            set { fIsLevelSQL = value; }
        }

        private int fNoChildNodeLevel = -1;

        //[D("5.Tree")]
        //[D("没有孩子节点级别SQL")]
        //[DtValue(-1)]
        public int NoChildNodeLevel
        {
            get { return fNoChildNodeLevel; }
            set { fNoChildNodeLevel = value; }
        }

        private DbType fTopNodeValueType = DbType.Int32;

        //[D("5.Tree")]
        //[D("顶级节点数据类型,用于动态树")]
        //[DtValue(DbType.Int32)]
        public DbType TopNodeValueType
        {
            get { return fTopNodeValueType; }
            set { fTopNodeValueType = value; }
        }

        object fTopNodeValue = null;
        /// <summary>
        /// 顶级节点值,一般默认为0
        /// </summary>
        //[D("5.Tree")]
        //[D("顶级节点值,一般默认为0")]
        //[DtValue(null)]
        public object TopNodeValue
        {
            get { return fTopNodeValue; }
            set { fTopNodeValue = value; }
        }

        private string fRootCaption;

        //[D("5.Tree")]
        //[D("根节点名称")]
        public string RootCaption
        {
            get { return fRootCaption; }
            set { fRootCaption = value; }
        }

        //[D("5.Tree")]
        //[D("是否使用树默认节点图标")]
        //[DtValue(false)]
        public bool UseTreeDefaultNodeImage
        {
            get;
            set;
        }
        #endregion
  

        #region Runtime data loading

        public RuntimeLoadSetting RuntimeLoadSetting { get; set; }
 
        #endregion

       
 
    }
}
