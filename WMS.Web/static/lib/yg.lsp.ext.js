angular.$wrapEventFunction = function (scope,element,name,fun) {
    var value = name;
    var arglist = value.substring(value.indexOf('(') + 1, value.indexOf(')')).split(',');
    for (var i = 0; i < arglist.length; i++) {
        arglist[i] = $.trim(arglist[i]);
    }

    return function (event,args) {
        //var args = arguments;
        var locals = {element:element};        
        for (var i = 0; i < args.length; i++) {
            locals[arglist[i+1]] = args[i];
        }
        scope.$apply(function () {
            fun(scope, locals);
        });
    }

    //return arglist;
}


//text,textarea,checkbox,numberbox,validatebox,datebox,combobox,combotree
$.extend({
    getRenderer: function (field) {
        //        if (field.ReadOnly)
        //            return null;
        var map = {
            ButtonEdit: '',
            CheckBox: 'checkbox',
            ComboBox: 'combobox',
            Date: 'datebox',
            ImageListBox: 'imageList',
            LookupComboBox: 'combobox',
            PopupControl: 'popupControl',
            Picture: 'image',
            RadioGroup: 'radioGroup',
            RichText: 'ritchtext',
            Memo: 'memo',
            Spin: 'spin',
            Text: 'text',
            Time: 'time',
            CheckedComboBoxEdit: '',
            MulComboBox: '',
            TreeComboBox: '',
            CheckedListBox: ''
        };

        var renderer = {};
        renderer.type = map[field.DisplayType];

        if (field && field.DispValueType == 'dvtSQL' && field.DisplayValue && field.DisplayValue.length > 0) {
            var name = field.DisplayMember.toUpperCase();
            var value = field.ValueMember.toUpperCase();
            renderer.options = {
                valueField: value,
                textField: name,
                method: 'GET',
                url: '/api/module/GetFieldDataSource/?' +
                            'moduleid=' + field.moduleId +
                            '&queryName=' + field.queryName +
                            '&fieldName=' + field.FieldName
            };
        }

        return renderer;
        //        {
        //                            type:'combobox',
        //                            options:{
        //                                valueField:'productid',
        //                                textField:'productname',
        //                                url:'products.json',
        //                                required:true
        //                            }
        //                        }
        return;
    }
});

