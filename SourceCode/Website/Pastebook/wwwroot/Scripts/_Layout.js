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
        console.log(url);
        window.location = url;
    });

    //account button
    $(window).click(function (e) { 
        // e.preventDefault();
        var target = e.target;
        if(!target.closest(".layout-accountpanel") && !target.closest(".layout-header-right-settings")){
            $(".layout-accountpanel").css("display", "none");
        }
        console.log(target);
        if(!target.closest(".layout-header-left-searchpanel") && !target.closest(".layout-header-left-searchform-input")){
            $(".layout-header-left-searchpanel").css("display", "none");
            $(".layout-header-left-searchform-input").val('');
            $(".layout-header-left-searchpanel-searchresults").empty();
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
    
});

async function SearchUsers(searchTerm){
    // console.log(searchTerm);

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
        console.log(data);
        data.forEach((item,index) => {
            // console.log(item.);
            AddSearchCard(item.profileName,item.profileLink,item.photo);
        });
    }
    
}

async function deleteSession() {  
        fetch(`/login`, {
        method: 'DELETE',
    });
}

function AddSearchCard(profileName,profileLink,photo){
    var searchCard = 
    `<a class="layout-header-left-searchpanel-searchresults-link" href="/${profileLink}">
        <div class="layout-header-left-searchpanel-searchresults-link-searchcard">
            <div class="layout-header-left-searchpanel-searchresults-link-searchcard-picture">
                <img src="${photo}">
            </div>
            <div class="layout-header-left-searchpanel-searchresults-link-searchcard-name">
                ${profileName}
            </div>
        </div>
    </a>`
    $(".layout-header-left-searchpanel-searchresults").append(searchCard);
}


