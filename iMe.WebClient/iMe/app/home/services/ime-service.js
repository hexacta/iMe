(function () {
  'use strict';

  /**
   * @ngdoc service
   * @name home.service:Ime
   *
   * @description
   *
   */
  angular
    .module('home')
    .service('Ime', Ime);

  Ime.$inject = ['$http'];

  function Ime($http) {
    var self = this;
    self.searchUser = function (source, username) {
      return $http.get('http://localhost:62294/getpersonalinfo/' + source + '/' + username)
        .then(function (response) {
          return response.data;
        });
    };

    self.get = function () {
      return 'Ime';
    };
  }
} ());
