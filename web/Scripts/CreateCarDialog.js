$(function () {

    var createDialog = $("#test");

   createDialog.dialog();

    $("#CreateBtn").click(function(e) {
        e.preventDefault();
        
        createDialog.dialog("open");
        debugger;
    });
});