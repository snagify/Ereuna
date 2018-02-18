
app.controller('CharactersController', function ($rootScope, $scope, $http, $stateParams, $facebook, $window, $location) {
    $scope.ProjectId = $stateParams.projectId;

    $scope.characterAccordionStatus = { isProtagonistsOpen: false, isAntagonistsOpen: false, isOthersOpen: false };

    $scope.protaganists = [];

    $scope.NewCharacterName = '';
    $scope.NewCharacterDescription = '';
    $scope.NewCharacterSelectedType = { value: 1, name: 'Protagonist' };
    $scope.AddCharacter = doAddCharacter;

    $scope.SelectedCharacterId = $stateParams.characterid;
    $scope.IsSelectedCharacter = function(id) {
        var result = $scope.SelectedCharacterId == id;
        return result;
    };
    $scope.IsCharacterSelected = function() {
        return ($scope.SelectedCharacterId > 0);
    };

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
                $scope.allCharacters = data;
                $scope.protaganists = data.filter(filterProtagonists);
                $scope.antagonists = data.filter(filterAntagonists);
                $scope.others = data.filter(filterOthers);

                ExpandAccordianToSelectedCharacter();
            })
            .error(function(data, status, headers, config) {

            });
    };

    function ExpandAccordianToSelectedCharacter() {
        if ($scope.SelectedCharacterId > 0) {
            var matches = $scope.allCharacters.filter(filterSelectedCharacter);
            var character = matches[0];

            if (character.TypeId === 1) $scope.characterAccordionStatus.isProtagonistsOpen = true;
            if (character.TypeId === 2) $scope.characterAccordionStatus.isAntagonistsOpen = true;
            if (character.TypeId === 3) $scope.characterAccordionStatus.isOthersOpen = true;
        }
    };

    function filterSelectedCharacter(character) {
        return character.Id == $scope.SelectedCharacterId;
    }

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