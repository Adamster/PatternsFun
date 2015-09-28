$(function() {
    $("#CarJq").jqGrid({
        url: "/Grid/CarGridData",
        datatype: "json",
        colNames: ["Version", "Name", "Tank", "Info"],
        colModel: [
            { name: "Version", index: 'Name' },
            { name: "Name", index: 'Name' },
            { name: "Tank", index: 'Tank' },
            { name: "Info", index: 'Info' },
        ],
        rowNum: 5,
        rowList: [5, 10, 20],
        sortname: 'Name',
        pager: "#CarPage",
        viewrecords: true,
        sortorder: "desc",

        caption: "Car Grid"
});
})