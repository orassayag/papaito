//Inline HTML Tooltip script: By JavaScript Kit: http://www.javascriptkit.com
//Created: July 10th, 08'
var tooltip;
var htmltooltip = {
    tipclass: 'htmltooltip',
    fadeeffect: [true, 500],
    anchors: [],
    tooltips: [], //array to contain references to all tooltip DIVs on the page

    positiontip: function($, tipindex, e) {
        var anchor = this.anchors[tipindex]
        tooltip = this.tooltips[tipindex]
    },

    showtip: function($, tipindex, e) {
    $(tooltip).css('opacity', '0.8').fadeIn(this.fadeeffect[0])

    },

    hidetip: function($, tipindex, e) {
        $(tooltip).hide(this.fadeeffect[1]);
    },

    updateanchordimensions: function($) {
        var $anchors = $('*[@accesskey="' + htmltooltip.tipclass + '"]')
        $anchors.each(function(index) {
            this.dimensions = { w: this.offsetWidth, h: this.offsetHeight, offsetx: $(this).offset().left, offsety: $(this).offset().top }
        })
    },

    render: function() {
        jQuery(document).ready(function($) {
            htmltooltip.iebody = (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body
            var $anchors = $('*[@accesskey="' + htmltooltip.tipclass + '"]')
            var $tooltips = $('div[@class="' + htmltooltip.tipclass + '"]')
            $anchors.each(function(index) { //find all links with "title=htmltooltip" declaration
                this.dimensions = { w: this.offsetWidth, h: this.offsetHeight, offsetx: $(this).offset().left, offsety: $(this).offset().top} //store anchor dimensions
                this.tippos = index + ' pos' //store index of corresponding tooltip
                var tooltip = $tooltips.eq(index).get(0) //ref corresponding tooltip
                if (tooltip == null) //if no corresponding tooltip found
                    return //exist
                tooltip.dimensions = { w: tooltip.offsetWidth, h: tooltip.offsetHeight }
                $(tooltip).remove().appendTo('body') //add tooltip to end of BODY for easier positioning
                htmltooltip.tooltips.push(tooltip) //store reference to each tooltip
                htmltooltip.anchors.push(this) //store reference to each anchor
                var $anchor = $(this)
                $anchor.click(
					function(e) { //onMouseover element
					    htmltooltip.hidetip($, parseInt(this.tippos), e);
					    htmltooltip.positiontip($, parseInt(this.tippos), e)
					    htmltooltip.showtip($, parseInt(this.tippos), e)
					}

				)
                $(window).bind("resize", function() { htmltooltip.updateanchordimensions($) })
            })
        })
    }
}

htmltooltip.render()