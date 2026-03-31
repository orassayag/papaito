using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Dal
{
    public class PapaDal
    {
        private PapaitoDalDataContext papaDal = new PapaitoDalDataContext();

        public PapaDal() { }

        public bool CheckAvailablePlace(string type, int place)
        {
            if (place == 0)
            {
                return true;
            }

            if (type == "" || type == null || place < 0)
            {
                return false;
            }

            bool result = false;

            switch (type)
            {
                case "roomA":
                case "roomB":
                case "roomC":
                case "complexLookAroundRoom":
                case "complexWhoPlaysHere":
                    ComplexRoomPic j = this.papaDal.ComplexRoomPics.SingleOrDefault(g => g.RoomName == type && g.PicPlace == place);
                    result = j == null;
                    break;
                case "allArtistsGallery":
                    Gallery y = this.papaDal.Galleries.SingleOrDefault(g => g.GalleryType == type && g.GalleryPlace == place);
                    result = y == null;
                    break;
                case "publishGallery":
                    Gallery n = this.papaDal.Galleries.SingleOrDefault(g => g.GalleryType == type && g.GalleryPlace == place);
                    result = n == null;
                    break;
                case "headerPic":
                    HeaderPic q = this.papaDal.HeaderPics.SingleOrDefault(g => g.PicPlace == place);
                    result = q == null;
                    break;
                case "lastNews":
                    LastNew w = this.papaDal.LastNews.SingleOrDefault(g => g.NewsPlace == place);
                    result = w == null;
                    break;
                case "lastRecordsPic":
                    LastRecordPic e = this.papaDal.LastRecordPics.SingleOrDefault(g => g.PicPlace == place);
                    result = e == null;
                    break;
                case "production":
                    Production r = this.papaDal.Productions.SingleOrDefault(g => g.ProPlace == place);
                    result = r == null;
                    break;
                case "publishPicTop":
                    PublishPic v = this.papaDal.PublishPics.SingleOrDefault(g => g.TopPagePlace == place);
                    result = v == null;
                    break;
                case "song":
                    Song u = this.papaDal.Songs.SingleOrDefault(g => g.SongPlace == place);
                    result = u == null;
                    break;
                case "staffPic":
                    Staff i = this.papaDal.Staffs.SingleOrDefault(g => g.StaffPlace == place);
                    result = i == null;
                    break;
                case "controlRoom":
                case "recordingRoom":
                case "studioLookAroundRoom":
                case "studioWhoPlaysHere":
                    StudioRoomPic l = this.papaDal.StudioRoomPics.SingleOrDefault(g => g.RoomName == type && g.PicPlace == place);
                    result = l == null;
                    break;
                case "willComePic":
                    WillComePic a = this.papaDal.WillComePics.SingleOrDefault(g => g.PicPlace == place);
                    result = a == null;
                    break;
                default:
                    break;
            }

            return result;
        }

        public bool CheckAvailablePlaceExcept(string type, int existingPlace, int newPlace)
        {
            if (newPlace == 0)
            {
                return true;
            }

            if (type == "" || type == null || newPlace < 0 || existingPlace < 0)
            {
                return false;
            }

            bool result = false;

            switch (type)
            {
                case "roomA":
                case "roomB":
                case "roomC":
                case "complexLookAroundRoom":
                case "complexWhoPlaysHere":
                    ComplexRoomPic s = this.papaDal.ComplexRoomPics.SingleOrDefault
                      (g => g.RoomName == type && g.PicPlace != existingPlace && g.PicPlace == newPlace);
                    result = s == null;
                    break;
                case "allArtistsGallery":
                    Gallery u = this.papaDal.Galleries.SingleOrDefault(g => g.GalleryType == type &&
                        g.GalleryPlace != existingPlace && g.GalleryPlace == newPlace);
                    result = u == null;
                    break;
                case "publishGallery":
                    Gallery x = this.papaDal.Galleries.SingleOrDefault(g => g.GalleryType == type &&
                        g.GalleryPlace != existingPlace && g.GalleryPlace == newPlace);
                    result = x == null;
                    break;
                case "headerPic":
                    HeaderPic r = this.papaDal.HeaderPics.SingleOrDefault
                    (g => g.PicPlace != existingPlace && g.PicPlace == newPlace);
                    result = r == null;
                    break;
                case "lastNews":
                    LastNew t = this.papaDal.LastNews.SingleOrDefault
                    (g => g.NewsPlace != existingPlace && g.NewsPlace == newPlace);
                    result = t == null;
                    break;
                case "lastRecordsPic":
                    LastRecordPic z = this.papaDal.LastRecordPics.SingleOrDefault
                    (g => g.PicPlace != existingPlace && g.PicPlace == newPlace);
                    result = z == null;
                    break;
                case "production":
                    Production a = this.papaDal.Productions.SingleOrDefault
                    (g => g.ProPlace != existingPlace && g.ProPlace == newPlace);
                    result = a == null;
                    break;
                case "publishPicTop":
                    PublishPic v = this.papaDal.PublishPics.SingleOrDefault
                    (g => g.TopPagePlace!= existingPlace && g.TopPagePlace == newPlace);
                    result = v == null;
                    break;
                case "song":
                    Song o = this.papaDal.Songs.SingleOrDefault
                    (g => g.SongPlace != existingPlace && g.SongPlace == newPlace);
                    result = o == null;
                    break;
                case "staffPic":
                    Staff b = this.papaDal.Staffs.SingleOrDefault
                    (g => g.StaffPlace != existingPlace && g.StaffPlace == newPlace);
                    result = b == null;
                    break;
                case "controlRoom":
                case "recordingRoom":
                case "studioLookAroundRoom":
                case "studioWhoPlaysHere":
                    StudioRoomPic f = this.papaDal.StudioRoomPics.SingleOrDefault
                      (g => g.RoomName == type && g.PicPlace != existingPlace
                      && g.PicPlace == newPlace);
                    result = f == null;
                    break;
                case "willComePic":
                    WillComePic m = this.papaDal.WillComePics.SingleOrDefault
                    (g => g.PicPlace != existingPlace && g.PicPlace == newPlace);
                    result = m == null;
                    break;
                default:
                    break;
            }

            return result;
        }

        public bool CheckExistingStaff()
        {
            var r = from st in this.papaDal.Staffs
                    where st.IsExistingStaff == 1
                    select st;
            return r.Count() == 4;
        }

        public bool CheckAvailableGalleriesPicturePlace(string type, string galleryID, int place)
        {
            bool result = false;

            if (place == 0)
            {
                return true;
            }

            if (galleryID == "" || galleryID == null || place < 0)
            {
                return result;
            }

            Gallery u = (Gallery)this.Get("gallery", galleryID);
            if (u == null)
            {
                return result;
            }

            switch (type)
            {
                case "allArtistsPic":
                    AllArtistPic v = this.papaDal.AllArtistPics.SingleOrDefault
                    (g => g.GalleryID == galleryID && g.PicPlace == place);
                    result = v == null;
                    break;
                case "publishPic":
                    PublishPic t = this.papaDal.PublishPics.SingleOrDefault
                    (g => g.GalleryID == galleryID && g.PicPlace == place);
                    result = t == null;
                    break;
                default:
                    break;
            }

            return result;
        }

        public bool CheckAvailableGalleriesPicturePlaceExcept(string type, string galleryID, int existingPlace, int newPlace)
        {
            bool result = false;

            if (newPlace == 0)
            {
                return true;
            }

            if (galleryID == "" || galleryID == null || existingPlace  < 0 || newPlace < 0)
            {
                return result;
            }

            Gallery u = (Gallery)this.Get("gallery", galleryID);
            if (u == null)
            {
                return result;
            }

            switch (type)
            {
                case "allArtistsPic":
                    AllArtistPic v = this.papaDal.AllArtistPics.SingleOrDefault
                    (g => g.GalleryID == galleryID && g.PicPlace != existingPlace && g.PicPlace == newPlace);
                    result = v == null;
                    break;
                case "publishPic":
                    PublishPic t = this.papaDal.PublishPics.SingleOrDefault
                    (g => g.GalleryID == galleryID && g.PicPlace != existingPlace && g.PicPlace == newPlace);
                    result = t == null;
                    break;
                default:
                    break;
            }

            return result;
        }

        public object GetGalleryByName(string type, string textHe, string textEn)
        {
            if (type == "" || type == null || 
                textEn == "" || textHe == null || 
                textHe == "" || textHe == null)
            {
                return null;
            }

            Gallery g = this.papaDal.Galleries.SingleOrDefault
           (h => (h.GalleryType == type) &&
            h.GalleryNameEn == textEn || h.GalleryNameEn == textHe);

            if (g == null)
            {
                return null;
            }
            return g;
        }

        public IEnumerable<Song> GetAllSongs(string productionID)
        {
            if (productionID == "" || productionID == null)
            {
                return null;
            }

            Production m = (Production)this.Get("production", productionID);
            if (m == null)
            {
                return null;
            }

            var result = from so in this.papaDal.Songs
                         where so.ProID == m.ProID
                         orderby so.SongPlace ascending
                         select so;
            return result;
        }

        public IEnumerable<AllArtistPic> GetAllAllArtistsPictures(string galleryID)
        {
            if (galleryID == "" || galleryID == null)
            {
                return null;
            }

            Gallery m = (Gallery)this.Get("gallery", galleryID);
            if (m == null)
            {
                return null;
            }

            var result = from so in this.papaDal.AllArtistPics
                         where so.GalleryID == m.GalleryID
                         orderby so.PicPlace ascending
                         select so;
            return result;
        }

        public IEnumerable<PublishPic> GetAllPublishPictures(string galleryID)
        {
            if (galleryID == "" || galleryID == null)
            {
                return null;
            }

            Gallery m = (Gallery)this.Get("gallery", galleryID);
            if (m == null)
            {
                return null;
            }

            var result = from so in this.papaDal.PublishPics
                         where so.GalleryID == m.GalleryID
                         orderby so.PicPlace ascending
                         select so;
            return result;
        }

        public void RemoveAllGalleryPicsByID(string type, string galleryID)
        {
            if (type == "" || type == null ||
                galleryID == "" || galleryID == null)
            {
                return;
            }

            Gallery g = (Gallery)this.Get("gallery", galleryID);
            if (g == null)
            {
                return;
            }

            if (g.GalleryType == "publishGallery")
            {
                for (int i = 0; i < g.PublishPics.Count; i++)
                {
                    this.Remove("publishPic", g.PublishPics[i].PicID);
                }
            }

            if (g.GalleryType == "allArtistsGallery")
            {
                for (int i = 0; i < g.AllArtistPics.Count; i++)
                {
                    this.Remove("allArtistsPic", g.AllArtistPics[i].PicID);
                }
            }
        }

        public void RemoveAllSongs(string productionID)
        {
            if (productionID == "")
            {
                return;
            }

            Production m = (Production)this.Get("production", productionID);
            if (m == null)
            {
                return;
            }

            IEnumerable<Song> f = this.GetAllSongs(m.ProID);

            for (int i = 0; i < f.Count(); i++)
            {
                this.Remove("song", (f.ElementAt(i)).SongID);
            }
        }

        public void AddSong(Song obj, string productionID)
        {
            if (obj == null || productionID == "" || productionID == null)
            {
                return;
            }

            Production f = (Production)this.Get("production", productionID);
            if (f == null)
            {
                return;
            }

            Song l = (Song)this.Get("song", obj.SongID);
            if (l != null)
            {
                return;
            }

            this.papaDal.Songs.InsertOnSubmit(obj);
            this.papaDal.SubmitChanges();
        }

        public object GetAll(string type)
        {
            object result = null;

            if (type == "" || type == null)
            {
                return result;
            }

            switch (type)
            {
                case "admin":
                    result = this.papaDal.AdminUsers;
                    break;
                case "allArtistsPic":
                    result = this.papaDal.AllArtistPics;
                    break;
                case "complexAbout":
                    result = this.papaDal.ComplexAbouts;
                    break;
                case "complexPic":
                    result = this.papaDal.ComplexRoomPics;
                    break;
                case "designPic":
                    result = this.papaDal.DesignPics;
                    break;
                case "designText":
                    result = this.papaDal.DesignTexts;
                    break;
                case "allArtistsGallery":
                    var t = from ga in this.papaDal.Galleries
                            where ga.GalleryType == type
                            select ga;
                    result = t;
                    break;
                case "publishGallery":
                    var n = from ga in this.papaDal.Galleries
                            where ga.GalleryType == type
                            select ga;
                    result = n;
                    break;
                case "headerPic":
                    result = this.papaDal.HeaderPics;
                    break;
                case "homeContactAbout":
                    result = this.papaDal.HomeContactAbouts;
                    break;
                case "lastNews":
                    result = this.papaDal.LastNews;
                    break;
                case "lastRecordsPic":
                    result = this.papaDal.LastRecordPics;
                    break;
                case "log":
                    result = this.papaDal.Logs;
                    break;
                case "mainContactAbout":
                    result = this.papaDal.MainContactAbouts;
                    break;
                case "mainContactAboutPic":
                    result = this.papaDal.MainContactAboutPics;
                    break;
                case "production":
                    result = this.papaDal.Productions;
                    break;
                case "prPic":
                    result = this.papaDal.PrPics;
                    break;
                case "prText":
                    result = this.papaDal.PrTexts;
                    break;
                case "publishPic":
                    result = this.papaDal.PublishPics;
                    break;
                case "song":
                    result = this.papaDal.Songs;
                    break;
                case "staffPic":
                    result = this.papaDal.Staffs;
                    break;
                case "studioAbout":
                    result = this.papaDal.StudioAbouts;
                    break;
                case "studioPic":
                    result = this.papaDal.StudioRoomPics;
                    break;
                case "willComePic":
                    result = this.papaDal.WillComePics;
                    break;
                default:
                    break;
            }
            return result;
        }

        public AdminUser GetAdminUser(string userID, string password)
        {
            if (userID == "" || userID == null || password == "" || password == null)
            {
                return null;
            }

            AdminUser m = this.papaDal.AdminUsers.SingleOrDefault
                          (x => x.UserID.Trim() == userID && x.Password.Trim() == password);
            return m;
        }

        public AdminUser GetAdminUserByUserID(string userID)
        {
            if (userID == "" || userID == null)
            {
                return null;
            }

            AdminUser m = this.papaDal.AdminUsers.SingleOrDefault(x => x.UserID == userID);
            return m;
        }

        public AdminUser GetAdminUserByPassword(string password)
        {
            if (password == "" || password == null)
            {
                return null;
            }

            AdminUser m = this.papaDal.AdminUsers.SingleOrDefault(x => x.Password == password);
            return m;
        }

        public AdminUser GetAdminByUserIDExcept(string existUserID, string userID)
        {
            if (existUserID == "" || existUserID == null || userID == "" || userID == null)
            {
                return null;
            }

            AdminUser m = this.papaDal.AdminUsers.SingleOrDefault
            (x => x.UserID != existUserID && x.UserID == userID);
            return m;
        }

        public AdminUser GetAdminUserByPasswordExcept(string existPassword, string password)
        {
            if (existPassword == "" || existPassword == null || password == "" || password == null)
            {
                return null;
            }

            AdminUser m = this.papaDal.AdminUsers.SingleOrDefault
            (x => x.Password != existPassword && x.Password == password);
            return m;
        }

        public bool CheckPlacesStatus(string type)
        {
            if (type == "" || type == null)
            {
                return false;
            }

            bool result = false;

            switch (type)
            {
                case "roomA":
                case "roomB":
                case "roomC":
                case "complexLookAroundRoom":
                    var a = from pic in this.papaDal.ComplexRoomPics
                            where pic.RoomName == type && pic.Active == 1 && pic.PicPlace != 0
                            select pic;
                    result = a.Count() != 3;
                    break;
                case "complexWhoPlaysHere":
                    var j = from pic in this.papaDal.StudioRoomPics
                            where pic.RoomName == type && pic.Active == 1 && pic.PicPlace != 0
                            select pic;
                    result = j.Count() != 12;
                    break;
                case "lastRecordsPic":
                    var e = from pic in this.papaDal.LastRecordPics
                            where pic.Active == 1 && pic.PicPlace != 0
                                 select pic;
                    result = e.Count() != 9;
                    break;
                case "controlRoom":
                case "recordingRoom":
                case "studioLookAroundRoom":
                    var h = from pic in this.papaDal.StudioRoomPics
                            where pic.RoomName == type && pic.Active == 1 && pic.PicPlace != 0
                            select pic;
                    result = h.Count() != 3;
                    break;
                case "studioWhoPlaysHere":
                    var u = from pic in this.papaDal.StudioRoomPics
                            where pic.RoomName == type && pic.Active == 1 && pic.PicPlace != 0
                            select pic;
                    result = u.Count() != 12;
                    break;
                case "willComePic":
                    var i = from pic in this.papaDal.WillComePics
                            where pic.Active == 1 && pic.PicPlace != 0
                            select pic;
                    result = i.Count() != 3;
                    break;
                case "publishPicTop":
                    var o = from pic in this.papaDal.PublishPics
                            where pic.Active == 1 && pic.TopPagePlace != 0
                            select pic;
                    result = o.Count() != 6;
                    break;
                default:
                    break;
            }

            return result;
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
                case "allArtists":
                    result = this.papaDal.AllArtistPics.Count();
                    break;
                case "complexAbout":
                    result = this.papaDal.ComplexAbouts.Count();
                    break;
                case "complexPic":
                    result = this.papaDal.ComplexRoomPics.Count();
                    break;
                case "designPic":
                    result = this.papaDal.DesignPics.Count();
                    break;
                case "designText":
                    result = this.papaDal.DesignTexts.Count();
                    break;
                case "gallery":
                    result = this.papaDal.Galleries.Count();
                    break;
                case "headerPic":
                    result = this.papaDal.HeaderPics.Count();
                    break;
                case "homeContactAbout":
                    result = this.papaDal.HomeContactAbouts.Count();
                    break;
                case "lastNews":
                    result = this.papaDal.LastNews.Count();
                    break;
                case "lastRecords":
                    result = this.papaDal.LastRecordPics.Count();
                    break;
                case "log":
                    result = this.papaDal.Logs.Count();
                    break;
                case "mainContactAbout":
                    result = this.papaDal.MainContactAbouts.Count();
                    break;
                case "mainContactAboutPic":
                    result = this.papaDal.MainContactAboutPics.Count();
                    break;
                case "production":
                    result = this.papaDal.Productions.Count();
                    break;
                case "prPic":
                    result = this.papaDal.PrPics.Count();
                    break;
                case "prText":
                    result = this.papaDal.PrTexts.Count();
                    break;
                case "publish":
                    result = this.papaDal.PublishPics.Count();
                    break;
                case "song":
                    result = this.papaDal.Songs.Count();
                    break;
                case "staff":
                    result = this.papaDal.Staffs.Count();
                    break;
                case "studioAbout":
                    result = this.papaDal.StudioAbouts.Count();
                    break;
                case "studioPic":
                    result = this.papaDal.StudioRoomPics.Count();
                    break;
                case "willComePic":
                    result = this.papaDal.WillComePics.Count();
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
                    AdminUser a = null;
                    while ((a = (AdminUser)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "allArtistsPic":
                    AllArtistPic b = null;
                    while ((b = (AllArtistPic)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "complexAbout":
                    ComplexAbout c = null;
                    while ((c = (ComplexAbout)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "complexPic":
                    ComplexRoomPic d = null;
                    while ((d = (ComplexRoomPic)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "designPic":
                    DesignPic e = null;
                    while ((e = (DesignPic)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "designText":
                    DesignText f = null;
                    while ((f = (DesignText)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "gallery":
                    Gallery g = null;
                    while ((g = (Gallery)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "headerPic":
                    HeaderPic h = null;
                    while ((h = (HeaderPic)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "homeContactAbout":
                    HomeContactAbout i = null;
                    while ((i = (HomeContactAbout)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "lastNews":
                    LastNew j = null;
                    while ((j = (LastNew)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "lastRecordsPic":
                    LastRecordPic k = null;
                    while ((k = (LastRecordPic)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "log":
                    Log l = null;
                    while ((l = (Log)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "mainContactAbout":
                    MainContactAbout m = null;
                    while ((m = (MainContactAbout)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "mainContactAboutPic":
                    MainContactAboutPic o = null;
                    while ((o = (MainContactAboutPic)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "production":
                    Production p = null;
                    while ((p = (Production)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "prPic":
                    PrPic q = null;
                    while ((q = (PrPic)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "prText":
                    PrText r = null;
                    while ((r = (PrText)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "publishPic":
                    PublishPic s = null;
                    while ((s = (PublishPic)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "song":
                    Song t = null;
                    while ((t = (Song)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "staffPic":
                    Staff u = null;
                    while ((u = (Staff)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "studioAbout":
                    StudioAbout v = null;
                    while ((v = (StudioAbout)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "studioPic":
                    StudioRoomPic w = null;
                    while ((w = (StudioRoomPic)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                case "willComePic":
                    WillComePic x = null;
                    while ((x = (WillComePic)this.Get(type, result.ToString())) != null)
                    {
                        result += 1;
                    }
                    break;
                default:
                    break;
            }

            return result.ToString();
        }

        public void Add(string type, object obj)
        {
            if (type == "" || obj == null)
            {
                return;
            }

            switch (type)
            {
                case "admin":
                    AdminUser a = (AdminUser)obj;
                    if (this.Get(type, a.UserID) == null)
                    {
                        this.papaDal.AdminUsers.InsertOnSubmit(a);
                    }
                    break;
                case "allArtistsPic":
                    AllArtistPic b = (AllArtistPic)obj;
                    if (this.Get(type, b.PicID) == null)
                    {
                        this.papaDal.AllArtistPics.InsertOnSubmit(b);
                    }
                    break;
                case "complexAbout":
                    ComplexAbout c = (ComplexAbout)obj;
                    if (this.Get(type, c.ComplexAboutID) == null)
                    {
                        this.papaDal.ComplexAbouts.InsertOnSubmit(c);
                    }
                    break;
                case "complexPic":
                    ComplexRoomPic d = (ComplexRoomPic)obj;
                    if (this.Get(type, d.PicID) == null)
                    {
                        this.papaDal.ComplexRoomPics.InsertOnSubmit(d);
                    }
                    break;
                case "designPic":
                    DesignPic e = (DesignPic)obj;
                    if (this.Get(type, e.PicID) == null)
                    {
                        this.papaDal.DesignPics.InsertOnSubmit(e);
                    }
                    break;
                case "designText":
                    DesignText f = (DesignText)obj;
                    if (this.Get(type, f.DesignTextID) == null)
                    {
                        this.papaDal.DesignTexts.InsertOnSubmit(f);
                    }
                    break;
                case "gallery":
                    Gallery g = (Gallery)obj;
                    if (this.Get(type, g.GalleryID) == null)
                    {
                        this.papaDal.Galleries.InsertOnSubmit(g);
                    }
                    break;
                case "headerPic":
                    HeaderPic h = (HeaderPic)obj;
                    if (this.Get(type, h.PicID) == null)
                    {
                        this.papaDal.HeaderPics.InsertOnSubmit(h);
                    }
                    break;
                case "homeContactAbout":
                    HomeContactAbout i = (HomeContactAbout)obj;
                    if (this.Get(type, i.HomeContactAboutID) == null)
                    {
                        this.papaDal.HomeContactAbouts.InsertOnSubmit(i);
                    }
                    break;
                case "lastNews":
                    LastNew j = (LastNew)obj;
                    if (this.Get(type, j.NewsID) == null)
                    {
                        this.papaDal.LastNews.InsertOnSubmit(j);
                    }
                    break;
                case "lastRecordsPic":
                    LastRecordPic k = (LastRecordPic)obj;
                    if (this.Get(type, k.PicID) == null)
                    {
                        this.papaDal.LastRecordPics.InsertOnSubmit(k);
                    }
                    break;
                case "log":
                    Log l = (Log)obj;
                    if (this.Get(type, l.LogID) == null)
                    {
                        this.papaDal.Logs.InsertOnSubmit(l);
                    }
                    break;
                case "mainContactAbout":
                    MainContactAbout m = (MainContactAbout)obj;
                    if (this.Get(type, m.MainContactAboutID) == null)
                    {
                        this.papaDal.MainContactAbouts.InsertOnSubmit(m);
                    }
                    break;
                case "mainContactAboutPic":
                    MainContactAboutPic o = (MainContactAboutPic)obj;
                    if (this.Get(type, o.PicID) == null)
                    {
                        this.papaDal.MainContactAboutPics.InsertOnSubmit(o);
                    }
                    break;
                case "production":
                    Production p = (Production)obj;
                    if (this.Get(type, p.ProID) == null)
                    {
                        this.papaDal.Productions.InsertOnSubmit(p);
                    }
                    break;
                case "prPic":
                    PrPic q = (PrPic)obj;
                    if (this.Get(type, q.PicID) == null)
                    {
                        this.papaDal.PrPics.InsertOnSubmit(q);
                    }
                    break;
                case "prText":
                    PrText r = (PrText)obj;
                    if (this.Get(type, r.PrTextID) == null)
                    {
                        this.papaDal.PrTexts.InsertOnSubmit(r);
                    }
                    break;
                case "publishPic":
                    PublishPic s = (PublishPic)obj;
                    if (this.Get(type, s.GalleryID) == null)
                    {
                        this.papaDal.PublishPics.InsertOnSubmit(s);
                    }
                    break;
                case "song":
                    Song t = (Song)obj;
                    if (this.Get(type, t.SongID) == null)
                    {
                        this.papaDal.Songs.InsertOnSubmit(t);
                    }
                    break;
                case "staffPic":
                    Staff u = (Staff)obj;
                    if (this.Get(type, u.StaffID) == null)
                    {
                        this.papaDal.Staffs.InsertOnSubmit(u);
                    }
                    break;
                case "studioAbout":
                    StudioAbout v = (StudioAbout)obj;
                    if (this.Get(type, v.StudioAboutID) == null)
                    {
                        this.papaDal.StudioAbouts.InsertOnSubmit(v);
                    }
                    break;
                case "studioPic":
                    StudioRoomPic w = (StudioRoomPic)obj;
                    if (this.Get(type, w.PicID) == null)
                    {
                        this.papaDal.StudioRoomPics.InsertOnSubmit(w);
                    }
                    break;
                case "willComePic":
                    WillComePic x = (WillComePic)obj;
                    if (this.Get(type, x.PicID) == null)
                    {
                        this.papaDal.WillComePics.InsertOnSubmit(x);
                    }
                    break;
                default:
                    break;
            }

            this.papaDal.SubmitChanges();
        }

        public void Remove(string type, string ID)
        {
            if (type == "" || type == null || ID == "" || ID == null)
            {
                return;
            }

            switch (type)
            {
                case "admin":
                    AdminUser a = null;
                    if ((a = (AdminUser)this.Get(type, ID)) != null)
                    {
                        this.papaDal.AdminUsers.DeleteOnSubmit(a);
                    }
                    break;
                case "allArtistsPic":
                    AllArtistPic b = null;
                    if ((b = (AllArtistPic)this.Get(type, ID)) != null)
                    {
                        if (File.Exists(b.PicFullPath))
                        {
                            File.Delete(b.PicFullPath);
                        }

                        this.papaDal.AllArtistPics.DeleteOnSubmit(b);
                    }
                    break;
                case "complexAbout":
                    ComplexAbout c = null;
                    if ((c = (ComplexAbout)this.Get(type, ID)) != null)
                    {
                        this.papaDal.ComplexAbouts.DeleteOnSubmit(c);
                    }
                    break;
                case "complexPic":
                    ComplexRoomPic d = null;
                    if ((d = (ComplexRoomPic)this.Get(type, ID)) != null)
                    {
                        if (File.Exists(d.PicColorFullPath))
                        {
                            File.Delete(d.PicColorFullPath);
                        }

                        if (File.Exists(d.PicBWFullPath))
                        {
                            File.Delete(d.PicBWFullPath);
                        }

                        this.papaDal.ComplexRoomPics.DeleteOnSubmit(d);
                    }
                    break;
                case "designPic":
                    DesignPic e = null;
                    if ((e = (DesignPic)this.Get(type, ID)) != null)
                    {
                        if (File.Exists(e.PicFullPath))
                        {
                            File.Delete(e.PicFullPath);
                        }

                        this.papaDal.DesignPics.DeleteOnSubmit(e);
                    }
                    break;
                case "designText":
                    DesignText f = null;
                    if ((f = (DesignText)this.Get(type, ID)) != null)
                    {
                        this.papaDal.DesignTexts.DeleteOnSubmit(f);
                    }
                    break;
                case "gallery":
                    Gallery g = null;
                    if ((g = (Gallery)this.Get(type, ID)) != null)
                    {
                        this.RemoveAllGalleryPicsByID(g.GalleryType, ID);
                        this.papaDal.Galleries.DeleteOnSubmit(g);
                    }
                    break;
                case "headerPic":
                    HeaderPic h = null;
                    if ((h = (HeaderPic)this.Get(type, ID)) != null)
                    {
                        if (File.Exists(h.PicFullPath))
                        {
                            File.Delete(h.PicFullPath);
                        }

                        this.papaDal.HeaderPics.DeleteOnSubmit(h);
                    }
                    break;
                case "homeContactAbout":
                    HomeContactAbout i = null;
                    if ((i = (HomeContactAbout)this.Get(type, ID)) != null)
                    {
                        this.papaDal.HomeContactAbouts.DeleteOnSubmit(i);
                    }
                    break;
                case "lastNews":
                    LastNew j = null;
                    if ((j = (LastNew)this.Get(type, ID)) != null)
                    {
                        this.papaDal.LastNews.DeleteOnSubmit(j);
                    }
                    break;
                case "lastRecordsPic":
                    LastRecordPic k = null;
                    if ((k = (LastRecordPic)this.Get(type, ID)) != null)
                    {
                        if (File.Exists(k.PicColorFullPath))
                        {
                            File.Delete(k.PicColorFullPath);
                        }

                        if (File.Exists(k.PicBWFullPath))
                        {
                            File.Exists(k.PicBWFullPath);
                        }

                        this.papaDal.LastRecordPics.DeleteOnSubmit(k);
                    }
                    break;
                case "log":
                    Log l = null;
                    if ((l = (Log)this.Get(type, ID)) != null)
                    {
                        this.papaDal.Logs.DeleteOnSubmit(l);
                    }
                    break;
                case "mainContactAbout":
                    MainContactAbout m = null;
                    if ((m = (MainContactAbout)this.Get(type, ID)) != null)
                    {
                        this.papaDal.MainContactAbouts.DeleteOnSubmit(m);
                    }
                    break;
                case "mainContactAboutPic":
                    MainContactAboutPic n = null;
                    if ((n = (MainContactAboutPic)this.Get(type, ID)) != null)
                    {
                        if (File.Exists(n.PicFullPath))
                        {
                            File.Delete(n.PicFullPath);
                        }

                        this.papaDal.MainContactAboutPics.DeleteOnSubmit(n);
                    }
                    break;
                case "production":
                    Production p = null;
                    if ((p = (Production)this.Get(type, ID)) != null)
                    {
                        if (File.Exists(p.PicBWFullPath))
                        {
                            File.Delete(p.PicBWFullPath);
                        }

                        if (File.Exists(p.PicColorFullPath))
                        {
                            File.Delete(p.PicColorFullPath);
                        }

                        if (File.Exists(p.PicMainFullPath))
                        {
                            File.Delete(p.PicMainFullPath);
                        }

                        this.RemoveAllSongs(ID);
                        this.papaDal.Productions.DeleteOnSubmit(p);
                    }
                    break;
                case "prPic":
                    PrPic q = null;
                    if ((q = (PrPic)this.Get(type, ID)) != null)
                    {
                        if (File.Exists(q.PicFullPath))
                        {
                            File.Delete(q.PicFullPath);
                        }

                        this.papaDal.PrPics.DeleteOnSubmit(q);
                    }
                    break;
                case "prText":
                    PrText r = null;
                    if ((r = (PrText)this.Get(type, ID)) != null)
                    {
                        this.papaDal.PrTexts.DeleteOnSubmit(r);
                    }
                    break;
                case "publishPic":
                    PublishPic s = null;
                    if ((s = (PublishPic)this.Get(type, ID)) != null)
                    {
                        if (File.Exists(s.PicFullPath))
                        {
                            File.Delete(s.PicFullPath);
                        }

                        this.papaDal.PublishPics.DeleteOnSubmit(s);
                    }
                    break;
                case "song":
                    Song t = null;
                    if ((t = (Song)this.Get(type, ID)) != null)
                    {
                        this.papaDal.Songs.DeleteOnSubmit(t);
                    }
                    break;
                case "staffPic":
                    Staff v = null;
                    if ((v = (Staff)this.Get(type, ID)) != null)
                    {
                        if (File.Exists(v.PicFullPath))
                        {
                            File.Delete(v.PicFullPath);
                        }

                        this.papaDal.Staffs.DeleteOnSubmit(v);
                    }
                    break;
                case "studioAbout":
                    StudioAbout w = null;
                    if ((w = (StudioAbout)this.Get(type, ID)) != null)
                    {
                        this.papaDal.StudioAbouts.DeleteOnSubmit(w);
                    }
                    break;
                case "studioPic":
                    StudioRoomPic x = null;
                    if ((x = (StudioRoomPic)this.Get(type, ID)) != null)
                    {
                        if (File.Exists(x.PicFullBWPath))
                        {
                            File.Delete(x.PicFullBWPath);
                        }

                        if (File.Exists(x.PicFullColorPath))
                        {
                            File.Delete(x.PicFullColorPath);
                        }

                        this.papaDal.StudioRoomPics.DeleteOnSubmit(x);
                    }
                    break;
                case "willComePic":
                    WillComePic z = null;
                    if ((z = (WillComePic)this.Get(type, ID)) != null)
                    {
                        if (File.Exists(z.PicBWFullPath))
                        {
                            File.Delete(z.PicBWFullPath);
                        }

                        if (File.Exists(z.PicColorFullPath))
                        {
                            File.Delete(z.PicColorFullPath);
                        }

                        this.papaDal.WillComePics.DeleteOnSubmit(z);
                    }
                    break;
                default:
                    break;
            }

            try
            {
                this.papaDal.SubmitChanges();
            }
            catch (Exception)
            {
                switch (type)
                {
                    case "production":
                        Production k = null;
                        if ((k = (Production)this.Get(type, ID)) != null)
                        {
                            if (File.Exists(k.PicBWFullPath))
                            {
                                File.Delete(k.PicBWFullPath);
                            }

                            if (File.Exists(k.PicColorFullPath))
                            {
                                File.Delete(k.PicColorFullPath);
                            }

                            if (File.Exists(k.PicMainFullPath))
                            {
                                File.Delete(k.PicMainFullPath);
                            }

                            this.RemoveAllSongs(ID);
                            this.papaDal.Productions.DeleteOnSubmit(k);
                        }
                        break;
                    case "publishPic":
                        PublishPic s = null;
                        if ((s = (PublishPic)this.Get(type, ID)) != null)
                        {
                            if (File.Exists(s.PicFullPath))
                            {
                                File.Delete(s.PicFullPath);
                            }

                            this.papaDal.PublishPics.DeleteOnSubmit(s);
                        }
                        break;
                    case "allArtistsPic":
                        AllArtistPic b = null;
                        if ((b = (AllArtistPic)this.Get(type, ID)) != null)
                        {
                            if (File.Exists(b.PicFullPath))
                            {
                                File.Delete(b.PicFullPath);
                            }

                            this.papaDal.AllArtistPics.DeleteOnSubmit(b);
                        }
                        break;
                }
                this.papaDal.SubmitChanges();
            }
        }

        public void Disable(string type, string ID)
        {
            if (type == "" || type == null || ID == "" || ID == null)
            {
                return;
            }

            switch (type)
            {
                case "admin":
                    AdminUser ad = null;
                    if ((ad = (AdminUser)this.Get(type, ID)) != null)
                    {
                        if (ad.Active == 1)
                        {
                            ad.Active = 2;
                            ad.spActive = "Disable";
                        }
                    }
                    break;
                case "allArtistsPic":
                    AllArtistPic b = null;
                    if ((b = (AllArtistPic)this.Get(type, ID)) != null)
                    {
                        if (b.Active == 1)
                        {
                            b.Active = 2;
                            b.spActive = "Disable";
                            b.PicPlace = 0;
                        }
                    }
                    break;
                case "complexPic":
                    ComplexRoomPic c = null;
                    if ((c = (ComplexRoomPic)this.Get(type, ID)) != null)
                    {
                        if (c.Active == 1)
                        {
                            c.Active = 2;
                            c.spActive = "Disable";
                            c.PicPlace = 0;
                        }
                    }
                    break;
                case "designPic":
                    DesignPic d = null;
                    if ((d = (DesignPic)this.Get(type, ID)) != null)
                    {
                        if (d.Active == 1)
                        {
                            d.Active = 2;
                            d.spActive = "Disable";
                        }
                    }
                    break;
                case "headerPic":
                    HeaderPic e = null;
                    if ((e = (HeaderPic)this.Get(type, ID)) != null)
                    {
                        if (e.Active == 1)
                        {
                            e.Active = 2;
                            e.spActive = "Disable";
                            e.PicPlace = 0;
                        }
                    }
                    break;
                case "lastRecordsPic":
                    LastRecordPic f = null;
                    if ((f = (LastRecordPic)this.Get(type, ID)) != null)
                    {
                        if (f.Active == 1)
                        {
                            f.Active = 2;
                            f.spActive = "Disable";
                            f.PicPlace = 0;
                        }
                    }
                    break;
                case "mainContactAboutPic":
                    MainContactAboutPic g = null;
                    if ((g = (MainContactAboutPic)this.Get(type, ID)) != null)
                    {
                        if (g.Active == 1)
                        {
                            g.Active = 2;
                            g.spActive = "Disable";
                        }
                    }
                    break;
                case "production":
                    Production h = null;
                    if ((h = (Production)this.Get(type, ID)) != null)
                    {
                        if (h.Active == 1)
                        {
                            h.Active = 2;
                            h.spActive = "Disable";
                            h.ProPlace = 0;
                            this.DisableAllSongsByProductionID(h.ProID);
                        }
                    }
                    break;
                case "prPic":
                    PrPic i = null;
                    if ((i = (PrPic)this.Get(type, ID)) != null)
                    {
                        if (i.Active == 1)
                        {
                            i.Active = 2;
                            i.spActive = "Disable";
                        }
                    }
                    break;
                case "publishPic":
                    PublishPic j = null;
                    if ((j = (PublishPic)this.Get(type, ID)) != null)
                    {
                        if (j.Active == 1)
                        {
                            j.Active = 2;
                            j.spActive = "Disable";
                            j.PicPlace = 0;
                            j.TopPagePlace = 0;
                        }
                    }
                    break;
                case "song":
                    Song k = null;
                    if ((k = (Song)this.Get(type, ID)) != null)
                    {
                        if (k.Active == 1)
                        {
                            k.Active = 2;
                            k.spActive = "Disable";
                            k.SongPlace = 0;
                        }
                    }
                    break;
                case "staffPic":
                    Staff l = null;
                    if ((l = (Staff)this.Get(type, ID)) != null)
                    {
                        if (l.Active == 1 && l.IsExistingStaff == 2)
                        {
                            l.Active = 2;
                            l.spActive = "Disable";
                            l.StaffPlace = 0;
                        }
                    }
                    break;
                case "studioPic":
                    StudioRoomPic m = null;
                    if ((m = (StudioRoomPic)this.Get(type, ID)) != null)
                    {
                        if (m.Active == 1)
                        {
                            m.Active = 2;
                            m.spActive = "Disable";
                            m.PicPlace = 0;
                        }
                    }
                    break;
                case "willComePic":
                    WillComePic v = null;
                    if ((v = (WillComePic)this.Get(type, ID)) != null)
                    {
                        if (v.Active == 1)
                        {
                            v.Active = 2;
                            v.spActive = "Disable";
                            v.PicPlace = 0;
                        }
                    }
                    break;
                default:
                    break;
            }

            this.papaDal.SubmitChanges();
        }

        public void DisableAllSongsByProductionID(string productionID)
        {
            if (productionID == "" || productionID == null)
            {
                return;
            }

            Production p = (Production)this.Get("production", productionID);
            if (p == null)
            {
                return;
            }

            foreach (Song m in p.Songs)
            {
                this.Disable("song", m.SongID);
            }
        }

        public void Enable(string type, string ID)
        {
            if (type == "" || type == null || ID == "" || ID == null)
            {
                return;
            }

            switch (type)
            {
                case "admin":
                    AdminUser ad = null;
                    if ((ad = (AdminUser)this.Get(type, ID)) != null)
                    {
                        if (ad.Active == 2)
                        {
                            ad.Active = 1;
                            ad.spActive = "Enable";
                        }
                    }
                    break;
                case "allArtistsPic":
                    AllArtistPic b = null;
                    if ((b = (AllArtistPic)this.Get(type, ID)) != null)
                    {
                        if (b.Active == 2)
                        {
                            b.Active = 1;
                            b.spActive = "Enable";
                        }
                    }
                    break;
                case "complexPic":
                    ComplexRoomPic c = null;
                    if ((c = (ComplexRoomPic)this.Get(type, ID)) != null)
                    {
                        if (c.Active == 2)
                        {
                            c.Active = 1;
                            c.spActive = "Enable";
                        }
                    }
                    break;
                case "designPic":
                    DesignPic d = null;
                    if ((d = (DesignPic)this.Get(type, ID)) != null)
                    {
                        if (d.Active == 2)
                        {
                            d.Active = 1;
                            d.spActive = "Enable";
                        }
                    }
                    break;
                case "headerPic":
                    HeaderPic e = null;
                    if ((e = (HeaderPic)this.Get(type, ID)) != null)
                    {
                        if (e.Active == 2)
                        {
                            e.Active = 1;
                            e.spActive = "Enable";
                        }
                    }
                    break;
                case "lastRecordsPic":
                    LastRecordPic f = null;
                    if ((f = (LastRecordPic)this.Get(type, ID)) != null)
                    {
                        if (f.Active == 2)
                        {
                            f.Active = 1;
                            f.spActive = "Enable";
                        }
                    }
                    break;
                case "mainContactAboutPic":
                    MainContactAboutPic g = null;
                    if ((g = (MainContactAboutPic)this.Get(type, ID)) != null)
                    {
                        if (g.Active == 2)
                        {
                            g.Active = 1;
                            g.spActive = "Enable";
                        }
                    }
                    break;
                case "production":
                    Production h = null;
                    if ((h = (Production)this.Get(type, ID)) != null)
                    {
                        if (h.Active == 2)
                        {
                            h.Active = 1;
                            h.spActive = "Enable";
                        }
                    }
                    break;
                case "prPic":
                    PrPic i = null;
                    if ((i = (PrPic)this.Get(type, ID)) != null)
                    {
                        if (i.Active == 2)
                        {
                            i.Active = 1;
                            i.spActive = "Enable";
                        }
                    }
                    break;
                case "publishPic":
                    PublishPic j = null;
                    if ((j = (PublishPic)this.Get(type, ID)) != null)
                    {
                        if (j.Active == 2)
                        {
                            j.Active = 1;
                            j.spActive = "Enable";
                        }
                    }
                    break;
                case "song":
                    Song k = null;
                    if ((k = (Song)this.Get(type, ID)) != null)
                    {
                        if (k.Active == 2)
                        {
                            k.Active = 1;
                            k.spActive = "Enable";
                        }
                    }
                    break;
                case "staffPic":
                    Staff l = null;
                    if ((l = (Staff)this.Get(type, ID)) != null)
                    {
                        if (l.Active == 2 && l.IsExistingStaff == 2)
                        {
                            l.Active = 1;
                            l.spActive = "Enable";
                        }
                    }
                    break;
                case "studioPic":
                    StudioRoomPic m = null;
                    if ((m = (StudioRoomPic)this.Get(type, ID)) != null)
                    {
                        if (m.Active == 2)
                        {
                            m.Active = 1;
                            m.spActive = "Enable";
                        }
                    }
                    break;
                case "willComePic":
                    WillComePic v = null;
                    if ((v = (WillComePic)this.Get(type, ID)) != null)
                    {
                        if (v.Active == 2)
                        {
                            v.Active = 1;
                            v.spActive = "Enable";
                        }
                    }
                    break;
                default:
                    break;
            }

            this.papaDal.SubmitChanges();
        }

        private void DisableAll(string type)
        {
            if (type == "" || type == null)
            {
                return;
            }

            switch (type)
            {
                case "admin":
                    foreach (AdminUser p in (IEnumerable<AdminUser>)this.GetAll(type))
                    {
                        if (p.Active == 1)
                        {
                            p.Active = 2;
                            p.spActive = "Disable";
                        }
                    }
                    break;
                case "allArtistsPic":
                    foreach (AllArtistPic p in (IEnumerable<AllArtistPic>)this.GetAll(type))
                    {
                        if (p.Active == 1)
                        {
                            p.Active = 2;
                            p.spActive = "Disable";
                        }
                    }
                    break;
                case "complexPic":
                    foreach (ComplexRoomPic p in (IEnumerable<ComplexRoomPic>)this.GetAll(type))
                    {
                        if (p.Active == 1)
                        {
                            p.Active = 2;
                            p.spActive = "Disable";
                        }
                    }
                    break;
                case "designPic":
                    foreach (DesignPic p in (IEnumerable<DesignPic>)this.GetAll(type))
                    {
                        if (p.Active == 1)
                        {
                            p.Active = 2;
                            p.spActive = "Disable";
                        }
                    }
                    break;
                case "headerPic":
                    foreach (HeaderPic p in (IEnumerable<HeaderPic>)this.GetAll(type))
                    {
                        if (p.Active == 1)
                        {
                            p.Active = 2;
                            p.spActive = "Disable";
                        }
                    }
                    break;
                case "lastRecordsPic":
                    foreach (LastRecordPic p in (IEnumerable<LastRecordPic>)this.GetAll(type))
                    {
                        if (p.Active == 1)
                        {
                            p.Active = 2;
                            p.spActive = "Disable";
                        }
                    }
                    break;
                case "mainContactAboutPic":
                    foreach (MainContactAboutPic p in (IEnumerable<MainContactAboutPic>)this.GetAll(type))
                    {
                        if (p.Active == 1)
                        {
                            p.Active = 2;
                            p.spActive = "Disable";
                        }
                    }
                    break;
                case "production":
                    foreach (Production p in (IEnumerable<Production>)this.GetAll(type))
                    {
                        if (p.Active == 1)
                        {
                            p.Active = 2;
                            p.spActive = "Disable";
                        }
                    }
                    break;
                case "prPic":
                    foreach (PrPic p in (IEnumerable<PrPic>)this.GetAll(type))
                    {
                        if (p.Active == 1)
                        {
                            p.Active = 2;
                            p.spActive = "Disable";
                        }
                    }
                    break;
                case "publishPic":
                    foreach (PublishPic p in (IEnumerable<PublishPic>)this.GetAll(type))
                    {
                        if (p.Active == 1)
                        {
                            p.Active = 2;
                            p.spActive = "Disable";
                        }
                    }
                    break;
                case "song":
                    foreach (Song p in (IEnumerable<Song>)this.GetAll(type))
                    {
                        if (p.Active == 1)
                        {
                            p.Active = 2;
                            p.spActive = "Disable";
                        }
                    }
                    break;
                case "staffPic":
                    foreach (Staff p in (IEnumerable<Staff>)this.GetAll(type))
                    {
                        if (p.Active == 1)
                        {
                            p.Active = 2;
                            p.spActive = "Disable";
                        }
                    }
                    break;
                case "studioPic":
                    foreach (StudioRoomPic p in (IEnumerable<StudioRoomPic>)this.GetAll(type))
                    {
                        if (p.Active == 1)
                        {
                            p.Active = 2;
                            p.spActive = "Disable";
                        }
                    }
                    break;
                case "willComePic":
                    foreach (WillComePic p in (IEnumerable<WillComePic>)this.GetAll(type))
                    {
                        if (p.Active == 1)
                        {
                            p.Active = 2;
                            p.spActive = "Disable";
                        }
                    }
                    break;
                default:
                    break;
            }

            this.papaDal.SubmitChanges();
        }

        public bool CheckAllArtistsGalleryStatus(string galleryID)
        {
            if (galleryID == "" || galleryID == null)
            {
                return false;
            }

            Gallery g = (Gallery)this.Get("gallery", galleryID);
            if (g == null)
            {
                return false;
            }

            var f = from pic in g.AllArtistPics
                    where pic.Active == 1 && pic.PicPlace != 0
                    select pic;
            return f.Count() != 16;
        }

        public bool CheckPublishGalleryStatus(string galleryID)
        {
            if (galleryID == "" || galleryID == null)
            {
                return false;
            }

            Gallery g = (Gallery)this.Get("gallery", galleryID);
            if (g == null)
            {
                return false;
            }

            var f = from pic in g.PublishPics
                    where pic.Active == 1 && pic.PicPlace != 0
                    select pic;
            return f.Count() != 16;
        }

        public void Update(string type, object obj, DateTime time)
        {
            if (type == "" || type == null || obj == null || time == default(DateTime))
            {
                return;
            }

            switch (type)
            {
                case "admin":
                    AdminUser a1 = (AdminUser)obj;
                    AdminUser a2 = null;
                    if ((a2 = (AdminUser)this.Get(type, a1.LoginID)) != null)
                    {
                        a2.Active = a1.Active;
                        a2.CreateTime = a1.CreateTime;
                        a2.LastLogin = a1.LastLogin;
                        a2.LastUpdate = time;
                        a2.LoginID = a1.LoginID;
                        a2.Password = a1.Password;
                        a2.spActive = a1.spActive;
                        a2.spCreateTime = a1.spCreateTime;
                        a2.spLastLogin = a1.spLastLogin;
                        a2.spLastUpdate = time.ToShortDateString();
                        a2.UserID = a1.UserID;
                    }
                    break;
                case "allArtistsPic":
                    AllArtistPic b1 = (AllArtistPic)obj;
                    AllArtistPic b2 = null;
                    if ((b2 = (AllArtistPic)this.Get(type, b1.PicID)) != null)
                    {
                        b2.Active = b1.Active;
                        b2.GalleryID = b1.GalleryID;
                        b2.LastUpdate = time;
                        b2.PicID = b1.PicID;
                        b2.PicFullPath = b1.PicFullPath;
                        b2.PicRelativePath = b1.PicRelativePath;
                        b2.PicName = b1.PicName;
                        b2.PicPlace = b1.PicPlace;
                        b2.spActive = b1.spActive;
                        b2.spLastUpdate = time.ToShortDateString();
                        b2.spUploadTime = b1.spUploadTime;
                        b2.UploadTime = b1.UploadTime;
                    }
                    break;
                case "complexAbout":
                    ComplexAbout c1 = (ComplexAbout)obj;
                    ComplexAbout c2 = null;
                    if ((c2 = (ComplexAbout)this.Get(type, c1.ComplexAboutID)) != null)
                    {
                        c2.AboutEn = c1.AboutEn;
                        c2.AboutHe = c1.AboutHe;
                        c2.ComplexAboutID = c1.ComplexAboutID;
                        c2.LastUpdate = time;
                        c2.spLastUpdate = time.ToShortDateString();
                    }
                    break;
                case "complexPic":
                    ComplexRoomPic d1 = (ComplexRoomPic)obj;
                    ComplexRoomPic d2 = null;
                    if ((d2 = (ComplexRoomPic)this.Get(type, d1.PicID)) != null)
                    {
                        d2.Active = d1.Active;
                        d2.LastUpdate = time;
                        d2.PicBWFullPath = d1.PicBWFullPath;
                        d2.PicBWRelativePath = d1.PicBWRelativePath;
                        d2.PicColorFullPath = d1.PicColorFullPath;
                        d2.PicColorRelativePath = d1.PicColorRelativePath;
                        d2.PicID = d1.PicID;
                        d2.PicPlace = d1.PicPlace;
                        d2.RoomName = d1.RoomName;
                        d2.RoomTextEn = d1.RoomTextEn;
                        d2.RoomTextHe = d1.RoomTextHe;
                        d2.RoomTitleEn = d1.RoomTitleEn;
                        d2.RoomTitleHe = d1.RoomTitleHe;
                        d2.spActive = d1.spActive;
                        d2.spLastUpdate = time.ToShortDateString();
                        d2.spUploadTime = d1.spUploadTime;
                        d2.UploadTime = d1.UploadTime;
                    }
                    break;
                case "designPic":
                    DesignPic e1 = (DesignPic)obj;
                    DesignPic e2 = null;
                    if ((e2 = (DesignPic)this.Get(type, e1.PicID)) != null)
                    {
                        e2.Active = e1.Active;
                        e2.LastUpdate = time;
                        e2.PicID = e1.PicID;
                        e2.PicFullPath = e1.PicFullPath;
                        e2.PicRelativePath = e1.PicRelativePath;
                        e2.PicName = e1.PicName;
                        e2.spActive = e1.spActive;
                        e2.spLastUpdate = time.ToShortDateString();
                        e2.spUploadTime = e1.spUploadTime;
                        e2.UploadTime = e1.UploadTime;
                    }
                    break;
                case "designText":
                    DesignText f1 = (DesignText)obj;
                    DesignText f2 = null;
                    if ((f2 = (DesignText)this.Get(type, f1.DesignTextID)) != null)
                    {
                        f2.DesignTextID = f1.DesignTextID;
                        f2.LastUpdate = time;
                        f2.spLastUpdate = time.ToShortDateString();
                        f2.TextEn = f1.TextEn;
                        f2.TextHe = f1.TextHe;
                    }
                    break;
                case "gallery":
                    Gallery g1 = (Gallery)obj;
                    Gallery g2 = null;
                    if ((g2 = (Gallery)this.Get(type, g1.GalleryID)) != null)
                    {
                        g2.CreationTime = g1.CreationTime;
                        g2.GalleryID = g1.GalleryID;
                        g2.GalleryNameEn = g1.GalleryNameEn;
                        g2.GalleryNameHe = g1.GalleryNameHe;
                        g2.GalleryPlace = g1.GalleryPlace;
                        g2.GalleryType = g1.GalleryType;
                        g2.LastUpdate = time;
                        g2.spCreationTime = g1.spCreationTime;
                        g2.spLastUpdate = time.ToShortDateString();
                    }
                    break;
                case "headerPic":
                    HeaderPic h1 = (HeaderPic)obj;
                    HeaderPic h2 = null;
                    if ((h2 = (HeaderPic)this.Get(type, h1.PicID)) != null)
                    {
                        h2.Active = h1.Active;
                        h2.LastUpdate = time;
                        h2.PicID = h1.PicID;
                        h2.PicFullPath = h1.PicFullPath;
                        h2.PicRelativePath = h1.PicRelativePath;
                        h2.PicName = h1.PicName;
                        h2.PicPlace = h1.PicPlace;
                        h2.spActive = h1.spActive;
                        h2.spLastUpdate = time.ToShortDateString();
                        h2.spUploadTime = h1.spUploadTime;
                        h2.UploadTime = h1.UploadTime;
                    }
                    break;
                case "homeContactAbout":
                    HomeContactAbout i1 = (HomeContactAbout)obj;
                    HomeContactAbout i2 = null;
                    if ((i2 = (HomeContactAbout)this.Get(type, i1.HomeContactAboutID)) != null)
                    {
                        i2.AboutEn = i1.AboutEn;
                        i2.AboutHe = i1.AboutHe;
                        i2.ContactEn = i1.ContactEn;
                        i2.ContactHe = i1.ContactHe;
                        i2.HomeContactAboutID = i1.HomeContactAboutID;
                        i2.LastUpdate = time;
                        i2.spLastUpdate = time.ToShortDateString();
                    }
                    break;
                case "lastNews":
                    LastNew j1 = (LastNew)obj;
                    LastNew j2 = null;
                    if ((j2 = (LastNew)this.Get(type, j1.NewsID)) != null)
                    {
                        j2.LastUpdate = time;
                        j2.NewsEn = j1.NewsEn;
                        j2.NewsHe = j1.NewsHe;
                        j2.NewsID = j1.NewsID;
                        j2.NewsPlace = j1.NewsPlace;
                        j2.spLastUpdate = time.ToShortDateString();
                        j2.spUploadTime = j1.spUploadTime;
                        j2.UploadTime = j1.UploadTime;
                    }
                    break;
                case "lastRecordsPic":
                    LastRecordPic k1 = (LastRecordPic)obj;
                    LastRecordPic k2 = null;
                    if ((k2 = (LastRecordPic)this.Get(type, k1.PicID)) != null)
                    {
                        k2.Active = k1.Active;
                        k2.LastUpdate = time;
                        k2.PicID = k1.PicID;
                        k2.PicBWFullPath = k1.PicBWFullPath;
                        k2.PicBWRelativePath = k1.PicBWRelativePath;
                        k2.PicColorFullPath = k1.PicColorFullPath;
                        k2.PicColorRelativePath = k1.PicColorRelativePath;
                        k2.PicName = k1.PicName;
                        k2.PicPlace = k1.PicPlace;
                        k2.spActive = k1.spActive;
                        k2.spLastUpdate = time.ToShortDateString();
                        k2.spUploadTime = k1.spUploadTime;
                        k2.UploadTime = k1.UploadTime;
                    }
                    break;
                case "log":
                    Log l1 = (Log)obj;
                    Log l2 = null;
                    if ((l2 = (Log)this.Get(type, l1.LogID)) != null)
                    {
                        l2.LogDate = l1.LogDate;
                        l2.LogID = l1.LogID;
                        l2.LogMessage = l1.LogMessage;
                        l2.LogType = l1.LogType;
                        l2.spLogDate = l1.spLogDate;
                    }
                    break;
                case "mainContactAbout":
                    MainContactAbout o1 = (MainContactAbout)obj;
                    MainContactAbout o2 = null;
                    if ((o2 = (MainContactAbout)this.Get(type, o1.MainContactAboutID)) != null)
                    {
                        o2.AboutEn = o1.AboutEn;
                        o2.AboutHe = o1.AboutHe;
                        o2.ContactEn = o1.ContactEn;
                        o2.ContactHe = o1.ContactHe;
                        o2.LastUpdate = time;
                        o2.MainContactAboutID = o1.MainContactAboutID;
                        o2.spLastUpdate = time.ToShortDateString();
                    }
                    break;
                case "mainContactAboutPic":
                    MainContactAboutPic m1 = (MainContactAboutPic)obj;
                    MainContactAboutPic m2 = null;
                    if ((m2 = (MainContactAboutPic)this.Get(type, m1.PicID)) != null)
                    {
                        m2.Active = m1.Active;
                        m2.LastUpdate = time;
                        m2.PicID = m1.PicID;
                        m2.PicFullPath = m1.PicFullPath;
                        m2.PicRelativePath = m1.PicRelativePath;
                        m2.PicName = m1.PicName;
                        m2.spActive = m1.spActive;
                        m2.spLastUpdate = time.ToShortDateString();
                        m2.spUploadTime = m1.spUploadTime;
                        m2.UploadTime = m1.UploadTime;
                    }
                    break;
                case "production":
                    Production p1 = (Production)obj;
                    Production p2 = null;
                    if ((p2 = (Production)this.Get(type, p1.ProID)) != null)
                    {
                        p2.Active = p1.Active;
                        p2.ArtistNameEn = p1.ArtistNameEn;
                        p2.ArtistNameHe = p1.ArtistNameHe;
                        p2.ArtistTextEn = p1.ArtistTextEn;
                        p2.ArtistTextHe = p1.ArtistTextHe;
                        p2.LastUpdate = time;
                        p2.PicBWFullPath = p1.PicBWFullPath;
                        p2.PicBWRelativePath = p1.PicBWRelativePath;
                        p2.PicColorFullPath = p1.PicColorFullPath;
                        p2.PicColorRelativePath = p1.PicColorRelativePath;
                        p2.PicMainFullPath = p1.PicMainFullPath;
                        p2.PicMainRelativePath = p1.PicMainRelativePath;
                        p2.ProID = p1.ProID;
                        p2.ProPlace = p1.ProPlace;
                        p2.spActive = p1.spActive;
                        p2.spLastUpdate = time.ToShortDateString();
                        p2.spUploadTime = p1.spUploadTime;
                        p2.UploadTime = p1.UploadTime;
                    }
                    break;
                case "prPic":
                    PrPic q1 = (PrPic)obj;
                    PrPic q2 = null;
                    if ((q2 = (PrPic)this.Get(type, q1.PicID)) != null)
                    {
                        q2.Active = q1.Active;
                        q2.LastUpdate = time;
                        q2.PicID = q1.PicID;
                        q2.PicFullPath = q1.PicFullPath;
                        q2.PicRelativePath = q1.PicRelativePath;
                        q2.PicName = q1.PicName;
                        q2.spActive = q1.spActive;
                        q2.spLastUpdate = time.ToShortDateString();
                        q2.spUploadTime = q1.spUploadTime;
                        q2.UploadTime = q1.UploadTime;
                    }
                    break;
                case "prText":
                    PrText r1 = (PrText)obj;
                    PrText r2 = null;
                    if ((r2 = (PrText)this.Get(type, r1.PrTextID)) != null)
                    {
                        r2.LastUpdate = time;
                        r2.PrTextID = r1.PrTextID;
                        r2.spLastUpdate = time.ToShortDateString();
                        r2.TextEn = r1.TextEn;
                        r2.TextHe = r1.TextHe;
                    }
                    break;
                case "publishPic":
                    PublishPic s1 = (PublishPic)obj;
                    PublishPic s2 = null;
                    if ((s2 = (PublishPic)this.Get(type, s1.PicID)) != null)
                    {
                        s2.Active = s1.Active;
                        s2.GalleryID = s1.GalleryID;
                        s2.LastUpdate = time;
                        s2.PicID = s1.PicID;
                        s2.PicFullPath = s1.PicFullPath;
                        s2.PicRelativePath = s1.PicRelativePath;
                        s2.PicPlace = s1.PicPlace;
                        s2.spActive = s1.spActive;
                        s2.spLastUpdate = time.ToShortDateString();
                        s2.spUploadTime = s1.spUploadTime;
                        s2.TextEn = s1.TextEn;
                        s2.TextHe = s1.TextHe;
                        s2.TopPagePlace = s1.TopPagePlace;
                        s2.UploadTime = s1.UploadTime;
                    }
                    break;
                case "song":
                    Song t1 = (Song)obj;
                    Song t2 = null;
                    if ((t2 = (Song)this.Get(type, t1.SongID)) != null)
                    {
                        t2.Active = t1.Active;
                        t2.LastUpdate = time;
                        t2.ProID = t1.ProID;
                        t2.SongID = t1.SongID;
                        t2.SongNameEn = t1.SongNameEn;
                        t2.SongNameHe = t1.SongNameHe;
                        t2.SongPlace = t1.SongPlace;
                        t2.spActive = t1.spActive;
                        t2.spLastUpdate = time.ToShortDateString();
                        t2.spUploadTime = t1.spUploadTime;
                        t2.UploadTime = t1.UploadTime;
                        t2.YouTubePath = t1.YouTubePath;
                    }
                    break;
                case "staffPic":
                    Staff n1 = (Staff)obj;
                    Staff n2 = null;
                    if ((n2 = (Staff)this.Get(type, n1.StaffID)) != null)
                    {
                        n2.Active = n1.Active;
                        n2.IsExistingStaff = n1.IsExistingStaff;
                        n2.LastUpdate = time;
                        n2.spActive = n1.spActive;
                        n2.spLastUpdate = time.ToShortDateString();
                        n2.StaffID = n1.StaffID;
                        n2.PicFullPath = n1.PicFullPath;
                        n2.PicRelativePath = n1.PicRelativePath;
                        n2.StaffPlace = n1.StaffPlace;
                        n2.TextEn = n1.TextEn;
                        n2.TextHe = n1.TextHe;
                        n2.TitleEn = n1.TitleEn;
                        n2.TitleHe = n1.TitleHe;
                    }
                    break;
                case "studioAbout":
                    StudioAbout v1 = (StudioAbout)obj;
                    StudioAbout v2 = null;
                    if ((v2 = (StudioAbout)this.Get(type, v1.StudioAboutID)) != null)
                    {
                        v2.AboutEn = v1.AboutEn;
                        v2.AboutHe = v1.AboutHe;
                        v2.LastUpdate = time;
                        v2.spLastUpdate = time.ToShortDateString();
                        v2.StudioAboutID = v1.StudioAboutID;
                        v2.TechnicalEn = v1.TechnicalEn;
                        v2.TechnicalHe = v1.TechnicalHe;
                    }
                    break;
                case "studioPic":
                    StudioRoomPic z1 = (StudioRoomPic)obj;
                    StudioRoomPic z2 = null;
                    if ((z2 = (StudioRoomPic)this.Get(type, z1.PicID)) != null)
                    {
                        z2.Active = z1.Active;
                        z2.LastUpdate = time;
                        z2.PicFullBWPath = z1.PicFullBWPath;
                        z2.PicFullColorPath = z1.PicFullColorPath;
                        z2.PicRelativeBWPath = z1.PicRelativeBWPath;
                        z2.PicRelativeColorPath = z1.PicRelativeColorPath;
                        z2.PicName = z1.PicName;
                        z2.PicID = z1.PicID;
                        z2.PicPlace = z1.PicPlace;
                        z2.RoomName = z1.RoomName;
                        z2.spActive = z1.spActive;
                        z2.spLastUpdate = time.ToShortDateString();
                        z2.spUploadTime = z1.spUploadTime;
                        z2.UploadTime = z1.UploadTime;
                    }
                    break;
                case "willComePic":
                    WillComePic x1 = (WillComePic)obj;
                    WillComePic x2 = null;
                    if ((x2 = (WillComePic)this.Get(type, x1.PicID)) != null)
                    {
                        x2.Active = x1.Active;
                        x2.LastUpdate = time;
                        x2.PicID = x1.PicID;
                        x2.PicBWFullPath = x1.PicBWFullPath;
                        x2.PicBWRelativePath = x1.PicBWRelativePath;
                        x2.PicColorFullPath = x1.PicColorFullPath;
                        x2.PicColorRelativePath = x1.PicColorRelativePath;
                        x2.PicName = x1.PicName;
                        x2.PicPlace = x1.PicPlace;
                        x2.spActive = x1.spActive;
                        x2.spLastUpdate = time.ToShortDateString();
                        x2.spUploadTime = x1.spUploadTime;
                        x2.UploadTime = x1.UploadTime;
                    }
                    break;
                default:
                    break;
            }

            this.papaDal.SubmitChanges();
        }

        public object Get(string type, string ID)
        {
            if (type == "" || type == null || ID == "" || ID == null)
            {
                return null;
            }

            object obj = null;

            switch (type)
            {
                case "admin":
                    obj = this.papaDal.AdminUsers.SingleOrDefault(g => g.LoginID == ID);
                    break;
                case "allArtistsPic":
                    obj = this.papaDal.AllArtistPics.SingleOrDefault(g => g.PicID == ID);
                    break;
                case "complexAbout":
                    obj = this.papaDal.ComplexAbouts.SingleOrDefault(g => g.ComplexAboutID == ID);
                    break;
                case "complexPic":
                    obj = this.papaDal.ComplexRoomPics.SingleOrDefault(g => g.PicID == ID);
                    break;
                case "designPic":
                    obj = this.papaDal.DesignPics.SingleOrDefault(g => g.PicID == ID);
                    break;
                case "designText":
                    obj = this.papaDal.DesignTexts.SingleOrDefault(g => g.DesignTextID == ID);
                    break;
                case "gallery":
                    obj = this.papaDal.Galleries.SingleOrDefault(g => g.GalleryID == ID);
                    break;
                case "headerPic":
                    obj = this.papaDal.HeaderPics.SingleOrDefault(g => g.PicID == ID);
                    break;
                case "homeContactAbout":
                    obj = this.papaDal.HomeContactAbouts.SingleOrDefault(g => g.HomeContactAboutID == ID);
                    break;
                case "lastNews":
                    obj = this.papaDal.LastNews.SingleOrDefault(g => g.NewsID == ID);
                    break;
                case "lastRecordsPic":
                    obj = this.papaDal.LastRecordPics.SingleOrDefault(g => g.PicID == ID);
                    break;
                case "log":
                    obj = this.papaDal.Logs.SingleOrDefault(g => g.LogID == ID);
                    break;
                case "mainContactAbout":
                    obj = this.papaDal.MainContactAbouts.SingleOrDefault(g => g.MainContactAboutID == ID);
                    break;
                case "mainContactAboutPic":
                    obj = this.papaDal.MainContactAboutPics.SingleOrDefault(g => g.PicID == ID);
                    break;
                case "production":
                    obj = this.papaDal.Productions.SingleOrDefault(g => g.ProID == ID);
                    break;
                case "prPic":
                    obj = this.papaDal.PrPics.SingleOrDefault(g => g.PicID == ID);
                    break;
                case "prText":
                    obj = this.papaDal.PrTexts.SingleOrDefault(g => g.PrTextID == ID);
                    break;
                case "publishPic":
                    obj = this.papaDal.PublishPics.SingleOrDefault(g => g.PicID == ID);
                    break;
                case "song":
                    obj = this.papaDal.Songs.SingleOrDefault(g => g.SongID == ID);
                    break;
                case "staffPic":
                    obj = this.papaDal.Staffs.SingleOrDefault(g => g.StaffID == ID);
                    break;
                case "studioAbout":
                    obj = this.papaDal.StudioAbouts.SingleOrDefault(g => g.StudioAboutID == ID);
                    break;
                case "studioPic":
                    obj = this.papaDal.StudioRoomPics.SingleOrDefault(g => g.PicID == ID);
                    break;
                case "willComePic":
                    obj = this.papaDal.WillComePics.SingleOrDefault(g => g.PicID == ID);
                    break;
                default:
                    break;
            }

            return obj;
        }

        public void DeleteAllByType(string type)
        {
            if (type == "" || type == null)
            {
                return;
            }

            switch (type)
            {

                case "admin":
                    this.papaDal.AdminUsers.DeleteAllOnSubmit(this.papaDal.AdminUsers);
                    break;
                case "allArtistsPic":
                    this.papaDal.AllArtistPics.DeleteAllOnSubmit(this.papaDal.AllArtistPics);
                    break;
                case "complexAbout":
                    this.papaDal.ComplexAbouts.DeleteAllOnSubmit(this.papaDal.ComplexAbouts);
                    break;
                case "complexPic":
                    this.papaDal.ComplexRoomPics.DeleteAllOnSubmit(this.papaDal.ComplexRoomPics);
                    break;
                case "designPic":
                    this.papaDal.DesignPics.DeleteAllOnSubmit(this.papaDal.DesignPics);
                    break;
                case "designText":
                    this.papaDal.DesignTexts.DeleteAllOnSubmit(this.papaDal.DesignTexts);
                    break;
                case "gallery":
                    for (int i = 0; i < this.papaDal.Galleries.Count(); i++)
                    {
                        Gallery g = this.papaDal.Galleries.ElementAt(i);
                        this.Remove(g.GalleryType, g.GalleryID);
                    }
                    break;
                case "headerPic":
                    this.papaDal.HeaderPics.DeleteAllOnSubmit(this.papaDal.HeaderPics);
                    break;
                case "homeContactAbout":
                    this.papaDal.HomeContactAbouts.DeleteAllOnSubmit(this.papaDal.HomeContactAbouts);
                    break;
                case "lastNews":
                    this.papaDal.LastNews.DeleteAllOnSubmit(this.papaDal.LastNews);
                    break;
                case "lastRecordsPic":
                    this.papaDal.LastRecordPics.DeleteAllOnSubmit(this.papaDal.LastRecordPics);
                    break;
                case "log":
                    this.papaDal.Logs.DeleteAllOnSubmit(this.papaDal.Logs);
                    break;
                case "mainContactAbout":
                    this.papaDal.MainContactAbouts.DeleteAllOnSubmit(this.papaDal.MainContactAbouts);
                    break;
                case "mainContactAboutPic":
                    this.papaDal.MainContactAboutPics.DeleteAllOnSubmit(this.papaDal.MainContactAboutPics);
                    break;
                case "production":
                    for (int i = 0; i < this.papaDal.Productions.Count(); i++)
                    {
                        Production g = this.papaDal.Productions.ElementAt(i);
                        this.Remove("production", g.ProID);
                    }
                    break;
                case "prPic":
                    this.papaDal.PrPics.DeleteAllOnSubmit(this.papaDal.PrPics);
                    break;
                case "prText":
                    this.papaDal.PrTexts.DeleteAllOnSubmit(this.papaDal.PrTexts);
                    break;
                case "publish":
                    this.papaDal.PublishPics.DeleteAllOnSubmit(this.papaDal.PublishPics);
                    break;
                case "song":
                    this.papaDal.Songs.DeleteAllOnSubmit(this.papaDal.Songs);
                    break;
                case "staffPic":
                    this.papaDal.Staffs.DeleteAllOnSubmit(this.papaDal.Staffs);
                    break;
                case "studioAbout":
                    this.papaDal.StudioAbouts.DeleteAllOnSubmit(this.papaDal.StudioAbouts);
                    break;
                case "studioPic":
                    this.papaDal.StudioRoomPics.DeleteAllOnSubmit(this.papaDal.StudioRoomPics);
                    break;
                case "willComePic":
                    this.papaDal.WillComePics.DeleteAllOnSubmit(this.papaDal.WillComePics);
                    break;
                default:
                    break;
            }

            this.papaDal.SubmitChanges();
        }

        ////Niv
        ////Home Methods

        //public MainAbout GetHomeAboutText()
        //{
        //    var Text = from st in papaDal.MainAbouts
        //               select st;
        //    foreach (MainAbout s in Text)
        //    {
        //        return s;
        //    }
        //    return null;
        //}



        //public MainContact GetContactUsText()
        //{
        //    var Text = from st in papaDal.MainContacts
        //               select st;
        //    foreach (MainContact s in Text)
        //    {
        //        return s;
        //    }
        //    return null;
        //}

        //Unlimited
        //public List<string> GetHeaderPics()
        //{
        //    var pics = from srr in papaDal.HeaderPics
        //               where srr.Active > 0
        //               orderby srr.PicID ascending
        //               select srr.PicPath;
        //    List<string> L = new List<string>();
        //    foreach (string s in pics)
        //    {
        //        L.Add(s);
        //    }
        //    return L;
        //}

        //First 6
        //public List<PublishPic> GetFishEyePics()
        //{
        //    var pics = from srr in papaDal.PublishPics
        //               where srr.Active > 0
        //               orderby srr.PicID ascending
        //               select srr;
        //    List<PublishPic> L = new List<PublishPic>();
        //    foreach (PublishPic P in pics.Take(6))
        //    {
        //        L.Add(P);
        //    }
        //    return L;
        //}

        //First 12
        //public List<LastRecordPic> GetLastRecPics()
        //{
        //    var pics = from srr in papaDal.LastRecordPics
        //               where srr.Active > 0
        //               orderby srr.PicID ascending
        //               select srr;
        //    List<LastRecordPic> L = new List<LastRecordPic>();
        //    foreach (LastRecordPic P in pics)
        //    {
        //        L.Add(P);
        //    }
        //    return L;
        //}





        //Complex Methods

        //public ComplexAbout GetComplexAboutText()
        //{
        //    var Text = from st in papaDal.ComplexAbouts
        //               select st;
        //    foreach (ComplexAbout s in Text)
        //    {
        //        return s;
        //    }
        //    return null;
        //}



        //First 3
        //public List<string> GetComplexRoomAPics()
        //{
        //    var pics = from srr in papaDal.RoomAPics
        //               where srr.Active > 0
        //               orderby srr.PicID ascending
        //               select srr.PicPath;
        //    List<string> L = new List<string>();
        //    foreach (string s in pics.Take(3))
        //    {
        //        L.Add(s);
        //    }
        //    return L;
        //}
        //First 3
        //public List<string> GetComplexRoomBPics()
        //{
        //    var pics = from srr in papaDal.RoomBPics
        //               where srr.Active > 0
        //               orderby srr.PicID ascending
        //               select srr.PicPath;
        //    List<string> L = new List<string>();
        //    foreach (string s in pics.Take(3))
        //    {
        //        L.Add(s);
        //    }
        //    return L;
        //}
        //First 3
        //public List<string> GetComplexRoomCPics()
        //{
        //    var pics = from srr in papaDal.RoomCPics
        //               where srr.Active > 0
        //               orderby srr.PicID ascending
        //               select srr.PicPath;
        //    List<string> L = new List<string>();
        //    foreach (string s in pics.Take(3))
        //    {
        //        L.Add(s);
        //    }
        //    return L;

        //}
        //First2
        //public List<string> GetComplexLookAroundPics()
        //{
        //    var pics = from srr in papaDal.ComplexLookAroundPics
        //               where srr.Active > 0
        //               orderby srr.PicID ascending
        //               select srr.PicPath;
        //    List<string> L = new List<string>();
        //    foreach (string s in pics.Take(2))
        //    {
        //        L.Add(s);
        //    }
        //    return L;

        //}
        ////First 12
        //public List<WhoPlaysHereComplex> WhoPlaysComplex()
        //{
        //    var pics = from srr in papaDal.WhoPlaysHereComplexes
        //               where srr.Active > 0
        //               orderby srr.PicID ascending
        //               select srr;
        //    List<WhoPlaysHereComplex> L = new List<WhoPlaysHereComplex>();
        //    foreach (WhoPlaysHereComplex P in pics)
        //    {
        //        L.Add(P);
        //    }
        //    return L;
        //}

        //Studio Methods

        //public StudioAbout GetStudioAboutText()
        //{
        //    var Text = from st in papaDal.StudioAbouts
        //               select st;
        //    foreach (StudioAbout s in Text)
        //    {
        //        return s;
        //    }
        //    return null;
        //}

        //First 3
        //public List<string> GetStudioRecRoomPics()
        //{
        //    var pics = from srr in papaDal.RecordingRoomPics
        //               where srr.Active > 0
        //               orderby srr.PicID ascending
        //               select srr.PicPathColor;
        //    List<string> L = new List<string>();
        //    foreach (string s in pics.Take(3))
        //    {
        //        L.Add(s);
        //    }
        //    return L;
        //}
        ////First 3
        //public List<string> GetStudioLookAPics()
        //{
        //    var pics = from srr in papaDal.StudioLookAroundPics
        //               where srr.Active>0
        //               orderby srr.PicID ascending
        //               select srr.PicPath;
        //    List<string> L = new List<string>();
        //    foreach (string s in pics.Take(3))
        //    {
        //        L.Add(s);
        //    }
        //    return L;
        //}
        ////First 3
        //public List<string> GetStudioControlPics()
        //{
        //    var pics = from srr in papaDal.ControlRoomPics
        //               where srr.Active > 0
        //               orderby srr.PicID ascending
        //               select srr.PicPath;
        //    List<string> L = new List<string>();
        //    foreach (string s in pics.Take(3))
        //    {
        //        L.Add(s);
        //    }
        //    return L;

        //}

        //First 12
        //public List<WhoPlaysHereStudio> WhoPlaysStudio()
        //{
        //    var pics = from srr in papaDal.WhoPlaysHereStudios
        //               where srr.Active > 0
        //               orderby srr.PicID ascending
        //               select srr;
        //    List<WhoPlaysHereStudio> L = new List<WhoPlaysHereStudio>();
        //    foreach (WhoPlaysHereStudio P in pics)
        //    {
        //        L.Add(P);
        //    }
        //    return L;
        //}



        //PR Methods
        //public PrText GetPRText()
        //{
        //    var txt = from t in papaDal.PrTexts
        //              select t;
        //    foreach (PrText p in txt)
        //    {
        //        return p;
        //    }
        //    return null;
        //}

        //First 1
        //public string GetPRPic()
        //{
        //    var txt = from t in papaDal.PrPics
        //              where t.Active > 0
        //              orderby t.PicID ascending
        //              select t.PicPath;
        //    foreach (string S in txt.Take(1))
        //    {
        //        return S;
        //    }
        //    return null;
        //}


        //Design Methods
        //public List<string> GetDesignPics()
        //{
        //    var pics = from srr in papaDal.DesignPics
        //               where srr.Active > 0
        //               orderby srr.PicID ascending
        //               select srr.PicPath;
        //    List<string> L = new List<string>();
        //    foreach (string s in pics)
        //    {
        //        L.Add(s);
        //    }
        //    return L;
        //}
        //public DesignText GetDesignText()
        //{
        //    var txt = from t in papaDal.DesignTexts
        //              select t;
        //    foreach (DesignText p in txt)
        //    {
        //        return p;
        //    }
        //    return null;
        //}

        //Staff Methods
        //public StaffDudu GetStaffDudu()
        //{
        //    var staff = from t in papaDal.StaffDudus
        //                select t;
        //    foreach (var a in staff)
        //    {
        //        return a;
        //    }
        //    return null;
        //}
        //public StaffPerri GetStaffPerri()
        //{
        //    var staff = from t in papaDal.StaffPerris
        //                select t;
        //    foreach (var a in staff)
        //    {
        //        return a;
        //    }
        //    return null;
        //}
        //public StaffNapo GetStaffNapo()
        //{
        //    var staff = from t in papaDal.StaffNapos
        //                select t;
        //    foreach (var a in staff)
        //    {
        //        return a;
        //    }
        //    return null;
        //}
        //public StaffItay GetStaffItay()
        //{
        //    var staff = from t in papaDal.StaffItays
        //                select t;
        //    foreach (var a in staff)
        //    {
        //        return a;
        //    }
        //    return null;
        //}
    }
}
