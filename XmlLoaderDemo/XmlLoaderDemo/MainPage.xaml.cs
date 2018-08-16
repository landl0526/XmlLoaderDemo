using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace XmlLoaderDemo
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        List<Song> _rawData = new List<Song>();
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("XmlLoaderDemo." + "songs.xml");
            await Task.Factory.StartNew(delegate {
                XDocument doc = XDocument.Load(stream);
                IEnumerable<Song> songs = from s in doc.Descendants("Song")
                                          select new Song
                                          {
                                              Title = s.Attribute("Title").Value,
                                              Artist = s.Attribute("Artist").Value,
                                              TrackId = s.Attribute("TrackId").Value,
                                              Timestamp = s.Attribute("Timestamp").Value
                                          };
                _rawData = songs.ToList();
            });
        }
    }

    public class Song
    {
        public string Title { set; get; }
        public string Artist { set; get; }
        public string TrackId { set; get; }
        public string Timestamp { set; get; }
    }
}
