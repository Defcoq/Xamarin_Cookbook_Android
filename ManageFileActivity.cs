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
using System.IO;
using Android.Support.V7.App;
using ActionBar = Android.Support.V7.App.ActionBar;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using System.Xml.Linq;
using Mono.Data.Sqlite;
using SQLiteNet= SQLite.Net;

namespace XamarinCookbook
{

    [Activity(Label = "ManageFileActivity", ParentActivity =typeof(MainActivity), Theme = "@style/Theme.AppCompat")]
    public class ManageFileActivity : AppCompatActivity, ActionBar.ITabListener
    {
        private const string MyFileName = "myFile.txt";
        private const string TextValue = "this is my value";

        private string basePath = "";
        private string baseFilename = "";

        private string before = "";
        private string stats = "";
        private string create = "";
        private string query = "";

        #region ado.net
        private string createTable = "CREATE TABLE IF NOT EXISTS [MyTable] (id INTEGER PRIMARY KEY AUTOINCREMENT, firstName TEXT, lastName TEXT)";
        private string insertQuery = "INSERT INTO [MyTable] (firstName, lastName) VALUES (@firstName, @lastName)";
        private string selectQuery = "SELECT * FROM [MyTable]";
        private string orderAndFilterQuery = "SELECT * FROM [MyTable] WHERE [lastname] = @lastname ORDER BY [firstName]";
        private Tuple<string, string>[] people = {
            new Tuple<string, string>("Joseph", "Albahari"),
            new Tuple<string, string>("Ben", "Albahari"),
            new Tuple<string, string>("Ian", "Griffiths"),
            new Tuple<string, string>("Tony", "Bevis"),
            new Tuple<string, string>("Jon", "Skeet"),
            new Tuple<string, string>("Jennifer", "Greene"),
            new Tuple<string, string>("Andrew", "Stellman"),
            new Tuple<string, string>("Tomas", "Petricek")
        };
        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
           // ActionBar.SetDisplayHomeAsUpEnabled(true);

            // load the layout
            SetContentView(Resource.Layout.JPManageFile);

            //    // allow the user to select a location
            var spinner = FindViewById<Spinner>(Resource.Id.spinner);
            var basePathLabel = FindViewById<TextView>(Resource.Id.basePathLabel);
            var dic = new Dictionary<string, string> {
                { "Internal Files", FilesDir.AbsolutePath },
                { "External Files", GetExternalFilesDir (null).AbsolutePath },
                { "Internal Cache", CacheDir.AbsolutePath },
                { "External Cache", ExternalCacheDir.AbsolutePath },
                { "Public Downloads", Android.OS.Environment.GetExternalStoragePublicDirectory (Android.OS.Environment.DirectoryDownloads).AbsolutePath },
            };
            string[] keys = dic.Keys.ToArray();
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, keys);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            spinner.ItemSelected += (sender, e) =>
            {
                string key = keys[e.Position];
                basePath = dic[key];
                baseFilename = Path.Combine(basePath, MyFileName);
                // update the UI
                basePathLabel.Text = string.Format("Base: {0}\n\nFile Path: {1}", basePath, baseFilename);
            };


            // get the controls
            var checkFile = FindViewById<Button>(Resource.Id.checkFile);
            var readFile = FindViewById<Button>(Resource.Id.readFile);
            var writeFile = FindViewById<Button>(Resource.Id.writeFile);
            var listFiles = FindViewById<Button>(Resource.Id.listFiles);
            var storageState = FindViewById<Button>(Resource.Id.storageState);
            var outputLabel = FindViewById<TextView>(Resource.Id.outputLabel);

            // set the operation actions
            checkFile.Click += delegate
            {
                // wrap the operation to check for errors
                try
                {
                    // the the existance of the file
                    var exists = File.Exists(baseFilename);
                    // update the UI
                    outputLabel.Text = "File Exists: " + exists;
                }
                catch (Exception ex)
                {
                    // write any error messages to the UI
                    outputLabel.Text = ex.Message;
                }
            };
            readFile.Click += delegate
            {
                // wrap the operation to check for errors
                try
                {
                    // read the value out of the file
                    string value = File.ReadAllText(baseFilename);
                    // update the UI
                    outputLabel.Text = "The value is: " + value;
                }
                catch (Exception ex)
                {
                    // write any error messages to the UI
                    // an exception may be thrown if the file does not exist
                    outputLabel.Text = ex.Message;
                }
            };
            writeFile.Click += delegate
            {
                // wrap the operation to check for errors
                try
                {
                    // write the value into of the file
                    File.WriteAllText(baseFilename, TextValue);
                    // update the UI
                    outputLabel.Text = "The value was written.";
                }
                catch (Exception ex)
                {
                    // write any error messages to the UI
                    // an exception may be thrown if the file does not exist
                    outputLabel.Text = ex.Message;
                }
            };
            listFiles.Click += delegate
            {
                // wrap the operation to check for errors
                try
                {
                    // list the files
                    var files = Directory.GetFiles(basePath);
                    var value = string.Join(System.Environment.NewLine, files);
                    outputLabel.Text = "The files are: \n" + value;
                }
                catch (Exception ex)
                {
                    // write any error messages to the UI
                    // an exception may be thrown if the file does not exist
                    outputLabel.Text = ex.Message;
                }
            };
            storageState.Click += delegate
            {
                var writable = Android.OS.Environment.ExternalStorageState == Android.OS.Environment.MediaMounted;
                var readable = writable || Android.OS.Environment.ExternalStorageState == Android.OS.Environment.MediaMountedReadOnly;

                var file = new Java.IO.File(basePath);

                const int bytesInMB = 1000 * 1000;

                outputLabel.Text = string.Format(
                    "Readable: {0}\nWritable: {1}\nFree Space: {2} MB\nTotal Space: {3} MB\n",
                    readable, writable, file.FreeSpace / bytesInMB, file.TotalSpace / bytesInMB);
            };

            var readAsset = FindViewById<Button>(Resource.Id.readAsset);
            var readRaw = FindViewById<Button>(Resource.Id.readRaw);
            var readString = FindViewById<Button>(Resource.Id.readString);
           

            // set the operation actions
            readAsset.Click += delegate {
                using (Stream asset = Resources.Assets.Open("MyAsset.txt"))
                using (StreamReader reader = new StreamReader(asset))
                {
                    outputLabel.Text = reader.ReadToEnd();
                }
            };
            readRaw.Click += delegate {
                using (Stream raw = Resources.OpenRawResource(Resource.Raw.MyRaw))
                using (StreamReader reader = new StreamReader(raw))
                {
                    outputLabel.Text = reader.ReadToEnd();
                }
            };
            readString.Click += delegate {
                outputLabel.Text = Resources.GetString(Resource.String.myString);
            };


            // do the work
            CreateBeforeContent();
            CreateStatsContent();
            CreateProcessedContent();
            CreateDynamicContent();

            // create the tabs
            ActionBar actionBar = SupportActionBar;
            actionBar.NavigationMode = (int)ActionBarNavigationMode.Tabs;
            actionBar.SetDisplayShowTitleEnabled(false);
            var beforeTab = actionBar.NewTab().SetText("Initial XML").SetTabListener(this).SetTag("before");
            var statsTab = actionBar.NewTab().SetText("Processing").SetTabListener(this).SetTag("stats");
            var createTab = actionBar.NewTab().SetText("Simplified XML").SetTabListener(this).SetTag("create");
            var queryTab = actionBar.NewTab().SetText("Dynamic XML").SetTabListener(this).SetTag("query");
            actionBar.AddTab(beforeTab);
            actionBar.AddTab(statsTab);
            actionBar.AddTab(createTab);
            actionBar.AddTab(queryTab);

            actionBar.SelectTab(statsTab);


            #region ado.net
            string databasePath = Path.Combine(FilesDir.AbsolutePath, "database.sqlite");
            string connectionString = string.Format("Data Source={0}", databasePath);

            using (var conn = new SqliteConnection(connectionString))
            {
                // open the connection, creating the file
                conn.Open();

                // create the table
                using (var cmd = new SqliteCommand(createTable, conn))
                {
                    cmd.ExecuteNonQuery();
                    outputLabel.Text += "Created table.\n";
                }

                // insert the items
                using (var trans = conn.BeginTransaction())
                using (var cmd = new SqliteCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@firstname", "");
                    cmd.Parameters.AddWithValue("@lastName", "");
                    foreach (var person in people)
                    {
                        cmd.Parameters["@firstname"].Value = person.Item1;
                        cmd.Parameters["@lastName"].Value = person.Item2;
                        cmd.ExecuteNonQuery();
                    }
                    outputLabel.Text += "Populated table.\n";
                    trans.Commit();
                }

                // query the items
                using (var cmd = new SqliteCommand(selectQuery, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    List<string> names = new List<string>();
                    while (reader.Read())
                    {
                        names.Add(reader["lastname"].ToString());
                    }

                    if (names.Count > 0)
                    {
                        outputLabel.Text += string.Format(
                            "There are {0} people: {1}\n",
                            names.Count, string.Join(", ", names));
                    }
                    else {
                        outputLabel.Text += "There are no people.\n";
                    }
                }

                // query the items using a filter
                using (var cmd = new SqliteCommand(orderAndFilterQuery, conn))
                {
                    // add the filter
                    string lastName = "Albahari";
                    cmd.Parameters.AddWithValue("@lastname", lastName);

                    // read the results
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<string> names = new List<string>();
                        while (reader.Read())
                        {
                            names.Add(reader["firstname"].ToString());
                        }

                        if (names.Count > 0)
                        {
                            outputLabel.Text += string.Format(
                                "There are {0} people with the last name '{1}': {2}\n",
                                names.Count, lastName, string.Join(", ", names));
                        }
                        else {
                            outputLabel.Text += string.Format(
                                "There are no people with the lame '{0}'.\n", lastName);
                        }
                    }
                }
            }
            #endregion

            #region sqlite.net
            var outputSQliteNetLabel = FindViewById<TextView>(Resource.Id.outputSQLiteNetLabel);

            using (var connSqliteNet = new SQLiteNet.SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), databasePath))
            {
                // create the table
                connSqliteNet.CreateTable<MyTable>();
                outputSQliteNetLabel.Text += "Created table.\n";

                // insert the items
                connSqliteNet.RunInTransaction(delegate
                {
                    foreach (var person in people)
                    {
                        var p = new MyTable
                        {
                            FirstName = person.Item1,
                            LastName = person.Item2
                        };
                        connSqliteNet.Insert(p);
                    }
                });

                outputLabel.Text += "Populated table.\n";

                // query the items
                var rows = connSqliteNet
                    .Table<MyTable>()
                    .ToList();
                if (rows.Count > 0)
                {
                    outputLabel.Text += string.Format(
                        "There are {0} people: {1}\n", rows.Count, string.Join(", ", rows.Select(r => r.LastName)));
                }
                else {
                    outputSQliteNetLabel.Text += "There are no people.\n";
                }

                // query the items using a filter
                string lastName = "Albahari";
                var filtered = connSqliteNet
                    .Table<MyTable>()
                    .Where(r => r.LastName == lastName)
                    .OrderBy(r => r.FirstName)
                    .ToList();
                if (filtered.Count > 0)
                {
                    outputLabel.Text += string.Format(
                        "There are {0} people with the last name '{1}': {2}\n",
                        filtered.Count, lastName, string.Join(", ", filtered.Select(r => r.FirstName)));
                }
                else {
                    outputSQliteNetLabel.Text += string.Format(
                        "There are no people with the lame '{0}'.\n", lastName);
                }

                // clean up
                connSqliteNet.DropTable<MyTable>();
            }
        #endregion
    }


        private void CreateBeforeContent()
        {
            // load the xml asset
            using (var asset = Resources.Assets.Open("bookshelf.xml"))
            using (StreamReader reader = new StreamReader(asset))
            {
                before = reader.ReadToEnd();
            }
        }

        private void CreateStatsContent()
        {
            // process the xml
            using (var asset = Resources.Assets.Open("bookshelf.xml"))
            {
                XDocument doc = XDocument.Load(asset);
                var root = doc.Root;

                // simple query
                stats += string.Format("Books ({0}): ", root.Elements("book").Count());
                stats += string.Join(", ",
                    from title in root.Elements("book").Attributes("title")
                    select title.Value);
                stats += System.Environment.NewLine + System.Environment.NewLine;

                // more advanced
                var authors =
                    from author in root.Elements("book").Elements("authors").Elements("author")
                    where author.HasAttributes
                    let firstname = author.Attribute("firstname").Value
                    let lastname = author.Attribute("lastname").Value
                    orderby lastname, firstname
                    select string.Format("{0} {1}", firstname, lastname);
                stats += string.Format("Authors: {0}", string.Join(", ", authors.Distinct()));
                stats += System.Environment.NewLine + System.Environment.NewLine;

                // grouping
                var booksByAuthor =
                    from book in root.Elements("book")
                    from author in book.Elements("authors").Elements("author")
                    group book by string.Format("{0} {1}",
                            author.Attribute("firstname").Value,
                            author.Attribute("lastname").Value) into authorBook
                    select string.Format("{0} ({1}):\n{2}",
                            authorBook.Key,
                            authorBook.Count(),
                            string.Join(", ", authorBook.Select(b => b.Attribute("title").Value)));
                stats += string.Format("Books by Author: \n{0}", string.Join(System.Environment.NewLine, booksByAuthor));
                stats += System.Environment.NewLine;
            }
        }

        private void CreateProcessedContent()
        {
            var doc =
                new XDocument(
                    new XElement("bookshelf",
                        new XElement("book",
                            new XAttribute("title", "C# 5.0 in a Nutshell"),
                            new XElement("authors",
                                new XElement("author", "Joseph Albahari"),
                                new XElement("author", "Ben Albahari"))),
                        new XElement("book",
                            new XAttribute("title", "Programming C# 5.0"),
                            new XElement("authors",
                                new XElement("author", "Ian Griffiths"))),
                        new XElement("book",
                            new XAttribute("title", "C# Design Pattern Essentials"),
                            new XElement("authors",
                                new XElement("author", "Tony Bevis"))),
                        new XElement("book",
                            new XAttribute("title", "C# in Depth"),
                            new XElement("authors",
                                new XElement("author", "Jon Skeet"))),
                        new XElement("book",
                            new XAttribute("title", "Head First C#"),
                            new XElement("authors",
                                new XElement("author", "Jennifer Greene"),
                                new XElement("author", "Andrew Stellman"))),
                        new XElement("book",
                            new XAttribute("title", "Real-World Functional Programming"),
                            new XElement("authors",
                                new XElement("author", "Jon Skeet"),
                                new XElement("author", "Tomas Petricek")))));
            create = doc.ToString();
        }

        private void CreateDynamicContent()
        {
            using (var asset = Resources.Assets.Open("bookshelf.xml"))
            {
                XDocument doc = XDocument.Load(asset);
                var root = doc.Root;

                var newDoc =
                    new XDocument(
                        new XElement("bookshelf",
                            from book in root.Elements("book")
                            select new XElement("book",
                                    new XAttribute("title", book.Attribute("title").Value),
                                    from author in book.Elements("authors").Elements("author")
                                    select new XElement("author", author.Attribute("lastname").Value))));
                query = newDoc.ToString();
            }
        }

        public void OnTabSelected(ActionBar.Tab tab, FragmentTransaction ft)
        {
            var tabLabel = FindViewById<TextView>(Resource.Id.tabLabel);
            var tag = (string)tab.Tag;
            if (tag == "before")
            {
                tabLabel.Text = before;
            }
            else if (tag == "stats")
            {
                tabLabel.Text = stats;
            }
            else if (tag == "create")
            {
                tabLabel.Text = create;
            }
            else if (tag == "query")
            {
                tabLabel.Text = query;
            }
        }

        public void OnTabReselected(ActionBar.Tab tab, FragmentTransaction ft)
        {
        }

        public void OnTabUnselected(ActionBar.Tab tab, FragmentTransaction ft)
        {
        }
    }
}