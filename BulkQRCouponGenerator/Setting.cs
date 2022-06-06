using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BulkImage_app
{
	// using System.Xml.Serialization;
	// XmlSerializer serializer = new XmlSerializer(typeof(Setting));
	// using (StringReader reader = new StringReader(xml))
	// {
	//    var test = (Setting)serializer.Deserialize(reader);
	// }

	[XmlRoot(ElementName = "QR_Pos_X")]
	public class QRPosX
	{

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "QR_Pos_Y")]
	public class QRPosY
	{

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "QR_Size_X")]
	public class QRSizeX
	{

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "QR_Size_Y")]
	public class QRSizeY
	{

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "QR_Colour")]
	public class QRColour
	{

		[XmlAttribute(AttributeName = "value")]
		public String Value { get; set; }
	}

	[XmlRoot(ElementName = "QR_Link")]
	public class QRLink
	{

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "TXT_Pos_X")]
	public class TXTPosX
	{

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "TXT_Pos_Y")]
	public class TXTPosY
	{

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}



	[XmlRoot(ElementName = "TXT_Colour")]
	public class TXTColour
	{

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "TXT_Font")]
	public class TXTFont
	{

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "TXT_Overlay")]
	public class TXTOverlay
	{

		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "Setting")]
	public class Setting
	{

		[XmlElement(ElementName = "QR_Pos_X")]
		public QRPosX QRPosX { get; set; }

		[XmlElement(ElementName = "QR_Pos_Y")]
		public QRPosY QRPosY { get; set; }

		[XmlElement(ElementName = "QR_Size_X")]
		public QRSizeX QRSizeX { get; set; }

		[XmlElement(ElementName = "QR_Size_Y")]
		public QRSizeY QRSizeY { get; set; }

		[XmlElement(ElementName = "QR_Colour")]
		public QRColour QRColour { get; set; }

		[XmlElement(ElementName = "QR_Link")]
		public QRLink QRLink { get; set; }

		[XmlElement(ElementName = "TXT_Pos_X")]
		public TXTPosX TXTPosX { get; set; }

		[XmlElement(ElementName = "TXT_Pos_Y")]
		public TXTPosY TXTPosY { get; set; }

		[XmlElement(ElementName = "TXT_Colour")]
		public TXTColour TXTColour { get; set; }

		[XmlElement(ElementName = "TXT_Font")]
		public TXTFont TXTFont { get; set; }

		[XmlElement(ElementName = "TXT_Overlay")]
		public TXTOverlay TXTOverlay { get; set; }
	}


}
