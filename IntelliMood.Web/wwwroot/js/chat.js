function appendMessage(message) {
if (message.isResponse) {
		$("#messages").append(
            $(`<div class="message d-block"><img src="../images/robot.png" class="imgBot"> <p class="messageContent primaryColor secondaryColor"> ${message.content.trim()
				}</p><div class="timestamp">${message.time}</div></div>`));
	} else
	{
        $("#messages").append($(`<div class="message person d-block"><p class="messageContent primaryColor secondaryColor">${message.content.trim()}</p><div class="timestamp">${message.time.trim()}</div></div>`));
	}}

function appendRecommendation(time, element) {
    let div = $(
        `<div class="message d-block"></div>`);

    div.append($(`<img src="../images/robot.png" class="imgBot">`));
    div.append(element);
    div.append($(`<div class="timestamp">${time}</div>`));

    $("#messages").append(div);
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


function addRating(recommendationId, rating) {
    $.ajax({
        url: '/Chat/AddRating',
        type: 'post',
        dataType: 'json',
        data: {
            'recommendationId': recommendationId,
            rating: rating
        },
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
}

function addRecommendationWithRating(button, rating) {
    let recommendation = $($(button).parent().find('input')[0]).val();
    $.ajax({
        url: '/Chat/AddRecommendationWithRating',
        type: 'post',
        dataType: 'json',
        data: {
            'recommendation': recommendation,
            rating: rating
        },
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
}

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
                success: function (data) {
                    var message = data.myMessage;
                    var response = data.response;
                    console.log(data);

                    appendMessage(response);
                    if (data.hasRecommendation) {
                        var element = $("<div>")
                            .append($(`<div>Rate my recommendation!</div>`))
                            .append($(`<button onclick="addRating(${data.recommendationId}, 1)">1</button>`))
                            .append($(`<button onclick="addRating(${data.recommendationId}, 2)">2</button>`))
                            .append($(`<button onclick="addRating(${data.recommendationId}, 3)">3</button>`))
                            .append($(`<button onclick="addRating(${data.recommendationId}, 4)">4</button>`))
                            .append($(`<button onclick="addRating(${data.recommendationId}, 5)">5</button>`))
                            .append($(`<div>You did something else? Rate how good it made you feel!</div>`))
                            .append($(`<input type='text'></input>`))
                            .append($(`<button onclick="addRecommendationWithRating(this, 1)">1</button>`))
                            .append($(`<button onclick="addRecommendationWithRating(this, 2)">2</button>`))
                            .append($(`<button onclick="addRecommendationWithRating(this, 3)">3</button>`))
                            .append($(`<button onclick="addRecommendationWithRating(this, 4)">4</button>`))
                            .append($(`<button onclick="addRecommendationWithRating(this, 5)">5</button>`));

                        appendRecommendation(response.time, element);
                    }

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
        if (!event.shiftKey) {
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