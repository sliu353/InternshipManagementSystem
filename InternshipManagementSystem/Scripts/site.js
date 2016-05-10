var startLoading = function (id) {
    if (typeof id != "string"){
        $(".fakeLoader").show();
        $(".submitButton").attr("disabled", true);
        $(".failingAlert").hide();
    }
    else
    {
        $("#" + id + " .fakeLoader").show();
        $("#" + id + " input").attr("disabled", true);
        $("#" + id + " .failingAlert").hide();
    }
}

var endLoading = function (id) {
    if (typeof id != "string"){
        $(".fakeLoader").hide();
        $(".submitButton").attr("disabled", false);
    }
    else{
        $("#" + id + " .fakeLoader").hide();
        $("#" + id + " input").attr("disabled", false);
    }
}

var failLoading = function (id) {
    endLoading(id);
    if (typeof id != "string"){
        $(".fallingAlert").show();
    }
    else {
        $("#" + id + " .failingAlert").show();
    }
}