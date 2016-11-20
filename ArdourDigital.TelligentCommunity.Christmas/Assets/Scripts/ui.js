(function ($, global, undef) {
    $.ardour = $.ardour || {};
    $.ardour.extensions = $.ardour.extensions || {};
    $.ardour.extensions.plugins = $.ardour.extensions.plugins || {};

    $.ardour.extensions.plugins.christmas = {
        setupSnow: function(color) {
            if (!color) {
                color = '#cccccc';
            }

            $.fn.snow({
                minSize: 5,
                maxSize: 25,
                newOn: 350,
                flakeColor: color
            });
        },

        setupSnowman: function () {
            $('body').append('<div class="ardour-snow-background">&nbsp;</div><div class="ardour-snowman">&nbsp;</div>');

            $('.ardour-snowman').click(function () {
                $(this).toggleClass('ardour-snowman-expanded');
            });
        },

        isMobile: function() {
            return $('.single-column').is(':visible');
        },

        register: function (options) {
            if (options.snowEnabled && (options.snowEnabledForMobile || !this.isMobile())) {
                this.setupSnow(options.snowColor);
            }

            if (options.snowmanEnabled && (options.snowmanEnabledForMobile || !this.isMobile())) {
                this.setupSnowman();
            }
        }
    };

})(jQuery, window);