public class AAAeee
{
	public static int[] posLeft;

	public static int[] posMid;

	public static int[] posRight;

	public static int toado;

	public static void ResetPos()
	{
		posLeft = new int[2];
		posMid = new int[2];
		posRight = new int[2];
	}

	public static void TeleportTo(int x, int y)
	{
		Char.myCharz().statusMe = -1;
		if (GameScr.canAutoPlay)
		{
			if (x < 15)
				Char.myCharz().cx = 15;
			else if (x > TileMap.pxw - 15)
			{
				Char.myCharz().cx = TileMap.pxw - 15;
			}
			else
			{
				Char.myCharz().cx = x;
			}
			Char.myCharz().cy = y;
			Service.gI().charMove();
			return;
		}
		Char.myCharz().cdir = ((Char.myCharz().cx - x < 0) ? 1 : (-1));
		if (Math.abs(Char.myCharz().cx - x) > toado)
		{
			TeleportTo(Char.myCharz().cx + toado * Char.myCharz().cdir, GetYGround(Char.myCharz().cx + toado * Char.myCharz().cdir) - 24);
			TeleportTo(x, y);
			Service.gI().charMove();
		}
		if (Math.abs(Char.myCharz().cx - x) <= toado)
		{
			if (x < 15)
				Char.myCharz().cx = 15;
			else if (x > TileMap.pxw - 15)
			{
				Char.myCharz().cx = TileMap.pxw - 15;
			}
			else
			{
				Char.myCharz().cx = x;
			}
			Char.myCharz().cy = y;
			Service.gI().charMove();
		}
	}

	public static int GetYGround(int x)
	{
		int num = 50;
		int num2 = 0;
		while (num2 < 30)
		{
			num2++;
			num += 24;
			if (TileMap.tileTypeAt(x, num, 2))
			{
				if (num % 24 != 0)
					num -= num % 24;
				break;
			}
		}
		return num;
	}

	public static void LoadMap(int index)
	{
		switch (index)
		{
		case 0:
			if (posLeft[0] != 0 && posLeft[1] != 0)
				TeleportTo(posLeft[0], posLeft[1]);
			else
				TeleportTo(60, GetYGround(60));
			break;
		case 1:
			if (posRight[0] != 0 && posRight[1] != 0)
				TeleportTo(posRight[0], posRight[1]);
			else
				TeleportTo(TileMap.pxw - 60, GetYGround(TileMap.pxw - 60));
			break;
		case 2:
			if (posMid[0] != 0 && posMid[1] != 0)
				TeleportTo(posMid[0], posMid[1]);
			else
				TeleportTo(TileMap.pxw / 2, GetYGround(TileMap.pxw / 2));
			break;
		}
		Service.gI().requestChangeMap();
	}

	public static void LoadWaypointPos()
	{
		ResetPos();
		int num = TileMap.vGo.size();
		if (num != 2)
		{
			for (int i = 0; i < num; i++)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
				if (waypoint.maxX < 60)
				{
					posLeft[0] = waypoint.minX + 15;
					posLeft[1] = waypoint.maxY;
				}
				else if (waypoint.maxX > TileMap.pxw - 60)
				{
					posRight[0] = waypoint.maxX - 15;
					posRight[1] = waypoint.maxY;
				}
				else
				{
					posMid[0] = waypoint.minX + 15;
					posMid[1] = waypoint.maxY;
				}
			}
			return;
		}
		Waypoint waypoint2 = (Waypoint)TileMap.vGo.elementAt(0);
		Waypoint waypoint3 = (Waypoint)TileMap.vGo.elementAt(1);
		if ((waypoint2.maxX < 60 && waypoint3.maxX < 60) || (waypoint2.minX > TileMap.pxw - 60 && waypoint3.minX > TileMap.pxw - 60))
		{
			posLeft[0] = waypoint2.minX + 15;
			posLeft[1] = waypoint2.maxY;
			posRight[0] = waypoint3.maxX - 15;
			posRight[1] = waypoint3.maxY;
		}
		else if (waypoint2.maxX < waypoint3.maxX)
		{
			posLeft[0] = waypoint2.minX + 15;
			posLeft[1] = waypoint2.maxY;
			posRight[0] = waypoint3.maxX - 15;
			posRight[1] = waypoint3.maxY;
		}
		else
		{
			posLeft[0] = waypoint3.minX + 15;
			posLeft[1] = waypoint3.maxY;
			posRight[0] = waypoint2.maxX - 15;
			posRight[1] = waypoint2.maxY;
		}
	}

	static AAAeee()
	{
		toado = 150;
	}

	public static void unused_method_0(int int_0)
	{
		switch (int_0)
		{
		case 0:
			if (posLeft[0] != 0 && posLeft[1] != 0)
				AAAMYs.TeleportTo(posLeft[0], posLeft[1]);
			else
				AAAMYs.TeleportTo(60, GetYGround(60));
			break;
		case 1:
			if (posRight[0] != 0 && posRight[1] != 0)
				AAAMYs.TeleportTo(posRight[0], posRight[1]);
			else
				AAAMYs.TeleportTo(TileMap.pxw - 60, GetYGround(TileMap.pxw - 60));
			break;
		case 2:
			if (posMid[0] != 0 && posMid[1] != 0)
				AAAMYs.TeleportTo(posMid[0], posMid[1]);
			else
				AAAMYs.TeleportTo(TileMap.pxw / 2, GetYGround(TileMap.pxw / 2));
			Service.gI().requestChangeMap();
			break;
		}
		Service.gI().getMapOffline();
	}
}
