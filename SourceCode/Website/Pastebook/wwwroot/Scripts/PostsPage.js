$(document).ready(function () {
    var modelObj = JSON.parse(model.replace(/&quot;/g,"\""));
    console.log(modelObj)

    editPost();
    deletePost(modelObj);
    modifyPhoto();

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

    //When the user clicks on the "X" button,
    $('.delete-but').click(() => {
        //show Delete Post Modal
        $('#post-modal-container-delete').css("display", "flex");
    });
    
    //When the user clicks on the "Yes" button,
    $('#yes-btn').click(() => {
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

async function deletePostById(postId, profileLink) {

    const response = await fetch(`/posts/${postId}`, {
        method: 'DELETE',
    });
    if (response.status == 200) {
        location.replace(`/${profileLink}`)
    }

}

