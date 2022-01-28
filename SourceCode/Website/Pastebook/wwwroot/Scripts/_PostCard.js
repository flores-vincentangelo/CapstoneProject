$(document).ready(() => {
    openDeleteModal();
    // openUpdateModal();
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

// function openUpdateModal() {
//     $('.update-but').click(() => {
//         // show edit form 
//         $('#post-modal-container-update').css("display", "flex");
//     });

//     // When the user clicks on the "x",
//     $('#modal-container-close-update').click(() => {
//         // Close modal
//         $('#post-modal-container-update').css("display", "none");
//     });
// }