$(document).ready(function () {

    //search bar functionality
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

    //account button
    $(window).click(function (e) { 
        // e.preventDefault();
        $(".layout-accountpanel").css("display", "none");
    });

    $(".layout-header-right-settings").click(function (e) { 
        // e.preventDefault();
        e.stopPropagation();
        $(".layout-accountpanel").css("display", "block");
    });

    //logout button
    $(".layout-accountpanel-logout").click(() => {
        window.location.replace("/login");
        //delete sessions
        deleteSession();
        //delete cookies
        document.cookie = "email=; expires=Thu, 01 Jan 1970 00:00:00 UTC;";
        document.cookie = "sessionId=; expires=Thu, 01 Jan 1970 00:00:00 UTC;";
    })
    
});

async function deleteSession() {  
        fetch(`/login`, {
        method: 'DELETE',
    });
}


