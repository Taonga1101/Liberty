document.getElementById('privilegeForm').onsubmit = function(e) {
    e.preventDefault();
    Swal.fire({
        title: 'Confirm submission',
        icon: 'question',
        text: 'Do you really want to submit this?',
        showCancelButton: true
    }).then(r => {
        if (r.isConfirmed) {
            let data = new FormData(document.getElementById('privilegeForm'));
            $.ajax(
                {
                    type: 'POST',
                    url: 'SavePrivileges',
                    data: data,
                    cache: false,
                    processData: false,
                    contentType: false,
                    success: response => {
                        if(response.success) {
                            Swal.fire({
                                title: 'Success',
                                icon: 'success',
                                text: 'The privilege has been saved 😊',
                                showCancelButton: false
                            }).then(r => {
                                location.href = "/User/Privilege";
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