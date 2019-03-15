var today = new Date();
var month = today.getMonth();
var year = today.getFullYear();
const monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

document.getElementById("month-name").innerText = monthNames[month];

var days = daysInMonth(month, year);
console.log(days);
var daysDiv = document.getElementById("calendar-days");


function daysInMonth(month, year) {
    return new Date(year, month + 1, 0).getDate();
}

function nextMonth() {
    month += 1;
    if (month == 12) {
        month = 0;
        year++;
    }
    days = daysInMonth(month, year);
    daysDiv.innerHTML = "";
    displayDays();
    document.getElementById("month-name").innerText = monthNames[month];
}

function prevMonth() {
    month -= 1;
    if (month == -1) {
        month = 11;
        year--;
    }
    days = daysInMonth(month, year);
    daysDiv.innerHTML = "";
    displayDays();
    document.getElementById("month-name").innerText = monthNames[month];
    console.log(`Year: ${year}; Month: ${month}`);
}

function displayDays() {
    for (let day = 1; day <= days; day++) {
        daysDiv.innerHTML += `<button type="button" class="btn btn-outline-primary circular-btn" data-toggle="modal" data-target=".bd-example-modal-lg${day}"><div class="day">${day}</div></button>

                                <div class="modal fade bd-example-modal-lg${day}" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                                    <div class="modal-dialog" role="document">
                                        <div class="modal-content">
                                          <div class="modal-header">
                                            <h5 class="modal-title" id="Modal${day}">${day}</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                              <span aria-hidden="true">&times;</span>
                                            </button>
                                          </div>
                                          <div class="modal-body">
                                            ...
                                          </div>
                                          <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                            <button type="button" class="btn btn-primary">Save changes</button>
                                          </div>
                                        </div>
                                    </div>
                                </div>`;
        if (day % 7 == 0) {
            daysDiv.innerHTML += `<br /><br />`
        }
    }
}

displayDays();