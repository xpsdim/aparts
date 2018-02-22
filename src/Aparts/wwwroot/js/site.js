$(document).ready(function () {
	$('#loginForm a').kendoButton({
		click: function (e) {
			window.location = e.event.target.href;
		}
	});
	$("#logoutForm, #loginForm").show();

	kendo.popupNotification = $("#popupNotification").kendoNotification().data("kendoNotification");
});