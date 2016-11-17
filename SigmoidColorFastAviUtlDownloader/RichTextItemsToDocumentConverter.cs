using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Documents;

namespace SigmoidColorFastAviUtlDownloader
{
	public class RichTextItemsToDocumentConverter : IValueConverter
	{
		public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var doc = new FlowDocument();

			foreach (var item in value as ICollection<RichTextItem>)
			{
				var paragraph = new Paragraph(new Run(item.Text))
				{
					Foreground = item.Foreground,
					FontWeight = item.FontWeight,
					Margin = item.Margin,
				};

				doc.Blocks.Add(paragraph);
			}

			return doc;
		}

		public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new System.NotImplementedException();
		}
	}
}
