$(function () {
    //$("#categorySelector").ready(function () {
    //    $("#categorySelector").val(selectedCategoryId);
    //});

    $("#addPropertyBtn").click(function () {
        $.getJSON('GetProperties', function (result) {
            var dropdownHtml =
                '<select class="form-control" \
                    data-val="true" \
                    data-val-number="The field Property must be a number." \
                    data-val-required="The Property field is required." \
                    id="propertySelector" \
                    name="SelectedPropertyId">';
            $.each(result.Properties, function (i, property) {
                var optionHtml = '<option value="' + property.Id + '">' + property.Name + '</option>';
                dropdownHtml += optionHtml;
            });
            dropdownHtml += '</select>';
            append(dropdownHtml);
        });
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
                    else {
                        alert(result.Errors);
                    }
                }
            });
        }
    });
});

function append(dropdownHtml) {
    var tableRowHtml =
        '<tr> \
            <td>' + dropdownHtml + '</td> \
            <td><input type="button" value="Remove" onclick="remove_row(this)" class="btn btn-danger" /></td> \
         </tr>';
    $("#propertyTable tbody").append(tableRowHtml);
}

function remove_row(button) {
    var row = button.parentNode.parentNode;
    var body = row.parentNode;
    body.removeChild(row);
}