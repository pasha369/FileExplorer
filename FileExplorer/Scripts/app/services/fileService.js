app.factory('fileService', ['$http', function ($http) {

    var fileService = {};
    var urlBase = '/api/File/';

    fileService.init = function () {
        return $http.get(urlBase + 'GetRootDirs');
    };

    fileService.getFiles = function (path) {
        return $http.get(urlBase + 'GetDirSub', { params: { path: path } });
    };

    return fileService;
}]);