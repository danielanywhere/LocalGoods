var itemData = {};
var loadedInfo = false;

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

// 	self.canEdit = ko.observable(false);
// 	self.isDebug = false;
// 	self.loggedIn = false;
// 	// self.itemCount = 0;

// }
// //----------

//----------
// btnBulletAddClick
//----------
/**
	* The bullet entry Add button has been clicked.
	*/
function btnBulletAddClick()
{
	var count = itemData.BulletPoints.length;
	var text = $("#txtBullet").val();

	itemData.BulletPoints[count] =
		{
			"BulletIndex": count,
			"BulletText": text
		};
	$("#cmboBullet").append(`<option>${text}</option>`);
	$("#btnBulletAdd").prop("disabled", true);
}
//----------

//----------
// btnBulletDeleteClick
//----------
/**
	* Bullet delete button has been clicked.
	* @returns {undefined}
	*/
function btnBulletDeleteClick()
{
	var $btnDelete = $("#btnBulletDelete");
	var $btnDown = $("#btnBulletDown");
	var $btnUp = $("#btnBulletUp");
	var count = 0;
	var index = 0;
	var item = null;
	var $selected = $("#cmboBullet option:selected");
	var text = $selected.text();

	if(text)
	{
		//	Remove the item from data.
		count = itemData.BulletPoints.length;
		for(index = 0; index < count; index ++)
		{
			item = itemData.BulletPoints[index];
			if(item.BulletText == text)
			{
				itemData.BulletPoints.splice(index, 1);
				break;
			}
		}
		//	Remove the item from list.
		$selected.remove();
		$btnDelete.prop("disabled", true);
		$btnDown.prop("disabled", true);
		$btnUp.prop("disabled", true);
		txtBulletChange();
	}
}
//----------

//----------
// btnBulletDownClick
//----------
/**
	* Bullet down button has been clicked.
	* @returns {undefined}
	*/
function btnBulletDownClick()
{
	var count = 0;
	var found = false;
	var index = 0;
	var item = null;
	var $selected = $("#cmboBullet option:selected");
	var target = "";
	var text = $selected.text();

	if(text)
	{
		//	Move the item down in data.
		count = itemData.BulletPoints.length;
		for(index = 0; index < count; index ++)
		{
			item = itemData.BulletPoints[index];
			if(item.BulletText == text)
			{
				//	Remove the item.
				itemData.BulletPoints.splice(index, 1);
				count --;
				//	Place the item one position down.
				if(index < count)
				{
					//	The item can move down.
					itemData.BulletPoints.splice(index + 1, 0, item);
				}
				else
				{
					//	Insert the item at the top.
					itemData.BulletPoints.splice(0, 0, item);
				}
				found = true;
				break;
			}
		}
		//	Move the item down in list.
		if(found)
		{
			console.log("Move down: Item found...");
			target = `<option selected>${text}</option>`;
			$selected.remove();	//	Remove the selected item.
			if(index < count)
			{
				//	The item can move down.
				index += 2;		//	nth child is 1-based + (1 invisible).
				console.log(` Item can move after position ${index}.`);
				$(target).insertAfter($(`#cmboBullet option:nth-child(${index})`));
			}
			else
			{
				//	Insert the item at the top.
				$(target).insertAfter($(`#cmboBullet option:nth-child(1)`));
			}
		}
	}
}
//----------

//----------
// btnBulletUpClick
//----------
/**
	* Bullet up button has been clicked.
	* @returns {undefined}
	*/
function btnBulletUpClick()
{
	var count = 0;
	var found = false;
	var index = 0;
	var item = null;
	var $selected = $("#cmboBullet option:selected");
	var target = "";
	var text = $selected.text();

	if(text)
	{
		//	Move the item up in data.
		count = itemData.BulletPoints.length;
		for(index = 0; index < count; index ++)
		{
			item = itemData.BulletPoints[index];
			if(item.BulletText == text)
			{
				//	Remove the item.
				itemData.BulletPoints.splice(index, 1);
				//	Place the item one position up.
				if(index > 0)
				{
					//	The item can move up.
					itemData.BulletPoints.splice(index - 1, 0, item);
				}
				else
				{
					//	Insert the item at the bottom.
					itemData.BulletPoints.splice(count, 0, item);
				}
				found = true;
				break;
			}
		}
		//	Move the item down in list.
		if(found)
		{
			console.log("Move up: Item found...");
			target = `<option selected>${text}</option>`;
			$selected.remove();	//	Remove the selected item.
			if(index > 0)
			{
				//	The item can move up.
				index += 1;		//	nth child is 1-based + (1 invisible).
				console.log(` Item can move before position ${index}.`);
				$(target).insertBefore($(`#cmboBullet option:nth-child(${index})`));
			}
			else
			{
				//	Insert the item at the bottom.
				$("#cmboBullet").append(target);
			}
		}
	}
}
//----------

//----------
// btnImageAddClick
//----------
/**
	* The add image button has been clicked.
	* Initiate the file control.
	* @returns {undefined}
	*/
function btnImageAddClick()
{
	//	DOM click.
	$("#fileControl")[0].click();
}
//----------

//----------
// btnImageDeleteClick
//----------
/**
	* Delete the image reference from data and from the page.
	* @param {string} id Element ID of the item to delete.
	*/
function btnImageDeleteClick(id)
{
	var count = 0;
	var imageIndex = 0;
	var index = 0;

	if(id)
	{
		//	Remove the image reference from data.
		imageIndex = getNumericValue(id);
		if(imageIndex > -1)
		{
			count = itemData.Images.length;
			for(index = 0; index < count; index ++)
			{
				item = itemData.Images[index];
				if(item.ImageIndex == imageIndex)
				{
					itemData.Images.splice(index, 1);
					break;
				}
			}
		}
		$(`#${id}`).remove();
	}
}
//----------

//----------
// btnImageDownClick
//----------
function btnImageDownClick(id)
{
	var count = 0;
	var found = false;
	var html = "";
	var imageIndex = 0;
	var index = 0;

	if(id)
	{
		//	Remove the image reference from data.
		imageIndex = getNumericValue(id);
		if(imageIndex > -1)
		{
			count = itemData.Images.length;
			for(index = 0; index < count; index ++)
			{
				item = itemData.Images[index];
				if(item.ImageIndex == imageIndex)
				{
					//	Remove the item from the collection.
					itemData.Images.splice(index, 1);
					count --;
					if(index < count)
					{
						//	The item can move down.
						itemData.Images.splice(index + 1, 0, item);
					}
					else
					{
						//	Insert the item at the top.
						itemData.Images.splice(0, 0, item);
					}
					found = true;
					break;
				}
			}
		}
		html = $(`#${id}`)[0].outerHTML;
		$(`#${id}`).remove();
		if(found)
		{
			//	Move the item down in the page.
			if(index < count)
			{
				//	The item can move down.
				index ++;		//	nth child is 1-based.
				console.log(` Item can move after position ${index}.`);
				$(html).insertAfter($(`#imgRows div:nth-child(${index})`));
			}
			else
			{
				//	Insert the item at the top.
				$(html).insertBefore($(`#imgRows div:nth-child(1)`));
			}
		}
	}
}
//----------

//----------
// btnImageUpClick
//----------
/**
	* The image up button has been clicked.
	* @param {string} id Alphanumeric ID of the element to move.
	* @returns {undefined}
	*/
function btnImageUpClick(id)
{
	var count = 0;
	var found = false;
	var html = "";
	var imageIndex = 0;
	var index = 0;

	if(id)
	{
		//	Remove the image reference from data.
		imageIndex = getNumericValue(id);
		if(imageIndex > -1)
		{
			count = itemData.Images.length;
			for(index = 0; index < count; index ++)
			{
				console.log(` Checking item ${index}...`);
				item = itemData.Images[index];
				if(item.ImageIndex == imageIndex)
				{
					//	Remove the item from the collection.
					itemData.Images.splice(index, 1);
					if(index > 0)
					{
						//	The item can move up.
						itemData.Images.splice(index - 1, 0, item);
					}
					else
					{
						//	Insert the item at the bottom.
						itemData.Images[count - 1] = item;
					}
					found = true;
					break;
				}
			}
		}
		html = $(`#${id}`)[0].outerHTML;
		$(`#${id}`).remove();
		if(found)
		{
			//	Move the item up in the page.
			if(index > 0)
			{
				//	The item can move up.
				index += 0;		//	nth child is 1-based.
				console.log(` Item can move before position ${index}.`);
				$(html).insertBefore($(`#imgRows div:nth-child(${index})`));
			}
			else
			{
				//	Insert the item at the bottom.
				$(`#imgRows`).append(html);
			}
		}
	}
}
//----------

//----------
// btnSaveChangesClick
//----------
/**
	* Save the local changes to the server.
	* @returns {undefined}
	*/
function btnSaveChangesClick()
{
	var cancel = false;
	var chunk = "";
	var chunkSize = 8192;
	var count = 0;
	var createData = {};
	var data = "";
	var dataIndex = 0;
	var dataLength = 0;
	var frac = 0.0;
	var index = 0;
	var item = {};
	var packet = {};
	var progress = 0.0;
	var response = {};
	
	activateLoader("Saving changes...");
	$("#loaderProgress").css("display", "block");

	itemData.UserItemTicket = getCookieValue("userTicket");
	itemData.CityItemID = $("#cmboCities option:selected").val();
	itemData.DepartmentName = $("#cmboDepartments option:selected").text();
	itemData.ProductTitle = $("#txtProductTitle").val();
	itemData.ContactInfo = $("#txtContactInfo").val();
	itemData.ProductDescription = $("#txtProductDescription").val();
	itemData.ItemPrice = $("#txtItemPrice").val();
	itemData.ItemUnit = $("#txtItemUnit").val();

	// //	TEST: Save without ImageData.
	// count = itemData.Images.length;
	// for(index = 0; index < count; index ++)
	// {
	// 	itemData.Images[index].ImageData = "";
	// }
	console.log(`CatalogItemEdit: Save ${JSON.stringify(itemData)}`);
	if(itemData.CatalogItemID == 0)
	{
		//	Pre-create the catalog item record so the image chunks can be
		//	accepted for this user and item combination.
		createData.CatalogItemID = 0;
		createData.UserItemTicket = itemData.UserItemTicket;
		createData = aPostSync("/api/v1/catalogitemcreate", createData);
		if(createData && createData.CatalogItemID)
		{
			console.log(`Catalog item ID received: ${createData.CatalogItemID}`);
			itemData.CatalogItemID = createData.CatalogItemID;
			viewModel.catalogItemID(createData.CatalogItemID);
		}
	}
	count = itemData.Images.length;
	for(index = 0; index < count; index ++)
	{
		item = itemData.Images[index];
		if(item.ImageData && item.ImageData.length > 0)
		{
			//	Upload the current file as separate chunks.
			data = item.ImageData;
			dataLength = item.ImageData.length;
			//	Get the target filename.
			packet = {};
			packet.UserItemTicket = itemData.UserItemTicket;
			packet.CatalogItemID = itemData.CatalogItemID;
			console.log(`Calling /api/v1/catalognewimageitemticket`);
			response = aPostSync("/api/v1/catalognewimageitemticket", packet);
			console.log(`Resuming local with ${JSON.stringify(response)}`);
			if(response && response.Ticket)
			{
				console.log(`Preparing to upload ${dataLength} bytes to ${response.Ticket}.tmp`);
				packet = {};
				packet.Ticket = response.Ticket;
				while(dataIndex < dataLength)
				{
					frac = (dataIndex / dataLength) * 100.0;
					progress = Math.round(frac);
					console.log(`Progress: ${progress}`);
					$("#loaderProgressBar").attr("aria-valuenow", progress);
					$("#loaderProgressBar").css("width", `${progress}%`);
					$("#loaderProgressBar span").html(`${progress}% Complete`);
					if(dataIndex + chunkSize <= dataLength)
					{
						chunk = data.substr(dataIndex, chunkSize);
					}
					else if(dataIndex + 1 < dataLength)
					{
						chunk = data.substr(dataIndex, dataLength - dataIndex);
					}
					packet.Chunk = chunk;
					console.log(`Send chunk at ${dataIndex}`);
					response = aPostSync("/api/v1/catalogsendfilechunk", packet);
					if(response.Error)
					{
						cancel = true;
						break;
					}
					dataIndex += chunkSize;
				}
			}
			if(!cancel)
			{
				//	File was uploaded.
				item.ImageData = packet.Ticket + ".tmp";
			}
		}
	}
	if(!cancel)
	{
		//	All new files made it okay. Save the rest of the file.
		aPost("/api/v1/catalogitemsave", itemData, saveFinish);
		// // aPostForm("/api/v1/catalogitemsave", itemData, saveFinish);
	}
}
//----------

//----------
// cmboBulletChange
//----------
/**
	* Selection within the list has changed.
	*/
function cmboBulletChange()
{
	var disabled = false;
	var text = $("#cmboBullet option:selected").text();

	$("#txtBullet").val(text);
	txtBulletChange();
	disabled = (text == null || text.length == 0);
	$("#btnBulletUp").prop("disabled", disabled);
	$("#btnBulletDown").prop("disabled", disabled);
	$("#btnBulletDelete").prop("disabled", disabled);
}
//----------
	
//----------
// debugItem
//----------
/**
	* Load the content of itemData with test information.
	* @returns {object} Data for a test item.
	*/
function debugItem()
{
	itemData = {};

	//	0 - Catalog Item ID.
	itemData.CatalogItemID = 1;
	//	1 - City Item ID.
	itemData.CityItemID = 0;
	//	2 - Department Name.
	itemData.DepartmentName = "Food";
	//	3 - Product Title.
	itemData.ProductTitle = "Test Item";
	//	4 - Product Description.
	itemData.ProductDescription = "This item will be deleted as soon as testing is finished.";
	//	5 - Star Count.
	itemData.StarCount = 5.0;
	//	6 - Item Price.
	itemData.ItemPrice = 22.50;
	//	7 - Item Unit.
	itemData.ItemUnit = "ea";
	//	8 - Visible.
	itemData.Visible = true;
	//	9 - From Username.
	itemData.FromUsername = "danielanywhere";
	//	10 - Contact Info.
	itemData.ContactInfo = "Big Hollow Food Co-Op, Laramie";
	//	11 - Editable.
	itemData.Editable = true;
	//	12 - Images.
	itemData.Images = [
		{
			"ImageIndex": 0,
			"ImageURL": "images/CatalogItems/i1-4B9BE7C6-BDF2-409F-8300-1D3CD70B3DF1.png"
		},
		{
			"ImageIndex": 1,
			"ImageURL": "images/CatalogItems/i1-5508EBD5-E079-4283-8D18-C684CE42BB9A.png"
		},
		{
			"ImageIndex": 2,
			"ImageURL": "images/CatalogItems/i1-F721330F-D2DE-420A-95C5-79C2C95BF9CB.png"
		}
	];
	//	13 - Bullet Points.
	itemData.BulletPoints = [
		{
			"BulletIndex": 0,
			"BulletText": "Tasty treat."
		},
		{
			"BulletIndex": 1,
			"BulletText": "Interesting flavor."
		},
		{
			"BulletIndex": 2,
			"BulletText": "Long lasting."
		}
	];
}
//----------

//----------
// fillCities
//----------
/**
	* Fill the cities select list with values from the CityItem array.
	* @param {array} data Array of CityItem records.
	* @returns {undefined}
	*/
function fillCities(data)
{
	var cityID = 0;
	var cityName = "";
	var count = 0;
	var index = 0;
	var item = null;
	var sel = "";
	var $cmbo = $("#cmboCities");

	if(data && data.length)
	{
		$cmbo.empty();
		sel = (itemData.CityItemID == 0 ? " selected" : "");
		$cmbo.append(`<option style="display:none" value="0"${sel}></option>`);
		count = data.length;
		for(index = 0; index < count; index ++)
		{
			item = data[index];
			cityID = item.CityItemID;
			cityName = item.CityName;
			sel = (itemData.CityItemID == cityID ? " selected" : "");
			$cmbo.append(`<option value="${cityID}"${sel}>${cityName}</option>`);
		}
	}
	aGet("/api/v1/homedepartments", "", fillDepartments);
}
//----------

//----------
// fillDepartments
//----------
function fillDepartments(data)
{
	var departmentID = 0;
	var departmentName = "";
	var count = 0;
	var index = 0;
	var item = null;
	var sel = "";
	var $cmbo = $("#cmboDepartments");

	if(data && data.length)
	{
		$cmbo.empty();
		sel = (itemData.DepartmentName == "" ? " selected" : "");
		$cmbo.append(`<option style="display:none" value="0"${sel}></option>`);
		count = data.length;
		for(index = 0; index < count; index ++)
		{
			item = data[index];
			departmentID = item.DepartmentItemID;
			departmentName = item.DepartmentName;
			sel = (item.DepartmentName.toLowerCase() ==
				itemData.DepartmentName.toLowerCase() ? " selected" : "");
			$cmbo.append(
				`<option value="${departmentID}"${sel}>${departmentName}</option>`);
		}
	}
}
//----------

//----------
// getHighImageIndex
//----------
/**
	* Return the highest image index found on item data.
	* @returns {number} The high index value found in images list.
	*/
function getHighImageIndex()
{
	var count = itemData.Images.length;
	var imageIndex = 0;
	var index = 0;
	var result = 0

	for(; index < count; index ++)
	{
		imageIndex = itemData.Images[index].ImageIndex;
		if(imageIndex > result)
		{
			result = imageIndex;
		}
	}
	return result;
}
//----------

//----------
// getNumericValue
//----------
/**
	* Return the numeric suffix from the specified value.
	* @param {string} value Alphanumeric value that ends in a numberic value.
	* @returns {number} The numeric suffix of the caller's value, if found.
	* Otherwise, -1.
	*/
function getNumericValue(value)
{
	var count = 0;
	var index = 0;
	var numeric = "";
	var result = -1;
	var val = "";

	if(value)
	{
		count = value.length;
		for(index = count - 1; index > -1; index --)
		{
			val = value[index];
			if(val >= '0' && val <= '9')
			{
				//	Prepend the current character.
				numeric = val + numeric;
			}
			else
			{
				break;
			}
		}
		if(numeric)
		{
			result = parseInt(numeric);
		}
	}
	console.log(`getNumericValue: Input: ${value}, Output: ${result}`);
	return result;
}
//----------

//----------
// newItem
//----------
/**
	* Load the content of itemData with prototype information.
	* @returns {object} Data for a new item.
	*/
function newItem()
{
	itemData = {};

	//	0 - Catalog Item ID.
	itemData.CatalogItemID = 0;
	//	1 - City Item ID.
	itemData.CityItemID = parseInt(viewModel.cityID());
	//	2 - Department Name.
	itemData.DepartmentName = "Food";
	//	3 - Product Title.
	itemData.ProductTitle = "New Item";
	//	4 - Product Description.
	itemData.ProductDescription = "";
	//	5 - Star Count.
	itemData.StarCount = 5.0;
	//	6 - Item Price.
	itemData.ItemPrice = 0.0;
	//	7 - Item Unit.
	itemData.ItemUnit = "ea";
	//	8 - Visible.
	itemData.Visible = true;
	//	9 - From Username.
	itemData.FromUsername = getCookieValue("username");
	//	10 - Contact Info.
	itemData.ContactInfo = "Big Hollow Food Co-Op, Laramie";
	//	11 - Editable.
	itemData.Editable = true;
	//	12 - Images.
	itemData.Images = [];
	//	13 - Bullet Points.
	itemData.BulletPoints = [];
}
//----------

//----------
// saveFinish
//----------
/**
	* The server has completed processing the saved information.
	* @returns {undefined}
	*/
function saveFinish()
{
	window.location.replace(
		`CatalogItem.html?CatalogItemID=${viewModel.catalogItemID()}`);
}
//----------

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
// txtBulletKeypress
//----------
/**
	* Keypress on the bullet entry textbox.
	*/
function txtBulletKeypress()
{
	if(event.keyCode == 13)
	{
		//	Enter. Click the add button.
		if(!$("#btnBulletAdd").prop("disabled"))
		{
			btnBulletAddClick();
		}
	}
}
//----------

//----------
// txtBulletChange
//----------
/**
	* Bullet entry text has changed.
	* @returns {undefined}
	*/
function txtBulletChange()
{
	var $btnAdd = $("#btnBulletAdd");
	// var $btnDown = $("#btnBulletDown");
	// var $btnUp = $("#btnBulletUp");
	var disabled = false;
	var entryText = $("#txtBullet").val();
	var $items = $("#cmboBullet > option");
	var text = "";

	if(entryText.length > 0 && $items.length > 0)
	{
		$items.each(function(index)
		{
			text = $(this).text();
			// console.log(` Item: ${text}`)
			if(text == entryText)
			{
				// console.log(" Match found...");
				disabled = true;
				return false;
			}
		});
	}
	else
	{
		disabled = true;
	}
	// console.log(`Bullet text changed, enable button: ${!disabled}...`);
	$btnAdd.prop("disabled", disabled);
	// $btnDown.prop("disabled", disabled);
	// $btnUp.prop("disabled", disabled);
}
//----------
	
//----------
// updateItem
//----------
/**
	* Display the item elements in the page.
	* @param {object} item Full information about a catalog item.
	*/
function updateItem()
{
	var count = 0;
	var crd = "";		//	Card content.
	var departmentName = "";	//	Current department.
	var imageIndex = 0;
	var index = 0;
	var item = null;
	var $sec = $("#pageContainer");

	if(itemData)
	{
		item = itemData;
	}
	console.log("Update Item: " + JSON.stringify(item));
	if(item && (item.CatalogItemID || item.CatalogItemID == 0))
	{
		//	Catalog Item data is present.
		departmentName = item.DepartmentName;

		$("#docTitle").html(`Item - ${item.ProductTitle}`);

		//	Main page content.
		crd = `<div class="container">`;
		crd += `<div class="row">`;
		//	Start: Editable image thumbnail column.
		crd += `<div class="col col-lg-6">`;
		count = item.Images.length;
		//	Add image button.
		crd += `<div><button id="btnAddImage" ` +
			`class="btn btn-primary" ` +
			`onclick="btnImageAddClick()">Add Image</button>` +
			`<br /><p>&nbsp;</p></div>`;
		//	Images with Delete, Move Up, and Move Down.
		crd += `<div id="imgRows">`;
		for(index = 0; index < count; index ++)
		{
			imageIndex = item.Images[index].ImageIndex;
			crd += `<div id="imgRow${imageIndex}">` +
				`<img class="item-thumb-lg" src="` +
				`${item.Images[index].ImageURL}` +
				`" /> `;
			crd += `<button id="btnImageDelete" ` +
				`class="btn btn-primary btn-image" ` +
				`onclick="btnImageDeleteClick('imgRow${imageIndex}')">Delete</button> ` +
				`<button id="btnImageUp" class="btn btn-primary btn-image" ` +
				`onclick="btnImageUpClick('imgRow${imageIndex}')">` +
				`<img src="images/MoveUpWhiteOutline.png" /></button> ` +
				`<button id="btnImageDown" class="btn btn-primary btn-image" ` +
				`onclick="btnImageDownClick('imgRow${imageIndex}')">` +
				`<img src="images/MoveDownWhiteOutline.png" /></button>`;
			crd += `</div>`;
		}
		crd += `</div>`;
		crd += `</div>`;
		//	End: Editable image thumbnail column.
		//	Start: Description column.
		crd += `<div class="col col-lg-6">`;
		//	City selection.
		crd += `<div>Market City:</div>`;
		crd += `<div><select class="form-control" id="cmboCities"></select></div>`;
		crd += `<p>&nbsp;</p>`;
		//	Department selection.
		crd += `<div>Department:</div>`;
		crd += `<div><select class="form-control" id="cmboDepartments"></select></div>`;
		crd += `<p>&nbsp;</p>`;
		//	Product title.
		crd += `<div>Product Title:</div>`;
		crd += `<div>` +
			`<input id="txtProductTitle" ` +
			`type="text" class="form-control mr-sm-2" ` +
			`value="${item.ProductTitle}" ` +
			`placeholder="Enter the name of the product." ` +
			`/></div>`;
		crd += `<p>&nbsp;</p>`;
		//	From:
		crd += `<div>from ` +
			`<span id="lblFrom" class="item-from">${item.FromUsername}</span></div>`;
		//	Contact info.
		crd += `<div>Contact Info:</div>`;
		crd += `<div>`;
		crd += `<input id="txtContactInfo" ` +
			`type="text" class="form-control mr-sm-2" ` +
			`value="${item.ContactInfo}" ` +
			`placeholder="Where can customers buy your product?" ` +
			`/>`;
		crd += `</div>`;
		crd += `<hr>`;
		//	Product description.
		crd += `<div>Product Description:</div>`;
		crd += `<div><textarea id="txtProductDescription" ` +
			`class="form-control" rows="4" ` +
			`placeholder="Enter a product description." ` +
			`>` +
			`${item.ProductDescription}</textarea></div>`;
		//	Suggested price.
		crd += `<div>Suggested price:</div>`;
		crd += `<div><input id="txtItemPrice" ` +
			`type="text" class="form-control mr-sm-2" ` +
			`value="${item.ItemPrice.toFixed(2)}" ` +
			`placeholder="Enter the per-unit price." ` +
			`/></div>`;
		//	Item unit.
		crd += `<div>Item unit:</div>`;
		crd += `<div><input id="txtItemUnit" ` +
			`type="text" class="form-control mr-sm-2" ` +
			`value="${item.ItemUnit}" ` +
			`placeholder="Unit of measure." ` +
			`/></div>`;
		crd += `<p>&nbsp;</p>`;
		//	Bullet points.
		crd += `<div>Bullet Points:</div>`;
		crd += `<div class="card card-full">`;
		crd += `<div class="card-body">`;
		//	Bullet text entry.
		crd += `<div>Entry:</div>`;
		crd += `<div style="margin-bottom: 8px;">`;
		crd += `<input id="txtBullet" type="text" class="form-control" ` +
			`style="width: 82%; display: inline;" value="" ` +
			`oninput="txtBulletChange()" ` +
			`onkeypress="txtBulletKeypress()" ` +
			`/>`;
		crd += `<button id="btnBulletAdd" class="btn btn-primary" ` +
			`onclick="btnBulletAddClick()"` +
			`disabled>Add</button>`;
		crd += `</div>`;
		crd += `<div>`;
		crd += `<select id="cmboBullet" class="form-control" size="10" ` +
			`style="display: inline-block; width: 82%" ` +
			`onchange="cmboBulletChange()"` +
			`>`;
		crd += `<option style="display:none" selected></option>`;
		count = item.BulletPoints.length;
		for(index = 0; index < count; index ++)
		{
			crd += `<option>${item.BulletPoints[index].BulletText}</option>`;
		}
		crd += `</select>`;
		crd += `<div style="display: inline-block; vertical-align: top;">`;
		//	Bullet up button.
		crd += `<button id="btnBulletUp" ` +
			`style="display: inline; margin-bottom: 8px;" ` +
			`class="btn btn-primary btn-image" onclick="btnBulletUpClick()" ` +
			`disabled><img src="images/MoveUpWhiteOutline.png" /></button><br />`;
		crd += `<button id="btnBulletDown" ` +
			`style="display: inline; margin-bottom: 8px;" ` +
			`class="btn btn-primary btn-image" onclick="btnBulletDownClick()" ` +
			`disabled><img src="images/MoveDownWhiteOutline.png" /></button><br />`;
		crd += `<button id="btnBulletDelete" ` +
			`style="display: inline; font-size: 10pt;" ` +
			`class="btn btn-primary btn-image" onclick="btnBulletDeleteClick()" ` +
			`disabled>Delete</button>`;
		crd += `</div>`;
		crd += `</div>`;
		crd += `</div>`;
		//	End: Description column.
		crd += `</div>`;
		crd += `</div>`;
		//	Save changes button.
		crd += `<button id="btnSaveChanges" ` +
			`class="btn btn-success btn-item-edit" ` +
			`onclick="btnSaveChangesClick()">Save Changes</button>`;
		crd += `</div>`;
		crd += `</div>`;
		$sec.append(crd);
		aGet("/api/v1/homecities", "", fillCities);
	}

	loadedInfo = true;
	setPageTitles();
	//	Override the page title for catalog items.
	ws = `LocalGoods - Edit Catalog Item`;
	$("#navLead").html(ws);

	updateLoader(loadedInfo);
}
//----------

//----------
// updateItemPrep
//----------
/**
	* Update the itemData object with catalog information to edit.
	* @param {object} item CatalogItemMin containing the information to
	* place in global itemData.
	*/
function updateItemPrep(item)
{
	itemData = item;
	updateItem();
}
//----------

//----------
// uploadFile
//----------
/**
	* Upload the file in question.
	* @param {object} event Information about the event.
	* @returns {undefined}
	*/
function uploadFile(event)
{
	var file = event.files[0];
	var imageType = /image.*/;
	var reader = null;

	if(!file.type.match(imageType))
	{
		//	TODO: Convert to modal dialog.
		alert("File type must be image.");
	}
	else
	{
		reader = new FileReader();
		reader.onload = (function(e) { uploadFileFinish(file.name, e); });
		reader.readAsDataURL(file);
	}
}
//----------

//----------
// uploadFileFinish
//----------
/**
	* The local file is finished uploading.
	* @param {string} name Name of the file.
	* @param {object} e Event data.
	* @returns {undefined}
	*/
function uploadFileFinish(name, e)
{
	var html = "";
	var index = getHighImageIndex() + 1;
	var item = {};

	//	Prepare the record.
	item.ImageIndex = index;
	item.ImageURL = name;
	item.ImageData = e.target.result;
	itemData.Images[itemData.Images.length] = item;

	//	Append the row.
	html = `<div id="imgRow${index}">`;
	html += `<img class="item-thumb-lg" src="${item.ImageData}" /> `;
	html += `<button class="btn btn-primary btn-image" ` +
		`onclick="btnImageDeleteClick('imgRow${index}')">Delete</button> `;
	html += `<button class="btn btn-primary btn-image" ` +
		`onclick="btnImageUpClick('imgRow${index}')">` +
		`<img src="images/MoveUpWhiteOutline.png" /></button> `;
	html += `<button class="btn btn-primary btn-image" ` +
		`onclick="btnImageDownClick('imgRow${index}')">` +
		`<img src="images/MoveDownWhiteOutline.png" /></button>`;
	html += `</div>`;
	$("#imgRows").append(html);
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

	viewModel.isDebug = false;

	viewModel.titleSuffix("Item");
	viewModel.leadSuffix("Edit Catalog Item");

	console.log(`Is new item? : ${catalogItemID == "0"}`);
	if(viewModel.isDebug)
	{
		console.log("Document ready...");
	}

	if(!viewModel.isDebug && catalogItemID != "0")
	{
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
	viewModel.cityID(cityID);

	//	Get the results for the catalog item.
	if((catalogItemID || catalogItemID == "0") && !viewModel.isDebug)
	{
		data.CatalogItemID = parseInt(catalogItemID);
		console.log(`Configure catalog item: ${data.CatalogItemID}`);
		data.UserItemTicket = getCookieValue("userTicket");
		if(data.CatalogItemID != 0)
		{
			aPost("api/v1/catalogitem", data,  updateItemPrep);
		}
		else
		{
			newItem();
			updateItem();
		}
	}

	if(viewModel.isDebug)
	{
		debugItem();
		updateItem();
	}

	if(viewModel.isDebug)
	{
		console.log("Bind controls...");
	}

	setPageTitles();

	ko.applyBindings(viewModel);

});
//----------
