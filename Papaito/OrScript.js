$(document).ready(function() {

//if other artists is selected, disable it
var id = $(this).find('div.songAboout').attr('id');
var k = $('#music' + id);
k.css('visibility', 'hidden');

$('.mover').hide();

    $("ul.thumbPro li").click(function() {
        var imga = $(this).find('img.a');
        imga.hide();

        //insert the pic of the album to the left div
//        var g = $('#selectedPic');
//        g.attr("src", "");

        //insert description of the album
        var j = $(this).find('input.textAboout');
        var o = $('#seletedText');
        o.attr("innerText", j.attr('value'));

        //insert songs of the album
        var q = $(this).find('div.songAboout');
        k.attr('innerHTML', q.attr('innerHTML'));
        k.css('visibility','visible');


        window.setTimeout(function() {

        //fade in all the information
        $('#fadeInOut').toggle(function() {
            $(this).find('.mover').fadeIn('slow');
        }, function() {
        }, 3000);
        });
    });

    $("ul.thumbPro li").hover(function() {
        $(this).css({ 'z-index': '10' });
        $(this).find('img.a').animate({ "opacity": "0" }, "slow");
        $(this).find('img.b').addClass("hover").stop()
		.animate({
		    marginTop: '0',
		    marginLeft: '0',
		    top: '0',
		    left: '0',
		    width: '50px',
		    height: '50px',
		    padding: '1px'
		}, 200);

    },

    function() {
        $(this).css({ 'z-index': '0' });
        $(this).find('img.a').animate({ "opacity": "1" }, "slow");
        $(this).find('img.b').removeClass("hover").stop()
		.animate({
		    marginTop: '0',
		    marginLeft: '0',
		    top: '0',
		    left: '0',
		    width: '50px',
		    height: '50px',
		    padding: '1px'
		}, 400);
    });
});

$(document).ready(function() { //perform actions when DOM is ready
    var z = 0; //for setting the initial z-index's
    var inAnimation = false; //flag for testing if we are in a animation

    $('#pictures img').each(function() { //set the initial z-index's
        z++; //at the end we have the highest z-index value stored in the z variable
        $(this).css('z-index', z); //apply increased z-index to <img>
    });

    function swapFirstLast(isFirst) {
        if (inAnimation) return false; //if already swapping pictures just return
        else inAnimation = true; //set the flag that we process a image

        var processZindex, direction, newZindex, inDeCrease; //change for previous or next image

        if (isFirst) { processZindex = z; direction = '-'; newZindex = 1; inDeCrease = 1; } //set variables for "next" action
        else { processZindex = 1; direction = ''; newZindex = z; inDeCrease = -1; } //set variables for "previous" action

        $('#pictures img').each(function() { //process each image
            if ($(this).css('z-index') == processZindex) { //if its the image we need to process
                $(this).animate({ 'top': direction + $(this).height() + 'px' }, 'slow', function() { //animate the img above/under the gallery (assuming all pictures are equal height)
                    $(this).css('z-index', newZindex) //set new z-index
            .animate({ 'top': '0' }, 'slow', function() { //animate the image back to its original position
                inAnimation = false; //reset the flag
            });
                });
            } else { //not the image we need to process, only in/de-crease z-index
                $(this).animate({ 'top': '0' }, 'slow', function() { //make sure to wait swapping the z-index when image is above/under the gallery
                    $(this).css('z-index', parseInt($(this).css('z-index')) + inDeCrease); //in/de-crease the z-index by one
                });
            }
        });

        return false; //don't follow the clicked link
    }

    $('#next a').click(function() {
        return swapFirstLast(true); //swap first image to last position
    });

    $('#prev a').click(function() {
        return swapFirstLast(false); //swap last image to first position
    });
});

////contact and about pics




//Staff


//$(document).ready(function() {

//    $('.showDetails').hide(); // hide all the pics

//    $('img.staff').hover(function() {
//        $(this).css({ "border": "Solid Orange 2px" }); //when over one of the images, put border
//    },
//    function() {
//        $(this).css({ "border": "" }); //when out of one of the images, remove border
//    });


//    $('img.staff').click(function() { //when click on one of the images
//        $('img').hide();  //hide all images
//        $(this).attr('src', 'OrImages3/' + $(this).attr("id") + '.jpg').show(); // get the image you clicked, chenge the src to image with black background. finnally, show the new image
//        $('#outerContainer').css('background-image', 'url()'); // remove the background pic
//        $('#outerContainer').css('width', '100px'); // shrink the div with all the images
//        $('.showDetails').css('width', '700px'); //expend the div with the text
//        //        $('.showDetails').addClass('active');
//        $('img.staff').hover(function() { //remove the border
//            $(this).css({ "border": "" });
//        });
//        $('#fadeInOut').find('#ctl00_placeHolder1_' + $(this).attr("id") + 'e').show(); // get the div with all the details from .cs and show it
//    });

//    $(".showDetailsR").hover(function() { //when over the link to exit, put underline
//        $(this).css("text-decoration", "underline");
//    }, function() {
//        $(this).css("text-decoration", "none"); // when out of the link to exit, remove underline
//    }
//);

//    $('.showDetailsR').click(function() { //when click on the link to exit
//        $('img').show(); // show all images
//        $('#outerContainer').css('background-image', 'url(\'OrImages3/6.jpg\')'); //show the background pic
//        $('#outerContainer').css('width', '800px'); // expend the images div
//        $('img').each(function() { $(this).attr('src', 'OrImages3/' + $(this).attr("id") + 'b.jpg').show(); }); //for each image, change the src to the prev image and show it
//        $('.showDetails').css('width', '10px'); // shrink the details div
//        $('.showDetails').hide(); // and hide it
//        //        var r = $('.active');
//        //        r.hide();
//        $('img.staff').hover(function() {
//            $(this).css({ "border": "Solid Orange 2px" }); // put the border back, when over one of the images, put border
//        },
//        function() {
//            $(this).css({ "border": "" }); //when out of one of the images, remove border
//        });
//    });
//});



//Staff2


$(document).ready(function() {

    $('.htmltooltip').hide(); // hide all the pics

    //    window.setTimeout(function() {
    //    $('#outerContainer').css('background-position', '0px 5px').fadeIn();
    //    $('.staffSec').show();
    //    $('.spacer').hide();
    //}, 4000);

    //$('.staffSec').hide();
    //$('#outerContainer').css('background-position', '0px 5px').fadeIn();  





    //show none of the guys for 2 secnds, and then show them all
    window.setTimeout(function() {
        $('#outerContainer').css('background-position', '0px 0px').fadeOut(function() {
            $(this).css('background-position', '0px 547px').fadeIn();
        });
        $('.staffSec').show();
        $('.spacer').hide();
    }, 3000);

    $('#outerContainer').css('background-position', '0px 0px').fadeIn();
    $('.staffSec').hide();
    $('.spacer').hide();


    $('.staffSec').click(function() { //when click on one of the images

    var id = $(this).attr("id");
    $('.staffSec').hide();

        switch (id) {
            case ('itay'):
                $('.staffSec ' + id).show();
                $('#outerContainer').css('background-position', '0px 2735px');
                $('.htmltooltip').css({ 'margin-top': '-550px', 'margin-left': '180px' });
                break;
            case ('napo'):
                $('.staffSec ' + id).show();
                $('#outerContainer').css('background-position', '0px 2188px');
                $('.htmltooltip').css({ 'margin-top': '-550px', 'margin-left': '60px' });
                break;
            case ('peri'):
                $('.staffSec ' + id).show();
                $('#outerContainer').css('background-position', '0px 1641px');
                $('.htmltooltip').css({ 'margin-top': '-500px', 'margin-left': '510px' });
                break;
            case ('dudu'):
                $('.staffSec ' + id).show();
                $('#outerContainer').css('background-position', '0px 1094px');
                $('.htmltooltip').css({ 'margin-top': '-400px', 'margin-left': '410px' });
                break;
        };

    });

    $(".showDetailsR").hover(function() { //when over the link to exit, put underline
        $(this).css("text-decoration", "underline");
    }, function() {
        $(this).css("text-decoration", "none"); // when out of the link to exit, remove underline
    });

    $('.showDetailsR').click(function() { //when click on the link to exit

        $('.htmltooltip').hide();
        $('#outerContainer').css('background-position', '0px 547px');
        $('.staffSec').show();

    });
});



