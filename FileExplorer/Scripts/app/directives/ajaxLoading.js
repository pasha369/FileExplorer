/**
 * Loader start on ajax request. 
 * Stop on responce callback.
 */  
app.directive('loading', function () {
    return {
        restrict: 'E',
        replace:true,
        template: '<div class="loading-spiner-holder" data-loading>'
                        +'<div class="loading-spiner"><img src="Images/loading.gif"/></div>'
                 +'</div>',
        link: function (scope, element, attr) {
            scope.$watch('loading', function (val) {
                if (val)
                    $(element).show();
                else
                    $(element).hide();
            });
        }
    }
});
