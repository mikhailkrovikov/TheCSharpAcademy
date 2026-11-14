// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function calculate() {
    var table = document.getElementById("records");
    var resultArea = document.getElementById("result");
    var result = 0;
    for (var i = 1; i < table.rows.length; i++) {
        result = result + +table.rows[i].cells[1].innerHTML;
    }
    resultArea.append(`${result}`)
}