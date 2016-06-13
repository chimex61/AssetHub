$(function () {
    $("#editContainer").on('submit', '#editForm', function (event) {
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
                    $("#editContainer").html(result);
                }
            }
        });
    });

    $("#editContainer").on('click', "#addPropertyBtn", function () {
        var tableRow =
            '<tr class="success"> \
                <td hidden> \
                    <input type="hidden" name="NewProperties.Index" value="' + propertyCounter + '" /> \
                </td> \
                <td class="col-md-4"> \
                    <input class="form-control text-box single-line" data-val-required="The Name field is required." name="NewProperties[' + propertyCounter + '].Name" type="text" value="" /> \
                </td> \
                <td class="col-md-2"> \
                    <input data-val="true" data-val-required="The IsNumeric field is required." name="NewProperties[' + propertyCounter + '].IsNumeric" type="checkbox" value="true" /><input name="Properties[' + propertyCounter + '].IsNumeric" type="hidden" value="false" /> \
                </td> \
                <td class="col-md-1"> \
                    <button type="button" class="btn btn-default" onclick="remove_property(this)">Remove</button> \
                </td> \
                <td> \
                    <span class="field-validation-valid text-danger" data-valmsg-for="NewProperties[' + propertyCounter + ']" data-valmsg-replace="true"></span> \
                </td> \
            </tr>';
        $('#propertyTable tr:last').after(tableRow);
        propertyCounter++;
    });

    $("#editContainer").on('click', '#cancelBtn', function () {
        var btn = $(this);
        btn.html('Remove');
        btn.prop('id', 'deleteBtn');
        btn.closest('tr').toggleClass('danger');

        var html = btn.closest('tr').html();
        var regex = /Deleted/gi;
        btn.closest('tr').html(html.replace(regex, ""));
    });

    $("#editContainer").on('click', '#deleteBtn', function () {
        var btn = $(this);
        btn.html('Cancel');
        btn.prop('id', 'cancelBtn');
        btn.closest('tr').toggleClass('danger');

        var html = btn.closest('tr').html();
        var regex = /Properties/gi;
        btn.closest('tr').html(html.replace(regex, "DeletedProperties"));
    })
});

function remove_property(btn) {
    var parent = btn.parentNode.parentNode;
    parent.remove();
}