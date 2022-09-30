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
		private DataGridViewCheckBoxColumn Column4;
		private DataGridViewTextBoxColumn Column1;
		private DataGridViewTextBoxColumn Column2;
		private DataGridViewTextBoxColumn Column3;
		private DataGridViewButtonColumn Login2;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView_0 = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Login2 = new System.Windows.Forms.DataGridViewButtonColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_0)).BeginInit();
            this.groupBox_0.SuspendLayout();
            this.groupBox_1.SuspendLayout();
            this.panel_0.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_0
            // 
            this.dataGridView_0.AllowUserToDeleteRows = false;
            this.dataGridView_0.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_0.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView_0.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridView_0.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_0.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView_0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_0.ColumnHeadersVisible = false;
            this.dataGridView_0.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Login2});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_0.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView_0.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.dataGridView_0.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_0.MultiSelect = false;
            this.dataGridView_0.Name = "dataGridView_0";
            this.dataGridView_0.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_0.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView_0.RowHeadersVisible = false;
            this.dataGridView_0.RowHeadersWidth = 35;
            this.dataGridView_0.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_0.Size = new System.Drawing.Size(527, 221);
            this.dataGridView_0.TabIndex = 0;
            this.dataGridView_0.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_0_CellContentClick);
            this.dataGridView_0.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_0_CellDoubleClick);
            this.dataGridView_0.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_0_KeyDown);
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 30;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Server";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 70;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Tai khoan";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 200;
            // 
            // Login2
            // 
            this.Login2.HeaderText = "Login";
            this.Login2.Name = "Login2";
            this.Login2.ReadOnly = true;
            this.Login2.Text = "Đăng nhập";
            this.Login2.ToolTipText = "zz";
            this.Login2.UseColumnTextForButtonValue = true;
            // 
            // button_0
            // 
            this.button_0.BackColor = System.Drawing.Color.PeachPuff;
            this.button_0.Location = new System.Drawing.Point(170, 35);
            this.button_0.Name = "button_0";
            this.button_0.Size = new System.Drawing.Size(90, 23);
            this.button_0.TabIndex = 1;
            this.button_0.Text = "Thêm";
            this.button_0.UseVisualStyleBackColor = false;
            this.button_0.Click += new System.EventHandler(this.button_0_Click);
            // 
            // textBox_0
            // 
            this.textBox_0.BackColor = System.Drawing.Color.PeachPuff;
            this.textBox_0.Location = new System.Drawing.Point(68, 14);
            this.textBox_0.Name = "textBox_0";
            this.textBox_0.Size = new System.Drawing.Size(100, 20);
            this.textBox_0.TabIndex = 2;
            // 
            // textBox_1
            // 
            this.textBox_1.BackColor = System.Drawing.Color.PeachPuff;
            this.textBox_1.Location = new System.Drawing.Point(68, 37);
            this.textBox_1.Name = "textBox_1";
            this.textBox_1.Size = new System.Drawing.Size(100, 20);
            this.textBox_1.TabIndex = 3;
            this.textBox_1.TextChanged += new System.EventHandler(this.textBox_1_TextChanged);
            // 
            // comboBox_0
            // 
            this.comboBox_0.BackColor = System.Drawing.Color.PeachPuff;
            this.comboBox_0.FormattingEnabled = true;
            this.comboBox_0.Items.AddRange(new object[] {
            "Server 1",
            "Server 2",
            "Server 3",
            "Server 4",
            "Server 5",
            "Server 6",
            "Server 7",
            "Server 8",
            "Server 9",
            "Naga",
            "1 Star"});
            this.comboBox_0.Location = new System.Drawing.Point(171, 13);
            this.comboBox_0.Name = "comboBox_0";
            this.comboBox_0.Size = new System.Drawing.Size(89, 21);
            this.comboBox_0.TabIndex = 4;
            this.comboBox_0.Text = "Server 1";
            // 
            // label_0
            // 
            this.label_0.AutoSize = true;
            this.label_0.Location = new System.Drawing.Point(6, 17);
            this.label_0.Name = "label_0";
            this.label_0.Size = new System.Drawing.Size(58, 13);
            this.label_0.TabIndex = 5;
            this.label_0.Text = "Tài khoản:";
            // 
            // label_1
            // 
            this.label_1.AutoSize = true;
            this.label_1.Location = new System.Drawing.Point(7, 40);
            this.label_1.Name = "label_1";
            this.label_1.Size = new System.Drawing.Size(55, 13);
            this.label_1.TabIndex = 6;
            this.label_1.Text = "Mật khẩu:";
            // 
            // groupBox_0
            // 
            this.groupBox_0.Controls.Add(this.textBox_0);
            this.groupBox_0.Controls.Add(this.label_1);
            this.groupBox_0.Controls.Add(this.textBox_1);
            this.groupBox_0.Controls.Add(this.label_0);
            this.groupBox_0.Controls.Add(this.comboBox_0);
            this.groupBox_0.Controls.Add(this.button_0);
            this.groupBox_0.Location = new System.Drawing.Point(484, 9);
            this.groupBox_0.Name = "groupBox_0";
            this.groupBox_0.Size = new System.Drawing.Size(270, 66);
            this.groupBox_0.TabIndex = 7;
            this.groupBox_0.TabStop = false;
            // 
            // button_1
            // 
            this.button_1.BackColor = System.Drawing.Color.PeachPuff;
            this.button_1.Location = new System.Drawing.Point(484, 153);
            this.button_1.Name = "button_1";
            this.button_1.Size = new System.Drawing.Size(113, 23);
            this.button_1.TabIndex = 8;
            this.button_1.Text = "Chọn/Bỏ chọn all";
            this.button_1.UseVisualStyleBackColor = false;
            this.button_1.Click += new System.EventHandler(this.button_1_Click);
            // 
            // label_2
            // 
            this.label_2.AutoSize = true;
            this.label_2.Location = new System.Drawing.Point(6, 16);
            this.label_2.Name = "label_2";
            this.label_2.Size = new System.Drawing.Size(77, 13);
            this.label_2.TabIndex = 11;
            this.label_2.Text = "Nội dung chat:";
            // 
            // textBox_2
            // 
            this.textBox_2.BackColor = System.Drawing.Color.PeachPuff;
            this.textBox_2.Location = new System.Drawing.Point(89, 13);
            this.textBox_2.Name = "textBox_2";
            this.textBox_2.Size = new System.Drawing.Size(100, 20);
            this.textBox_2.TabIndex = 10;
            this.textBox_2.TextChanged += new System.EventHandler(this.textBox_2_TextChanged);
            // 
            // button_2
            // 
            this.button_2.BackColor = System.Drawing.Color.PeachPuff;
            this.button_2.Location = new System.Drawing.Point(195, 11);
            this.button_2.Name = "button_2";
            this.button_2.Size = new System.Drawing.Size(65, 23);
            this.button_2.TabIndex = 9;
            this.button_2.Text = "Lưu";
            this.button_2.UseVisualStyleBackColor = false;
            this.button_2.Click += new System.EventHandler(this.button_2_Click);
            // 
            // label_3
            // 
            this.label_3.AutoSize = true;
            this.label_3.Location = new System.Drawing.Point(6, 39);
            this.label_3.Name = "label_3";
            this.label_3.Size = new System.Drawing.Size(71, 13);
            this.label_3.TabIndex = 13;
            this.label_3.Text = "Nội dung ctg:";
            // 
            // textBox_3
            // 
            this.textBox_3.BackColor = System.Drawing.Color.PeachPuff;
            this.textBox_3.Location = new System.Drawing.Point(89, 36);
            this.textBox_3.Name = "textBox_3";
            this.textBox_3.Size = new System.Drawing.Size(100, 20);
            this.textBox_3.TabIndex = 12;
            // 
            // button_3
            // 
            this.button_3.BackColor = System.Drawing.Color.PeachPuff;
            this.button_3.Location = new System.Drawing.Point(195, 34);
            this.button_3.Name = "button_3";
            this.button_3.Size = new System.Drawing.Size(65, 23);
            this.button_3.TabIndex = 14;
            this.button_3.Text = "Lưu";
            this.button_3.UseVisualStyleBackColor = false;
            this.button_3.Click += new System.EventHandler(this.button_3_Click);
            // 
            // groupBox_1
            // 
            this.groupBox_1.Controls.Add(this.label_2);
            this.groupBox_1.Controls.Add(this.button_3);
            this.groupBox_1.Controls.Add(this.button_2);
            this.groupBox_1.Controls.Add(this.label_3);
            this.groupBox_1.Controls.Add(this.textBox_2);
            this.groupBox_1.Controls.Add(this.textBox_3);
            this.groupBox_1.Location = new System.Drawing.Point(484, 81);
            this.groupBox_1.Name = "groupBox_1";
            this.groupBox_1.Size = new System.Drawing.Size(270, 66);
            this.groupBox_1.TabIndex = 15;
            this.groupBox_1.TabStop = false;
            this.groupBox_1.Enter += new System.EventHandler(this.groupBox_1_Enter);
            // 
            // panel_0
            // 
            this.panel_0.Controls.Add(this.dataGridView_0);
            this.panel_0.Location = new System.Drawing.Point(12, 12);
            this.panel_0.Name = "panel_0";
            this.panel_0.Size = new System.Drawing.Size(458, 234);
            this.panel_0.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(765, 259);
            this.Controls.Add(this.panel_0);
            this.Controls.Add(this.groupBox_1);
            this.Controls.Add(this.button_1);
            this.Controls.Add(this.groupBox_0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "KaiTeamV3";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_0)).EndInit();
            this.groupBox_0.ResumeLayout(false);
            this.groupBox_0.PerformLayout();
            this.groupBox_1.ResumeLayout(false);
            this.groupBox_1.PerformLayout();
            this.panel_0.ResumeLayout(false);
            this.ResumeLayout(false);

		}
	}
}
