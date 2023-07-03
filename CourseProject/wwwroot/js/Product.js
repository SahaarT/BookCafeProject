$(document).ready(function () {
    debugger;
    LoadDataTable();
});

function LoadDataTable() {

    $('#myTable').DataTable({
        ajax: {
            url: '/Admin/Product/GetAll',
            dataSrc: 'data'
        },
        columns: [
            { data: 'Title' },
            { data: 'ISBN' },
            { data: 'Price' }
        ]
    });
}