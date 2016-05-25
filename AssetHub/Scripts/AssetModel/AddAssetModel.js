$(function () {
    $("#addContainer").on('load', '#categorySelector', function (event) {
        $("#categorySelector").val(selectedAssetModelId);
    });

    $("#addContainer").on('click', "#addPropertyBtn", function () {
        var tableRow =
            '<tr> \
                <td hidden> \
                    <input type="hidden" name="Properties.Index" value="' + propertyCounter + '" /> \
                </td> \
                <td class="col-md-4"> \
                    <input class="form-control text-box single-line" data-val-required="The Name field is required." name="Properties[' + propertyCounter + '].Name" type="text" value="" /> \
                </td> \
                <td class="col-md-1"> \
                    <input data-val="true" data-val-required="The IsNumeric field is required." name="Properties[' + propertyCounter + '].IsNumeric" type="checkbox" value="true" /><input name="Properties[' + propertyCounter + '].IsNumeric" type="hidden" value="false" /> \
                </td> \
                <td class="col-md-1"> \
                    <button type="button" class="btn btn-default" onclick="remove_property(this)">Remove</button> \
                </td> \
                <td> \
                    <span class="field-validation-valid text-danger" data-valmsg-for="Properties[' + propertyCounter + ']" data-valmsg-replace="true"></span> \
                </td> \
            </tr>';
        $('#propertyTable tr:last').after(tableRow);
        propertyCounter++;
    });

    $("#addContainer").on('submit', "#addForm", function (event) {
        event.preventDefault();
        var form = $(this);
        if (form.valid()) {
            $.ajax({
                type: form.attr('method'),
                url: form.attr('action'),
                data: form.serialize(),
                success: function (result) {
                    if (result.Success != null) {
                        if (result.Success) {
                            alert(result.Message);
                            window.location.href = '/AssetModel/Index'
                        }
                        else if (result.Message != "") {
                            alert(result.Message);
                        }
                        else {
                            alert("Unknown error");
                        }
                    }
                    else {
                        $("#addContainer").html(result);
                    }
                }
            });
        }
    });
});

function remove_property(btn) {
    var parent = btn.parentNode.parentNode;
    parent.remove();
}