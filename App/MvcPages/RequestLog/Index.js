$(function(){
	initRequestLogSearch();
	initRequestLogResults();
});

function initRequestLogSearch(){

}

function initRequestLogResults(){
	var typeSelector = $("#detailType");
	
	$(".js-log-results .js-log-link").click(function(event){
		var type = typeSelector.val();
		if (type === "Modal" || type === "All") {
			event.preventDefault();
			console.log("TODO: Modal");
		}
		if (type === "SamePage") {
			//Perform default click behavior
		}
		if (type === "NewPage" || type === "All") {
			event.preventDefault();
			window.open($(this).attr("href"), '_blank');
		}
		if (type === "Panel" || type === "All") {
			event.preventDefault();
			console.log("TODO: Panel");
		}	
	});
}
