app.controller('ProjectController').controller('ProjectController', ProjectController);
ProjectController.$inject = ['$scope', '$http', '$state', '$stateParams'];


function ProjectController($scope, $http, $state, $stateParams) {
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
            tab.active = ($state.current.name === tab.state);
        }

    };
};

