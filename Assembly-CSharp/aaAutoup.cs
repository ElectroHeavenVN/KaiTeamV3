public class aaAutoup
{
	public static long TimePick;

	public static void UpSSXayda()
	{
		if (TileMap.mapID == 15)
		{
			GameScr.canAutoPlay = true;
			GameScr.isAutoPlay = true;
		}
		if (TileMap.mapID == 23)
		{
			Service.gI().openMenu(4);
			Service.gI().menu(4, 0, 0);
			if (mSystem.currentTimeMillis() - TimePick > 2000L)
			{
				GameScr.info1.addInfo("Đang chờ thu hoạch đậu.", 0);
				TimePick = mSystem.currentTimeMillis();
				Service.gI().pickItem(-1);
			}
		}
		if (GameScr.hpPotion > 1)
		{
			if (TileMap.mapID == 23)
				AAAMYs.ChangeMap(0);
			if (TileMap.mapID == 14)
			{
				GameScr.canAutoPlay = false;
				AAAMYs.ChangeMap(0);
			}
		}
		if (GameScr.hpPotion <= 1 && TileMap.mapID == 14)
			AAAMYs.ChangeMap(1);
		if (GameScr.hpPotion <= 0 && (Char.myCharz().cHP <= 20 || Char.myCharz().cMP < 5))
		{
			GameScr.canAutoPlay = false;
			GameScr.isAutoPlay = false;
			if (TileMap.mapID == 15)
				AAAMYs.ChangeMap(0);
		}
	}

	public static void UpSSTD()
	{
		if (TileMap.mapID == 21)
		{
			Service.gI().openMenu(4);
			Service.gI().menu(4, 0, 0);
			if (mSystem.currentTimeMillis() - TimePick > 2000L)
			{
				GameScr.info1.addInfo("Đang chờ thu hoạch đậu.", 0);
				TimePick = mSystem.currentTimeMillis();
				Service.gI().pickItem(-1);
			}
		}
		if (GameScr.hpPotion > 1)
		{
			if (TileMap.mapID == 21)
				AAAMYs.ChangeMap(0);
			if (TileMap.mapID == 0)
				AAAMYs.ChangeMap(0);
		}
		if (GameScr.hpPotion <= 1 && TileMap.mapID == 0)
			AAAMYs.ChangeMap(1);
		if (GameScr.hpPotion <= 0 && (Char.myCharz().cHP <= 40 || Char.myCharz().cMP < 5))
		{
			GameScr.canAutoPlay = false;
			GameScr.isAutoPlay = false;
			if (TileMap.mapID == 1)
				AAAMYs.ChangeMap(0);
		}
	}
}
