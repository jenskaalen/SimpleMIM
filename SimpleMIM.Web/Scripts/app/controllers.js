var app = angular.module('simpleMim');


app.controller('flowController', function ($scope, $http) {
    $scope.objectType = "person";
    $scope.compiled = false;
    $scope.funcName = "testFlow";
    $scope.flowRule = {
        TargetAttribute: "Attribute", Expression: "x = entry['FirstName'].Value + ' random text ' + entry['LastName'].Value\nreturn x",
        Name: "testRule", RuleType: "Python"
    };
    
    $scope.createNewRule = function () {
        $scope.flowRule = {
            TargetAttribute: "Attribute", Expression: "",
            Name: "newRuleName", RuleType: "Python"
        };

        $scope.ruleSaved = false;
    }

    $scope.flowRules = [];

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

    $scope.loadFlowRules = function() {
        $http.get('/api/FlowRule/GetAll').success(function(flowRules) {
            $scope.flowRules = flowRules;
        });
    }

    $scope.saveFlowRule = function() {
        $http.post('/api/FlowRule/Save', $scope.flowRule)
            .success(function () {
                if ($scope.flowRules.indexOf($scope.flowRule < 0)) {
                    $scope.flowRules.push($scope.flowRule);
                }

                $scope.ruleSaved = true;
            })
            .error(function (result) {
            alert('couldnt save flowrule');
        });
    }

    $scope.testFunction = function () {
        
        var test = {
            Attributes: $scope.attribs,
            FlowRule: $scope.flowRule
        };

        if ($scope.flowRule.Expression === "")
            return;

        $http.post('/api/FlowRule/Test', test).then(function success(result) {
            $scope.result = result.data;
        }, function error(result) {
            alert('test failed ' + result.data + result.statusText);
        });
    }

    $scope.loadFlowRules();

    $(document).delegate('#textbox', 'keydown', function (e) {
        //var $this, end, start;
        if (e.keyCode === 9) {
            var start = this.selectionStart;
            var end = this.selectionEnd;

            $scope.$apply(function () {
                $scope.flowRule.Expression = [$scope.flowRule.Expression.slice(0, start), '\t', $scope.flowRule.Expression.slice(start)].join('');
                angular.element(document.getElementById('#textbox')).triggerHandler('change');
            });

            $(this).get(0).selectionStart =
            $(this).get(0).selectionEnd = start + 1;

            return false;
        }
    });
});



//$(document).delegate('#textbox', 'keydown', function (e) {
//    var keyCode = e.keyCode || e.which;

//    if (keyCode == 9) {
//        e.preventDefault();
//        var start = $(this).get(0).selectionStart;
//        var end = $(this).get(0).selectionEnd;

//        // set textarea value to: text before caret + tab + text after caret
//        $(this).val($(this).val().substring(0, start)
//                    + "\t"
//                    + $(this).val().substring(end));

//        // put caret at right position again
//        $(this).get(0).selectionStart =
//        $(this).get(0).selectionEnd = start + 1;
//    }
//});