var currentPostPage = "Default.aspx";

$(document).ready(function () {

    $("#fileLoadFeedback").uploadify({
        'swf': 'scripts/uploadify.swf',
        'cancelImg': 'Images/uploadify-cancel.png',
        'buttonText': '  Select Feedback File  ',
        'uploader': 'FeedbackUploaderHandler.ashx',
        'fileDesc': 'Customer Feedback',
        'fileExt': '*.txt',
        'multi': false,
        'auto': true,
        'onSelect': function (event, queueID, fileObj, response, data) {
            // CALL AjaxRequest
            
        },
        'onUploadComplete': function (file) {
            // End AjaxRequest
            //ProcessFeedback(file.name);
        },
        'onUploadSuccess': function (file, data, response) {
            //alert(file.name);
            ProcessFeedback(file.name);
        },
        'onError': function (event, queueID, fileObj, response, data) {
            // END AjaxRequest
        }
    });

});

function ProcessFeedback(updatedFileName) {
    //alert(updatedFileName);
    startAjaxRequest();
    $.post(currentPostPage, {
        Type: "ProcessUploadedFeedback",
        strFileName: updatedFileName
    }, function (data) {
        if ($.browser.msie || $.browser.mozilla) {
            startIndex = data.indexOf("##StartTag##") + 19
            endIndex = data.indexOf("##EndTag##") - 30
            $('#divGridView')[0].innerHTML = data.substring(startIndex, endIndex);
            endAjaxRequest();
        }

    });

}