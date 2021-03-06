/**
 * jquery.snow - jQuery Snow Effect Plugin
 *
 * Available under MIT licence
 *
 * @version 1 (21. Jan 2012)
 * @author Ivan Lazarevic
 * @requires jQuery
 * @see http://workshop.rs
 *
 * @params minSize - min size of snowflake, 10 by default
 * @params maxSize - max size of snowflake, 20 by default
 * @params newOn - frequency in ms of appearing of new snowflake, 500 by default
 * @params flakeColor - color of snowflake, #FFFFFF by default
 * @example $.fn.snow({ maxSize: 200, newOn: 1000 });
 */
!function (n) { n.fn.snow = function (o) { var e = n('<div id="flake" />').css({ position: "absolute", top: "-50px" }).html("&#10052;"), t = n(document).height(), a = n(document).width(), i = { minSize: 10, maxSize: 20, newOn: 500, flakeColor: "#FFFFFF" }, o = n.extend({}, i, o); setInterval(function () { var i = Math.random() * a - 100, m = .5 + Math.random(), r = o.minSize + Math.random() * o.maxSize, d = t - 40, l = i - 100 + 200 * Math.random(), c = 10 * t + 5e3 * Math.random(); e.clone().appendTo("body").css({ left: i, opacity: m, "font-size": r, color: o.flakeColor }).animate({ top: d, left: l, opacity: .2 }, c, "linear", function () { n(this).remove() }) }, o.newOn) } }(jQuery);
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