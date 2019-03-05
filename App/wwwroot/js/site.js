function standardFail(jqXHR, textStatus, errorThrown) {
	console.log("Error: " + textStatus + ": " + errorThrown);
	alert(errorThrown);
}

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
