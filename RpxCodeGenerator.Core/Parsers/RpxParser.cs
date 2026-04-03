using RpxCodeGenerator.Core.Models;
using System;
using System.IO;
using System.Xml.Linq;

namespace RpxCodeGenerator.Core.Parsers;

/// <summary>
/// Parser cho file RPX (XML format)
/// </summary>
public class RpxParser
{
	/// <summary>
	/// Parse file RPX và trích xuất cấu trúc Section và Control
	/// </summary>
	public RpxDocument ParseFile(string filePath)
	{
		if (!File.Exists(filePath))
			throw new FileNotFoundException($"File not found: {filePath}");

		var doc = XDocument.Load(filePath);
		var root = doc.Root;

		if (root == null)
			throw new InvalidOperationException("Invalid RPX file format");

		var rpxDoc = new RpxDocument
		{
			DocumentName = Path.GetFileNameWithoutExtension(filePath),
			Version = root.Attribute("Version")?.Value ?? "Unknown"
		};

		var sectionsElement = root.Element("Sections");
		if (sectionsElement != null)
		{
			foreach (var sectionElement in sectionsElement.Elements("Section"))
			{
				var section = ParseSection(sectionElement);
				rpxDoc.Sections.Add(section);
			}
		}

		var ScriptElement = root.Element("Script");
		if (ScriptElement != null)
		{
			rpxDoc.Script = ScriptElement?.Value ?? "";
		}
		return rpxDoc;
	}

	/// <summary>
	/// Parse một Section element từ XML
	/// </summary>
	private RpxSection ParseSection(XElement sectionElement)
	{
		var section = new RpxSection
		{
			Type = sectionElement.Attribute("Type")?.Value ?? string.Empty,
			Name = sectionElement.Attribute("Name")?.Value ?? string.Empty
		};

		foreach (var controlElement in sectionElement.Elements("Control"))
		{
			var control = ParseControl(controlElement);
			section.Controls.Add(control);
		}

		return section;
	}

	/// <summary>
	/// Parse một Control element từ XML
	/// </summary>
	private RpxControl ParseControl(XElement controlElement)
	{
		var control = new RpxControl
		{
			Type = controlElement.Attribute("Type")?.Value ?? string.Empty,
			Name = controlElement.Attribute("Name")?.Value ?? string.Empty
		};

		// Lấy tất cả các attributes
		foreach (var attr in controlElement.Attributes())
		{
			if (attr.Name.LocalName != "Type" && attr.Name.LocalName != "Name")
			{
				control.Properties[attr.Name.LocalName] = attr.Value;
			}
		}

		return control;
	}

	/// <summary>
	/// Parse từ nội dung XML (string)
	/// </summary>
	public RpxDocument ParseContent(string xmlContent)
	{
		var doc = XDocument.Parse(xmlContent);
		var root = doc.Root;

		if (root == null)
			throw new InvalidOperationException("Invalid RPX content format");

		var rpxDoc = new RpxDocument
		{
			DocumentName = root.Attribute("DocumentName")?.Value ?? "Unknown",
			Version = root.Attribute("Version")?.Value ?? "Unknown"
		};

		var sectionsElement = root.Element("Sections");
		if (sectionsElement != null)
		{
			foreach (var sectionElement in sectionsElement.Elements("Section"))
			{
				var section = ParseSection(sectionElement);
				rpxDoc.Sections.Add(section);
			}
		}

		return rpxDoc;
	}
}
