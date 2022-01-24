$(document).ready( () => {
    EditInformation();
});

function EditInformation() {
    // When the user clicks on the "Edit" button,
    $('#firstname-edit-btn').click(() => {
        // hide readonly profile
        $('.user-firstname-readonly').css("display","none");
        // show edit form 
        $('.user-firstname-edit').css("display","flex");
    });
    
    // When the user clicks on the "Cancel" button,
    $('#firstname-cancel-btn').click(() => {
        // show readonly profile
        $('.user-firstname-readonly').css("display","flex");
        // hide edit form 
        $('.user-firstname-edit').css("display","none");
    });
    
    // When the user clicks on the "Save" button,
    $('#firstname-save-btn').click((event) => {
        var about = document.getElementById("form-edit-firstname");
        // Change "About me" text
        $('#user-firstname-text').text(about.value);
        // Save about.value to database
        var formData = new FormData(document.getElementById('form-user-firstname'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyInfo(event, data);

        // show readonly profile
        $('.user-firstname-readonly').css("display","flex");
        // hide edit form
        $('.user-firstname-edit').css("display","none");
    });
}

async function modifyInfo(event, jsonData) {
    event.preventDefault();
    const link = localStorage.getItem('profileId');
    const response = await fetch(`/settings/${link}`, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: jsonData
    });
    if(response.status == 200) {
        alert("Profile information modified successfully!");
    }
}

