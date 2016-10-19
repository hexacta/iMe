/* global describe, beforeEach, it, expect, inject, module */
'use strict';

describe('Ime', function () {
  var service;

  beforeEach(module('home'));

  beforeEach(inject(function (Ime) {
    service = Ime;
  }));

  it('should equal Ime', function () {
    expect(service.get()).toEqual('Ime');
  });
});
