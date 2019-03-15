function appendMessage(message) {
    $("#messages").append($(`<div class="message"><p class="messageContent">${message.content.trim()}</p><div class="timestamp">${message.time}</div></div>`)
            .addClass(message.isResponse ? "d-block" : "person d-block"));
}

function clearMessages()
{
    $("#messages").html("");
}

function scrollToBottom() {
    $("#messages").animate({ scrollTop: $('#messages').prop('scrollHeight') }, 500);
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
            success: function (messageData) {
                var message = messageData.myMessage;
                var response = messageData.response;
                console.log("SUCCESS");
                clearChatBox();
                appendMessage(message);
                appendMessage(response);

                scrollToBottom();
            },
            error: function() {
                console.log("Error");
            }
        });

        event.preventDefault();
    });

$("#chatBox").on("keypress", function(event) 
{
    if (event.which === 13) {
        $("#enterBtn").click();
    }
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
            scrollToBottom();
        },
        error: function () {
            console.log("Error");
        }
    });
});