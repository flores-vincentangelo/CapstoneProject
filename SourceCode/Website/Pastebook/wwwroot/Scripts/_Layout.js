$(document).ready(function () {

    //search bar functionality
    $(".layout-header-left-searchform-input").focus(function (e) { 
        $(".layout-header-left-searchpanel").css("display", "block");
        $(".layout-header-left-searchpanel-recent").css("display", "block");
    });

    $(".layout-header-left-searchform-input").keyup(function (e) { 
        $(".layout-header-left-searchpanel-searchresults").empty();
        var searchTerm = $(this).val();
        $(".layout-header-left-searchpanel-recent").css("display", "none");
        SearchUsers(searchTerm);
    });

    $(".profilelink").on("click", function () {
        var url = $(this).attr("href");
        window.location = url;
    });

    //account button
    $(window).click(function (e) { 
        var target = e.target;
        if(!target.closest(".layout-accountpanel") && !target.closest(".layout-header-right-settings")){
            $(".layout-accountpanel").css("display", "none");
        }
        if(!target.closest(".layout-header-left-searchpanel") && !target.closest(".layout-header-left-searchform-input")){
            $(".layout-header-left-searchpanel").css("display", "none");
            $(".layout-header-left-searchform-input").val('');
            $(".layout-header-left-searchpanel-searchresults").empty();
        }
        if(!target.closest(".layout-header-right-notificationscontainer") && !target.closest(".layout-header-right-notifications")){
            $(".layout-header-right-notificationscontainer").css("display", "none");
        }
        
    });

    $(".layout-header-right-settings").click(function (e) { 
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
        document.cookie = "profilelink=; expires=Thu, 01 Jan 1970 00:00:00 UTC;";
    });

    $(".layout-header-right-notifications").click(function (e) { 
        $(".layout-header-right-notificationscontainer").css("display", "flex");
        GetNotifications();
    });
    
});

async function deleteSession() {  
        fetch(`/login`, {
        method: 'DELETE',
    });
}

async function SearchUsers(searchTerm){

    if(!searchTerm){
        $(".layout-header-left-searchpanel-searchresults").html("No Results");
        return;
    }
    
    const url = `/search/${searchTerm}`;
    const response = await fetch(url, {
        method: "GET"
    });
    var data = await response.json();
    if(data.length == 0){
        $(".layout-header-left-searchpanel-searchresults").html("No Results");
    } else {
        data.forEach((item,index) => {
            AddSearchCard(item.firstName,item.lastName,item.profileLink,item.photo);
        });
    }
    
}

function AddSearchCard(firstName,lastName,profileLink,photo){
    var searchCard = 
    `<a class="layout-header-left-searchpanel-searchresults-link" href="/${profileLink}">
        <div class="layout-header-left-searchpanel-searchresults-link-searchcard">
            <div class="layout-header-left-searchpanel-searchresults-link-searchcard-picture">
                <img src="${photo}">
            </div>
            <div class="layout-header-left-searchpanel-searchresults-link-searchcard-name">
                ${firstName} ${lastName}
            </div>
        </div>
    </a>`
    $(".layout-header-left-searchpanel-searchresults").append(searchCard);
}

async function GetNotifications(){
    const response = await fetch("/notifications",{
        method: "GET"
    });
    if(response.ok){
        var data = await response.json();
        console.log(data);
        data.friendReq.forEach((item,index) =>{
            AddNotifCard(item.firstName, item.lastName, item.photo, "friend");
        });
        data.likers.forEach((item,index) =>{
            AddNotifCard(item.firstName, item.lastName, item.photo, "likes");
        });
        data.commenters.forEach((item,index) =>{
            AddNotifCard(item.firstName, item.lastName, item.photo, "comments");
        });
    }
}

function AddNotifCard(firstName, lastName, photo, category){
    var notifText;
    switch (category) {
        case "friend":
            notifText = "sent you a friend request";
            break;
        case "likes":
            notifText = "liked you post";
            break;
        case "comments":
            notifText = "commented on your post";
            break;
    }
    
    var notifCard = 
    `<div class="layout-header-right-notificationscontainer-notifcard">
        <div class="layout-header-right-notificationscontainer-notifcard-picture">
            <img src="${photo}">
        </div>
        <div class="layout-header-right-notificationscontainer-notifcard-text">
            ${firstName} ${lastName} ${notifText}
        </div>
    </div>`;
    $(".layout-header-right-notificationscontainer").append(notifCard);
}