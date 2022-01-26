$(document).ready( () => {
    
    AddNewAlbum();

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


