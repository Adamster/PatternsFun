$(function() {
    var formUrl = "/Car/Create";
    var createDialog = $("#test");

    createDialog.dialog(
    {
        autoOpen: false,
        width: 900,
        height: 700,
        buttons: [
            {
                text: "Save",
                click: function() {
                    var form = $("#FormCreation form");
                    var formData = form.serialize();
                    $.post(formUrl, formData, function(result, status, xhr) {
                        if (xhr.status === 200) {
                            $("#tableContainer").html(result);
                            createDialog.dialog("close");
                        } else {
                            alert("ABORT MISSION \nerror: " + xhr.status);
                        }
                    });
                }
            },
            {
                text: "Cancel",
                click: function() {
                    createDialog.dialog("close");
                }
            }
        ]
    });

    $("#CreateBtn").click(function(e) {
        e.preventDefault();

        $("#FormCreation").load(formUrl, function() {
            $.validator.unobtrusive.parse("#FormCreation");
            createDialog.dialog("open");
        });


    });


});