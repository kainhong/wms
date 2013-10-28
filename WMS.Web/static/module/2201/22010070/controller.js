var app = angular.module('app', ['ui.component', 'yg.services', 'ui.envirment']);
app.provider({
    $exceptionHandler: function () {
        var handler = function (exception, cause) {
           
        };

        this.$get = function () {
            return handler;
        };
    }
});

app.controller('ctrl',
['$scope', 'Module', 'DataQueryFactory', function ($scope, Module, DataQueryFactory) {
    $scope.queries = {};
    $scope.conditions = [];
     
    $scope.onItemClick = function (item) {
        //alert(item.key);
        var dataQuery = $scope.currentDataQuery;
        if (!dataQuery)
            return;
        var gridview = dataQuery.view;
        if (gridview == null)
            return;
        if (item.key == 'add') {
            var newRow = dataQuery.add();
        }
        else if (item.key == 'remove') {
            dataQuery.remove();
        }
        else if (item.key == 'save') {
            dataQuery.save();
        }
    }

    $scope.search = function () {
        $scope.currentDataQuery.search($scope.conditions);
    }

    $scope.onClickRow = function (element, rowIndex, rowData) {
        //$scope.queries.dqDetail.currentitem = rowData;
    }

    $scope.onClickCell = function (element, rowIndex, field, value) {

    }

    $scope.onDblClickCell = function (element) {
        console.log('onDblClickCell');
    }

    var query = Module.getModuleQuery({ id: 22010070 }).$promise;
    var then = query.then(function (data) {
        $scope.queries = data;
        angular.forEach(data, function (query, index) {
            var item = DataQueryFactory.create($scope, query);
            $scope.queries[query.Name] = item;
            //item.init();
        });

        if ($scope.queries.length > 0) {
            $scope.queries[0].init(function () {
                $scope.currentDataQuery = $scope.queries[0]; //$scope.currentDataQuery.open();
                //$scope.currentDataQuery.ReadOnly = true;
                $scope.conditions = $scope.currentDataQuery.getConditionFields();
              
            });
        }
    });

} ]);