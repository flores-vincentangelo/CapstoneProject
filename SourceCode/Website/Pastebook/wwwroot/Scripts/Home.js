$(document).ready(function () {
    $(".home-header-left-searchform-input").focus(function (e) { 
        e.preventDefault();       
        $(".home-header-left-searchpanel").css("display", "block");
        $(".home-header-left-searchpanel-recent").css("display", "block");
    });

    $(".home-header-left-searchform-input").blur(function (e) { 
        e.preventDefault();
        $(".home-header-left-searchpanel").css("display", "none");
        $(this).val('');
        $(".home-header-left-searchpanel-searchresults").html('');
    });

    $(".home-header-left-searchform-input").keypress(function (e) { 
        $(".home-header-left-searchpanel-searchresults").html($(this).val());
        $(".home-header-left-searchpanel-recent").css("display", "none");
    });

    // Auto refresh
    setInterval(function() {
        console.log("refresh");
        
        $.ajax({
            type: 'GET',
            url: '/',
            success: function () {
                $('.home-timeline-body').load('/ .home-timeline-body');
                // Hide all posts
                $('.profile-post-container-status-post').css("display", "none");
            }
        });
    }, 15000);

    // Hide all posts
    $('.profile-post-container-status-post').css("display", "none");
    showPostsInHome();

    // Auto refresh
    // setInterval(function() {
    //     $.ajax({
    //         type: 'GET',
    //         url: '/',
    //         success: function () {
    //             location.reload();
    //             // console.log("refresh");
    //         }
    //     })
    // }, 60000);



 
   
});

function showPostsInHome() {
    $('.profile-post-container-status-post').slice(0, 10).show().slideDown();

    // listen for scroll event and load more images if we reach the bottom of window
    window.addEventListener('scroll', () => {
        if(window.scrollY + window.innerHeight >= document.documentElement.scrollHeight) {
            // console.log("bottom reached");
            $('.profile-post-container-status-post:hidden').slice(0, 10).show().slideDown();
        }
    });
}