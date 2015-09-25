$(function() {
    $("#PilotJq").jqGrid({
        url: 'Grid/',
        datatype: "json",
        colNames: ['Name', 'Age', 'Team', 'Debut'],
        colModel: [
            { name: 'Name', index: 'Name' },
            { name: 'Age', index: 'Age' },
            { name: 'Team', index: 'Team' },
            { name: 'Debut', index: 'DebutDate' , formatter: "date" }
        ],
        rowNum: 5,
        rowList: [5, 10, 20],
        sortname: 'Name',
        pager: "#pilotPage",
        viewrecords: true,
        sortorder: "desc",

        caption: "Pilot Grid"
});
})