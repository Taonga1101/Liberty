document.getElementById('departmentForm').onsubmit = function(e) {
    e.preventDefault();
    Swal.fire({
        title: 'Confirm submission',
        icon: 'question',
        text: 'Do you really want to submit this?',
        showCancelButton: true
    }).then(r => {
        if (r.isConfirmed) {
            let data = new FormData(document.getElementById('departmentForm'));
            $.ajax(
                {
                    type: 'POST',
                    url: 'SaveDepartment',
                    data: data,
                    cache: false,
                    processData: false,
                    contentType: false,
                    success: response => {
                        if(response.success) {
                            Swal.fire({
                                title: 'Success',
                                icon: 'success',
                                text: 'The department has been saved 😊',
                                showCancelButton: false
                            }).then(r => {
                                location.href = "/Config/Departments";
                            })
                        }
                        else {
                            Swal.fire({
                                title: 'Save failed',
                                icon: 'error',
                                text: 'An error occured while processing your request',
                                showCancelButton: true
                            }).then(r => {})
                        }
                    },
                    error: err => {
                        console.log(err)
                    }
                }
            )
        }
    })
}