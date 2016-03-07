var app = angular.module('simpleMim');

//app.controller('attributeEditorController', function ($scope, $http) {
//    $http.get('/api/Mock/GetMockAttribs').success(function (attribs) {
//        $scope.attribs = attribs;
//    });
//});

app.directive('pythonEditor', function() {
    return {
        restrict: 'E',
        scope: { editorText: '=' },
        link: function(scope, elem, attr) {

            $(document).delegate('#textbox', 'keydown', function (e) {
                //var $this, end, start;
                if (e.keyCode === 9) {
                    var start = this.selectionStart;
                    var end = this.selectionEnd;

                    scope.$apply(function () {
                        scope.editorText = [scope.editorText.slice(0, start), '\t', scope.editorText.slice(start)].join('');
                        angular.element(document.getElementById('#textbox')).triggerHandler('change');
                    });

                    $(this).get(0).selectionStart =
                    $(this).get(0).selectionEnd = start + 1;

                    return false;
                }
            });
        },
        templateUrl: '/Content/templates/pythonEditor.html'
    }
});
