$('body').scrollspy({ target: "#prodNav", offset: 150 });

$('.prod-info').hide();

$('.img-info').hide();

$('.card').mouseover(function () {
    id = '#'+this.id;
    $(id + ' .img-info').fadeIn(250);
    $(id + ' .prod-info').slideDown();

});

$('.card').mouseleave(function () {
    id = '#'+this.id;
    $(id + ' .prod-info').slideUp();
    $(id + ' .img-info').fadeOut(250);
})