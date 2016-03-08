using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XamarinCodeSharing.Core
{
    public class XamarinBlog
    {
        private const string BlogUrl ="http://blog.xamarin.com/feed";
        // blog metadata properties
        public string Title { get; set; }
        public string Link { get; set; }
        public List<BlogItem> Items { get; private set; }
        // Download the feed, parse it and return a blog object
        public static async Task<XamarinBlog> Download()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await
            client.GetAsync(BlogUrl);
            // if all went well, read the feed, otherwise fail
            if (response.IsSuccessStatusCode)
            {
                return await ParseResponse(response.Content);
            }
            else {
                throw new Exception("There was a problem.");
            }
        }

        // Read the response out of the content and
        // create objects
        private static async Task<XamarinBlog> ParseResponse(HttpContent content)
        {
            XamarinBlog blog = new XamarinBlog();
            using (Stream stream = await content.ReadAsStreamAsync())
            {
                XDocument doc = XDocument.Load(stream);
                XElement channel = doc.Root.Element("channel");
                // load the blog metadata out of the xml
                blog.Title = WebUtility.HtmlDecode(channel.Element("title").Value);
                blog.Link = WebUtility.HtmlDecode(channel.Element("link").Value);
                // load the blog items out of the xml
                var items = from item in channel.Elements("item")
                            select new BlogItem
                            {
                                Title = WebUtility.HtmlDecode(item.Element("title").Value),
                                Link = WebUtility.HtmlDecode(item.Element("link").Value),
                                PublishDate = DateTime.Parse(WebUtility.HtmlDecode(item.Element("pubDate").Value)),
                                Description = WebUtility.HtmlDecode(item.Element("description").Value),
                            };
                blog.Items = items.ToList();
            }
            return blog;
        }
    

}
        }
