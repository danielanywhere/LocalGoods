
// //----------
// // koViewModel
// //----------
// /**
// 	* Knockout view-model.
// 	*/
// function koViewModel()
// {
// 	//	Data.
// 	var self = this;
// 	self.loggedIn = false;
// 	self.itemCount = 0;
// }
// //----------

//----------
// loadStatus
//----------
/**
	* Return the current required load status needed for hiding the modal
	* dialog.
	* @returns {boolean} Value indicating whether the required status for
	* hiding the modal loader dialog has been met.
	*/
function loadStatus()
{
	// console.log(" Load status check...");
	return loadedCities && loadedThumbnails;
}
//----------

//----------
// updateProductCount
//----------
/**
	* Update the count of products owned and managed by this user.
	* @param {object} value A CountItem object containing the count of items
	* owned by this user.
	*/
function updateProductCount(value)
{
	if(value && value.Count)
	{
		viewModel.itemCount(value.Count);
	}
	else
	{
		viewModel.itemCount(0);
	}
}
//----------

//----------
// updateThumbnailList
//----------
/**
	* Display the list of thumbnails in the page.
	* @param {array} value Array of minimal items to display.
	*/
function updateThumbnailList(value)
{
	var count = 0;
	var countStars = 0;
	var crd = "";		//	Card content.
	var departmentLast = "";	//	Last department.
	var departmentName = "";	//	Current department.
	var index = 0;
	var indexStars = 0;
	var item = null;
	var $sec = null;
	var starClass = "staroff";
	var stars = "";
	var unitPriceDecimal = 0.0;
	var unitPriceWhole = 0.0;

	count = value.length;
	// console.log(`Thumbnails returned : ${JSON.stringify(value)}`);
	for(index = 0; index < count; index ++)
	{
		item = value[index];
		departmentName = item.DepartmentName;
		if(departmentName != departmentLast)
		{
			$sec = $(`#secDepartment${departmentName}`);
			if(!$sec)
			{
				//	The department wasn't listed on the page by default.
				//	Add it now and get a reference.
				$("#pageContainer").append(
					`<div id=secDepartment${departmentName}></div>`);
				$sec = $(`#secDepartment${departmentName}`);
			}
			else
			{
				// console.log(`Section found for ${departmentName}...`);
			}
		}
		//	Stars are rigged backwards for user interaction.
		//	The first star is displayed rightmost on on the screen.
		stars = "";
		countStars = Math.round(item.StarCount);
		for(indexStars = 0; indexStars < 5; indexStars ++)
		{
			if(5 - indexStars <= countStars)
			{
				starClass = "staron";
			}
			else
			{
				starClass = "staroff";
			}
			stars += `<span class="${starClass}" ` +
				`onclick="rateItem(${item.CatalogItemID}, ${5 - indexStars})">` +
				`</span>`;
		}
		unitPriceWhole = Math.floor(item.ItemPrice);
		unitPriceDecimal = Math.floor((item.ItemPrice - unitPriceWhole) * 100.0);
		crd = `<div class="card" style="max-width: 200px">
		 <a href="CatalogItem.html?CatalogItemID=${item.CatalogItemID}">
			<img class="card-img-top" src="${item.ImageURL}" width="100" />
			</a>
			<div class="card-body">
		 <a href="CatalogItem.html?CatalogItemID=${item.CatalogItemID}">
			<h4 class="card-title">${item.ProductTitle}</h4>
			</a>
			<p class="card-text">${item.ProductDescription}</p>
			<div class="rating">
			${stars}
			</div>
			<div class="text-primary"><span class="pricePrefix">&#36;</span><span class="price">${unitPriceWhole}</span><span class="priceDecimal">${unitPriceDecimal}</span> ${item.ItemUnit}</div>
			</div>
			</div>
			`;
		$sec.append(crd);
	}
	loadedThumbnails = true;
	updateLoader(loadStatus());
}
//----------

//----------
// document ready
//----------
/**
	* The document has completed loading.
 */
$(document).ready(function()
{
	var cityID = "";
	var ticket = {};
	// var viewModel = new koViewModel();

	viewModel = new koViewModel();
	dialogLoginPrep();
	dialogSignupPrep();
	activateLoader();

	//	User login.
	viewModel.loggedIn = getCookieLoggedIn();
	if(viewModel.loggedIn)
	{
		//	Get the user identity.
		viewModel.userTicket = getCookieValue("userTicket");
	}

	//	Set the default selected city.
	cityID = getCookieValue("cityItemID");
	if(!cityID)
	{
		setCookieValue("cityItemID", "1");
		setCookieValue("cityName", "Laramie, WY");
		cityID = "1";
	}
	viewModel.cityID(parseInt(cityID));
	viewModel.cityName(getCookieValue("cityName"));

	//	Cities list.
	aGet("api/v1/homecities", "", updateCityList);
	//	Product previews.
	aGet(`api/v1/homeinfo/${cityID}`, "", updateThumbnailList);
	//	User products.
	if(viewModel.userTicket)
	{
		ticket.UserTicket = viewModel.userTicket;
		aPost(`api/v1/userproductcount`, ticket, updateProductCount);
	}

	ko.applyBindings(viewModel);
});
//----------

