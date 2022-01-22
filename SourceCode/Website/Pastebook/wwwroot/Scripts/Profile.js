$(document).ready( () => {
<<<<<<< Updated upstream

    // Profile picture
    EditProfilePicture();

=======
    // Profile picture
    EditProfilePicture();
>>>>>>> Stashed changes
    // About Me
    EditAboutMe();
});

function EditProfilePicture() {
    // When the user clicks on the "Change photo" button,
    $('#photo-edit-btn').click(() => {
        // Open modal
        $('.profile-photo-edit-modal').css("display","flex");
    });

    // When the user clicks on the "Cancel" button,
    $('#photo-cancel-btn').click(() => {
        // Close modal
        $('.profile-photo-edit-modal').css("display","none");
    });

    // When the user clicks on the "x",
    $('#profile-photo-close-modal').click(() => {
        // Close modal
        $('.profile-photo-edit-modal').css("display","none");
    });

    // When the user clicks anywhere outside of the modal
<<<<<<< Updated upstream
    var modal = document.getElementById("profile-photo-edit-modal");
    window.onclick = function(event) {
        if (event.target == modal) {
=======
    window.onclick = function(event) {
        if (event.target == document.getElementById("profile-photo-edit-modal")) {
>>>>>>> Stashed changes
            // Close modal
            $('.profile-photo-edit-modal').css("display","none");
        }
    }

    // When the user clicks on the "Save" button,
<<<<<<< Updated upstream
    $('#photo-save-btn').click(() => {
        console.log("Save photo");
        // Change photo
        // ...write code here...
        
        // Save in database
        var img = document.getElementById('myFile').files;
        console.log(img);
        console.log(img.length);
        if (img.length > 0) {
            getBase64(img[0]);
        }

        // Close modal
        $('.profile-photo-edit-modal').css("display","none");
    });
}

function getBase64(file) {
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
      console.log(reader.result);
    };
    reader.onerror = function (error) {
      console.log('Error: ', error);
=======
    $('#photo-save-btn').click((event) => {
        event.preventDefault();
        // Close modal
        $('.profile-photo-edit-modal').css("display","none");

        var myImage = document.getElementById("myFile");
        if (myImage.files && myImage.files[0] && myImage.files[0].type.includes("image") ) {
            var reader = new FileReader();
            reader.readAsDataURL(myImage.files[0]);
            reader.onload = imageIsLoaded;
        }
        else {
            alert("This is not valid image file");
        }

        // Change profile photo
        function imageIsLoaded(e) {
            result = e.target.result;
            $('#profile-photo-img').attr('src', result);
            const formData = {
                Photo: result
            }
            var data = JSON.stringify(formData);
            modifyProfile(event, data);
        };
        
    });

    // When the user clicks on "Choose File"
    $("#myFile").change(function() {
        if (this.files && this.files[0] && this.files[0].type.includes("image")) { 
            var reader = new FileReader();
            reader.onload = imageIsLoaded;
            reader.readAsDataURL(this.files[0]);
        }
        else {
            alert("This is not valid image file");
        }
    });
    // Change modal photo
    function imageIsLoaded(e) {
        result = e.target.result;
        $('#profile-photo-modal-img').attr('src', result);
>>>>>>> Stashed changes
    };
}

function EditAboutMe() {
    // When the user clicks on the "Edit" button,
    $('#about-edit-btn').click(() => {
        // hide readonly profile
        $('.profile-about-readonly').css("display","none");
        // show edit form 
        $('.profile-about-edit').css("display","flex");
    });
    
    // When the user clicks on the "Cancel" button,
    $('#about-cancel-btn').click(() => {
        // show readonly profile
        $('.profile-about-readonly').css("display","flex");
        // hide edit form 
        $('.profile-about-edit').css("display","none");
    });
    
    // When the user clicks on the "Save" button,
<<<<<<< Updated upstream
    var about = document.getElementById("form-edit-about");
    $('#about-save-btn').click(() => {
        console.log(about.value);

        // Change "About me" text
        $('#profile-about-text').text(about.value);
        // Save about.value to database
        // ...write code here...
=======
    $('#about-save-btn').click((event) => {
        var about = document.getElementById("form-edit-about");
        // Change "About me" text
        $('#profile-about-text').text(about.value);
        // Save about.value to database
        var formData = new FormData(document.getElementById('form-profile-about'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyProfile(event, data);
>>>>>>> Stashed changes

        // show readonly profile
        $('.profile-about-readonly').css("display","flex");
        // hide edit form
        $('.profile-about-edit').css("display","none");
    });
<<<<<<< Updated upstream
}
=======
}

async function modifyProfile(event, jsonData) {
    event.preventDefault();
    const id = localStorage.getItem('profileId');
    const response = await fetch(`/${id}`, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: jsonData
    });
    if(response.status == 200) {
        const myProfile = await response.json();
        // Update currentProfile in local storage
        localStorage.setItem('currentProfile', JSON.stringify(myProfile));
    }
}

>>>>>>> Stashed changes
