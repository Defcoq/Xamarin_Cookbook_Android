using System.Collections.Generic;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace XamarinCookbook
{
	[XmlRoot(ElementName = "rss")]
	public class Rss
	{
		[XmlElement("channel")]
		public Channel Channel { get; set; }
	}

	[XmlRootAttribute(ElementName = "channel")]
	public class Channel
	{
		[XmlElement("title")]
		public string Title { get; set; }

		[XmlElement(ElementName = "item", Type = typeof(Item))]
		public List<Item> Items { get; set; }
	}

	[XmlRootAttribute(ElementName = "item")]
	public class Item
	{
		[XmlElement("guid")]
		public string Guid { get; set; }

		[XmlElement("title")]
		public string Title { get; set; }

		[XmlElement("description")]
		public XmlCDataSection DescriptionCData { get; set; }

		public string Content
		{
			get { return WebUtility.HtmlDecode(DescriptionCData.Data); }
		}

		public override string ToString()
		{
			return string.Format(
				"Post Url: {0}\nTitle: {1}\nBody: {2}",
				Guid, Title, Content);
		}
	}	
}
