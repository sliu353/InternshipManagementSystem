var startLoading = function (id) {
    if (id == null) {
        $(".fakeLoader").show();
        $(".submitButton").attr("disabled", true);
        $(".failingAlert").hide();
    }
    else {
        $("#" + id + " .fakeLoader").show();
        $("#" + id + " .submitButton").attr("disabled", true);
        $("#" + id + " .failingAlert").hide();
    }
}

var endLoading = function (id) {
    if (id == null) {
        $(".fakeLoader").hide();
        $(".submitButton").attr("disabled", false);
    }
    else {
        $("#" + id + " .fakeLoader").hide();
        $("#" + id + " .submitButton").attr("disabled", false);
    }
}

var failLoading = function (id) {
    endLoading(id);
    if (id == null) {
        $(".fallingAlert").show();
    }
    else {
        $("#" + id + " .failingAlert").show();
    }
}