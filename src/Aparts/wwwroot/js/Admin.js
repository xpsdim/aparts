$(document).ready(function () {

	var rolesDs = [];
	var storedDs = {
		transport: {
			read: "/Admin/Storelist",
			update: {
				type: "POST",
				url: "/Admin/UpdateStore",
				data: {
					__RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
				}
			}
		},

		autoSync: true,
		schema: {
			model: {
				id: "id",
				fields: {
					id: { editable: false },
					caption: { editable: true },
					storeManName: { editable: true }
				}
			}
		}
	};

	$("#tabstrip").kendoTabStrip({
		value: "Users"
	}).show();

	$.ajax({ url: "/Admin/RolesList" }).done(function (data) {
		rolesDs = data;
	});


	$("#gridUsers").kendoGrid({
		dataSource: {
			transport: {
				read: "/Admin/UserList",
				update: {
					type: "POST",
					url: "/Admin/UpdateUser",
					data: {
						__RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
					}
				}
			},
			pageSize: 10,
			autoSync: true,
			schema: {
				data: "data",
				model: {
					id: "id",
					fields: {
						id: { editable: false },
						email: { editable: false },
						roles: { editable: true, sortable: false },
						visibleStores: { editable: true, sortable: false }
					}
				}
			}
		},
		sortable: true,
		editable: true,
		pageable: {
			refresh: true,
			pageSizes: true,
			buttonCount: 5
		},
		columns: [
			{
				field: "id",
				title: "Id",
				hidden: true
			}, {
				field: "email",
				title: "Email",
				editable: false
			}, {
				field: "roles",
				title: "Roles",
				template: "#= roles.join(', ') #",
				editor: function (container, options) {
					$("<select multiple='multiple' data-bind='value :roles'/>")
						.appendTo(container)
						.kendoMultiSelect({
							autoBind: false,
							dataSource: rolesDs
						});
				}
			},
			{
				field: "visibleStores",
				title: "Visible Stores",
				template: function (dataItem) {
					var formattedStores = "";
					dataItem.visibleStores.forEach(function (el) {
						if (formattedStores !== "") {
                            formattedStores = formattedStores + ", ";
						}
						formattedStores = formattedStores + el.caption;
					});
					return formattedStores;
				},
				editor: function (container, options) {
					$("<select multiple='multiple' data-bind='value :visibleStores'/>")
						.appendTo(container)
						.kendoMultiSelect({
							autoBind: false,
							dataSource: storedDs,
							dataTextField: "caption",
							dataValueField: "id"
						});
				}
			}
		]
	});


	$("#gridStores").kendoGrid({
		dataSource: storedDs,
		sortable: true,
		editable: true,
		columns: [
			{
				field: "id",
				title: "Id"
			}, {
				field: "caption",
				title: "Caption"
			}, {
				field: "storeManId",
				title: "StoreMan Name",
				template: "#= storeManName #",
				editor: function (container, options) {
					$("<select data-bind='value :storeManName'/>")
						.appendTo(container)
						.kendoDropDownList({
							autoBind: false,
							dataSource: rolesDs
						});
				}
			}
		]
	});

	$("#ImportData").kendoButton({
		click: function (e) {
			var $body = $("body");
			kendo.ui.progress($body, true);
			$.ajax(
				{
					type: 'POST',
					url: '/Admin/ImportData/',
					dataType: 'json',
					success: function (response) {
						if (!response.success) {
							kendo.popupNotification.show(response.status, "error");
						} else {
							window.apartApp.showInfoDialog("Information", "Import successfully done!");
						}
					}
				}).always(function() {
					kendo.ui.progress($body, false);
			});
		}
	});
	
});
