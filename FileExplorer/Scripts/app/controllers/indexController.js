app.controller('indexController', ['$scope', 'fileService', function ($scope, fileService) {

    $scope.fileService = fileService;
    $scope.dirs =  {};
    $scope.currentDir = "";
    $scope.host = "";
    // Init field from model values.
    function getFolder(folder) {
        $scope.dirs = folder.Dirs;
        $scope.files = folder.Files;
        $scope.less10Mb = folder.Less10Mb;
        $scope.mb10Mb50 = folder.Mb10Mb50;
        $scope.more100Mb = folder.More100Mb;
        $scope.currentDir = folder.CurrentDir;
        $scope.host = folder.Host;
    }
    // Initialization.
    function Init() {
        $scope.loading = true;
        $scope.fileService.init()
            .success(function(folder) {
                getFolder(folder);

                $scope.loading = false;
            });
    }
    // Load sub dir by click.
    $scope.getFiles = function (item) {
        $scope.loading = true;
        fileService.getFiles($scope.currentDir + item)
            .success(function (folder) {
                getFolder(folder);

                $scope.loading = false;
            });
    };
    // Call data init function. 
    Init();
}]);