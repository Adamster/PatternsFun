$(document).ready(function() {


    $("#btnAlive").mouseenter(function(e) {

        var test = e;

        $(this).animate({
                left: "+=250"
            }, 300, "easeInOutBounce"
        );
    });

    $("#datepicker").datepicker();


})