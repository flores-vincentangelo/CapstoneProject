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

});