var app = angular.module('ereuna', ['ngFacebook', 'ui.bootstrap', 'ui.router'])
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
            templateUrl: 'partials/home.html',
            controller: 'HomeController',
            controllerAs: 'vm'
        })
        .state('projects', {
            url: '/projects',
            templateUrl: 'partials/projects.html',
            controller: 'ManageProjectsController',
            controllerAs: 'vm'
        })
        .state('project', {
            abstract: true,
            url: '/projects/:projectId',
            templateUrl: 'partials/project.html',
            controller: function ($scope, $http, $state, $stateParams) {
                var vm = this;

                vm.projectId = $state.projectId;
                vm.tabs = [
                    { text: 'Overview', state: 'project.overview' },
                    { text: 'World', state: 'project.world' },
                    { text: 'Characters', state: 'project.characters' }
                ];

                activate();

                function activate() {
                    for (var i = 0; i < vm.tabs.length; i++) {
                        var tab = vm.tabs[i];
                        tab.active = ($state.$current.name === tab.state);
                    }
                };
            },
            controllerAs: 'vm'
        })
        .state('project.overview', {
            url: '/overview',
            templateUrl: 'partials/project-overview.html',
            controller: 'ProjectOverviewController',
            controllerAs: 'vm'
        })
        .state('project.world', {
            url: '/world',
            templateUrl: 'partials/project-world.html',
            controller: 'ProjectWorldController',
            controllerAs: 'vm'
        })
        .state('project.characters', {
            url: '/characters',
            templateUrl: 'partials/project-characters.html',
            controller: 'ProjectCharactersController',
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