if (!Object.create) {
    Object.create = (function () {
        function F() { }

        return function (o) {
            if (arguments.length != 1) {
                throw new Error('Object.create implementation only accepts one parameter.');
            }
            F.prototype = o;
            return new F()
        }
    })()
}

angular.$wrapEventFunction = function (scope, element, name, fun) {
    var value = name;
    var arglist = value.substring(value.indexOf('(') + 1, value.indexOf(')')).split(',');
    for (var i = 0; i < arglist.length; i++) {
        arglist[i] = $.trim(arglist[i]);
    }

    return function (event, args) {
        //var args = arguments;
        var locals = { element: element };
        for (var i = 0; i < args.length; i++) {
            locals[arglist[i + 1]] = args[i];
        }
        scope.$apply(function () {
            fun(scope, locals);
        });
    }

    //return arglist;
}

angular.$filterAry = function (ary, exp) {
    if (!exp)
        return ary;
    var str = exp.replace(/(\w+)\s/g, function ($1) {
        if ($1 == 'and ')
            return '&& ';
        else if ($1 == 'or ')
            return '|| ';
        else
            return 'a.' + $1;
    });

    var content = "(function(a){ return " + str + ";})"
    var fun = eval(content);
    var items = [];
    angular.forEach(ary, function (item, index) {
        if (fun(item))
            items.push(item);
    });
    return items;
}

//text,textarea,checkbox,numberbox,validatebox,datebox,combobox,combotree
$.extend({
    getRenderer: function (field) {
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
                textField: name
            };
        }

        return renderer;

        return;
    }

});

