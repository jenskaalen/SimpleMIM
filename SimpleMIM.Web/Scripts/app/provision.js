var app = angular.module('simpleMim');

app.controller('provisionController', function ($scope, $http) {
    //public string Id { get; set; }
    //public string SourceObject { get; set; }
    //public string TargetObject { get; set; }
    //public string Agent { get; set; }
    //public string Condition { get; set; }
    //public RuleType RuleType { get; set; }
    //public string[] InitialFlows { get; set; }
    //public bool Deprovision { get; set; }

    $scope.provRule = {
        SourceObject: "person", Condition: "entry['DepartmentID'].Value == 'FABRIKAM'",
        TargetObject: "HRMUser", Agent: "HRM Agent", RuleType: "Python", InitialFlows: []
    };


    $scope.loadFlowRules = function () {
        $http.get('/api/ProvRule/GetAll').success(function (flowRules) {
            $scope.flowRules = flowRules;
        });
    }

    $http.get('/api/Mock/GetMockAttribs').success(function (attribs) {
        $scope.attribs = attribs;
    });


    $scope.testFunction = function () {
        var test = {
            Attributes: $scope.attribs,
            ProvisionRule: $scope.provRule
        };

        $http.post('/api/ProvRule/Test', test).then(function success(result) {
            $scope.result = result.data;
        }, function error(result) {
            alert('test failed ' + result.data + result.statusText);
        });
    }


    $(document).delegate('#textbox', 'keydown', function (e) {
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