$(document).ready( () => {
    openCommentModal();
});

function openCommentModal() {
    $('.post-button-comment').click(() => {
        // show edit form 
        $('#post-modal-container-comment').css("display", "flex");
    });

    // When the user clicks on the "x",
    $('#modal-container-close-comment').click(() => {
        // Close modal
        $('#post-modal-container-comment').css("display", "none");
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
   //show Delete Modal 
    $('#post-modal-container-delete').css("display", "flex");
    //When the user clicks on the "Yes" button,
    $('#yes-btn').click(() => {
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