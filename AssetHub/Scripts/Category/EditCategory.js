$(function () {
    $("#editContainer").on('submit', '#editForm', function (event) {
        event.preventDefault();
        var form = $(this);

        var type = form.attr('method');
        var url = form.attr('action');
        var data = form.serialize();

        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            success: function (result) {
                if (result.Success != null) {
                    if (result.Success) {
                        alert(result.Message);
                        location.reload();
                    }
                    else if(result.Message != "") {
                        alert(result.Message);
                    }
                    else {
                        alert("Unknown error");
                    }
                }
                else {
                    $("#editContainer").html(result);
                }
            }
        });
    });
});