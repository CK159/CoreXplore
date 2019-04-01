$(() => {
	var form = $("#logIndex");
	initRequestLogSearch(form);
	initRequestLogResults(form);
});

//JS for the setup of the search inputs
function initRequestLogSearch(form) {
	form.submit((event) => {
		event.preventDefault();

		indexResultReload(form, 1);
	});

	form.find(".js-datepicker").defaultDatepicker();
}

function indexResultReload(form, page) {
	if (typeof page === "undefined") {
		page = 1;
	}
	
	var formData = form.serializeArray();
	formData.push({name: "CurrentPage", value: page});

	$.webMvc({
		url: "/RequestLog/IndexResultReload.php",
		data: formData,
		failTo: "alert"
	}).done((data) => {
		$("#logResultTable").html(data);
		initRequestLogResults(form);
	});
}

//JS setup and re-setup for the search result table
function initRequestLogResults(form) {
	form.find(".pagination a").click(function(event) {
		event.preventDefault();
		var page = $(this).attr("href");
		
		if ($.isNumeric(page)) {
			indexResultReload(form, page);
		}
	});

	var typeSelector = $("#detailType");

	$(".js-log-results .js-log-link").click(function(event) {
		var type = typeSelector.val();
		var requestLogId = $(this).data("requestlogid");

		if (type === "Modal" || type === "All") {
			event.preventDefault();
			loadRequestLogModal(requestLogId);
		}

		if (type === "SamePage") {
			//Perform default click behavior
		}

		if (type === "NewPage" || type === "All") {
			event.preventDefault();
			window.open($(this).attr("href"), "_blank");
		}

		if (type === "Panel" || type === "All") {
			event.preventDefault();
			loadRequestLogPanel(requestLogId);
		}
	});
}

function loadRequestLogPanel(requestLogID) {
	loadRequestLog(requestLogID, "DetailPanel", (data) => {
		$("#requestLogDetailPanel").html(data);
	});
}

function loadRequestLogModal(requestLogID) {
	loadRequestLog(requestLogID, "DetailModal", (data) => {
		$("#requestLogDetailModal").html(data).modal("show");
	});
}

function loadRequestLog(requestLogID, type, successCallback) {
	$.webMvc({
		url: "/RequestLog/" + type + ".php",
		data: {requestLogID: requestLogID},
		failTo: "alert"
	}).done(successCallback);
}
