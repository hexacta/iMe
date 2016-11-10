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
    }];

    vm.gridOptions = {
    columnDefs: [{ field: 'UserName', displayName: 'User Name', width: 100, cellTemplate: '<div class="ui-grid-cell-contents" >{{grid.getCellValue(row, col)}}</div>' },
    { field: 'UserId', displayName: 'User Id', width: 100, cellTemplate: '<div class="ui-grid-cell-contents" >{{grid.getCellValue(row, col)}}</div>' },
    { field: 'ProfilePicUrl', displayName: 'Profile Pic', width: 100, cellTemplate: '<div class="ui-grid-cell-contents" >{{grid.getCellValue(row, col)}}</div>' },
    { field: 'Bio', displayName: 'Short Bio', width: 100, cellTemplate: '<div class="ui-grid-cell-contents" >{{grid.getCellValue(row, col)}}</div>' }]
  };

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