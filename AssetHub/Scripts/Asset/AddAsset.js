$(function () {
    $("#addContainer").on('change', '#modelSelector', function () {
        var id = $('option:selected', this).val();
        $.getJSON('GetAssetModelProperties/' + id, function (data) {
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
                            <input type="hidden" name="Properties[' + i + '].PropertyId" value="' + property.PropertyId + '" /> \
                        </td> \
                        <td> \
                            <input class="form-control" name="Properties[' + i + '].Name" type="hidden" value="' + property.Name + '">' + property.Name + ' \
                        </td> \
                        <td> \
                            <input class="form-control" name="Properties[' + i + '].IsNumeric" type="hidden" value="' + property.IsNumeric + '">' + (JSON.parse(property.IsNumeric) ? '<input checked="checked" class="check-box" disabled="disabled" type="checkbox">' : '<input class="check-box" disabled="disabled" type="checkbox">') + '\
                        </td> \
                        <td> \
                            <input class="form-control text-box single-line" data-val-required="Property value is required" name="Properties[' + i + '].Value" type="text" value="" /> \
                        </td> \
                        <td> \
                            <span class="field-validation-valid text-danger" data-valmsg-for="Properties[' + i + ']" data-valmsg-replace="true"></span> \
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

    $("#addContainer").on('submit', '#addForm', function (event) {
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
                            window.location.href = '/Asset/Index'
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