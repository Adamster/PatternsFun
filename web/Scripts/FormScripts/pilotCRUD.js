
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

            $("#datepicker").datepicker().datepicker("option", "dateFormat", "dd/mm/yy");
            

            $("#createPilotForm form").submit(submitpilot);

            $.validator.unobtrusive.parse("#createPilotForm");

            createPilotDialog.dialog("open");
        });
    }

    $("#pCreatBtn").click(createPilot);


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

                var editForm = $("#EditPilotForm form");
                var editFormData = editForm.serialize();
                     $.post(fullUrl, editFormData, function (result, status, xhr) {
                    if (xhr.status === 200) {
                        $("#pilotTableContainer").html(result);
                        $("#pCreatBtn").click(createPilot);
                        $(".pDetails").click(detailPilot);
                        $(".pEdit").click(editPilot);
                        editPilotDialog.dialog("close");
                }
                else {
                        alert("ABORT MISSION \nerror: " + xhr.status);
                }
                   
                });
               

              
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

    function editPilot(e) {
        e.preventDefault();
        var itemId = $(this).attr('id');
        fullUrl = editFormUrl + itemId;
        $("#EditPilotForm").load(fullUrl, function() {
            editPilotDialog.dialog("open");
        });
    }


})