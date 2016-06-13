$(function () {
    $("#editBtn").click(function () {
        $(this).text(inEdit ? "Cancel" : "Edit");
        $("#viewContainer").toggle();
        $("#editContainer").toggle();
        inEdit = !inEdit;
    });

    $("#deleteBtn").click(function () {
        if (confirm("Are you sure you want to delete this asset?")) {
            $.ajax({
                url: deleteUrl,
                type: 'POST',
                success: function (result) {
                    if (result.Success) {
                        alert(result.Message);
                        window.location.href = '/Asset/Index'
                    } else if (result.Message != "") {
                        alert(result.Message);
                    }
                    else {
                        alert("Unknown error");
                    }
                }
            });
        }
    });

    $("#reportBtn").click(function () {
        $("#viewContainer").hide();
        $("#editContainer").hide();
        $("#reportContainer").show();
    })

    $("#changeLocationBtn").click(function () {
        $("#viewLocationContainer").toggle();
        $("#changeLocationContainer").toggle();
    });

    $("#changeLocationContainer").on('submit', '#changeLocationForm', function () {
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
                    else if (result.Message != "") {
                        alert(result.Message);
                    }
                    else {
                        alert("Unknown error");
                    }
                }
                else {
                    $("#changeLocationContainer").html(result);
                }
            }
        });
    });
});