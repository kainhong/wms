//angular.module('yg.directives.editor').factory('widgetFactory', ['$parse', '$log', function($parse, $log) {
//    var create = function(editor)
//    {
//
//    };
//    return create;
//}]);

uimodule.factory('editorFactory', ['$timeout', '$parse',
  function ($timeout, $parse) {

      var create = function (editor,Module) {
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
                      if (field && field.DispValueType == 'dvtSQL' && field.DisplayValue && field.DisplayValue.length > 0) {
                          var op = {
                              valueField: field.ValueMember.toUpperCase(),
                              textField: field.DisplayMember.toUpperCase()
                          };

                          op = $.extend(options, op);

                          Module.getFieldDataSource({ id: field.moduleId, name: field.queryName, fieldName: field.FieldName },
                              function (data) {
                                  op.data = data;
                                  element[editor](op);
                              }, function (error) {

                              }
                         );
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
        uimodule.directive(editor, ['editorFactory','Module',
            function (editorFactory,Module) {
                return editorFactory.create(editor,Module);
            }
        ]);
    });
} (angular));