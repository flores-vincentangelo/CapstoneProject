$(document).ready(function () {
    // alert("HI");
    // var model = @(Model.UserEmail);

    //confirm friend request 
    //moves email from friend request to the friendslist column
    $(".friends-main-friendcardcontainer-friendcard-content-confirm-button").click(function (e) { 
        var emailToAdd = $(this).attr("id");
        console.log(emailToAdd);
        ConfirmFriendReq(emailToAdd);
    });

    $(".friends-main-friendcardcontainer-friendcard-content-delete-button").click(function (e) { 
        var emailToDelete = $(this).attr("id");
        console.log(emailToDelete);
        DeleteFriendReq(emailToDelete);
        
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