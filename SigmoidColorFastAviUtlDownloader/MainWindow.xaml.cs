using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace SigmoidColorFastAviUtlDownloader
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		//https://www.infoq.com/jp/news/2016/09/HttpClient
		public static HttpClient httpClient = new HttpClient();
		private AviUtlVersionManager aviutl;
		private const string AviutlPathDefault = "aviutlのある場所もしくはインストールする場所";
		public static DependencyProperty AviutlPathProperty = DependencyProperty.Register(
			"AviutlPath",
			typeof(string),
			typeof(MainWindow),
			new PropertyMetadata()
		);
		public static ObservableCollection<RichTextItem> OutMessage { get; } = new ObservableCollection<RichTextItem>();
		public static void WriteOutMessage(RichTextItem item)
		{
			if(null == item.Foreground)
			{
				item.Foreground = new SolidColorBrush(Colors.Black);
			}
			OutMessage.Add(item);
			System.Diagnostics.Debug.WriteLine(item.Text);
		}
		public MainWindow()
		{
			AviutlPath = AviutlPathDefault;
			aviutl = new AviUtlVersionManager(AviutlPath);
			InitializeComponent();
			this.DataContext = this;
			WriteOutMessage(new RichTextItem {
				Text = "SigmoidColorFastAviUtlDownloader起動"
			});
			WriteOutMessage(new RichTextItem
			{
				Text = "SigmoidColorFastAviUtlDownloader起動"
			});
		}
		public string AviutlPath
		{
			get { return GetValue(AviutlPathProperty) as string; }
			set {
				if (AviutlPathDefault != value && !File.Exists(System.IO.Path.Combine(value, "aviutl.exe")))
					throw new IOException("Fail to find Aviutl.exe");
				SetValue(AviutlPathProperty, value);
			}
		}
		public string SampleMoviePath
		{
			get {
				var re = System.IO.Path.Combine(AviutlPath, @"sample\sample.avi");
				if (!File.Exists(re))
					throw new IOException("Fail to find sample movie");
				return re;
			}
		}
		private void RunAviUtl()
		{
			System.Diagnostics.Process.Start(aviutl.AviutlExecPath, '"' + SampleMoviePath + '"');
		}
		private void aviutl_ref_Click(object sender, RoutedEventArgs e)
		{
			var ofd = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
			if (ofd.ShowDialog() != true)
				return;
			this.AviutlPath = ofd.SelectedPath;
		}

		private async void install_update_Click(object sender, RoutedEventArgs e)
		{
			WriteOutMessage(new RichTextItem
			{
				Text = "Download check"
			});

			if (!aviutl.IsLatest()) await aviutl.Download();
		}

		private void run_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
