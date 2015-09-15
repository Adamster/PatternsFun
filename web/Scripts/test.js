$(document).ready(function() {
    $("#btnAlive").mouseenter(function() {
        $(this).animate({
                left: "+=250",
                top: "+100"
            }, 3
        );
    });
})