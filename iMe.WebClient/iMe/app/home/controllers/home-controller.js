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

  HomeCtrl.$inject = ['Ime'];

  function HomeCtrl(Ime) {
    var vm = this;
    vm.ctrlName = 'HomeCtrl';

    vm.sourceOptions = [{
      value: 'twitter',
      name: 'Twitter',
      sourceOptionImg: 'http://www.twitter.com/favicon.ico'
    }

    ];

    vm.SearchUser = function searchUser(source, username) {
      Ime.searchUser(source, username)
        .then(onSuccess, onError);

    };

    function onError(response) {
      vm.status = response.error;

    };

    function onSuccess(data) {
      vm.PersonalInfo = data;
    }
  }
} ());