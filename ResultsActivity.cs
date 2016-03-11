using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Net;
using System;
using Uri = Android.Net.Uri;

namespace XamarinCookbook
{
	[Activity]
	public class ResultsActivity : ListActivity
	{
		public const string ExtraKey = "ExtraKey";

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			var items = new []{ "First", "Second", "Third", "Fourth", "Fifth" };
			ListAdapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, items);

			ListView.ItemClick += (sender, e) => {
				// send a result back to the caller
				var result = new Intent ();
				result.PutExtra(ExtraKey, items [e.Position]);
				SetResult (Result.Ok, result);
				Finish ();
			};
		}
	}
}
