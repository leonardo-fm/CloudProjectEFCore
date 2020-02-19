var map;
var hostAndPort;

function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: {
            lat: 45.081780,
            lng: 7.660570
        },
        zoom: 4
    });
    GetPhotoList();
}

function GetPhotoList() {
    hostAndPort = location.protocol
        + '//'
        + location.hostname
        + (location.port ? ':' + location.port : '');

    fetch(hostAndPort + '/api/Map/GetPhotos').then(
        function (response) {
            console.log("Loaded photo list");
            return response.json();
        }).then(json => {
            for (var i = 0; i < json['length']; i++) {
                addMarkecr(json[i]);
            }
        }).catch(err => {
            err.text().then(errorMessage => {
                this.props.dispatch(displayTheError(errorMessage))
            })
        });
}

function addMarkecr(photo) {
    var myLatLng = {
        lat: photo['PhotoLatitude'],
        lng: photo['PhotoLongitude']
    };

    var photoName = photo['PhotoName'];

    var marker = new google.maps.Marker({
        position: myLatLng,
        title: photoName,
        icon: photo['IconPath'],
        url: hostAndPort + '/Photo/SinglePhoto?photoId=' + photo['_id']
    });

    marker.setMap(map);

    google.maps.event.addListener(marker, 'click', function () {
        window.location = marker.url;
    });
}