
// function koViewModel()
// {
// 	//	Data.
// 	var self = this;
// 	self.username = ko.observable("");
// 	self.emailAddress = ko.observable("");
// 	self.password1 = ko.observable("");
// 	self.password2 = ko.observable("");

// 	self.isAvailableEmail = ko.observable(false);
// 	self.isAvailableUsername = ko.observable(false);

// 	self.isBadUsername = ko.observable(false);
// 	self.isServerNotFound = ko.observable(false);

// 	self.loggedIn = false;
// 	self.userTicket = "";

// 	self.checkEmailPostTimer = null;
// 	self.checkUsernamePostTimer = null;

// 	//----------
// 	// checkEmail
// 	//----------
// 	/**
// 		* Send a request to check availability of email.
// 	 */
// 	self.checkEmail = function()
// 	{
// 		if(self.checkEmailPostTimer)
// 		{
// 			clearTimeout(self.checkEmailPostTimer);
// 		}

// 		self.checkEmailMech();

// 		self.checkEmailPostTimer =
// 			setTimeout(self.checkEmailMech, 500);
// 	};
// 	self.checkEmailMech = function()
// 	{
// 		var data = {};
// 		var text = $("#txtEmailAddress").val();

// 		console.log("checkEmail called...");
// 		self.isServerNotFound(false);
// 		self.isAvailableEmail(false);

// 		if(text.length > 0)
// 		{
// 			data.Name = "Email";
// 			data.Value = text;
// 			aPost("/api/v1/signupemailcheck", data,
// 				responseEmail);
// 		}
// 	};
// 	//----------

// 	//----------
// 	// checkUsername
// 	//----------
// 	/**
// 		* Send a request to check availability of username.
// 	 */
// 	self.checkUsername = function()
// 	{
// 		if(self.checkUsernamePostTimer)
// 		{
// 			clearTimeout(self.checkUsernamePostTimer);
// 		}

// 		self.checkUsernameMech();

// 		self.checkUsernamePostTimer =
// 			setTimeout(self.checkUsernameMech, 500);
// 	};
// 	self.checkUsernameMech = function()
// 	{
// 		var data = {};
// 		var text = $("#txtDisplayName").val();

// 		console.log("checkUsername called...");
// 		self.isServerNotFound(false);
// 		self.isAvailableUsername(false);

// 		if(text.length > 0)
// 		{
// 			data.Name = "Username";
// 			data.Value = text;
// 			aPost("/api/v1/signupusernamecheck", data,
// 				responseUsername);
// 		}
// 	};
// 	//----------

// 	//----------
// 	// doSignup
// 	//----------
// 	/**
// 		* Post the completed signup data to the server and get a login
// 		* response.
// 	 */
// 	self.doSignup = function()
// 	{
// 		var data = {};

// 		console.log("Signup clicked...");
// 		self.isServerNotFound(false);
// 		self.isBadUsername(false);
// 		clearLogin();
// 		// console.log(`Email Address: ${self.emailAddress()}`);
// 		// console.log(`Password: ${self.password()}`);
// 		data.Username = self.username();
// 		data.Email = self.emailAddress();
// 		data.Password = self.password1();
// 		aPost("/api/v1/signup", data, responseSignup);
// 	};
// 	//----------

// 	//----------
// 	// isInfoValid
// 	//----------
// 	/**
// 		* Return a value indicating whether the current data on the form
// 		* is valid for a signup.
// 	 */
// 	self.isInfoValid = ko.computed(function()
// 	{
// 		// console.log("Update infoValid");
// 		// console.log(".");
// 		return self.isAvailableEmail() &&
// 			self.isAvailableUsername &&
// 			self.password1().length > 0 &&
// 			self.password1() == self.password2();
// 	});
// 	//----------

// 	//----------
// 	// isPasswordMatch
// 	//----------
// 	/**
// 		* Return a value indicating whether password1 matches password2.
// 		* @returns {undefined}
// 	 */
// 	self.isPasswordMatch = ko.computed(function()
// 	{
// 		return (self.password1().length > 0 &&
// 			self.password1() == self.password2());
// 	});
// 	//----------

// 	//----------
// 	// responseEmail
// 	//----------
// 	/**
// 		* Process the server response to email availability.
// 		* @param {object} response AvailableStatusItem.
// 		* @returns {undefined}
// 		*/
// 	function responseEmail(response)
// 	{
// 		if(response)
// 		{
// 			//	Value presented.
// 			if(response.ServerNotFound)
// 			{
// 				self.isServerNotFound(true);
// 			}
// 			else
// 			{
// 				//	Valid reponse received.
// 				//	AvailableStatusItem.
// 				//	Name={string}, Available={boolean}
// 				if(response.Name == self.emailAddress())
// 				{
// 					//	Current value.
// 					self.isAvailableEmail(response.Available);
// 				}
// 				//	Responses for other typed values are discarded.
// 			}
// 		}
// 	};
// 	//----------

// 	//----------
// 	// responseSignup
// 	//----------
// 	/**
// 		* Process the server's response to the signup post.
// 		* @param {object} response UsernameUserTicketModel response.
// 		* @returns {undefined}
// 		*/
// 	function responseSignup(response)
// 	{
// 		console.log(" Request finished...");
// 		if(response)
// 		{
// 			if(response.ServerNotFound)
// 			{
// 				self.isServerNotFound(true);
// 			}
// 			else
// 			{
// 				//	Some valid reponse.
// 				self.username = response.Username;
// 				self.userTicket = response.UserTicket;
// 			}
// 			console.log(`Processing for username: ${self.username}...`);
// 			if(self.username == "BADUSERNAMEORPASSWORD")
// 			{
// 				self.isBadUsername(true);
// 				clearLogin();
// 			}
// 			else
// 			{
// 				setCookieValue("username", self.username);
// 				setCookieValue("userTicket", self.userTicket);
// 				window.location.replace("Index.html");
// 			}
// 		}
// 		else
// 		{
// 			console.log("Response from callback was null...")
// 		}
// 	};
// 	//----------

// 	//----------
// 	// responseUsername
// 	//----------
// 	/**
// 		* Process the server response to username availability.
// 		* @param {object} response AvailableStatusItem.
// 		*/
// 		function responseUsername(response)
// 	{
// 		if(response)
// 		{
// 			//	Value presented.
// 			if(response.ServerNotFound)
// 			{
// 				self.isServerNotFound(true);
// 			}
// 			else
// 			{
// 				//	Valid reponse received.
// 				//	AvailableStatusItem.
// 				//	Name={string}, Available={boolean}
// 				if(response.Name == self.username())
// 				{
// 					//	Current value.
// 					self.isAvailableUsername(response.Available);
// 				}
// 				//	Responses for other typed values are discarded.
// 			}
// 		}
// 	};
// 	//----------

// }

/**
	* The document has completed loading.
 */
$(document).ready(function()
{
	viewModel = new koViewModel();
	ko.applyBindings(viewModel);
});
