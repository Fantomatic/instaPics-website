//déclaration du module d'angular
var app = angular.module('AppInstaPics', ['ngRoute']);

//déclaration des Constantes
//peut être utile si on appelle plusieurs fois la même action
app.constant('Config', {
    ActionLoginHome: 'Home/userConnect'
});

//controller login
app.controller('LoginController', function ($scope, $http, $rootScope, Config) {
//à la soumission du formulaire de login
//appele le controller .NET qui va vérifier/créer le login et redirige vers l'espace client si tout se passe bien
    $scope.login = function () {
        $http({
            method: 'POST',
            url: Config.ActionLoginHome,
            data: { username: $scope.username }
        }).then(function successCallback(response) {
            if(response.data == "error")
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