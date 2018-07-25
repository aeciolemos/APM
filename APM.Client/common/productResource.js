(function() {

    "use strict";

    function productResource($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/products/:id", null,
            {
                'update': { method: 'PUT'}
            });
    }

    function productResourceGet($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/products", null,
            {
                'fetch': {method: 'GET'}
            })
    }

    angular.module("common.services")
        .factory("productResource",
            ["$resource",
            "appSettings",
            productResource]);
}());