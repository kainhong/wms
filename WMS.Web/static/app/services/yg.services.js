var services = angular.module("yg.services", ['ngResource']);

services.factory('System', function ($resource, $http) {
    var service = $resource('/api/system', {

    }, {
        list: { method: "GET", params: {}, isArray: true }
    });
    return service;
});

services.factory('Menu', function ($resource, $http) {
    var service = $resource('/api/menu/:id', { id: "@id" }, {
        query: { method: "GET", params: { id: '@SystemId' }, isArray: true }
    });
    return service;
});

services.factory('Module', function ($resource, $http) {
    var service = $resource('/api/module/:action/?id=:id', { id: "@id", action: '@action' }, {
        getModuleQuery: { method: "GET", params: { id: '@id', action: 'getmodulequery' }, isArray: true },
        getQueryData: {
            method: "GET",
            params: { id: '@id', name: '@name', action: 'getQueryData' },
            isArray: false,
            url: '/api/module/:action/?moduleid=:id&queryName=:name'
        },
        getFieldDataSource: {
            method: "GET",
            params: { id: '@id', name: '@name', action: 'GetFieldDataSource' },
            isArray: true,
            url: '/api/module/:action/?moduleid=:id&queryName=:name&fieldName=:fieldName'
        },

        searchQueryData: {
            method: "POST",
            url: '/api/module/PostQuerySearch'
        },
        saveData: {
            method: "POST",
            params: { id: '@id', name: '@name' },
            url: '/api/module/PostSaveData/?moduleid=:id&queryName=:name'
        }
    });
    return service;
});

services.factory('DataQueryFactory', ['Module', '$log', '$q', function (Module, $log, $q) {
    function DataQuery(scope) {
        this.view = null;
        this.data = null;
        this.options = {};
        this.scope = scope;
        var inited = false;

        /* private methods */
        function wrapEvent(options) {
            var op = $.fn.datagrid.defaults;
            angular.forEach(op, function (value, name) {
                var item = op[name];
                if (name.indexOf('on') != 0)
                    return;

                options[name] = function () {
                    var args = arguments;
                    scope.$broadcast(name, args);
                };
                //var name = item.toString();
                //options[name] = item;
            });
        }
        function initialFieldDataSource(cols) {
            var dataCols = [];
            var queries = [];
            angular.forEach(cols, function (item, index) {
                var field = item.$field;
                if (!(field.DispValueType == 'dvtSQL' && field.DisplayValue && field.DisplayValue.length > 0))
                    return;
                var q = Module.getFieldDataSource(
                        {
                            id: field.moduleId,
                            name: field.queryName,
                            fieldName: field.FieldName
                        }
                ).$promise;

                queries.push(q);
                dataCols.push(item);
            });

            return $q.all(queries).then(function (datas) {
                angular.forEach(datas, function (data, index) {
                    dataCols[index].editor.data = data;
                });
            });
        }

        /* publish methods */
        this.init = function (callback) {
            if (inited)
                return;

            wrapEvent(this.options);
            var query = this;
            angular.forEach(this.Fields, function (field, index) {
                field.moduleId = query.ModuleID;
                field.queryName = query.Name;
            });
            var cols = this.getGridColumns();

            this.options = $.extend(this.options, { columns: cols });
            this.options.data = null;

            var then = initialFieldDataSource(cols[0]);

            then.then(function (data) {
                inited = true;
                if (callback)
                    callback();
            });

        }

        this.open = function () {
            //$log.warn(this.Name);
            this.init();
            var view = this.view;
            var options = this.options;
            view.datagrid(options);
            view.datagrid('loading');
            Module.getQueryData({ id: this.ModuleID, name: this.Name }, function (data) {
                options.data = data;
                view.datagrid('loaded');
                view.datagrid(options);
            }, function (error) {
                view.datagrid('loaded');
            });
        };

        this.search = function (conditions) {
            var view = this.view;
            var query = {
                moduleId: this.ModuleID,
                dataQueryName: this.Name,
                parameters: []
            };

            angular.forEach(conditions, function (value, index) {
                var p = { feildName: value.field, value: value.value, operation: null };
                query.parameters.push(p);
            });

            view.datagrid('loading');
            Module.searchQueryData(query, function (data) {
                var opt = view.datagrid('options');
                opt = $.extend(opt, { data: data });
                view.datagrid(opt);
                view.datagrid('loaded');
            }, function (error) {

                view.datagrid('loaded');
            });

        }

        this.getConditionFields = function () {
            var ary = [];
            angular.forEach(this.Fields, function (field, index) {
                if (field.IsSearchCondition)
                    ary.push({
                        value: null,
                        editor: field.DisplayType,
                        label: field.Caption,
                        field: field.FieldName,
                        operation: null
                    });
            });
            return ary;
        }

        this.getGridColumns = function () {
            var ary = [];
            var query = this;
            angular.forEach(this.Fields, function (field, index) {
                if (!field.VisibleInGrid)
                    return;
                var col = {
                    $field: field,
                    field: field.FieldName,
                    title: field.Caption,
                    width: field.DisplayWidth,
                    sortable: true,
                    //editor: (query.ReadOnly || field.ReadOnly) ? null : $.getRenderer(field)// 'text'
                    editor: $.getRenderer(field)// 'text'
                };
                if (col.editor.type == 'combobox') {
                    col.formatter = function (value, row, index) {
                        var val = null;
                        if (col.editor.data) {
                            angular.forEach(col.editor.data, function (item, index) {
                                if (item[field.ValueMember.toUpperCase()] == value)
                                    val = item[field.DisplayMember.toUpperCase()];
                            });
                            return val;
                        } else {
                            return value;
                        }
                    }
                }
                ary.push(col);
            });

            return [ary];
        };

        this.add = function () {
            var obj = {};
            angular.forEach(this.Fields, function (index, field) {
                var val = field.DefaultValue;
                obj[field.FieldName] = val;
            });

            if (this.view)
                this.view.datagrid('appendRow', obj);
            return obj;
        }

        this.remove = function () {
            var rowindex = this.focusRowIndex;
            if (rowindex >= 0)
                this.view.datagrid('deleteRow', rowindex);
        }

        this.save = function () {
            var data = {};
            // inserted,deleted,updated
            data.modified = this.view.datagrid('getChanges', 'updated');
            data.added = this.view.datagrid('getChanges', 'inserted');
            data.deleted = this.view.datagrid('getChanges', 'deleted');
            Module.saveData({ id: this.ModuleID, name: this.Name, data: data });
        }
    };

    var create = function (scope, dataQuery) {
        var obj = $.extend(dataQuery, new DataQuery(scope));
        return obj;
    }

    return { create: create };
} ]);
