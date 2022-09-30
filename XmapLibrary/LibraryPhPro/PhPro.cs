using System;

namespace LibraryPhPro
{
	public class PhPro
	{
		public static bool Chat(string text)
		{
			try
			{
				if (text.Equals("xm"))
					XMap.gI().method_3();
				else
				{
					if (!text.Equals("csxm"))
						return false;
					GameScr.info1.addInfo("Sự dụng capsual khi XMap: " + ((XMap.isUseCapsual = !XMap.isUseCapsual) ? "Bật" : "Tắt"), 0);
				}
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		public static void smethod_0(mGraphics mGraphics_0)
		{
			mFont.tahoma_7_yellow.drawString(mGraphics_0, "mapID: " + TileMap.mapID, 30, 100, 0);
			if (Char.myCharz().npcFocus != null)
				mFont.tahoma_7_yellow.drawString(mGraphics_0, "npcId: " + Char.myCharz().npcFocus.template.npcTemplateId, 30, 110, 0);
		}
	}
}
