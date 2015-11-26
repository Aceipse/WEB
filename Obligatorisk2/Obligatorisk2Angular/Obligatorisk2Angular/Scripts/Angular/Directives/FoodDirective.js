(function() {
 /*
 * @desc
 * @example
 */

    "use strict";

    angular.module("app").directive("FoodDirective", FoodDirective);

    function FoodDirective() {
        var directive = {
            restrict: "EA",
            templateUrl: 'Views/Home/FoodView.html',
            controller: HomeController,
            controllerAs: "vm",
            bindToController: true
        };
        return directive;
    }
})();
