jQuery(function () {
    jQuery("#increment").click( function () {
        var cartsize = jQuery("#cartsize"),
            value = jQuery("#cartsize").html();
        cartsize.html(value++);
    });
});
jQuery(function () {
    jQuery("#decrement").click( function () {
        var cartsize = jQuery("#cartsize"),
            value = jQuery("#cartsize").html();
        cartsize.html(value--);
    });
});