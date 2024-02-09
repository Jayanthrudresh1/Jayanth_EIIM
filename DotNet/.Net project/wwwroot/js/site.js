$(document).ready(function () {
    $.validator.addMethod("strongPassword", function (value, element) {
        return /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/.test(value);
    }, "Invalid password format.");

    // Enable client-side validation on the form
    $("#loginForm").validate({
        rules: {
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,
                strongPassword: true
            }
        },
        messages: {
            Email: {
                required: "Email is required.",
                email: "Invalid email address."
            },
            Password: {
                required: "Password is required.",
                strongPassword: "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character."
            }
        },
        errorPlacement: function (error, element) {
            error.appendTo("#errorMessage");
        }
    });
});
