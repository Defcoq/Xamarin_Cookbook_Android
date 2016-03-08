using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Android.Runtime;
using Android.Support.V7.App;

namespace XamarinCookbook
{
	[Register ("com.xamarincookbook.SearchActivity")]
	[IntentFilter (new [] { Intent.ActionSearch })]
	[IntentFilter (new [] { "com.google.android.gms.actions.SEARCH_ACTION" }, Categories = new [] { Intent.CategoryDefault })]
	[MetaData ("android.app.searchable", Resource = "@xml/searchable")]
	[Activity (Label = "SearchActivity", LaunchMode = LaunchMode.SingleTop, Theme = "@style/Theme.AppCompat")]			
	public class SearchActivity : AppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.SearchLayout);

			// do our search
			HandleIntent (Intent);
		}

		protected override void OnNewIntent (Intent intent)
		{
			// do our search
			HandleIntent (intent);
		}

		private void HandleIntent(Intent intent)
		{
			if (intent.Action == Intent.ActionSearch || 
				intent.Action == "com.google.android.gms.actions.SEARCH_ACTION") {
				string query = intent.GetStringExtra (SearchManager.Query);

				TextView textView = FindViewById<TextView> (Resource.Id.textView);
				textView.Text = string.Format("Search Query: '{0}'", query);
			}
		}
	}
}

