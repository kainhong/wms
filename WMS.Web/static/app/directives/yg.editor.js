
uimodule.factory('editorFactory', ['$timeout', '$parse',
  function ($timeout, $parse) {

      var create = function (editor, Module) {
          //var editName = editor.substring(6);
          return {
              restrict: 'ACE',
              //transclude: true,
              require: [editor, '?ngModel'],
              scope: false,
              //replace:false,
              controller: ['$scope', '$attrs', '$element', '$transclude', function ($scope, $attrs, $element, $transclude) {
                  var self = this;
              } ],
              link: function (scope, element, attrs, ctrls) {
                  var ctrl = ctrls[0];
                  var ngmodelCtrl = ctrls[1];

                  var field = null;
                  var options = {}; // 
                  scope.$watch(attrs.field, function (newl, oldl) {
                      if (newl == field)
                          return;
                      field = newl;
                      //element[editor](field.editor.options);

                    if (field && field.DispValueType == 'dvtSQL' && field.DisplayValue && field.DisplayValue.length > 0) {
                        var op = {
                            valueField: field.ValueMember.toUpperCase(),
                            textField: field.DisplayMember.toUpperCase(),
                            data: field.datasource
                        };

                        op = $.extend(options, op);
                        if (!op.data) {
                            Module.getFieldDataSource({ id: field.moduleId, name: field.queryName, fieldName: field.FieldName },
                                function (data) {
                                    op.data = data;
                                    element[editor](op);
                                }, function (error) {

                                });
                        }
                        else {
                            element[editor](op);
                        }
                    }
                  });

                  var methods = $.fn[editor].methods;
                  if (attrs.options) {
                      options = angular.element.extend(true, {}, scope.$eval(attrs.options));
                      element[editor](options);
                  }

                  if (methods.hasOwnProperty('setValue')) {
                      var lastvalue;
                      scope.$watch(function () {
                          if (lastvalue != ngmodelCtrl.$viewValue) {
                              lastvalue = ngmodelCtrl.$viewValue;
                              if (typeof lastvalue == 'Array')
                                  element[editor]('setValues', lastvalue);
                              else
                                  element[editor]('setValue', lastvalue);
                              //ngmodelCtrl.$render();
                          }
                      });
                  }

              }
          }
      }
      return {
          create: create
      };
  } ]);

(function (angular) {
    var editors = angular.injector(['ui.component']).get('editors');

    angular.forEach(editors, function (editor) {
        uimodule.directive(editor, ['editorFactory', 'Module',
            function (editorFactory, Module) {
                return editorFactory.create(editor, Module);
            }
        ]);
    });

    angular.forEach($.fn.datagrid.defaults.editors,function(editor){
       //var proxy = $.extend(editor,{});
       var fun = $.fn.datagrid.defaults.onCellValueChanged = function(cellValue){
           //console.log(cellValue);
       };

       var getFun = editor.getValue;
       var setFun = editor.setValue;
       var initFun = editor.init;
       editor.init = function(container, options)
       {
           target = initFun(container, options);
           return target;
       }
       //editor.cellValue = null;
       editor.setValue = function(target,value){
          target.cellValue = value;
          setFun(target,value);
       };

       editor.getValue = function(target){
          var val = getFun(target);
          var grid = target.parents('.datagrid-view').children('table');
          var fun = grid.datagrid('options').onCellValueChanged;          
          if( fun && target.cellValue != val)
            fun.apply(grid,[val]);
            
          if(!val || val.length == 0)
              val = null;
          target.cellValue = val;
          return val;
       }
    });
} (angular));