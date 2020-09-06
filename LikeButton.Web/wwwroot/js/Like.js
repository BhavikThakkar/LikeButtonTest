$(function () {
    debugger

    var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:28175/PostHub").build();

    async function start() {
        try {
            await connection.start({ withCredentials: false });
            console.log("connected");
        } catch (err) {
            console.log(err);
            setTimeout(() => start(), 5000);
        }
    };

    connection.onclose(async () => {
        await start();
    });

    // Start the connection.
    start();

    $.get('http://localhost:28175/post/GetList', function (data) {
        $('#result').empty();
        $.each(data.Items, function (i, v) {

            var blog = '<span> ' + v.Title + '  </span><button data-id="'+ v.Id +'" type="button" class="like-button" title="Click to like this post!">               <i class="fa fa-thumbs-up"></i> Like                 | ' +
                '<span class="like-count">' + v.PostLikes +'</span>' +
                '</button> ';
            $('#result').html($('#result').html() + blog + '<br />');
        });

        if ($(".like-button").size() > 0) {
          
            connection.on("updateLikeCount", function (post) {
                debugger
                var counter = $("[data-id=" + post.postId + "] .like-count");
                $(counter).fadeOut(function () {
                    $(this).text(post.likeCount);
                    $(this).fadeIn();
                });
            });
            $(".like-button").on("click", function () {
                var code = $(this).attr("data-id");
                connection.invoke("like", code).catch(function (err) {
                    return console.error(err.toString());
                });
            });
        }
    }, 'json');

});