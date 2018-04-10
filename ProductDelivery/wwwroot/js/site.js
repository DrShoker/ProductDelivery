$(".ProductNav .col-md-1").click(function () {
    var id = this.id;
    window.location = "/Product/Catalog?dep=" + id;
});
