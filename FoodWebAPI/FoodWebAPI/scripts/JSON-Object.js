const webAPI = 'http://localhost:50485/api/';

function GET(tbl) {
    const id = document.querySelector('#dataid').value;
    var myurl = webAPI + tbl + '/' + id;
    $.getJSON(myurl, function (data) {

        var name = `${data.name}`;
        var price = `${data.price}`;

        $(".dataname").html('Name: '+ name);
        $(".dataprice").html('Price: '+ price);
    });
}