(function() {
    'use strict';
    angular.module("app").controller("HomeController", HomeController);

    HomeController.$inject = ['dataservice'];

    function HomeController(dataservice) {
        var vm = this;
        vm.Name = "It works";
        vm.FoodTypes = [];
        vm.SelectedFoodList = [];
        vm.addSelectedFood = addSelectedFood;
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
        function addSelectedFood(selectedFood) {
            selectedFood.Amount = vm.Amount;
            selectedFood.TotalProtein = ((vm.Amount) / 100) * selectedFood.Protein;
            vm.SelectedFoodList.push(selectedFood);
        }

    }
})();