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

        isMobile: function() {
            return $('.single-column').is(':visible');
        },

        register: function (options) {
            if (options.snowEnabled && (options.snowEnabledForMobile || !this.isMobile())) {
                this.setupSnow(options.snowColor);
            }
        }
    };

})(jQuery, window);