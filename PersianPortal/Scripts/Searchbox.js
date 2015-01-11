$('#search-box').submit(function (e) {
    window.location = '/Search/Details/' + $('#searchInput').val();
    e.preventDefault();
});