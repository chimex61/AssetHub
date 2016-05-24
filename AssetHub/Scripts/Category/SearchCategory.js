$(function () {
    $("#searchContainer").on('click', '#searchBtn', function () {
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
});