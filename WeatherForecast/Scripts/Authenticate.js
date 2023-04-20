function B2CLogOff() {
    try {
        window.location.href = "/B2CAccount/SignOut"
    } catch (err) {
        HandleError(err);
    }
}

function B2CResetPassword() {
    try {
        window.location.href = "/B2CAccount/ResetPassword"
    } catch (err) {
        HandleError(err);
    }
}