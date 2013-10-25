uimodule.directive('ygDetailPanel', ['$parse', '$compile', function ($parse, $compile) {
    return {
        restrict: 'A',
        transclude: false,
        controller: function ($scope, $element) {
            var self = this;
        },
        link: function (scope, element, attrs, ctrl) {
            var panel = $('<ul></ul>');
            $(element).append(panel);
            var childScope = scope.$new();
            var dataquery = null;
            childScope.item = null;
            scope.$watch(attrs.dataquery, function (newl, oldl) {
                if (newl == dataquery)
                    return;
                dataquery = newl;
                render(dataquery.Fields);
            });

            scope.$watch(attrs.dataquery + '.focusRow', function (newl, oldl) {
                if (newl == childScope.item)
                    return;
                childScope.item = newl;
            });

            var render = function (fields) {
                if (fields == null)
                    fields = [];
                var template = '';
                $.each(fields, function (index, field) {
                    if (!field.VisibleInDetail)
                        return;
                    childScope[field.FieldName] = field;
                    var renderer = $.getRenderer(field);
                    template = template + "<li>"
                            + "<label class='label'>" + field.Caption + "</label>"
                            + "<input type='text' " + renderer.type
                            + " field='{{" + field.FieldName + "}}' ng-model='item." + field.FieldName + "'>"
                            + "</li>"

                });

                var node = $compile(template)(childScope);
                $(panel).append(node);
            }

        }
    };
} ]);