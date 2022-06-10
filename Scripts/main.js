var Sortable = {
    baseUrl: '',
    sortBy: 0,
    searchTerm: '',
    Search() {
        var searchKey = $('#txtSearch').val();
        var enSearch = encodeURI(searchKey);
        window.location.href = Sortable.baseUrl + enSearch + "/";
    },
    Sort(sortBy) {

        var isDesc = true;
        var urlParams = new URLSearchParams(window.location.search);
        var oldDesc = urlParams.get('isDesc');
        var oldSort = urlParams.get('sortBy');

        if (oldSort == sortBy) {
            if (oldDesc == 'true') {
                isDesc = false;
            }
        }

        window.location.href = Sortable.baseUrl + "?sortBy=" + sortBy + "&isDesc=" + isDesc;
    }
}
$(document).ready(function () {
    $('input[type=datetime]').datepicker({
        dateFormat: "yy-mm-dd",
        changeMonth: true,
        changeYear: true,
        yearRange: "-60:+0"
    });

});