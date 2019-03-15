
console.log(`Year: ${year}; Month: ${month}`);

$("#btn-back").click(function () {
    $("#month-name").fadeOut("slow", function () {
        $(this).html(monthNames[month - 1 == -1 ? 11 : month - 1]).fadeIn("slow");
    });
    month -= 1;
    if (month == -1) {
        month = 11;
        year--;
    }
    days = daysInMonth(month, year);
    daysDiv.innerHTML = "";
    displayDays();
});

$("#btn-forward").click(function () {
    $("#month-name").fadeOut("slow", function () {
        $(this).html(monthNames[month + 1 == 12 ? 0 : month + 1]).fadeIn("slow");
    });
    console.log(`Year: ${year}; Month: ${month}`);
    if (month == 12) {
        month = 0;
        year++;
    }
    days = daysInMonth(month, year);
    daysDiv.innerHTML = "";
    displayDays();
});