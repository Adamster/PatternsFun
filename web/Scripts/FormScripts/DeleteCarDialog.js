$(function() {
    var delFormUrl = "/Car/Delete/";
    var delDialog = $("#delTest");
    var fullrl;
    delDialog.dialog({
        autoOpen: false,
        width: 600,
        buttons: [
        {
            text: "Delete",
            click: function () {
                $.post(fullrl, function (result, status, xhr) {
                    $("#tableContainer").html(result);
                    delDialog.dialog("close");
                });


            }
},
            {
                text: "Cancel",
                click: function() {
                    delDialog.dialog("close");
                }
                
            }
        ]
    });


    $(".delBtn").click(function(e) {
        e.preventDefault();
        var itemId = $(this).attr("id");
        fullrl = delFormUrl + itemId;

        $("#DeleteForm").load(delFormUrl + itemId, function () {
            
            delDialog.dialog("open");

        });
    });

});