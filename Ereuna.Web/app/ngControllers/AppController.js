// The root controller for the app.
// Try not to do too much in here, just overall high level checks like auth.

app.controller('AppController', function ($rootScope, $scope, $http, $facebook, $window, $location) {
    // App centric items: User details, current project details

    $rootScope.LoginTypeIsFacebook = true;
    $rootScope.FullName = "Default username";
    
    $rootScope.IsProjectLoaded = false;
    $rootScope.ProjectName = "Default projectname";
    $rootScope.ProjectType = "Book/Series";
    $rootScope.ProjectId = 0;

    // Step 1, verify the user is logged in.
    $http({
        method: 'GET',
        url: 'api/login/'
    }).success(function (data, status) {

        if (status == 403) $window.location.assign('/index.html#login');
        else {
            $rootScope.SessionToken = data.SessionToken;
            $window.sessionStorage.token = data.SessionToken;

            $rootScope.FullName = data.User.First + ' ' + data.User.Last;
            $rootScope.LoginType = 'FB';
            $rootScope.UserId = data.User.UserId;

        }

    }).error(function (data, status) {
        // Assume login failed. Redirect user
        $window.location.assign('/index.html#login');
    });



    // Actions are basically navigation and menus
    // FB Logout
    // Change project
    // POssibly a home option for currnet project
    // Also possibly flash items to show occasionally
    // And of course load the main project


    

});