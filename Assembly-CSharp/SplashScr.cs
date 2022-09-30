public class SplashScr : mScreen
{
	public static int splashScrStat;

	private bool isCheckConnect;

	private bool isSwitchToLogin;

	public static int nData = -1;

	public static int maxData = -1;

	public static SplashScr instance;

	public static Image imgLogo;

	public SplashScr()
	{
		instance = this;
	}

	public static void loadSplashScr()
	{
		splashScrStat = 0;
	}

	public override void update()
	{
		if (splashScrStat == 30 && !isCheckConnect)
		{
			isCheckConnect = true;
			if (Rms.loadRMSInt("isPlaySound") != -1)
				GameCanvas.isPlaySound = Rms.loadRMSInt("isPlaySound") == 1;
			if (GameCanvas.isPlaySound)
				SoundMn.gI().loadSound(TileMap.mapID);
			SoundMn.gI().getStrOption();
			ServerListScreen.loadIP();
		}
		splashScrStat++;
		ServerListScreen.updateDeleteData();
	}

	public static void loadIP()
	{
		if (Rms.loadRMSInt("svselect") == -1)
		{
			int num = 0;
			if (mResources.language > 0)
			{
				for (int i = 0; i < mResources.language; i++)
				{
					num += ServerListScreen.lengthServer[i];
				}
			}
			if (ServerListScreen.serverPriority == -1)
				ServerListScreen.ipSelect = AAAMYs.Server;
			else
				ServerListScreen.ipSelect = AAAMYs.Server;
			Rms.saveRMSInt("svselect", AAAMYs.Server);
			GameMidlet.IP = ServerListScreen.address[AAAMYs.Server];
			GameMidlet.PORT = ServerListScreen.port[AAAMYs.Server];
			mResources.loadLanguague(ServerListScreen.language[AAAMYs.Server]);
			LoginScr.serverName = ServerListScreen.nameServer[AAAMYs.Server];
			GameCanvas.connect();
			return;
		}
		ServerListScreen.ipSelect = AAAMYs.Server;
		if (ServerListScreen.ipSelect > ServerListScreen.serverPriority)
		{
			ServerListScreen.ipSelect = AAAMYs.Server;
			Rms.saveRMSInt("svselect", AAAMYs.Server);
		}
		string text = Rms.loadRMSString("acc");
		string text2 = Rms.loadRMSString("userAo" + AAAMYs.Server);
		if ((text == null || text.Equals(string.Empty)) && (text2 == null || text2.Equals(string.Empty)))
		{
			int num2 = 0;
			if (mResources.language > 0)
			{
				for (int j = 0; j < mResources.language; j++)
				{
					num2 += ServerListScreen.lengthServer[j];
				}
			}
			if (ServerListScreen.serverPriority == -1)
				ServerListScreen.ipSelect = AAAMYs.Server;
			else
				ServerListScreen.ipSelect = AAAMYs.Server;
		}
		GameMidlet.IP = ServerListScreen.address[AAAMYs.Server];
		GameMidlet.PORT = ServerListScreen.port[AAAMYs.Server];
		mResources.loadLanguague(ServerListScreen.language[AAAMYs.Server]);
		LoginScr.serverName = ServerListScreen.nameServer[AAAMYs.Server];
		GameCanvas.connect();
	}

	public override void paint(mGraphics g)
	{
		if (imgLogo != null && splashScrStat < 30)
		{
			g.setColor(16777215);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			g.drawImage(imgLogo, GameCanvas.w / 2, GameCanvas.h / 2, 3);
		}
		if (nData != -1)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			g.drawImage(LoginScr.imgTitle, GameCanvas.w / 2, GameCanvas.h / 2 - 24, StaticObj.BOTTOM_HCENTER);
			GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.h / 2 + 24, g);
			mFont.tahoma_7b_white.drawString(g, mResources.downloading_data + nData * 100 / maxData + "%", GameCanvas.w / 2, GameCanvas.h / 2, 2);
		}
		else if (splashScrStat >= 30)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			GameCanvas.paintShukiren(GameCanvas.hw, GameCanvas.hh, g);
			if (ServerListScreen.cmdDeleteRMS != null)
				mFont.tahoma_7_white.drawString(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
		}
	}

	public static void loadImg()
	{
		imgLogo = GameCanvas.loadImage("/gamelogo.png");
	}
}
