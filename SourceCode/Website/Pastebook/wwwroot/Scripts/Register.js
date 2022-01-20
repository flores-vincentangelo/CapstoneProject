// LETTER INPUT RESTRICTION
function lettersInput(input) {
    var regex = /[^a-z]/gi;
    input.value = input.value.replace(regex,"");
}

// NUMBER INPUT RESTRICTION
function numbersInput(input) {
    var regex = /[^0-9]/g;
    input.value = input.value.replace(regex,"");
}

//CONFIRM PASSWORD
document.querySelector('.signup-button').onclick = function() {
    var password = document.querySelector('.password').value;
    var confirmPassword = document.querySelector('.confirm-password').value;
    if (password != confirmPassword) {
        alert("Passwords do not match. Try again.");
        return false;
    }
    else if (password == confirmPassword) {
        return true;
    }
    return true;
}