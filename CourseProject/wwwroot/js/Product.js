$(document).ready(function () {
    debugger;
    LoadDataTable();
});

function LoadDataTable() {

    $('#myTable').DataTable({
        ajax: {
            url: '/Admin/Product/GetAll',
            dataSrc: 'data'
        }
        ,
        columns: [ 
            { data: 'name' },
            { data: 'description' },
            { data: 'isbn' },
            { data: 'author' },
            { data: 'category.name' },
            { data: 'coverType.name' },
            { data: 'listPrice' },
            { data: 'price' },
            { data: 'price50' },
            { data: 'price100' },
            {
                data: 'id',
                render: function (data)
                {
                    return `
                    <div class="btn bg-secondary">
                            <a href="/Admin/Product/Upsert?id=${data}" class="text-light"> <i class="bi bi-pencil"></i> &ensp; Edit</a>
                    </div>    
                    <div class="btn bg-secondary">
                            <a class="text-light"><i class="bi bi-trash"></i> &ensp; Delete</a>
                    </div>
                    `
                }
            }
        ]
    });
}