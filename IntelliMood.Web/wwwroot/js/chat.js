function appendMessage(message) {
    $("#messages").append($(`<p>${message.content}</p>`));
}

function clearMessages()
{
    $("#messages").html("");
}

function clearChatBox() {
    $("#chatBox").val("");
}

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
            success: function (message) {
                console.log("SUCCESS");
                clearChatBox();
                appendMessage(message);
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
            clearMessages();
            for (let message of data) {
                appendMessage(message);
            }
        },
        error: function () {
            console.log("Error");
        }
    });
})