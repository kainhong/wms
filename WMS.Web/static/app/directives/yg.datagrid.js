uimodule.directive("gridview", ['$parse', 'Module', function ($parse, Module) {


    return {
        restrict: 'A',
        require: ['gridview', '?ngModel'],
        controller: function ($scope, $element) {
            var self = this;
            var grid = $('table', $element);
            var editIndex = undefined;
            this.onClickRow = function (index) {

                if (!self.dataquery.Editable)
                    return;
                if (editIndex != index) {
                    if (endEditing()) {
                        grid.datagrid('selectRow', index)
                            .datagrid('beginEdit', index);
                        editIndex = index;
                    } else {
                        grid.datagrid('selectRow', editIndex);
                    }
                }
            }

            function endEditing() {
                if (editIndex == undefined) { return true }
                if (grid.datagrid('validateRow', editIndex)) {
                    grid.datagrid('endEdit', editIndex);
                    $scope.$digest();
                    editIndex = undefined;
                    return true;
                } else {
                    return false;
                }
            }
        },
        link:
            {
                pre: function (scope, element, attrs, ctrls) {
                    var selectCtrl = ctrls[0];
                    var ngModelCtrl = ctrls[1];
                    var table = $('<table></table>');
                    $(element).append(table);
                    var options = {};
                    var dataquery = null;
                    var gridOptions = null;

                    if (attrs.dataquery) {
                        scope.$watch(attrs.dataquery, function (newl, oldl) {
                            if (newl == dataquery)
                                return;
                            selectCtrl.dataquery = dataquery = newl;
                            if (!newl)
                                return;
                            dataquery.view = table;
                            dataquery.init(function () {
                                table.datagrid(dataquery.options);
                                if (dataquery.AutoOpen)
                                    dataquery.load();
                            });
                        });
                    }

                    //var childScope = scope.$new();

                    angular.forEach(attrs, function (value, action) {
                        if (typeof action != "string")
                            return;
                        if (action.indexOf('on') != 0)
                            return;
                        var fn = $parse(value);

                        var callback = angular.$wrapEventFunction(scope, dataquery, value, fn);

                        scope.$on(action, callback);
                    });

                    scope.$on('onClickRow', function (event, args) {
                        dataquery.focusRowIndex = args[0];
                        dataquery.focusRow = args[1];
                        scope.$digest();
                        scope.currentDataQuery = dataquery;
                        selectCtrl.onClickRow(args[0]);
                    });

                },
                post: function (scope, element, attrs, ctrl) {

                }
            }
    };
} ]);