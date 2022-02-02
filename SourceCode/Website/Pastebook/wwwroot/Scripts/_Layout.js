$(document).ready(function () {

    GetNotifications();
    GetUserDetails();
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
        //delete sessions
        deleteSession();
        window.location.replace("/login");
    });

    $(".layout-header-right-notifications").click(function (e) { 
        $(".layout-header-right-notificationscontainer").css("display", "flex");
    });

    $(".layout-header-right-notificationscontainer-clear").click(function (e) { 
        DeleteNotifications();
    });
    
});

async function deleteSession() {  
    const response = await fetch(`/login`, {
        method: 'DELETE',
    });
    location.reload();
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

        if(data.friendReq != null){
            $(".layout-header-right-notifications").css("background-color", "red");
            data.friendReq.forEach((item,index) =>{
                AddNotifCard(item.firstName, item.lastName, item.photo, "sent you a friend request");
            });
        }

        if(data.likers != null){
            $(".layout-header-right-notifications").css("background-color", "red");
            data.likers.forEach((item,index) =>{
                AddNotifCard(item.firstName, item.lastName, item.photo, "liked your post");
            });
        }

        if(data.commenters != null){
            $(".layout-header-right-notifications").css("background-color", "red");
            data.commenters.forEach((item,index) =>{
                AddNotifCard(item.firstName, item.lastName, item.photo, "commented on your post");
            });
        }
    }
}

function AddNotifCard(firstName, lastName, photo, text){
    var notifCard = 
    `<div class="layout-header-right-notificationscontainer-notifcard">
        <div class="layout-header-right-notificationscontainer-notifcard-picture">
            <img src="${photo}">
        </div>
        <div class="layout-header-right-notificationscontainer-notifcard-text">
            ${firstName} ${lastName} ${text}
        </div>
    </div>`;
    $(".layout-header-right-notificationscontainer").append(notifCard);
}

async function DeleteNotifications(){
    const response = await fetch("/notifications",{
        method:"DELETE"
    });
    if(response.ok){
        location.reload();
    }
}

async function GetUserDetails(){
    const response = await fetch(`/user`, {
        method: "GET",
        headers: {
            'Content-Type': 'application/json'
        },
    });
    if(response.status == 200) {
        var data = await response.json();
        AddUserCardOnLogoutPanel(data.firstName, data.lastName, data.photo, data.profileLink);
        AddUserCardonLayoutPanel(data.photo, data.profileLink);
    }
}

function AddUserCardOnLogoutPanel(firstName, lastName, photo, profileLink){
    var userCard = 
    `<div class="layout-accountpanel-user">
        <img class="layout-accountpanel-user-photo" src="${photo}">
        <span class="layout-accountpanel-user-details"><span class="layout-accountpanel-user-fullname">${firstName} ${lastName}</span><br><span class="layout-accountpanel-user-profile"><a href="${profileLink}")>See your profile</a></span></span>
    </div>`
    $(".layout-accountpanel-container").append(userCard);
}

function AddUserCardonLayoutPanel(photo, profileLink){
    var userPhoto = 
    `<a href="${profileLink}"><img class="layout-header-right-photo" src="${photo}"></a>`
    $(".layout-header-right-user").append(userPhoto);
}