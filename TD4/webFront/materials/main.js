var stations;

function reqListener(){
    var res = JSON.parse(this.responseText);

    var listElem = document.getElementById("stations");

    stations = res;

    for(var i = 0; i < res.length; i++){
        var elem = document.createElement("option");
        elem.setAttribute("value", res[i].contractName);
        listElem.appendChild(elem);
    }
}

function retrieveAllContracts(){
    var apiKey = document.getElementById("apiKey").value;
    var url = "https://api.jcdecaux.com/vls/v3/stations?apiKey=" + apiKey;

    var xhttp = new XMLHttpRequest();
    xhttp.open("GET", url, true);

    xhttp.setRequestHeader ("Accept", "application/json");
    xhttp.onload = reqListener;
    xhttp.send();
}

function getDistanceFrom2GpsCoordinates(lat1, lon1, lat2, lon2) {
    // Radius of the earth in km
    var earthRadius = 6371;
    var dLat = deg2rad(lat2-lat1);
    var dLon = deg2rad(lon2-lon1);
    var a =
        Math.sin(dLat/2) * Math.sin(dLat/2) +
        Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
        Math.sin(dLon/2) * Math.sin(dLon/2)
    ;
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
    var d = earthRadius * c; // Distance in km
    return d;
}

function deg2rad(deg) {
    return deg * (Math.PI/180)
}

async function getClosestStation(){
    var latitude = document.getElementById("lat").value;
    var longitude = document.getElementById("lng").value;

    var closestStation = stations[0].name;
    var initDistance = getDistanceFrom2GpsCoordinates(latitude, longitude, stations[0].position.latitude, stations[0].position.longitude);

    console.log(initDistance);

    for(var i = 0; i < stations.length; i++){
        if(initDistance < getDistanceFrom2GpsCoordinates(latitude, longitude, stations[i].position.latitude, stations[i].position.longitude)){
            closestStation = stations[i].name;
        }
    }
    var p = document.getElementById("displayClosestStation").innerHTML = closestStation;
}