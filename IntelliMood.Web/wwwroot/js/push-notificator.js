document.addEventListener('DOMContentLoaded', function () {
	if (Notification.permission !== "granted") {
		Notification.requestPermission();
	}

	setTimeout(function() {
		notify('IntelliMood', 'Hi there, do you want to tell me about your day?', 'http://localhost:49735/')
	}, 5000);


	function notify(title, desc, url) {

		if (Notification.permission !== "granted") {
			Notification.requestPermission();
		}
		else {
			var notification = new Notification(title, {
				icon: 'http://Your_Website.com/logo.png',
				body: desc,
			});

			/* Remove the notification from Notification Center when clicked.*/
			notification.onclick = function () {
				window.open(url);
			};

			/* Callback function when the notification is closed. */
			notification.onclose = function () {
				console.log('Notification closed');
			};

		}
	}  
});  