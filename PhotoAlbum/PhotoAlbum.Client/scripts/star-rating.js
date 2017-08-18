function set_votes(widget) {
    var avg = Math.round($(widget).data('rate'));

    if (avg > 0 && $(widget).hasClass('star-enabled')) {
        $(widget).removeClass('star-enabled').addClass('star-disable');
    } 

    $(widget).find('.star_' + avg).prevAll().removeClass('glyphicon-star-empty').addClass('glyphicon-star');
    $(widget).find('.star_' + avg).removeClass('glyphicon-star-empty').addClass('glyphicon-star');
    $(widget).find('.star_' + avg).nextAll().removeClass('glyphicon-star').addClass('glyphicon-star-empty');
}