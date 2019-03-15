﻿$("#btn-back").click(function () {
    month -= 1;
    if (month == -1) {
        month = 11;
        year--;
    }
    $("#month-name").fadeOut("slow", function () {
        $(this).html(monthNames[month]).fadeIn("slow");
    });
    days = daysInMonth(month, year);
    daysDiv.innerHTML = "";
    displayDays();
});

$("#btn-forward").click(function () {
    month += 1;
    if (month == 12) {
        month = 0;
        year++;
    }
    $("#month-name").fadeOut("slow", function () {
        $(this).html(monthNames[month]).fadeIn("slow");
    });    
    days = daysInMonth(month, year);
    daysDiv.innerHTML = "";
    displayDays();
});