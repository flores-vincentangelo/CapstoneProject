$(document).ready(function () {

    //confirm friend request 
    //moves email from friend request to the friendslist column
    $(".friends-main-requests-friendcardcontainer-friendreqcard-content-confirm-button").click(function (e) { 
        var emailToAdd = $(this).attr("id");
        console.log(emailToAdd);
        ConfirmFriendReq(emailToAdd);
    });

    $(".friends-main-requests-friendcardcontainer-friendreqcard-content-delete-button").on("click",function (e) { 
        var emailToDelete = $(this).attr("id");
        console.log(emailToDelete);
        DeleteFriendReq(emailToDelete);
        
    });

    $("#friends-sidepanel-link-all").click(function (e) { 
        e.preventDefault();
        // alert("hi");
        $(".friends-main-requests").css("display", "none");
        $(".friends-main-all").css("display", "block");
    });

    $("#friends-sidepanel-link-reqs").click(function (e) { 
        e.preventDefault();
        $(".friends-main-requests").css("display", "block");
        $(".friends-main-all").css("display", "none");
        
    });
});

async function ConfirmFriendReq(email) { 
    var requestBodyObj = {
        ConfirmFriendReqOf: email
    }
    console.log(requestBodyObj);
    const response = await fetch("/friends/confirm",{
        method: "PATCH",
        headers:{
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestBodyObj)
    });
    if(response.ok){
        location.reload();
    }
}

async function DeleteFriendReq(email){
    var requestBodyObj = {
        DeleteFriendReqOf: email
    }
    const response = await fetch("/friends/delete",{
        method: "PATCH",
        headers:{
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestBodyObj)
    });
    if(response.ok){
        location.reload();
    }
}