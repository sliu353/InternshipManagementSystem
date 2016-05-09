var startLoading = function (id) {
    try{
        $(".fakeLoader").show();
        $(".submitButton").attr("disabled", true);
        $(".failingAlert").hide();
    }
    catch(er) {
        $("#" + id + " .fakeLoader").show();
        $("#" + id + " input").attr("disabled", true);
        $("#" + id + " .failingAlert").hide();
    }
}

var endLoading = function (id) {
    try{
        $(".fakeLoader").hide();
        $(".submitButton").attr("disabled", false);
    }
    catch(er) {
        $("#" + id + " .fakeLoader").hide();
        $("#" + id + " input").attr("disabled", false);
    }
}

var failLoading = function (id) {
    endLoading(id);
    try {
        $(".fallingAlert").show();
    }
    catch(er) {
        $("#" + id + " .failingAlert").show();
    }
}