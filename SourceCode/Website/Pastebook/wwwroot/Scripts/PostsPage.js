 $(document).ready(function () {
    var modelObj = JSON.parse(model.replace(/&quot;/g,"\""));
    editPost();
    deletePost(modelObj);
    modifyPhoto();
    openCommentModal();

    //When the user clicks on the "X/Close" button,
    $('.post-container-right-action-close').click(() => {
        history.back();
    });

    //When the user clicks on "Update" button,
    $('.user-button-update').click((event) => {
        event.preventDefault();           
        //Save value of caption and photo to database
        var caption=$('#caption-update').val();
        var imageSrc=$('#modal-container-photo-img-update').attr('src');

        if (!caption && imageSrc) {
            const formData = {
                Photo: imageSrc,
                Caption: ""
            }
            var data = JSON.stringify(formData);
            addPostModification(event, data, modelObj.PostId);
        }

        else if (imageSrc && caption) {
            const formData = {
                Photo: imageSrc,
                Caption: caption
            }
            var data = JSON.stringify(formData);
            addPostModification(event, data, modelObj.PostId);
        }
        
        //close modal
        $('#post-modal-container-update').css("display", "none");
        location.reload();
    });

    //When a user liked a post
    $(".post-container-right-like").click(function (e) {
        var postId = $(this).attr("id");
        console.log(postId);
        LikedPost(postId);
    });

    //When a user unliked a post
    $(".post-container-right-unlike").click(function (e) {
        var postId = $(this).attr("id");
        console.log(postId);
        UnlikedPost(postId);
    });

});

function modifyPhoto() { 

    // When the user clicks on the "Choose file" button
    $("#myPhotoPost-update").change(function() {
        if (this.files && this.files[0] && this.files[0].type.includes("image")) { 
            var reader = new FileReader();
            reader.readAsDataURL(this.files[0]);
            reader.onload = imageIsLoaded;
        }
        else {
            alert("This is not valid image file");
        }
    });

    //Show preview of uploaded photo
    function imageIsLoaded(e) {
        result = e.target.result;
        $('#modal-container-photo-img-update').attr('src', result);
        return result;
    };
}

async function addPostModification(event, jsonData, postId) {
    console.log(jsonData);
    console.log(postId);
    event.preventDefault();
    const response = await fetch(`/posts/${postId}`, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: jsonData
    });
}

function editPost() {

    //When the user clicks on the "Settings" button,
    $('.update-but').click(() => {
        //show Edit Post Modal
        $('#post-modal-container-update').css("display", "flex");
    });

    //When the user clicks on the "x",
    $('#modal-container-close-update').click(() => {
        //close Create Post Modal
        $('#post-modal-container-update').css("display", "none");
    });

}


function deletePost(modelObj) {

    console.log(modelObj);

    //When the user clicks on the "X" button,
    $('.delete-but').click(() => {
        //show Delete Post Modal
        $('#post-modal-container-delete').css("display", "flex");
    });
    
    //When the user clicks on the "Yes" button,
    $('#yes-btn').click(() => {
        //Delete photo if it exists in album
        if(modelObj.PhotoId != 0) {
            deletePhoto(modelObj.PhotoId);
        }

        //Deletes the entire post
        deletePostById(modelObj.PostId, modelObj.ProfileLink);
    });

    //When the user clicks on the "Cancel" button,
    $('#cancel-btn').click(() => {
        //show Delete Post Modal
        $('#post-modal-container-delete').css("display", "none");
    });

    //When the user clicks on the "x" button,
    $('#modal-container-close-delete').click(() => {
        //show Delete Post Modal
        $('#post-modal-container-delete').css("display", "none");
    });

}

async function deletePhoto(photoId) {
    const url = `/photos/${photoId}`;
    const response = await fetch(url, {
        method: 'DELETE'
    });
    if(response.status == 200) {
        console.log("Deleted Photo Id", photoId);
    }
}

function openCommentModal() {
    $('.post-container-right-comment').click(function() {
        var postId = $(this).attr("id");
        console.log(postId);
        // show edit form 
        $(`#post-modal-container-comment-${postId}`).css("display", "flex");
    });

    // When the user clicks on the "x",
    $('#modal-container-close-comment').click(() => {
        // Close modal
        $('.post-modal-container-comment').css("display", "none");
    });
}

async function deletePostById(postId, profileLink) {

    const response = await fetch(`/posts/${postId}`, {
        method: 'DELETE',
    });
    if (response.status == 200) {
        location.replace(`/${profileLink}`)
    }
}

function submitAddComment(e){
    e.preventDefault();
    var postId = $(e.target).attr("id");
    const formData = new FormData(e.target);
    const formDataObj = Object.fromEntries(formData.entries());
    // console.log(e.target);
    // console.log(postId);
    // console.log(formDataObj);
    const jsonObj = {
        PostId: postId,
        CommentText: formDataObj.comment
    }
    SendCommentToController(jsonObj)
}

async function SendCommentToController(jsonObj){
    const response = await fetch("/comments", {
        method: "POST",
        headers: {
            "content-Type": "application/json"
        },
        body: JSON.stringify(jsonObj)
    });
    if(response.ok){
        // alert("Comment Added");
        $('.post-modal-container-comment').css("display", "none");
        location.reload();
    }
}
async function LikedPost(postId) {
    const response = await fetch(`/likes/${postId}`, {
        method: "PATCH"
    });
    if(response.ok){
        location.reload();
    }
}

async function UnlikedPost(postId) {
    const response = await fetch(`/unlike/${postId}`, {
        method: "PATCH"
    });
    if(response.ok){
        location.reload();
    }
}

async function GetLikers(postId) {
    const response = await fetch(`/likes/${postId}`, {
        method: "GET"
    });
    if (response.ok) {
        var data = await response.json();
        $(".modal-container-title-likers").empty();
        if (data !=null)
        { 
            data.forEach((item,index) =>{
                AddLikersToPage(item.photo, item.firstName, item.lastName);
            });
        }
        else {
            var noData = "<div>No likers</div>"
            $(".modal-container-title-likers").append(noData);
        }
    }
}

function AddLikersToPage(photo, firstName, lastName) {
    var likersModal = 
    `<div class="modal-container-likers">
        <img class="modal-container-likers-photo" src="${photo}">
        <p id="modal-container-likers-name">${firstName} ${lastName}</p>       
    </div>`;
    $(".modal-container-title-likers").append(likersModal);
}

function openLikeModal(postId) {
    //Show list of liker/s on a post
        $('#post-modal-container-likers').css("display", "flex");
        console.log(postId);
        GetLikers(postId);
    // When the user clicks on the "x",
    $('#modal-container-close-likers').click(() => {
        // Close modal
        $('#post-modal-container-likers').css("display", "none");
    });
}