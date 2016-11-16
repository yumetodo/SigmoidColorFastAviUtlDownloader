using Microsoft.Win32;
using System;
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
		public MainWindow()
		{
			AviutlPath = AviutlPathDefault;
			InitializeComponent();
		}
		public string AviutlPath
		{
			get { return GetValue(AviutlPathProperty) as string; }
			set {
				if (AviutlPathDefault != value && !System.IO.File.Exists(value))
					throw new ArgumentException("Fail to find Aviutl.exe");
				SetValue(AviutlPathProperty, value);
			}
		}
		private void aviutl_ref_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "aviutl.exe|*.exe|すべてのファイル(*.*)|*.*";
			if (ofd.ShowDialog() != true)
				return;
			this.AviutlPath = ofd.FileName;

		}

		private void install_update_Click(object sender, RoutedEventArgs e)
		{

		}

		private void run_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
