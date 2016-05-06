var startLoading = function () {
    $(".fakeLoader").show();
    $(".registerTab").bind('click', false)
    $(".submitButton").attr("disabled", true);
}

var endLoading = function () {
    $(".fakeLoader").hide();
    $(".registerTab").unbind('click', false)
    $(".submitButton").attr("disabled", false);
}

var directToHomePage = function (result) {
    if (result.isRedirect) {
        window.location.href = result.redirectUrl;
    }
}