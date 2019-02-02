$(function(){
	console.log("Js Works");
	$(".js-works").text("The Custom Js Works!!!").click(function(event){
		event.preventDefault();
		alert("It works already, what more do you need???");
		$(this).css("font-size", 180).css("color", "red");
	})
});
