$(document).ready(function () {
    $('.btn-weather').click(function (e) {
        e.preventDefault();
        var params = {
            lat: $(this).data("lat"),
            lon: $(this).data("lon"),
        };
        var WeatherApiUrl = "/api/weather?" + $.param(params);
        var serviceId = $(this).data("serviceid");

        $.ajax({
            url: WeatherApiUrl,
            type: 'GET',
            dataType: "json",
            success: function (json) {
                document.getElementById(serviceId + "-weather-main").innerText = json.weather[0].main;
                document.getElementById(serviceId + "-weather-description").innerText = json.weather[0].description;
            }
        }).fail(function () {
            document.getElementById(serviceId + "-weather-main").innerText = "Sorry";
            document.getElementById(serviceId + "-weather-description").innerText = "The Weather service is currently unavailable"
        });
    });
});

function weatherSuccess(json, id) {
    document.getElementById(id + "-weather-main").innerText = json.weather[0].main;
    document.getElementById(id + "-weather-description").innerText = json.weather[0].description;
}

function weatherError() {
    document.getElementById(id + "-weather-main").innerText = "Sorry";
    document.getElementById(id + "-weather-description").innerText = "Weather is currently unavailable";
}