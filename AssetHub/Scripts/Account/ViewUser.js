$(function () {
    $("#editBtn").click(function () {
        $(this).text(inEdit ? "Cancel" : "Edit");
        if (!inEdit) {
            $("#viewContainer").show();
            $("#editContainer").hide();
        }
        else {
            $("#viewContainer").hide();
            $("#editContainer").show();
        }
        $("#changePasswordContainer").hide();
        inEdit = !inEdit;
    });

    $("#changePasswordBtn").click(function () {
        $("#changePasswordContainer").show();
        $("#viewContainer").hide();
        $("#editContainer").hide();
    });

    $("#reportBtn").click(function () {
        $("#viewContainer").hide();
        $("#editContainer").hide();
        $("#changePasswordContainer").hide();
        $("#reportContainer").show();
    })

    $("#changePasswordContainer").on('submit', '#changePassForm', function (event) {
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
                    $("#changePasswordContainer").html(result);
                }
            }
        });
    });

    $("#deleteBtn").click(function () {
        if (confirm("Are you sure you want to delete this user?")) {
            $.ajax({
                url: deleteUrl,
                type: 'POST',
                success: function (result) {
                    if (result.Success) {
                        alert(result.Message);
                        window.location.href = '/Admin/UserIndex'
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
});