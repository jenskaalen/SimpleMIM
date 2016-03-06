var app = angular.module('simpleMim');

//app.controller('attributeEditorController', function ($scope, $http) {
//    $http.get('/api/Mock/GetMockAttribs').success(function (attribs) {
//        $scope.attribs = attribs;
//    });
//});

app.directive('entryViewer', function() {
    return {
        restrict: 'E',
        scope: { entry: '=', editable: '@' },
        link: function(scope, elem, attr) {
            scope.addNewAttribute = function(entry, name) {
                entry.MockAttributes[name] = {
                    Name: name,
                    Value: "Somevalue"
                };
            };
        },
        //controller: 'attributeEditorController',
        templateUrl: '/Content/templates/entryViewer.html'
    }
});
