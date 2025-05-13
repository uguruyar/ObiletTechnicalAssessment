// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#switchBtn').click(function () {
        var origin = $('#origin').val();
        var destination = $('#destination').val();
        $('#origin').val(destination);
        $('#destination').val(origin);
    });

    $('#todayBtn').click(function () {
        let today = new Date().toISOString().split('T')[0];
        $('#DepartureDate').val(today);
    });

    $('#tomorrowBtn').click(function () {
        let tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        let formatted = tomorrow.toISOString().split('T')[0];
        $('#DepartureDate').val(formatted);
    });
});
