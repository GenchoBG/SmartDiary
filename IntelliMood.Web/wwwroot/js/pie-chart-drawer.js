﻿google.charts.load('current', { 'packages': ['corechart'] });
//google.charts.setOnLoadCallback(drawMonthlyChart);

// Happy, Happy, Happy, Sad
function drawMonthlyChart(rawData) {
    var chart = new google.visualization.PieChart(document.getElementById('monthly-piechart'));
    console.log(rawData);

    var dict = {};
    for (var i = 0; i < rawData.length; i++) {
        var current = rawData[i];

        if (dict.hasOwnProperty(current)) {
            dict[current]++;
        } else {
            dict[current] = 1;
        }
    }

    var arr = [
        ['Mood', 'Days']
    ];

    for (var key in dict) {
        arr.push([key, dict[key]]);
    }

    console.log(arr);

	var data = google.visualization.arrayToDataTable(arr);
	
	var options = { 'title': 'Monthly', 'width': 550, 'height': 400 };

    chart.draw(data, options);
}

function drawYearlyChart(rawData) {
    var chart = new google.visualization.PieChart(document.getElementById('yearly-piechart'));
    console.log(rawData);

    var dict = {};
    for (var i = 0; i < rawData.length; i++) {
        var current = rawData[i];

        if (dict.hasOwnProperty(current)) {
            dict[current]++;
        } else {
            dict[current] = 1;
        }
    }

    var arr = [
        ['Mood', 'Days']
    ];

    for (var key in dict) {
        arr.push([key, dict[key]]);
    }

    console.log(arr);
    var data = google.visualization.arrayToDataTable(arr);

    var options = { 'title': 'Yearly', 'width': 550, 'height': 400 };

    chart.draw(data, options);
}
$(window).on("load", function () {

    let currentYear = new Date().getFullYear();
    let currentMonth = new Date().getMonth();

    $.ajax({
        url: `/Stats/GetMonthly?month=${currentMonth}&year=${currentYear}`,
        type: 'get',
        success: function (data) {
            drawMonthlyChart(data);
        },
        error: function () {
            console.log("Error");
        }
    });

    $.ajax({
        url: `/Stats/GetYearly?year=${currentYear}`,
        type: 'get',
        success: function (data) {
            drawYearlyChart(data);
        },
        error: function () {
            console.log("Error");
        }
    });
});