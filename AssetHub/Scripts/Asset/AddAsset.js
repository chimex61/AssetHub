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
                    '<div class="form-group"> \
                             <label class="control-label col-md-2" for="Properties[' + i + '].ModelId">' + property.Name + '</label> \
                             <div class="col-md-10"> \
                                 <input type="hidden" name="Properties[' + i + '].ModelId" value="' + property.Id + '"> \
                                 <input type="hidden" name="Properties[' + i + '].Name" value="' + property.Name + '"> \
                                 <input class="form-control text-box single-line" data-val="true" data-val-required="The ' + property.Name + ' + field is required." id="Properties[' + i + '].Value" name="Properties[' + i + '].Value" type="text" value="" /> \
                                 <span class="field-validation-valid text-danger" data-valmsg-for="Properties[' + i + '].ModelId" data-valmsg-replace="true"></span> \
                             </div> \
                         </div>'
                editors += editor;
            });
            table += editors;
            table +=
                                '</tbody> \
                        </table> \
                        <hr /> \
                </div>';
            $("#propertiesList").html(editors);
            $("#propertiesList").show();
        });
    });
});