








function getElems() {
    var NameOnCard = document.getElementById("nameOnCard").value;
    var cardNumber = document.getElementById("cardNumber").value;
    var expiryDate = document.getElementById("expiryDate").value;
    var cvv = document.getElementById("cvv").value;

    if (!(NameOnCard) || !(cardNumber) || !(expiryDate) || !(cvv)) {
        document.getElementById('error').innerHTML = "Please Fill all the values";
        document.getElementById('error').style.color = "red";
        return false;
    }
    else {
        checkValues(NameOnCard, cardNumber, expiryDate, cvv);
    }
}


function checkValues(NameOnCard, cardNumber, expiryDate, cvv) {
    var regexAlpha = "[a-zA-Z]+";
    var regexNumber = "[0-9]+";	
    //var firstDig = cardNumber.substr(0,1)
    //var twoDig = cardNumber.substr(0, 2)
    if (!NameOnCard.match(regexAlpha)) {
        return false;
    }

    if (cardNumber.length > 13 && cardNumber.length <= 16) {
        if (!cardNumber.match(regexNumber)) {
            return false;
        }
    }
    if ((!expiryDate.match(regexNumber)) && expiryDate.length != 4) {
        return false;
    }
    else {
        var month = expiryDate.substr(0, 2)
        if (!(month > 0) && !(month <= 12)) {
            return false
        }
    }
    //else if ()

}
