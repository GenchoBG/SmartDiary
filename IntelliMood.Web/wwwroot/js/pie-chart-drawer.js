google.charts.load('current', { 'packages': ['corechart'] });
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

function drawCharts() {
	$.ajax({
		url: '/Stats/GetMonthly?month=3&year=2019',
		type: 'get',
		success: function (data) {
			try {
				drawMonthlyChart(data);
			} catch (e) {
				location.reload();
			} 
		},
		error: function () {
			console.log("Error");
		}
	});

	$.ajax({
		url: '/Stats/GetYearly?year=2019',
		type: 'get',
		success: function (data) {
			try {
				drawYearlyChart(data);
			} catch (e) {
				location.reload();
			} 
		},
		error: function () {
			console.log("Error");
		}
	});
}

$(document).ready(function() {
	drawCharts();
});