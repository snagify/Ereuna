var app = angular.module('ereuna', ['ProjectTypesService', 'ngFacebook', 'ui.bootstrap', 'ui.router', 'ngRoute', 'ui.sortable'])
    .config(['$facebookProvider', function ($facebookProvider) {
        $facebookProvider.setAppId('923459464377126').setPermissions(['email', 'public_profile']);
    }])
    .run(['$rootScope', '$window', '$state', '$stateParams', function ($rootScope, $window, $state, $stateParams) {


    }]);

app.config(['$stateProvider', '$urlRouterProvider', configRoutes]);

function configRoutes($stateProvider, $urlRouterProvider) {
    $stateProvider
        .state('home', {
            url: '/',
            templateUrl: 'app/ngPartials/Home.html',
            controller: 'HomeController',
            controllerAs: 'vm'
        })
    .state('projectoverview', {
        url: '/:projectId',
        templateUrl: 'app/ngPartials/ProjectOverview.html',
        controller: 'ProjectOverviewController',
        controllerAs: 'vm'
    })

    .state('events', {
        url: '/:projectId/events',
        templateUrl: 'app/ngPartials/Events.html',
        controller: 'EventsController',
        controllerAs: 'vm'
    })
    .state('world', {
        url: '/:projectId/world',
        templateUrl: 'app/ngPartials/World.html',
        controller: 'WorldController',
        controllerAs: 'vm'
    })
    .state('locations', {
        url: '/:projectId/locations',
        templateUrl: 'app/ngPartials/Locations.html',
        controller: 'LocationsController',
        controllerAs: 'vm'
    })
    .state('characters', {
        url: '/:projectId/characters',
        templateUrl: 'app/ngPartials/Characters.html',
        controller: 'CharactersController',
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