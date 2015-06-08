// http://ngmodules.org/modules/ngFacebook

app.controller('LoginController', function ($rootScope, $scope, $http, $facebook, $window) {
    var endpoint = 'api/login';

    $scope.loading = true;
    
    $scope.DoFacebookLogin = function() {
        $facebook.login();
    };

    $scope.DoFacebookLogout = function () {
        $facebook.logout();
    };

    $scope.DoFacebookDeauthentication = function () {
        var url = '/me/permissions';
        $facebook.api(url, 'DELETE').then(function (response) {
            console.log(response);
            if (response) {
                $rootScope.userFullName = '';
                $rootScope.LoginType = 'None';
                $rootScope.IsLoggedIn = false;
            }
        });
    };


    function FakeProjects() {
        var p = [
            { Id: 1, Name: 'Dancing with the Dead', Type: 'Books' },
            { Id: 2, Name: 'Salmon Man', Type: 'Comics' },
            { Id: 3, Name: 'Dude: Wheres My Car - The Stage Show', Type: 'Scripts' }
        ];
        return p;
    }

    function LoginToServer(user) {
        var response = $facebook.getAuthResponse();
        var token = {
            UserId: response.userID,
            AccessToken: response.accessToken,
            ExpiresIn: response.expiresIn,
            SignedRequest: response.signedRequest,
            Email: user.email,
            FirstName: user.first_name,
            LastName: user.last_name
        };
        $http.post(endpoint, token).success(function (data, status, headers, config) {
            console.log(data);
            console.log(status);
            console.log(headers);
            console.log(config);
            
            $rootScope.SessionToken = data;
            $window.sessionStorage.token = data;

            // TODO: Remove this, it's just for testing
            $rootScope.HasProjects = true;
            //$rootScope.Projects = FakeProjects();

            $http({
                method: 'GET',
                url: 'api/projects'
            }).success(function (data, status) {
                console.log(data);
            }).error(function (data, status) {
                console.log(status);
            });

            // data contains the response
            // status is the HTTP status
            // headers is the header getter function
            // config is the object that was used to create the HTTP request
        }).error(function (data, status, headers, config) {
            $rootScope.HasProjects = false;
            $rootScope.IsLoggedIn = false;

            console.log('Error occurred during login getting response from server');
            console.log(data);
            console.log(status);
            console.log(headers);
            console.log(config);
        });
    }


    $scope.$on('fb.auth.authResponseChange', function () {
        $rootScope.IsLoggedIn = $facebook.isConnected();
        if ($rootScope.IsLoggedIn) {
            $rootScope.LoginType = 'FB';

            $facebook.api('/me').then(function (user) {
                $rootScope.userFullName = user.name;

                LoginToServer(user);
            });
        }
    });

    $scope.$on('fb.load', function () {
        $scope.loading = false;
    });

});