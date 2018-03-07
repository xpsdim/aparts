$(document).ready(function () {
	$('#loginForm a').kendoButton({
		click: function (e) {
			window.location = e.event.target.href;
		}
	});
	$("#logoutForm, #loginForm").show();

	kendo.popupNotification = $("#popupNotification").kendoNotification().data("kendoNotification");
	
    window.apartApp = window.apartApp || {};

	window.apartApp.processDoneDlg = $("#infoDialog").kendoDialog({
		visible: false,
		width: "50%",
		actions: [{
			text: "OK"
		}]
	}).data("kendoDialog");

    window.apartApp.showInfoDialog = function (title, message) {
        window.apartApp.processDoneDlg.title(title);
        window.apartApp.processDoneDlg.content(message);
        window.apartApp.processDoneDlg.open();
    };
});