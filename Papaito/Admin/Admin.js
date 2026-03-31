function validateLogin() {

    var login = document.getElementById('ctl00_getUserID').value;
    var password = document.getElementById('ctl00_getPassword').value;

    if (login == null || login == undefined || login == "") {
        document.getElementById('ctl00_errorLoginLabel').innerText = 'Please Enter User ID';
        return false;
    }

    if (password == null || password == undefined || password == "") {
        document.getElementById('ctl00_errorLoginLabel').innerText = 'Please Enter Password';
        return false;
    }
    return true;
}

function enableMail() {
    document.getElementById('ctl00_getMailDiv').className = 'mailYes';
}

function validate() {

    var email = document.getElementById('ctl00_recoverMail').value;

    if (email == null || email == undefined || email == "") {
        document.getElementById('ctl00_errorLoginLabel').innerText = "Please Enter Mail Address";
        return false;
    }

    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

    if (reg.test(email)) {
        return true;
    }
    document.getElementById('ctl00_errorLoginLabel').innerText = "Invalid Mail";
    return false;
}