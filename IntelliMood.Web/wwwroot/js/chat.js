$("#enterBtn").on("click",
    function(event) {

        console.log($("#chatBox").val());

        $.ajax({
            url: '/Chat/CreateMessage',
            type: 'post',
            dataType: 'json',
            data: {
                'message': $("#chatBox").val()
            },
            success: function (response) {
                console.log("SUCCESS");
                $("#chatBox").val("");
                
            },
            error: function() {
                //TODO: Error popup
            }
        });

        event.preventDefault();
    });