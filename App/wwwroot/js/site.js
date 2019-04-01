jQuery.fn.extend({
	//TODO: Allow extending default values with provided object
	defaultDatepicker: function() {
		return this.each(function() {
			$(this).datepicker({
				zIndexOffset: 250, //Fix datepicker appearing behind header bar
				autoclose: true,
				todayBtn: true,
				todayHighlight: true,
				clearBtn: true
			});
		});
	}
});

/*
* Methods to simplify posting to controller actions and handling responses
* Has multiple built-in error handlers and the ability to chain additional error handling like normal $.ajax() calls
* Use $.webApi() for JSON requests and JSON responses
* Use $.webMvc for form post requests (x-www-form-urlencoded) and HTML responses
* */
(function($) {
	$.webApi = function (argOpts) {
		var opt = $.extend({
			//Common properties

			//URL to use. Can be combined with baseURL.
			url: "",
			//Object to submit. Auto-converted to JSON if contentType is application/json (default)
			data: null,
			//Standard $.ajax data type: 'text' 'html' 'xml' 'json' 'jsonp' 'script'
			dataType: "json",
			//Default failure handlers
			//'alert' for simple JS alert()
			//'popup' for dynamically created Bootstrap popup modal
			//jQuery selector or jQuery object for inserting error information 
			//null for when failures are to be handled by the caller through a .fail() handler
			failTo: null,

			//Uncommon properties
			baseUrl: "",
			method: "POST",
			contentType: "application/json; charset=utf-8"
		}, argOpts);

		var finalData = opt.data;
		//Auto-convert data to JSON if needed
		if (opt.contentType.indexOf("application/json") >= 0 && typeof finalData !== "string") {
			finalData = JSON.stringify(finalData);
		}

		var promise = $.ajax({
			url: opt.baseUrl + opt.url,
			contentType: opt.contentType,
			data: finalData,
			method: opt.method,
			dataType: opt.dataType
		});

		if (opt.failTo != null && opt.failTo !== "") {
			promise = promise.then(null, (jqXHR, textStatus, errorThrown) => {
				var failMethod;
				if (opt.failTo === "alert") {
					failMethod = buildFailAlert();
				}
				else if (opt.failTo === "popup") {
					failMethod = buildFailPopup();
				}
				else {
					failMethod = buildFailBox(opt.failTo);
				}

				var msgs = [];

				//Try to get error messages from the title for HTML error pages first
				var htmlErrorMsgs = htmlErrorProcessor(jqXHR);
				msgs = msgs.concat(htmlErrorMsgs);

				if (htmlErrorMsgs.length === 0){
					//Try to get the generic error messages if there's no HTML message
					if (typeof textStatus == "string") {
						msgs = msgs.concat(textStatus.split(/\r?\n/));
					}

					if (typeof errorThrown == "string") {
						msgs = msgs.concat(errorThrown.split(/\r?\n/));
					}
				}

				//Check if errorThrown is an exception object (if there was a JS exception like due to deserialization errors)
				if (errorThrown && errorThrown.stack && errorThrown.message) {
					msgs = msgs.concat(errorThrown.message.split(/\r?\n/));
				}

				//Run selected default failure handler
				failMethod(msgs, jqXHR);

				//Return rejected promise for any failure handlers which may have been chained
				return $.Deferred().reject([jqXHR, textStatus, errorThrown]);
			});
		}

		return promise;
	};

	$.webMvc = function (argOpts) {
		return $.webApi(
			$.extend({
				dataType: "html",
				contentType: "application/x-www-form-urlencoded; charset=UTF-8"
			}, argOpts)
		);
	};

	function htmlErrorProcessor(jqXHR) {
		try {
			var type = jqXHR.getResponseHeader("Content-Type");

			if (type.indexOf("text/html") >= 0) {
				//Attempt to read error message out of <title> tag of returned error HTML page
				var regex = /<title>([\s\S]*)<\/title>/;
				var regexResp = regex.exec(jqXHR.responseText);
				if (regexResp.length) {
					return [regexResp[regexResp.length - 1]];
				}
			}
		}
		catch (e) {
			console.error(e);
		}
		return [];
	}
}(jQuery));

function buildFailAlert(){
	return (messages, details) => {
		alert(messages.join("\n"));
	}
}

function buildFailPopup(messages, details){
	//TODO: Dynamically build and open a Bootstrap modal popup
	return (messages, details) => {
		alert(messages.join("\n"));
	}
}

function buildFailBox(container){
	//TODO: Create an element containing failure messages and insert into specified element
	return (messages, details) => {
		console.log("box container", $(container));
		alert(messages.join("\n"));
	}
}

(function($) {
	$.fn.extend({
		loading: function(input) {
			return this.each(function() {
				loader.call($(this), input, "Loading...", "<i class='fa fa-spinner fa-spin'></i> ");
			});
		},
		saving: function(input){
			return this.each(function() {
				loader.call($(this), input, "Saving...", "<i class='fa fa-spinner fa-spin'></i> ");
			});
		}
	});

	function loader(input, defaultHtml, iconHtml) {
		var objData = this.data("ipn-loading");
		if (objData == null) {
			objData = {oldHtml: "", count: 0};
		}

		if (input === false) {
			//Undo loading
			if (objData.count === 1) {
				this.html(objData.oldHtml).prop("disabled", false);
			}

			if (objData.count > 0) {
				objData.count--;
			}
		}
		else {
			//Set loading
			if (typeof input === "undefined") {
				input = defaultHtml;
			}

			if (objData.count === 0) {
				objData.oldHtml = this.html();
			}

			this.html(iconHtml + input).prop("disabled", true);
			objData.count++;
		}

		this.data("ipn-loading", objData);
	}
}(jQuery));
