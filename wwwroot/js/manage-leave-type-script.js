document.getElementById('leaveTypeForm').onsubmit = e => {
    e.preventDefault();
    Swal.fire({
        title: 'Confirm submission',
        icon: 'question',
        text: 'Do you really want to submit this?',
        showCancelButton: true
    }).then(r => {
        if (r.isConfirmed) {
            $.ajax(
                {
                    type: 'POST',
                    url: 'SaveLeaveType',
                    data: new FormData(document.getElementById('leaveTypeForm')),
                    cache: false,
                    success: response => {
                        console.log(response)
                    },
                    error: err => {
                        console.log(err)
                    }
                }
            )
        }
    })
}