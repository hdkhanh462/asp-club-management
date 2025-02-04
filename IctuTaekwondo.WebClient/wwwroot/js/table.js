$(document).ready(function () {
    function updateSortIcons(col, order) {
        var iconSort = $(col).find("svg[icon-sort]")
        var iconAsc = $(col).find("svg[icon-asc]")
        var iconDesc = $(col).find("svg[icon-desc]")

        if (order === 'ASC') {
            iconSort.hide();
            iconAsc.show();
            iconDesc.hide();
        } else if (order === 'DESC') {
            iconSort.hide();
            iconAsc.hide();
            iconDesc.show();
        } else {
            iconSort.show();
            iconAsc.hide();
            iconDesc.hide();
        }
    }

    $('th[sortable]').click(function () {
        var input = $(this).find('input[name="order"]');
        var order = input.val();

        updateSortIcons(this, order);

        $(this).find("form").submit();
    });

    $('th[sortable]').each(function () {
        var input = $(this).find('input[name="order"]');
        var order = input.val();

        updateSortIcons(this, order);
    });
});