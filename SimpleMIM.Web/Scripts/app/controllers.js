var app = angular.module('simpleMim');


app.controller('flowController', function ($scope, $http) {
    $scope.objectType = "person";
    $scope.compiled = false;
    $scope.funcName = "testFlow";
    $scope.script = "entry['FirstName'].Value + ' random text ' + entry['LastName'].Value";

    $http.get('/api/Mock/GetMockAttribs').success(function (attribs) {
        $scope.attribs = attribs;
    });

    $scope.compilePython = function() {
        var compilation = { Script: $scope.script, Name: $scope.funcName };

        $http.post('/api/Python/Compile', compilation)
            .success(function () {
                $scope.compiled = true;
            })
            .error(function () {
            alert('couldnt compile');
        });
    }

    $scope.testFunction = function() {
        var test = {
            ObjectType: $scope.objectType,
            Attribs: $scope.attribs,
            Name: $scope.funcName
        };

        $http.post('/api/Python/Test', test).success(function (result) {
            $scope.result = result;
        }).error(function() {
            alert('test failed');
        });
    }
});



$(document).delegate('#textbox', 'keydown', function (e) {
    var keyCode = e.keyCode || e.which;

    if (keyCode == 9) {
        e.preventDefault();
        var start = $(this).get(0).selectionStart;
        var end = $(this).get(0).selectionEnd;

        // set textarea value to: text before caret + tab + text after caret
        $(this).val($(this).val().substring(0, start)
                    + "\t"
                    + $(this).val().substring(end));

        // put caret at right position again
        $(this).get(0).selectionStart =
        $(this).get(0).selectionEnd = start + 1;
    }
});