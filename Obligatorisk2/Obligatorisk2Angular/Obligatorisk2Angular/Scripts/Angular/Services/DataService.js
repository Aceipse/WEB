(function() {

    "use strict";
    angular.module("app").factory("dataservice", DataService);

    DataService.$inject = ['$http'];

    function DataService($http) {
        var restHostName = "http://localhost:12345/";
        //var restHostName = "http://proteinrest.azurewebsites.net/";

        var service = {
            //Food
            getFoodTypes: getFoodTypes,/*
            getFoodType: getFoodType,
            postFoodType: postFoodType,
            patchFoodType: patchFoodType,
            deleteFoodType: deleteFoodType,
            //Users
            */getUsers: getUsers,/*
            getUser: getUser,
            */postUser: postUser/*,
            patchUser: patchUser,
            deleteUser: deleteUser*/
        }

        return service;

        function getFoodTypes() {
            return $http.get(restHostName + "api/Food");
        }
        function getUsers() {
            return $http.get(restHostName + "api/User");
        }
        function postUser(user) {
            var stringified = JSON.stringify(user);
            return $http.post(restHostName + "api/User", "'"+ JSON.stringify(user) +"'");
        }
    }

})();