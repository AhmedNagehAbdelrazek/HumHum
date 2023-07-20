// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Search bar code-----------------------------------------------------------------------------
let selectedTopic = '';
let insertedtext = '';
//@Url.Action("GetSearchResult","Menu",new { search="__search",category="__category"})
let destUrl = '/Menu/GetSearchResult/__search/__category';
let Urllocation = '';
$(document).ready(function () {

});
$('.dropdown-item').on("click", function () {
    selectedTopic = $(this).text();
    $('#dropdown-toggle').text(selectedTopic);
});
$('#input-text').on("focusout", function () {
    insertedtext = $(this).val();
});
$('.input-group-text').on("click", function () {
    insertedtext = $('#input-text').val();
    searchfor(insertedtext, selectedTopic);
});
function searchfor(search, category) {
    if (search != '') {
        if (category == '') category = "Food";
        Urllocation = destUrl.replace('__category', category);
        Urllocation = Urllocation.replace("__search", search);
        console.log(Urllocation);
        window.location.href = Urllocation;
    }
}
//--------------------------------------------------------------------------------------------