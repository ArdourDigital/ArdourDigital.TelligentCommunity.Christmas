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
        
        isCookiePresent: function(cookieName) {
            return this.getCookie(cookieName) == 'true';
        },

        getCookie: function(name) {
            var value = "; " + document.cookie;
            var parts = value.split("; " + name + "=");
            if (parts.length == 2) return parts.pop().split(";").shift();
        },

        setCookie: function(name, value, date) {
            document.cookie = name + "=" + value + "; expires=" + date + "; path=/";
        },

        showDecorations: function (options) {
            if (options.snowEnabled && (options.snowEnabledForMobile || !this.isMobile())) {
                this.setupSnow(options.snowColor);
            }

            if (options.snowmanEnabled && (options.snowmanEnabledForMobile || !this.isMobile())) {
                this.setupSnowman();
            }
        },

        hideDecorations: function (options) {
            this.setCookie(options.cookieName, '', 'Thu, 01 Jan 1970 00:00:00 UTC');
            window.location.reload();
        },

        register: function (options) {
            var _this = this;
            if (options.textboxSelector && options.textboxEnabledValue && options.textboxDisabledValue) {
                $(options.textboxSelector).keyup(function () {
                    var value = $(options.textboxSelector).val().toLowerCase();

                    if (value == options.textboxEnabledValue.toLowerCase()) {
                        _this.setCookie(options.cookieName, 'true', options.cookieExpiryTime);
                        _this.showDecorations(options);
                    }

                    if (value == options.textboxDisabledValue.toLowerCase()) {
                        _this.hideDecorations(options);
                    }
                });
            }

            if (options.requireCookie && !this.isCookiePresent(options.cookieName)) {
                return;
            }

            setTimeout(function () {
                _this.showDecorations(options);
            }, 1000);
        }
    };

})(jQuery, window);