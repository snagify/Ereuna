
app.controller('CharacterController', function ($rootScope, $scope, $http, $stateParams, $facebook, $window, $location) {
    $scope.ProjectId = $stateParams.projectId;

    $scope.Character = { Name: '', Description: '', Id: $stateParams.characterid, ProjectId: $scope.ProjectId, ImageId: 0, TypeId: 1 };

    $scope.EditingName = '';
    $scope.EditingDescription = '';
    $scope.IsEditingName = false;
    $scope.IsEditingDescription = false;
    
    if ($scope.Character.Id > 0) doLoadCharacter($scope.Character.Id);

    $scope.SaveName = doSaveName;
    $scope.CancelName = doCancelName;
    $scope.SaveDescription = doSaveDescription;
    $scope.CancelDescription = doCancelDescription;
    $scope.StartEditingName = doStartEditName;
    $scope.StartEditingDescription = doStartEditDescription;

    function doStartEditName() {
        $scope.EditingName = $scope.Character.Name;
        $scope.IsEditingName = true;
    };
    function doStartEditDescription() {
        $scope.EditingDescription = $scope.Character.Description;
        $scope.IsEditingDescription = true;
    };

    function doSaveName() {
        $scope.Character.Name = $scope.EditingName;

        $scope.IsEditingName = false;
    };

    function doCancelName() {
        $scope.EditingName = '';
        $scope.IsEditingName = false;
    }

    function doSaveDescription() {
        $scope.Character.Description = $scope.EditingDescription;

        $scope.IsEditingDescription = false;
    };
    function doCancelDescription() {
        $scope.EditingDescription = '';
        $scope.IsEditingDescription = false;
    };

    function doLoadCharacter(id) {
        $http.get('api/characterdetail?characterid=' + id + '&projectid=' + $scope.ProjectId)
            .success(function(data, status) {
                if (status == 200) {
                    $scope.Character = data;
                }
            })
            .error(function(data, status, headers, config) {

            });
    }
});