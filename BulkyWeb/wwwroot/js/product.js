﻿let table = new DataTable('#tblData', {
    ajax: "/admin/product/getall",
    columns: [
        { data: 'title', width: "25%" },
        { data: 'isbn', width: "15%" },
        { data: 'price', width: "10%" },
        { data: 'author', width: "15%" },
        { data: 'category.name', width: "10%" },
        {
            data: 'id',
            render: function (data) {
                return `<div class="w-75 btn-group" role="group">
                    <a href="/admin/product/edit?id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
                    <a href="" class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i> Edit</a>
                </div>
                `
            },
            width: "25%"
        },
    ]
});