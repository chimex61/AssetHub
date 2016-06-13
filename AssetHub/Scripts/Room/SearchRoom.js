$(function () {
    $("#searchContainer").on('click', '#searchBtn', function () {
        var form = $("#searchForm");

        $.getJSON(form.attr('action'), form.serialize(), function (result) {
            if (result.Success) {
                $("#resultsTable tbody").empty();
                $.each(result.Rooms, function (i, room) {
                    var result =
                        '<tr onclick="location.href = \'/Rooms/ViewRoom/' + room.Id + '\'">\n \
                                <td>' + room.Name + '</td>\n \
                                <td><a class="btn btn-default" href="/Rooms/ViewRoom/' + room.Id + '">View</a></td>\n \
                            </tr>\n';
                    $("#resultsTable tbody").append(result);
                });
            } else {
                alert("No results found");
            }
        });
    });
});