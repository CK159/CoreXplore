//Get random int in range [min, max] -> Can include min and max values in result
function getRandomInt(min, max) {
	return Math.floor(Math.random() * (max - min + 1)) + min;
}

//Based on https://stackoverflow.com/a/48830705
//C# is basically JS, right?
function toTwosComplement(value, numberBits) {
	var MODULO = 1 << numberBits;
	var MAX_VALUE = (1 << (numberBits - 1)) - 1;

	if (value > MAX_VALUE) {
		value -= MODULO;
	}
	return value;
}

//Object.Assign polyfill. Because care about IE I guess...
//from https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Object/assign
if (typeof Object.assign != 'function') {
	// Must be writable: true, enumerable: false, configurable: true
	Object.defineProperty(Object, "assign", {
		value: function assign(target, varArgs) { // .length of function is 2
			'use strict';
			if (target == null) { // TypeError if undefined or null
				throw new TypeError('Cannot convert undefined or null to object');
			}

			var to = Object(target);

			for (var index = 1; index < arguments.length; index++) {
				var nextSource = arguments[index];

				if (nextSource != null) { // Skip over if undefined or null
					for (var nextKey in nextSource) {
						// Avoid bugs when hasOwnProperty is shadowed
						if (Object.prototype.hasOwnProperty.call(nextSource, nextKey)) {
							to[nextKey] = nextSource[nextKey];
						}
					}
				}
			}
			return to;
		},
		writable: true,
		configurable: true
	});
}

function DatesToISO8601(obj) {
	for (var prop in obj) {
		if (obj.hasOwnProperty(prop)) {
			if (obj[prop] instanceof Date) {
				obj[prop] = obj[prop].toISOString();
			}
		}
	}
}

//https://stackoverflow.com/a/52311051
function file2Base64(file) {
	return new Promise((resolve, reject) => {
		const reader = new FileReader();
		reader.readAsDataURL(file);
		reader.onload = () => {
			let encoded = reader.result.replace(/^.*;base64,/, '');
			if ((encoded.length % 4) > 0) {
				encoded += '='.repeat(4 - (encoded.length % 4));
			}
			resolve(encoded);
		};
		reader.onerror = error => reject(error);
	});
}

var nextNewId = (function () {
	var counter = -1;
	return function () {
		return counter--;
	}
})();

function fixSortOrder(arr, key) {
	key = key || "SortOrder";

	for (var i = 0; i < arr.length; i++) {
		arr[i][key] = i + 1;
	}
}
