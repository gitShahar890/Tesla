document.addEventListener('DOMContentLoaded', function () {
    var signUpBtn = document.getElementById("signUpBtn");
    var logInBtn = document.getElementById("logInBtn");
    var mover = document.getElementById("mover");
    var formContainer = document.getElementById("form-container");
    var regForm = document.getElementById("reg-form");
    var logForm = document.getElementById("log-form");
    var signUpClicked = true;
    var firstName = document.getElementById("firstName");
    var LastName = document.getElementById("LastName");
    var email = document.getElementById("email");
    var phoneNumber = document.getElementById("phoneNumber");
    var password = document.getElementById("password");
    var date = document.getElementById("date");
    var gender_conatainer = document.getElementById("gender-conatainer");
    var male = document.getElementById("maleradio");
    var female = document.getElementById("femaleradio");
    var favorite_car_container = document.getElementById("favorite-car-container");
    var text_area_container = document.getElementById("text-area-container");
    var text_area = document.getElementById("text-area");
    var checkbox_container = document.getElementById("checkbox-container");
    var submit_rest_container = document.getElementById("submit-rest-container");
    var T_P_checkbox = document.getElementById("T_P-checkbox");
    var validation_container = document.querySelector('.validation-container');
    var v_icons = document.querySelector('.v-icons');
    var x_icons = document.querySelector('.x-icons');
    var v1 = document.getElementById("v1");
    var v2 = document.getElementById("v2");
    var v3 = document.getElementById("v3");
    var v4 = document.getElementById("v4");
    var v5 = document.getElementById("v5");
    var v6 = document.getElementById("v6");
    var x1 = document.getElementById("x1");
    var x2 = document.getElementById("x2");
    var x3 = document.getElementById("x3");
    var x4 = document.getElementById("x4");
    var x5 = document.getElementById("x5");
    var x6 = document.getElementById("x6");
    var check_empty = true;
    var check_only_letters = true;
    var check_date = true;
    var check_phone_num = true;
    var check_gmail = true;
    var check_password = true;
    var code1 = document.getElementById("code1");
    var code2 = document.getElementById("code2");


    //login and sign up trasform start
    signUpBtn.onclick = function () {
        if (!signUpClicked) {
                mover.style.left = "97px";
                signUpClicked = true;
                formContainer.style.height = "86vh";
                formContainer.style.top = "12vh";
                logForm.style.opacity = 0;
                logForm.style.transform = "translateX(100vw)";
                regForm.style.transform = "translateX(0)";
                firstName.classList.add("enterance-id");
                LastName.classList.add("enterance-id");
                date.classList.add("enterance-id");
                gender_conatainer.classList.add("enterance-id");
                email.classList.add("enterance-id");
                phoneNumber.classList.add("enterance-id");
                password.classList.add("enterance-id");
                favorite_car_container.classList.add("enterance-id");
                checkbox_container.classList.add("enterance-id");
                text_area_container.classList.add("enterance-id");
                submit_rest_container.classList.add("enterance-id");
                regForm.style.opacity = "1";
                firstName.style.opacity = "1";
                LastName.style.opacity = "1";
                date.style.opacity = "1";
                gender_conatainer.style.opacity = "1";
                email.style.opacity = "1";
                phoneNumber.style.opacity = "1";
                password.style.opacity = "1";
                favorite_car_container.style.opacity = "1";
                checkbox_container.style.opacity = "1";
                text_area_container.style.opacity = "1";
                submit_rest_container.style.opacity = "1";
        }
    }
    logInBtn.onclick = function () {
        if (signUpClicked) {
            mover.style.left = "calc(99px + 20%)";
            signUpClicked = false;
            formContainer.style.height = "45vh";
            formContainer.style.top = "25vh";
            regForm.style.opacity = "0";
            firstName.classList.add("enterance-id");

            LastName.classList.remove("enterance-id");
            date.classList.remove("enterance-id");
            gender_conatainer.classList.remove("enterance-id");
            email.classList.remove("enterance-id");
            phoneNumber.classList.remove("enterance-id");
            password.classList.remove("enterance-id");
            favorite_car_container.classList.remove("enterance-id");
            checkbox_container.classList.remove("enterance-id");
            text_area_container.classList.remove("enterance-id");
            submit_rest_container.classList.remove("enterance-id");

            firstName.style.opacity = "0";
            LastName.style.opacity = "0";
            date.style.opacity = "0";
            gender_conatainer.style.opacity = "0";
            email.style.opacity = "0";
            phoneNumber.style.opacity = "0";
            password.style.opacity = "0";
            favorite_car_container.style.opacity = "0";
            checkbox_container.style.opacity = "0";
            text_area_container.style.opacity = "0";
            submit_rest_container.style.opacity = "0";
            regForm.style.transform = "translateX(-100vw)";
            logForm.style.transform = "translateX(0)";
        }
        logForm.style.opacity = "1";
        const urlParams = new URLSearchParams(window.location.search);

        if (urlParams.has('code') && urlParams.get('code') === '1') {
            code1.style.opacity = "0";
        }
        if (urlParams.has('code') && urlParams.get('code') === '2') {
            code2.style.opacity = "0";
        }
    }
    //login and sign up transform end


    //OnReset start
    regForm.onreset = function (event) {
        //event.preventDefault();
        validation_container.style.opacity = "0";
        firstName.value = "";
        LastName.value = "";
        date.value = undefined;
        email.value = "";
        phoneNumber.value = "";
        phoneNumber.value = "";
        password.value = "";
        text_area.value = "";
    }

    //OnReset end
    //Validation Check Main Function Start
    regForm.onsubmit = function (event) {
        event.preventDefault();

        resetVars();
        if ((!(T_P_checkbox.checked)) || EmptynessCheck(firstName) || EmptynessCheck(LastName) || EmptynessCheck(email) || EmptynessCheck(phoneNumber) || EmptynessCheck(password)) {
            check_empty = false;
        }
        if (onlyLetters(firstName) || onlyLetters(LastName)) {
            check_only_letters = false;
        }
        if (validateEmail(email)) {
            check_gmail = false;
        }
        if (dateCheck(date.value)) {
            check_date = false;
        }
        if (phoneNumCheck(phoneNumber)) {
            check_phone_num = false;
        }
        if (password.value.length < 8) {
            check_password = false;
        }
        realization();
        setTimeout(function () {
            if (check_empty && check_only_letters && check_gmail && check_date && check_phone_num && check_password) {
                alert("press submit again to send the form");
                regForm.onsubmit = function () {
                    window.location.href = 'Home.aspx';
                };
            }
        }, 1600)
        setTimeout(function () {
            resetVars();
        }, 60000)

    }
    //Validation Check Main Function End


    //Validation Check Secondary Function's Start
    function EmptynessCheck(inp) {

        if (inp.value == "") {
            return true;
        }
        return false;
    }
    function onlyLetters(inp) {
        if (inp.value == "") {
            return true;
        }
        for (var i = 0; i < inp.value.length; i++) {
            var charCode = inp.value.charCodeAt(i);
            if (!((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122))) {
                return true;
            }
        }
        return false;
    }
    function dateCheck(date) {
        if (date === undefined) {
            return true;
        }
        var dateParts = date.split("-");
        var year = parseInt(dateParts[0]);
        var month = parseInt(dateParts[1]);
        var day = parseInt(dateParts[2]);

        var currentYear = new Date().getFullYear();
        var age = currentYear - year;

        if (age > 18) {
            return false; 
        }
        if (age === 18) {
            var currentMonth = new Date().getMonth() + 1;
            if (month < currentMonth) {
                return false;
            }
            else if (month === currentMonth) {
                var currentDay = new Date().getDate();
                if (day <= currentDay) {
                    return false;
                }
            }
        }
        return true;
    }

    function phoneNumCheck(inp) {
        if (inp.value == "") {
            return true;
        }
        if (inp.value[0] != '0') {
            return true;
        }
        if (inp.value[2] != '-' && inp.value[3] != '-') {
            return true;
        }
        for (var i = 0; i < inp.value.length; i++) {
            var charCode = inp.value.charCodeAt(i);
            if (i != 2 && i != 3 && (!(charCode >= 48 && charCode <= 57))) {
                return true;
            }
        }
        return false;
    }

    function validateEmail(email) {
        if (email.value == "" || email.value.indexOf('@') == -1 || email.value.indexOf('.') == -1) {
            return true;
        }
        if (email.value.indexOf('@') == 0||email.value.indexOf('@') == email.value.length - 1) {
            return true;
        }
        if (email.value.indexOf('.') == 0||email.value.indexOf('.') == email.value.length - 1) {
            return true;
        }
        return false;
    }



    function realization() {
        validation_container.style.opacity = 1;
        setTimeout(function () {
            setTimeout(function () {
                if (check_empty) {
                    v1.style.display = "unset";
                }
                else {
                    x1.style.display = "unset";
                }
                setTimeout(function () {
                    if (check_only_letters) {
                        v2.style.display = "unset";
                    }
                    else {
                        x2.style.display = "unset";
                    }
                    setTimeout(function () {
                        if (!check_gender || !check_date) {
                            x3.style.display = "unset";
                        }
                        else {
                            v3.style.display = "unset";
                        }
                        setTimeout(function () {
                            if (check_phone_num) {
                                v4.style.display = "unset";
                            }
                            else {
                                x4.style.display = "unset";
                            }
                            setTimeout(function () {
                                if (check_gmail) {
                                    v5.style.display = "unset";
                                }
                                else {
                                    x5.style.display = "unset";
                                }
                                setTimeout(function () {
                                    if (check_password) {
                                        v6.style.display = "unset";
                                    }
                                    else {
                                        x6.style.display = "unset";
                                    }
                                }, 200)
                            }, 200)
                        }, 200)
                    }, 200)
                }, 200)
            },200)
        },200)
    }
    function resetVars() {
        validation_container.style.opacity = "0";

        check_empty = true;
        check_only_letters = true;
        check_gender = true;
        check_date = true;
        check_phone_num = true;
        check_gmail = true;
        check_password = true;

        v1.style.display = "none";
        v2.style.display = "none";
        v3.style.display = "none";
        v4.style.display = "none";
        v5.style.display = "none";
        v6.style.display = "none";
        x1.style.display = "none";
        x2.style.display = "none";
        x3.style.display = "none";
        x4.style.display = "none";
        x5.style.display = "none";
        x6.style.display = "none";


    }



    //first and last name no unletters start
    firstName.oninput = function () {
        var val = firstName.value;
        var lastChar = val.charAt(val.length - 1);
        var charCode = lastChar.charCodeAt(0);

        if (!((charCode >= 97 && charCode <= 122) || (charCode >= 65 && charCode <= 90))) {
            firstName.value = val.slice(0, -1);
        }
        else if (firstName.value.length == 1 && (charCode >= 97 && charCode <= 122))
            firstName.value = firstName.value.toUpperCase();
    };
    LastName.oninput = function () {
        var val = LastName.value;
        var lastChar = val.charAt(val.length - 1);
        var charCode = lastChar.charCodeAt(0);

        if (!((charCode >= 97 && charCode <= 122) || (charCode >= 65 && charCode <= 90))) {
            LastName.value = val.slice(0, -1);
        }
        else if (LastName.value.length == 1 && (charCode >= 97 && charCode <= 122))
            LastName.value = LastName.value.toUpperCase();
    };
    //first and last name no unletters end

    //phone number no unnumbers and - start
    phoneNumber.oninput = function () {
        var val = phoneNumber.value;
        var lastChar = val.charAt(val.length - 1);
        var charCode = lastChar.charCodeAt(0);

        if (val.length == 0 && charCode != 48) {
            phoneNumber.value = val.slice(0, -1);
        }
        if (!(charCode >= 48 && charCode <= 57) && !(((val.length - 1 == 2 || val.length - 1 == 3) && charCode == 45))) {
            phoneNumber.value = val.slice(0, -1);
        }
    };
    //phone number no unnumbers and - end
    //Validation Check Secondary Function's End

 
});