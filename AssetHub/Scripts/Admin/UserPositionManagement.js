$(function () {
    $("#container").on('submit', '#form', function (event) {
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
                    $("#container").html(result);
                }
            }
        });
    });

    $("#container").on('click', "#addPositionBtn", function () {
        var tableRow =
            '<tr class="success"> \
                <td hidden> \
                    <input type="hidden" name="NewPositions.Index" value="' + positionCounter + '" /> \
                </td> \
                <td class="col-md-4"> \
                    <input class="form-control text-box single-line" data-val-required="Position name is required" name="NewPositions[' + positionCounter + '].Name" type="text" value="" /> \
                </td> \
                <td class="col-md-1"> \
                    <button type="button" class="btn btn-default" onclick="remove_position(this)">Remove</button> \
                </td> \
                <td> \
                    <span class="field-validation-valid text-danger" data-valmsg-for="NewPositions[' + positionCounter + ']" data-valmsg-replace="true"></span> \
                </td> \
            </tr>';
        $('#positionTable tr:last').after(tableRow);
        positionCounter++;
    });

    $("#container").on('click', '#cancelBtn', function () {
        var btn = $(this);
        btn.html('Remove');
        btn.prop('id', 'deleteBtn');
        btn.closest('tr').toggleClass('danger');

        var html = btn.closest('tr').html();
        var regex = /Deleted/gi;
        btn.closest('tr').html(html.replace(regex, ""));
    });

    $("#container").on('click', '#deleteBtn', function () {
        var btn = $(this);
        btn.html('Cancel');
        btn.prop('id', 'cancelBtn');
        btn.closest('tr').toggleClass('danger');

        var html = btn.closest('tr').html();
        var regex = /Positions/gi;
        btn.closest('tr').html(html.replace(regex, "DeletedPositions"));
    })
});

function remove_position(btn) {
    var parent = btn.parentNode.parentNode;
    parent.remove();
}