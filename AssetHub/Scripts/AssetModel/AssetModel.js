$(function () {

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