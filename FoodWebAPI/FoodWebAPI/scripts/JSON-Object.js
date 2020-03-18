function GET(tbl) {
    const id = document.querySelector('#dataid').value;
    var myurl = 'http://localhost:50485/api/' + tbl + '/' + id;
    $.getJSON(myurl, function (data) {

        var name = `${data.name}`;
        var price = `${data.price}`;

        $(".dataname").html('Name: '+ name);
        $(".dataprice").html('Price: '+ price);
    });
}