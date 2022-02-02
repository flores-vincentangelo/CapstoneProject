$(document).ready(() => {
    viewAddPostModal();
    addPost();
    resetForm();
    openCommentModal();   
    
    //When the user clicks on "Post" button,
    $('.user-post-button').click((event) => {
        event.preventDefault();           
        //Save value of caption and photo to database
        var caption=$('#caption').val();
        var imageSrc=$('#modal-container-photo-img').attr('src');

        if (!caption && imageSrc) {
            const formData = {
                Photo: imageSrc,
                Caption: ""
            }
            var data = JSON.stringify(formData);
            addPostToProfile(event, data);
        }

        else if (!imageSrc && caption) {
            const formData = {
                Photo: "",
                Caption: caption
            }
            var data = JSON.stringify(formData);
            addPostToProfile(event, data);
        }

        else if (imageSrc && caption) {
            const formData = {
                Photo: imageSrc,
                Caption: caption
            }
            var data = JSON.stringify(formData);
            addPostToProfile(event, data);
        }

        else {
            alert("No post added!");   
        }
        
        //close modal
        $('.post-modal-container').css("display", "none");
        resetForm();
    });

    //When a user liked a post
    $(".post-button-like").click(function (e) {
        var postId = $(this).attr("id");
        console.log(postId);
        LikedPost(postId);
    });

    //When a user unliked a post
    $(".post-button-unlike").click(function (e) {
        var postId = $(this).attr("id");
        console.log(postId);
        UnlikedPost(postId);
    });
    // Hide all posts
    $('.profile-post-container-status-post').css("display", "none");
    showPostsInTimeline();
});

function showPostsInTimeline() {
    $('.profile-post-container-status-post').slice(0, 10).show();

    // listen for scroll event and load more images if we reach the bottom of window
    window.addEventListener('scroll', () => {
        if(window.scrollY + window.innerHeight >= document.documentElement.scrollHeight) {
            $('.profile-post-container-status-post:hidden').slice(0, 10).show().slideDown();
        }
    });
}

function viewAddPostModal() {
    //When the user clicks on the status form,
    $('.status-edit').click(() => {
        //show Create Post Modal
        $('.post-modal-container').css("display", "block");
    });
    //When the user clicks on the "x",
    $('#modal-container-close').click(() => {
        //close Create Post Modal
        $('.post-modal-container').css("display", "none");
        resetForm();
        $('#modal-container-photo-img').attr('src', "");
    });
}

function addPost() { 

    // When the user clicks on the "Choose file" button
    $("#myPhotoPost").change(function() {
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
        $('#modal-container-photo-img').attr('src', result);
        return result;
    };

}

async function addPostToProfile(event, jsonData) {
    event.preventDefault();
    const url = `/post`;
    const response = await fetch(url, {
        method: 'POST',
        headers: {  
            'Content-Type': 'application/json'
        },
        body: jsonData
    });
    if(response.status == 200) {
        $('#modal-container-photo-img').attr('src', "");
        location.reload();
    }
}

function resetForm() {
    document.getElementById("caption-add").reset();
}

function openCommentModal() {
    $('.post-button-comment').click(function() {
        var postId = $(this).attr("id");
        // show edit form 
        $(`#post-modal-container-comment-${postId}`).css("display", "flex");
    });

    // When the user clicks on the "x",
    $('.modal-container-close-comment').click(() => {
        // Close modal
        $('.post-modal-container-comment').css("display", "none");
    });
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

    SendCommentToController(jsonObj);
    location.reload();
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

function modifyPost (postId, profileLink) {
    //Show Update Modal
        $('#post-modal-container-update').css("display", "flex");

    // When the user clicks on the "x",
    $('#modal-container-close-update').click(() => {
        // Close modal
        $('#post-modal-container-update').css("display", "none");
    });

    //When the user clicks on "Update" button,
    $('.user-button-update').click((event) => {
        event.preventDefault();           
        //Save value of text to database
        var caption=$('.caption-update').val();      
            const formData = {
                Caption: caption
            }
            var data = JSON.stringify(formData);
            addPostModification(event, data, postId, profileLink);
        //close modal
        $('#post-modal-container-update').css("display", "none");
    });
}


function deletePost(postId, profileLink) {
    var modelObj = JSON.parse(model.replace(/&quot;/g,"\""));

    console.log("Post Id: " , postId);
    console.log("PostsList Length" , modelObj.PostsList.length);
    console.log(modelObj.PostsList);

   //show Delete Modal 
    $('#post-modal-container-delete').css("display", "flex");
    //When the user clicks on the "Yes" button,
    $('#yes-btn').click(() => {
        // check if Post has photoId
        for(var i = 0; i < modelObj.PostsList.length; i++) {
            var post = modelObj.PostsList[i]
            if(post.PostId == postId && post.PhotoId != 0) {
                deletePhoto(post.PhotoId);
            }
        }
        //Deletes the entire post
        deletePostById(postId, profileLink);
    });

    //When the user clicks on the "Cancel" button,
    $('#cancel-btn').click(() => {
        //show Delete Post Modal
        $('#post-modal-container-delete').css("display", "none");
    });

    // When the user clicks on the "x",
    $('#modal-container-close-delete').click(() => {
        // Close modal
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

async function deletePostById(postId, profileLink) {
    const response = await fetch(`/posts/${postId}`, {
        method: 'DELETE',
    });
    if (response.status == 200) {
        location.replace(`/${profileLink}`)
    }

}

async function addPostModification(event, jsonData, postId, profileLink) {
    event.preventDefault();
    const response = await fetch(`/posts/${postId}`, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: jsonData
    });
    if (response.status == 200) {
        location.replace(`/${profileLink}`)
    }
}
