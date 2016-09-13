
app.controller('HomeController', function ($rootScope, $scope, $http, $facebook, $window, $location) {

    $scope.HasProjects = false;

    LoadProjects();
    LoadRecentProjects();

    function LoadProjects() {
        $http({
            method: 'GET',
            url: 'api/projectsummary/'
        }).success(function(data, status) {

            $scope.AllProjects = data;
            if (data.length > 0) $scope.HasProjects = true;


        }).error(function (data, status) {
            
        });
    }

    function LoadRecentProjects() {
        $http({
            method: 'GET',
            url: 'api/recentprojects/'
        }).success(function (data, status) {

            $scope.RecentProjects = data;

        }).error(function (data, status) {

        });
    }
});