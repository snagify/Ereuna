app.controller('ProjectOverviewController').controller('ProjectOverviewController', ProjectOverviewController);
ProjectController.$inject = ['$scope', '$http', '$state', '$stateParams'];


function ProjectOverviewController($scope, $http, $state, $stateParams) {
    console.log($stateParams);
};

