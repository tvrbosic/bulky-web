

let dataTable = new DataTable('#tblData', {
    ajax: "/admin/company/getall",
    columns: [
        { data: 'name', width: "13%" },
        { data: 'streetAddress', width: "17%" },
        { data: 'city', width: "11%" },
        { data: 'state', width: "11%" },
        { data: 'postalCode', width: "9%" },
        { data: 'phoneNumber', width: "11%" },
        {
            data: 'id',
            render: function (data) {
                return `<div class="w-75 btn-group" role="group">
                    <a href="/admin/company/edit?id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
                    <a onClick=Delete('/admin/company/delete?id=${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i> Delete</a>
                </div>
                `
            },
            width: "23%"
        },
    ]
});

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url,
                type: "DELETE",
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}