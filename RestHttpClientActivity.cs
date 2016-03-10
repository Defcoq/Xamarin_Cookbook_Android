using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace XamarinCookbook
{
    [Activity(Label = "RestHttpClientActivity", ParentActivity =typeof(ComunicationActivity))]
    public class RestHttpClientActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.RestHttpClientLayout);

            Button readJson = FindViewById<Button>(Resource.Id.readJson);
            Button readXml = FindViewById<Button>(Resource.Id.readXml);
            Button sendData = FindViewById<Button>(Resource.Id.sendData);
            TextView textView = FindViewById<TextView>(Resource.Id.textView);

            readJson.Click += async delegate
            {
                using (var client = new HttpClient())
                {
                    // send the GET request
                    var uri = "http://jsonplaceholder.typicode.com/posts";
                    var result = await client.GetStringAsync(uri);

                    // process the response
                    var posts = JsonConvert.DeserializeObject<List<Post>>(result);

                    // generate output
                    var post = posts.First();
                    textView.Text = "First post:\n\n" + post;
                }
            };

            readXml.Click += async delegate
            {
                using (var client = new HttpClient())
                {
                    // send the GET request
                    var uri = "http://blog.xamarin.com/feed";
                    var result = await client.GetStreamAsync(uri);

                    // process the response
                    var serializer = new XmlSerializer(typeof(Rss));
                    var feed = (Rss)serializer.Deserialize(result);

                    // generate output
                    var item = feed.Channel.Items.First();
                    textView.Text = "First item:\n\n" + item;
                }
            };

            sendData.Click += async delegate
            {
                using (var client = new HttpClient())
                {
                    // create the new post
                    var newPost = new Post
                    {
                        UserId = 12,
                        Title = "My First Post",
                        Content = "This is some real deep stuff in here!"
                    };

                    // create the request content
                    var json = JsonConvert.SerializeObject(newPost);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // send the POST request
                    var uri = "http://jsonplaceholder.typicode.com/posts";
                    var result = await client.PostAsync(uri, content);

                    // throw an exception if there was any error
                    result.EnsureSuccessStatusCode();

                    // process the response
                    var resultString = await result.Content.ReadAsStringAsync();
                    var post = JsonConvert.DeserializeObject<Post>(resultString);

                    // generate output
                    textView.Text = post.ToString();
                }
            };
        }
    }
}