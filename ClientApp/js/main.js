$(document).ready(function () {
    var url = "http://192.168.31.184:5000/api/registrations"
    $.getJSON(url, function(data){
        $('#calendar').fullCalendar('addEventSource', data)    
    });

    function createNewRegistrant () {
        // body... 
        debugger;
    }

    $('#createNewRegistrant').on('click', function(event) {
      event.preventDefault(); // To prevent following the link (optional)
      debugger;

    });

    $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,listWeek'
        },
        defaultDate: new Date().toDateString(),
        navLinks: true, // can click day/week names to navigate views

        weekNumbers: true,
        weekNumbersWithinDays: true,
        weekNumberCalculation: 'ISO',
        selectable: true,
        selectHelper: true,

        select: function (start, end) {
            
            $('#ModalEdit').modal('show'); 
            /*var eventData;
            if (title) {
                eventData = {
                    title: title,
                    start: start,
                    end: end
                };
                $('#calendar').fullCalendar('renderEvent', eventData, true); // stick? = true
            }*/
            $('#calendar').fullCalendar('unselect');
        },

        editable: true,
        eventLimit: true, // allow "more" link when too many events
        
         eventRender: function(event, element) {
         },


        eventClick: function(calEvent, jsEvent, view) {
            $('.modal-body').empty();
            $(".modal-body" ).append("<ul>");
            $(".modal-body" ).append( "<li> Name: " + calEvent.title +"</li>" );
            $(".modal-body" ).append("<li> Phone:" + calEvent.client.phone +"</li>");
            $(".modal-body" ).append("<li> Room type:" + calEvent.room.roomType +"</li>");
            $(".modal-body" ).append("<li> Room capacity:" + calEvent.room.capacity +"</li>");
            $(".modal-body" ).append("<li> Room price:" + calEvent.room.price +"</li>");
            $(".modal-body" ).append("<li> Start date:" + calEvent.start._d +"</li>");
            $(".modal-body" ).append("<li> End date:" + calEvent.end._d +"</li>");
            $(".modal-body" ).append("</ul>");
            $('#exampleModalCenter').modal('show'); 
        
          },

        eventDrop: function(event, delta, revertFunc) {

            alert(event.title + " was dropped on " + event.start.format());
        
            if (!confirm("Are you sure about this change?")) {
              revertFunc();
            }
            var data = {};
            data.clientName = event.client.name;
            data.phone = event.client.phone;
            data.roomId = event.room.id,
            data.registrationId = event.id,
            data.start = event.start._d,
            data.end = event.end._d
            var json = JSON.stringify(data);

            var xhr = new XMLHttpRequest();
            xhr.open("PUT", url, true);
            xhr.setRequestHeader('Content-type','application/json; charset=utf-8');
            xhr.send(json);
          }

    });


});