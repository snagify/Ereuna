app.controller('LoginController', function ($rootScope, $scope, $http, $facebook) {

    $scope.loading = true;

    //function InitialiseFacebook() {
    //    window.FB.init({
    //        appId: '923459464377126',
    //        cookie: true, // enable cookies to allow the server to access the session
    //        xfbml: true, // parse social plugins on this page
    //        version: 'v2.3' // use version 2.2
    //    });
    //};

    //function CheckFacebookLoginStatus() {
    //    window.FB.getLoginStatus(function (response) {
    //        console.log('Facebook login response: ' + response);
    //        if (response.status === 'connected') {
    //            $rootScope.IsLoggedIn = true;
    //            $rootScope.LoginType = 'FB';

    //            window.FB.api('/me', function (response) {
    //                console.log('User info: ' + response);
    //                $rootScope.userFullName = response.name;
    //            });

    //        } else if (response.status === 'not_authorized') {
    //            $rootScope.IsLoggedIn = false;
    //        } else {
    //            Console.log('Unexpected response from Facebook API');
    //            $rootScope.IsLoggedIn = false;
    //        }
    //        $scope.loading = false;
    //    });
    //};

    //$scope.DoFacebookLogin = function() {
    //    CheckFacebookLoginStatus();
    //};
    
    //window.fbAsyncInit = function () {
    //    InitialiseFacebook();
    //    CheckFacebookLoginStatus();
    //};

    $scope.DoFacebookLogin = function() {
        $facebook.login();
    };

    $scope.DoFacebookLogout = function () {
        $facebook.logout();
    };

    $scope.$on('fb.auth.authResponseChange', function () {
        $rootScope.IsLoggedIn = $facebook.isConnected();
        if ($rootScope.IsLoggedIn) {
            $rootScope.LoginType = 'FB';

            $facebook.api('/me').then(function (user) {
                $rootScope.userFullName = user.name;
            });
        }
    });

    $scope.$on('fb.load', function () {
        $scope.loading = false;
    });

});