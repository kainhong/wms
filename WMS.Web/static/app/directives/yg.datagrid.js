﻿uimodule.directive("gridview", ['$parse', 'Module', function ($parse, Module) {


    return {
        restrict: 'A',
        require: ['gridview', '?ngModel'],
        controller: function ($scope, $element) {
            var self = this;
            var editIndex = undefined;
            this.onClickRow = function (index) {
                if (!self.dataquery.Editable)
                    return;
                var grid = self.dataquery.view;
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
                var grid = self.dataquery.view;
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
                var menu = $('div', $(element));
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
                        dataquery.view.contextMenu = menu;
                        dataquery.init(function () {
                            dataquery.options.onRowContextMenu = function (e, rowIndex, rowData) {
                                e.preventDefault();
                                menu.menu('show', {
                                    left:e.pageX,
                                    top:e.pageY
                                });   
                            };

                            table.datagrid(dataquery.options);
                            if (dataquery.AutoOpen)
                                dataquery.load();
                        });

                        dataquery.$on('onClickRow', function (event, args) {                           
                            selectCtrl.onClickRow(args[0]);
                        });
                    });
                }                

                angular.forEach(attrs, function (value, action) {
                    if (typeof action != "string")
                        return;
                    if (action.indexOf('on') != 0)
                        return;
                    var fn = $parse(value);

                    var callback = angular.$wrapEventFunction(scope, dataquery, value, fn);

                    scope.$on(action, callback);
                });

                

            },
            post: function (scope, element, attrs, ctrl) {

            }
        },
        template: '<div context-menu></div> '
    };
}]);