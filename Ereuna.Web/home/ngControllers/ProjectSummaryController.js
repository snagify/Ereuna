app.controller('ProjectSummaryController', function ($scope, $http, $routeParams, $stateParams) {
    $scope.ProjectId = $stateParams.projectId;
    $scope.LastUsed = Date();
    $scope.Name = 'Unknown';
    $scope.Description = 'Unknown';
    $scope.Type = 'Unknown';

    $http({
        method: 'GET',
        url: 'api/projectsummary/' + $stateParams.projectId
    }).success(function (data, status) {
        
        $scope.LastUsed = data[0].LastUsed;
        $scope.Name = data[0].Name;
        $scope.Description = data[0].Description;
        $scope.Type = data[0].Type;

    }).error(function (data, status) {
        console.log(status);
        // TODO: Redirect to an error page
    });

});