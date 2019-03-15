google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart);

// Happy, Happy, Happy, Sad
function drawChart(rawData) {
	var data = google.visualization.arrayToDataTable([
		['Mood', 'Days'],
		['Happy', 8],
		['Sad', 2],
		['Okay', 4],
		['Angry', 2],
		['Sleep', 8]
	]);

	
	var options = { 'title': 'Monthly', 'width': 550, 'height': 400 };

	
	var chart = new google.visualization.PieChart(document.getElementById('monthly-piechart'));
	chart.draw(data, options);


	data = google.visualization.arrayToDataTable([
		['Mood', 'Days'],
		['Happy', 122],
		['Sad', 44],
		['Okay', 33],
		['Angry', 22],
		['Sleep', 11]
	]);

	options = { 'title': 'Yearly', 'width': 550, 'height': 400 };

	var chart = new google.visualization.PieChart(document.getElementById('yearly-piechart'));
	chart.draw(data, options);
}

$(document).ready(function() {

});