// Hook up the click events of the log in and log out buttons.
if ($get('btnLogIn') != null) {
    $addHandler($get('btnLogIn'), 'click', loginHandler);
}
if (null != $get('btnLogOut')) {
    $addHandler($get('btnLogOut'), 'click', logoutHandler);
}
if (null != $get('btnCancel')) {
    $addHandler($get('btnCancel'), 'click', CancelLoginHandler);
}

var ssa = Sys.Services.AuthenticationService;
loadRoles();
var profileService = Sys.Services.ProfileService;
var propertyNames = new Array();
var isLoggingIn = false;

if (ssa.get_isLoggedIn() == true) {
    HideElement('dLoginMenu');
    ShowElement('dLogoutMenu');
    LoadProfile();
    ShowProfileInfo();
} else {
    ShowElement('dLoginMenu');
    HideElement('dLogoutMenu');
}

HideElement('LoginDlg');
HideElement('PopUpBackGround');

function DisplayLoginHandler() {

    if (null != $get('LoginDlg'))
        $get('LoginDlg').style.display = 'block';
    if (null != $get('PopUpBackGround'))
        $get('PopUpBackGround').style.display = 'block';
    if (null != $get('AnonymousView'))
        $get('AnonymousView').style.display = 'block';
    if (null != $get('txtUsername'))
        $get('txtUsername').focus();

}

function CancelLoginHandler() {
    $get('LoginDlg').style.display = 'none';
    $get('PopUpBackGround').style.display = 'none';
}

function loginHandler() {
    var username = $get('txtUsername').value;
    var password = $get('pwdPassword').value;
    var isPersistent = $get('chkRememberMe').checked;
    var customInfo = null;
    var redirectUrl = null;
    isLoggingIn = true;
    // Log them in.
    ssa.login(username,
                          password,
                          isPersistent,
                          customInfo,
                          redirectUrl,
                          onLoginComplete,
                          onError);

}

function logoutHandler() {
    // Log them out.
    var redirectUrl = null;
    var userContext = null;
    ssa.logout(redirectUrl,
                           onLogoutComplete,
                           onError,
                           userContext);
}

function onLoginComplete(result, context, methodName) {

    if (result) {

        //        isAdministrator();
        loadRoles();
        // Logged in.  Hide the anonymous view.
        ShowElement('LoggedInView');
        HideElement('AnonymousView');
        HideElement('LoginDlg');
        HideElement('PopUpBackGround');

        HideElement('dLoginMenu');
        ShowElement ('dLogoutMenu');

        LoadProfile();

    } else {
        alert('Sorry those Credentials did not work.');
        $get('txtUsername').value = '';
        $get('pwdPassword').value = '';

    }
}

function isAdministrator() {

    if (Sys.Services.RoleService.isUserInRole('Administrators')) {
        ShowElement('liAdmin');
    } else {
        HideElement('liAdmin');
    }

}

function onLogoutComplete(result, context, methodName) {
    // Logged out.  Hide the logged in view.
    ShowElement('dLoginMenu');
    HideElement('dLogoutMenu');
}

function loadRoles() {
    Sys.Services.RoleService.load(onLoadRolesCompleted, onLoadRolesFailed, null);
}

function onLoadRolesCompleted(result, userContext, methodName) {
    isAdministrator();
}

function onLoadRolesFailed(error, userContext, methodName) {
    alert(error.get_message());
}

function onError(error, context, methodName) {
    alert(error.get_message());
}


// Loads the profile of the current
// authenticated user.
function LoadProfile() {

    propertyNames[0] = "FirstName";
    propertyNames[1] = "LastName";

    Sys.Services.ProfileService.load(null,
	    OnLoadCompleted, OnProfileFailed, null);
    
}

// Reads the profile information and displays it.
function OnLoadCompleted(numProperties, userContext, methodName) {

    firstName = Sys.Services.ProfileService.properties.FirstName;

    if (firstName.length > 0 && isLoggingIn == true) {
        alert("Welcome " + firstName);
    }

    ShowProfileInfo();

}

function ShowProfileInfo() {

    if (null != $get('dUserName')) {

        if (undefined != Sys.Services.ProfileService.properties.FirstName && 
            undefined != Sys.Services.ProfileService.properties.LastName) {

            $get('dUserName').innerHTML =
                '<a href="editprofile.aspx">' + 
	            Sys.Services.ProfileService.properties.FirstName + ' ' +
	            Sys.Services.ProfileService.properties.LastName + '</a>';


        } else {
            $get('dUserName').innerHTML =
	            '<a href="editprofile.aspx">Update Profile</a><BR/>';
        }
    }

}


// This is the callback function called 
// if the profile load or save operations failed.
function OnProfileFailed(error_object, userContext, methodName) {
    alert("Profile service failed with message: " +
	        error_object.get_message());
}


function ValidateLoginValues() {

    if (event.which || event.keyCode) {
        if ((event.which == 13) || (event.keyCode == 13)) {

            Username = $get('txtUsername');
            Password = $get('pwdPassword');

            if (Username.value.length > 0 && Password.value.length > 0) {

                ProcessEnter('btnLogIn');

            } else {

                alert('You must enter both a Username and a Password to authenticate.');

            }
        }
    }

}

function ProcessEnter(btnSubmit) {

    if (event.which || event.keyCode) {
        if ((event.which == 13) || (event.keyCode == 13)) {
            $get(btnSubmit).click();
            return false;
        }
    } else {
        return true;
    };

}

function DisableElement(ElementName) {

    $get(ElementName).Enable == false;

}

function ShowLocation(Childelement, TextElement) {

    bounds = Sys.UI.DomElement.getBounds($get(Childelement));
    alert(bounds.x + ' ' + bounds.y + ' ' + bounds.width + ' ' + bounds.height);

}


function GoOpenId() {

    OpenIdURL = $get(txtOpenId);
    window.open(OpenIdURL.value, 'MyOpenId', null, null);

}



function ConfirmMsg(entityName) {
    return confirm('Warning: This will delete the ' + entityName + ' from the database.');
}


function SelectAllCheckBoxes(id) {
    var frm = document.forms[0];

    for (i = 0; i < frm.elements.length; i++) {
        if (frm.elements[i].type == "checkbox") {
            frm.elements[i].checked = document.getElementById(id).checked;
        }
    }
}

function SubscribeToNewsletter() {
    var email = window.document.getElementById('NewsletterEmail').value;
    window.document.location.href = 'Register.aspx?Email=' + email;
}

function ShowElement(ElementName) {
    if (null != $get(ElementName))
        $get(ElementName).style.display = 'block';
}

function HideElement(ElementName) {
    if (null != $get(ElementName))
        $get(ElementName).style.display = 'none';
}

function toggleDivState(divName) {
    var ctl = $get(divName);

    if (ctl.style.display == "none") {
        ShowElement(divName);
    } else {
        HideElement(divName);
    }
}
