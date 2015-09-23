$(function () {
    var editFormUrl = "/Car/Edit/";
    var editDialog = $("#editTest");
    var fullUrl;
    editDialog.dialog(
    {
        autoOpen: false,
        width: 900,
        height: 700,
        buttons: [
            {
                text: "Save",
                click: function () {
                    var form = $("#EditForm form");
                    var formData = form.serialize();
                    $.post(fullUrl, formData, function (result, status, xhr) {
                        if (xhr.status === 200) {
                            $("#tableContainer").html(result);
                            $(".edBtn").click(edit);
                            $(".delBtn").click(mydelete);
                            $(".detailBtn").click(details);
                            editDialog.dialog("close");
                        } else {
                            alert("ABORT MISSION \nerror: " + xhr.status);
                        }
                    });
                }
            },
            {
                text: "Cancel",
                click: function () {
                    editDialog.dialog("close");
                }
            }
        ]
    });

    function edit(e) {
        e.preventDefault();
        var itemId = $(this).attr("id");
        fullUrl = editFormUrl + itemId;

        $("#EditForm").load(fullUrl, function () {
            $.validator.unobtrusive.parse("#EditForm");
            editDialog.dialog("open");
        });
    }

    $(".edBtn").click(edit);







    var delFormUrl = "/Car/Delete/";
    var delDialog = $("#delTest");

    delDialog.dialog({
        autoOpen: false,
        width: 600,
        buttons: [
            {
                text: "Delete",
                click: function() {
                    $.post(fullUrl, function(result, status, xhr) {
                        if (xhr.status === 200) {
                            $("#tableContainer").html(result);
                            $(".delBtn").click(mydelete);
                            $(".edBtn").click(edit);
                            $(".detailBtn").click(details);
                            delDialog.dialog("close");
                        } else {
                            alert("HOUSTON WE HAVE A PROBLEM");
                        }
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


    $(".delBtn").click(mydelete);

    function mydelete(e) {
        e.preventDefault();
        var itemId = $(this).attr("id");
        fullUrl = delFormUrl + itemId;

        $("#DeleteForm").load(fullUrl, function() {

            delDialog.dialog("open");

        });
    }


    var detailFormUrl = "/Car/Details/";
    var detailDialog = $("#detailTest");

    detailDialog.dialog({
        autoOpen: false,
        width: 600,
        buttons: [
            {
                text: "Ok",
                click: function () {
                    detailDialog.dialog("close");
                }

            }
        ]
    });


    $(".detailBtn").click(details);

    function details(e) {
        e.preventDefault();
        var itemId = $(this).attr("id");
        fullUrl = detailFormUrl + itemId;

        $("#detailForm").load(fullUrl, function () {

            detailDialog.dialog("open");

        });
    }





});