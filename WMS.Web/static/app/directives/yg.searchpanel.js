uimodule.directive('searchPanel', ['$parse', '$compile', function ($parse, $compile) {

    return {
        restrict: 'AC',
        require: ['searchPanel', '?ngModel'],
        controller: function ($scope, $element) {

        },
        link: function (scope, element, attrs, ctrls) {
            var template = '';
            var conditionItems;
            var selectCtrl = ctrls[0];
            var ngModelCtrl = ctrls[1];
            //yg-search
            var searchfn = $parse(attrs.ygSearch);

            scope.$watch(function () {
                if (conditionItems != ngModelCtrl.$viewValue) {
                    conditionItems = ngModelCtrl.$viewValue;
                    ngModelCtrl.$render(conditionItems);
                }
            });

            var panel = $('<ul></ul>');
            $(element).append(panel);
            var button = $('<button>Search</button>');
            $(element).append(button);

            button.on('click', function (event) {
                scope.$apply(function () {
                    searchfn(scope, { $event: event });
                });
            });

            ngModelCtrl.$render = function (items) {
                //items = items | {};
                if (items == null)
                    items = [];
                $.each(items, function (index, item) {
                    var childScope = scope.$new();
                    childScope['item'] = item;
                    var template = template + "<li>"
                            + "<span class='label'>{{ item.label }}</span>"
                            + "<input type='text' " + item.editor
                            + " text-field='itemnamedetail'  value-field='itemvalue' ng-model='item.value'>"
                            + "</li>"
                    var node = $compile(template)(childScope);
                    $(panel).append(node);
                });
            }
        }
    }
} ]);