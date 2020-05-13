
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
	return loadedThumbnails;
}
//----------
	
//----------
// updateThumbnailList
//----------
/**
	* Display the list of thumbnails in the page.
	* @param {array} itemData Array of minimal items to display.
	*/
function updateThumbnailList(itemData)
{
	var count = 0;
	var countStars = 0;
	var crd = "";		//	Card content.
	var departmentName = "";	//	Current department.
	var index = 0;
	var indexStars = 0;
	var item = null;
	var $sec = $("#pageContainer");
	var starClass = "staroff";
	var stars = "";
	var unitPriceDecimal = 0.0;
	var unitPriceWhole = 0.0;

	count = itemData.length;
	// console.log(`Thumbnails returned : ${JSON.stringify(itemData)}`);
	for(index = 0; index < count; index ++)
	{
		item = itemData[index];
		departmentName = item.DepartmentName;

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
			<div class="small">${item.CityName} - ${departmentName}</div>
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
	var data = {};
	var searchText = getQueryValue("SearchText");
	// var viewModel = new koViewModel();

	viewModel = new koViewModel();

	viewModel.titleSuffix("Products");
	viewModel.leadSuffix("Product List");

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

	//	Search in this module finds items owned by the user, and
	//	acts as a server-based filter.
	//	Place the search text.
	$("#txtSearch").val(searchText);

	//	Get the results for the search.
	if(searchText)
	{
		data.SearchText = searchText;
		data.UserTicket = viewModel.userTicket;
		aPost("api/v1/userproductsearch", data,  updateThumbnailList);
	}
	else
	{
		data.UserTicket = viewModel.userTicket;
		aPost("api/v1/userproductlist", data,  updateThumbnailList);
	}

	setPageTitles();

	ko.applyBindings(viewModel);
});
//----------
