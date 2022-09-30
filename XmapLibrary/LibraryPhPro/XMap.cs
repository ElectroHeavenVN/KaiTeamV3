using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace LibraryPhPro
{
	public class XMap : IActionListener
	{
		public sealed class GClass0
		{
			public int int_0;

			public int int_1;

			public int int_2;

			public GClass0(int int_3, int int_4, int int_5)
			{
				int_0 = int_3;
				int_1 = int_4;
				int_2 = int_5;
			}

			public void method_0()
			{
				if (int_2 == -1 && int_1 == -1)
				{
					Waypoint waypoint = method_1();
					if (waypoint != null)
						smethod_1(waypoint);
				}
				else if (int_1 == -2 && int_2 != -1)
				{
					Service.gI().requestMapSelect(int_2);
				}
				else if (int_1 != -1 && int_2 != -1)
				{
					Service.gI().openMenu(int_1);
					Service.gI().confirmMenu(0, (sbyte)int_2);
				}
			}

			private Waypoint method_1()
			{
				int num = 0;
				Waypoint waypoint;
				while (true)
				{
					if (num < TileMap.vGo.size())
					{
						waypoint = (Waypoint)TileMap.vGo.elementAt(num);
						if (method_2().Equals(smethod_0(waypoint.popup)))
							break;
						num++;
						continue;
					}
					return null;
				}
				return waypoint;
			}

			private static string smethod_0(PopUp popUp_0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < popUp_0.says.Length; i++)
				{
					stringBuilder.Append(popUp_0.says[i]);
					stringBuilder.Append(" ");
				}
				return stringBuilder.ToString().Trim();
			}

			private string method_2()
			{
				return TileMap.mapNames[int_0];
			}

			private static void smethod_1(Waypoint waypoint_0)
			{
				int num = -1;
				int num2 = -1;
				num = ((waypoint_0.maxX < 60) ? 15 : ((waypoint_0.minX <= TileMap.pxw - 60) ? ((waypoint_0.minX + waypoint_0.maxX) / 2) : (TileMap.pxw - 15)));
				num2 = waypoint_0.maxY;
				if (num != -1 && num2 != -1)
				{
					smethod_2(num, num2);
					if (waypoint_0.isOffline)
						Service.gI().getMapOffline();
					else
						Service.gI().requestChangeMap();
				}
				else
					GameScr.info1.addInfo("Có lỗi xảy ra", 0);
			}

			private static void smethod_2(int int_3, int int_4)
			{
				Char.myCharz().cx = int_3;
				Char.myCharz().cy = int_4;
				Service.gI().charMove();
				Char.myCharz().cx = int_3;
				Char.myCharz().cy = int_4 + 1;
				Service.gI().charMove();
				Char.myCharz().cx = int_3;
				Char.myCharz().cy = int_4;
				Service.gI().charMove();
			}
		}

		private static XMap xmap_0;

		public Dictionary<int, List<GClass0>> dictionary_0;

		public Dictionary<string, int[]> dictionary_1;

		public Thread thread_0;

		public static bool isXMapRuning;

		public static bool isUseCapsual;

		public XMap()
		{
			dictionary_0 = new Dictionary<int, List<GClass0>>();
			dictionary_1 = new Dictionary<string, int[]>();
			method_0();
			method_1();
			method_2();
		}

		public static XMap gI()
		{
			if (xmap_0 == null)
				xmap_0 = new XMap();
			return xmap_0;
		}

		private void method_0()
		{
			List<int[]> list = new List<int[]>();
			StreamReader streamReader = File.OpenText("DataXMap.txt");
			while (!streamReader.ReadLine().Trim().Equals("Waypoint"))
			{
			}
			while (true)
			{
				string text = streamReader.ReadLine().Trim();
				if (text.Equals("Waypoint_End"))
					break;
				string[] array = text.Trim().Split(' ');
				List<int> list2 = new List<int>();
				for (int i = 0; i < array.Length; i++)
				{
					list2.Add(int.Parse(array[i]));
				}
				list.Add(list2.ToArray());
			}
			int[][] array2 = list.ToArray();
			for (int j = 0; j < array2.Length; j++)
			{
				for (int k = 0; k < array2[j].Length; k++)
				{
					if (!dictionary_0.ContainsKey(array2[j][k]))
						dictionary_0.Add(array2[j][k], new List<GClass0>());
					if (k != 0)
						dictionary_0[array2[j][k]].Add(new GClass0(array2[j][k - 1], -1, -1));
					if (k != array2[j].Length - 1)
						dictionary_0[array2[j][k]].Add(new GClass0(array2[j][k + 1], -1, -1));
				}
			}
		}

		private void method_1()
		{
			StreamReader streamReader = File.OpenText("DataXMap.txt");
			while (!streamReader.ReadLine().Trim().Equals("Npc"))
			{
			}
			while (true)
			{
				string text = streamReader.ReadLine().Trim();
				if (!text.Equals("Npc_End"))
				{
					string[] array = text.Trim().Split(' ');
					int[] array2 = new int[4];
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i] = int.Parse(array[i]);
					}
					if (!dictionary_0.ContainsKey(array2[0]))
						dictionary_0.Add(array2[0], new List<GClass0>());
					dictionary_0[array2[0]].Add(new GClass0(array2[1], array2[2], array2[3]));
					continue;
				}
				break;
			}
		}

		private void method_2()
		{
			StreamReader streamReader = File.OpenText("DataXMap.txt");
			while (!streamReader.ReadLine().Trim().Equals("GroupMap"))
			{
			}
			while (true)
			{
				string text = streamReader.ReadLine().Trim();
				if (!text.Equals("GroupMap_End"))
				{
					string key = text;
					string[] array = streamReader.ReadLine().Trim().Trim()
						.Split(' ');
					int[] array2 = new int[array.Length];
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i] = int.Parse(array[i]);
					}
					dictionary_1.Add(key, array2);
					continue;
				}
				break;
			}
		}

		public void method_3()
		{
			MyVector myVector = new MyVector();
			foreach (KeyValuePair<string, int[]> item in dictionary_1)
			{
				myVector.addElement(new Command(item.Key, this, 1, item.Value));
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		public void method_4(int[] int_0)
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < int_0.Length; i++)
			{
				if ((Char.myCharz().cgender != 0 || (int_0[i] != 22 && int_0[i] != 23)) && (Char.myCharz().cgender != 1 || (int_0[i] != 21 && int_0[i] != 23)) && (Char.myCharz().cgender != 2 || (int_0[i] != 21 && int_0[i] != 22)))
					myVector.addElement(new Command(TileMap.mapNames[int_0[i]], this, 2, int_0[i]));
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		private void method_5(int int_0)
		{
			foreach (GClass0 item in dictionary_0[TileMap.mapID])
			{
				if (item.int_0 == int_0)
				{
					item.method_0();
					return;
				}
			}
			GameScr.info1.addInfo("không thể thực hiện", 0);
		}

		public static int smethod_0(string string_0)
		{
			if (string_0.Equals("Về nhà"))
				return 21 + Char.myCharz().cgender;
			if (string_0.Equals("Trạm tàu vũ trụ"))
				return 24 + Char.myCharz().cgender;
			string_0.Replace("Về chỗ cũ: ", "");
			string_0 = string_0.Trim();
			int num = 0;
			while (true)
			{
				if (num < TileMap.mapNames.Length)
				{
					if (string_0 == TileMap.mapNames[num])
						break;
					num++;
					continue;
				}
				return -1;
			}
			return num;
		}

		private void method_6()
		{
			if (GameCanvas.panel.mapNames == null || GameCanvas.panel.mapNames.Length == 0)
				return;
			if (!dictionary_0.ContainsKey(TileMap.mapID))
				dictionary_0.Add(TileMap.mapID, new List<GClass0>());
			for (int i = 0; i < GameCanvas.panel.mapNames.Length; i++)
			{
				int num = smethod_0(GameCanvas.panel.mapNames[i]);
				if (num != -1)
					dictionary_0[TileMap.mapID].Add(new GClass0(num, -2, i));
			}
		}

		private bool method_7()
		{
			int num = 0;
			while (true)
			{
				if (num < Char.myCharz().arrItemBag.Length)
				{
					if (Char.myCharz().arrItemBag[num] != null && Char.myCharz().arrItemBag[num].template.id == 194)
						break;
					num++;
					continue;
				}
				if (isUseCapsual)
				{
					for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
					{
						if (Char.myCharz().arrItemBag[i] != null && Char.myCharz().arrItemBag[i].template.id == 193)
						{
							Service.gI().useItem(0, 1, -1, 193);
							return true;
						}
					}
				}
				return false;
			}
			Service.gI().useItem(0, 1, -1, 194);
			return true;
		}

		public void method_8(object object_0)
		{
			isXMapRuning = true;
			if ((GameCanvas.panel.mapNames == null || GameCanvas.panel.mapNames.Length == 0) && method_7())
			{
				while (!GameCanvas.panel.isShow)
				{
					Thread.Sleep(100);
				}
			}
			if (dictionary_0.ContainsKey(84))
				dictionary_0.Remove(84);
			dictionary_0.Add(84, new List<GClass0>());
			dictionary_0[84].Add(new GClass0(24 + Char.myCharz().cgender, 10, 0));
			int mapID = TileMap.mapID;
			List<GClass0> value = new List<GClass0>(dictionary_0[mapID]);
			method_6();
			int[] array = method_9((int)object_0);
			if (array == null)
			{
				GameScr.info1.addInfo("Không thể tìm thấy đường đi", 0);
				dictionary_0[mapID] = value;
				isXMapRuning = false;
				return;
			}
			int num = 0;
			while (true)
			{
				if (num < array.Length - 1)
				{
					while (TileMap.mapID == array[num])
					{
						if (!Char.ischangingMap && !Controller.isStopReadMessage)
							method_5(array[num + 1]);
						Thread.Sleep(500);
					}
					if (TileMap.mapID != array[num + 1])
						break;
					num++;
					continue;
				}
				GameScr.info1.addInfo("XMap by Phucprotein", 0);
				dictionary_0[mapID] = value;
				isXMapRuning = false;
				return;
			}
			dictionary_0[mapID] = value;
			method_8(object_0);
		}

		private int[] method_9(int int_0)
		{
			return method_10(int_0, new int[1] { TileMap.mapID });
		}

		private int[] method_10(int int_0, int[] int_1)
		{
			List<int[]> list = new List<int[]>();
			List<int> list2 = new List<int>();
			list2.AddRange(int_1);
			foreach (GClass0 item in dictionary_0[int_1[int_1.Length - 1]])
			{
				if (int_0 != item.int_0)
				{
					if (!list2.Contains(item.int_0))
					{
						int[] array = method_10(int_0, new List<int>(list2) { item.int_0 }.ToArray());
						if (array != null)
							list.Add(array);
					}
					continue;
				}
				list2.Add(int_0);
				return list2.ToArray();
			}
			int num = 9999;
			int[] result = null;
			foreach (int[] item2 in list)
			{
				if (!smethod_1(item2) && (Char.myCharz().taskMaint.taskId > 30 || !smethod_2(item2)) && item2.Length < num)
				{
					num = item2.Length;
					result = item2;
				}
			}
			return result;
		}

		private static bool smethod_1(int[] int_0)
		{
			int num = 1;
			while (true)
			{
				if (num < int_0.Length - 1)
				{
					if (int_0[num] == 102 && int_0[num + 1] == 24 && (int_0[num - 1] == 27 || int_0[num - 1] == 28 || int_0[num - 1] == 29))
						break;
					num++;
					continue;
				}
				return false;
			}
			return true;
		}

		private static bool smethod_2(int[] int_0)
		{
			int num = 0;
			while (true)
			{
				if (num < int_0.Length)
				{
					if (int_0[num] >= 105 && int_0[num] <= 110)
						break;
					num++;
					continue;
				}
				return false;
			}
			return true;
		}

		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 2:
				if (thread_0 != null && thread_0.IsAlive)
				{
					GameScr.info1.addInfo("Có lỗi xảy ra, XMap trước đó chưa kết thúc", 0);
					break;
				}
				thread_0 = new Thread(method_8);
				thread_0.IsBackground = true;
				thread_0.Start(p);
				break;
			case 1:
			{
				int[] array = (int[])p;
				if (array.Length == 1)
					perform(2, array[0]);
				else
					method_4(array);
				break;
			}
			}
		}

		public static void RunToMap(int mapID)
		{
			gI().perform(2, mapID);
		}
	}
}
