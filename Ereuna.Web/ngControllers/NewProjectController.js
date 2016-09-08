app.controller('NewProjectController', function ($scope, $http, $projectTypes, $location) {

    $scope.ProjectName = "My first story";
    $scope.Description = "";
    $scope.SelectedProjectType = 1;

    $scope.ProjectTypes = $projectTypes;

    $scope.ClearForm = function() {
        $scope.ProjectName = "My first story";
        $scope.Description = "";
        $scope.SelectedProjectType = "";
    };


    $scope.SaveProject = function () {
        var newProject = { name: $scope.ProjectName, description: $scope.Description, projectTypeId: $scope.SelectedProjectType };

        $http
            .post('api/projects', newProject)
            .success(function (data, status, headers, config) {
                console.log('New project id:' + data);
                $location.path('/projectsummary/' + data);
            })
            .error(function (data, status, headers, config) {
                console.log(data);
                console.log(status);
                console.log(headers);
                console.log(config);
        });
    };
});