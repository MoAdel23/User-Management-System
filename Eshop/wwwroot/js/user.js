
//var dtble;

////document.addEventListener("DOMContentLoaded", function () {
////});

//$(document).ready(function () {

//    loadData();
//});



//function loadData() {
//    dtble = $("#products").DataTable(
//        {
//            ajax: {
//                "url": '/Admin/Product/GetData',
//                /*dataSrc:'data'*/
//            },
//            columns: [
//                { data: 'title' },
//                { data: 'category.name' },
//                //{ data:'description'},
//                { data: 'price' },
//                {
//                    data: 'image',
//                    render: function (data) {
//                        return `
//                        <img src="/${data}" style="width:50px; height:40px"/>
//                    `
//                    }
//                },
//                {
//                    data: 'id',
//                    render: function (data) {
//                        return `
//                                    <a class="btn btn-primary btn-sm" href="#">
//                                        <i class="fas fa-folder">
//                                        </i>
//                                        View
//                                    </a>
//                                    <a class="btn btn-danger btn-sm" onclick="DeleteItem(${data})">
//                                        <i class="fas fa-trash">
//                                        </i>
//                                        Delete
//                                    </a>
//                                    <a class="btn btn-info btn-sm" href="/Admin/Product/Edit/${data}">
//                                        <i class="fas fa-pencil-alt">
//                                        </i>
//                                        Edit
//                                    </a>
                               
//                               `
//                    }
//                }
//            ]
//        });
//}


function DeleteItem(id) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/Users/Delete/${id}`,
                type: "Delete",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        //setTimeout(function () {
                        //    dtble.ajax.reload();
                        //}, 3000);
                    } else {

                        toastr.error(data.message);
                    }
                }
            });
            swalWithBootstrapButtons.fire({
                title: "Deleted!",
                text: "Your item has been deleted.",
                icon: "success"
            });
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire({
                title: "Cancelled",
                text: "Your imaginary file is safe :>>",
                icon: "error"
            });
        }
    });
} 