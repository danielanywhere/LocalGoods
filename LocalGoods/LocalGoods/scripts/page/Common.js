//	CityItemID, CityItemTicket, CityName.
var cityData = [];
//	Flag - Cities have been loaded.
var loadedCities = false;
//	Flag - Thumbnails have been loaded.
var loadedThumbnails = false;
//	CityItemID, Email, Password, Username, UserTicket
//	Only used when editing profile.
var userData = {};

//----------
// activateLoader
//----------
/**
	* Activate the loader modal control.
	* @param {string} waitMessage Optional message to display while loading.
	*/
function activateLoader(waitMessage)
{
	var content = "";
	var message = "Loading...";

	if(waitMessage)
	{
		message = waitMessage;
	}

	$("#loader").remove();
	content = `<div id="loader" class="loader-modal">`;
	content += `<div class="loader-modal-content ` +
		`lead rounded-lg">${message}<br />`;
	content += `<img src="images/ovalloader.svg" width="50" />`;
	content += `<div id="loaderProgress" style="display:none">`;
	content += `<p>&nbsp;</p>`;
	content += `<div class="progress">`;
	content += `<div id="loaderProgressBar" class="progress-bar" ` +
		`role="progressbar" ` +
		`aria-valuenow="1" aria-valuemin="0" aria-valuemax="100" ` +
		`style="width:1%">`;
	content += `<span class="sr-only">1% Complete</span>`;
	content += `</div>`;
	content += `</div>`;
	content += `</div>`;
	content += `</div>`;
	content += `</div>`;
	$("body").append(content);
}
//----------

//----------
// aGet
//----------
/**
	* Send a GET request to the server, and optionally process result in
	* callback.
	* @param {string} url The URL to open with the request.
	* @param {string} data URL formatted Name/Value pairs.
	* @param {object} callback Reference to the callback function.
	* @returns {undefined}
	*/
function aGet(url, data, callback)
{
	var response = { "ServerNotFound": true };
	var xhttp = new XMLHttpRequest();
	try
	{
		xhttp.onreadystatechange = function()
		{
			if(this.readyState == 4 && callback)
			{
				if(this.status == 200)
				{
					console.log(`aGet: Received ${this.responseText}`);
					response = JSON.parse(this.responseText);
				}
				else
				{
					console.log(`aGet: Server response ${this.status}`);
				}
				console.log(`aGet: Calling callback for ${url}`);
				callback(response);
			}
		};
	}
	catch(ex)
	{
		console.log(ex);
	}
	xhttp.open("GET", url + (data ? "?" + data : ""), true);
	xhttp.send();
	console.log(`aGet: Request sent for ${url}`);
}
//----------

//----------
// aPost
//----------
/**
	* Send a POST request to the server, and optionally process result in
	* callback.
	* @param {string} url The URL to open with the request.
	* @param {object} data JSON object or array to send to the server.
	* @param {object} callback Reference to the callback function.
	* @returns {undefined}
	*/
function aPost(url, data, callback)
{
	var body = "";
	var responseObj = { "ServerNotFound": true };
	var xhttp = new XMLHttpRequest();
	try
	{
		xhttp.onreadystatechange = function()
		{
			if(this.readyState == 4 && callback)
			{
				if(this.status == 200)
				{
					console.log(`aPost: Result received: ${this.responseText}...`);
					responseObj = JSON.parse(this.responseText);
				}
				callback(responseObj);
			}
		};
		xhttp.open("POST", url);
		xhttp.setRequestHeader("Content-Type", "application/json; charset=utf-8");
		xhttp.setRequestHeader("Accept", "application/json");
		body = JSON.stringify(data);
		// console.log(`aPost: Prepare to send body ${body}`);
		xhttp.send(body);
	}
	catch(ex)
	{
		console.log(ex);
	}
}
//----------

//----------
// aPostForm
//----------
/**
	* Send a POST request to the server within a single Payload form variable,
	* and optionally process result in callback.
	* @param {string} url The URL to open with the request.
	* @param {object} data JSON object or array to send to the server.
	* @param {object} callback Reference to the callback function.
	* @returns {undefined}
	*/
function aPostForm(url, data, callback)
{
	var body = "";
	var formData = new FormData();
	var responseObj = { "ServerNotFound": true };
	var xhttp = new XMLHttpRequest();
	try
	{
		xhttp.onreadystatechange = function()
		{
			if(this.readyState == 4 && callback)
			{
				if(this.status == 200)
				{
					console.log(`aPostForm: Result received: ${this.responseText}...`);
					responseObj = JSON.parse(this.responseText);
				}
				callback(responseObj);
			}
		};
		xhttp.open("POST", url);
		// xhttp.setRequestHeader("Content-Type", "application/json; charset=utf-8");
		xhttp.setRequestHeader("Accept", "application/json");
		body = JSON.stringify(data);
		formData.append("Payload", body);
		// console.log(`aPost: Prepare to send body ${body}`);
		xhttp.send(formData);
	}
	catch(ex)
	{
		console.log(ex);
	}
}
//----------

//----------
// aPostSync
//----------
/**
	* Send a POST request to the server, and return the result.
	* @param {string} url The URL to open with the request.
	* @param {object} data JSON object or array to send to the server.
	* @param {object} callback Reference to the callback function.
	* @returns {object} The object or array returned from the server.
	*/
function aPostSync(url, data)
{
	var body = "";
	var canContinue = false;
	var responseObj = { "ServerNotFound": true };
	var xhttp = new XMLHttpRequest();
	try
	{
		xhttp.onreadystatechange = function()
		{
			if(this.readyState == 4)
			{
				if(this.status == 200)
				{
					console.log(`aPostSync: Result received: ${this.responseText}...`);
					responseObj = JSON.parse(this.responseText);
					canContinue = true;
				}
			}
		};
		xhttp.open("POST", url, false);
		xhttp.setRequestHeader("Content-Type", "application/json; charset=utf-8");
		xhttp.setRequestHeader("Accept", "application/json");
		body = JSON.stringify(data);
		// console.log(`aPost: Prepare to send body ${body}`);
		xhttp.send(body);
		// while(!canContinue)
		// {
		// 	console.log("Sleep for a second.");
		// }
		return responseObj;
	}
	catch(ex)
	{
		console.log(ex);
	}
}
//----------

//----------
// aPut
//----------
/**
	* Set a PUT request to the server.
	* @param {string} url The URL at which to place the data.
	* @param {string} data URL formatted Name/Value pairs.
	* @returns {undefined}
	*/
function aPut(url, data)
{
	var xhttp = new XMLHttpRequest();
	try
	{
		xhttp.onreadystatechange = function()
		{
			if(this.readyState == 4)
			{
				if(this.status == 200)
				{
				}
				else
				{
					console.log(`aPut: Server response ${this.status}`);
				}
			}
		};
	}
	catch(ex)
	{
		console.log(ex);
	}
	xhttp.open("PUT", url + (data ? "?" + data : ""), true);
	xhttp.send();
}
//----------

// //----------
// // btnNewCityNameClicked
// //----------
// /**
// 	* The new city name submit button has been clicked.
// 	*/
// function btnNewCityNameClicked()
// {
// 	var data = {};

// 	data.CityName = $("#txtNewCityName").val();
// 	aPost("/api/v1/homerequestcity", data);
// 	$("#dialog-requestnewcity-container").css("display", "none");
// }
// //----------

//----------
// clearLogin
//----------
/**
	* Clear the login cookie information.
	*/
function clearLogin()
{
	var expires = "expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
	document.cookie = `username=; `;
	document.cookie = `userTicket=; `;
	console.log("Login values cleared...");
}
//----------

//----------
// dialogLogin
//----------
/**
	* Display the modal login dialog over the currently loaded screen.
	* Behavior: Reload the parent page on OK or Cancel.
	* @returns {undefined}
	*/
function dialogLogin()
{
	$("#dialog-login-container").css("display", "block");
}
//----------

//----------
// dialogLoginPrep
//----------
/**
	* Preparation for on-page presentation of modal login dialog over the
	* currently loaded screen.
	* Behavior: Reload the parent page on OK or Cancel.
	* @returns {undefined}
	*/
function dialogLoginPrep()
{
	var crd = "";
	var $sec = $("#pageContainer");

	console.log("Preparing login dialog...");
	//	Remove the section if it has previously been placed.
	$("#dialog-login-container").remove();
	crd += `<div id="dialog-login-container" class="loader-modal">`;
	crd += `<div id="dialog-login" class="card">`;
	crd += `<div class="card-body">`;
	crd += `<h3>Login - LocalGoods</h3>`;
	crd += `<div class="alert alert-danger" role="alert" data-bind="visible: isBadUsername">Bad username or password...</div>`;
	crd += `<div class="alert alert-danger" role="alert" data-bind="visible: isServerNotFound">Server Not Found...</div>`;
	crd += `<table style="border: 0">`;
	crd += `<tr>`;
	crd += `<td>Email address:</td>`;
	crd += `<td><input id="txtLoginEmailAddress" class="form-control" type="text" data-bind="value: emailAddress, valueUpdate: 'afterkeydown'" placeholder="Enter your email address" /></td>`;
	crd += `<td id="loginStatEmail"><img data-bind="visible: isEmailAddressValid" src="images/StatusV.png" /><img data-bind="hidden: isEmailAddressValid" src="images/StatusX.png" /></td>`;
	crd += `</tr>`;
	crd += `<tr>`;
	crd += `<td>Password:</td>`;
	crd += `<td><input id="txtLoginPassword" class="form-control" type="password" data-bind="value: password1, valueUpdate: 'afterkeydown'" placeholder="Enter your password" /></td>`;
	crd += `<td id="loginStatPassword"><img data-bind="visible: isPasswordValid" src="images/StatusV.png" /><img data-bind="hidden: isPasswordValid" src="images/StatusX.png" /></td>`;
	crd += `</tr>`;
	crd += `<tr>`;
	crd += `<td colspan="2" style="text-align: right;">`;
	crd += `<button id="btnLoginSubmit" type="button" class="btn btn-primary" data-bind="enable: isLoginInfoValid, click: doLogin">Login</button>`;
	crd += `</td>`;
	crd += `<td>`;
	crd += `<button id="btnLoginCancel" type="button" class="btn btn-primary" data-bind="click: clickCancel">Cancel</button>`;
	crd += `</td>`;
	crd += `</tr>`;
	crd += `</table>`;
	crd += `</div>`;
	crd += `</div>`;
	crd += `</div>`;
	$sec.append(crd);
	$("#txtLoginEmailAddress").keydown(function(e)
	{
		if(e.keyCode == 13)
		{
			//	Enter pressed.
			if(!$("#btnLoginSubmit").prop("disabled"))
			{
				$("#btnLoginSubmit").click();
			}
		}
	});
	$("#txtLoginPassword").keydown(function(e)
	{
		if(e.keyCode == 13)
		{
			//	Enter pressed.
			if(!$("#btnLoginSubmit").prop("disabled"))
			{
				$("#btnLoginSubmit").click();
			}
		}
	});
}
//----------

//----------
// dialogRequestNewCity
//----------
/**
	* Present the modal dialog for requesting a new city name.
	* @description Note that this dialog does not require knockout MVVM
	* bindings, so is loaded onto the page on-demand.
	*/
function dialogRequestNewCity()
{
	var crd = "";
	var element = null;
	var elementi = null;
	var $sec = $("#pageContainer");

	console.log("Preparing new city request dialog...");
	//	Remove any previous instance of the dialog.
	$("#dialog-requestnewcity-container").remove();
	crd += `<div id="dialog-requestnewcity-container" class="loader-modal" ` +
		`>`;
	crd += `<div id="dialog-requestnewcity" class="card">`;
	crd += `<div class="card-body">`;
	crd += `<h3>Request New City</h3>`;
	crd += `<table style="border:0">`;
	crd += `<tr>`;
	crd += `<td>New City Name: </td>`;
	crd += `<td>`;
	crd += `<input type="text" id="txtNewCityName" class="form-control" ` +
		`value="" placeholder="Name of the new city." ` +
		`/>`;
	crd += `</td>`;
	crd += `</tr>`;
	crd += `<tr>`;
	crd += `<td colspan="2" style="text-align: right">`;
	crd += `<button id="btnNewCityName" class="btn btn-primary" ` +
		`disabled>Submit</button>`;
	crd += `</td>`;
	crd += `</tr>`;
	crd += `</table>`;
	crd += `</div>`;
	crd += `</div>`;
	crd += `</div>`;
	$sec.append(crd);
	$("#btnNewCityName").click(function()
	{
		var data = {};

		data.CityName = $("#txtNewCityName").val();
		console.log(`Requesting: ${data.CityName}`);
		aPost("/api/v1/homerequestcity", data);
		$("#dialog-requestnewcity-container").css("display", "none");
		$("#dialog-requestnewcity-container").off("click");
		$("#dialog-requestnewcity").off("click");
		$("#dialog-requestnewcity-container").off("keydown");
		$("#txtNewCityName").off("keydown");
		$("#btnNewCityName").off("click");
	});
	$("#txtNewCityName").keydown(function(e)
	{
		var txt = $("#txtNewCityName").val();

		if(txt)
		{
			$("#btnNewCityName").prop("disabled", false);
			if(e.keyCode == 13)
			{
				//	Enter was pressed. Click the button.
				$("#btnNewCityName").click();
			}
		}
		else
		{
			$("#btnNewCityName").prop("disabled", true);
		}
	
	});
	$("#txtNewCityName").focus();
	$("#dialog-requestnewcity-container").click(function()
	{
		// console.log(`Container click. Dialog clicked? ${viewModel.dialogClicked}...`);
		if(!viewModel.dialogClicked)
		{
			$("#dialog-requestnewcity-container").css("display", "none");
		}
		viewModel.dialogClicked = false;
	});
	$("#dialog-requestnewcity").click(function()
	{
		// console.log("Dialog click...");
		viewModel.dialogClicked = true;
	});
	$("#dialog-requestnewcity-container").keydown(function(e)
	{
		// console.log(`Container key.`);
		if(e.keyCode == 27)
		{
			$("#dialog-requestnewcity-container").css("display", "none");
			$("#dialog-requestnewcity-container").off("click");
			$("#dialog-requestnewcity").off("click");
			$("#dialog-requestnewcity-container").off("keydown");
			$("#txtNewCityName").off("keydown");
			$("#btnNewCityName").off("click");
		}
	});
}
//----------

//----------
// dialogSignup
//----------
/**
	* Present the modal signup dialog over the currently loaded screen.
	* Behavior: Reload the parent page on OK or Cancel.
	* @returns {undefined}
	*/
function dialogSignup()
{
	$("#dialog-signup-container").css("display", "block");
}
//----------

//----------
// dialogSignupPrep
//----------
/**
	* Preparation for on-page presentation of modal signup dialog over the
	* currently loaded screen.
	* Behavior: Reload the parent page on OK or Cancel.
	* @returns {undefined}
	*/
function dialogSignupPrep()
{
	var crd = "";
	var $sec = $("#pageContainer");

	console.log("Preparing signup dialog...");
	//	Remove the section if it has previously been placed.
	$("#dialog-signup-container").remove();
	crd += `<div id="dialog-signup-container" class="loader-modal">`;
	crd += `<div id="dialog-signup" class="card">`;
	crd += `<div class="card-body">`;
	crd += `<h3 id="lblSignupTitle">Signup - LocalGoods</h3>`;
	crd += `<div class="alert alert-danger" role="alert" data-bind="visible: isBadUsername">Bad username or password...</div>`;
	crd += `<div class="alert alert-danger" role="alert" data-bind="visible: isServerNotFound">Server Not Found...</div>`;
	crd += `<table style="border: 0">`;
	crd += `<tr>`;
	crd += `<td>Display name:</td>`;
	crd += `<td><input id="txtDisplayName" class="form-control" type="text" data-bind="value: username, valueUpdate: 'afterkeydown', event: { keyup: checkUsername }" placeholder="Create a display username" /></td>`;
	crd += `<td id="statDisplayName"><img data-bind="visible: isAvailableUsername" src="images/StatusV.png" /><img data-bind="hidden: isAvailableUsername" src="images/StatusX.png" /></td>`;
	crd += `</tr>`;
	crd += `<tr>`;
	crd += `<td>Email address:</td>`;
	crd += `<td><input id="txtEmailAddress" class="form-control" type="text" data-bind="value: emailAddress, valueUpdate: 'afterkeydown', event: { keyup: checkEmail }" placeholder="Enter your email address" /></td>`;
	crd += `<td id="statEmail"><img data-bind="visible: isAvailableEmail" src="images/StatusV.png" /><img data-bind="hidden: isAvailableEmail" src="images/StatusX.png" /></td>`;
	crd += `</tr>`;
	crd += `<tr>`;
	crd += `<td>Password:</td>`;
	crd += `<td><input id="txtPassword1" class="form-control" type="password" data-bind="value: password1, valueUpdate: 'afterkeydown'" placeholder="Create a password" /></td>`;
	crd += `<td id="statPassword1"><img data-bind="hidden: isPasswordMatch" src="images/StatusX.png" /><img data-bind="visible: isPasswordMatch" src="images/StatusV.png" /></td>`;
	crd += `</tr>`;
	crd += `<tr>`;
	crd += `<td>&nbsp;</td>`;
	crd += `<td><input id="txtPassword2" class="form-control" type="password" data-bind="value: password2, valueUpdate: 'afterkeydown'" placeholder="Re-enter your password" /></td>`;
	crd += `<td id="statPassword2">&nbsp;</td>`;
	crd += `</tr>`;
	crd += `<tr>`;
	crd += `<td colspan="2" style="text-align: right;">`;
	crd += `<button id="btnSignupSubmit" type="button" class="btn btn-primary" data-bind="enable: isSignupInfoValid, click: doSignup">Signup</button>`;
	crd += `</td>`;
	crd += `<td>`;
	crd += `<button id="btnSignupCancel" type="button" class="btn btn-primary" data-bind="click: clickCancel">Cancel</button>`;
	crd += `</td>`;
	crd += `</tr>`;
	crd += `</table>`;
	crd += `</div>`;
	crd += `</div>`;
	crd += `</div>`;
	$sec.append(crd);
	$("#txtDisplayName").keydown(function(e)
	{
		if(e.keyCode == 13)
		{
			//	Enter pressed.
			if(!$("#btnSignupSubmit").prop("disabled"))
			{
				$("#btnSignupSubmit").click();
			}
		}
	});
	$("#txtEmailAddress").keydown(function(e)
	{
		if(e.keyCode == 13)
		{
			//	Enter pressed.
			if(!$("#btnSignupSubmit").prop("disabled"))
			{
				$("#btnSignupSubmit").click();
			}
		}
	});
	$("#txtPassword1").keydown(function(e)
	{
		if(e.keyCode == 13)
		{
			//	Enter pressed.
			if(!$("#btnSignupSubmit").prop("disabled"))
			{
				$("#btnSignupSubmit").click();
			}
		}
	});
	$("#txtPassword2").keydown(function(e)
	{
		if(e.keyCode == 13)
		{
			//	Enter pressed.
			if(!$("#btnSignupSubmit").prop("disabled"))
			{
				$("#btnSignupSubmit").click();
			}
		}
	});
}
//----------

//----------
// dialogUpdateUser
//----------
/**
	* Update user information.
	*/
function dialogUpdateUser()
{
	var crd = "";
	var data = {};
	var $sec = $("#pageContainer");

	data.UserTicket = getCookieValue("userTicket");
	data.Username = getCookieValue("username");
	aPost("/api/v1/signupretrieveprofile", data, dialogUpdateUserFill);
	viewModel.userTicket = getCookieValue("userTicket");
	// $("#dialog-updateuser-container").css("display", "block");
	console.log("Preparing update user dialog...");
	//	Remove the section if it has previously been placed.
	$("#dialog-updateuser-container").remove();
	crd += `<div id="dialog-updateuser-container" class="loader-modal">`;
	crd += `<div id="dialog-updateuser" class="card">`;
	crd += `<div class="card-body">`;
	crd += `<h3 id="lblUpdateTitle">Update Profile - LocalGoods</h3>`;
	crd += `<div id="divUpdateBadUsername" class="alert alert-danger" role="alert" style="display:none">Bad username or password...</div>`;
	crd += `<div id="divUpdateServerNotFound" class="alert alert-danger" role="alert" style="display:none">Server Not Found...</div>`;
	crd += `<table style="border: 0">`;
	crd += `<tr>`;
	crd += `<td>Display name:</td>`;
	crd += `<td><input id="txtUpdateDisplayName" class="form-control" type="text" placeholder="Create a display username" /></td>`;
	crd += `<td id="statUpdateDisplayName"><img id="statUpdateDisplayAvailable" src="images/StatusV.png" style="display:inline" /><img id="statUpdateDisplayUnavailable" src="images/StatusX.png" style="display:none" /></td>`;
	crd += `</tr>`;
	crd += `<tr>`;
	crd += `<td>Email address:</td>`;
	crd += `<td><input id="txtUpdateEmailAddress" class="form-control" type="text" placeholder="Enter your email address" /></td>`;
	crd += `<td id="statUpdateEmail"><img id="statUpdateEmailAvailable" src="images/StatusV.png" style="display:inline" /><img id="statUpdateEmailUnavailable" src="images/StatusX.png" style="display:none" /></td>`;
	crd += `</tr>`;
	crd += `<tr>`;
	crd += `<td>Password:</td>`;
	crd += `<td><input id="txtUpdatePassword1" class="form-control" type="password" placeholder="Create a password" /></td>`;
	crd += `<td id="statUpdatePassword1"><img id="statUpdatePasswordMismatch" src="images/StatusX.png" style="display:none" /><img id="statUpdatePasswordMatch" src="images/StatusV.png" style="display:inline" /></td>`;
	crd += `</tr>`;
	crd += `<tr>`;
	crd += `<td>&nbsp;</td>`;
	crd += `<td><input id="txtUpdatePassword2" class="form-control" type="password" placeholder="Re-enter your password" /></td>`;
	crd += `<td id="statUpdatePassword2">&nbsp;</td>`;
	crd += `</tr>`;
	crd += `<tr>`;
	crd += `<td colspan="2" style="text-align: right;">`;
	crd += `<button id="btnUpdateProfileSubmit" type="button" class="btn btn-primary" data-bind="enable: isSignupInfoValid, click: doSignup">Update</button>`;
	crd += `</td>`;
	crd += `<td>`;
	crd += `<button id="btnUpdateProfileCancel" type="button" class="btn btn-primary" data-bind="click: clickCancel">Cancel</button>`;
	crd += `</td>`;
	crd += `</tr>`;
	crd += `</table>`;
	crd += `</div>`;
	crd += `</div>`;
	crd += `</div>`;
	$sec.append(crd);
	viewModel.isAvailableEmail(false);
	viewModel.isAvailableUsername(false);
	viewModel.dialogClicked = false;
	$("#dialog-updateuser-container").click(function()
	{
		// console.log(`Container click. Dialog clicked? ${viewModel.dialogClicked}...`);
		if(!viewModel.dialogClicked)
		{
			$("#dialog-updateuser-container").css("display", "none");
		}
		viewModel.dialogClicked = false;
	});
	$("#dialog-updateuser").click(function()
	{
		// console.log("Dialog click...");
		viewModel.dialogClicked = true;
	});
	$("#dialog-updateuser-container").keydown(function(e)
	{
		// console.log(`Container key.`);
		if(e.keyCode == 27)
		{
			$("#dialog-updateuser-container").css("display", "none");
		}
	});
	$("#btnUpdateProfileCancel").click(function(e)
	{
		//	Update profile cancel clicked.
		$("#dialog-updateuser-container").css("display", "none");
	});
	$("#btnUpdateProfileSubmit").click(function(e)
	{
		//	Update profile submit clicked.
		var data = {};
		data.CityItemID = userData.CityItemID;
		data.UserTicket = userData.UserTicket;
		data.Email = $("#txtUpdateEmailAddress").val();
		data.Username = $("#txtUpdateDisplayName").val();
		data.Password = $("#txtUpdatePassword1").val();
		userData.Username = data.Username;
		aPost("/api/v1/signupupdateprofile", data,
			dialogUpdateUserSubmitFinish);
		$("#dialog-updateuser-container").css("display", "none");
	});
	$("#txtUpdateDisplayName").keyup(function(e)
	{
		if(e.keyCode == 13)
		{
			//	Enter pressed.
			if(!$("#btnUpdateProfileSubmit").prop("disabled"))
			{
				$("#btnUpdateProfileSubmit").click();
			}
		}
		else
		{
			dialogUpdateUserCheckUsername();
		}
	});
	$("#txtUpdateEmailAddress").keyup(function(e)
	{
		if(e.keyCode == 13)
		{
			//	Enter pressed.
			if(!$("#btnUpdateProfileSubmit").prop("disabled"))
			{
				$("#btnUpdateProfileSubmit").click();
			}
		}
		else
		{
			dialogUpdateUserCheckEmail();
		}
	});
	$("#txtUpdatePassword1").keyup(function(e)
	{
		if(e.keyCode == 13)
		{
			//	Enter pressed.
			if(!$("#btnUpdateProfileSubmit").prop("disabled"))
			{
				$("#btnUpdateProfileSubmit").click();
			}
		}
		else
		{
			dialogUpdateUserCheckPassword();
		}
	});
	$("#txtUpdatePassword2").keyup(function(e)
	{
		if(e.keyCode == 13)
		{
			//	Enter pressed.
			if(!$("#btnUpdateProfileSubmit").prop("disabled"))
			{
				$("#btnUpdateProfileSubmit").click();
			}
		}
		else
		{
			dialogUpdateUserCheckPassword();
		}
	});

}
//----------

//----------
// dialogUpdateUserCheckEmail
//----------
/**
	* Check the email to see if it is valid.
	*/
function dialogUpdateUserCheckEmail()
{
	var data = {};
	var text = $("#txtUpdateEmailAddress").val();

	if(text.toLowerCase() == userData.Email.toLowerCase())
	{
		dialogUpdateUserValidateEmail(true);
	}
	else if(text.length > 0)
	{
		data.Name = "Email";
		data.Value = text;
		aPost("/api/v1/signupemailcheck", data,
			dialogUpdateUserCheckEmailFinish);
	}
	else
	{
		dialogUpdateUserValidateEmail(false);
	}
	dialogUpdateUserEnableSubmit();
}
//----------

//----------
// dialogUpdateUserCheckEmailFinish
//----------
/**
	* Process returning validation from server about an email value.
	* @param {object} data Email validation.
	* @returns {undefined}
	*/
function dialogUpdateUserCheckEmailFinish(data)
{
	if(data)
	{
		if(data.Name == $("#txtUpdateEmailAddress").val())
		{
			//	Matching try.
			if(data.Available)
			{
				dialogUpdateUserValidateEmail(true);
			}
			else
			{
				dialogUpdateUserValidateEmail(false);
			}
		}
	}
	dialogUpdateUserEnableSubmit();
}
//----------
	
//----------
// dialogUpdateUserCheckPassword
//----------
/**
	* Check the password to see if it is valid.
	*/
function dialogUpdateUserCheckPassword()
{
	if(dialogUpdateUserPasswordIsValid())
	{
		$("#statUpdatePasswordMatch").css("display", "inline");
		$("#statUpdatePasswordMismatch").css("display", "none");
	}
	else
	{
		$("#statUpdatePasswordMatch").css("display", "none");
		$("#statUpdatePasswordMismatch").css("display", "inline");
	}
	dialogUpdateUserEnableSubmit()
}
//----------

//----------
// dialogUpdateUserCheckUsername
//----------
/**
	* Check the username to see if it is valid.
	*/
function dialogUpdateUserCheckUsername()
{
	var data = {};
	var text = $("#txtUpdateDisplayName").val();

	if(text.toLowerCase() == userData.Username.toLowerCase())
	{
		dialogUpdateUserValidateUsername(true);
	}
	else if(text.length > 0)
	{
		data.Name = "Username";
		data.Value = text;
		aPost("/api/v1/signupusernamecheck", data,
			dialogUpdateUserCheckUsernameFinish);
	}
	else
	{
		viewModel.isAvailableUsername(false);
	}
	dialogUpdateUserEnableSubmit();
}
//----------

//----------
// dialogUpdateUserCheckUsernameFinish
//----------
/**
	* Returning validation from server about a username value.
	* @param {object} data Username validation.
	*/
function dialogUpdateUserCheckUsernameFinish(data)
{
	if(data)
	{
		if(data.Name == $("#txtUpdateDisplayName").val())
		{
			//	Matching try.
			if(data.Available)
			{
				dialogUpdateUserValidateUsername(true);
			}
			else
			{
				dialogUpdateUserValidateUsername(false);
			}
		}
	}
	dialogUpdateUserEnableSubmit();
}
//----------

//----------
// dialogUpdateUserDisplayNameIsValid
//----------
/**
	* Return a value indicating whether the user display name is valid.
	* The text in txtUpdateDisplayName is either equal to userData.Username,
	* or viewModel.isAvailableUsername is equal to true.
	*/
function dialogUpdateUserDisplayNameIsValid()
{
	var result = false;
	var text = $("#txtUpdateDisplayName").val();

	if(text.toLowerCase() == userData.Username.toLowerCase())
	{
		result = true;
	}
	else if(viewModel.isAvailableUsername())
	{
		result = true;
	}
	return result;
}
//----------

//----------
// dialogUpdateUserEmailAddressIsValid
//----------
/**
	* Return a value indicating whether the user email address is valid.
	* The text in txtUpdateEmailAddress is either equal to userData.Email,
	* or viewModel.isAvailableEmail is equal to true.
	*/
function dialogUpdateUserEmailAddressIsValid()
{
	var result = false;
	var text = $("#txtUpdateEmailAddress").val();

	if(text.toLowerCase() == userData.Email.toLowerCase())
	{
		result = true;
	}
	else if(viewModel.isAvailableEmail())
	{
		result = true;
	}
	return result;
}
//----------

//----------
// dialogUpdateUserEnableSubmit
//----------
/**
	* Handling the enabling of the submit button depending on whether
	* all values are ready.
	* @returns {undefined}
	*/
function dialogUpdateUserEnableSubmit()
{
	var enabled = dialogUpdateUserDisplayNameIsValid() &&
		dialogUpdateUserEmailAddressIsValid() &&
		dialogUpdateUserPasswordIsValid();
	$("#btnUpdateProfileSubmit").prop("disabled", !enabled);
}
//----------

//----------
// dialogUpdateUserFill
//----------
/**
	* Populate the user update dialog with user's profile info.
	* @param {object} data SignupItem containing user's profile data.
	* @returns {undefined}
	*/
function dialogUpdateUserFill(data)
{
	userData = data;
	if(data && data.UserTicket)
	{
		$("#txtUpdateDisplayName").val(data.Username);
		$("#txtUpdateEmailAddress").val(data.Email);
		$("#txtUpdatePassword1").val(data.Password);
		$("#txtUpdatePassword2").val(data.Password);
		dialogUpdateUserCheckPassword();
	}
}
//----------
	
//----------
// dialogUpdateUserPasswordIsValid
//----------
/**
	* Return a value indicating whether the user password is valid.
	* Both passwords must match.
	* @returns {boolean} True if password is valid. Otherwise, false.
	*/
function dialogUpdateUserPasswordIsValid()
{
	var val = $("#txtUpdatePassword1").val();
	return val.length > 0 && val == $("#txtUpdatePassword2").val();
}
//----------

//----------
// dialogUpdateUserSubmitFinish
//----------
/**
	* The server has responded with OK to the user profile change.
	* @param {object} data Response from server.
	*/
function dialogUpdateUserSubmitFinish(data)
{
	if(data && data.Message == "User profile updated...")
	{
		console.log(`User profile updated: ${userData.Username}...`);
		setCookieValue("userTicket", userData.UserTicket);
		setCookieValue("username", userData.Username);
	}
}
//----------

//----------
// dialogUpdateUserValidateEmail
//----------
/**
	* Validate the user's email address.
	* @param {boolean} value Value indicating whether new email is valid.
	*/
function dialogUpdateUserValidateEmail(value)
{
	viewModel.isAvailableEmail(value);
	if(value)
	{
		$("#statUpdateEmailAvailable").css("display", "inline");
		$("#statUpdateEmailUnavailable").css("display", "none");
	}
	else
	{
		$("#statUpdateEmailAvailable").css("display", "none");
		$("#statUpdateEmailUnavailable").css("display", "inline");
	}
}
//----------

//----------
// dialogUpdateUserValidateUsername
//----------
/**
	* Set the condition of the username validated or invalidated.
	* @param {boolean} value Value indicating whether the username is valid.
	* @returns {undefined}
	*/
function dialogUpdateUserValidateUsername(value)
{
	viewModel.isAvailableUsername(value);
	if(value)
	{
		$("#statUpdateDisplayAvailable").css("display", "inline");
		$("#statUpdateDisplayUnavailable").css("display", "none");		
	}
	else
	{
		$("#statUpdateDisplayAvailable").css("display", "none");
		$("#statUpdateDisplayUnavailable").css("display", "inline");
	}
}
//----------

//----------
// getCookieLoggedIn
//----------
/**
	* Return a value indicating whether this user is logged in.
	* @returns {boolean} Value indicating whether the user is logged in.
	*/
function getCookieLoggedIn()
{
	var rv = (getCookieValue("username").length > 0 &&
		getCookieValue("userTicket").length > 0);
	return rv;
}
//----------
	
//----------
// getCookieValue
//----------
	/**
	* Return the value of the specified entry in the cookie.
	* @param {string} cookieName Name of the cookie value to retrieve.
	* @returns {string} Value found for the specified cookie.
	*/
function getCookieValue(cookieName)
{
	var c = "";
	var ca = [];
	var decodedCookie = "";
	var i = 0;
	var name = cookieName + "=";
	var rv = "";

	decodedCookie = decodeURIComponent(document.cookie);
	ca = decodedCookie.split(';');

	for(; i < ca.length; i++)
	{
			c = ca[i];
			while(c.charAt(0) == ' ')
			{
				c = c.substring(1);
			}
			if(c.indexOf(name) == 0)
			{
					rv = c.substring(name.length, c.length);
					break;
			}
	}
	return rv;
}
//----------

//----------
// getQueryValue
//----------
/**
	* Return the value of the specified query.
	* @param {string} name Name of the query parameter to return.
	* @returns {string} Value of the specified query, if found. Otherwise,
	* empty string.
	*/
function getQueryValue(name)
{
	var queryString = window.location.search;
	var searchParams = null;
	var value = "";

	if(queryString.toLowerCase().indexOf(name.toLowerCase()) > -1)
	{
		searchParams = new URLSearchParams(queryString);
		if(searchParams.has(name))
		{
			//	The name/value pair exists.
			value = searchParams.get(name);
		}
	}
	return value;
}
//----------

//----------
// koViewModel
//----------
/**
	* Local view model for UI on knockout MVVM.
	* @returns {function} Knockout View Model (VM).
	*/
function koViewModel()
{
	var self = this;

	//	*** Variables ***

	self.canEdit = ko.observable(false);
	self.catalogItemID = ko.observable(0);
	self.checkEmailPostTimer = null;
	self.checkUsernamePostTimer = null;
	self.cityID = ko.observable(1);
	self.cityName = ko.observable("Laramie, WY");
	self.dialogClicked = false;
	self.emailAddress = ko.observable("");
	self.isAvailableEmail = ko.observable(false);
	self.isAvailableUsername = ko.observable(false);
	self.isBadUsername = ko.observable(false);
	self.isDebug = false;
	self.isServerNotFound = ko.observable(false);
	self.itemCount = ko.observable(0);
	self.leadSuffix = ko.observable("");
	self.loggedIn = false;
	self.password1 = ko.observable("");
	self.password2 = ko.observable("");
	self.titleSuffix = ko.observable("");
	self.username = ko.observable("");
	self.userTicket = "";

	//	*** Functions ***
	//----------
	// checkEmail
	//----------
	/**
		* Set the timer to request availability of email check.
		* @returns {undefined}
	 */
	self.checkEmail = function()
	{
		if(self.checkEmailPostTimer)
		{
			clearTimeout(self.checkEmailPostTimer);
		}

		self.checkEmailMech();

		self.checkEmailPostTimer =
			setTimeout(self.checkEmailMech, 500);
	};
	//- - - - -
	/**
		* The check-email mechanism.
		* @returns {undefined}
	 */
	self.checkEmailMech = function()
	{
		var data = {};
		var text = $("#txtEmailAddress").val();

		console.log("checkEmail called...");
		self.isServerNotFound(false);
		self.isAvailableEmail(false);

		if(text.length > 0)
		{
			data.Name = "Email";
			data.Value = text;
			aPost("/api/v1/signupemailcheck", data,
				responseEmail);
		}
	};
	//----------

	//----------
	// checkUsername
	//----------
	/**
		* Send a request to check availability of username.
	 */
	self.checkUsername = function()
	{
		if(self.checkUsernamePostTimer)
		{
			clearTimeout(self.checkUsernamePostTimer);
		}

		self.checkUsernameMech();

		self.checkUsernamePostTimer =
			setTimeout(self.checkUsernameMech, 500);
	};
	//- - - - - 
	/**
		* The check-username mechanism.
	 */
	self.checkUsernameMech = function()
	{
		var data = {};
		var text = $("#txtDisplayName").val();

		console.log("checkUsername called...");
		self.isServerNotFound(false);
		self.isAvailableUsername(false);

		if(text.length > 0)
		{
			data.Name = "Username";
			data.Value = text;
			aPost("/api/v1/signupusernamecheck", data,
				responseUsername);
		}
	};
	//----------

	//----------
	// clickCancel
	//----------
	/**
		* Process the click of a cancel button on modal forms.
		* @returns {undefined}
	 */
	self.clickCancel = function()
	{
		window.location.reload();
	}
	//----------

	//----------
	// doLogin
	//----------
	/**
		* Send a request to login at the server.
		* @returns {undefined}
	 */
	self.doLogin = function()
	{
		var data = {};

		console.log("doLogin...");
		self.isServerNotFound(false);
		self.isBadUsername(false);
		clearLogin();
		// console.log(`Email Address: ${self.emailAddress()}`);
		// console.log(`Password: ${self.password1()}`);
		data.Email = self.emailAddress();
		data.Password = self.password1();
		aPost("/api/v1/login", data, responseLogin);
	};
	//----------

	//----------
	// doSignup
	//----------
	/**
		* Post the completed signup data to the server and get a login
		* response.
	 */
	self.doSignup = function()
	{
		var data = {};

		console.log("Signup clicked...");
		self.isServerNotFound(false);
		self.isBadUsername(false);
		clearLogin();
		// console.log(`Email Address: ${self.emailAddress()}`);
		// console.log(`Password: ${self.password1()}`);
		data.Username = self.username();
		data.Email = self.emailAddress();
		data.Password = self.password1();
		data.CityItemID = self.cityID();
		if(!self.userTicket)
		{
			aPost("/api/v1/signup", data, responseSignup);
		}
		else
		{
			data.UserTicket = self.userTicket;
			aPost("/api/v1/signupupdateprofile", data, responseSignup);
		}
	};
	//----------

	//----------
	// isEmailAddressValid
	//----------
	/**
		* Return a value indicating whether the email address is valid for
		* sending to the server.
		* @returns {boolean} Value indicating whether the email address is
		* valid for transfer to server.
	 */
	self.isEmailAddressValid = ko.computed(function()
	{
		return self.emailAddress().length > 0;
	});
	//----------

	//----------
	// isLoginInfoValid
	//----------
	/**
		* Return a value indicating whether the email and password combination
		* is valid for transmission to server.
		* @returns {boolean} True if the email and password values can be sent
		* to the server.
	 */
	self.isLoginInfoValid = ko.computed(function()
	{
		// console.log("Update isLoginInfoValid");
		// console.log(".");
		// return self.emailAddress().length > 0 &&
		// 	self.password1().length > 0;
		return self.isEmailAddressValid() &&
			self.isPasswordValid();
	});
	//----------

	//----------
	// isPasswordMatch
	//----------
	/**
		* Return a value indicating whether password1 matches password2.
		* @returns {undefined}
	 */
	self.isPasswordMatch = ko.computed(function()
	{
		return (self.password1().length > 0 &&
			self.password1() == self.password2());
	});
	//----------

	//----------
	// isPasswordValid
	//----------
	/**
		* Return a value indicating whether the password is valid for
		* sending to the server.
		* @returns {boolean} Value indicating whether the password is
		* valid for transfer to server.
	 */
	self.isPasswordValid = ko.computed(function()
	{
		return self.password1().length > 0;
	});
	//----------

	//----------
	// isSignupInfoValid
	//----------
	/**
		* Return a value indicating whether the current data on the form
		* is valid for a signup.
	 */
	self.isSignupInfoValid = ko.computed(function()
	{
		// console.log("Update isSignupInfoValid");
		// console.log(".");
		return self.isAvailableEmail() &&
			self.isAvailableUsername() &&
			self.password1().length > 0 &&
			self.password1() == self.password2();
	});
	//----------

	//----------
	// responseEmail
	//----------
	/**
		* Process the server response to email availability.
		* @param {object} response AvailableStatusItem.
		* @returns {undefined}
		*/
	function responseEmail(response)
	{
		if(response)
		{
			//	Value presented.
			if(response.ServerNotFound)
			{
				self.isServerNotFound(true);
			}
			else
			{
				//	Valid reponse received.
				//	AvailableStatusItem.
				//	Name={string}, Available={boolean}
				if(response.Name == self.emailAddress())
				{
					//	Current value.
					self.isAvailableEmail(response.Available);
					if(self.isAvailableEmail() &&
						!self.isAvailableUsername() &&
						self.username().length > 0)
					{
						//	If email is approved, run another check of the username.
						self.checkUsernameMech();
					}
				}
				//	Responses for other typed values are discarded.
			}
		}
	};
	//----------

	//----------
	// responseLogin
	//----------
	/**
		* Process the login response received from the server.
		* @param {object} response UsernameUserTicket object containing response.
		* @returns {undefined}
		*/
	function responseLogin(response)
	{
		console.log(" Request finished...");
		if(response)
		{
			if(response.ServerNotFound)
			{
				self.isServerNotFound(true);
			}
			else
			{
				//	Some valid reponse.
				self.username = response.Username;
				self.userTicket = response.UserTicket;
			}
			console.log(`Processing for username: ${self.username}...`);
			if(self.username == "BADUSERNAMEORPASSWORD")
			{
				self.isBadUsername(true);
				clearLogin();
			}
			else
			{
				setCookieValue("username", self.username);
				setCookieValue("userTicket", self.userTicket);
				window.location.replace("Index.html");
			}
		}
		else
		{
			console.log("Response from callback was null...")
		}
	};
	//----------
		
	//----------
	// responseSignup
	//----------
	/**
		* Process the server's response to the signup post.
		* @param {object} response UsernameUserTicketModel response.
		* @returns {undefined}
		*/
	function responseSignup(response)
	{
		console.log(" Request finished...");
		if(response)
		{
			if(response.ServerNotFound)
			{
				self.isServerNotFound(true);
			}
			else
			{
				//	Some valid reponse.
				self.username = response.Username;
				self.userTicket = response.UserTicket;
			}
			console.log(`Processing for username: ${self.username}...`);
			if(self.username == "BADUSERNAMEORPASSWORD")
			{
				self.isBadUsername(true);
				clearLogin();
			}
			else
			{
				setCookieValue("username", self.username);
				setCookieValue("userTicket", self.userTicket);
				window.location.replace("Index.html");
			}
		}
		else
		{
			console.log("Response from callback was null...")
		}
	};
	//----------

	//----------
	// responseUsername
	//----------
	/**
		* Process the server response to username availability.
		* @param {object} response AvailableStatusItem.
		*/
		function responseUsername(response)
	{
		if(response)
		{
			//	Value presented.
			if(response.ServerNotFound)
			{
				self.isServerNotFound(true);
			}
			else
			{
				//	Valid reponse received.
				//	AvailableStatusItem.
				//	Name={string}, Available={boolean}
				if(response.Name == self.username())
				{
					//	Current value.
					self.isAvailableUsername(response.Available);
					if(self.isAvailableUsername() &&
						!self.isAvailableEmail() &&
						self.emailAddress().length > 0)
					{
						//	If username is approved, run another check of email.
						self.checkEmailMech();
					}
				}
				//	Responses for other typed values are discarded.
			}
		}
	};
	//----------

	//----------
	// userHasItems
	//----------
	/**
		* Return a value indicating whether the currently logged-in
		* user has items.
	 */
	self.userHasItems = ko.computed(function()
	{
		return self.itemCount() && self.itemCount() > 0;
	});
	//----------
}
//----------

//----------
// loadFile
//----------
/**
	* Navigate to another file in the browser.
	* @param {string} url The URL to load.
	* @returns {undefined}
	*/
function loadFile(url)
{
	if(url)
	{
		window.location.replace(url);
	}
}
//----------

//----------
// logoutSession
//----------
/**
	* Cancel the user's session.
	*/
function logoutSession()
{
	clearLogin();
	window.location.reload(true);
}
//----------

//----------
// rateItem
//----------
/**
	* Rate the specified item.
	* @param {number} catalogItemID Local identification of the item.
	* @param {number} rating Value to rate, between 1 and 5.
	*/
function rateItem(catalogItemID, rating)
{
	aPut(`api/v1/catalogitemrate/${catalogItemID}/${rating}`);
}
//----------
	
//----------
// selectCity
//----------
/**
	* Select the active city to view.
	* @param {number} cityID Unique identification of the city to select.
	* @returns {undefined}
	*/
function selectCity(cityID)
{
	var count = 0;
	var index = 0;
	var item = null;

	setCookieValue("cityItemID", cityID.toString());
	if(cityData.length > 0)
	{
		//	Cities are present. Get the name.
		count = cityData.length;
		for(index = 0; index < count; index ++)
		{
			item = cityData[index];
			if(item.CityItemID == cityID)
			{
				//	Matching city found.
				setCookieValue("cityName", item.CityName);
			}
		}
	}
	window.location.reload(true);
}
//----------

//----------
// setCookieValue
//----------
/**
	* Set the value of the named cookie for the domain.
	* @param {string} cookieName Name of the cookie to set.
	* @param {string} cookieValue Value to place in the cookie.
	*/
function setCookieValue(cookieName, cookieValue)
{
	document.cookie = `${cookieName}=${cookieValue};path=/`;
}
//----------
	
//----------
// setPageTitles
//----------
/**
	* Set the localized titles for the page.
	*/
function setPageTitles()
{
	var ws = `LocalGoods`;
	if(viewModel.titleSuffix().length > 0)
	{
		ws += ` - ${viewModel.titleSuffix()}`;
	}
	$("#docTitle").html(ws);
	ws = `LocalGoods - ${viewModel.cityName()}`;
	if(viewModel.leadSuffix().length > 0)
	{
		ws += ` - ${viewModel.leadSuffix()}`;
	}
	$("#navLead").html(ws);
}
//----------

// //----------
// // sleep
// //----------
// function sleep(ms)
// {
// 	return new Promise(resolve => setTimeout(resolve, ms));
// }
// //----------

// //----------
// // txtNewCityNameChanged
// //----------
// /**
// 	* Text on the new city name request has changed. Enable the button
// 	* if content. Disable the button if no content present.
// 	*/
// function txtNewCityNameChanged()
// {
// 	var txt = $("#txtNewCityName").val();

// 	if(txt)
// 	{
// 		$("#btnNewCityName").prop("disabled", false);
// 		if(event.keyCode == 13)
// 		{
// 			//	Enter was pressed. Click the button.
// 			$("#btnNewCityName").click();
// 		}
// 	}
// 	else
// 	{
// 		$("#btnNewCityName").prop("disabled", true);
// 	}
// }
// //----------

//----------
// updateCityList
//----------
/**
	* Load the list of available cities.
	* @param {array} data List of CityItem records.
	*/
function updateCityList(data)
{
	var cityItemIDSelected = 0;
	var count = 0;
	var index = 0;
	var item = null;
	var $mnu = $("#mnuCityList");
	var ws = "";

	cityData = data;
	// console.log(`updateCityList: Data ${JSON.stringify(data)}...`);
	if($mnu)
	{
		// console.log("updateCityList: Menu found...");
		//	Erase the items of the menu.
		$mnu.html("");
		//	Get the selected city.
		ws = getCookieValue("cityItemID");
		// console.log(`updateCityList: City cookie ${ws}...`);
		if(ws)
		{
			cityItemIDSelected = parseInt(ws);
		}
		count = data.length;
		//	Place the selected item first.
		for(index = 0; index < count; index ++)
		{
			item = data[index];
			if(item.CityItemID == cityItemIDSelected)
			{
				$mnu.append(
					`<a class="dropdown-item active"` +
					`cityItemID=${item.CityItemID} ` +
					`href="#">${item.CityName}</a>`);
				// console.log(`cityItemIDSelected: ${cityItemIDSelected}; ` +
				// `CityItemID: ${item.CityItemID}`);
				// console.log("updateCityList: Selected city found.");
				// ws = `LocalGoods`;
				// if(viewModel.titleSuffix().length > 0)
				// {
				// 	ws += ` - ${viewModel.titleSuffix()}`;
				// }
				// $("#docTitle").html(ws);
				// ws = `LocalGoods - ${item.CityName}`;
				// if(viewModel.leadSuffix().length > 0)
				// {
				// 	ws += ` - ${viewModel.leadSuffix()}`;
				// }
				// $("#navLead").html(ws);
				setCookieValue("cityName", item.CityName);
				viewModel.cityName(item.CityName);
				setPageTitles();
				break;
			}
		}
		//	Add all of the non-selected items.
		for(index = 0; index < count; index ++)
		{
			item = data[index];
			if(item.CityItemID != cityItemIDSelected)
			{
				ws = `selectCity(${item.CityItemID});`;
				$mnu.append(
					`<a class="dropdown-item" ` +
					`cityItemID=${item.CityItemID} ` +
					`onclick="${ws}"` +
					`>${item.CityName}</a>`);
			}
		}
	}
	loadedCities = true;
	// updateLoader(loadedCities && loadedThumbnails);
	if(loadStatus)
	{
		// console.log("Load Status function found...");
		updateLoader(loadStatus());
	}
}
//----------

//----------
// updateLoader
//----------
/**
	* Update the status of the loader, and hide it if applicable.
	* @param {boolean} hide Value indicating whether to hide the loader.
	* @returns {undefined}
	*/
function updateLoader(hide)
{
	if(hide)
	{
		$("#loader").addClass("display-none");
	}
}
//----------

