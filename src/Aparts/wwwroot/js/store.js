$(document).ready(function () {
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
				serverPaging: true,
				serverFiltering: true,
				serverSorting: true,
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
				}
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
});
