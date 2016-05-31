$(function () {
    $("#editContainer").on('change', '#modelSelector', function () {
        var id = $('option:selected', this).val();
        $.getJSON('/Asset/GetAssetModelProperties/' + id, function (data) {
            var table =
                '<h4>Properties</h4> <hr /> \
                <span class="field-validation-valid text-danger" data-valmsg-for="Properties" data-valmsg-replace="true"></span> \
                <div class="form-group"> \
                    <table class="table table-striped" id="propertyTable"> \
                    <thead> \
                        <tr> \
                            <th>Name</th> \
                            <th>Is numeric</th> \
                            <th>Value</th> \
                            <th></th> \
                        </tr> \
                    </thead> \
                    <tbody>';
            $.each(data, function (i, property) {
                var editor =
                    '<tr> \
                        <td hidden> \
                            <input type="hidden" name="Properties.Index" value="' + i + '" /> \
                            <input type="hidden" name="Properties[' + i + '].ModelPropertyId" value="' + property.PropertyId + '" /> \
                            <input type="hidden" name="Properties[' + i + '].AssetPropertyId" value="-1"/> \
                        </td> \
                        <td class="col-md-2"> \
                            <input class="form-control" name="Properties[' + i + '].Name" type="hidden" value="' + property.Name + '">' + property.Name + ' \
                        </td> \
                        <td class="col-md-2"> \
                            <input class="form-control" name="Properties[' + i + '].IsNumeric" type="hidden" value="' + property.IsNumeric + '">' + (JSON.parse(property.IsNumeric) ? '<input checked="checked" class="check-box" disabled="disabled" type="checkbox">' : '<input class="check-box" disabled="disabled" type="checkbox">') + '\
                        </td> \
                        <td class="col-md-4"> \
                            <input class="form-control text-box single-line" data-val-required="Property value is required" name="Properties[' + i + '].Value" type="text" value="" /> \
                        </td> \
                        <td class="col-md-1"> \
                            <button type="button" class="btn btn-default" onclick="remove_property(this)">Remove</button> \
                        </td> \
                        <td> \
                            <span class="field-validation-valid text-danger" data-valmsg-for="Properties[' + i + '].Value" data-valmsg-replace="true"></span> \
                        </td> \
                    </tr>';

                table += editor;
            });
            table +=
                                '</tbody> \
                        </table> \
                        <hr /> \
                </div>';

            $("#propertyContainer").html(table);
        });
    });

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
});

function remove_property(btn) {
    var parent = btn.parentNode.parentNode;
    parent.remove();
}