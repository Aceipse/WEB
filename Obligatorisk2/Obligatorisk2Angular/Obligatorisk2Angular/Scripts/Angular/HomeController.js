(function() {
    'use strict';
    angular.module("app").controller("HomeController", HomeController);

    HomeController.$inject = ['dataservice'];

    function HomeController(dataservice) {
        var vm = this;
        vm.Name = "It works";
        vm.FoodTypes = [];

        activate();

        ////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////
        function activate() {
            return getFoodTypes();
        }

        function getFoodTypes() {
            return dataservice.getFoodTypes().then(function(response) {
                vm.FoodTypes = JSON.parse(response.data);
                return vm.FoodTypes;
            });
        }
    }
})();