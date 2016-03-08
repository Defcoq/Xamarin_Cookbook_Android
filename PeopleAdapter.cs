using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;
using Android.Views;
using System;
using Android.Graphics.Drawables;

namespace XamarinCookbook
{
	public class PeopleAdapter : BaseAdapter<Person>
	{
		private readonly Context context;
		private readonly List<Person> data;

		public PeopleAdapter(Context context, List<Person> data)
		{
			this.data = data;
			this.context = context;
		}

		// How many items are in the adapter
		public override int Count { get { return data.Count; } }

		// Gets the Person at the specified index.
		public override Person this [int index] { get { return data [index]; } }

		public override long GetItemId (int position)
		{
			// return the ID, but we could just return the position
			return data [position].Id;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			// get the data
			var person = data [position];

			// inflate the view
			if (convertView == null) {
				var inflater = LayoutInflater.From (context);
				convertView = inflater.Inflate (Resource.Layout.ListItemLayout, parent, false);
			}

			// get the controls
			var icon = convertView.FindViewById<ImageView> (Resource.Id.icon);
			var firstRow = convertView.FindViewById<TextView> (Resource.Id.firstRow);
			var secondRow = convertView.FindViewById<TextView> (Resource.Id.secondRow);

			// put the data into the views
			var image = person.IsMale ? Resource.Drawable.male : Resource.Drawable.female;
			icon.SetImageResource (image);
			firstRow.Text = person.Name;
			secondRow.Text = person.Status;

			// return the view we want to use
			return convertView;
		}
	}
}
