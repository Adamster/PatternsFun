$(function () {
    var editFormUrl = "/Car/Edit/";
    var editDialog = $("#editTest");
    var fullUrl;
    editDialog.dialog(
    {
      
        autoOpen: false,
        width: 500,
        show: {
            effect: "scale",
            duration: 1000
        },
        hide: {
            effect: "fold",
            duration: 500
        },

        autoResize: true,
        buttons: [
            {
                text: "Save",
                click: function () {
                    var form = $("#EditForm form");
                    var formData = form.serialize();
                    $.post(fullUrl, formData, function(result, status, xhr) {
                        if (xhr.status === 200) {
                            $("#tableContainer").html(result);
                            $(".delBtn").click(mydelete);
                            $(".edBtn").click(edit);
                            $(".detailBtn").click(details);
                            $("#CreateBtn").click(createForm);
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


        show: {
            effect: "bounce",
            duration: 500
        },
        hide: {
            effect: "puff",
            duration: 500
        },


       
        autoResize: true,
        width: 400,
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
                            $("#CreateBtn").click(createForm);
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
        autoResize: true,

        show: {
            effect: "puff",
            duration: 500
        },
        hide: {
            effect: "puff",
            duration: 500
        },


        width: 500,
        buttons: [
            {
                text: "OK",
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


    var createFormUrl = "/Car/Create";
    var createDialog = $("#test");

    createDialog.dialog(
    {
        autoOpen: false,
        autoResize: true,
      
        buttons: [
            {
                text: "Save",
                click: function () {
                    var form = $("#content form");
                    var formData = form.serialize();
                    $.post(createFormUrl, formData, function (result, status, xhr) {
                        if (xhr.status === 200) {
                            $("#tableContainer").html(result);
                            $(".delBtn").click(mydelete);
                            $(".edBtn").click(edit);
                            $(".detailBtn").click(details);
                            $("#CreateBtn").click(createForm);
                            createDialog.dialog("close");
                        } else {
                            alert("ABORT MISSION \nerror: " + xhr.status);
                        }
                    });
                }
            },
            {
                text: "Cancel",
                click: function () {
                    createDialog.dialog("close");
                }
            }
        ]
    });


    $("#CreateBtn").click(createForm);
    function createForm(e) {
        e.preventDefault();

        $("#content").load(createFormUrl, function () {
            $.validator.unobtrusive.parse("#content");
            createDialog.dialog("open");
        });
    }




});