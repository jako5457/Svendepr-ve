// Initialize and add the map
var directionsServic;
var directionRenderer

function initMap() {
    directionsService = new google.maps.DirectionsService();
    directionRenderer = new google.maps.DirectionsRenderer();
    // The map, centered at Denmark
    var mapOptions = {
        zoom: 6,
        center: { lat: 55.676098, lng: 12.568337 },
        disableDefaultUI: true,
    };

    var map = new google.maps.Map(document.getElementById('map'), mapOptions);

    directionRenderer.setMap(map);
}

function callRoute(currentlat, currentlng, destinationlat, destinationlng) {
    var start = { lat: currentlat, lng: currentlng };
    var end = { lat: destinationlat, lng: destinationlng };

    var request = {
        origin: start,
        destination: end,
        travelMode: google.maps.TravelMode.DRIVING
    };

    directionsService.route(request).then((Response) => {
        directionRenderer.setDirections(Response);
    });
}