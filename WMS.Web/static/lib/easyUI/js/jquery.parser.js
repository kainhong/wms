/**
* parser - jQuery EasyUI
* 
* Licensed under the GPL terms
* To use it on other terms please contact us
*
* Copyright(c) 2009-2012 stworthy [ stworthy@gmail.com ] 
* 
*/

(function ($) {
    $.parser = {
        auto: true,
        onComplete: function (context) { },
        plugins: ['linkbutton', 'menu', 'menubutton', 'splitbutton', 'progressbar',
				 'tree', 'combobox', 'combotree', 'numberbox', 'validatebox', 'searchbox',
				 'numberspinner', 'timespinner', 'calendar', 'datebox', 'datetimebox', 'slider',
				 'layout', 'panel', 'datagrid', 'propertygrid', 'treegrid', 'tabs', 'accordion', 'window', 'dialog'
		],
        parse: function (context) {
            var aa = [];
            for (var i = 0; i < $.parser.plugins.length; i++) {
                var name = $.parser.plugins[i];
                var r = $('.easyui-' + name, context);
                if (r.length) {
                    if (r[name]) {
                        r[name]();
                    } else {
                        aa.push({ name: name, jq: r });
                    }
                }
            }
            if (aa.length && window.easyloader) {
                var names = [];
                for (var i = 0; i < aa.length; i++) {
                    names.push(aa[i].name);
                }
                easyloader.load(names, function () {
                    for (var i = 0; i < aa.length; i++) {
                        var name = aa[i].name;
                        var jq = aa[i].jq;
                        jq[name]();
                    }
                    $.parser.onComplete.call($.parser, context);
                });
            } else {
                $.parser.onComplete.call($.parser, context);
            }
        },
        parseOptions: function (_6, _7) {
            var t = $(_6);
            var _8 = {};
            var s = $.trim(t.attr("data-options"));
            if (s) {
                if (s.substring(0, 1) != "{") {
                    s = "{" + s + "}";
                }
                _8 = (new Function("return " + s))();
            }
            if (_7) {
                var _9 = {};
                for (var i = 0; i < _7.length; i++) {
                    var pp = _7[i];
                    if (typeof pp == "string") {
                        if (pp == "width" || pp == "height" || pp == "left" || pp == "top") {
                            _9[pp] = parseInt(_6.style[pp]) || undefined;
                        } else {
                            _9[pp] = t.attr(pp);
                        }
                    } else {
                        for (var _a in pp) {
                            var _b = pp[_a];
                            if (_b == "boolean") {
                                _9[_a] = t.attr(_a) ? (t.attr(_a) == "true") : undefined;
                            } else {
                                if (_b == "number") {
                                    _9[_a] = t.attr(_a) == "0" ? 0 : parseFloat(t.attr(_a)) || undefined;
                                }
                            }
                        }
                    }
                }
                $.extend(_8, _9);
            }
            return _8;
        }
    };
    $(function () {
        if (!window.easyloader && $.parser.auto) {
            $.parser.parse();
        }
    });
})(jQuery);