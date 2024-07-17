


$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this);
        bootbox.confirm({
            message: 'Are you sure that you need to delete this user?',
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-secondary'
                }
            },
            callback: function (result) {
                if (result) {
                    console.log(btn.data('id'));
                    $.ajax({
                        url: `/api/users/?userId=${btn.data('id')}`,
                        method: 'DELETE',
                        success: function () {
                            //alert("User deleted");

                            toastr.success('Item has been deleted successfully');
                            $('#alert').fadeIn(2000);
                            setTimeout(function () {
                                $('#alert').fadeOut(1000);
                                btn.parent('td').parent('tr').fadeOut(1000);
                            }, 5000);

                        },
                        error: function () {

                            //alert('Error Somehing went wrong during deleting');

                            toastr.error('Error Somehing went wrong during deleting');
                        }
                    });
                } else {
                    setTimeout(function () {
                        toastr.info('Your imaginary data is safe :>>');
                    }, 1000)
                }

            }
        });

        //var result = confirm("Are you sure that you need to delete this user?");
        //if (result) {
        //    console.log(btn.data('id'));
        //}
    });
});

