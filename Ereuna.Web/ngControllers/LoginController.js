// http://ngmodules.org/modules/ngFacebook

app.controller('LoginController', function ($rootScope, $scope, $http, $facebook, $window, $location) {


    function GetStyleClassForGlassMenu(menutarget) {
        var current = $location.path().substr(1, menutarget.length + 1);
        var style = (current === menutarget) ? 'glass redglass' : 'glass blackglass';
        return style;
    }

    //$rootScope.$on('$locationChangeSuccess'), function () {
    //    $scope.LoginMenuClass = GetStyleClassForGlassMenu('login');
    //    $scope.HomeMenuClass = GetStyleClassForGlassMenu('home');
    //};


    //$rootScope.$on('$locationChangeStart'), function () {
    //    $scope.LoginMenuClass = GetStyleClassForGlassMenu('login');
    //    $scope.HomeMenuClass = GetStyleClassForGlassMenu('home');

    //};

    //$rootScope.$on('$stateChangeStart'), function () {
    //    $scope.LoginMenuClass = GetStyleClassForGlassMenu('login');
    //    $scope.HomeMenuClass = GetStyleClassForGlassMenu('home');

    //};

    //$rootScope.$on('$stateChangeSuccess'), function () {
    //    $scope.LoginMenuClass = GetStyleClassForGlassMenu('login');
    //    $scope.HomeMenuClass = GetStyleClassForGlassMenu('home');

    //};
});