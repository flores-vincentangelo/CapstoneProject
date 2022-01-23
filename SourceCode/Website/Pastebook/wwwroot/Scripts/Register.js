// LETTER INPUT RESTRICTION
function lettersInput(input) {
    var regex = /[^a-z ]/gi;
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

// LENGTH OF USER'S INPUT RESTRICTION
function mobileNumLimit(element) {
    var max_chars = 10;

    if(element.value.length > max_chars) {
        element.value = element.value.substr(0, max_chars);
        alert("Input is restricted to 11 digits only!");
    }
}

function passwordLimit(element)
{
    var max_chars = 11;

    if(element.value.length > max_chars) {
        element.value = element.value.substr(0, max_chars);
        alert("Password should have a maximum of 12 characters only!");
    }
}

// RESTRICT FUTURE DATES
var date = new Date();
var tdate = date.getDate(); 
var month = date.getMonth() + 1;
if (tdate < 10) {
    tdate = '0' + tdate;
}
if (month < 10) {
    month = '0' + month;
}
var year = date.getUTCFullYear();
var maxDate = year + "-" + month + "-" + tdate;
document.getElementById("date").setAttribute("max", maxDate);