$(document).ready(() => {
    viewAddPostModal();
    addPost();
    resetForm();
});

function viewAddPostModal() {
    //When the user clicks on the status form,
    $('.status-edit').click(() => {
        //show Create Post Modal
        $('.modal-container').css("display", "block");
    });
    //When the user clicks on the "x",
    $('#modal-container-close').click(() => {
        //close Create Post Modal
        $('.modal-container').css("display", "none");
    });
}

function addPost() {
    //When the user clicks on "Post" button,
    $('.user-post-button').click((event) => {
        event.preventDefault();
        //Save post.value to database
        var formData = new FormData(document.getElementById('caption-add'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        addPostToProfile(event, data);
        //close modal
        $('.modal-container').css("display", "none");
        resetForm();
    })
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
        alert("Post successfully added!");
    }
}

function resetForm() {
    document.getElementById("caption-add").reset();
}