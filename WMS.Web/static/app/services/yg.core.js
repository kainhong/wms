//var app = angular.module('app', []);
var app = angular.module('app', ['ui.component', 'yg.services', 'ui.envirment']);
app.filter("billStatus", function () {
    var filterfun = function (id) {
        if (id == 11)
            return "建单";
        return id;
    };
    return filterfun;
});

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
			        Constructor = constructors[cacheKey] = $injector.invoke(controllerFactory)
			    }

			    return $injector.instantiate(Constructor, { "$scope": $scope });
			}
		);
        return (core);
    };
    core.factory = factory;

})(app, app.factory);

app.factory(
	"BaseController",
	function () {
	    function BaseController($scope,$location,Module, DataQueryFactory) {
	        $scope.queries = {};
	        $scope.conditions = [];
	        $scope.moduleId = getModuleId($location.$$absUrl);

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

	        function getModuleId(url) {
	            var reg = /\/(\d{8})\/.*htm(l?)/i;
	            var m = reg.exec(url);
	            if (m)
	                return m[1];
	            else
	                return 0;
	        }

	        this.init = function (moduleId) {
	            $scope.moduleId = moduleId;
	            var query = Module.getModuleQuery({ id: moduleId }).$promise;
	            var then = query.then(function (data) {
	                $scope.queries = DataQueryFactory.create($scope, data, null);
	                if ($scope.currentDataQuery) {
	                    $scope.currentDataQuery.visible = true;
	                    $scope.currentDataQuery.init(function () {
	                        $scope.conditions = $scope.currentDataQuery.getConditionFields();
	                    });
	                }
	            });
	            return then;
	        };

	        BaseController.prototype = {
	            init: this.init
	        };
	        return (this);
	    }
	    return (BaseController);
	}
);

app.factory('BillController', function (BaseController, $location, Module, DataQueryFactory, $timeout) {

    function BillController($scope, $location, Module, DataQueryFactory) {
	        $scope.viewType = "bill";
	        function load() {
	            $scope.queries.dqMaster.$on('opened', function (event, args) {
	                var master = $scope.queries.dqMaster;
	                $scope.billState = master.datasource.length > 0 ? master.datasource[0] : {};
	                if (master.datasource && master.datasource.length > 0) {
	                    master.focusRowIndex = 0;
	                    master.focusRow = master.datasource[0];
	                    $timeout(function () {
	                        $scope.queries.dqDetail.open(master.focusRow);
	                    }, 100);
	                }
	            });
	        }

	        $scope.changeView = function (viewtype) {
	            $timeout(function () {
	                $scope.viewType = viewtype;
	            }, 10);
	        }

	        $scope.$on('onDblClickRow', function (event, args) {
	            //console.log(query);
	            var query = args.dataquery;
	            if (query.Name != 'dqBillList') {
	                $scope.changeView('bill');
	                return;
	            }
	            $scope.changeView("list");
	            var row = query.focusRow;
	            if (row == null)
	                return;
	            var billNO = row[$scope.BillNOFieldName];
	            if (billNO == $scope.billNO)
	                return;

	            $scope.billNO = billNO;
	            //$scope.$digest();
	            if (billNO && $scope.queries.dqMaster) {
	                $timeout(function () {
	                    $scope.queries.dqMaster.open(billNO);
	                }, 100);
	            }
	        });

	        BaseController.call(this, $scope, $location, Module, DataQueryFactory);

	        BillController.prototype.init = this.init = function (moduleId, current) {
	            $scope.moduleId = moduleId;
	            var query = Module.getModuleQuery({ id: moduleId }).$promise;
	            var then = query.then(function (data) {
	                $scope.queries = DataQueryFactory.create($scope, data, 'dqBillList');
	                if ($scope.currentDataQuery) {
	                    $scope.currentDataQuery.visible = true;
	                    $scope.currentDataQuery.ReadOnly = true;
	                    $scope.currentDataQuery.Editable = false;
	                    $scope.currentDataQuery.init(function () {
	                        $scope.conditions = $scope.currentDataQuery.getConditionFields();
	                    });
	                }
	            }).then(load);

	            return then;
	        }
	    }

	    BillController.prototype = Object.create(BaseController.prototype);

	    return (BillController);
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