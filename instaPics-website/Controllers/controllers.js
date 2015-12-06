var app = angular.module('AppInstaPics', ['ngRoute']);

app.constant('Config', {
    ActionLoginHome: 'Home/userConnect',
    ActionUpload: 'Accueil/uploadImage'
});

app.controller('LoginController', function ($scope, $http, $rootScope, Config) {
    $scope.login = function () {
        $http({
            method: 'POST',
            url: Config.ActionLoginHome,
            data: { username: $scope.username }
        }).then(function successCallback(response) {
            if(console.log(response.data) == "error")
            {
                $scope.resultLogin = "Une erreur est survenu lors de la connexion à votre compte";
            }
            else
            {
                window.location = "/Accueil";
            }
        }, function errorCallback(data) {
            $scope.resultLogin = "Une erreur est survenu lors de la connexion à votre compte";
        });
    }
});

app.controller('AccueilController', function ($scope, $http, $rootScope, Config) {
    $scope.upload = function () {
        $http({
            method: 'POST',
            url: Config.ActionUpload,
            data: { file: $scope.inputFile }
        }).then(function successCallback(response) {
            console.log(response);
        }, function errorCallback(data) {
            $scope.resultUpload = "Une erreur est survenu lors de l'upload de l'image";
        });
    }
});