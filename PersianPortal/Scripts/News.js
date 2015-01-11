function HideOthersEc() {
    $(".economics").css("display", "block");
    $(".politics").css("display", "none");
    $(".science").css("display", "none");
    $(".cultural").css("display", "none");
    $(".sports").css("display", "none");
    $(".art").css("display", "none");
}
function HideOthersPo() {
    $(".economics").css("display", "none");
    $(".politics").css("display", "block");
    $(".science").css("display", "none");
    $(".cultural").css("display", "none");
    $(".sports").css("display", "none");
    $(".art").css("display", "none");
}
function HideOthersAr() {
    $(".economics").css("display", "none");
    $(".politics").css("display", "none");
    $(".science").css("display", "none");
    $(".cultural").css("display", "none");
    $(".sports").css("display", "none");
    $(".art").css("display", "block");
}
function HideOthersSc() {
    $(".economics").css("display", "none");
    $(".politics").css("display", "none");
    $(".science").css("display", "block");
    $(".cultural").css("display", "none");
    $(".sports").css("display", "none");
    $(".art").css("display", "none");
}
function HideOthersSp() {
    $(".economics").css("display", "none");
    $(".politics").css("display", "none");
    $(".science").css("display", "none");
    $(".cultural").css("display", "none");
    $(".sports").css("display", "block");
    $(".art").css("display", "none");
}
function HideOthersCu() {
    $(".economics").css("display", "none");
    $(".politics").css("display", "none");
    $(".science").css("display", "none");
    $(".cultural").css("display", "block");
    $(".sports").css("display", "none");
    $(".art").css("display", "none");
}
function showAllnews() {
    console.log("kharrrrrr");
    $(".economics").css("display", "block");
    $(".politics").css("display", "block");
    $(".science").css("display", "block");
    $(".cultural").css("display", "block");
    $(".sports").css("display", "block");
    $(".art").css("display", "block");
}


function spellCheck() {
    if ($("#searchInput").val() == "سلنام" || $("#searchInput").val() == "اسعار" || $("#searchInput").val() == "احبار")
        $("#searchInput").css("color", "blue");
    if ($("#searchInput").val() == "سسس" || $("#searchInput").val() == "..." || $("#searchInput").val() == "پپ")
        $("#searchInput").css("color", "red");
    if ($("#searchInput").val() == "")
        $("#searchInput").css("color", "black");
}
var count = 1;
function ShowResult() {
    if ($("#dictionary").val() != "")
        $("#searchResult").css("display", "block");
}

function AddToCart() {
    $("#ShoppingCartNum").css("display", "block");
    $("#ShoppingCartNum").html(count);
    count++;
}

function radio(id) {
    if (id == "rbShippingX")
        $("#rbShippingX").addAttr(checked, "checked");
    $("#rbShippingT").removeAttr(checked);
}


var count = 1;
function Omen() {
    if (count > 3)
        count = 1;
    switch (count) {
        case 1:
            $("#fal2").css("display", "none");
            $("#fal3").css("display", "none");
            $("#fal1").css("display", "block");
            break;
        case 2:
            $("#fal1").css("display", "none");
            $("#fal3").css("display", "none");
            $("#fal2").css("display", "block");
            break;
        case 3:
            $("#fal2").css("display", "none");
            $("#fal1").css("display", "none");
            $("#fal3").css("display", "block");
            break;

    }
    count++;
}