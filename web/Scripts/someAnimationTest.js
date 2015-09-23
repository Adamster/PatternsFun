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

    $(".puff").hide();
    $(".puff").toggle("blind");

    $(".tableList").hide();
    $(".tableList").toggle("highlight");


    $(".trItem").hover(function() {
        $(this).animate({
            backgroundColor: "#336DA6",
            color: "white"
        }, 100);

        $(".edd").animate({
            color: "white"
        }, 100);

    });

    $(".trItem").mouseleave(function() {
        $(this).animate({
            backgroundColor: "white",
            color: "black"
        }, 850);

        $(".edd").animate({
            color: "#336DA6"
        }, 850);


    });
    $("#Speed").hide();
    var t = $("#HorsePowers").contents();
    $("#Speed").val(t.text());


    $("#Speed").myfunc({ divFact: 10 });
    setTimeout(function() {
        $("#Speed").change();
    }, 400);


})