$(document).ready(function() {
    var dirChange = false;

    $("#btnAlive").mouseenter(function (e) {
        e.preventDefault(); 
        $(this).animate({
               opacity : 0
            }, 300, "easeInOutBounce"
        );
       
    });


    $("#btnAlive").mouseleave(function (e) {
        $(this).animate({
            opacity: 1
        }, 300, "easeInOutBounce"
        );
    });

    $("#datepicker").datepicker()
        .datepicker("option", "dateFormat", "dd/mm/yy");

    $(".puff").hide();
    $(".puff").toggle("blind");

    //$(".tableList").hide();
    //$(".tableList").toggle("highlight");


    $(".trItem").hover(function() {
        $(this).animate({
           // backgroundColor: "#336DA6",
            backgroundColor: "#87EBE2",
            color: "#175485"
        }, 100);

       
    });

    $(".trItem").mouseleave(function() {
        $(this).animate({
            backgroundColor: "white",
            color: "black"
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