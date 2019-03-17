$(window).on("load", function() {
    $.get({
        url: '/Recommendation/GetUnpopulatedArray',
        success: function (data) {

            console.log(data);

            for (var row = 0; row < data.length; row++) {
                for (var col = 0; col < data[0].length; col++) {
                    $(`#tableContent-${row}-${col}`).text(data[row][col] == 0 ? "???" : data[row][col].toFixed(2));
                }
            }
        },
        error: function(err) {
            console.log(err);
            console.log("Error");
        }
    });

    $.get({
        url: '/Recommendation/GetPopulatedArray',
        success: function (data) {

            console.log(data);

            for (var row = 0; row < data.length; row++) {
                for (var col = 0; col < data[0].length; col++) {
                    $(`#tablePredictionContent-${row}-${col}`).text(data[row][col].toFixed(2));
                }
            }
        },
        error: function (err) {
            console.log(err);
            console.log("Error");
        }
    });
});