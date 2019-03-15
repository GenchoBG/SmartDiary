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
                console.log("Error");
            }
        });

        event.preventDefault();
    });

$(document).ready(function() {
    $.ajax({
        url: '/Chat/GetMessages',
        type: 'get',
        success: function (data) {
            console.log(data);
            for (let message of data) {
                $("#messages").append($(`<p>${message.content}</p>`));
            }
        },
        error: function () {
            console.log("Error");
        }
    });
})