$(document).ready(function () {
    var $propertyList = $('#propertyList');
    $('#btnAddProperty').click(function (e) {
        e.preventDefault();
        $('<input class="form-control text-box single-line" type="text" name="property" /><br/>').appendTo($propertyList);
    });
});