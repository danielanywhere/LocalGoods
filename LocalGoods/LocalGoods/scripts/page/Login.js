
// function koViewModel()
// {
// 	//	Data.
// 	var self = this;
// 	self.emailAddress = ko.observable("");
// 	self.isBadUsername = ko.observable(false);
// 	self.isServerNotFound = ko.observable(false);
// 	self.loggedIn = false;
// 	self.password = ko.observable("");
// 	self.username = "";
// 	self.userTicket = "";

// 	//----------
// 	// checkLogin
// 	//----------
// 	/**
// 		* Send a request to login at the server.
// 		* @returns {undefined}
// 	 */
// 	self.checkLogin = function()
// 	{
// 		var data = {};

// 		console.log("checkLogin clicked...");
// 		self.isServerNotFound(false);
// 		self.isBadUsername(false);
// 		clearLogin();
// 		// console.log(`Email Address: ${self.emailAddress()}`);
// 		// console.log(`Password: ${self.password()}`);
// 		data.Email = self.emailAddress();
// 		data.Password = self.password();
// 		aPost("/api/v1/login", data, responseLogin);
// 	};
// 	//----------

// 	//----------
// 	// infoValid
// 	//----------
// 	/**
// 		* Return a value indicating whether the email and password combination
// 		* is valid for transmission to server.
// 		* @returns {boolean} True if the email and password values can be sent
// 		* to the server.
// 	 */
// 	self.infoValid = ko.computed(function()
// 	{
// 		// console.log("Update infoValid");
// 		// console.log(".");
// 		return self.emailAddress().length > 0 &&
// 			self.password().length > 0;
// 	});
// 	//----------

// 	//----------
// 	// responseLogin
// 	//----------
// 	/**
// 		* Process the login response received from the server.
// 		* @param {object} response UsernameUserTicket object containing response.
// 		* @returns {undefined}
// 		*/
// 	function responseLogin(response)
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

// }

/**
	* The document has completed loading.
 */
$(document).ready(function()
{
	viewModel = new koViewModel();
	ko.applyBindings(viewModel);
});
