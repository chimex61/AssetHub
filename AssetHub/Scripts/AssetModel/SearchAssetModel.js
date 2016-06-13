$(function () {
    $("#categorySelector").ready(function () {
        $("#categorySelector").val(-1);
    })

    $("#searchContainer").on('click', '#resetCategoryBtn', function() {
        $("#categorySelector").val(-1);
    });

    $("#searchContainer").on('click', '#searchBtn', function () {
        var form = $("#searchForm");

        $.getJSON(form.attr('action'), form.serialize(), function (result) {
            if (result.Success) {
                $("#resultsTable tbody").empty();
                $.each(result.Models, function (i, model) {
                    var row =
                        '<tr onclick="location.href = \'/AssetModel/ViewAssetModel/' + model.Id + '\'">\n \
                                <td>' + model.Name + '</td>\n \
                                <td>' + model.Category + '</td>\n \
                                <td>' + model.AssetCount + '</td>\n \
                                <td><a class="btn btn-default" href="/AssetModel/ViewAssetModel/' + model.Id + '">View</a></td>\n \
                            </tr>\n';
                    $("#resultsTable tbody").append(row);
                });
            } else {
                alert("No results found");
            }
        });
    });
});