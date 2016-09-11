
app.controller('ProjectController', function ($rootScope, $scope, $http, $facebook, $window, $location, $stateParams) {
    $scope.ProjectId = $stateParams.projectId;
   
    $http({
        method: 'GET',
        url: 'api/projects?projectId=' + $stateParams.projectId
    }).success(function (data, status) {

        if (status == 200) {
            $rootScope.IsProjectLoaded = true;
            $rootScope.ProjectName = data.Name;
            $rootScope.ProjectType = data.Type;
        }

    }).error(function (data, status) {
        
    });
});