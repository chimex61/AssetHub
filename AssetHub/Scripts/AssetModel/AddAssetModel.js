var propertyCounter = 0;

$(function () {
    $("#addPropertyBtn").click(function () {
        var form = '<div class="form-group"> \
                        <label class="control-label col-md-2">Property</label> \
                        <input type="hidden" name="Properties.Index" value="' + propertyCounter + '" /> \
                        <div class="col-xs-2"> \
                            <input type="text" class="form-control" name="Properties[' + propertyCounter + '].Name"> \
                        </div> \
                        <div class="col-xs-2"> \
                            <input type="text" class="form-control" name="Properties[' + propertyCounter + '].Expression"> \
                        </div> \
                        <div class="col-xs-2"> \
                            <button type="button" class="btn btn-danger" onclick="remove_property(this)">Remove</button> \
                        </div> \
                    </div>';

        $("#Properties").append(form);
        propertyCounter++;
    });

    $("#addForm").on('submit', function (event) {
        event.preventDefault();
        var form = $(this);
        if (form.valid()) {
            $.ajax({
                type: form.attr('method'),
                url: form.attr('action'),
                data: form.serialize(),
                success: function (result) {
                    if (result.Success) {
                        alert(result.Message);
                        window.location.href = '/AssetModel/Index'
                    } else if (result.Message != "") {
                        alert(result.Message);
                    }
                    else if (result.Errors != "") {
                        alert(result.Errors);
                    }
                    else {
                        alert('Unknown error');
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