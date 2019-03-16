function appendMessage(message) {
	if (message.isResponse) {
		$("#messages").append(
            $(`<div class="message d-block"><img src="../images/robot.png" class="imgBot"> <p class="messageContent primaryColor secondaryColor"> ${message.content.trim()
				}</p><div class="timestamp">${message.time}</div></div>`));
	} else
	{
        $("#messages").append($(`<div class="message person d-block"><p class="messageContent primaryColor secondaryColor">${message.content.trim()}</p><div class="timestamp">${message.time}</div></div>`));
	}
	
}

function clearMessages() {
    $("#messages").html("");
}

function scrollToBottom() {
    var heightMessages = $('#messages').prop('scrollHeight') * 2;
    $("#messages").animate({ scrollTop: heightMessages }, 1000);
}

function clearChatBox() {
    $("#chatBox").val('');
    console.log($("#chatBox").val());
}

function DisplayCurrentTime(date) {
    console.log(date);

    var hours = date.getHours();
    var ampm = "am";
    if (hours > 12) {
        hours -= 12;
        ampm = "pm";
    }
    var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();

    return `${hours}:${minutes} ${ampm}`;
};

$("#enterBtn").on("click",
    function (event) {
        if ($("#chatBox").val()) {
            let val = $("#chatBox").val();
            clearChatBox(val);

            appendMessage({
                content: val,
                isResponse: false,
                time: DisplayCurrentTime(new Date(Date.now()))
            });
            console.log(val);
            scrollToBottom();

            $.ajax({
                url: '/Chat/CreateMessage',
                type: 'post',
                dataType: 'json',
                data: {
                    'message': val
                },
                success: function (messageData) {
                    var message = messageData.myMessage;
                    var response = messageData.response;
                    console.log("SUCCESS");


                    appendMessage(response);

                    scrollToBottom();
                },
                error: function () {
                    console.log("Error");
                }
            });
            event.preventDefault();
        }
    });

$("#chatBox").on("keypress", function (event) {
    if (event.which === 13) {
        if (!event.shiftKey)
        {
            $("#enterBtn").click();
        }
        event.preventDefault();
    }
    
});

function FocusTextarea() {
    $("#chatBox").focus();
}

$(document).ready(function () {
    FocusTextarea();
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