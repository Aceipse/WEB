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
            templateUrl: '/templates/UserView.html',
            controller: "HomeController",
            controllerAs: "vm",
            bindToController: true
        };
        return directive;
    }
})();
