var vueApp = new Vue({
	el: "#vueContainer",
	data: function () {
		return {
			search: {
				productName: null,
				productDesc: null,
				catalogId: null,
				active: true
			},
			preview: [],
			detail: {},
			newDto: {},
			pk: "productId",
			newDetailKey: "product",
			apiUrl: "/api/Product/",
			//initialLoad: false
		}
	},
	methods: {
		itemActive: function (previewId) {
			if (!this.$refs.hasOwnProperty("pdRef")) {
				return false;
			}

			return this.$refs["pdRef"].itemActive(previewId, this.detail[this.pk]);
		},
		detailLoad: function (id) {
			this.$refs['pdRef'].detailLoad(id);
		},
		refreshSearch: function () {
			this.$refs["pdRef"].refreshSearch();
		},
		resetSearch: function () {
			this.componentDataReset("search");
			this.refreshSearch();
		},
		fileAdded: function (e) {
			var vm = this;
			var chain = Promise.resolve();
			var newResources = [];

			for (var i = 0; i < e.target.files.length; i++) {
				var file = e.target.files[i];

				(function (f) {
					chain = chain.then(function () {
						return file2Base64(f);
					}).then(function (b64) {
						newResources.push(vm.createNewProductResource(f, b64));
					});
				})(file);
			}

			chain = chain.then(function () {
				vm.detail.resources = newResources.concat(vm.detail.resources);
				fixSortOrder(vm.detail.resources);

				//Reset file input
				e.target.value = null;
			});

			chain.catch(function (e) {
				console.log("fileAdded failure", e);
			});
		},
		createNewProductResource: function (file, b64) {
			//Resources contain files. Create new extended file then put it in new extended resource
			var fileDto = Object.assign({}, this.newDto["resource"].file, {
				fileId: nextNewId(),
				fileName: file.name,
				mimeType: file.type,
				content: b64,
				dateCreated: new Date().toISOString()
			});

			return Object.assign({}, this.newDto["resource"], {
				productResourceId: nextNewId(),
				productId: this.detail.productId,
				file: fileDto,
				sortOrder: 0,
				active: true,
				dateCreated: new Date().toISOString()
			});
		},
		smartSrc: function (fileDto) {
			if (fileDto.content) {
				return "data:" + fileDto.mimeType + ";base64," + fileDto.content;
			}
			return "/api/File/" + fileDto.fileId;
		}
	}
});
