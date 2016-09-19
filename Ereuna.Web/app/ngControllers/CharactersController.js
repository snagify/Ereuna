
app.controller('CharactersController', function ($rootScope, $scope, $http, $stateParams, $facebook, $window, $location) {
    $scope.ProjectId = $stateParams.projectId;

    $scope.characterAccordionStatus = { isProtagonistsOpen: false, isAntagonistsOpen: false, isOthersOpen: false };

    $scope.protaganists = [];

    $scope.NewCharacterName = '';
    $scope.NewCharacterDescription = '';
    $scope.NewCharacterSelectedType = { value: 1, name: 'Protagonist' };
    $scope.AddCharacter = doAddCharacter;

    doLoadCharacters();

    function doAddCharacter() {
        var character = {
            name: $scope.NewCharacterName,
            description: $scope.NewCharacterDescription,
            typeId: $scope.NewCharacterSelectedType,
            projectId: $scope.ProjectId
        };

        $http.post('api/charactersummary', character)
            .success(function(data, status) {
                doLoadCharacters();
            })
            .error(function (data, status) {

            });
    };

    function doLoadCharacters() {
        $http.get('api/charactersummary?id=' + $scope.ProjectId)
            .success(function(data, status) {
                $scope.protaganists = data.filter(filterProtagonists);
                $scope.antagonists = data.filter(filterAntagonists);
                $scope.others = data.filter(filterOthers);

            })
            .error(function(data, status, headers, config) {

            });
    };

    function filterProtagonists(character) {
        return character.TypeId === 1;
    };

    function filterAntagonists(character) {
        return character.TypeId === 2;
    };

    function filterOthers(character) {
        return character.TypeId === 3;
    };

});