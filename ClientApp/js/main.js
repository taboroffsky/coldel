$(document).ready(function () {

    var baseUri = "http://172.20.10.3:5000/api/";
    var registrationsUri = baseUri + "registrations";
    var roomsUri = baseUri + "rooms";

    var eventId;


    $.getJSON(registrationsUri, function (data) {
        for (let booking of data) {
            booking.allDay = true;
        }
        $('#calendar').fullCalendar('addEventSource', data);
    });

    $('#createNewRegistrant').on('click', function (event) {
        event.preventDefault(); // To prevent following the link (optional)

        var registrantName = $('#name').val();
        $('#name').val('')
        var registrantPhone = $('#phone').val();
        $('#phone').val('')

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
                registrationData.allDay = true;
                $('#calendar').fullCalendar('renderEvent', registrationData, true); // stick? = true
                $('#calendar').fullCalendar('unselect');
            },
            error: function (response) {
                alert("Unknown error occured");
                $('#calendar').fullCalendar('unselect');
            }
        });

    });

    $('#deleteBtn').on('click', function (event) {
        event.preventDefault(); // To prevent following the link (optional)

        var recordId = $("#recordId").val();
        $.ajax({
            url: registrationsUri,
            type: 'delete',
            data: JSON.stringify({
                id: recordId
            }),
            headers: {
                "Content-Type": "application/json"
            },
            success: function (respnse) {
                $('#calendar').fullCalendar('removeEvents', [recordId]);
            },
            error: function (response) { }
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
            $(".modal-body").append("<li> Room price: " + calEvent.room.price + " UAH</li>");
            $(".modal-body").append("<li> Start date: " + calEvent.start.format() + "</li>");
            $(".modal-body").append("<li> End date: " + calEvent.end.format() + "</li>");
            $(".modal-body").append("</ul>");
            $(".modal-body").append("<div class='text-right'>Total price: " + (diffDays * calEvent.room.price) + " UAH</div>");
            $('#exampleModalCenter').modal('show');

            $("#recordId").val(calEvent.id);

            $('#editBtn').on('click', function (event) {
                event.preventDefault(); // To prevent following the link (optional)
                $('#nameEdit').val('')
                $('#nameEdit').attr('value', calEvent.client.name)
                $('#phoneEdit').val('')
                $('#phoneEdit').attr('value', calEvent.client.phone)
                $('#startEdit').attr('value', calEvent.start.format())
                $('#endEdit').attr('value', calEvent.end.format())
                $('#sel1Edit').attr('selected', calEvent.room.roomType)
                $('#modalChange').modal('show');
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

                        var select = document.getElementById("sel1Edit");
                        select.innerHTML = '';

                        for (let room of rooms) {
                            var option = document.createElement("option");
                            option.text = "Beds: " + room.capacity + " \\ " + "Type: " + room.roomType + " \\ " + "Price: " + room.price;
                            option.value = room.id;
                            select.appendChild(option);
                        }
                    }
                });

                $('#backBtn').on('click', function (event) {
                    $('#exampleModalCenter').modal('show');
                });

                $('#confirmChanges').on('click', function (event) {
                    {
                        event.preventDefault(); // To prevent following the link (optional)

                        var recordId = $("#recordId").val();
                        debugger;
                        var data = {};
                        data.clientName = $('#nameEdit').val();
                        data.phone = $('#phoneEdit').val();
                        data.roomId = $('#sel1Edit').val();
                        data.registrationId = calEvent.id;
                        data.start = $('#startEdit').val();
                        data.end = $('#endEdit').val();
                        var json = JSON.stringify(data);


                        $.ajax({
                            url: registrationsUri,
                            type: 'put',
                            data: json,
                            headers: {
                                "Content-Type": "application/json"
                            },
                            success: function (registrationData) {
                                debugger;
                                registrationData.allDay = true;
                                $('#calendar').fullCalendar('removeEvents', [recordId] );
                                $('#calendar').fullCalendar('renderEvent', registrationData, true);
                            },
                            error: function (response) {
                                alert("Unknown error occured");
                                $('#calendar').fullCalendar('unselect');
                            }
                        });















                    };
                });
            });

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