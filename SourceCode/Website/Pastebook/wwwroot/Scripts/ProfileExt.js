$(document).ready(() => {
    EditProfile();
});

function EditProfile() {
    // When the user clicks on the "Edit" button,
    $('#profile-edit-btn').click(() => {
        // show edit form 
        $('.profile-edit-modal').css("display","flex");
    });
    
    // When the user clicks on the "Cancel" button,
    $('#cancel-btn').click(() => {
        // hide edit form 
        $('.profile-edit-modal').css("display","none");
    });
    
    // When the user clicks on the "Save" button,
    $('#about-save-btn').click((event) => {
        // Change "Details"
        var firstName = document.getElementById("first-name");
        $('#first-name').text(firstName.value);
        // Save about.value to database
        var formData = new FormData(document.getElementById('form-profile'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyProfile(event, data);

        // show readonly profile
        $('.profile-about-readonly').css("display","flex");
        // hide edit form
        $('.profile-about-edit').css("display","none");
    });
}

async function modifyProfileData(event, jsonData) {
    event.preventDefault();
    const link = localStorage.getItem('profileLink');
    const response = await fetch(`/${link}`, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: jsonData
    });
    if (response.status == 200) {
        alert("Profile modified successfully!");
        var userData = await response.json();
        localStorage.setItem('User', JSON.stringify(userData));
    }
}
