var app = angular.module('AppInstaPics', ['ngRoute']);


app.directive('file', function () {
    return {
        scope: {
            file: '='
        },
        link: function (scope, el, attrs) {
            el.bind('change', function (event) {
                var files = event.target.files;
                var file = files[0];
                //scope.file = file ? file.name : undefined;
                scope.file = file;
                scope.$apply();
            });
        }
    };
});

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


//pas utilisé car problème lors de l'nvoie des fichiers en angularjs
/*app.controller('AccueilController', function ($scope, $http, $rootScope, Config) {
    $scope.upload = function () {
        $http({
            method: 'POST',
            url: Config.ActionUpload,
            headers : { 'enctype' : 'multipart/form-data'},
            data: { file: $scope.file }
        }).then(function successCallback(response) {
            console.log(response);
        }, function errorCallback(data) {
            $scope.resultUpload = "Une erreur est survenu lors de l'upload de l'image";
        });
    }
});*/