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

namespace SigmoidColorFastAviUtlDownloader
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		private const string AviutlPathDefault = "aviutlのある場所もしくはインストールする場所";
		public static DependencyProperty AviutlPathProperty = DependencyProperty.Register(
			"AviutlPath",
			typeof(string),
			typeof(MainWindow),
			new PropertyMetadata()
		);
		private ObservableCollection<RichTextItem> OutMessage { get; } = new ObservableCollection<RichTextItem>();
		public MainWindow()
		{
			AviutlPath = AviutlPathDefault;
			InitializeComponent();
			this.DataContext = this;
			OutMessage.Add(new RichTextItem {
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
		public string AviutlExecPath
		{
			get { return System.IO.Path.Combine(AviutlPath,"aviutl.exe"); }
			set { AviutlPath = System.IO.Path.GetDirectoryName(value); }
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
			System.Diagnostics.Process.Start(AviutlExecPath, '"' + SampleMoviePath + '"');
		}
		private void aviutl_ref_Click(object sender, RoutedEventArgs e)
		{
			var ofd = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
			if (ofd.ShowDialog() != true)
				return;
			this.AviutlPath = ofd.SelectedPath;
		}

		private void install_update_Click(object sender, RoutedEventArgs e)
		{

		}

		private void run_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
