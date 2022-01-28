$(document).ready( () => {
    AddNewAlbum();
    AddNewPhoto();
    CloseAlbum();
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

// const getCookieValue = (name) => (
//     document.cookie.match('(^|;)\\s*' + name + '\\s*=\\s*([^;]+)')?.pop() || ''
// )

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
    }
}

function AddNewPhoto() {
    // When the user clicks on the "Add New Photo" button,
    $('#photo-add-btn').click(() => {
        // Open modal
        $('.photo-add-modal').css("display","flex");
    });

    // When the user clicks on the "Cancel" button,
    $('#add-photo-cancel-btn').click(() => {
        // Close modal
        $('.photo-add-modal').css("display","none");
    });

    // When the user clicks on the "x",
    $('#photo-add-close-modal').click(() => {
        // Close modal
        $('.photo-add-modal').css("display","none");
    });

    // When the user clicks anywhere outside of the modal
    window.onclick = function(event) {
        if (event.target == document.getElementById("photo-add-modal")) {
            // Close modal
            $('.photo-add-modal').css("display","none");
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
    }
}

function OpenAlbum(albumId) {
    console.log("Current Album Id: " + albumId);
    document.cookie = 'currentAlbumId=' + albumId;
    $('.album-container').css("display", "none");
    $('.photos-container').css("display", "block");
}

function CloseAlbum() {
    $('#photo-close-album-btn').click(function() {
        document.cookie = 'currentAlbumId=' + 0;
        $('.album-container').css("display", "block");
        $('.photos-container').css("display", "none");
    });
}

async function DeleteAlbum(albumId) {
    console.log("Album Id to be deleted: " + albumId);
    document.cookie = 'currentAlbumId=' + 0;
    const url = `/albums/${albumId}`;
    const response = await fetch(url, {
        method: 'DELETE'
    });
    if(response.status == 200) {
        alert(await response.text());
    }
}

function EditAlbumName(albumId) {
    var readOnlyId = "#albumcard-title-readonly-" + albumId;
    var editId = "#albumcard-title-edit-" + albumId;
    console.log("Album Id to be modified: " + albumId);
    document.cookie = 'currentAlbumId=' + albumId;
    $(readOnlyId).css("display", "none");
    $(editId).css("display", "block");
}

function CancelEditAlbumName(albumId) {
    console.log("Album Id to be modified: " + albumId);
    document.cookie = 'currentAlbumId=' + 0;
    $('.albumcard-title-readonly').css("display", "block");
    $('.albumcard-title-edit').css("display", "none");
}

async function SaveEditAlbumName(event, albumId) {
    event.preventDefault();
    var formId = "albumcard-form-edit-" + albumId;
    var formData = new FormData(document.getElementById(formId));
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
        $('.albumcard-title-readonly').css("display", "block");
        $('.albumcard-title-edit').css("display", "none");
    }
}