(function () {
    /*
    * @desc
    * @example
    */

    "use strict";

    angular.module("app").directive("user", UserDirective);

    function UserDirective() {
        var directive = {
            restrict: "EA",
            templateUrl: '/templates/UserView.html'
            //controller: "HomeController", - this already exists
            //controllerAs: "vm",
            //bindToController: true
        };
        return directive;
    }
})();
