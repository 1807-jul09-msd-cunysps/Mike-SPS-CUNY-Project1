$(document).ready(
    function () {
        // Change content
        //var para1 = $("#area").text();
        //alert(para1);

        // Change  css
        //var para1 = $(".red").css("background", "red");

        // Events
        //$(".red").click(
        //    function () {
        //        $(".red").css("background", "blue");
        //    }
        //);

        // Get val
        //$("#btnLabel1").blur(
        //    function () {
        //        console.log($("#btnLabel1").val());
        //    }
        //);

        // AJAX
        var url = "https://mdn.github.io/learning-area/javascript/oojs/json/superheroes.json";
        var para = $("#area");

        $("#GetHeroes").click(function () {
            $.ajax({
            url: url,
            success: function (response) {
                if (response != null) {
                    para.text(JSON.stringify(response));
                }
                else {
                    alert("error");
                }
            }
        })});
    }
);