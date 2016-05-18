$(function () {
    $("#searchBtn").click( function () {
        if ($("#searchName").val() != "") {
            $.getJSON('GetSearchResults/?name=' + $("#searchName").val(), function (result) {
                if (result.Success) {
                    $("#resultsTable tbody").empty();
                    $.each(result.Categories, function (i, category) {
                        var result =
                            '<tr onclick="location.href = \'/Category/ViewCategory/' + category.Id + '\'">\n \
                                <td>' + category.Name + '</td>\n \
                                <td>' + category.ModelCount + '</td>\n \
                                <td><a class="btn btn-default" href="/Category/ViewCategory/' + category.Id + '">View</a></td>\n \
                            </tr>\n';
                        $("#resultsTable tbody").append(result);
                    });
                } else {
                    alert("No results found");
                }
            });
        }
    });

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
                }
            });
        }
    });

    $("#editForm").on('submit', function (event) {
        event.preventDefault();
        var form = $(this);
        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            success: function (result) {
                if (result.Success) {
                    alert(result.Message);
                } else if (result.Message != "") {
                    alert(result.Message);
                }
            }
        });
    });

    $("#addForm").on('submit', function (event) {
        event.preventDefault();
        var form = $(this);
        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            success: function (result) {
                if (result.Success) {
                    alert(result.Message);
                    window.location.href = '/Category/Index'
                } else if (result.Message != "") {
                    alert(result.Message);
                }
            }
        });
    });
});