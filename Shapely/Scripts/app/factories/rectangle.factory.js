angular.module('shapely').factory('rectangleFactory', ['$q', '$http', function ($q, $http) {
    return {
        get: function (id) {
            return $http.get('/api/rectangle/' + id);
        },
        getAll: function (params) {
            if (!params) params = { filter: '', skip: 0, take: 0, orderBy: '' }
            return $http.get('/api/rectangle/', { params: params });
        },
        post: function (data) {
            return $http.post('/api/rectangle/', data);
        },
        put: function (data) {
            return $http.put('/api/rectangle/', data);
        },
        delete: function (id) {
            return $http.delete('/api/rectangle/' + id);
        }
    };
}]);