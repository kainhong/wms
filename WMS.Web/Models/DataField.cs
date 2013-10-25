#region Copyright(C) 2010
/*
 * Copyright XD  Information System 2007
 * 
 * Authors:		xiangdong
 * Created:		2007.09.10
 * Description:
 *     �����ֶ���
 * 
 * * 2008.03.01 xiangdong ���¹滮���� �汾����Ϊ 2.0
 * * 2010.11.01 xiangdong �ع�����
 * * 2010.11.30 xiangdong ������������С���ϸ�����У��Լ�������ʾ�������
 * + 2011.01.17 xiangdong �����ֶ�Ȩ�޿���
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
    /// �����ֶ���
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
        /// �Ƿ�ֻ�� 
        /// <summary>
        //[D("�Ƿ�ֻ��")]
        //[DtValue(false)]
        //[D("2.Data")]
        public bool ReadOnly
        {
            get;
            set;
        }

        //[D("�Ƿ�����,�����ɶ���ֶ����")]
        //[DtValue(false)]
        //[D("2.Data")]
        public bool IsPrimaryKey
        {
            get;
            set;
        }

        private bool fIsUniqueField = false;

        //[D("�Ƿ�ΨһԼ��")]
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
        /// �Ƿ��Զ������ֶ�
        /// </summary> 
        //[D("�Զ������ֶ�����, aitLocal ���������ֶ�, aitServer�����������ֶ�")]
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
        /// �����������ֶα��
        /// </summary>
        //[D("�Զ������ֶα��, Ϊ 0 ��Ϊģ���� ")]
        //[DtValue("0")]
        //[D("2.Data")]
        public string ServerAutoIncNO
        {
            get;
            set;
        }

        private bool fIsNewRowInherit = false;
        /// <summary>
        /// �¼�¼�Ƿ�̳�
        /// </summary>
        //[D("�¼�¼�Ƿ�̳�")]
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
        //[D("�Ƿ���������ֶ�")]
        //[DtValue(false)]
        //[D("2.Data")]
        public bool IsMasterTableUpdateField
        {
            get { return fIsMasterTableUpdateField; }
            set { fIsMasterTableUpdateField = value; }
        }

        private bool fOnlyInsertEdit = false;
        //[D("ֻ��������״̬���Խ��б༭,�����ܽ����޸�.")]
        //[DtValue(false)]
        //[D("2.Data")]
        public bool OnlyInsertEdit
        {
            get { return fOnlyInsertEdit; }
            set { fOnlyInsertEdit = value; }
        }

        private bool fAutoBindDetailControl = true;
        //[D("�Ƿ��Զ�����ϸ�ؼ�")]
        //[DtValue(true)]
        //[D("2.Data")]
        public bool AutoBindDetailControl
        {
            get { return fAutoBindDetailControl; }
            set { fAutoBindDetailControl = value; }
        }

        private bool fDetailHasRecordCannotModify = false;
        //[D("��ϸ���м�¼ʱ�����ֶ��Ƿ�����޸�")]
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
        /// ��������Ƿ�ɼ�
        /// </summary>
        //[D("��������Ƿ�ɼ�")]
        //[DtValue(false)]
        //[D("3.View")]
        public bool VisibleInDetail
        {
            get { return fVisibleInDetail; }
            set { fVisibleInDetail = value; }
        }


        private bool fVisibleInGrid = true;   //�Ƿ�ɼ�
        /// <summary>
        /// ���������Ƿ�ɼ�
        /// </summary>
        //[D("���������Ƿ�ɼ�")]
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
        //[D("TreeList���ɼ�����Ҳ����,���ڱ�������")]
        //[DtValue(true)]
        //[D("3.View")]
        public bool TreeColumnNoVisibleCreate
        {
            get { return fTreeColumnNoVisibleCreate; }
            set { fTreeColumnNoVisibleCreate = value; }
        }
        
        /// <summary>
        /// �û����ֶ�Ȩ��?
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool? UserHasFieldRight
        {
            get;
            set;
        }

        //[D("������ͼ����")]
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
        /// ��������SQL
        /// </summary>
        //[D("��������Select��䣬����: select Name from XXTable where ID = @ID")]
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
        //[D("���������¼Ϊ��ʱ���Ƿ���ʾ��ʾ��Ϣ")]
        //[DtValue(true)]
        //[D("7.Quick Input")]
        public bool QuickInputShowError
        {
            get { return fQuickInputShowError; }
            set { fQuickInputShowError = value; }
        }

        private string fQuickInputEmptyError = "[{0}]����û���ҵ�!";
        //[D("���������¼Ϊ��ʱ�򣬴�����ʾ��Ϣ")]
        //[D("7.Quick Input")]
        //[DtValue("[{0}]����û���ҵ�!")]
        public string QuickInputEmptyError
        {
            get { return fQuickInputEmptyError; }
            set { fQuickInputEmptyError = value; }
        }

        private string[] fQuickInputUpdateField = null;
        //[D("������������ֶΣ����ֶ��ö��Ÿ��������Ϊ�գ���ʾ QuickInputSQL ��Ӧ�ֶ�ȫ������")]
        //[D("7.Quick Input")]
        public string[] QuickInputUpdateField
        {
            get { return fQuickInputUpdateField; }
            set { fQuickInputUpdateField = value; }
        }

        private bool fCheckExistRecord = false;
        //[D("����¼�Ƿ����!������ʵʱ���,�� IsPrimaryKey �� UniqueField ʵ�������������Ƿ��������������Ƚ�.")]
        //[DtValue(false)]
        //[D("7.Quick Input")]
        public bool CheckExistRecord
        {
            get { return fCheckExistRecord; }
            set { fCheckExistRecord = value; }
        }

        private string fCheckExistTableName = "";
        //[D("����¼�ı�,Ϊ�յ�ʱ��ʹ��DataQuery.TableName")]
        //[DtValue("")]
        //[D("7.Quick Input")]
        public string CheckExistTableName
        {
            get { return fCheckExistTableName; }
            set { fCheckExistTableName = value; }
        }

        private ExistShowErrorType fCheckExistShowErrorType = ExistShowErrorType.NotExistShow;
        //[D("����¼ʱ,�Ǵ��ڵ�ʱ����ʾ����,���ǲ����ڵ���ʾ����")]
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
        //[D("���������ֶ�")]
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
        /// ��ǰ�е�ֵ�����ֻ��һ������ʱ��Ĭ��ʹ�ø�ֵ
        /// </summary>
        public object Value { get;  set; }

        /// <summary>
        /// �Զ���ű�
        /// </summary>
        public string Script { get; set; }

        /// <summary>
        /// �������Ҫ�Զ�����ű�����Ҫ�ֶ��޸�ΪTrue
        /// </summary>
        public bool Handled { get; set; }
    }
}
