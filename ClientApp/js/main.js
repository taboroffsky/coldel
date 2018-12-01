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
            $(".modal-body" ).append( "<ul><li> Name: " + calEvent.title +"</li> <li>" + calEvent.title +"</li></ul>" );
            $('#exampleModalCenter').modal('show'); 
        
          }

    });


});