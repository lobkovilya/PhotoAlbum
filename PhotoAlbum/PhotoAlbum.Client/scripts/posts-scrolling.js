function loadPosts(requestData, postsUrl, appendTo) {
    if (requestData.page >= 0) {

        $.ajax({
            type: 'GET',
            url: postsUrl,
            data: requestData,
            success: function(data) {
                $(appendTo).append(data);

                $('.widget-rating').each(function () {
                    set_votes($(this));
                });

                $('.widget-my-rating').each(function () {
                    set_votes($(this));
                });

                $('.my-rating').hover(ratingMouseOver, ratingMouseOut);
            }
        });
    }
}


function ratingClick() {
    
    var star = this;

    var widget = $(this).parent();

    if ($(widget).data('rate') != 0) {
        return;
    }

    var rateData = {
        PostId: $(widget).data('postid'),
        Mark: $(star).data('rating')
    }

    $.ajax({
        type: 'POST',
        url: "Feed/RatePost",
        data: rateData,
        success: function (mdata) {
            $(widget).prev().data('rate', mdata.Rating);
            set_votes($(widget).prev());
            $(widget).prev().attr("title", mdata.MarksAmount);
            $(widget).data('rate', rateData.Mark);
            set_votes($(widget));
        },
        error: function(xhr, textStatus, errorThrown) {
            if (xhr.status == 403) {
                window.location.replace($("#LoginURL").val());
            }
        }
    });
}


function ratingMouseOver() {
    if ($(this).parent().data('rate') == 0) {
        $(this).prevAll().removeClass('glyphicon-star-empty').addClass('glyphicon-star');
        $(this).removeClass('glyphicon-star-empty').addClass('glyphicon-star');
        $(this).nextAll().removeClass('glyphicon-star').addClass('glyphicon-star-empty');
    }
}

function ratingMouseOut() {
    if ($(this).parent().data('rate') == 0) {
        $(this).prevAll().removeClass('glyphicon-star').addClass('glyphicon-star-empty');
        $(this).removeClass('glyphicon-star').addClass('glyphicon-star-empty');
    }
    else {
        set_votes($(this).parent());
    }
}



