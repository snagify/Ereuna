
app.controller('ProjectController', function ($rootScope, $scope, $http, $facebook, $window, $location, $stateParams) {
    $scope.ProjectId = $stateParams.projectId;
    $scope.ProjectInformation = "";
    $scope.IsEditingProject = false;

    $scope.EditProject = function () {
        doEditProject();
    };

    $scope.CancelProjectChanges = function () {
        doCancelProjectChanges();
    };

    $scope.SaveProject = function() {
        doSaveProject();
    };

    doLoadProject();


    function doLoadProject() {
        $http({
                method: 'GET',
                url: 'api/projectsummary?projectId=' + $stateParams.projectId
            })
            .success(function(data, status) {

                if (status == 200) {
                    $rootScope.IsProjectLoaded = true;
                    $rootScope.ProjectName = data.Name;
                    $rootScope.ProjectType = data.Type;
                    $rootScope.ProjectId = data.Id;
                    $scope.ProjectInformation = "";
                }


            })
            .error(function(data, status) {

            });
    }

    function doEditProject() {
        if ($rootScope.IsProjectLoaded == true) {
            $scope.IsEditingProject = true;
        }
    }

    function doCancelProjectChanges() {
        if ($rootScope.IsProjectLoaded == true) {
            doLoadProject();
            $scope.IsEditingProject = false;
        }

    }
    function doSaveProject() {
        if ($rootScope.IsProjectLoaded == true) {

            var data = { Id: $scope.ProjectId, Name: $scope.ProjectName, Description: $scope.ProjectInformation };

            $http.put('api/projectsummary', data)
                .success(function (data, status) {
                    if (status == 200) {
                        $rootScope.ProjectName = $scope.ProjectName;
                    }

                    $scope.IsEditingProject = false;
                })
                .error(function (data, status) {

                });


            
        }
    }
});