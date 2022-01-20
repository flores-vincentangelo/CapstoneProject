function lettersInput(input) {
    var regex = /[^a-z]/gi;
    input.value = input.value.replace(regex,"");
}

function numbersInput(input) {
    var regex = /[^0-9]/g;
    input.value = input.value.replace(regex,"");
}