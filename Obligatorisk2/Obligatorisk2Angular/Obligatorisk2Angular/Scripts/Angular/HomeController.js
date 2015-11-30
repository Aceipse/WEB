(function() {
    'use strict';
    angular.module("app").controller("HomeController", HomeController);

    HomeController.$inject = ['dataservice'];

    function HomeController(dataservice) {
        var vm = this;
        vm.FoodTypes = [];
        vm.Users = [];
        vm.HealthStates=[
        { value: "Normal" },
        { value: "Sick" },
        { value: "Fit" }
    ];
        vm.CurrentUser = {};
        vm.CurrentUsersDailyRequirement = 0;
        vm.UsersLoaded = false;
        vm.UserSelected = false;
        vm.FoodTypesLoaded = false;
        vm.ReachedProteinIntake = false;
        vm.ShowHistory = false;
        vm.SelectedFoodList = [];
        vm.calcRequiredProtein = calcRequiredProtein;
        vm.calculateTotalProtein = calculateTotalProtein;
        vm.addSelectedFood = addSelectedFood;
        vm.setCurrentUser = setCurrentUser;
        vm.createUser = createUser;
        vm.SaveListAndSwapToHistory = SaveListAndSwapToHistory;
        activate();

        ////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////
        function activate() {
            getUsers();
            getFoodTypes();
        }

        function getFoodTypes() {
            return dataservice.getFoodTypes().then(function (response) {
                vm.FoodTypesLoaded = true;
                vm.FoodTypes = JSON.parse(response.data);
                return vm.FoodTypes;
            });
        }
        function getUsers() {
            return dataservice.getUsers().then(function (response) {
                vm.UsersLoaded = true;
                vm.Users = JSON.parse(response.data);
                return vm.Users;
            });
        }
        function createUser(name, weight, healthState) {
            
            dataservice.postUser({
               Name: name,
               Weight:weight,
               HealthState: healthState
           }).then(function (response) {
               setCurrentUser(JSON.parse(response.data));
                return;
            });
        }
        function addSelectedFood(selectedFood) {
            selectedFood.Amount = vm.Amount;
            selectedFood.TotalProtein = ((vm.Amount) / 100) * selectedFood.Protein;
            vm.SelectedFoodList.push(selectedFood);

            if (calculateTotalProtein(vm.SelectedFoodList) > vm.CurrentUsersDailyRequirement)
                vm.ReachedProteinIntake = true;
            else
                vm.ReachedProteinIntake = false;
            
        }
        function setCurrentUser(selectedUser) {
            vm.CurrentUser = selectedUser;
            vm.UserSelected = true;
        }
        function calcRequiredProtein(selectedUser) {
            vm.CurrentUsersDailyRequirement = (function() {
                switch (selectedUser.HealthState) {
                case "Normal":
                    return 0.8 * selectedUser.Weight;
                case "Sick":
                    return 1.5 * selectedUser.Weight;
                case "Fit":
                    return 2 * selectedUser.Weight;
                default:
                    return 100;
                }

            })();
            return vm.CurrentUsersDailyRequirement;                     
        }
        function calculateTotalProtein(foodList) {
            var total = 0;
            for (var i = 0; i < foodList.length; i++) {
                total += foodList[i].TotalProtein;
            }
            return total;
        }
        function AddList() {
            if (vm.CurrentUser.FoodCollections === undefined||vm.CurrentUser.FoodCollections===null) {
                vm.CurrentUser.FoodCollections = [];
            }
            var tempFoodCollection={foods:vm.SelectedFoodList,Date:vm.FoodListDate}
            vm.CurrentUser.FoodCollections.push(tempFoodCollection);

        }
    }
})();