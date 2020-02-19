var map;
var hostAndPort;

function initMap() {
    var photoId = document.getElementById('photoId').value;
    GetPhotoList(photoId);
}

function GetPhotoList(photoId) {
    hostAndPort = location.protocol
        + '//'
        + location.hostname
        + (location.port ? ':' + location.port : '');

    fetch(hostAndPort + '/api/Map/GetPhoto?photoId=' + photoId).then(
        function (response) {
            console.log("Loaded photo");
            return response.json();
        }).then(json => {
            createMap(json['PhotoLatitude'], json['PhotoLongitude']);
            addMarkecr(json);
        }).catch(err => {
            err.text().then(errorMessage => {
                this.props.dispatch(displayTheError(errorMessage))
            })
        });
}

function createMap(photoLat, photoLng) {
    map = new google.maps.Map(document.getElementById('map'), {
        center: {
            lat: photoLat,
            lng: photoLng
        },
        zoom: 17
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
}