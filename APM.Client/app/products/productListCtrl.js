(function () {
    "use strict";

    function productListCtrl(productResource) {
        var vm = this;
        
        vm.currencySymbol = "$";

        vm.searchCriteria = "GDN"; // could be bound to a user input on the front end

        vm.LoadData = function () {
            productResource.query(function (data) {
                vm.products = data;
            })
        };
        vm.LoadData();

        // Convert list items currency
        vm.convertCurrency = function () {
            console.log("converting");
            if (vm.currencySymbol == "$") {
                angular.forEach(vm.products, function (product, key) {
                    // Dollars to Euros
                    product.price = product.price * 0.9;
                })
                vm.currencySymbol = "\u20ac"
            }
            else {
                // Euros to Dollars by reloading data
                vm.LoadData();
                vm.currencySymbol = "$"
            };
        };
    }

    angular
        .module("productManagement")
        .controller("ProductListCtrl",
                     productListCtrl,
                     ["productResource",
                         productListCtrl]);
}());
