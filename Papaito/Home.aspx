<%@ Page Title="" Language="C#" MasterPageFile="~/MasterStudio.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="_02Home" %>

<%@ MasterType VirtualPath="~/MasterStudio.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHolder1" runat="Server">

<script type="text/javascript" language="javascript">
    $(document).ready(
		function() {
		    $('#dock2').Fisheye(
				{
				    maxWidth: 80,
				    items: 'a',
				    itemsText: 'span',
				    container: '.dock-container2',
				    itemWidth: 130,
				    proximity: 100,
				    alignment: 'center',
				    valign: 'bottom',
				    halign: 'center'
				}
			)
		}
	);

</script>
    <div class="space7">
    </div>
    <div id="templaetmo_header">
        <div id="slideshow" style="text-align: center;">
            <div>
                <img id="HomePic1" runat="server" src="" alt="Slideshow Image 1" />
            </div>
            <div>
                <img id="HomePic2" runat="server" src="" alt="Slideshow Image 2" />
            </div>
            <div>
                <img id="HomePic3" runat="server" src="" alt="Slideshow Image 3" />
            </div>
            <div>
                <img id="HomePic4" runat="server" src="" alt="Slideshow Image 4" />
            </div>
            <div>
                <img id="HomePic5" runat="server" src="" alt="Slideshow Image 4" />
            </div>
            <div>
                <img id="HomePic6" runat="server" src="" alt="Slideshow Image 4" />
            </div>
        </div>
    </div>
    <div id="templatemo_content_area">
    
        <div class="dock" id="dock2">
            <div class="dock-container2">
                <a class="dock-item2" href="#"><span id="FishSpan1" runat="server"></span><img id="FishImg1" runat="server" src="" alt="" /></a>
                <a class="dock-item2" href="#"><span id="FishSpan2" runat="server"></span><img id="FishImg2" runat="server" src="" alt="" /></a>
                <a class="dock-item2" href="#"><span id="FishSpan3" runat="server"></span><img id="FishImg3" runat="server" src="" alt="" /></a>
                <a class="dock-item2" href="#"><span id="FishSpan4" runat="server"></span><img id="FishImg4" runat="server" src="" alt="" /></a>
                <a class="dock-item2" href="#"><span id="FishSpan5" runat="server"></span><img id="FishImg5" runat="server" src="" alt="" /></a>
                <a class="dock-item2" href="#"><span id="FishSpan6" runat="server"></span><img id="FishImg6" runat="server" src="" alt="" /></a>
            </div>
        </div>
        <div style="height: 100px">
        </div>
        <div class="templaetmo1_3_col">
            <h1 id="LatestNews" runat="server">
                </h1>
                <div class="space4">
            </div>
                 <p id="AboutLeft" runat="server"></p>
            <div class="space2">
            </div>
            <div class="space7">
            </div>
            <div class="space4">
            </div>
            <div class="space4">
            </div>
            <div class="fadeIn">
                <img src="ImagesR/0none.jpg" alt="" />
                <img src="ImagesR/1itay.jpg" alt="" />
                <img src="ImagesR/2napo.jpg" alt="" />
                <img src="ImagesR/3perry.jpg" alt="" />
                <img src="ImagesR/4dudu.jpg" alt="" />
                <img src="ImagesR/5all.jpg" alt="" />
            </div>
        </div>
        <div class="templaetmo_3_col templatemo_3_col_middle">
        <a href="Admin.aspx">Admin</a>
            <h1 id="LatestRecords" runat="server" >
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
                </ul>
                <div class="cleaner">
                </div>
            </div>
            <div class="templatemo_more_2">
                <a href="#" id="MoreLink1" runat="server"></a></div>
            <div class="cleaner">
            </div>
            <h1 id="WillComeSoon" runat="server" >
                </h1>
            <div class="cleaner">
            </div>
            <div class="templatemo_gallery">
                <ul class="thumbSm">
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
                    <a href="#" id="MoreLink2" runat="server"></a>
                    </div>
                    </div>
            </div>
        
        <div class="templaetmo_3_col">
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
