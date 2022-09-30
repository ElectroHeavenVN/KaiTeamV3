using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace CapNhatMod
{
	public class Form1 : Form
	{
		[CompilerGenerated]
		private sealed class Class0
		{
			public Form1 form1_0;

			public DownloadProgressChangedEventArgs downloadProgressChangedEventArgs_0;

			internal void method_0()
			{
				form1_0.progressBar_0.Minimum = 0;
				double num = double.Parse(downloadProgressChangedEventArgs_0.BytesReceived.ToString()) / double.Parse(downloadProgressChangedEventArgs_0.TotalBytesToReceive.ToString()) * 100.0;
				form1_0.label_0.Text = "Trạng thái: đang tải xuống " + $"{num:0.##}" + "%";
				form1_0.progressBar_0.Value = int.Parse(Math.Truncate(num).ToString());
			}
		}

		private WebClient webClient_0;

		public string versiob;

		private IContainer icontainer_0 = null;

		private ProgressBar progressBar_0;

		private Label label_0;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			new Thread((ThreadStart)delegate
			{
				try
				{
					webClient_0 = new WebClient();
					versiob = webClient_0.DownloadString("https://8sao.club/tool/version.txt");
					if (File.ReadAllText("Data\\version") == versiob)
					{
						MessageBox.Show("Chưa có bản cập nhật mới.");
						Application.Exit();
					}
					else
					{
						if (File.Exists("KteamV3.exe"))
							File.Delete("KteamV3.exe");
						if (File.Exists("KaiTeamV3.zip"))
							File.Delete("KaiTeamV3.zip");
						if (File.Exists("Dragonboy_vn_187.exe"))
							File.Delete("Dragonboy_vn_187.exe");
						Directory.Delete("Dragonboy_vn_187_Data", true);
						webClient_0.DownloadProgressChanged += webClient_0_DownloadProgressChanged;
						webClient_0.DownloadFileCompleted += webClient_0_DownloadFileCompleted;
						capnhat();
					}
				}
				catch
				{
					MessageBox.Show("Lỗi không xác định.");
					Application.Exit();
				}
			}).Start();
		}

		private void webClient_0_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			File.WriteAllText("Data\\version", versiob);
			ZipFile.ExtractToDirectory(Application.StartupPath + "\\KaiTeamV3.zip", Application.StartupPath);
			File.Delete(Application.StartupPath + "\\KaiTeamV3.zip");
			MessageBox.Show("Thành công.");
			Application.Exit();
		}

		private void webClient_0_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			Invoke((MethodInvoker)delegate
			{
				progressBar_0.Minimum = 0;
				double num = double.Parse(e.BytesReceived.ToString()) / double.Parse(e.TotalBytesToReceive.ToString()) * 100.0;
				label_0.Text = "Trạng thái: đang tải xuống " + $"{num:0.##}" + "%";
				progressBar_0.Value = int.Parse(Math.Truncate(num).ToString());
			});
		}

		public void capnhat()
		{
			Uri uri = new Uri("https://8sao.club/tool/KaiTeamV3.zip");
			string fileName = Path.GetFileName(uri.AbsolutePath);
			webClient_0.DownloadFileAsync(uri, Application.StartupPath + "/" + fileName);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && icontainer_0 != null)
				icontainer_0.Dispose();
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			progressBar_0 = new ProgressBar();
			label_0 = new Label();
			SuspendLayout();
			progressBar_0.Location = new Point(16, 29);
			progressBar_0.Name = "progressBar1";
			progressBar_0.Size = new Size(431, 23);
			progressBar_0.TabIndex = 0;
			label_0.AutoSize = true;
			label_0.Location = new Point(13, 13);
			label_0.Name = "label1";
			label_0.Size = new Size(58, 13);
			label_0.TabIndex = 1;
			label_0.Text = "Trạng thái:";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(463, 70);
			base.Controls.Add(label_0);
			base.Controls.Add(progressBar_0);
			base.Icon = (Icon)new ComponentResourceManager(typeof(Form1)).GetObject("$this.Icon");
			base.Name = "Form1";
			Text = "Cập nhật mod";
			base.Load += Form1_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		[CompilerGenerated]
		private void method_1()
		{
			try
			{
				webClient_0 = new WebClient();
				versiob = webClient_0.DownloadString("https://8sao.club/tool/version.txt");
				if (File.ReadAllText("Data\\version") == versiob)
				{
					MessageBox.Show("Chưa có bản cập nhật mới.");
					Application.Exit();
					return;
				}
				if (File.Exists("KteamV3.exe"))
					File.Delete("KteamV3.exe");
				if (File.Exists("KaiTeamV3.zip"))
					File.Delete("KaiTeamV3.zip");
				if (File.Exists("Dragonboy_vn_187.exe"))
					File.Delete("Dragonboy_vn_187.exe");
				Directory.Delete("Dragonboy_vn_187_Data", true);
				webClient_0.DownloadProgressChanged += webClient_0_DownloadProgressChanged;
				webClient_0.DownloadFileCompleted += webClient_0_DownloadFileCompleted;
				capnhat();
			}
			catch
			{
				MessageBox.Show("Lỗi không xác định.");
				Application.Exit();
			}
		}
	}
}
