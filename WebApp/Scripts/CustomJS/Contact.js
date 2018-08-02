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
    $("#inputFirstName").blur(validateName);
    // Validate last name
    $("#inputLastName").blur(validateName);
    // Validate age
    $("#inputAge").blur(validateAge);
    // Validate gender
    $("#inputGender").change(validateGender);
    // Validate Addr Line 1
    $("#inputAddr1").blur(CannotBeEmpty);
    // Validate Zipcode
    $("#inputZipcode").blur(validateZipcode);
    // Validate City
    $("#inputCity").blur(CannotBeEmpty);
    // Validate State
    $("#inputState").blur(CannotBeEmpty);
    // Validate Country
    //$("#inputCountry").change(CannotBeEmpty);
    // Validate Phone
    $("#inputPhoneNum").blur(validatePhone);
    // Validate Email
    $("#inputEmail").blur(validateEmail);
    // Populate Countries
    // On submit
    $("#inputForm").submit(formSubmitted);
    // Add listener to zipcodes
    document.querySelector("#inputZipcode").addEventListener("blur", zipcodePopCityState);
    document.querySelector("#inputPrimZipcode").addEventListener("blur", zipcodePopPrimCityState);
}

/*
 * Zipcode API
 */
// https://www.zipcodeapi.com/API API key
const zipcodeApiKey = "js-8CmO1wuQQBeCe7z5bIChCXAOTbwaJm8reizkWEG8Gmg5IhAcqOfUQQoYxHq5lAFm";

// Generic AJAX for GET requests
function ajaxGet(url, cbFunction) {                 
    var req;                                        
    if (window.XMLHttpRequest) {                  
        req = new XMLHttpRequest();
    } else {                                       
        req = new ActiveXObject("Microsoft.XMLHTTP");
    }
    req.onreadystatechange = function () {        
        if (this.readyState == 4 &&
            this.status == 200) {
            cbFunction(this);                      
        }
    };
    req.open("GET", url, true);                   
    req.send();                                     
}

// Populate city and state for Address Info
function zipcodePopCityState(e) {
    let zipcode = e.target.value;
    let url = "https://www.zipcodeapi.com/rest/" + zipcodeApiKey + "/info.json/" + zipcode + "/radians";
    var cbFunc = function (req) {
        var data = JSON.parse(req.responseText);
        document.querySelector("#inputCity").value = data.city;
        document.querySelector("#inputState").value = data.state;
    }
    ajaxGet(url, cbFunc);
}

// Populate city and state for Primary Address Info
function zipcodePopPrimCityState(e) {
    let zipcode = e.target.value;
    let url = "https://www.zipcodeapi.com/rest/" + zipcodeApiKey + "/info.json/" + zipcode + "/radians";
    var cbFunc = function (req) {
        var data = JSON.parse(req.responseText);
        document.querySelector("#inputPrimCity").value = data.city;
        document.querySelector("#inputPrimState").value = data.state;
    }
    ajaxGet(url, cbFunc);
}

/*
 * Primary Address Div
 */
// Function for showing Primary Address div
function togglePrimAddr() {
    var selector = document.querySelector("#isPrimAddr").value;
    var primAddrDiv = document.querySelector("#primaryAddr");
    if (selector == "No") {
        primAddrDiv.style.display = "block";
    }
    else {
        primAddrDiv.style.display = "none";
    }
}

/*
 * Form Validations
 */
function validateName(e) {
    // Remove previous span if exists
    $("#" + this.id + " + span").remove();
    // Validate
    if (!(/^([a-zA-Z]{1,})$/.test($(this).val()))) {
        $(this).after('<span class="error">Cannot be empty.<br/>Can only contain letters.</span>');
    }   
}

function validateAge(e) {
    // Remove previous span if exists
    $("#" + this.id + " + span").remove();
    // Check if empty
    if (!(/^([0-9]{1,})$/.test($(this).val()))) {
        $(this).after('<span class="error">Only numbers. <br/> Cannot be empty.</span>');
    }
    else if ($(this).val() > 120 || $(this).val() < 15) {
        $(this).after('<span class="error">Must be between 15 and 120.</span>');
    }
}

function validateGender(e) {
    // Remove previous span if exists
    $("#" + this.id + " + span").remove();
    // Check if empty
    if ($(this).val() == "Select") {
        $(this).after('<span class="error">Required field.</span>');
    }
}

//function validateAddr(e) {
//    // Remove previous span if exists
//    $("#" + this.id + " + span").remove();
//    // Validate
//    if ((/^\s*$/.test($(this).val()))) {
//        $(this).after('<span class="error">Cannot be empty.</span>');
//    }
//}

function validateZipcode(e) {
    // Remove previous span if exists
    $("#" + this.id + " + span").remove();
    // Validate
    if (!(/^\d{5}(?:[-\s]\d{4})?$/.test($(this).val()))) {
        $(this).after('<span class="error">Must be 5 or 9 digits.</span>');
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

function validatePhone(e) {
    // Remove previous span if exists
    $("#" + this.id + " + span").remove();
    // Validate
    if (!(/^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/.test($(this).val()))) {
        $(this).after('<span class="error">Please use a 10 digit phone number.</span>');
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

    // Firstname
    if (!(/^([a-zA-Z]{1,})$/.test($("#inputFirstName").val()))) {
        $("#inputFirstName").after('<span class="error">Cannot be empty.<br/>Can only contain letters.</span>');
    }
    // Lastname
    if (!(/^([a-zA-Z]{1,})$/.test($("#inputLastName").val()))) {
        $("#inputLastName").after('<span class="error">Cannot be empty.<br/>Can only contain letters.</span>');
    }
    // Age
    if (!(/^([0-9]{1,})$/.test($("#inputAge").val()))) {
        $("#inputAge").after('<span class="error">Only numbers. <br/> Cannot be empty.</span>');
    }
    else if ($("#inputAge").val() > 120 || $("#inputAge").val() < 15) {
        $("#inputAge").after('<span class="error">Must be between 15 and 120.</span>');
    }
    // Gender
    if ($("#inputGender").val() == "Select") {
        $("#inputGender").after('<span class="error">Required field.</span>');
    }
    // Addr Line 1
    if ($("#inputAddr1").val().length < 1) {
        $("#inputAddr1").after('<span class="error">Cannot be empty.</span>');
    }
    // City
    if ($("#inputCity").val().length < 1) {
        $("#inputCity").after('<span class="error">Cannot be empty.</span>');
    }
    // State
    if ($("#inputState").val().length < 1) {
        $("#inputState").after('<span class="error">Cannot be empty.</span>');
    }
    // Zip
    if (!(/^\d{5}(?:[-\s]\d{4})?$/.test($("#inputZipcode").val()))) {
        $("#inputZipcode").after('<span class="error">Must be 5 or 9 digits.</span>');
    }
    // Country
    // Phone Num
    // Email
    if (!(/(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])$/.test($("#inputEmail").val()))) {
        $("#inputEmail").after('<span class="error">Please enter a valid email address.</span>');
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
        //console.log("Not valid!");
        return;
    }
    //console.log("Valid!");
    // Create new person
    var newPerson = buildPersonObj();
    //
    console.log(newPerson);

    // Post new person with AJAX
    sendPost(newPerson);
}

function sendPost(newPerson) {
    var url = "http://mc-proj-0.azurewebsites.net/api/person";
    var person = newPerson;
    $.ajax({
        type: "POST",
        url: url,
        data: person,
        dataType: "JSON",
        success: function (response) {
            if (response != null) {
                para.text(JSON.stringify(response));
            }
            else {
                alert("error");
            }
        }
    })
}

function buildPersonObj() {
    
    var newPerson = {
        "Firstname": document.querySelector("#inputFirstName").value,
        "Lastname": document.querySelector("#inputLastName").value,
        "Age": document.querySelector("#inputAge").value,
        "Gender": document.querySelector("#inputGender").value,
        "Address": {
            "AddrLine1": document.querySelector("#inputAddr1").value,
            "AddrLine2": document.querySelector("#inputAddr2").value,
            "City": document.querySelector("#inputCity").value,
            "State": document.querySelector("#inputState").value,
            "Country": document.querySelector("#inputCountry").value,
            "Zipcode": document.querySelector("#inputZipcode").value
        },
        "Contact": {
            "Country": document.querySelector("#inputCountry").value,
            "Number": document.querySelector("#inputPhoneNum").value,
            "Ext": document.querySelector("#inputPhoneExt").value,
            "Email": document.querySelector("#inputEmail").value
        },
    };
    return newPerson;
}