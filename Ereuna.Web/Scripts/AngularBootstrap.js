var app = angular.module('ereuna', ['ngFacebook'])
    .config(['$facebookProvider', function ($facebookProvider) {
        $facebookProvider.setAppId('923459464377126').setPermissions(['email', 'public_profile']);
    }])
    .run(['$rootScope', '$window', function($rootScope, $window) {
        $rootScope.userFullName = '';
        $rootScope.IsLoggedIn = false;
        $rootScope.LoginType = 'None';

        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));
        $rootScope.$on('fb.load', function () {
            $window.dispatchEvent(new Event('fb.load'));
        });
    }]);

