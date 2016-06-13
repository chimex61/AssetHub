$(function () {
    $("#editBtn").click(function () {
        $(this).text(inEdit ? "Cancel" : "Edit");
        $("#viewContainer").toggle();
        $("#editContainer").toggle();
        inEdit = !inEdit;
    });

    $("#deleteBtn").click(function () {
        if (confirm("Are you sure you want to delete this category?")) {
            $.ajax({
                url: deleteUrl,
                type: 'POST',
                success: function (result) {
                    if (result.Success) {
                        alert(result.Message);
                        window.location.href = '/Category/Index'
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