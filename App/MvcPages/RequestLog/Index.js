$(function () {
	initRequestLogSearch();
	initRequestLogResults();
});

function initRequestLogSearch() {

}

function initRequestLogResults() {
	var typeSelector = $("#detailType");

	$(".js-log-results .js-log-link").click(function (event) {
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
	loadRequestLog(requestLogID, "DetailPanel", function(data){
		$("#requestLogDetailPanel").html(data);
	});
}

function loadRequestLogModal(requestLogID) {
	loadRequestLog(requestLogID, "DetailModal", function(data){
		$("#requestLogDetailModal").html(data).modal("show");
	});
}

function loadRequestLog(requestLogID, type, successCallback){
	$.ajax({
		type: "POST",
		url: "/RequestLog/" + type + ".php",
		data: {requestLogID: requestLogID},
		dataType: "html"
	})
		.done(successCallback)
		.fail(function (jqXHR, textStatus, errorThrown) {
			console.log("Error: " + textStatus + ": " + errorThrown);
			alert(errorThrown);
		})
		.always(function () {

		});
}
