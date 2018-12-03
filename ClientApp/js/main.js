$(document).ready(function () {

    var baseUri = "http://192.168.31.184:5000/api/";
    var registrationsUri = baseUri + "registrations";
    var roomsUri = baseUri + "rooms";


    $.getJSON(registrationsUri, function (data) {
        for (let booking of data) {
            booking.allDay = true;
        }
        $('#calendar').fullCalendar('addEventSource', data);
    });

    $('#createNewRegistrant').on('click', function (event) {
        event.preventDefault(); // To prevent following the link (optional)

        var registrantName = $('#name').val();
        var registrantPhone = $('#phone').val();

        var room = $('#sel1').val();

        var start = $('#start').val();
        var end = $('#end').val();

        $.ajax({
            url: registrationsUri,
            type: 'post',
            data: JSON.stringify({
                start: start,
                end: end,
                clientName: registrantName,
                phone: registrantPhone,
                roomId: room
            }),
            headers: {
                "Content-Type": "application/json"
            },
            success: function (registrationData) {
                $('#calendar').fullCalendar('renderEvent', registrationData, true); // stick? = true
                $('#calendar').fullCalendar('unselect');
            },
            error: function (response) {
                alert("Unknown error occured");
                $('#calendar').fullCalendar('unselect');
            }
        });

    });

    $('#calendar').fullCalendar({
        themeSystem: 'bootstrap4',
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,listWeek'
        },
        defaultDate: new Date().toDateString(),
        navLinks: true, // can click day/week names to navigate views

        height: 910,
        weekNumbersWithinDays: true,
        weekNumberCalculation: 'ISO',
        selectable: true,
        selectHelper: true,

        select: function (start, end) {
            $('#start').val(moment(start._d).format('YYYY-MM-DD'));
            $('#end').val(moment(end._d).format('YYYY-MM-DD'));

            $.ajax({
                url: roomsUri,
                type: 'post',
                data: JSON.stringify({
                    start: start._d,
                    end: end._d
                }),
                headers: {
                    "Content-Type": "application/json"
                },
                success: function (rooms) {
                    $('#ModalEdit').modal('show');

                    var select = document.getElementById("sel1");
                    select.innerHTML = '';

                    for (let room of rooms) {
                        var option = document.createElement("option");
                        option.text = "Beds: " + room.capacity + " \\ " + "Type: " + room.roomType + " \\ " + "Price: " + room.price;
                        option.value = room.id;
                        select.appendChild(option);
                    }
                },
                error: function (response) {
                    var data = [
                        {
                            "id": "0f329ff6-e7d3-4686-9538-2dffe68adcbd",
                            "roomType": "Standart",
                            "price": 800,
                            "capacity": 4
                        },
                        {
                            "id": "cd99427a-2e7d-466a-8edd-72faffe355f4",
                            "roomType": "Lux",
                            "price": 900,
                            "capacity": 2
                        },
                        {
                            "id": "e50b72be-d2d6-473d-8aa4-c89951b1f47e",
                            "roomType": "Standart",
                            "price": 500,
                            "capacity": 2
                        }
                    ];
                }
            });
        },

        editable: true,
        eventLimit: true, // allow "more" link when too many events

        eventClick: function (calEvent, jsEvent, view) {
            var timeDiff = Math.abs(new Date(calEvent.end).getTime() - new Date(calEvent.start).getTime());
            var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
            $('.modal-body').empty();
            $(".modal-body").append("<ul>");
            $(".modal-body").append("<li> Name: " + calEvent.title + "</li>");
            $(".modal-body").append("<li> Phone: " + calEvent.client.phone + "</li>");
            $(".modal-body").append("<li> Room type: " + calEvent.room.roomType + "</li>");
            $(".modal-body").append("<li> Beds: " + calEvent.room.capacity + "</li>");
            $(".modal-body").append("<li> Room price: " + calEvent.room.price + "</li>");
            $(".modal-body").append("<li> Start date: " + calEvent.start.format() + "</li>");
            $(".modal-body").append("<li> End date: " + calEvent.end.format() + "</li>");
            $(".modal-body").append("</ul>");
            $(".modal-body").append("<div class='text-right'>Total price: " + ( diffDays * calEvent.room.price) + "</div>");
            $('#exampleModalCenter').modal('show');

        },

        eventDrop: function (event, delta, revertFunc) {

            $('#dragModal').modal('show');
            $("#discardChanges").click(function () {
                revertFunc();
            });
            var data = {};
            data.clientName = event.client.name;
            data.phone = event.client.phone;
            data.roomId = event.room.id;
            data.registrationId = event.id;
            data.start = event.start._d;
            data.end = event.end._d;
            var json = JSON.stringify(data);

            var xhr = new XMLHttpRequest();
            xhr.open("PUT", registrationsUri, true);
            xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
            xhr.send(json);
        }

    });


});