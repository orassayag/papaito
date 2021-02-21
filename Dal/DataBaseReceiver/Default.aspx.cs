using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    private PapaitoDalDataContext papaDal = new PapaitoDalDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DeleteAll();

        //-----TAB 1 - HOME-----//
        this.Start("header");
        this.Start("lastRec");
        this.Start("news");

        //-----TAB 2 - COMPLEX-----//
        this.Start("roomA");
        this.Start("roomB");
        this.Start("roomC");
        this.Start("complexLookAround");
        this.Start("complexAbout");

        //-----TAB 3 - STUDIO-----//
        this.Start("recordingRoom");
        this.Start("controlRoom");
        this.Start("studioLookAround");
        this.Start("studioAbout");

        //-----TAB 4 - DESIGN-----//
        this.Start("designPic");
        this.Start("designText");

        //-----TAB 5 - PRODUCTION-----//
        this.Start("production");

        //-----TAB 6 - ALL ARTISTS-----//
        this.Start("allArtistsGallery");
        this.Start("allArtists");

        //-----TAB 7 - CONTECT AND ABOUT-----//
        this.Start("mainContact");
        this.Start("mainAbout");

        //-----TAB 8 - STAFF-----//
        this.Start("staffItay");
        this.Start("staffNapo");
        this.Start("staffPerri");
        this.Start("staffDudu");

        //-----TAB 9 - PR-----//
        this.Start("prPic");
        this.Start("prText");

        //-----TAB 10 - PUBLISH-----//
        this.Start("publishGallery");
        this.Start("publish");

        AdminUser t = this.papaDal.AdminUsers.SingleOrDefault(g => g.UserID == "itay");
        if (t == null)
        {
            AdminUser m = new AdminUser
            {
                LoginID = this.GetNextAvailableID("admin"),
                UserID = "itay",
                Password = "papaito",
                Active = 1,
                spActive = "Enable",
                CreateTime = DateTime.Now,
                LastLogin = DateTime.Now,
                spCreateTime = DateTime.Now.ToString(),
                spLastLogin = DateTime.Now.ToString()
            };
            this.papaDal.AdminUsers.InsertOnSubmit(m);
            this.papaDal.SubmitChanges();
        }

        this.finishLabel.Text = "Done!";
    }

    public void Start(string type)  //this method create and insert values for all the lists, or update them if they are exists
    {
        //in this part, will be inserted all the text stuff
        //check to see witch section need to be full of values
        //if yes, insert them. if no, create
        //if no, create object and insert him to the session
        //the first case have explenation, same for all

        switch (type)
        {
            case "complexAbout":
                if (this.GetCount(type) == 0)
                {
                    ComplexAbout m = new ComplexAbout
                    {
                        ComplexAboutID = this.GetNextAvailableID(type),
                        AboutEn = "contact us",
                        AboutHe = "צור קשר",
                        LastUpdate = DateTime.Now,
                        spLastUpdate = DateTime.Now.ToString()

                    };
                    this.papaDal.ComplexAbouts.InsertOnSubmit(m);
                }
                break;
            case "studioAbout":

                if (this.GetCount(type) == 0)
                {
                    StudioAbout m = new StudioAbout
                    {
                        StudioAboutID = this.GetNextAvailableID(type),
                        AboutEn = "contact us",
                        AboutHe = "צור קשר",
                        LastUpdate = DateTime.Now,
                        spLastUpdate = DateTime.Now.ToString()

                    };
                    this.papaDal.StudioAbouts.InsertOnSubmit(m);
                }
                break;
            case "designText":
                if (this.GetCount(type) == 0)
                {
                    DesignText m = new DesignText
                    {
                        DesignTextID = this.GetNextAvailableID(type),
                        TextEn = "contact us",
                        TextHe = "צור קשר",
                        LastUpdate = DateTime.Now,
                        spLastUpdate = DateTime.Now.ToString()

                    };
                    this.papaDal.DesignTexts.InsertOnSubmit(m);
                }
                break;
            case "prText":
                if (this.GetCount(type) == 0)
                {
                    PrText m = new PrText
                    {
                        PrTextID = this.GetNextAvailableID(type),
                        TextEn = "contact us",
                        TextHe = "צור קשר",
                        LastUpdate = DateTime.Now,
                        spLastUpdate = DateTime.Now.ToString()

                    };
                    this.papaDal.PrTexts.InsertOnSubmit(m);
                }
                break;
            case "mainContact":
                if (this.GetCount(type) == 0)
                {
                    StreamReader aben = new StreamReader(@"C:\Works\Papaito\StudioMokeUpUniteDfinal3\DataBaseReceiver\Files\ContactEn.txt");
                    StringBuilder contactEn = new StringBuilder();

                    try
                    {
                        while (!aben.EndOfStream)
                        {
                            contactEn.Append(aben.ReadLine());
                        }
                    }
                    finally
                    {
                        if (aben != null)
                        {
                            aben.Close();
                        }
                    }


                    StreamReader abhe = new StreamReader(@"C:\Works\Papaito\StudioMokeUpUniteDfinal3\DataBaseReceiver\Files\ContactHe.txt");
                    StringBuilder contactHe = new StringBuilder();

                    try
                    {
                        while (!abhe.EndOfStream)
                        {
                            string w = abhe.ReadLine();

                            UTF8Encoding utf8Contact = new UTF8Encoding();
                            byte[] theTextContact = utf8Contact.GetBytes(w);
                            string hebrewTextContact = utf8Contact.GetString(theTextContact);

                            contactHe.Append(hebrewTextContact);
                        }
                    }
                    finally
                    {
                        if (abhe != null)
                        {
                            abhe.Close();
                        }
                    }


                    MainContact m = new MainContact
                    {
                        MainContactID = this.GetNextAvailableID(type),
                        ContactEn = contactEn.ToString(),
                        ContactHe = contactHe.ToString(),
                        LastUpdate = DateTime.Now,
                        spLastUpdate = DateTime.Now.ToString()

                    };
                    this.papaDal.MainContacts.InsertOnSubmit(m);
                }
                break;
            case "mainAbout":
                if (this.GetCount(type) == 0)
                {
                    StreamReader aben = new StreamReader(@"C:\Works\Papaito\StudioMokeUpUniteDfinal3\DataBaseReceiver\Files\AboutEn.txt");
                    StringBuilder aboutEn = new StringBuilder();

                    try
                    {
                        while (!aben.EndOfStream)
                        {
                            aboutEn.Append(aben.ReadLine());
                        }
                    }
                    finally
                    {
                        if (aben != null)
                        {
                            aben.Close();
                        }
                    }

                    StreamReader abhe = new StreamReader(@"C:\Works\Papaito\StudioMokeUpUniteDfinal3\DataBaseReceiver\Files\AboutHe.txt");
                    StringBuilder aboutHe = new StringBuilder();

                    try
                    {
                        while (!abhe.EndOfStream)
                        {
                            string y = abhe.ReadLine();

                            UTF8Encoding utf8About = new UTF8Encoding();
                            byte[] theTextAbout = utf8About.GetBytes(y);
                            string hebrewTextAbout = utf8About.GetString(theTextAbout);
                            aboutHe.Append(hebrewTextAbout);
                        }
                    }
                    finally
                    {
                        if (abhe != null)
                        {
                            abhe.Close();
                        }
                    }

                    MainAbout m = new MainAbout
                    {
                        MainAboutID = this.GetNextAvailableID(type),
                        AboutEn = aboutEn.ToString(),
                        AboutHe = aboutHe.ToString(),
                        LastUpdate = DateTime.Now,
                        spLastUpdate = DateTime.Now.ToString()
                    };
                    this.papaDal.MainAbouts.InsertOnSubmit(m);
                }
                break;
            case "staffItay":
                if (this.GetCount(type) == 0)
                {
                    StaffItay m = new StaffItay
                    {
                        StaffItayID = this.GetNextAvailableID(type),
                        ItayTitleEn = "Itay Ben Margy",
                        ItayTitleHe = "איתי בן מרגי",
                        ItayTextEn = "test",
                        ItayTextHe = "ניסוי",
                        LastUpdate = DateTime.Now,
                        spLastUpdate = DateTime.Now.ToString()

                    };
                    this.papaDal.StaffItays.InsertOnSubmit(m);
                }
                break;
            case "staffNapo":
                if (this.GetCount(type) == 0)
                {
                    StaffNapo m = new StaffNapo
                    {
                        StaffNapoID = this.GetNextAvailableID(type),
                        NapoTitleEn = "Napo",
                        NapoTitleHe = "נאפו",
                        NapoTextEn = "test",
                        NapoTextHe = "ניסוי",
                        LastUpdate = DateTime.Now,
                        spLastUpdate = DateTime.Now.ToString()

                    };
                    this.papaDal.StaffNapos.InsertOnSubmit(m);
                }
                break;
            case "staffPerri":
                if (this.GetCount(type) == 0)
                {
                    StaffPerri m = new StaffPerri
                    {
                        StaffPerriID = this.GetNextAvailableID(type),
                        PerriTitleEn = "Perri Attia",
                        PerriTitleHe = "פרי אטיה",
                        PerriTextEn = "test",
                        PerriTextHe = "ניסוי",
                        LastUpdate = DateTime.Now,
                        spLastUpdate = DateTime.Now.ToString()

                    };
                    this.papaDal.StaffPerris.InsertOnSubmit(m);
                }
                break;
            case "staffDudu":
                if (this.GetCount(type) == 0)
                {
                    StaffDudu m = new StaffDudu
                    {
                        StaffDuduID = this.GetNextAvailableID(type),
                        DuduTitleEn = "Dudu Bones",
                        DuduTitleHe = "דוד 'דודו' בונס",
                        DuduTextEn = "test",
                        DuduTextHe = "ניסוי",
                        LastUpdate = DateTime.Now,
                        spLastUpdate = DateTime.Now.ToString()

                    };
                    this.papaDal.StaffDudus.InsertOnSubmit(m);
                }
                break;
            case "news":
                StreamReader newsREn = new StreamReader(@"C:\Works\Papaito\StudioMokeUpUniteDfinal3\DataBaseReceiver\Files\NewsEn.txt");
                StringBuilder newsBEn = new StringBuilder();

                                    List<string> listEn = new List<string>();
                try
                {
                    while (!newsREn.EndOfStream)
                    {
                        string m = newsREn.ReadLine();
                        if (m == "%")
                        {
                            listEn.Add(newsBEn.ToString());
                            newsBEn = new StringBuilder();
                        }
                        else
                        {
                            newsBEn.Append(m);
                        }
                    }
                }
                finally
                {
                    if (newsREn != null)
                    {
                        newsREn.Close();
                    }
                }


                StreamReader newsRHe = new StreamReader(@"C:\Works\Papaito\StudioMokeUpUniteDfinal3\DataBaseReceiver\Files\NewsHe.txt");
                StringBuilder newsBHe = new StringBuilder();

                List<string> listHe = new List<string>();
                try
                {
                    while (!newsRHe.EndOfStream)
                    {
                        UTF8Encoding utf8News = new UTF8Encoding();


                        string m = newsRHe.ReadLine();
                        if (m == "%")
                        {
                            byte[] theTextNews = utf8News.GetBytes(m);
                            string hebrewTextNews = utf8News.GetString(theTextNews);

                            listHe.Add(hebrewTextNews);
                            newsBHe = new StringBuilder();
                        }
                        else
                        {
                            byte[] theTextNews = utf8News.GetBytes(m);
                            string hebrewTextNews = utf8News.GetString(theTextNews);

                            newsBHe.Append(hebrewTextNews);
                        }
                    }
                }
                finally
                {
                    if (newsRHe != null)
                    {
                        newsRHe.Close();
                    }
                }

                for (int i = 0; i < listHe.Count; i++)
                {
                    NewsOb p20 = new NewsOb
                    {
                        NewsID = i.ToString(),
                        NewsEn = listEn[i],
                        NewsHe = listHe[i],
                        UploadTime = DateTime.Now,
                        spUploadTime = DateTime.Now.ToString()
                    };
                    this.papaDal.NewsObs.InsertOnSubmit(p20);
                }
                break;
        }


        //in this section, all the pics will be created and inserted to the list


        string ID = "";                                         //thr id of the object
        string path = "ImagesR/1.jpg";                          //example of pic
        string pathColor = "ImagesR/4smC.jpg";                  //example of color pic
        string pathBW = "ImagesR/4smB.jpg";                     //example of black & white pic
        byte active = 1;                                        //pic is enabled
        string spActive = "Enable";                             //pic is enabled
        string artistNameHe = "מולה ג";                         //example of hebrew artist name
        string artistNameEn = "Mula j";                         //example of english artist name
        string pathProBig = "OrImage/proC43big.jpg";            //example of normal pic
        string pathProColor = "OrImage/proC43.jpg";             //example of small color pic
        string pathProBW = "OrImage/proB43.jpg";                //example of small black and white pic
        string textPublishHe = "שם הפירסום";                    //example of name of the publish pics in hebrew
        string textPublishEn = "Publish Name";                  //example of name of the publish pics in english
        string artistTextEn = @"Text Text Text Text Text Text Text
                                Text Text Text Text Text Text Text
                                Text Text Text Text Text Text Text "; //example pf artist Text English
        string artistTextHe = @"טקסט טקסט טקסט טקסט טקסט טקסט טקסט
                                טקסט טקסט טקסט טקסט טקסט טקסט טקסט
                                טקסט טקסט טקסט טקסט טקסט טקסט טקסט  "; //example pf artist Text English


        for (int i = 0, z = 1; i < 30; i++, z++) //create 10 objects for each list
        {
            ID = i.ToString();

            if (z == 6)
            {
                z = 1;
            }


            //the first case have explenation, same for all

            switch (type)
            {
                case "header":
                    HeaderPic p1 = new HeaderPic //create new object
                    {
                        Active = active,
                        PicID = ID,
                        PicPath = path,
                        UploadTime = DateTime.Now,
                        spUploadTime = DateTime.Now.ToString(),
                        spActive = spActive
                    };
                    this.papaDal.HeaderPics.InsertOnSubmit(p1); //add the object to the list
                    break;
                case "lastRec":
                    LastRecordPic p3 = new LastRecordPic
                        {
                            Active = active,
                            PicID = ID,
                            PicPathColor = pathColor,
                            PicPathBW = pathBW,
                            UploadTime = DateTime.Now,
                            spUploadTime = DateTime.Now.ToString(),
                            spActive = spActive
                        };
                    this.papaDal.LastRecordPics.InsertOnSubmit(p3);
                    break;
                case "roomA":
                    RoomAPic p4 = new RoomAPic
                        {
                            Active = active,
                            PicID = ID,
                            PicPath = path,
                            UploadTime = DateTime.Now,
                            spUploadTime = DateTime.Now.ToString(),
                            spActive = spActive
                        };
                    this.papaDal.RoomAPics.InsertOnSubmit(p4);
                    break;
                case "roomB":
                    RoomBPic p5 = new RoomBPic
                    {
                        Active = active,
                        PicID = ID,
                        PicPath = path,
                        UploadTime = DateTime.Now,
                        spUploadTime = DateTime.Now.ToString(),
                        spActive = spActive
                    };
                    this.papaDal.RoomBPics.InsertOnSubmit(p5);
                    break;
                case "roomC":
                    RoomCPic p6 = new RoomCPic
                    {
                        Active = active,
                        PicID = ID,
                        PicPath = path,
                        UploadTime = DateTime.Now,
                        spUploadTime = DateTime.Now.ToString(),
                        spActive = spActive
                    };
                    this.papaDal.RoomCPics.InsertOnSubmit(p6);
                    break;
                case "complexLookAround":
                    ComplexLookAroundPic p7 = new ComplexLookAroundPic
                    {
                        Active = active,
                        PicID = ID,
                        PicPath = path,
                        UploadTime = DateTime.Now,
                        spUploadTime = DateTime.Now.ToString(),
                        spActive = spActive
                    };
                    this.papaDal.ComplexLookAroundPics.InsertOnSubmit(p7);
                    break;
                case "recordingRoom":
                    RecordingRoomPic p9 = new RecordingRoomPic
                    {
                        Active = active,
                        PicID = ID,
                        PicPathColor = pathColor,
                        PicPathBW = pathBW,
                        UploadTime = DateTime.Now,
                        spUploadTime = DateTime.Now.ToString(),
                        spActive = spActive
                    };
                    this.papaDal.RecordingRoomPics.InsertOnSubmit(p9);
                    break;
                case "controlRoom":
                    ControlRoomPic p10 = new ControlRoomPic
                    {
                        Active = active,
                        PicID = ID,
                        PicPath = path,
                        UploadTime = DateTime.Now,
                        spUploadTime = DateTime.Now.ToString(),
                        spActive = spActive
                    };
                    this.papaDal.ControlRoomPics.InsertOnSubmit(p10);
                    break;
                case "studioLookAround":
                    StudioLookAroundPic p11 = new StudioLookAroundPic
                    {
                        Active = active,
                        PicID = ID,
                        PicPath = path,
                        UploadTime = DateTime.Now,
                        spUploadTime = DateTime.Now.ToString(),
                        spActive = spActive
                    };
                    this.papaDal.StudioLookAroundPics.InsertOnSubmit(p11);
                    break;
                case "designPic":
                    DesignPic p13 = new DesignPic
                    {
                        Active = active,
                        PicID = ID,
                        PicPath = path,
                        UploadTime = DateTime.Now,
                        spUploadTime = DateTime.Now.ToString(),
                        spActive = spActive
                    };
                    this.papaDal.DesignPics.InsertOnSubmit(p13);
                    break;
                case "production":
                    //same is normal, add the songlist to the new Production object

                    Production p14 = new Production
                        {
                            Active = active,
                            ArtistNameHe = artistNameHe,
                            ArtistNameEn = artistNameEn,
                            ArtistTextEn = artistTextEn,
                            ArtistTextHe = artistTextHe,
                            PicPathMain = pathProBig,
                            PicPathColor = pathProColor,
                            PicPathBW = pathProBW,
                            ProID = ID,
                            UploadTime = DateTime.Now,
                            spUploadTime = DateTime.Now.ToString(),
                            spActive = spActive
                        };
                    this.papaDal.Productions.InsertOnSubmit(p14);
                    this.papaDal.SubmitChanges();

                    for (int y = 0; y < 3; y++) //create 3 songs
                    {
                        string songID = this.GetNextAvailableID("song"); //get next available id for song
                        /*this is the the
                         * youtube embad*/
                        string youtube = @"<object width=""320"" height=""265""><param name=""movie"" value=""http://www.youtube.com/v/ECbJdeh1VvA&hlen_US&fs=1&color1=0xe1600f&color2=0xfebd01""></param><param
                                                                name=""allowFullScreen"" value=""true""></param><param name=""allowscriptaccess""value=""always""></param><embed
                                                                src=""http://www.youtube.com/v/ECbJdeh1VvA&hlen_US&fs=1&color1=0xe1600f&color2=0xfebd01"" type=""application/x-shockwave-flash"" allowscriptaccess=""always""
                                                                allowfullscreen=""true""width=""200"" height=""25""></embed></object>";
                        Song s = new Song //create new song object
                            {
                                Active = active,
                                SongID = songID,
                                SongNameHe = "זמר זמר" + y,
                                SongNameEn = "song song" + y,
                                YouTubePath = youtube,
                                ProIDParent = ID,
                                UploadTime = DateTime.Now,
                                spUploadTime = DateTime.Now.ToString(),
                                spActive = spActive
                            };

                        this.papaDal.Songs.InsertOnSubmit(s); //add the objec to the songlist
                        this.papaDal.SubmitChanges();
                    }

                    break;
                case "allArtistsGallery":
                    if (i < 5)
                    {
                        AllArtistGallery p81 = new AllArtistGallery
                        {
                            AllArtistGalleryID = z.ToString(),
                            AllArtistNameEn = "Gallery " + z.ToString(),
                            AllArtistNameHe = "גלריה " + z.ToString(),
                            LastUpdate = DateTime.Now,
                            spLastUpdate = DateTime.Now.ToString()
                        };
                        this.papaDal.AllArtistGalleries.InsertOnSubmit(p81);
                    }
                    break;
                case "allArtists":
                    AllArtistPic p15 = new AllArtistPic
                    {
                        PicID = ID,
                        PicPath = path,
                        TextEn = textPublishEn,
                        TextHe = textPublishHe,
                        Active = active,
                        GalleryID = z.ToString(),
                        UploadTime = DateTime.Now,
                        spUploadTime = DateTime.Now.ToString(),
                        spActive = spActive
                    };
                    this.papaDal.AllArtistPics.InsertOnSubmit(p15);
                    break;
                case "prPic":
                    PrPic p21 = new PrPic
                    {
                        Active = active,
                        PicID = ID,
                        PicPath = path,
                        UploadTime = DateTime.Now,
                        spUploadTime = DateTime.Now.ToString(),
                        spActive = spActive
                    };
                    this.papaDal.PrPics.InsertOnSubmit(p21);
                    break;
                case "publishGallery":
                    if (i < 5)
                    {
                        PublishGallery p82 = new PublishGallery
                        {
                            PublishGalleryID = z.ToString(),
                            PublishGalleryNameEn = "Gallery " + z.ToString(),
                            PublishGalleryNameHe = "גלריה " + z.ToString(),
                            LastUpdate = DateTime.Now,
                            spLastUpdate = DateTime.Now.ToString()
                        };
                        this.papaDal.PublishGalleries.InsertOnSubmit(p82);
                    }
                    break;
                case "publish":
                    PublishPic p22 = new PublishPic
                        {
                            PicID = ID,
                            PicPath = path,
                            TextEn = textPublishEn,
                            TextHe = textPublishHe,
                            Active = active,
                            TopPage = 2,
                            TopPagePlace = 0,
                            GalleryID = z.ToString(),
                            UploadTime = DateTime.Now,
                            spUploadTime = DateTime.Now.ToString(),
                            spActive = spActive

                        };
                    this.papaDal.PublishPics.InsertOnSubmit(p22);
                    break;
            }

            //here it just save the new list with the values in session (DB)

            this.papaDal.SubmitChanges();
        }
    }

    public int GetCount(string type)
    {
        int result = -1;

        if (type == "")
        {
            return result;
        }

        switch (type)
        {
            case "admin":
                result = this.papaDal.AdminUsers.Count();
                break;
            case "complexAbout":
                result = this.papaDal.ComplexAbouts.Count();
                break;
            case "studioAbout":
                result = this.papaDal.StudioAbouts.Count();
                break;
            case "designText":
                result = this.papaDal.DesignTexts.Count();
                break;
            case "prText":
                result = this.papaDal.PrTexts.Count();
                break;
            case "mainContact":
                result = this.papaDal.MainContacts.Count();
                break;
            case "mainAbout":
                result = this.papaDal.MainAbouts.Count();
                break;
            case "staffItay":
                result = this.papaDal.StaffItays.Count();
                break;
            case "staffNapo":
                result = this.papaDal.StaffNapos.Count();
                break;
            case "staffPerri":
                result = this.papaDal.StaffPerris.Count();
                break;
            case "staffDudu":
                result = this.papaDal.StaffDudus.Count();
                break;
            case "header":
                result = this.papaDal.HeaderPics.Count();
                break;
            case "lastRec":
                result = this.papaDal.LastRecordPics.Count();
                break;
            case "roomA":
                result = this.papaDal.RoomAPics.Count();
                break;
            case "roomB":
                result = this.papaDal.RoomBPics.Count();
                break;
            case "roomC":
                result = this.papaDal.RoomCPics.Count();
                break;
            case "complexLookAround":
                result = this.papaDal.ComplexLookAroundPics.Count();
                break;
            case "recordingRoom":
                result = this.papaDal.RecordingRoomPics.Count();
                break;
            case "controlRoom":
                result = this.papaDal.ControlRoomPics.Count();
                break;
            case "studioLookAround":
                result = this.papaDal.StudioLookAroundPics.Count();
                break;
            case "designPic":
                result = this.papaDal.DesignPics.Count();
                break;
            case "production":
                result = this.papaDal.Productions.Count();
                break;
            case "song":
                result = this.papaDal.Songs.Count();
                break;
            case "allArtists":
                result = this.papaDal.AllArtistPics.Count();
                break;
            case "news":
                result = this.papaDal.NewsObs.Count();
                break;
            case "prPic":
                result = this.papaDal.PrPics.Count();
                break;
            case "publish":
                result = this.papaDal.PublishPics.Count();
                break;
            default:
                break;
        }

        return result;
    }

    public string GetNextAvailableID(string type)
    {
        int result = 0;

        if (type == "")
        {
            return "";
        }

        switch (type)
        {
            case "admin":
                AdminUser a1 = null;
                while ((a1 = (AdminUser)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "complexAbout":
                ComplexAbout a2 = null;
                while ((a2 = (ComplexAbout)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "studioAbout":
                StudioAbout a3 = null;
                while ((a3 = (StudioAbout)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "designText":
                DesignText a4 = null;
                while ((a4 = (DesignText)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "prText":
                PrText a5 = null;
                while ((a5 = (PrText)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "mainContact":
                MainContact a6 = null;
                while ((a6 = (MainContact)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "mainAbout":
                MainAbout a7 = null;
                while ((a7 = (MainAbout)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "staffItay":
                StaffItay a8 = null;
                while ((a8 = (StaffItay)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "staffNapo":
                StaffNapo a9 = null;
                while ((a9 = (StaffNapo)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "staffPerri":
                StaffPerri a10 = null;
                while ((a10 = (StaffPerri)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "staffDudu":
                StaffDudu a11 = null;
                while ((a11 = (StaffDudu)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "header":
                HeaderPic a12 = null;
                while ((a12 = (HeaderPic)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "lastRec":
                LastRecordPic a13 = null;
                while ((a13 = (LastRecordPic)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "roomA":
                RoomAPic a14 = null;
                while ((a14 = (RoomAPic)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "roomB":
                RoomBPic a15 = null;
                while ((a15 = (RoomBPic)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "roomC":
                RoomCPic a16 = null;
                while ((a16 = (RoomCPic)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "complexLookAround":
                ComplexLookAroundPic a17 = null;
                while ((a17 = (ComplexLookAroundPic)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "recordingRoom":
                RecordingRoomPic a18 = null;
                while ((a18 = (RecordingRoomPic)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "controlRoom":
                ControlRoomPic a19 = null;
                while ((a19 = (ControlRoomPic)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "studioLookAround":
                StudioLookAroundPic a20 = null;
                while ((a20 = (StudioLookAroundPic)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "designPic":
                DesignPic a21 = null;
                while ((a21 = (DesignPic)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "production":
                Production a22 = null;
                while ((a22 = (Production)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "song":
                Song a23 = null;
                while ((a23 = (Song)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "allArtists":
                AllArtistPic a24 = null;
                while ((a24 = (AllArtistPic)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "news":
                NewsOb a25 = null;
                while ((a25 = (NewsOb)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "prPic":
                PrPic a26 = null;
                while ((a26 = (PrPic)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "publish":
                PublishPic a27 = null;
                while ((a27 = (PublishPic)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "publishGallery":
                PublishGallery a28 = null;
                while ((a28 = (PublishGallery)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "allArtistsGallery":
                AllArtistGallery a29 = null;
                while ((a29 = (AllArtistGallery)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            default:
                break;
        }

        return result.ToString();
    }

    public object Get(string type, string ID)
    {
        if (type == "")
        {
            return null;
        }

        object obj = null;

        switch (type)
        {
            case "admin":
                obj = this.papaDal.AdminUsers.SingleOrDefault(g => g.UserID == ID);
                break;
            case "header":
                obj = this.papaDal.HeaderPics.SingleOrDefault(g => g.PicID == ID);
                break;
            case "lastRec":
                obj = this.papaDal.LastRecordPics.SingleOrDefault(g => g.PicID == ID);
                break;
            case "roomA":
                obj = this.papaDal.RoomAPics.SingleOrDefault(g => g.PicID == ID);
                break;
            case "roomB":
                obj = this.papaDal.RoomBPics.SingleOrDefault(g => g.PicID == ID);
                break;
            case "roomC":
                obj = this.papaDal.RoomCPics.SingleOrDefault(g => g.PicID == ID);
                break;
            case "complexLookAround":
                obj = this.papaDal.ComplexLookAroundPics.SingleOrDefault(g => g.PicID == ID);
                break;
            case "recordingRoom":
                obj = this.papaDal.RecordingRoomPics.SingleOrDefault(g => g.PicID == ID);
                break;
            case "controlRoom":
                obj = this.papaDal.ControlRoomPics.SingleOrDefault(g => g.PicID == ID);
                break;
            case "studioLookAround":
                obj = this.papaDal.StudioLookAroundPics.SingleOrDefault(g => g.PicID == ID);
                break;
            case "designPic":
                obj = this.papaDal.DesignPics.SingleOrDefault(g => g.PicID == ID);
                break;
            case "production":
                obj = this.papaDal.Productions.SingleOrDefault(g => g.ProID == ID);
                break;
            case "allArtists":
                obj = this.papaDal.AllArtistPics.SingleOrDefault(g => g.PicID == ID);
                break;
            case "news":
                obj = this.papaDal.NewsObs.SingleOrDefault(g => g.NewsID == ID);
                break;
            case "pr":
                obj = this.papaDal.PrPics.SingleOrDefault(g => g.PicID == ID);
                break;
            case "publish":
                obj = this.papaDal.PublishPics.SingleOrDefault(g => g.PicID == ID);
                break;
            case "complexAbout":
                obj = this.papaDal.ComplexAbouts.SingleOrDefault(g => g.ComplexAboutID == ID);
                break;
            case "studioAbout":
                obj = this.papaDal.StudioAbouts.SingleOrDefault(g => g.StudioAboutID == ID);
                break;
            case "designText":
                obj = this.papaDal.DesignTexts.SingleOrDefault(g => g.DesignTextID == ID);
                break;
            case "prText":
                obj = this.papaDal.PrTexts.SingleOrDefault(g => g.PrTextID == ID);
                break;
            case "mainAbout":
                obj = this.papaDal.MainAbouts.SingleOrDefault(g => g.MainAboutID == ID);
                break;
            case "mainContact":
                obj = this.papaDal.MainContacts.SingleOrDefault(g => g.MainContactID == ID);
                break;
            case "staffItay":
                obj = this.papaDal.StaffItays.SingleOrDefault(g => g.StaffItayID == ID);
                break;
            case "staffNapo":
                obj = this.papaDal.StaffNapos.SingleOrDefault(g => g.StaffNapoID == ID);
                break;
            case "staffPerri":
                obj = this.papaDal.StaffPerris.SingleOrDefault(g => g.StaffPerriID == ID);
                break;
            case "staffDudu":
                obj = this.papaDal.StaffDudus.SingleOrDefault(g => g.StaffDuduID == ID);
                break;
            case "song":
                obj = this.papaDal.Songs.SingleOrDefault(g => g.SongID == ID);
                break;
            case "publishGallery":
                obj = this.papaDal.PublishGalleries.SingleOrDefault(g => g.PublishGalleryID == ID);
                break;
            case "allArtistsGallery":
                obj = this.papaDal.AllArtistGalleries.SingleOrDefault(g => g.AllArtistGalleryID == ID);
                break;
            default:
                break;
        }

        return obj;
    }

    public void DeleteAll()
    {
        this.papaDal.AdminUsers.DeleteAllOnSubmit(this.papaDal.AdminUsers);
        this.papaDal.AllArtistGalleries.DeleteAllOnSubmit(this.papaDal.AllArtistGalleries);
        this.papaDal.AllArtistPics.DeleteAllOnSubmit(this.papaDal.AllArtistPics);
        this.papaDal.ControlRoomPics.DeleteAllOnSubmit(this.papaDal.ControlRoomPics);
        this.papaDal.ComplexAbouts.DeleteAllOnSubmit(this.papaDal.ComplexAbouts);
        this.papaDal.ComplexLookAroundPics.DeleteAllOnSubmit(this.papaDal.ComplexLookAroundPics);
        this.papaDal.DesignPics.DeleteAllOnSubmit(this.papaDal.DesignPics);
        this.papaDal.DesignTexts.DeleteAllOnSubmit(this.papaDal.DesignTexts);
        this.papaDal.HeaderPics.DeleteAllOnSubmit(this.papaDal.HeaderPics);
        this.papaDal.LastRecordPics.DeleteAllOnSubmit(this.papaDal.LastRecordPics);
        this.papaDal.MainAbouts.DeleteAllOnSubmit(this.papaDal.MainAbouts);
        this.papaDal.MainContacts.DeleteAllOnSubmit(this.papaDal.MainContacts);
        this.papaDal.NewsObs.DeleteAllOnSubmit(this.papaDal.NewsObs);
        this.papaDal.Productions.DeleteAllOnSubmit(this.papaDal.Productions);
        this.papaDal.PrPics.DeleteAllOnSubmit(this.papaDal.PrPics);
        this.papaDal.PrTexts.DeleteAllOnSubmit(this.papaDal.PrTexts);
        this.papaDal.PublishGalleries.DeleteAllOnSubmit(this.papaDal.PublishGalleries);
        this.papaDal.PublishPics.DeleteAllOnSubmit(this.papaDal.PublishPics);
        this.papaDal.RecordingRoomPics.DeleteAllOnSubmit(this.papaDal.RecordingRoomPics);
        this.papaDal.RoomAPics.DeleteAllOnSubmit(this.papaDal.RoomAPics);
        this.papaDal.RoomBPics.DeleteAllOnSubmit(this.papaDal.RoomBPics);
        this.papaDal.RoomCPics.DeleteAllOnSubmit(this.papaDal.RoomCPics);
        this.papaDal.Songs.DeleteAllOnSubmit(this.papaDal.Songs);
        this.papaDal.StaffDudus.DeleteAllOnSubmit(this.papaDal.StaffDudus);
        this.papaDal.StaffItays.DeleteAllOnSubmit(this.papaDal.StaffItays);
        this.papaDal.StaffNapos.DeleteAllOnSubmit(this.papaDal.StaffNapos);
        this.papaDal.StaffPerris.DeleteAllOnSubmit(this.papaDal.StaffPerris);
        this.papaDal.StudioAbouts.DeleteAllOnSubmit(this.papaDal.StudioAbouts);
        this.papaDal.StudioLookAroundPics.DeleteAllOnSubmit(this.papaDal.StudioLookAroundPics);

        this.papaDal.SubmitChanges();
    }
}