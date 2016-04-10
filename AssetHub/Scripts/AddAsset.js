$(function () {
    $("#modelSelector").ready(function () {
        $("#modelSelector").val(-1);
    });

    $("#modelSelector").change(function () {
            var id = $('option:selected', this).val();
            $.getJSON('GetAssetModelProperties/' + id, function (data) {
                var editors = "<h4>Properties</h4> <hr/>";
                $.each(data, function (i, property) {
                    var editor = 
                        '<div class="form-group"> \
                             <label class="control-label col-md-2" for="Properties[' + i + '].ModelId">' + property.Name + '</label> \
                             <div class="col-md-10"> \
                                 <input type="hidden" name="Properties[' + i + '].ModelId" value=' + property.Id + '> \
                                 <input type="hidden" name="Properties[' + i + '].Name" value=' + property.Name + '> \
                                 <input class="form-control text-box single-line" data-val="true" data-val-required="The ' + property.Name + ' + field is required." id="Properties[' + i + '].Value" name="Properties[' + i + '].Value" type="text" value="" /> \
                                 <span class="field-validation-valid text-danger" data-valmsg-for="Properties[' + i + '].ModelId" data-valmsg-replace="true"></span> \
                             </div> \
                         </div>'
                    editors += editor;
                });
                $("#propertiesList").html(editors);
                $("#propertiesList").show();
            });
    });

    $("roomSelector").ready(function () {
        $("#roomSelector").val(-1);
    })

    $("#roomSelector").change(function () {
        alert($('option:selected', this).text());
    });
});