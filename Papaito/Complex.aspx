<%@ Page Title="" Language="C#" MasterPageFile="~/MasterStudio.master" AutoEventWireup="true"
    CodeFile="Complex.aspx.cs" Inherits="_03Complex" %>
<%@ MasterType VirtualPath="~/MasterStudio.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHolder1" runat="Server">
<html>
	<head>
		<meta http-equiv="Content-type" content="text/html; charset=utf-8"/>
		<link rel="stylesheet" href="Gallerycss/galleriffic-3.css" type="text/css" />
		<script type="text/javascript" src="Galleryjs/jquery-1.3.2.js"></script>
		<script type="text/javascript" src="Galleryjs/jquery.history.js"></script>
		<script type="text/javascript" src="Galleryjs/jquery.gallerifficComplex.js"></script>
		<script type="text/javascript" src="Galleryjs/jquery.opacityrollover.js"></script>
		<!-- We only want the thumbnails to display when javascript is disabled -->
		<script type="text/javascript">
		    document.write('<style>.noscript { display: none; }</style>');
		    $(document).ready(
        function() {
            window.setTimeout(function() {
                $('#container').fadeIn('slow');
                //PreLoadComplex.src = "Complex/black.jpg";
            }, 4000);
            $('#container').hide()
        });
		</script>
	</head>
	<body>
		<div id="page">
		<img runat="server" src="" id="PreLoadComplex" alt="" style="position:relative; margin-top:65px; width:819px; height:370px; border:1px solid #fff" />
			<div id="container">
				<!-- Start Advanced Gallery Html Containers -->
				<div id="gallery" class="content">
					<div class="slideshow-container">
						<div id="loading" class="loader"></div>
						<div id="slideshow" class="slideshow"></div>
					</div>
					<div id="caption" class="caption-container"></div>
				</div>
				<div id="thumbs" class="navigation">
					<ul class="thumbs">
						<li>
							<a class="thumb" id="RoomAimg1" runat="server">
								<img src="Gallerycss/bt1.png" alt="" />
							</a>
						</li>

						<li>
							<a class="thumb" id="RoomAimg2" runat="server">
								<img src="Gallerycss/bt2.png" alt="" />
							</a>
						</li>

						<li>
							<a class="thumb" id="RoomAimg3" runat="server">
								<img src="Gallerycss/bt3.png" alt="" />
							</a>
						</li>

						<li>
							<a class="thumb" id="RoomBImg1" runat="server">
								<img class="RoomB" src="Gallerycss/bt1.png" alt="" />
							</a>
						</li>

						<li>
							<a class="thumb" id="RoomBImg2" runat="server">
								<img src="Gallerycss/bt2.png" alt="" />
							</a>
						</li>

						<li>
							<a class="thumb" id="RoomBImg3" runat="server">
								<img src="Gallerycss/bt3.png" alt="" />
							</a>
						</li>

						<li>
							<a class="thumb" id="RoomCImg1" runat="server">
								<img class="RoomC" src="Gallerycss/bt1.png" alt="" />
							</a>
						</li>

						<li>
							<a class="thumb" id="RoomCImg2" runat="server">
								<img src="Gallerycss/bt2.png" alt="" />
							</a>
						</li>

						<li>
							<a class="thumb" id="RoomCImg3" runat="server">
								<img src="Gallerycss/bt3.png" alt="" />
							</a>
						</li>

						<li>
							<a class="thumb" id="LookAroundImg1" runat="server">
								<img class="LookAroundComplex" src="Gallerycss/bt1.png" alt="" />
							</a>
						</li>

						<li>
							<a class="thumb" id="LookAroundImg2" runat="server">
								<img src="Gallerycss/bt2.png" alt="" />
							</a>
						</li>

						</ul>
				</div>
				<!-- End Advanced Gallery Html Containers -->
				<div style="clear: both;"></div>
			</div>
		</div>
		<script type="text/javascript">
		    jQuery(document).ready(function($) {
		        // We only want these styles applied when javascript is enabled
		        $('div.navigation').css({ 'width': '300px', 'float': 'left' });
		        $('div.content').css('display', 'block');

		        // Initially set opacity on thumbs and add
		        // additional styling for hover effect on thumbs
		        var onMouseOutOpacity = 0.67;
		        $('#thumbs ul.thumbs li').opacityrollover({
		            mouseOutOpacity: onMouseOutOpacity,
		            mouseOverOpacity: 1.0,
		            fadeSpeed: 'fast',
		            exemptionSelector: '.selected'
		        });

		        // Initialize Advanced Galleriffic Gallery
		        var gallery = $('#thumbs').galleriffic({
		            delay: 500,
		            numThumbs: 3,
		            preloadAhead: 10,
		            enableTopPager: true,
		            enableBottomPager: false,
		            maxPagesToShow: 7,
		            imageContainerSel: '#slideshow',
		            controlsContainerSel: '#controls',
		            captionContainerSel: '#caption',
		            loadingContainerSel: '#loading',
		            renderSSControls: true,
		            renderNavControls: true,
		            playLinkText: 'Play Slideshow',
		            pauseLinkText: 'Pause Slideshow',
		            prevLinkText: '&lsaquo; Previous Photo',
		            nextLinkText: 'Next Photo &rsaquo;',
		            nextPageLinkText: null,
		            prevPageLinkText: null,
		            enableHistory: true,
		            autoStart: false,
		            syncTransitions: true,
		            defaultTransitionDuration: 900,
		            onSlideChange: function(prevIndex, nextIndex) {
		                // 'this' refers to the gallery, which is an extension of $('#thumbs')
		                this.find('ul.thumbs').children()
							.eq(prevIndex).fadeTo('fast', onMouseOutOpacity).end()
							.eq(nextIndex).fadeTo('fast', 1.0);
		            },
		            onPageTransitionOut: function(callback) {
		                this.fadeTo('fast', 0.0, callback);
		            },
		            onPageTransitionIn: function() {
		                this.fadeTo('fast', 1.0);
		            }
		        });

		        /**** Functions to support integration of galleriffic with the jquery.history plugin ****/

		        // PageLoad function
		        // This function is called when:
		        // 1. after calling $.historyInit();
		        // 2. after calling $.historyLoad();
		        // 3. after pushing "Go Back" button of a browser
		        function pageload(hash) {
		            // alert("pageload: " + hash);
		            // hash doesn't contain the first # character.
		            if (hash) {
		                $.galleriffic.gotoImage(hash);
		            } else {
		                gallery.gotoIndex(0);
		            }
		        }

		        // Initialize history plugin.
		        // The callback is called at once by the present location.hash.
		        $.historyInit(pageload, "advanced.html");

		        // set onlick event for buttons using the jQuery 1.3 live method
		        $("a[rel='history']").live('click', function(e) {
		            if (e.button != 0) return true;

		            var hash = this.href;
		            hash = hash.replace(/^.*#/, '');

		            // moves to a new page.
		            // pageload is called at once.
		            // hash don't contain "#", "?"
		            $.historyLoad(hash);

		            return false;
		        });

		        /****************************************************************************************/
		    });
		</script>
	</body>
</html>
<div style="height:80px";></div>
    <div id="templatemo_content_area">
            <div class="templaetmo1_3_col"  style="margin-top:-70px";>
                <h1 id="AboutTheComplex" runat="server" style="margin-left:20px">
                    </h1>
                    <div class="space4">
            </div>
                        <p id="AboutLeft" runat="server"></p>
                <div class="space2">
                </div>
            </div>
            <div class="templaetmo_3_col templatemo_3_col_middle"  style="margin-top:-70px";>
                <h1 id="WhoPlaysHere" runat="server" style="margin-left:30px">
                    </h1>
                <div class="cleaner">
                </div>
                <div class="templatemo_gallery">
                    <ul class="thumbSm">
                        <li>
                            <div class="fadehover">
                                <img id="popSm1B" runat="server" src="" class="a" alt="" />
                                <img id="popSm1C" runat="server" src="" class="b" alt="" />
                            </div>
                        </li>
                        <li>
                            <div class="fadehover">
                                <img id="popSm2B" runat="server" src="" class="a" alt="" />
                                <img id="popSm2C" runat="server" src="" class="b" alt="" />
                            </div>
                        </li>
                        <li>
                            <div class="fadehover">
                                <img id="popSm3B" runat="server" src="" class="a" alt="" />
                                <img id="popSm3C" runat="server" src="" class="b" alt="" />
                            </div>
                        </li>
                        <li>
                            <div class="fadehover">
                                <img id="popSm4B" runat="server" src="" class="a" alt="" />
                                <img id="popSm4C" runat="server" src="" class="b" alt="" />
                            </div>
                        </li>
                        <li>
                            <div class="fadehover">
                                <img id="popSm5B" runat="server" src="" class="a" alt="" />
                                <img id="popSm5C" runat="server" src="" class="b" alt="" />
                            </div>
                        </li>
                        <li>
                            <div class="fadehover">
                                <img id="popSm6B" runat="server" src="" class="a" alt="" />
                                <img id="popSm6C" runat="server" src="" class="b" alt="" />
                            </div>
                        </li>
                        <li>
                            <div class="fadehover">
                                <img id="popSm7B" runat="server" src="" class="a" alt="" />
                                <img id="popSm7C" runat="server" src="" class="b" alt="" />
                            </div>
                        </li>
                        <li>
                            <div class="fadehover">
                                <img id="popSm8B" runat="server" src="" class="a" alt="" />
                                <img id="popSm8C" runat="server" src="" class="b" alt="" />
                            </div>
                        </li>
                        <li>
                            <div class="fadehover">
                                <img id="popSm9B" runat="server" src="" class="a" alt="" />
                                <img id="popSm9C" runat="server" src="" class="b" alt="" />
                            </div>
                        </li>
                        <li>
                            <div class="fadehover">
                                <img id="popSm10B" runat="server" src="" class="a" alt="" />
                                <img id="popSm10C" runat="server" src="" class="b" alt="" />
                            </div>
                        </li>
                        <li>
                            <div class="fadehover">
                                <img id="popSm11B" runat="server" src="" class="a" alt="" />
                                <img id="popSm11C" runat="server" src="" class="b" alt="" />
                            </div>
                        </li>
                        <li>
                            <div class="fadehover">
                                <img id="popSm12B" runat="server" src="" class="a" alt="" />
                                <img id="popSm12C" runat="server" src="" class="b" alt="" />
                            </div>
                        </li>
                    </ul>
                    <div class="templatemo_more_2">
                        <a id="MoreLink1" runat="server" href="#"></a></div>
                </div>
            </div>
            <div class="templaetmo_3_col" style="margin-top:-70px";>
                <h1 id="About" runat="server" >
                    </h1>
                <div class="space4">
                </div>
                <p id="AboutRight" runat="server" >
                    </p>
                <div class="space5">
                </div>
                <h1 id="ContactUs" runat="server" >
                    </h1>
                    <div class="space4">
            </div>
                <p runat="server" id="MainContact"></p>
            </div>
            <div class="cleaner">
            </div>
    </div>

</asp:Content>
