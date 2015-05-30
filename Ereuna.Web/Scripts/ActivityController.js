app.controller('ActivityController', function($scope, $http) {
    var endpoint = 'api/activity';

    $scope.loading = true;

    $http({
        method: 'GET',
        url: endpoint
    }).success(function (data, status) {
        $scope.loading = false;
        $scope.recentActivity = data;
    }).error(function(data, status) {

    });
});