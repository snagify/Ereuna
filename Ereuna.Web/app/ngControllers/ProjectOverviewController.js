
app.controller('ProjectOverviewController', function ($rootScope, $scope, $http, $facebook, $window, $location, $stateParams) {
    $scope.ProjectId = $stateParams.projectId;
    $scope.ProjectInformation = "";
    $scope.IsEditingProject = false;

    $scope.Ideas = [{}];
    $scope.HasIdeas = false;

    $scope.NewIdeaTitle = '';
    $scope.NewIdeaDescription = '';
    $scope.NewIdeaImportance = 1;

    $scope.SelectedIdeaId = 0;
    $scope.SelectedIdeaTitle = '';
    $scope.SelectedIdeaDescription = '';
    $scope.SelectedIdeaImportance = 0;
    
    $scope.EditProject = function () {
        doEditProject();
    };

    $scope.SaveExistingIdea = function() {
        var idea = FindIdea($scope.SelectedIdeaId);
        if (idea === null) {
        } else {
            idea.Title = $scope.SelectedIdeaTitle;
            idea.Description = $scope.SelectedIdeaDescription;
            idea.Importance = $scope.SelectedIdeaImportance;

            $http.put('api/idea', idea)
                .success(function(data, status) {
                    // Erm, should I flash dialog?
                })
                .error(function(data, status, headers, config) {

                });
        }
    };

    $scope.RevertExistingIdea = function () {
        // Do nothing (I think!)
    };

    $scope.SelectIdea = function (id) {

        var idea = FindIdea(id);
        if (idea === null) { }
        else {
            $scope.SelectedIdeaId = id;
            $scope.SelectedIdeaTitle = idea.Title;
            $scope.SelectedIdeaDescription = idea.Description;
            $scope.SelectedIdeaImportance = idea.Importance;

            $("#EditIdeaModal").modal();
        }
    };

    function FindIdea(id) {
        for (i = 0; i < $scope.Ideas.length; i++) {
            var idea = $scope.Ideas[i];
            if (idea.Id == id) {
                return idea;
            }
        }
        return null;
    }

    $scope.CancelProjectChanges = function () {
        doCancelProjectChanges();
    };

    $scope.SaveProject = function() {
        doSaveProject();
    };

    $scope.AddIdea = function() {
        var newIdea = {
            Id: 0,
            ProjectId: $stateParams.projectId,
            Title: $scope.NewIdeaTitle,
            Description: $scope.NewIdeaDescription,
            WhenAdded: Date(),
            Importance: $scope.NewIdeaImportance
        };

        $http.post('api/idea', newIdea)
            .success(function(data, status) {
                if (status == 200) {
                    newIdea.Id = data;

                    $scope.Ideas.push(newIdea);

                    $scope.NewIdeaTitle = '';
                    $scope.NewIdeaDescription = '';
                    $scope.NewIdeaImportance = 1;
                }
            })
            .error(function(data, status, headers, config) {

            });
    }

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
                    $scope.ProjectName = data.Name;
                    $rootScope.ProjectType = data.Type;
                    $rootScope.ProjectId = data.Id;
                    $scope.ProjectInformation = data.Description;

                    doLoadProjectIdeas($stateParams.projectId);

                }

            })
            .error(function(data, status) {

            });
    }

    function doLoadProjectIdeas(projectId) {
        $http({
            method: 'GET',
            url: 'api/idea?id=' + projectId
        })
           .success(function (data, status) {
               if (status == 200) {
                   $scope.Ideas = data;
                   if (data.length > 0) $scope.HasIdeas = true;
               }
           })
           .error(function (data, status) {

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