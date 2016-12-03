# Christmas Decorations for Telligent Community

![Build Status](https://ardourdigital.visualstudio.com/_apis/public/build/definitions/8b5ba8e6-4059-46da-8ac1-e2bcf922c889/6/badge)

Add a little christmas cheer to your Telligent Community with this plugin.

The plugin allows you to add a snow effect and/or a snowman footer to your community. You can configure if this is shown for all users of your community, or allow them to opt in or out (as a sort of christmas easter egg!).

![Example of the decorations](https://raw.githubusercontent.com/ArdourDigital/ArdourDigital.TelligentCommunity.Christmas/master/Assets/example.PNG)

## Supported Versions
This plugin should work with the following versions of Telligent Community
- Telligent Community 9.x
- Telligent Community 8.x

## Installation

### Telligent 9.x

You can install the plugin by [downloading the latest version of ArdourDigital.TelligentCommunity.Christmas.dll](https://github.com/ArdourDigital/ArdourDigital.TelligentCommunity.Christmas/releases/latest) and adding it to your sites `bin` folder, or if you are using Visual Studio you can use nuget:

```
Install-Package ArdourDigital.TelligentCommunity.Christmas
```

The plugin can then be enabled by logging in as an administrator, and going to `Administration` > `Extensions` and finding `Ardour Digital - Christmas Decorations`. 

Details of how to configure the decorations are below.

### Telligent 8.x

You can install the plugin by [downloading the latest version of ArdourDigital.TelligentCommunity.Christmas.Telligent8.dll](https://github.com/ArdourDigital/ArdourDigital.TelligentCommunity.Christmas/releases/latest) and adding it to your sites `bin` folder, or if you are using Visual Studio you can use nuget:

```
Install-Package ArdourDigital.TelligentCommunity.Christmas.Telligent8
```

The plugin can then be enabled by logging in as an administrator, and going to `Control Panel` > `Site Administration` > `Manage Plugins` and finding `Ardour Digital - Christmas Decorations`. 

Details of how to configure the decorations are below.

## Configuration

### General

![General Configuration Options](https://raw.githubusercontent.com/ArdourDigital/ArdourDigital.TelligentCommunity.Christmas/master/Assets/general-configuration.PNG)

For users to see the decorations you have to configure how they get to see them. There are two ways of doing this, they can be shown to all users, or displayed to users with a specific cookie set.

#### Date Limitations

The `Start Date` and `End Date` properties can be used to specify the range of dates the decorations can be seen on. If a user visits the site outside this range, the decorations will not be shown even if they are enabled for that user.

#### All Users

You can enable for all visitors to the site by checking the `Enabled for all users` check box and saving.

#### Users with the `christmas_decorations` cookie

The cookie can be set in two ways:

##### Via a Query String

*This is only available for Telligent Community 9.x*

Set the `Enable/Disable query string key` to your chosen value, users will then be able to enable the decorations by visiting the site with that querystring value set to `true`.

For example, if you set the key to be `christmas` you could then share a link with your users like `http://www.mycommunity.com/?christmas=true`, they will then see the decorations for all future visits.

To disable them you use the same link, but with `true` changed to `false`

##### Entering a specific value

You can choose a textbox on your site that the user has to enter a specific value into.

To do this set the `Textbox selector` to use a JQuery selector for the textbox you want to use (to use the default search text box you can use `.site-banner .search .field-item-input input`), then enter the text the user needs to enter to enable the decorations in the `Textbox enabled value` property, and the value for the user to enter to disable in the `Textbox diabled value` property.

For example if you set the enabled value to be `Snow on` and the disabled value to be `Snow off` and used the search textbox, the user could enable the snow by doing a search for `Snow on` and disable it by searching for `Snow off`.

### Snow

![Snow Configuration Options](https://raw.githubusercontent.com/ArdourDigital/ArdourDigital.TelligentCommunity.Christmas/master/Assets/snow-configuration.PNG)

Here you can disable the snow effect if you would like, and disable it for mobile users if required.

You can also set the color of the snow flakes, this can be useful to make it show more/less prominently with your design.

### Snowman

![Snow Configuration Options](https://raw.githubusercontent.com/ArdourDigital/ArdourDigital.TelligentCommunity.Christmas/master/Assets/snowman-configuration.PNG)

Here you can disable the snow effect if you would like, and disable it for mobile users if required.

## Development Tools

When working with the solution you will need to keep the `bundle.js` file up to date, this can be done using the [Bundler Minifier Visual Studio Extension](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.BundlerMinifier).

## Inspiration, Sources and Thanks

This plugin was inspired by the Christmas 'Easter Egg' created by [Kieran Eves](https://twitter.com/Krensieave) when we were working with [Macmillan Cancer Support](https://community.macmillan.org.uk).

The implementation has been adapted from [this example](http://jsfiddle.net/nsdRR/) from [this blog post](https://www.madebymagnitude.com/blog/7-ideas-to-spice-up-your-website-this-christmas/).

The snowman image is from [YoPriceVilleGallery](http://gallery.yopriceville.com/Free-Clipart-Pictures/Christmas-PNG/Transparent_Snowman_with_Green_Scarf_Clipart).

The snow effect uses [JQuery Snow](http://workshop.rs/2012/01/jquery-snow-falling-plugin/).