$(function () {
    $("#createContainer").on('submit', '#createForm', function (event) {
        event.preventDefault();
        var form = $(this);
        if (form.valid()) {
            $.ajax({
                type: form.attr('method'),
                url: form.attr('action'),
                data: form.serialize(),
                success: function (result) {
                    if (result.Success != null) {
                        if (result.Success) {
                            alert(result.Message);
                            window.location.href = '/Admin/UserIndex'
                        }
                        else if (result.Message != "") {
                            alert(result.Message);
                        }
                        else {
                            alert("Unknown error");
                        }
                    }
                    else {
                        $("#createContainer").html(result);
                    }
                }
            });
        }
    });
});