#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SilentNotes.Views
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#line 1 "ExportRazorView.cshtml"
using SilentNotes.ViewModels;

#line default
#line hidden


[System.CodeDom.Compiler.GeneratedCodeAttribute("RazorTemplatePreprocessor", "16.7.0.440")]
public partial class ExportRazorView : ExportRazorViewBase
{

#line hidden

#line 2 "ExportRazorView.cshtml"
public ExportViewModel Model { get; set; }

#line default
#line hidden


public override void Execute()
{
WriteLiteral("<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta");

WriteLiteral(" http-equiv=\"X-UA-Compatible\"");

WriteLiteral(" content=\"IE=edge\"");

WriteLiteral(" />\r\n    <base");

WriteAttribute ("href", " href=\"", "\""

#line 7 "ExportRazorView.cshtml"
, Tuple.Create<string,object,bool> ("", Model.HtmlBase

#line default
#line hidden
, false)
);
WriteLiteral(">\r\n    <title>SilentNotes</title>\r\n    <meta");

WriteLiteral(" charset=\"UTF-8\"");

WriteLiteral(" />\r\n    <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width, initial-scale=1, shrink-to-fit=no, user-scalable=no" +
"\"");

WriteLiteral(">\r\n\r\n    <link");

WriteLiteral(" href=\"bootstrap.min.css\"");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <link");

WriteLiteral(" href=\"silentnotes.css\"");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <link");

WriteAttribute ("href", " href=\"", "\""

#line 14 "ExportRazorView.cshtml"
, Tuple.Create<string,object,bool> ("", Model.Theme.DarkMode ? "silentnotes.dark.css" : "silentnotes.light.css"

#line default
#line hidden
, false)
);
WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n\r\n    <script");

WriteLiteral(" src=\"vue.min.js\"");

WriteLiteral("></script>\r\n    <script");

WriteLiteral(" src=\"jquery-3.5.1.slim.min.js\"");

WriteLiteral("></script>\r\n    <script");

WriteLiteral(" src=\"silentnotes.js\"");

WriteLiteral("></script>\r\n\r\n    <style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n        .background-icon {\r\n            right: -32px;\r\n            top: -108px" +
";\r\n        }\r\n    </style>\r\n    <script>\r\n");


#line 27 "ExportRazorView.cshtml"
        

#line default
#line hidden

#line 27 "ExportRazorView.cshtml"
          WriteLiteral(Model.VueDataBindingScript);

#line default
#line hidden
WriteLiteral("\r\n    </script>\r\n</head>\r\n<body><div");

WriteLiteral(" id=\"vueDataBinding\"");

WriteLiteral(">\r\n    <nav");

WriteLiteral(" id=\"navigation\"");

WriteLiteral(" class=\"d-flex\"");

WriteLiteral(">\r\n        <button");

WriteLiteral(" class=\"nav-item mr-auto\"");

WriteLiteral(" v-on:click=\"GoBackCommand\"");

WriteAttribute ("title", " title=\"", "\""

#line 32 "ExportRazorView.cshtml"
                                    , Tuple.Create<string,object,bool> ("", Model.Language["back"]

#line default
#line hidden
, false)
);
WriteLiteral(">");


#line 32 "ExportRazorView.cshtml"
                                                                                                      WriteLiteral(Model.Icon["arrow-left"]);

#line default
#line hidden
WriteLiteral("</button>\r\n    </nav>\r\n\r\n    <div");

WriteLiteral(" id=\"content\"");

WriteLiteral(" class=\"container-fluid p-4\"");

WriteLiteral(">\r\n        <svg");

WriteLiteral(" class=\"background-icon\"");

WriteLiteral(" width=\'24\'");

WriteLiteral(" height=\'24\'");

WriteLiteral(" viewBox=\'0 0 24 24\'");

WriteLiteral("><use");

WriteLiteral(" xlink:href=\"#svg-export\"");

WriteLiteral(" /></svg>\r\n\r\n        <h2>");


#line 38 "ExportRazorView.cshtml"
       Write(Model.Language["show_export"]);


#line default
#line hidden
WriteLiteral("</h2>\r\n\r\n        <p>");


#line 40 "ExportRazorView.cshtml"
      Write(Model.Language["export_desc"]);


#line default
#line hidden
WriteLiteral("</p>\r\n\r\n        <form");

WriteLiteral(" class=\"mb-4 col-md-9\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"form-check\"");

WriteLiteral(">\r\n              <input");

WriteLiteral(" class=\"form-check-input\"");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" v-model=\"ExportUnprotectedNotes\"");

WriteLiteral(" id=\"ExportUnprotectedNotes\"");

WriteLiteral(">\r\n              <label");

WriteLiteral(" class=\"form-check-label\"");

WriteLiteral(" for=\"ExportUnprotectedNotes\"");

WriteLiteral(">");


#line 45 "ExportRazorView.cshtml"
                                                                      Write(Model.Language["export_unencrypted"]);


#line default
#line hidden
WriteLiteral("</label>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"form-check mb-4\"");

WriteLiteral(">\r\n              <input");

WriteLiteral(" class=\"form-check-input\"");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" v-model=\"ExportProtectedNotes\"");

WriteLiteral(" id=\"ExportProtectedNotes\"");

WriteLiteral(" ");


#line 48 "ExportRazorView.cshtml"
                                                                                                                        if (!Model.CanExportProtectedNotes) { 

#line default
#line hidden

#line 48 "ExportRazorView.cshtml"
                                                                                                                                                          Write("disabled");


#line default
#line hidden

#line 48 "ExportRazorView.cshtml"
                                                                                                                                                                           ; }

#line default
#line hidden
WriteLiteral(">\r\n              <label");

WriteLiteral(" class=\"form-check-label\"");

WriteLiteral(" for=\"ExportProtectedNotes\"");

WriteLiteral(">");


#line 49 "ExportRazorView.cshtml"
                                                                    Write(Model.Language["export_encrypted"]);


#line default
#line hidden
WriteLiteral("</label>\r\n            </div>\r\n\r\n            <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-primary mb-2\"");

WriteLiteral(" v-on:click=\"OkCommand\"");

WriteLiteral(">");


#line 52 "ExportRazorView.cshtml"
                                                                                 Write(Model.Language["ok"]);


#line default
#line hidden
WriteLiteral("</button>\r\n            <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-secondary mb-2\"");

WriteLiteral(" v-on:click=\"CancelCommand\"");

WriteLiteral(">");


#line 53 "ExportRazorView.cshtml"
                                                                                       Write(Model.Language["cancel"]);


#line default
#line hidden
WriteLiteral("</button>\r\n        </form>\r\n    </div>\r\n\r\n    <div hidden>\r\n");


#line 58 "ExportRazorView.cshtml"
        

#line default
#line hidden

#line 58 "ExportRazorView.cshtml"
          WriteLiteral(Model.Icon.LoadIcon("export", new[] { new KeyValuePair<string, string>("id", "svg-export") }));

#line default
#line hidden
WriteLiteral("\r\n    </div>\r\n</div></body>\r\n</html>\r\n");

}
}

// NOTE: this is the default generated helper class. You may choose to extract it to a separate file 
// in order to customize it or share it between multiple templates, and specify the template's base 
// class via the @inherits directive.
public abstract class ExportRazorViewBase
{

		// This field is OPTIONAL, but used by the default implementation of Generate, Write, WriteAttribute and WriteLiteral
		//
		System.IO.TextWriter __razor_writer;

		// This method is OPTIONAL
		//
		/// <summary>Executes the template and returns the output as a string.</summary>
		/// <returns>The template output.</returns>
		public string GenerateString ()
		{
			using (var sw = new System.IO.StringWriter ()) {
				Generate (sw);
				return sw.ToString ();
			}
		}

		// This method is OPTIONAL, you may choose to implement Write and WriteLiteral without use of __razor_writer
		// and provide another means of invoking Execute.
		//
		/// <summary>Executes the template, writing to the provided text writer.</summary>
		/// <param name="writer">The TextWriter to which to write the template output.</param>
		public void Generate (System.IO.TextWriter writer)
		{
			__razor_writer = writer;
			Execute ();
			__razor_writer = null;
		}

		// This method is REQUIRED, but you may choose to implement it differently
		//
		/// <summary>Writes a literal value to the template output without HTML escaping it.</summary>
		/// <param name="value">The literal value.</param>
		protected void WriteLiteral (string value)
		{
			__razor_writer.Write (value);
		}

		// This method is REQUIRED if the template contains any Razor helpers, but you may choose to implement it differently
		//
		/// <summary>Writes a literal value to the TextWriter without HTML escaping it.</summary>
		/// <param name="writer">The TextWriter to which to write the literal.</param>
		/// <param name="value">The literal value.</param>
		protected static void WriteLiteralTo (System.IO.TextWriter writer, string value)
		{
			writer.Write (value);
		}

		// This method is REQUIRED, but you may choose to implement it differently
		//
		/// <summary>Writes a value to the template output, HTML escaping it if necessary.</summary>
		/// <param name="value">The value.</param>
		/// <remarks>The value may be a Action<System.IO.TextWriter>, as returned by Razor helpers.</remarks>
		protected void Write (object value)
		{
			WriteTo (__razor_writer, value);
		}

		// This method is REQUIRED if the template contains any Razor helpers, but you may choose to implement it differently
		//
		/// <summary>Writes an object value to the TextWriter, HTML escaping it if necessary.</summary>
		/// <param name="writer">The TextWriter to which to write the value.</param>
		/// <param name="value">The value.</param>
		/// <remarks>The value may be a Action<System.IO.TextWriter>, as returned by Razor helpers.</remarks>
		protected static void WriteTo (System.IO.TextWriter writer, object value)
		{
			if (value == null)
				return;

			var write = value as Action<System.IO.TextWriter>;
			if (write != null) {
				write (writer);
				return;
			}

			//NOTE: a more sophisticated implementation would write safe and pre-escaped values directly to the
			//instead of double-escaping. See System.Web.IHtmlString in ASP.NET 4.0 for an example of this.
			writer.Write(System.Net.WebUtility.HtmlEncode (value.ToString ()));
		}

		// This method is REQUIRED, but you may choose to implement it differently
		//
		/// <summary>
		/// Conditionally writes an attribute to the template output.
		/// </summary>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="prefix">The prefix of the attribute.</param>
		/// <param name="suffix">The suffix of the attribute.</param>
		/// <param name="values">Attribute values, each specifying a prefix, value and whether it's a literal.</param>
		protected void WriteAttribute (string name, string prefix, string suffix, params Tuple<string,object,bool>[] values)
		{
			WriteAttributeTo (__razor_writer, name, prefix, suffix, values);
		}

		// This method is REQUIRED if the template contains any Razor helpers, but you may choose to implement it differently
		//
		/// <summary>
		/// Conditionally writes an attribute to a TextWriter.
		/// </summary>
		/// <param name="writer">The TextWriter to which to write the attribute.</param>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="prefix">The prefix of the attribute.</param>
		/// <param name="suffix">The suffix of the attribute.</param>
		/// <param name="values">Attribute values, each specifying a prefix, value and whether it's a literal.</param>
		///<remarks>Used by Razor helpers to write attributes.</remarks>
		protected static void WriteAttributeTo (System.IO.TextWriter writer, string name, string prefix, string suffix, params Tuple<string,object,bool>[] values)
		{
			// this is based on System.Web.WebPages.WebPageExecutingBase
			// Copyright (c) Microsoft Open Technologies, Inc.
			// Licensed under the Apache License, Version 2.0
			if (values.Length == 0) {
				// Explicitly empty attribute, so write the prefix and suffix
				writer.Write (prefix);
				writer.Write (suffix);
				return;
			}

			bool first = true;
			bool wroteSomething = false;

			for (int i = 0; i < values.Length; i++) {
				Tuple<string,object,bool> attrVal = values [i];
				string attPrefix = attrVal.Item1;
				object value = attrVal.Item2;
				bool isLiteral = attrVal.Item3;

				if (value == null) {
					// Nothing to write
					continue;
				}

				// The special cases here are that the value we're writing might already be a string, or that the
				// value might be a bool. If the value is the bool 'true' we want to write the attribute name instead
				// of the string 'true'. If the value is the bool 'false' we don't want to write anything.
				//
				// Otherwise the value is another object (perhaps an IHtmlString), and we'll ask it to format itself.
				string stringValue;
				bool? boolValue = value as bool?;
				if (boolValue == true) {
					stringValue = name;
				} else if (boolValue == false) {
					continue;
				} else {
					stringValue = value as string;
				}

				if (first) {
					writer.Write (prefix);
					first = false;
				} else {
					writer.Write (attPrefix);
				}

				if (isLiteral) {
					writer.Write (stringValue ?? value);
				} else {
					WriteTo (writer, stringValue ?? value);
				}
				wroteSomething = true;
			}
			if (wroteSomething) {
				writer.Write (suffix);
			}
		}
		// This method is REQUIRED. The generated Razor subclass will override it with the generated code.
		//
		///<summary>Executes the template, writing output to the Write and WriteLiteral methods.</summary>.
		///<remarks>Not intended to be called directly. Call the Generate method instead.</remarks>
		public abstract void Execute ();

}
}
#pragma warning restore 1591
