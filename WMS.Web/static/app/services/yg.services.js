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
        getData: {
            method: "GET",
            params: { id: '@id', name: '@name', exp: '@exp', action: 'getQueryData' },
            isArray: false,
            url: '/api/module/:action/?moduleid=:id&queryName=:name&condition=:exp'
        },
        getFieldDataSource: {
            method: "GET",
            params: { id: '@id', name: '@name', action: 'GetFieldDataSource' },
            isArray: true,
            url: '/api/module/:action/?moduleid=:id&queryName=:name&fieldName=:fieldName'
        },
        query: {
            method: "POST",
            url: '/api/module/PostQuery'
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
        this.$scope = scope.$new();
        this.view = null;
        this.datasource = null;
        this.options = {};
        this.scope = scope;
        this.visible = false;
        this.preConditions = null;
        var inited = false;

        this.$on = function (name, callback) {
            this.$scope.$on(name, callback);
        }

        this.$broadcast = function (name, args) {
            this.$scope.$broadcast(name, args);
        }

        /* private methods */
        function wrapEvent(query) {
            var options = query.options;
            var op = $.fn.datagrid.defaults;
            angular.forEach(op, function (value, name) {
                var item = op[name];
                if (name.indexOf('on') != 0)
                    return;
                options[name] = function () {
                    var args = []; // arguments;
                    angular.forEach(arguments, function (val, index) {
                        args.push(val);
                    });                     
                    args.dataquery = query;
                    scope.$broadcast(name, args);
                };
            });
        }

        function getEditor(col) {
            var editor = $.getRenderer(col.$field);
            col.editor = editor;
            if (!editor || !editor.type)
                return null;

            var defaults = $.fn[editor.type].defaults;

            if (defaults && defaults.hasOwnProperty('data')) {
                col.formatter = function (value, row, index) {
                    var val = null;
                    if (editor.options.data) {
                        angular.forEach(editor.options.data, function (item, index) {
                            if (item[editor.options.valueField] == value)
                                val = item[editor.options.textField];
                        });
                    }
                    return val;
                }
            }
            return editor;
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
                    if (dataCols[index].editor && dataCols[index].editor.options)
                        dataCols[index].editor.options.data = data;
                    dataCols[index].$field.datasource = data;
                });
            });
        }

        /* publish methods */
        this.init = function (callback) {
            if (inited) {
                if (callback)
                    callback();
                return;
            }
            wrapEvent(this);
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
            }, function (reason) {
                alert('Failed: ' + reason);
            }, function (update) {
                alert('Got notification: ' + update);
            });
        }

        this.load = function () {
            //$log.warn(this.Name);
            var self = this;
            this.init();
            var view = this.view;
            var options = this.options;
            if (view) {
                view.datagrid(options);
                view.datagrid('loading');
            }
            Module.getData({ id: this.ModuleID, name: this.Name }, function (data) {
                options.data = data;
                self.datasource = data.rows;
                view.datagrid('loaded');
                view.datagrid(options);
            }, function (error) {
                view.datagrid('loaded');
            });
        };

        this.open = function (condition) {
            var self = this;
            var param = null; //
            var action = null;
            var view = this.view;
            if (condition && typeof condition == 'object') {
                param = self.preConditions;
                if (!param) {
                    param = {
                        moduleId: this.ModuleID,
                        dataQueryName: this.Name,
                        parameters: []
                    };
                }
                param.isDynamic = false;
                for (var name in condition) {
                    var p = { feildName: name, value: condition[name], operation: null };
                    param.parameters.push(p);
                }
                action = Module.query(param).$promise;
            }
            else {
                var exp = self.AliasTableName + '.' + self.scope.BillNOFieldName + " = '" + condition + "'";
                param = { id: self.ModuleID, name: self.Name, exp: exp };
                action = Module.getData(param).$promise;
            }
            if (view)
                view.datagrid('loading');

            action.then(function (data) {
                self.options.data = data;
                self.datasource = data.rows;
                if (view) {
                    var opt = view.datagrid('options');
                    opt = $.extend(opt, { data: data });
                    view.datagrid(opt);
                    view.datagrid('loaded');
                }
                self.$broadcast('opened', {});

            }, function (error) {
                if (view)
                    view.datagrid('loaded');
            });
        };


        this.search = function (conditions) {
            var self = this;
            var view = self.view;
            var query = {
                moduleId: this.ModuleID,
                dataQueryName: this.Name,
                parameters: [],
                isDynamic: true
            };

            angular.forEach(conditions, function (value, index) {
                var p = { feildName: value.field, value: value.value, operation: null };
                query.parameters.push(p);
            });

            self.preConditions = query;
            self.open(null);
        }

        this.reOpen = function (dataRow) {
            var self = this;
            self.preConditions = null;
            self.open(dataRow);
        }

        var conditionsFields;
        this.getConditionFields = function () {
            if (conditionsFields)
                return conditionsFields;
            var ary = conditionsFields = [];
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

        var columns;
        this.getGridColumns = function () {
            if (columns)
                return columns;
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
                    sortable: true
                };

                if (!query.Editable || field.ReadOnly)
                    col.editor = null;
                else
                    col.editor = getEditor(col)// 'text';
                ary.push(col);
            });
            columns = [ary]
            return columns;
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
            if (rowindex >= 0 && this.view)
                this.view.datagrid('deleteRow', rowindex);
        }

        this.save = function () {
            var data = {};
            if (!this.view)
                return;
            data.modified = this.view.datagrid('getChanges', 'updated');
            data.added = this.view.datagrid('getChanges', 'inserted');
            data.deleted = this.view.datagrid('getChanges', 'deleted');
            if (data.modified.length == 0 && data.added.length == 0 && data.deleted.length == 0) {
                window.alert('没有需要保存的数据');
                return;
            }
            Module.saveData({ id: this.ModuleID, name: this.Name, data: data });
        }
    };

    function make(scope, query) {
        var obj = $.extend(query, new DataQuery(scope));
        return obj;
    }

    var create = function (scope, queries, current) {
        scope.queries = queries;
        angular.forEach(queries, function (query, index) {
            var item = make(scope, query);
            scope.queries[query.Name] = item;
        });

        if (scope.queries.length > 0) {
            if (scope.queries.length == 1)
                scope.currentDataQuery = scope.queries[0]
            else if (current && scope.queries.hasOwnProperty(current))
                scope.currentDataQuery = scope.queries[current];
            else
                scope.currentDataQuery = scope.queries.dqMaster;
        }
        return scope.queries;
    }

    return { create: create };
} ]);
