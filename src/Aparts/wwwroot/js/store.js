$(document).ready(function () {

    function getRightAlignTemplate(field) {
        var tpl1 = '<div style="text-align:right;">#= ', tpl2 = '#</div>';
        return tpl1 + field + " != null ? " + field + " : '' " + tpl2;
    }

	$("#grid").kendoGrid({
		dataSource: {
			transport: {
				read: "/Store/GroupList"
			},
			schema: {
				data: "data"
			},
			serverSorting: false,
			sort: {
				field: "name",
				dir: "asc"
			}
		},
		sortable: true,
		detailInit: subgroupsInit,
		dataBound: function () {
			this.expandRow(this.tbody.find("tr.k-master-row").first());
		},
		columns: [
			{
				field: "name",
				title: "Group name"
			}
		]
	});

	function subgroupsInit(e) {
		$("<div/>").appendTo(e.detailCell).kendoGrid({
			dataSource: {
				type: "aspnetmvc-ajax",
				serverFiltering: true,
				transport: {
					read: {
						url: "/Store/Subgroups",
						type: "POST"
					}
				},
				filter: { field: "idGroup", operator: "eq", value: e.data.id },
				schema: {
					data: "data",
					model: { id: "id" }
				},
				serverSorting: false,
				sort: {
					field: "name",
					dir: "asc"
				}
			},
			detailInit: storeItemsInit,
			dataBound: function () {
				this.expandRow(this.tbody.find("tr.k-master-row").first());
			},
			scrollable: false,
			columns: [
				{
					field: "name",
					title: "Name"
				}
			]
		});
	}

	function storeItemsInit(e) {
		$("<div/>").appendTo(e.detailCell).kendoGrid({
			dataSource: {
				type: "aspnetmvc-ajax",
				serverPaging: true,
				serverFiltering: true,
				serverSorting: true,
				transport: {
					read: {
						url: "/Store/StoreItems",
						type: "POST"
					}
				},
				filter: { field: "idSubGroup", operator: "eq", value: e.data.id },
				schema: {
					data: "data",
					model: { id: "id" }
				}
			},
			scrollable: false,
            columns: function (e) {
                
                var columns = [
                    { field: "id", title: "Id", width: 80 },
					{ field: "codesByCatalog", title: "Codes", width: 500 },
                    { field: "priceIn", title: "Price In", width: 100, template: getRightAlignTemplate("priceIn") },
                    { field: "price", title: "Price", width: 100, template: getRightAlignTemplate("price") }
                ];
                $.each(window.apartApp.visibleStores, function (key, value) {
                    value.width = 50;
                    value.template = getRightAlignTemplate(value.field)
                    columns.push(value);
                });

				return columns;
			}()
		});
    }
});
