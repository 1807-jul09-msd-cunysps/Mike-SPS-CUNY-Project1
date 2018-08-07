/*
 * On Page Load
 */
$(document).ready(
    function () {
        init();
    }
);

/*
 * Add Listeners
 */
function init() {
    // Validate first name
    $("#inputFullName").blur(validateName);
    // Validate Email
    $("#inputEmail").blur(validateEmail);
    // Validate Message
    $("#inputMessage").blur(CannotBeEmpty);
    // On submit
    $("#inputForm").submit(formSubmitted);
}

/*
 * Form Validations
 */
function validateName(e) {
    // Remove previous span if exists
    $("#" + this.id + " + span").remove();
    // Validate
    if (!(/^([a-zA-Z ]{1,})$/.test($(this).val()))) {
        $(this).after('<span class="error">Cannot be empty.<br/>Can only contain letters.</span>');
    }   
}

function CannotBeEmpty(e) {
    // Remove previous span if exists
    $("#" + this.id + "+ span").remove();
    // Check if empty
    if ($(this).val().length < 1) {
        $(this).after('<span class="error">Cannot be empty.</span>');
    }
}

function validateEmail(e) {
    // Remove previous span if exists
    $("#" + this.id + "+ span").remove();
    // Validate
    if (!(/(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])$/.test($(this).val()))) {
        $(this).after('<span class="error">Please enter a valid email address.</span>');
    }
}

function validateAll() {

    // Wipe all errors
    $(".error").each(function () {
        $(this).remove();
    });

    // FullName
    if (!(/^([a-zA-Z ]{1,})$/.test($("#inputFullName").val()))) {
        $("#inputFullName").after('<span class="error">Cannot be empty.<br/>Can only contain letters.</span>');
    }
    // Email
    if (!(/(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])$/.test($("#inputEmail").val()))) {
        $("#inputEmail").after('<span class="error">Please enter a valid email address.</span>');
    }
    // Message
    if ($("#inputMessage").val().length < 1) {
        $("#inputMessage").after('<span class="error">Cannot be empty.</span>');
    }

    // If errors exist, return false
    if (document.querySelector(".error") != null)
        return false;
    return true;
}

/*
 * OnSubmit override and support functions
 */
function formSubmitted(e) {
    // Prevent page refresh
    e.preventDefault();
    // Validate all fields
    if (!validateAll()) {
        return;
    }
    // Create new person
    var newMessage = BuildMessageObj();
    //
    console.log(newMessage);

    // Post new person with AJAX
    sendPost(newMessage);

    //
}

function sendPost(newMessage) {
    var url = "http://rev-mc-project1.azurewebsites.net/api/message";
    var person = newMessage;
    $.ajax({
        type: "POST",
        url: url,
        data: person,
        dataType: "JSON",
        success: function () {
            window.location.replace('http://rev-mc-project1.azurewebsites.net/Pages/Home.html');
        },
        error: function (xhr, status, error) {
            if (xhr.status == 200)
                window.location.replace('http://rev-mc-project1.azurewebsites.net/Pages/Home.html');
        }
    })
}

function BuildMessageObj() {
    
    var newMessage = {
        "FullName": document.querySelector("#inputFullName").value,
        "Email": document.querySelector("#inputEmail").value,
        "Message": document.querySelector("#inputMessage").value
    };
    return newMessage;
}