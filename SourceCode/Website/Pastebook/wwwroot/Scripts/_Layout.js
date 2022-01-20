$(document).ready(function () {
    $(".layout-header-left-searchform-input").focus(function (e) { 
        e.preventDefault();       
        $(".layout-header-left-searchpanel").css("display", "block");
        $(".layout-header-left-searchpanel-recent").css("display", "block");
    });

    $(".layout-header-left-searchform-input").blur(function (e) { 
        e.preventDefault();
        $(".layout-header-left-searchpanel").css("display", "none");
        $(this).val('');
        $(".layout-header-left-searchpanel-searchresults").html('');
    });

    $(".layout-header-left-searchform-input").keypress(function (e) { 
        $(".layout-header-left-searchpanel-searchresults").html($(this).val());
        $(".layout-header-left-searchpanel-recent").css("display", "none");
    });

});