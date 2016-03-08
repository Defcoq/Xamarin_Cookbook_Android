package md5fd7133f87d13719555bff5d076de0c20;


public class MenuFragment
	extends android.app.ListFragment
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onAttach:(Landroid/app/Activity;)V:GetOnAttach_Landroid_app_Activity_Handler\n" +
			"n_onStart:()V:GetOnStartHandler\n" +
			"";
		mono.android.Runtime.register ("XamarinCookbook.MenuFragment, XamarinCookbook, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MenuFragment.class, __md_methods);
	}


	public MenuFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MenuFragment.class)
			mono.android.TypeManager.Activate ("XamarinCookbook.MenuFragment, XamarinCookbook, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onAttach (android.app.Activity p0)
	{
		n_onAttach (p0);
	}

	private native void n_onAttach (android.app.Activity p0);


	public void onStart ()
	{
		n_onStart ();
	}

	private native void n_onStart ();

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
