$(function () {
    $("#searchContainer").on('click', '#resetAssetModelBtn', function () {
        $("#modelSelector").val(-1);
    });

    $("#searchContainer").on('click', '#searchBtn', function () {
        var form = $("#searchForm");

        $.getJSON(form.attr('action'), form.serialize(), function (result) {
            if (result.Success) {
                $("#resultsTable tbody").empty();
                $.each(result.Models, function (i, model) {
                    var row =

                        '<tr onclick="location.href = \'/Asset/ViewAsset/' + model.Id + '\'">\n \
                                <td>' + model.Name + '</td>\n \
                                <td>' + model.SerialNumber + '</td>\n \
                                <td>' + model.AssetModel + '</td>\n \
                                <td><a class="btn btn-default" href="/Asset/ViewAsset/' + model.Id + '">View</a></td>\n \
                            </tr>\n';
                    $("#resultsTable tbody").append(row);
                });
            } else {
                alert("No results found");
            }
        });
    });
});