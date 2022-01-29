$(document).ready(() => {
    viewAddPostModal();
    addPost();
    resetForm();


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

});

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
        alert("Post successfully added!");
        $('#modal-container-photo-img').attr('src', "");
    }
}

function resetForm() {
    document.getElementById("caption-add").reset();
}