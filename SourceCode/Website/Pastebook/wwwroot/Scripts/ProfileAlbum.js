$(document).ready( () => {
    AddNewAlbum();
    AddNewPhoto();
    // CloseAlbum();
});

function AddNewAlbum() {
    // When the user clicks on the "Add New Album" button,
    $('#album-add-btn').click(() => {
        // Open modal
        $('.album-add-modal').css("display","flex");
    });

    // When the user clicks on the "Cancel" button,
    $('#album-cancel-btn').click(() => {
        // Close modal
        $('.album-add-modal').css("display","none");
    });

    // When the user clicks on the "x",
    $('#album-add-close-modal').click(() => {
        // Close modal
        $('.album-add-modal').css("display","none");
    });

    // When the user clicks anywhere outside of the modal
    window.onclick = function(event) {
        if (event.target == document.getElementById("album-add-modal")) {
            // Close modal
            $('.album-add-modal').css("display","none");
        }
    }

    // When the user clicks on the "Save" button,
    $('#album-save-btn').click((event) => {
        event.preventDefault();
        // Close modal
        $('.album-add-modal').css("display","none");

        var albumName = document.getElementById("name");
        var userEmail = getCookieValue('email');
        console.log("Album Name: " + albumName.value);
        console.log(getCookieValue('email'));
        // Save to database
        const formData = {
            "UserEmail": userEmail,
            "AlbumName": albumName.value
        }
        var data = JSON.stringify(formData);
        console.log(data);
        AddAlbum(event, data);
    });
}

async function AddAlbum(event, jsonData) {
    event.preventDefault();
    const url = "/albums";
    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: jsonData
    });
    if(response.status == 200) {
        alert("Album created successfully!");
        // Reload page
        location.reload();
    }
}

function AddNewPhoto() {
    // When the user clicks on the "Add New Photo" button,
    $('.add-new-photo').click(() => {
        // Open modal
        $('.photo-add-modal').css("display","flex");
        // Reset input form
        $("#myForm").val("");
        $("#form-add-photo").val(null);
        $('#album-photo-modal-img').attr('src', "./Images/img_avatar.png");
    });

    // When the user clicks on the "Cancel" button,
    $('#add-photo-cancel-btn').click(() => {
        // Close modal
        $('.photo-add-modal').css("display","none");
        // Reset input form
        $("#myForm").val("");
        $("#form-add-photo").val(null);
        $('#album-photo-modal-img').attr('src', "./Images/img_avatar.png");
    });

    // When the user clicks on the "x",
    $('#photo-add-close-modal').click(() => {
        // Close modal
        $('.photo-add-modal').css("display","none");
        // Reset input form
        $("#myForm").val("");
        $("#form-add-photo").val(null);
        $('#album-photo-modal-img').attr('src', "./Images/img_avatar.png");
    });

    // When the user clicks anywhere outside of the modal
    window.onclick = function(event) {
        if (event.target == document.getElementById("photo-add-modal")) {
            // Close modal
            $('.photo-add-modal').css("display","none");
            // Reset input form
            $("#myForm").val("");
            $("#form-add-photo").val(null);
            $('#album-photo-modal-img').attr('src', "./Images/img_avatar.png");
        }
    }

    // When the user clicks on the "Choose file" button
    $("#myPhoto").change(function() {
        if (this.files && this.files[0] && this.files[0].type.includes("image")) { 
            var reader = new FileReader();
            reader.readAsDataURL(this.files[0]);
            reader.onload = imageIsLoaded;
        }
        else {
            alert("This is not valid image file");
        }
    });
    // Change photo
    function imageIsLoaded(e) {
        result = e.target.result;
        $('#album-photo-modal-img').attr('src', result);
        saveImage(result);
    };

    function saveImage(image) {
        // When the user clicks on the "Save" button,
        $('#add-photo-save-btn').click((event) => {
            event.preventDefault();
            // Close modal
            $('.photo-add-modal').css("display","none");
            const formData = {
                Photo: image
            }
            var data = JSON.stringify(formData);
            addPhotoInAlbumId(event, data);
        });
    };
}

async function addPhotoInAlbumId(event, jsonData) {
    event.preventDefault();
    const albumId = getCookieValue('currentAlbumId'); 
    const url = `/photos/${albumId}`;
    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: jsonData
    });
    if(response.status == 200) {
        alert(await response.text());
        // Reload page
        location.reload();
    }
}

function OpenAlbum(albumId) {
    console.log("Current Album Id: " + albumId);
    document.cookie = 'currentAlbumId=' + albumId;
    $('.album-container').css("display", "none");
    $('.photos-container').css("display", "block");
    
    var albums = document.getElementsByClassName("photos-card-container");
    console.log("No. of Albums: " + albums.length);

    $(`#photos-card-container-${albumId}`).css("display", "flex");
    
}

function CloseAlbum(albumId) {
    console.log("Current Album Id to close: " + albumId)
    document.cookie = 'currentAlbumId=' + 0;
    CancelEditAlbumName(albumId);
    // Show album container
    $('.album-container').css("display", "block");
    // Hide photos container
    $('.photos-container').css("display", "none");
    $(`#photos-card-container-${albumId}`).css("display", "none");
}

function EditAlbumName(albumId) {
    console.log("Album Id to be modified: " + albumId);
    document.cookie = 'currentAlbumId=' + albumId;
    // Hide the read-only album title
    $(`#photos-card-title-readonly-${albumId}`).css("display", "none");
    // Show the read-only album title
    $(`#photos-card-title-edit-${albumId}`).css("display", "flex");
}

function CancelEditAlbumName(albumId) {
    document.cookie = 'currentAlbumId=' + 0;
    // Show the read-only album title
    $(`#photos-card-title-readonly-${albumId}`).css("display", "flex");
    // Hide the read-only album title
    $(`#photos-card-title-edit-${albumId}`).css("display", "none");
}

async function SaveEditAlbumName(event, albumId) {
    event.preventDefault();
    var formData = new FormData(document.getElementById(`photos-card-title-form-edit-${albumId}`));
    console.log($(`#photos-card-form-input-${albumId}`).val());
    var data = JSON.stringify(Object.fromEntries(formData.entries()));

    const url = `albums/${albumId}`;
    const response = await fetch(url, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: data
    });
    if(response.status == 200) {
        console.log("Album Id successfully modified: " + albumId);
        alert(await response.text());
        const newAlbumName = $(`#photos-card-form-input-${albumId}`).val();
        $(`#photos-card-title-albumname-${albumId}`).text(newAlbumName);
        // Show the read-only album title
        $(`#photos-card-title-readonly-${albumId}`).css("display", "flex");
        // Hide the read-only album title
        $(`#photos-card-title-edit-${albumId}`).css("display", "none");
        // Reload page
        location.reload();
    }
}

function OpenDeleteAlbumModal(albumId) {
    console.log("Album Id to be deleted: " + albumId);
    // Show modal
    $(`#album-delete-modal-${albumId}`).css("display", "flex");

    // When the user clicks anywhere outside of the modal
    window.onclick = function(event) {
        if (event.target == document.getElementById(`album-delete-modal-${albumId}`)) {
            // Close modal
            $(`#album-delete-modal-${albumId}`).css("display", "none");
        }
    }

    // When the user clicks on the "Cancel" button,
    $('.album-delete-cancel-btn').click(() => {
        // Close modal
        $(`#album-delete-modal-${albumId}`).css("display", "none");
    });

    // When the user clicks on the "x",
    $('.album-delete-close-modal').click(() => {
        // Close modal
        $(`#album-delete-modal-${albumId}`).css("display", "none");
    });
}

async function DeleteAlbum(albumId) {
    // Close modal
    $(`#album-delete-modal-${albumId}`).css("display", "none");
    document.cookie = 'currentAlbumId=' + 0;
    const url = `/albums/${albumId}`;
    const response = await fetch(url, {
        method: 'DELETE'
    });
    if(response.status == 200) {
        alert(await response.text());
        // Reload page
        location.reload();
    }
}

function OpenDeletePhotoModal(photoId) {
    console.log("Photo Id to be deleted: " + photoId);
    // Show modal
    $(`#photo-delete-modal-${photoId}`).css("display", "flex");

    // When the user clicks anywhere outside of the modal
    window.onclick = function(event) {
        if (event.target == document.getElementById(`photo-delete-modal-${photoId}`)) {
            // Close modal
            $(`#photo-delete-modal-${photoId}`).css("display", "none");
        }
    }

    // When the user clicks on the "Cancel" button,
    $('.photo-delete-cancel-btn').click(() => {
        // Close modal
        $(`#photo-delete-modal-${photoId}`).css("display", "none");
    });

    // When the user clicks on the "x",
    $('.photo-delete-close-modal').click(() => {
        // Close modal
        $(`#photo-delete-modal-${photoId}`).css("display", "none");
    });

}

async function DeletePhoto(photoId) {
    const url = `/photos/${photoId}`;
    const response = await fetch(url, {
        method: 'DELETE'
    });
    if(response.status == 200) {
        alert(await response.text());
        // Close modal
        $(`#photo-delete-modal-${photoId}`).css("display", "none");
        // Reload page
        location.reload();
    }
}


