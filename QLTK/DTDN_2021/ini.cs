using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace DTDN_2021
{
	public class ini
	{
		public class IniFileName
		{
			public string path;

			[DllImport("kernel32")]
			private static extern int GetPrivateProfileString(string string_0, string string_1, string string_2, StringBuilder stringBuilder_0, int int_0, string string_3);

			[DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
			private static extern int GetPrivateProfileString_1(string string_0, int int_0, string string_1, [MarshalAs(UnmanagedType.LPArray)] byte[] byte_0, int int_1, string string_2);

			[DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
			private static extern int GetPrivateProfileString_2(int int_0, string string_0, string string_1, [MarshalAs(UnmanagedType.LPArray)] byte[] byte_0, int int_1, string string_2);

			public IniFileName(string INIPath)
			{
				path = INIPath;
			}

			public string[] GetSectionNames()
			{
				int num = 500;
				byte[] array;
				int privateProfileString_;
				while (true)
				{
					array = new byte[num];
					privateProfileString_ = GetPrivateProfileString_2(0, "", "", array, num, path);
					if (privateProfileString_ < num - 2)
						break;
					num *= 2;
				}
				return Encoding.ASCII.GetString(array, 0, privateProfileString_ - ((privateProfileString_ > 0) ? 1 : 0)).Split(default(char));
			}

			public string[] GetEntryNames(string section)
			{
				int num = 500;
				byte[] array;
				int privateProfileString_;
				while (true)
				{
					array = new byte[num];
					privateProfileString_ = GetPrivateProfileString_1(section, 0, "", array, num, path);
					if (privateProfileString_ < num - 2)
						break;
					num *= 2;
				}
				return Encoding.ASCII.GetString(array, 0, privateProfileString_ - ((privateProfileString_ > 0) ? 1 : 0)).Split(default(char));
			}

			public object GetEntryValue(string section, string entry)
			{
				int num = 250;
				StringBuilder stringBuilder;
				while (true)
				{
					stringBuilder = new StringBuilder(num);
					if (GetPrivateProfileString(section, entry, "", stringBuilder, num, path) < num - 1)
						break;
					num *= 2;
				}
				return stringBuilder.ToString();
			}
		}

		public class IniFile
		{
			private string string_0;

			private string string_1 = Assembly.GetExecutingAssembly().GetName().Name;

			[DllImport("kernel32", CharSet = CharSet.Unicode)]
			private static extern long WritePrivateProfileString(string string_2, string string_3, string string_4, string string_5);

			[DllImport("kernel32", CharSet = CharSet.Unicode)]
			private static extern int GetPrivateProfileString(string string_2, string string_3, string string_4, StringBuilder stringBuilder_0, int int_0, string string_5);

			public IniFile(string IniPath = null)
			{
				string_0 = new FileInfo(IniPath ?? (string_1 + ".ini")).FullName.ToString();
			}

			[DllImport("kernel32")]
			private static extern uint GetPrivateProfileSectionNames(IntPtr intptr_0, uint uint_0, string string_2);

			public string[] SelectionName(string path)
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(32767);
				uint privateProfileSectionNames = GetPrivateProfileSectionNames(intPtr, 32767u, path);
				if (privateProfileSectionNames == 0)
					return null;
				string text = Marshal.PtrToStringAnsi(intPtr, (int)privateProfileSectionNames).ToString();
				Marshal.FreeCoTaskMem(intPtr);
				return text.Substring(0, text.Length - 1).Split(default(char));
			}

			public string Read(string Key, string Section = null)
			{
				StringBuilder stringBuilder = new StringBuilder(255);
				GetPrivateProfileString(Section ?? string_1, Key, "", stringBuilder, 255, string_0);
				return stringBuilder.ToString();
			}

			public void Write(string Key, string Value, string Section = null)
			{
				WritePrivateProfileString(Section ?? string_1, Key, Value, string_0);
			}

			public void DeleteKey(string Key, string Section = null)
			{
				Write(Key, null, Section ?? string_1);
			}

			public void DeleteSection(string Section = null)
			{
				Write(null, null, Section ?? string_1);
			}

			public bool KeyExists(string Key, string Section = null)
			{
				return Read(Key, Section).Length > 0;
			}
		}

		public static string giaima(string encodedData)
		{
			byte[] bytes = Convert.FromBase64String(encodedData);
			return Encoding.ASCII.GetString(bytes);
		}

		public static string mahoa(string toEncode)
		{
			return Convert.ToBase64String(Encoding.ASCII.GetBytes(toEncode));
		}

		public static string TimeAgo(DateTime dateTime)
		{
			string empty = string.Empty;
			TimeSpan timeSpan = DateTime.Now.Subtract(dateTime);
			if (timeSpan <= TimeSpan.FromSeconds(60.0))
				return $"{timeSpan.Seconds} giây trước";
			if (timeSpan <= TimeSpan.FromMinutes(60.0))
				return (timeSpan.Minutes > 0) ? $"{timeSpan.Minutes} phút trước" : " phút trước";
			if (timeSpan <= TimeSpan.FromHours(24.0))
				return (timeSpan.Hours > 1) ? $"{timeSpan.Hours} giờ trước" : " giờ trước";
			if (timeSpan <= TimeSpan.FromDays(30.0))
				return (timeSpan.Days > 1) ? $"{timeSpan.Days} ngày trước" : " hôm qua";
			if (timeSpan <= TimeSpan.FromDays(365.0))
				return (timeSpan.Days > 30) ? $"{timeSpan.Days / 30} tháng trước" : " tháng trước";
			return (timeSpan.Days > 365) ? $"{timeSpan.Days / 365} năm trước" : " năm trước";
		}
	}
}
