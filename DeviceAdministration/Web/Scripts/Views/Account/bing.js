function () {
    $.get("http://www.bing.com/HPImageArchive.aspx?format=xml&idx=0&n=1&mkt=en-US",function(data){
        //  $.get("~/Content/img/logo.svg", function (data) {
        background = data;
        var res = background.split("<url>");
        var res1 = res[1].split("</url>");
        background = res1[0];
        $("#NameOfTheDivToChange").style.backgroundImage = "url('http://bing.com" + background + "')";
        $("#NameOfTheDivToChange").style.backgroundSize = "100%";          
    })
}
$.ajax({
    url: "http://www.bing.com/HPImageArchive.aspx?format=xml&idx=0&n=1&mkt=en-US",
    dataType: "jsonp",
    success: function (data) {
        background = data;
        var res = background.split("<url>");
        var res1 = res[1].split("</url>");
        background = res1[0];
        $("#NameOfTheDivToChange").style.backgroundImage = "url('http://bing.com" + background + "')";
        $("#NameOfTheDivToChange").style.backgroundSize = "100%";          
    }
} )