function approveApplication(id) {
    console.log("approveApplication", id)

    Swal.fire({
        title: 'Are you sure',
        icon: 'question',
        text: 'Do you really want to approve this?',
        showCancelButton: true
    }).then(r => {
        if (r.isConfirmed) {
            let data = new FormData();
            data.append("id", id);
            $.ajax(
                {
                    type: 'POST',
                    url: 'ApproveApplication',
                    data: data,
                    cache: false,
                    processData: false,
                    contentType: false,
                    success: response => {
                        if(response.success) {
                            Swal.fire({
                                title: 'Success',
                                icon: 'success',
                                text: 'Aproved successfully',
                                showCancelButton: false
                            }).then(r => {
                                location.href = "/LeaveApplication/SupervisorLeaveApplication";
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
function rejectApplication(id) {
    console.log("rejectApplication", id)
    Swal.fire({
        title: 'Are you sure',
        icon: 'question',
        text: 'Do you really want to reject this?',
        showCancelButton: true
    }).then(r => {
        if (r.isConfirmed) {
            let data = new FormData();
            data.append("id", id)
            $.ajax(
                {
                    type: 'POST',
                    url: 'RejectApplication',
                    data: data,
                    cache: false,
                    processData: false,
                    contentType: false,
                    success: response => {
                        if(response.success) {
                            Swal.fire({
                                title: 'Success',
                                icon: 'success',
                                text: 'Rejected successfully',
                                showCancelButton: false
                            }).then(r => {
                                location.href = "/LeaveApplication/SupervisorLeaveApplication";
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