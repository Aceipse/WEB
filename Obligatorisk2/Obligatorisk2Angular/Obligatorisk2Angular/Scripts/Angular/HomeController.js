(function() {
    'use strict';
    angular.module("app").controller("HomeController", HomeController);

    HomeController.$inject = ['dataservice','$filter'];

    function HomeController(dataservice,$filter) {
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
        vm.UpdateChartValues = updateChartValues;
        vm.calcRequiredProtein = calcRequiredProtein;
        vm.calculateTotalProtein = calculateTotalProtein;
        vm.addSelectedFood = addSelectedFood;
        vm.setCurrentUser = setCurrentUser;
        vm.createUser = createUser;
        vm.AddList = AddList;
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
            //Fixed bug that lists contained same food
            var temp = angular.copy(selectedFood);
            temp.Amount = angular.copy(vm.Amount);
            temp.TotalProtein = ((vm.Amount) / 100) * temp.Protein;
            
            vm.SelectedFoodList.push(temp);

            if (calculateTotalProtein(vm.SelectedFoodList) > vm.CurrentUsersDailyRequirement)
                vm.ReachedProteinIntake = true;
            else
                vm.ReachedProteinIntake = false;
            
        }
        function setCurrentUser(selectedUser) {
            vm.CurrentUser = selectedUser;
            vm.UserSelected = true;
            vm.UpdateChartValues();
        }
        function calcRequiredProtein(selectedUser) {
           return vm.CurrentUsersDailyRequirement = (function() {
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
        }
        function calculateTotalProtein(foodList) {
            var total = 0;
            for (var i = 0; i < foodList.length; i++) {
               
                if (foodList[i].TotalProtein === undefined) {
                    total += (foodList[i].Protein * foodList[i].Amount) / 100;
                } else {
                    total += foodList[i].TotalProtein;
                }
            }
            return total;
        }
        function AddList() {
            if (vm.CurrentUser.FoodCollections === undefined||vm.CurrentUser.FoodCollections===null) {
                vm.CurrentUser.FoodCollections = [];
            }
            var tempFoodCollection = { Foods: vm.SelectedFoodList, Date: vm.FoodListDate }
            //exclude non needed propteries
            for (var i = 0; i < tempFoodCollection.Foods.length; i++) {
                tempFoodCollection.Foods[i].TotalProtein = undefined;
            }
            tempFoodCollection.TotalProtein = undefined;
            vm.CurrentUser.FoodCollections.push(tempFoodCollection);
            vm.UpdateChartValues();
            vm.SelectedFoodList = [];
            vm.selectedFood = [];
            dataservice.patchUser(vm.CurrentUser);
        }
        function updateChartValues() {
            if (vm.CurrentUser.FoodCollections === null)
                return;
            var tempLabels=[];
            var tempData = [[], []];
            //ensretning så date står på samme måde i alle objekter
            for (var i = 0; i < vm.CurrentUser.FoodCollections.length; i++) {
                if (vm.CurrentUser.FoodCollections[i].Date.$date !== undefined) {
                    vm.CurrentUser.FoodCollections[i].Date = new Date(vm.CurrentUser.FoodCollections[i].Date.$date);
                }
            }
            //OrderBy date
            var orderedLists = $filter('orderBy')(vm.CurrentUser.FoodCollections, 'Date', false);
            for (var i = 0; i < orderedLists.length; i++) {
                if (orderedLists[i].Date.$date === undefined) {
                    var date = new Date(orderedLists[i].Date);
                    tempLabels.push((date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear());
                } else {
                    var date = new Date(orderedLists[i].Date.$date);
                    tempLabels.push((date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear());
                }
                
                tempData[0].push(calculateTotalProtein(orderedLists[i].Foods));
                tempData[1].push(calcRequiredProtein(vm.CurrentUser));
            }

            vm.labels = tempLabels;
            vm.series = ['Obtained', 'Goal'];
            vm.data = tempData;
        }
    }
})();



