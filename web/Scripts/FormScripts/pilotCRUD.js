
//=======================================CREATE================================================
$(function () {
    var createFormUrl = "/Pilot/Create";
    var createPilotDialog = $("#createPilot");
    var fullUrl;
    createPilotDialog.dialog({
        autoOpen: false,
        autoReszie: true,
        width: 400,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "fold",
            duration: 500
        },
        buttons: [
            {
                text: "Save",
                click: function () {
                    $("#createPilotForm form").submit();
                }
            },
            {
                text: "Cancel",
                click: function() {
                    createPilotDialog.dialog("close");
                }
            }
        ]
});

    function submitpilot(e) {
        e.preventDefault();
        if ($(this).valid()) {

            var createform = $("#createPilotForm form");
            var createformData = createform.serialize();


            $.post(createFormUrl, createformData, function(result, status, xhr) {
                if (xhr.status === 200) {
                    debugger;
                    $("#pilotTableContainer").html(result);
                    $("#pCreatBtn").click(createPilot);
                    $(".pDetails").click(detailPilot);
                    $(".pEdit").click(editPilot);
                    $(".pDelete").click(pilotDelete);
                    createPilotDialog.dialog("close");
                } else {
                    alert("ABORT MISSION \nerror: " + xhr.status);
                }
            });
        }
        return false;
    }


    function createPilot(e) {
        e.preventDefault();
        $("#createPilotForm").load(createFormUrl, function () {
            $("#datepicker").datepicker({
                dateFormat: 'mm/dd/yy',
                maxDate: new Date()
        });

            $("#createPilotForm form").submit(submitpilot);
            $.validator.unobtrusive.parse("#createPilotForm");
            createPilotDialog.dialog("open");
        });
    }

    $("#pCreatBtn").click(createPilot);

//=======================================DETAILS================================================
    var detailtFormUrl = "/Pilot/Details/";
    var detailPilotDialog = $("#detailPilot");

    detailPilotDialog.dialog({
        autoOpen: false,
        autoReszie: true,
        width: 400,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "fold",
            duration: 500
        },
        buttons: [
            {
                text: "OK",
                click: function () {
                    detailPilotDialog.dialog("close");
                }
            }
        ]
    });

    $(".pDetails").click(detailPilot);

    function detailPilot(e) {
        e.preventDefault();
        var itemId = $(this).attr('id');
        fullUrl = detailtFormUrl + itemId;
        $("#detailFormPilot").load(fullUrl, function () {
            detailPilotDialog.dialog("open");
        });
    }


    //=======================================EDIT================================================
    var editFormUrl = "/Pilot/Edit/";
    var editPilotDialog = $("#EditPilot");

    editPilotDialog.dialog({
        autoOpen: false,
        autoResize: true,
        width: 400,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "fold",
            duration: 500
        },
        buttons: [
        {
            text: "Save",
            click: function () {

                $("#EditPilotForm form").submit();


            }
        },
           {
               text: "Cancel",
               click: function () {
                   editPilotDialog.dialog("close");
               }
           }
        ]
    });

    $(".pEdit").click(editPilot);

    function sumbitEditPilot(e) {
        if($(this).valid()){
            var editForm = $("#EditPilotForm form");
            var editFormData = editForm.serialize();
            $.post(fullUrl, editFormData, function (result, status, xhr) {
                if (xhr.status === 200) {
                    $("#pilotTableContainer").html(result);
                    $("#pCreatBtn").click(createPilot);
                    $(".pDetails").click(detailPilot);
                    $(".pEdit").click(editPilot);
                    $(".pDelete").click(pilotDelete);
                    editPilotDialog.dialog("close");
                }
                else {
                    alert("ABORT MISSION \nerror: " + xhr.status);
                }

            });
        }
        return false;


    }


    function editPilot(e) {
        e.preventDefault();
        var itemId = $(this).attr('id');
        fullUrl = editFormUrl + itemId;
        $("#EditPilotForm").load(fullUrl, function () {

            $("#EditPilotForm form").submit(sumbitEditPilot);
            $.validator.unobtrusive.parse("#EditPilotForm");
            editPilotDialog.dialog("open");
        });
    }

    //=======================================DELETE================================================
    var delFormUrl = "/Pilot/Delete/";
    var delFormDialog = $("#DelPilot");

    delFormDialog.dialog({
        autoOpen: false,
        show: {
            effect: "clip",
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
                click: function () {
                    $.post(fullUrl, function (result, status, xhr) {
                        if (xhr.status === 200) {
                            $("#pilotTableContainer").html(result);
                            $("#pCreatBtn").click(createPilot);
                            $(".pDetails").click(detailPilot);
                            $(".pEdit").click(editPilot);
                            $(".pDelete").click(pilotDelete);
                            delFormDialog.dialog("close");
                        } else {
                            alert("HOUSTON WE HAVE A PROBLEM");
                        }
                    });


                }
            },
            {
                text: "Cancel",
                click: function () {
                    delFormDialog.dialog("close");
                }

            }
        ]
    });


    $(".pDelete").click(pilotDelete);

    function pilotDelete(e) {
        e.preventDefault();
        var itemId = $(this).attr("id");
        fullUrl = delFormUrl + itemId;

        $("#delPilotForm").load(fullUrl, function () {
            delFormDialog.dialog("open");

        });
    }

})