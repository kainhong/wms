//var app = angular.module('app', []);
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

(function (core, coreFactory) {
    var pattern = /\.[^.]*?[Controller|Ctrl]$/i;
    var constructors = {};
    function factory(name, controllerFactory) {
        if (!pattern.test(name)) {
            return (coreFactory.apply(core, arguments));
        }

        core.controller(name,
			function ($scope, $injector) {
				var cacheKey = ("cache_" + name);
				var Constructor = constructors[cacheKey];

				if (!Constructor) {
					Constructor= constructors[cacheKey] = $injector.invoke(controllerFactory)
				}

				return $injector.instantiate(Constructor,{"$scope": $scope});
			}
		);
        return (core);
    };
    core.factory = factory;

})(app, app.factory);

app.factory(
	"BaseController",
	function () {
	    function BaseController($scope, Module, DataQueryFactory) {
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
	            var query = $scope.currentDataQuery;
	            query.search($scope.conditions);
	        }

	        $scope.onClickRow = function (element, rowIndex, rowData) {
	            //$scope.queries.dqDetail.currentitem = rowData;
	        }

	        $scope.onClickCell = function (element, rowIndex, field, value) {

	        }

	        $scope.onDblClickCell = function (element) {

	        }

	        this.init = function (moduleId) {
	            $scope.moduleId = moduleId;
	            var query = Module.getModuleQuery({ id: moduleId }).$promise;
	            var then = query.then(function (data) {
	                $scope.queries = data;
	                angular.forEach(data, function (query, index) {
	                    var item = DataQueryFactory.create($scope, query);
	                    $scope.queries[query.Name] = item;
	                });

	                if ($scope.queries.length > 0) {
	                    if ($scope.queries.length == 1)
	                        $scope.currentDataQuery = $scope.queries[0]
	                    else if ($scope.queries.hasOwnProperty('dqBillList'))
	                        $scope.currentDataQuery = $scope.queries.dqBillList;
	                    else
	                        $scope.currentDataQuery = $scope.queries.dqMaster;

	                    if ($scope.currentDataQuery) {
	                        $scope.currentDataQuery.init(function () {
	                            $scope.conditions = $scope.currentDataQuery.getConditionFields();
	                        });
	                    }
	                }
	            });
	        };

	        BaseController.prototype = {
	            init: this.init
	        };
	        return (this);
	    }
	    return (BaseController);
	}
);



//app.factory("app.SubController",
//	function (BaseController, Module, DataQueryFactory) {

//	    function SubController($scope, Module, DataQueryFactory) {

//	        BaseController.call(this, $scope, Module, DataQueryFactory);

//	        this.init('22050010');

//	        //BaseController.prototype.init.call(this,'22050010');
//	    }

//	    // Extend the base controller.
//	    SubController.prototype = Object.create(BaseController.prototype);

//	    // Add sub-class methods.
//	    //		SubController.prototype.getBar = function () {
//	    //			return ("Bar ( from SubController )");
//	    //		};

//	    //		SubController.prototype.getFoo = function () {

//	    //			return (
//	    //				BaseController.prototype.getFoo.call(this) +
//	    //				"( overridden by SubClass )"
//	    //			);

//	    //		};

//	    return (SubController);

//	}
//);