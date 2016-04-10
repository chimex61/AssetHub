$('#modelSelector').prop('selectedIndex', -1)

function loadProperties() {
    var id = document.getElementById("modelSelector").value
    $.getJSON('GetAssetModelProperties/' + id, function (data) {
        var editors = "<h4>Properties</h4>";
        $.each(data, function (i, property) {
            var editor =
                "<div class=\"form-group\"> \
                    <label class=\"control-label col-md-2\" for=" + property + ">" + property + "</label> \
                    <div class=\"col-md-10\" id=property>\
                        <input class=\"form-control text-box single-line\" data-val=\"true\" data-val-required=\"The " + property + " field is required.\" name=PropertyValue type=\"text\" value=\"\" /> \
                        <span class=\"field-validation-valid text-danger\" data-valmsg-for=property data-valmsg-replace=\"true\"></span> \
                    </div> \
                 </div>"

            editors += editor;
        });
        var properitesEditors = document.getElementById('propertiesEditors');
        properitesEditors.innerHTML = editors
    });
}