$(document).ready(function() {
    var dirChange = false;

    $("#btnAlive").mouseenter(function(e) {
        $(this).animate({
                left: "+=250"
            }, 300, "easeInOutBounce"
        );
    });

    $("#datepicker").datepicker()
    .datepicker("option", "dateFormat", "dd/mm/yy");

    $("#puff").hide();
    $("#puff").toggle("blind");
   
    $("#tableList").hide();
    $("#tableList").toggle("highlight");

})