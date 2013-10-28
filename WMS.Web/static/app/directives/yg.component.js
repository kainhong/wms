var uimodule = angular.module('ui.component', ["ui.envirment", "yg.services"], ['$provide', function ($provide) {

    // Iterate over the kendo.ui and kendo.dataviz.ui namespace objects to get the Kendo UI widgets adding
    // them to the 'widgets' array.
    var editors = ['validatebox','combo','combobox'
        ,'combotree','combogrid','numberbox'
        ,'datebox','datetimebox','calendar'
        ,'spinner','numberspinner','timespinner'
        ,'slider'];
    $provide.value('editors', editors);

}]);

uimodule.directive('ygMenu', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        transclude: true,
        controller: function ($scope, $element, envirmentSharedService, System, Menu) {
            var self = this;
            System.list({}, function (data) {
                makeTabs($element, data);
            });

            function makeTabs(element, nodes) {
                $.each(nodes, function (i, m) {
                    var id = 'panel' + i;
                    $(element).accordion('add', {
                        id: m.SystemId,
                        title: m.SystemName,
                        selected: false
                    });
                    makeSubMenu(m);
                });
            }

            function makeSubMenu(item) {
                var p = $('<div></div>').appendTo("#" + item.SystemId);
                var menu = Menu.query({ id: item.SystemId }, function (data) {
                    p.tree(
                    {
                        data:data,
                        checkbox: false,
                        onClick: function (node) {
                            $scope.$apply(function () {
                                self.nodeClick($scope, { node: node });
                            });
                            envirmentSharedService.broadcast("global.tree.node.selected", node);
                        }
                    });
                });
            }
        },
        link: function (scope, element, attrs, ctrl) {
            $(element).accordion({
                animate: false,
                fit: true
            });
            if (attrs.onNodeClick)
                ctrl.nodeClick = $parse(attrs.onNodeClick);
            $(element).addClass("easyui-accordion");
        },
        replace: false
    };
} ]);

uimodule.directive('ygTabs', function () {
    return {
        restrict: 'AC',
        controller: function ($scope, $element, envirmentSharedService) {
            var panes = $scope.panes = [];
            envirmentSharedService.on('global.tree.node.selected',
                function (event, msg) {
                    var node = msg;
                    if (!node)
                        return;

                    if (node.attributes.moduleId == undefined || node.attributes.moduleId == null)
                        return;
                    if (!$element.tabs('exists', node.text)) {
                        addTab(node);
                    } else {
                        selectTab(node);
                    }
                }
            );

            function selectTab(node) {
                $element.tabs('select', node.text);
            }

            function addTab(node) {
                var id = node.attributes.moduleId + '';
                var url = '/static/module/' + id.substring(0, 4) + '/' + id + '/index.htm';
                var t_p = $element.tabs('add', {
                    id: 'tags-' + node.id,
                    title: node.text,
                    content: '<iframe src="' + url + '" frameborder="0" border="0" marginwidth="0" marginheight="0" scrolling="yes" width="100%" height="100%"></iframe>',
                    closable: true
                });

            }
        },

        link: function (scope, element, attrs, tabsCtrl) {
            element.tabs({ border: false, fit: true });
        },

        replace: false
    };
})

uimodule.directive('toolBar', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        controller: function ($scope, $element) {
            var self = this;
            toolbar = $scope.toolbar = $.extend({}, $scope.toolbar);
            toolbar.selectedItem = null;
            if (!toolbar.barItems)
                toolbar.barItems = [];

            self.addBarItem = function (item) {
                if (!toolbar.selectedItem)
                    toolbar.selectedItem = item;
                toolbar.barItems.push(item);
            };

            self.onBarItemClick = function (item) {
                toolbar.selectedItem = item;
                $scope.$broadcast("toolbar.item.selected", item);
                if (self.onItemClickFn) {
                    $scope.$apply(
                        function () {
                            self.onItemClickFn($scope, { item: item });
                        }
                    );
                }
            }
        },
        link: function (scope, element, attrs, ctrl) {
            if (attrs.onItemClick) {
                ctrl.onItemClickFn = $parse(attrs.onItemClick);
            }
        },
        template: "<div><div class='transcluded' ng-transclude></div></div>",
        replace: true,
        transclude: true
    };
} ])
.directive('barItem', function () {
    return {
        require: "^toolBar",
        link: function (scope, element, attrs, tooBarCtrl) {
            var obj = {};
            obj.text = $(element).text();
            obj.key = attrs.key;
            $(element).linkbutton({
                iconCls: attrs.iconCls
            });
            var disable = false;
            scope.$watch(attrs.disable, function (newvalue, oldvalue) {
                disable = newvalue;
                if (newvalue)
                    $(element).linkbutton('disable');
                else
                    $(element).linkbutton('enable');
            });

            $(element).bind('click', function () {
                if(!disable)
                    tooBarCtrl.onBarItemClick(obj);
                return false;
            });
        }
    };
})



