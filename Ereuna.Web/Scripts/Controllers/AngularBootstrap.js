var app = angular.module('ereuna', ['ngFacebook', 'ui.router'])
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
            controller: 'ProjectController',
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

app.run(['$state', function ($state) {

}]);