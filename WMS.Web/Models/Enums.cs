using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.Web.Models
{
    public enum DefaultBoolean
    {
        True = 0,
        False = 1,
        Default = 2,
    }

    public enum EnumDisplayType
    {
        None,
        ButtonEdit,
        CheckBox,
        ComboBox,
        Date,
        ImageListBox,
        LookupComboBox,
        PopupControl,
        Picture,
        RadioGroup,
        RichText,
        Memo,
        Spin,
        Text,
        Time,
        CheckedComboBoxEdit,
        MulComboBox,
        TreeComboBox,
        CheckedListBox
    }

    public enum EnumFieldType
    {
        //正常字段
        NormalField,

        //计算字段
        CalculateField,

        /// <summary>
        /// 交叉字段
        /// </summary>
        CrossField,
        /// <summary>
        /// 占比字段
        /// </summary>
        PercentageField,
        /// <summary>
        /// 累计求和字段
        /// </summary>
        AccumulativeSumField,

        PicturePathField

    }

    // 摘要:
    //     指定 System.Windows.Forms.TextBox 控件中字符的大小写。
    public enum CharacterCasing
    {
        // 摘要:
        //     字符大小写保持不变。
        Normal = 0,
        //
        // 摘要:
        //     将所有字符都转换为大写。
        Upper = 1,
        //
        // 摘要:
        //     将所有字符都转换为小写。
        Lower = 2,
    }

    public enum FormatType
    {
        None = 0,
        Numeric = 1,
        DateTime = 2,
        Custom = 3,
    }

    [Serializable]
    public enum MaskType
    {
        None = 0,
        DateTime = 1,
        DateTimeAdvancingCaret = 2,
        Numeric = 3,
        RegEx = 4,
        Regular = 5,
        Simple = 6,
        Custom = 7,
    }

    [Serializable]
    public enum EnumDisplayValueType
    {
        dvtConstString,
        dvtSQL
        //dvtMethod
    }

    [Serializable]
    public enum ClauseType
    {
        Equals = 0,
        DoesNotEqual = 1,
        Greater = 2,
        GreaterOrEqual = 3,
        Less = 4,
        LessOrEqual = 5,
        Between = 6,
        NotBetween = 7,
        Contains = 8,
        DoesNotContain = 9,
        BeginsWith = 10,
        EndsWith = 11,
        Like = 12,
        NotLike = 13,
        IsNull = 14,
        IsNotNull = 15,
        AnyOf = 16,
        NoneOf = 17,
        IsNullOrEmpty = 18,
        IsNotNullOrEmpty = 19,
        IsBeyondThisYear = 20,
        IsLaterThisYear = 21,
        IsLaterThisMonth = 22,
        IsNextWeek = 23,
        IsLaterThisWeek = 24,
        IsTomorrow = 25,
        IsToday = 26,
        IsYesterday = 27,
        IsEarlierThisWeek = 28,
        IsLastWeek = 29,
        IsEarlierThisMonth = 30,
        IsEarlierThisYear = 31,
        IsPriorThisYear = 32,
    }

    public enum EnumQueryConditionDataType
    {
        cdtString,
        cdtNumber,
        cdtDate,
        cdtDateTime,
        cdtTimestamp,
        cdtTime,
        cdtStoredString,
        cdtCheckBox,
        cdtPicture
    }
}
