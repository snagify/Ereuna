var activityModule = (function($) {

    var getLatestActivities = function(formatItem) {
        $.getJSON('api/activity')
            .done(function(data) {
                    $.each(data, function(key, item) {
                        formatItem(key, item);
                    });
                }
            );
    };

    var formatActivityAsList = function(key, item) {
        $('<dt>', { text: item.Occurred }).appendTo($('#recentActivityList'));
        $('<dd>', { text: item.Title }).appendTo($('#recentActivityList'));

    }

    return {
        getLatestActivities: getLatestActivities,
        formatActivityAsList: formatActivityAsList
    };

})($);