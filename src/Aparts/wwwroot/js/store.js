$(document).ready(function () {
	var element = $("#grid").kendoGrid({
		dataSource: {
			transport: {
				read: "/Store/GroupList"
			},
			schema: {
				data: "data"
			},
			serverSorting: false
		},
		sortable: true,
		//detailInit: detailInit,
		/*dataBound: function () {
			this.expandRow(this.tbody.find("tr.k-master-row").first());
		},*/
		columns: [
			{
				field: "name",
				title: "Gtoup name"
			}
		]
	});
});
