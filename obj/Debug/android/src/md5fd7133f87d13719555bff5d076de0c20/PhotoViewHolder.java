package md5fd7133f87d13719555bff5d076de0c20;


public class PhotoViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("XamarinCookbook.PhotoViewHolder, XamarinCookbook, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PhotoViewHolder.class, __md_methods);
	}


	public PhotoViewHolder (android.view.View p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == PhotoViewHolder.class)
			mono.android.TypeManager.Activate ("XamarinCookbook.PhotoViewHolder, XamarinCookbook, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
