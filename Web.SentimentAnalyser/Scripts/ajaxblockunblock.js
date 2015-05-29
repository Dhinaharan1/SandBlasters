function startAjaxRequest(strPath) {
    strPath = strPath == null ? '<img src="Images/Loading.gif"/>' : '<img src="Images/Loading.gif"/>';
    $.blockUI({ message: strPath, theme: false });
}
function endAjaxRequest() {
    $.unblockUI();
}