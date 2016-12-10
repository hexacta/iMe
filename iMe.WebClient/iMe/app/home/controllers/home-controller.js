(function () {
  'use strict';

  /**
   * @ngdoc object
   * @name home.controller:HomeCtrl
   *
   * @description
   *
   */
  angular
    .module('home')
    .controller('HomeCtrl', HomeCtrl);

  HomeCtrl.$inject = ['Ime','$filter'];

  function HomeCtrl(Ime, filter) {
    var vm = this;
    vm.ctrlName = 'HomeCtrl';

    vm.sourceOptions = [{
      value: 'twitter',
      name: 'Twitter',
      sourceOptionImgUrl: 'http://www.twitter.com/favicon.ico',
      selected : false
    },
    {
      value: 'github',
      name: 'GitHub',
      sourceOptionImgUrl: 'http://www.github.com/favicon.ico',
      selected : false
    }];

    vm.SearchUser = function searchUser(username) {
      var selected = filter('filter')(vm.sourceOptions,{'selected' : 'true'});
      var source;
      if (selected.length==2)
        source = 'broadcast';
      else
        source = selected[0].value; // TODO validar en front end que haya un checkbox seleccionado y que el textbox no este vacio
            
       Ime.searchUser(source, username)
         .then(onSuccess, onError);
    };

    function onError(response) {
      vm.status = response.error;
    }

    function onSuccess(data) {
      vm.PersonalInfo = data;
    }
  }
} ());
