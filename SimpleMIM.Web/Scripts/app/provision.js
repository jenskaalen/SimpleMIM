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

    $scope.provRules = [];

    $scope.provRule = {
        SourceObject: "person", Condition: "return entry['DepartmentID'].Value == 'Fabrikam'",
        TargetObject: "HRMUser", Agent: "HRM Agent", RuleType: "Python", InitialFlows: []
    };

    $scope.createNewRule = function () {
        $scope.provRule = {
            SourceObject: "person", Condition: "",
            TargetObject: "systemUser", Agent: "Test Agent", RuleType: "Python", InitialFlows: []
        };

        $scope.ruleSaved = false;
    }


    $http.get('/api/Mock/GetMVEntryMock').success(function (mventry) {
        $scope.mventry = mventry;
    });

    $scope.saveprovRule = function () {
        $http.post('/api/ProvisionRule/Save', $scope.provRule)
            .success(function () {

                if ($scope.provRules.indexOf($scope.provRule) < 0) {
                    $scope.provRules.push($scope.provRule);
                }

                $scope.ruleSaved = true;
            })
            .error(function (result) {
                alert('couldnt save provRule');
            });
    }

    $scope.testFunction = function () {
        var test = {
            MVEntry: $scope.mventry,
            ProvisionRule: $scope.provRule
        };

        $http.post('/api/ProvisionRule/Test', test).then(function success(result) {
            $scope.result = result.data;

            if ($scope.result !== false && $scope.result !== true) {
                alert('Test didnt return true or false - it is not a valid provision rule');
            }

        }, function error(result) {
            alert('test failed ' + result.data + result.statusText);
        });
    }

    $scope.loadprovRules = function () {
        $http.get('/api/ProvisionRule/GetAll').success(function (provRules) {
            $scope.provRules = provRules;
        });
    }

    $scope.loadprovRules();
});