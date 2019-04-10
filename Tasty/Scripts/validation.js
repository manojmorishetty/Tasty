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
        var value = checkValues(NameOnCard, cardNumber, expiryDate, cvv);
        if (!value) {
            document.getElementById('error').innerHTML = "Values are wrong";
            document.getElementById('error').style.color = "red";
            return false;
        } else {
            return true;
        }
    }
}


function checkValues(NameOnCard, cardNumber, expiryDate, cvv) {
    var regexAlpha = "[a-zA-Z]+";
    var regexNumber = "[0-9]+";	
    var regexExp = "(0[1-9]|10|11|12)\/(1[6-9]|2[0-9]|30|31)";
    stringCvv = cvv;
    
    //var firstDig = cardNumber.substr(0,1)
    //var twoDig = cardNumber.substr(0, 2)
    if (!NameOnCard.match(regexAlpha)) {
        return false;
    }

    if (cardNumber.length > 14 && cardNumber.length <= 16) {
        if (!cardNumber.match(regexNumber)) {
            return false;
        }
    }
    if ((expiryDate.length != 5 || !(expiryDate.match(regexExp)))) {
        return false;
    }
    if (!(cvv.match(regexNumber)) || stringCvv.length !=3) {
        return false;
    }

    return true;
    }
    //else if ()


