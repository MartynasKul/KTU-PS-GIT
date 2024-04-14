

    // Initialize Userfront
    Userfront.init("demo1234");

    // 1. Reference the elements on the page
    var passwordResetFormEl = document.getElementById("password-reset-form");
    var alertEl = document.getElementById("alert");
    var passwordEl = document.getElementById("password");
    var passwordVerifyEl = document.getElementById("password-verify");

    // 2. Reset the user's password
    function formResetPassword(e) {
        // Prevent the form's default behavior
        e.preventDefault();
        // Reset the alert to empty
        setAlert();
        // Verify that the passwords match
        var password = passwordEl.value;
        var passwordVerify = passwordVerifyEl.value;
        if (password !== passwordVerify) {
            return setAlert("Password verification must match.");
        }
        // Call Userfront.resetPassword()
        Userfront.resetPassword({
            password: password
        }).catch(function (error) {
            setAlert(error.message);
        });
    }

    // Set the alert element to show the message
    function setAlert(message) {
        alertEl.innerText = message;
        alertEl.style.display = message ? "block" : "none";
    }

    // 3. Add an event listener for the password reset form submit
    passwordResetFormEl.addEventListener("submit", formResetPassword);
