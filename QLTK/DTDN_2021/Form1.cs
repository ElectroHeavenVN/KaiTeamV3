using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace DTDN_2021
{
	public class Form1 : Form
	{
		public string server = "server";

		public string taikhoan = "taikhoan";

		public string matkhau = "matkhau";

		private ini.IniFile iniFile_0 = new ini.IniFile("Data\\Acc.ini");

		public Thread TLogin;

		private IContainer icontainer_0 = null;

		private DataGridView dataGridView_0;

		private Button button_0;

		private TextBox textBox_0;

		private TextBox textBox_1;

		private ComboBox comboBox_0;

		private Label label_0;

		private Label label_1;

		private GroupBox groupBox_0;

		private Button button_1;

		private Label label_2;

		private TextBox textBox_2;

		private Button button_2;

		private Label label_3;

		private TextBox textBox_3;

		private Button button_3;

		private GroupBox groupBox_1;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn_0;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_0;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_2;

		private DataGridViewButtonColumn dataGridViewButtonColumn_0;

		private Panel panel_0;

		public Form1()
		{
			InitializeComponent();
			dataGridView_0.AllowUserToResizeColumns = false;
			base.Opacity = 0.93;
			dataGridView_0.BackgroundColor = BackColor;
			dataGridView_0.RowsDefaultCellStyle.BackColor = BackColor;
			dataGridView_0.AlternatingRowsDefaultCellStyle.BackColor = BackColor;
			dataGridView_0.GridColor = Color.LightGray;
			((DataGridViewButtonColumn)dataGridView_0.Columns["Login2"]).FlatStyle = FlatStyle.Popup;
			((DataGridViewCheckBoxColumn)dataGridView_0.Columns["Column4"]).FlatStyle = FlatStyle.Popup;
			dataGridView_0.ReadOnly = false;
			dataGridView_0.Columns[1].ReadOnly = true;
			dataGridView_0.Columns[2].ReadOnly = true;
			dataGridView_0.Columns[3].ReadOnly = true;
			dataGridView_0.Columns[4].ReadOnly = true;
			textBox_2.Text = File.ReadAllText("Data\\ndchat.txt");
			textBox_3.Text = File.ReadAllText("Data\\ndctg.txt");
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			fLoad();
			checkupdate();
		}

		public void checkupdate()
		{
			Thread thread = new Thread((ThreadStart)delegate
			{
				string obj = new WebClient().DownloadString("https://8sao.club/tool/version.txt");
				if (File.ReadAllText("Data\\version") != obj)
				{
					MessageBox.Show("Có cập nhật mới.");
					Process.Start("CapNhatMod.exe");
					Application.Exit();
				}
			});
			thread.IsBackground = true;
			thread.Start();
		}

		public void fLoad()
		{
			foreach (DataGridViewColumn column in dataGridView_0.Columns)
			{
				column.SortMode = DataGridViewColumnSortMode.NotSortable;
			}
			int i = 0;
			string[] array = new ini.IniFile().SelectionName(Environment.CurrentDirectory + "/Data/Acc.ini");
			if (array != null)
			{
				for (; i < array.Length; i++)
				{
					string value = array[i];
					string encodedData = iniFile_0.Read(taikhoan, array[i]);
					string encodedData2 = iniFile_0.Read(server, array[i]);
					DataGridViewRow dataGridViewRow = (DataGridViewRow)dataGridView_0.Rows[0].Clone();
					dataGridViewRow.Cells[1].Value = value;
					dataGridViewRow.Cells[2].Value = ini.giaima(encodedData2);
					dataGridViewRow.Cells[3].Value = ini.giaima(encodedData);
					dataGridView_0.Rows.Add(dataGridViewRow);
				}
			}
		}

		private void dataGridView_0_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dataGridView_0.Columns[e.ColumnIndex].Name == "Login2")
			{
				int rowIndex = e.RowIndex;
				DataGridViewRow dataGridViewRow = dataGridView_0.Rows[rowIndex];
				string text = dataGridViewRow.Cells[1].Value.ToString();
				LoginGame(text, dataGridViewRow.Cells[2].Value.ToString(), dataGridViewRow.Cells[3].Value.ToString(), ini.giaima(iniFile_0.Read(matkhau, text)), "2");
			}
			else if (dataGridView_0.Columns[e.ColumnIndex].Name == "loginwin")
			{
				MessageBox.Show("login 3");
			}
			else if (dataGridView_0.Columns[e.ColumnIndex].Name == "loginphu")
			{
				MessageBox.Show("login 4");
			}
		}

		public void LoginGame(string id, string server, string tkw, string mkw, string type)
		{
			TLogin = new Thread((ThreadStart)delegate
			{
				if (!(type == "0") && !(type == "1") && type == "2")
				{
					File.WriteAllText("Data\\Login", id + "|" + server + "|" + tkw + "|" + mkw);
					Process.Start("Dragonboy_vn_187.exe");
					Thread.Sleep(500);
					IntPtr zero = IntPtr.Zero;
					SendText(FindWindowHandle(null, "Dragonboy186"), tkw);
				}
			});
			TLogin.Start();
		}

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string string_0, string string_1);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

		public static IntPtr FindWindowHandle(string className, string windowName)
		{
			return FindWindow(className, windowName);
		}

		public static void SendText(IntPtr handle, string text)
		{
			SendMessage(handle, 12, 0, text);
		}

		private void button_0_Click(object sender, EventArgs e)
		{
			int num = 0;
			ini.IniFile iniFile = new ini.IniFile("Data\\Acc.ini");
			while (!(iniFile.Read(taikhoan, num.ToString() ?? "") == ""))
			{
				num++;
			}
			iniFile.Write(taikhoan, ini.mahoa(textBox_0.Text), num.ToString() ?? "");
			iniFile.Write(matkhau, ini.mahoa(textBox_1.Text), num.ToString() ?? "");
			iniFile.Write(server, ini.mahoa(comboBox_0.Text), num.ToString() ?? "");
			DataGridViewRow dataGridViewRow = (DataGridViewRow)dataGridView_0.Rows[0].Clone();
			dataGridViewRow.Cells[1].Value = num;
			dataGridViewRow.Cells[2].Value = comboBox_0.Text;
			dataGridViewRow.Cells[3].Value = textBox_0.Text;
			dataGridView_0.Rows.Add(dataGridViewRow);
		}

		private void dataGridView_0_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Delete)
				return;
			string text = "";
			foreach (DataGridViewRow selectedRow in dataGridView_0.SelectedRows)
			{
				text = selectedRow.Cells[1].Value.ToString();
			}
			if (text == "")
				return;
			switch (MessageBox.Show("Mày muốn xóa " + text + " à?", "Xác nhận cái", MessageBoxButtons.YesNo))
			{
			case DialogResult.Yes:
				new ini.IniFile("Data\\Acc.ini").DeleteSection(text ?? "");
				{
					foreach (DataGridViewRow selectedRow2 in dataGridView_0.SelectedRows)
					{
						dataGridView_0.Rows.RemoveAt(selectedRow2.Index);
					}
					break;
				}
			case DialogResult.No:
			{
				DialogResult dialogResult = DialogResult.Abort;
				break;
			}
			}
		}

		private void dataGridView_0_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dataGridView_0.CurrentRow.Cells["Column4"].Value != null && (bool)dataGridView_0.CurrentRow.Cells["Column4"].Value)
			{
				dataGridView_0.CurrentRow.Cells["Column4"].Value = false;
				dataGridView_0.CurrentRow.Cells["Column4"].Value = null;
			}
			else if (dataGridView_0.CurrentRow.Cells["Column4"].Value == null)
			{
				dataGridView_0.CurrentRow.Cells["Column4"].Value = true;
			}
		}

		private void button_1_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow item in (IEnumerable)dataGridView_0.Rows)
			{
				DataGridViewCheckBoxCell dataGridViewCheckBoxCell = (DataGridViewCheckBoxCell)item.Cells[0];
				dataGridViewCheckBoxCell.Value = dataGridViewCheckBoxCell.Value == null || !(bool)dataGridViewCheckBoxCell.Value;
			}
		}

		private void textBox_1_TextChanged(object sender, EventArgs e)
		{
		}

		private void groupBox_1_Enter(object sender, EventArgs e)
		{
		}

		private void textBox_2_TextChanged(object sender, EventArgs e)
		{
		}

		private void button_2_Click(object sender, EventArgs e)
		{
			File.WriteAllText("Data\\ndchat.txt", textBox_2.Text);
		}

		private void button_3_Click(object sender, EventArgs e)
		{
			File.WriteAllText("Data\\ndctg.txt", textBox_3.Text);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && icontainer_0 != null)
				icontainer_0.Dispose();
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DTDN_2021.Form1));
			this.dataGridView_0 = new System.Windows.Forms.DataGridView();
			this.dataGridViewCheckBoxColumn_0 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn_0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewButtonColumn_0 = new System.Windows.Forms.DataGridViewButtonColumn();
			this.button_0 = new System.Windows.Forms.Button();
			this.textBox_0 = new System.Windows.Forms.TextBox();
			this.textBox_1 = new System.Windows.Forms.TextBox();
			this.comboBox_0 = new System.Windows.Forms.ComboBox();
			this.label_0 = new System.Windows.Forms.Label();
			this.label_1 = new System.Windows.Forms.Label();
			this.groupBox_0 = new System.Windows.Forms.GroupBox();
			this.button_1 = new System.Windows.Forms.Button();
			this.label_2 = new System.Windows.Forms.Label();
			this.textBox_2 = new System.Windows.Forms.TextBox();
			this.button_2 = new System.Windows.Forms.Button();
			this.label_3 = new System.Windows.Forms.Label();
			this.textBox_3 = new System.Windows.Forms.TextBox();
			this.button_3 = new System.Windows.Forms.Button();
			this.groupBox_1 = new System.Windows.Forms.GroupBox();
			this.panel_0 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)this.dataGridView_0).BeginInit();
			this.groupBox_0.SuspendLayout();
			this.groupBox_1.SuspendLayout();
			this.panel_0.SuspendLayout();
			base.SuspendLayout();
			this.dataGridView_0.AllowUserToDeleteRows = false;
			this.dataGridView_0.AllowUserToResizeRows = false;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			dataGridViewCellStyle.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView_0.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridView_0.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
			this.dataGridView_0.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView_0.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView_0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_0.ColumnHeadersVisible = false;
			this.dataGridView_0.Columns.AddRange(this.dataGridViewCheckBoxColumn_0, this.dataGridViewTextBoxColumn_0, this.dataGridViewTextBoxColumn_1, this.dataGridViewTextBoxColumn_2, this.dataGridViewButtonColumn_0);
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Red;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView_0.DefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView_0.GridColor = System.Drawing.Color.FromArgb(255, 128, 255);
			this.dataGridView_0.Location = new System.Drawing.Point(3, 3);
			this.dataGridView_0.MultiSelect = false;
			this.dataGridView_0.Name = "dataGridView1";
			this.dataGridView_0.ReadOnly = true;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(128, 255, 128);
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView_0.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridView_0.RowHeadersVisible = false;
			this.dataGridView_0.RowHeadersWidth = 35;
			this.dataGridView_0.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_0.Size = new System.Drawing.Size(527, 221);
			this.dataGridView_0.TabIndex = 0;
			this.dataGridView_0.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView_0_CellContentClick);
			this.dataGridView_0.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView_0_CellDoubleClick);
			this.dataGridView_0.KeyDown += new System.Windows.Forms.KeyEventHandler(dataGridView_0_KeyDown);
			this.dataGridViewCheckBoxColumn_0.HeaderText = "Column4";
			this.dataGridViewCheckBoxColumn_0.Name = "Column4";
			this.dataGridViewCheckBoxColumn_0.ReadOnly = true;
			this.dataGridViewCheckBoxColumn_0.Width = 30;
			this.dataGridViewTextBoxColumn_0.HeaderText = "ID";
			this.dataGridViewTextBoxColumn_0.Name = "Column1";
			this.dataGridViewTextBoxColumn_0.ReadOnly = true;
			this.dataGridViewTextBoxColumn_0.Width = 50;
			this.dataGridViewTextBoxColumn_1.HeaderText = "Server";
			this.dataGridViewTextBoxColumn_1.Name = "Column2";
			this.dataGridViewTextBoxColumn_1.ReadOnly = true;
			this.dataGridViewTextBoxColumn_1.Width = 70;
			this.dataGridViewTextBoxColumn_2.HeaderText = "Tai khoan";
			this.dataGridViewTextBoxColumn_2.Name = "Column3";
			this.dataGridViewTextBoxColumn_2.ReadOnly = true;
			this.dataGridViewTextBoxColumn_2.Width = 200;
			this.dataGridViewButtonColumn_0.HeaderText = "Login";
			this.dataGridViewButtonColumn_0.Name = "Login2";
			this.dataGridViewButtonColumn_0.ReadOnly = true;
			this.dataGridViewButtonColumn_0.Text = "Đăng nhập";
			this.dataGridViewButtonColumn_0.ToolTipText = "zz";
			this.dataGridViewButtonColumn_0.UseColumnTextForButtonValue = true;
			this.button_0.BackColor = System.Drawing.Color.PeachPuff;
			this.button_0.Location = new System.Drawing.Point(170, 35);
			this.button_0.Name = "BtnAdd";
			this.button_0.Size = new System.Drawing.Size(90, 23);
			this.button_0.TabIndex = 1;
			this.button_0.Text = "Thêm";
			this.button_0.UseVisualStyleBackColor = false;
			this.button_0.Click += new System.EventHandler(button_0_Click);
			this.textBox_0.BackColor = System.Drawing.Color.PeachPuff;
			this.textBox_0.Location = new System.Drawing.Point(68, 14);
			this.textBox_0.Name = "txbTk";
			this.textBox_0.Size = new System.Drawing.Size(100, 20);
			this.textBox_0.TabIndex = 2;
			this.textBox_1.BackColor = System.Drawing.Color.PeachPuff;
			this.textBox_1.Location = new System.Drawing.Point(68, 37);
			this.textBox_1.Name = "txbMk";
			this.textBox_1.Size = new System.Drawing.Size(100, 20);
			this.textBox_1.TabIndex = 3;
			this.textBox_1.TextChanged += new System.EventHandler(textBox_1_TextChanged);
			this.comboBox_0.BackColor = System.Drawing.Color.PeachPuff;
			this.comboBox_0.FormattingEnabled = true;
			this.comboBox_0.Items.AddRange(new object[11]
			{
				"Server 1", "Server 2", "Server 3", "Server 4", "Server 5", "Server 6", "Server 7", "Server 8", "Server 9", "Naga",
				"1 Star"
			});
			this.comboBox_0.Location = new System.Drawing.Point(171, 13);
			this.comboBox_0.Name = "comboBox1";
			this.comboBox_0.Size = new System.Drawing.Size(89, 21);
			this.comboBox_0.TabIndex = 4;
			this.comboBox_0.Text = "Server 1";
			this.label_0.AutoSize = true;
			this.label_0.Location = new System.Drawing.Point(6, 17);
			this.label_0.Name = "label1";
			this.label_0.Size = new System.Drawing.Size(58, 13);
			this.label_0.TabIndex = 5;
			this.label_0.Text = "Tài khoản:";
			this.label_1.AutoSize = true;
			this.label_1.Location = new System.Drawing.Point(7, 40);
			this.label_1.Name = "label2";
			this.label_1.Size = new System.Drawing.Size(55, 13);
			this.label_1.TabIndex = 6;
			this.label_1.Text = "Mật khẩu:";
			this.groupBox_0.Controls.Add(this.textBox_0);
			this.groupBox_0.Controls.Add(this.label_1);
			this.groupBox_0.Controls.Add(this.textBox_1);
			this.groupBox_0.Controls.Add(this.label_0);
			this.groupBox_0.Controls.Add(this.comboBox_0);
			this.groupBox_0.Controls.Add(this.button_0);
			this.groupBox_0.Location = new System.Drawing.Point(484, 9);
			this.groupBox_0.Name = "groupBox1";
			this.groupBox_0.Size = new System.Drawing.Size(270, 66);
			this.groupBox_0.TabIndex = 7;
			this.groupBox_0.TabStop = false;
			this.button_1.BackColor = System.Drawing.Color.PeachPuff;
			this.button_1.Location = new System.Drawing.Point(1077, 500);
			this.button_1.Name = "chonAll";
			this.button_1.Size = new System.Drawing.Size(113, 23);
			this.button_1.TabIndex = 8;
			this.button_1.Text = "Chọn/Bỏ chọn all";
			this.button_1.UseVisualStyleBackColor = false;
			this.button_1.Click += new System.EventHandler(button_1_Click);
			this.label_2.AutoSize = true;
			this.label_2.Location = new System.Drawing.Point(6, 16);
			this.label_2.Name = "label3";
			this.label_2.Size = new System.Drawing.Size(77, 13);
			this.label_2.TabIndex = 11;
			this.label_2.Text = "Nội dung chat:";
			this.textBox_2.BackColor = System.Drawing.Color.PeachPuff;
			this.textBox_2.Location = new System.Drawing.Point(89, 13);
			this.textBox_2.Name = "textBox1";
			this.textBox_2.Size = new System.Drawing.Size(100, 20);
			this.textBox_2.TabIndex = 10;
			this.textBox_2.TextChanged += new System.EventHandler(textBox_2_TextChanged);
			this.button_2.BackColor = System.Drawing.Color.PeachPuff;
			this.button_2.Location = new System.Drawing.Point(195, 11);
			this.button_2.Name = "button1";
			this.button_2.Size = new System.Drawing.Size(65, 23);
			this.button_2.TabIndex = 9;
			this.button_2.Text = "Lưu";
			this.button_2.UseVisualStyleBackColor = false;
			this.button_2.Click += new System.EventHandler(button_2_Click);
			this.label_3.AutoSize = true;
			this.label_3.Location = new System.Drawing.Point(6, 39);
			this.label_3.Name = "label4";
			this.label_3.Size = new System.Drawing.Size(71, 13);
			this.label_3.TabIndex = 13;
			this.label_3.Text = "Nội dung ctg:";
			this.textBox_3.BackColor = System.Drawing.Color.PeachPuff;
			this.textBox_3.Location = new System.Drawing.Point(89, 36);
			this.textBox_3.Name = "textBox2";
			this.textBox_3.Size = new System.Drawing.Size(100, 20);
			this.textBox_3.TabIndex = 12;
			this.button_3.BackColor = System.Drawing.Color.PeachPuff;
			this.button_3.Location = new System.Drawing.Point(195, 34);
			this.button_3.Name = "button2";
			this.button_3.Size = new System.Drawing.Size(65, 23);
			this.button_3.TabIndex = 14;
			this.button_3.Text = "Lưu";
			this.button_3.UseVisualStyleBackColor = false;
			this.button_3.Click += new System.EventHandler(button_3_Click);
			this.groupBox_1.Controls.Add(this.label_2);
			this.groupBox_1.Controls.Add(this.button_3);
			this.groupBox_1.Controls.Add(this.button_2);
			this.groupBox_1.Controls.Add(this.label_3);
			this.groupBox_1.Controls.Add(this.textBox_2);
			this.groupBox_1.Controls.Add(this.textBox_3);
			this.groupBox_1.Location = new System.Drawing.Point(484, 81);
			this.groupBox_1.Name = "groupBox2";
			this.groupBox_1.Size = new System.Drawing.Size(270, 66);
			this.groupBox_1.TabIndex = 15;
			this.groupBox_1.TabStop = false;
			this.groupBox_1.Enter += new System.EventHandler(groupBox_1_Enter);
			this.panel_0.Controls.Add(this.dataGridView_0);
			this.panel_0.Location = new System.Drawing.Point(12, 12);
			this.panel_0.Name = "panel1";
			this.panel_0.Size = new System.Drawing.Size(458, 234);
			this.panel_0.TabIndex = 16;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.PeachPuff;
			base.ClientSize = new System.Drawing.Size(769, 252);
			base.Controls.Add(this.panel_0);
			base.Controls.Add(this.groupBox_1);
			base.Controls.Add(this.button_1);
			base.Controls.Add(this.groupBox_0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "Form1";
			this.Text = "KaiTeamV3";
			base.Load += new System.EventHandler(Form1_Load);
			((System.ComponentModel.ISupportInitialize)this.dataGridView_0).EndInit();
			this.groupBox_0.ResumeLayout(false);
			this.groupBox_0.PerformLayout();
			this.groupBox_1.ResumeLayout(false);
			this.groupBox_1.PerformLayout();
			this.panel_0.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
