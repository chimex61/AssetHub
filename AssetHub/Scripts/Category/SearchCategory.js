$(function () {
    $("#searchContainer").on('load', '#categorySelector', function () {
        $("#categorySelector").val(-1);
    });

    $("#searchContainer").on('click', '#resetCategoryBtn', function () {
        $("#categorySelector").val(-1);
    });

    $("#searchContainer").on('click', '#searchBtn', function () {
        var form = $("#searchForm");

        $.getJSON(form.attr('action'), form.serialize(), function (result) {
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
    });
});