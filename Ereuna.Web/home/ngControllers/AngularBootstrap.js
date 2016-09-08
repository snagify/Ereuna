var app = angular.module('ereuna', ['ProjectTypesService', 'ngFacebook', 'ui.bootstrap', 'ui.router', 'ngRoute'])
    .config(['$facebookProvider', function ($facebookProvider) {
        $facebookProvider.setAppId('923459464377126').setPermissions(['email', 'public_profile']);
    }])
    .run(['$rootScope', '$window', '$state', '$stateParams', function ($rootScope, $window, $state, $stateParams) {
        $rootScope.userFullName = '';
        $rootScope.IsLoggedIn = false;
        $rootScope.LoginType = 'None';
        $rootScope.HasProjects = false;
        $rootScope.Projects = [];

        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
        

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

app.config(['$stateProvider', '$urlRouterProvider', configRoutes]);

function configRoutes($stateProvider, $urlRouterProvider) {
    $stateProvider
        .state('home', {
            url: '/',
            templateUrl: 'home/ngPartials/home.html',
            controller: 'HomeController',
            controllerAs: 'vm'
        })
        .state('login', {
            url: '/login',
            templateUrl: 'home/ngPartials/login.html',
            controller: 'LoginController',
            controllerAs: 'vm'
        })
        .state('debug', {
            url: '/debug',
            templateUrl: 'home/ngPartials/debug.html',
            controller: 'DebugController',
            controllerAs: 'vm'
        })
        .state('projectsummary', {
            url: '/projectsummary/:projectId',
            templateUrl: 'home/ngPartials/projectsummary.html',
            controller: 'ProjectSummaryController',
            controllerAs: 'vm'
        })
        .state('newproject', {
            url: '/newproject',
            templateUrl: 'home/ngPartials/newproject.html',
            controller: 'NewProjectController',
            controllerAs: 'vm'
        })
        ;

    $urlRouterProvider.otherwise('/');
}

// "Borrowed" from https://auth0.com/blog/2014/01/07/angularjs-authentication-with-cookies-vs-token/
app.factory('authInterceptor', function ($rootScope, $q, $window) {
    return {
        request: function (config) {
            config.headers = config.headers || {};
            if ($window.sessionStorage.token) {
                config.headers.Authorization = 'Bearer ' + $window.sessionStorage.token;
            }
            return config;
        },
        response: function (response) {
            if (response.status === 401) {
                // handle the case where the user is not authenticated
            }
            return response || $q.when(response);
        }
    };
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptor');
});