using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Flickr
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                using (HttpClient client = new HttpClient())
                {
                    String content = await client.GetStringAsync("http://www.flickr.com/services/feeds/photos_public.gne?tags=soccer&format=json&nojsoncallback=1");
                    //String content = await client.GetStringAsync("https://api.flickr.com/services/rest/?method=flickr.photos.getRecent&api_key=e589b7a873555bc4ed85c3e1a6fd676e&format=json&nojsoncallback=1");

                    Feed feed = JsonConvert.DeserializeObject<Feed>(content);

                    Console.WriteLine("Titulo: {0}", feed.Title);

                    foreach (var f in feed.Items)
                    {
                        Console.WriteLine("Imagen: {0}", f.Media.M);
                    }
                    Console.ReadLine();
                }
            }).Wait();
        }

        class Feed
        {
            public String Title { get; set; }
            public List<Item> Items { get; set; }
        }

        class Item
        {
            public String Title { get; set; }
            public String Link { get; set; }
            public MUrl Media { get; set; }
        }

        class MUrl
        {
            public String M { get; set; }
        }
    }
}
