(function () {
  'use strict';

  angular
    .module('iMe')
    .config(config);

  function config($urlRouterProvider) {
    $urlRouterProvider.otherwise('/home');
  }
}());
