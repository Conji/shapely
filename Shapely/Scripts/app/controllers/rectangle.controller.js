angular.module('shapely').controller('RectangleController', ['$rootScope', '$scope', 'rectangleFactory', function ($rootScope, $scope, rectangleFactory) {
    $scope.allRectangles = [];
    $scope.model = null;
    $scope.errors = [];

    function initialize() {
        rectangleFactory.getAll().then(function (result) {
            $scope.allRectangles = result;
        }, function (err) {
            $scope.errors.push(err);
        });
    }

    $scope.addRectangle = function (color) {
        rectangleFactory.post({ Color: color }).then(function (result) {
            $scope.allRectangles.push(result);
        }, function (err) {
            $scope.errors.push(err);
        });
    }

    $scope.removeRectangle = function (rectangle) {
        if (!rectangle) return;
        rectangleFactory.delete(rectangle.Id).then(function (result) {
            $scope.allRectangles.splice($scope.allRectangles.indexOf(rectangle), 1);
        }, function (err) {
            $scope.errors.push(err);
        });
    }

    $scope.updateRectangle = function (rectangle) {
        if (!rectangle) return;
        rectangleFactory.put(rectangle).then(function (result) {
            for (var i = 0; i < $scope.allRectangles.length; i++) {
                var r = $scope.allRectangles[i];
                if (!$scope.allRectangles[i].Id == rectangle.Id) continue;
                $scope.allRectangles[i] = rectangle;
                break;
            }
        }, function (err) {
            $scope.errors.push(err);
        });
    }

    initialize();
}]);