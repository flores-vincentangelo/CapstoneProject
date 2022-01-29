$(document).ready(() => {
    var modelObj = JSON.parse(model.replace(/&quot;/g,"\""));
    console.log(modelObj)
    openDeleteModal();
    openUpdateModal();
    deletePost(modelObj);

    //When the user clicks on "Update" button,
    $('.user-button-update').click((event) => {
        event.preventDefault();           
        //Save value of text to database
        var caption=$('#caption-update').val();      
            const formData = {
                Caption: caption
            }
            var data = JSON.stringify(formData);
            addPostModification(event, data, modelObj.PostId);
        //close modal
        $('#post-modal-container-update').css("display", "none");
        location.reload();
    });


});

function openDeleteModal() {
    $('.delete-post').click(() => {
        // show edit form 
        $('#post-modal-container-delete').css("display", "flex");
    });

    // When the user clicks on the "x",
    $('#modal-container-close-delete').click(() => {
        // Close modal
        $('#post-modal-container-delete').css("display", "none");
    });
}

function openUpdateModal() {
    $('.update-post').click(() => {
        // show edit form 
        $('#post-modal-container-update').css("display", "flex");
    });

    // When the user clicks on the "x",
    $('#modal-container-close-update').click(() => {
        // Close modal
        $('#post-modal-container-update').css("display", "none");
    });
}

function deletePost(modelObj) {
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
    console.log(postId)
    const response = await fetch(`/posts/${postId}`, {
        method: 'DELETE',
    });
    if (response.status == 200) {
        location.replace(`/${profileLink}`)
    }

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

