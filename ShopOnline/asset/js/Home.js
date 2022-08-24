$(document).ready(function(){
    $('.banner1').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        dots: true,
    });
});
$(document).ready(function(){
    $('.slide71').slick({
        slidesToShow: 5,
        slidesToScroll: 2,
        dots: true,
    });
});

$('input.input-qty').each(function() {
    console.log("Hello world!");
  var $this = $(this),
    qty = $this.parent().find('.is-form'),
    min = Number($this.attr('min')),
    max = Number($this.attr('max'))
  if (min == 0) {
    var d = 0
  } else d = min
  $(qty).on('click', function() {
    if ($(this).hasClass('minus')) {
      if (d > min) d += -1
    } else if ($(this).hasClass('plus')) {
      var x = Number($this.val()) + 1
      if (x <= max) d += 1
    }
    $this.attr('value', d).val(d)
  })
})
