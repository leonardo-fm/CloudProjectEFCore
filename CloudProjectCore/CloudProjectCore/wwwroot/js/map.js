var map;

function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: {
            lat: 45.081780,
            lng: 7.660570
        },
        zoom: 4
    });

    addMarkecrs();
}

function addMarkecrs() {
    //foreach
    var myLatLng = {
        lat: 45.081780,
        lng: 7.660570
    };

    var photoName = "test";
    
    var marker = new google.maps.Marker({
        position: myLatLng,
        title: photoName,
        icon: "http://icons.iconarchive.com/icons/pelfusion/long-shadow-media/48/Mobile-Smartphone-icon.png",
        url: "https://www.google.com/",
        animation: google.maps.Animation.DROP
    });

    marker.setMap(map);

    google.maps.event.addListener(marker, 'click', function () {
        window.open(marker.url);
    });
}