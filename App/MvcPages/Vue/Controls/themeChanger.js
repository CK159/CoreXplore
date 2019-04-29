
Vue.component("my-theme-changer", {
	template: `
<div>
	<label>Select Your Theme</label>
	<div class="input-group">
		<span class="input-group-btn">
			<button class="btn btn-default" type="button" v-on:click="themeChange(-1)">&lt;</button>
		</span>
		<span class="input-group-btn">
			<button class="btn btn-default" type="button" v-on:click="themeChange(1)">&gt;</button>
		</span>
		<select class="form-control" v-model="theme">
			<option v-for="(item, index) in themes" :value="index">{{item.name}}</option>
		</select>
	</div>
</div>
	`,
	data: function() {
		return {
			theme: 0,
			themes: [
				{name: "Default", url: "/lib/bootstrap/css/bootstrap.min.css"},
				{name: "United (Ubuntu)", url: "https://bootswatch.com/3/united/bootstrap.css"},
				{name: "Cyborg (Dark)", url: "https://bootswatch.com/3/cyborg/bootstrap.css"},
				{name: "Darkly (Dark...)", url: "https://bootswatch.com/3/darkly/bootstrap.css"},
				{name: "Simplex", url: "https://bootswatch.com/3/simplex/bootstrap.css"},
				{name: "Flatly", url: "https://bootswatch.com/3/flatly/bootstrap.css"},
			]
		}
	},
	watch: {
		theme: function (id) {
			$("#bootstrap-theme-css").attr("href", this.themes[id].url);
		},
	},
	methods: {
		themeChange: function (shift) {
			this.theme = this.modulo(this.theme + shift, this.themes.length);
		},
		modulo: function (dividend, divisor) {
			return ((dividend%divisor)+divisor)%divisor;
		}
	}
});
