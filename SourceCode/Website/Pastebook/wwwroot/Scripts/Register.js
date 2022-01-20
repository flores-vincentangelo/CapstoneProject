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