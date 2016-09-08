angular.module('ProjectTypesService', [])
  .provider('$projectTypes', function() {

       function GetProjectTypes() {
          var types = [
              { id: 1, name: 'Book/Series' },
              { id: 2, name: 'Script' },
              { id: 3, name: 'Comic' }
          ];
          return types;
      };


      this.$get = function () {
          var $projectTypes = GetProjectTypes();
          return $projectTypes;
      };

  })
;