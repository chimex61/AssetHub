$(function () {
    $("#searchContainer").on('click', '#resetRoomBtn', function() {
        $("#roomSelector").val(-1);
    });

    $("#searchContainer").on('click', '#searchBtn', function () {
        var form = $("#searchForm");

        $.getJSON(form.attr('action'), form.serialize(), function (result) {
            if (result.Success) {
                $("#resultsTable tbody").empty();
                $.each(result.Users, function (i, user) {
                    var row =
                       '<tr onclick="location.href = \'/Account/ViewUser/' + user.Id + '"> \
                            <td>' + user.Name + '</td> \
                            <td>' + user.Position + '</td> \
                            <td>' + user.Room + '</td> \
                            <td><a class="btn btn-default" href="/Account/ViewUser/' + user.Id + '">View</a></td> \
                        </tr>';

                    $("#resultsTable tbody").append(row);
                });
            } else {
                alert("No results found");
            }
        });
    });
});