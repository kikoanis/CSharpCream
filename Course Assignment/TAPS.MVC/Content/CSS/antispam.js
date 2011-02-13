var antiSpam = function() {
	if (document.getElementById("antiSpam")) {
		a = document.getElementById("antiSpam");
		if (isNaN(a.value) == true) {
			a.value = 0;
		} else {
			a.value = parseInt(a.value) + 1;
		}
	}
	setTimeout("antiSpam()", 1000);
}

antiSpam();