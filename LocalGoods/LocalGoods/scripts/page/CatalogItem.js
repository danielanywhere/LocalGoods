
//----------
// setItemImage
//----------
/**
	* Set the current image for the item.
	* @param {string} imageURL URL of the image to set.
	* @returns {undefined}
	*/
function setItemImage(imageURL)
{
	$("#itemImage").attr("src", imageURL);
}
//----------
	
//----------
// updateItem
//----------
/**
* Display the item elements in the page.
* @param {object} itemData Full information about a catalog item.
*/
function updateItem(itemData)
{
	var count = 0;
	var crd = "";		//	Card content.
	var departmentName = "";	//	Current department.
	var index = 0;
	var item = null;
	var $sec = $("#pageContainer");
	var starClass = "staroff";
	var stars = "";

	console.log("updateItem: Callback received...");
	if(itemData && itemData.CatalogItemID)
	{
		//	Catalog Item data is present.
		item = itemData;
		departmentName = item.DepartmentName;

		$("#docTitle").html(`Item - ${item.ProductTitle}`);

		//	Stars are rigged backwards for user interaction.
		//	The first star is displayed rightmost on on the screen.
		stars = "";
		count = Math.round(item.StarCount);
		for(index = 0; index < 5; index ++)
		{
			if(5 - index <= count)
			{
				starClass = "staron";
			}
			else
			{
				starClass = "staroff";
			}
			stars += `<span class="${starClass}" ` +
				`onclick="rateItem(${item.CatalogItemID}, ${5 - index})">` +
				`</span>`;
		}
			//	Main page content.
			crd = `<div class="container">
			<div class="row">`;
			//	Image thumbnail column.
			crd += `<div class="col col-lg-2">`;
			count = item.Images.length;
			for(index = 0; index < count; index ++)
			{
				crd += `<div><img class="item-thumb" ` +
					`onclick="setItemImage('${item.Images[index].ImageURL}')" ` +
					`src="${item.Images[index].ImageURL}" /></div>`;
			}
			crd += `</div>`;
			//	Large image column.
			crd += `<div class="col col-lg-6">`;
			if(count > 0)
			{
				crd += `<img id="itemImage" ` +
				`class="item-image" ` +
				`src="${item.Images[0].ImageURL}" />`;
			}
			else
			{
				crd += `<img id="itemImage" ` +
					`class="item-image" ` +
					`src="images/NoImagesAvailable.png" />`;
			}
			crd += `</div>`;
			//	Description column.
			crd += `<div class="col col-lg-4">`;
			//	Product title.
			crd += `<div class="item-title">${item.ProductTitle}</div>`;
			//	From username.
			crd += `<div>from <span class="item-from">${item.FromUsername}</span></div>`;
			//	Contact info.
			crd += `<div>contact:<br /> ` +
				`<span class="item-contact">${item.ContactInfo}</span></div>`;
			crd += `<hr>`;
			crd += `<div class="item-description">${item.ProductDescription}</div>`;
			crd += `<div>Suggested price: ` +
			`<span class="item-price text-primary">$${item.ItemPrice.toFixed(2)}` +
			`</span> <span class="item-unit">${item.ItemUnit}</span></div>
			<ul class="item-bullet">`;
			count = item.BulletPoints.length;
			for(index = 0; index < count; index ++)
			{
				crd += `<li>${item.BulletPoints[index].BulletText}</li>`;
			}
			crd += `</ul>
			<div class="rating">${stars}</div>`;
			crd += `</div>`;
			crd += `</div>
			</div>`;
		$sec.append(crd);
		if(viewModel)
		{
			console.log(`updateItem: Setting canEdit ${itemData.Editable}`);
			viewModel.canEdit(itemData.Editable);
		}
	}

	loadedThumbnails = true;
	setPageTitles();
	//	Override the page title for catalog items.
	ws = `LocalGoods - ${itemData.CityName} - Catalog Item`;
	$("#navLead").html(ws);

	updateLoader(loadedThumbnails);
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
	var catalogItemID = getQueryValue("CatalogItemID");
	var cityID = "";
	var data = {};
	// var viewModel = new koViewModel();

	viewModel = new koViewModel();
	viewModel.catalogItemID(catalogItemID);

	viewModel.titleSuffix("Item");
	viewModel.leadSuffix("Catalog Item");

	viewModel.isDebug = false;

	if(!viewModel.isDebug)
	{
		dialogLoginPrep();
		activateLoader();
	}
	else
	{
		viewModel.canEdit(true);
	}

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


	if(!viewModel.isDebug)
	{
		$("#sketchDiv").remove();
	}

	//	Get the results for the catalog item.
	if(catalogItemID && !viewModel.isDebug)
	{
		data.CatalogItemID = parseInt(catalogItemID);
		data.UserItemTicket = getCookieValue("userTicket");
		aPost("api/v1/catalogitem", data, updateItem);
	}

	ko.applyBindings(viewModel);

});
//----------
