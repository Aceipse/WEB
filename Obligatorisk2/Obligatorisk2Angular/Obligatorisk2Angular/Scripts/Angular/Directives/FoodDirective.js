(function() {
 /*
 * @desc
 * @example
 */

    "use strict";

    angular.module("app").directive("food", FoodDirective);

    function FoodDirective() {
        var directive = {
            restrict: "EA",
            templateUrl: '/templates/FoodView.html'
            //controller: "HomeController", -this already exists
            //controllerAs: "vm",
            //bindToController: true
        };


        return directive;

    }
    
})();
