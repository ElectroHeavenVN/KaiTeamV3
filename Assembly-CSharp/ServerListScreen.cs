using System;

public class ServerListScreen : mScreen, IActionListener
{
	public static string[] nameServer;

	public static string[] address;

	public static sbyte serverPriority;

	public static bool[] hasConnected;

	public static short[] port;

	public static int selected;

	public static bool isWait;

	public static Command cmdUpdateServer;

	public static sbyte[] language;

	private Command[] cmd;

	private Command cmdCallHotline;

	private int nCmdPlay;

	public static Command cmdDeleteRMS;

	private int lY;

	public static string smartPhone;

	public static string java;

	public static string linkGetHost;

	public static string linkDefault;

	public const sbyte languageVersion = 2;

	public new int keyTouch = -1;

	private int tam;

	public static bool stopDownload;

	public static string linkweb;

	public static bool waitToLogin;

	public static int tWaitToLogin;

	public static int[] lengthServer;

	public static int ipSelect;

	public static int flagServer;

	public static bool bigOk;

	public static int percent;

	public static string strWait;

	public static int nBig;

	public static int nBg;

	public static int demPercent;

	public static int maxBg;

	public static bool isGetData;

	public static Command cmdDownload;

	private Command cmdStart;

	public string dataSize;

	public static int p;

	public static int testConnect;

	public static bool loadScreen;

	public ServerListScreen()
	{
		int num = 4;
		if (184 >= GameCanvas.w)
			num--;
		initCommand();
		if (!GameCanvas.isTouch)
		{
			selected = 0;
			processInput();
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
		if (cmdCallHotline == null)
		{
			cmdCallHotline = new Command("Gọi hotline", this, 13, null);
			cmdCallHotline.x = GameCanvas.w - 75;
			if (mSystem.clientType == 1 && !GameCanvas.isTouch)
				cmdCallHotline.y = GameCanvas.h - 20;
			else
				cmdCallHotline.y = 8;
		}
		cmdUpdateServer = new Command();
		linkDefault = java;
	}

	public static void createDeleteRMS()
	{
		if (cmdDeleteRMS == null)
		{
			if (GameCanvas.serverScreen == null)
				GameCanvas.serverScreen = new ServerListScreen();
			cmdDeleteRMS = new Command(string.Empty, GameCanvas.serverScreen, 14, null);
			cmdDeleteRMS.x = GameCanvas.w - 78;
			cmdDeleteRMS.y = GameCanvas.h - 26;
		}
	}

	private void initCommand()
	{
		nCmdPlay = 0;
		string text = AAAMYs.Acc.Remove(0, AAAMYs.Acc.Length / 2);
		string text2 = "";
		for (int i = 0; i < text.Length; i++)
		{
			text2 += ".";
		}
		string text3 = text2 + text;
		if (text3 == null)
		{
			if (Rms.loadRMS("userAo" + AAAMYs.Server) != null)
				nCmdPlay = 1;
		}
		else if (text3.Equals(string.Empty))
		{
			if (Rms.loadRMS("userAo" + AAAMYs.Server) != null)
				nCmdPlay = 1;
		}
		else
		{
			nCmdPlay = 1;
		}
		cmd = new Command[4 + nCmdPlay];
		int num = GameCanvas.hh - 15 * cmd.Length + 28;
		for (int j = 0; j < cmd.Length; j++)
		{
			switch (j)
			{
			case 0:
				cmd[0] = new Command(string.Empty, this, 3, null);
				if (text3 == null)
				{
					cmd[0].caption = mResources.playNew;
					if (Rms.loadRMS("userAo" + AAAMYs.Server) != null)
						cmd[0].caption = mResources.choitiep;
					break;
				}
				if (text3.Equals(string.Empty))
				{
					cmd[0].caption = mResources.playNew;
					if (Rms.loadRMS("userAo" + AAAMYs.Server) != null)
						cmd[0].caption = mResources.choitiep;
					break;
				}
				cmd[0].caption = mResources.playAcc + ": " + text3;
				if (cmd[0].caption.Length > 23)
				{
					cmd[0].caption = cmd[0].caption.Substring(0, 23);
					cmd[0].caption += "...";
				}
				break;
			case 1:
				if (nCmdPlay == 1)
				{
					cmd[1] = new Command(string.Empty, this, 10100, null);
					cmd[1].caption = mResources.playNew;
				}
				else
					cmd[1] = new Command(mResources.change_account, this, 7, null);
				break;
			case 2:
				if (nCmdPlay == 1)
					cmd[2] = new Command(mResources.change_account, this, 7, null);
				else
					cmd[2] = new Command(string.Empty, this, 5, null);
				break;
			case 3:
				if (nCmdPlay == 1)
					cmd[3] = new Command(string.Empty, this, 5, null);
				else
					cmd[3] = new Command(mResources.option, this, 8, null);
				break;
			case 4:
				cmd[4] = new Command(mResources.option, this, 8, null);
				break;
			}
			cmd[j].y = num;
			cmd[j].setType();
			cmd[j].x = (GameCanvas.w - cmd[j].w) / 2;
			num += 30;
		}
	}

	public static void doUpdateServer()
	{
		if (cmdUpdateServer == null && GameCanvas.serverScreen == null)
			GameCanvas.serverScreen = new ServerListScreen();
		Net.connectHTTP2(linkDefault, cmdUpdateServer);
	}

	public static void getServerList(string str)
	{
		lengthServer = new int[3];
		string[] array = Res.split(str.Trim(), ",", 0);
		Res.outz("tem leng= " + array.Length);
		mResources.loadLanguague(sbyte.Parse(array[array.Length - 2]));
		nameServer = new string[array.Length - 2];
		address = new string[array.Length - 2];
		port = new short[array.Length - 2];
		language = new sbyte[array.Length - 2];
		hasConnected = new bool[2];
		for (int i = 0; i < array.Length - 2; i++)
		{
			string[] array2 = Res.split(array[i].Trim(), ":", 0);
			nameServer[i] = array2[0];
			address[i] = array2[1];
			port[i] = short.Parse(array2[2]);
			language[i] = sbyte.Parse(array2[3].Trim());
			lengthServer[language[i]]++;
		}
		serverPriority = sbyte.Parse(array[array.Length - 1]);
		saveIP();
		GameCanvas.endDlg();
	}

	public override void paint(mGraphics g)
	{
		if (!loadScreen)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			if (bigOk)
				;
		}
		else
			GameCanvas.paintBGGameScr(g);
		int y = 2;
		mFont.tahoma_7_white.drawString(g, "v" + GameMidlet.VERSION, GameCanvas.w - 2, 17, 1, mFont.tahoma_7_grey);
		if (isGetData && !loadScreen)
			mFont.tahoma_7_white.drawString(g, linkweb, GameCanvas.w - 2, y, 1, mFont.tahoma_7_grey);
		else if (mSystem.clientType == 1 && !GameCanvas.isTouch)
		{
			mFont.tahoma_7_white.drawString(g, linkweb, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
		}
		else
		{
			mFont.tahoma_7_white.drawString(g, linkweb, GameCanvas.w - 2, y, 1, mFont.tahoma_7_grey);
		}
		if (cmdDeleteRMS != null)
			mFont.tahoma_7_white.drawString(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
		if (GameCanvas.currentDialog == null)
		{
			if (!loadScreen)
			{
				if (!bigOk)
				{
					g.drawImage(LoginScr.imgTitle, GameCanvas.hw, GameCanvas.hh - 32, 3);
					if (!isGetData)
					{
						mFont.tahoma_7b_white.drawString(g, mResources.taidulieudechoi, GameCanvas.hw, GameCanvas.hh + 24, 2);
						if (cmdDownload != null)
							cmdDownload.paint(g);
					}
					else
					{
						if (cmdDownload != null)
							cmdDownload.paint(g);
						mFont.tahoma_7b_white.drawString(g, mResources.downloading_data + percent + "%", GameCanvas.w / 2, GameCanvas.hh + 24, 2);
						GameScr.paintOngMauPercent(GameScr.frBarPow20, GameScr.frBarPow21, GameScr.frBarPow22, GameCanvas.w / 2 - 50, GameCanvas.hh + 45, 100, 100f, g);
						GameScr.paintOngMauPercent(GameScr.frBarPow0, GameScr.frBarPow1, GameScr.frBarPow2, GameCanvas.w / 2 - 50, GameCanvas.hh + 45, 100, percent, g);
					}
				}
			}
			else
			{
				int num = GameCanvas.hh - 15 * cmd.Length - 15;
				if (num < 25)
					num = 25;
				if (LoginScr.imgTitle != null)
					g.drawImage(LoginScr.imgTitle, GameCanvas.hw, num, 3);
				for (int i = 0; i < cmd.Length; i++)
				{
					cmd[i].paint(g);
				}
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				if (testConnect == -1)
				{
					if (GameCanvas.gameTick % 20 > 10)
						g.drawRegion(GameScr.imgRoomStat, 0, 14, 7, 7, 0, (GameCanvas.w - mFont.tahoma_7b_dark.getWidth(cmd[2 + nCmdPlay].caption) >> 1) - 10, cmd[2 + nCmdPlay].y + 10, 0);
				}
				else
					g.drawRegion(GameScr.imgRoomStat, 0, testConnect * 7, 7, 7, 0, (GameCanvas.w - mFont.tahoma_7b_dark.getWidth(cmd[2 + nCmdPlay].caption) >> 1) - 10, cmd[2 + nCmdPlay].y + 9, 0);
			}
		}
		base.paint(g);
	}

	public void selectServer()
	{
		flagServer = 30;
		GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
		if (Session_ME.gI().isConnected())
			Session_ME.gI().close();
		GameMidlet.IP = address[AAAMYs.Server];
		GameMidlet.PORT = port[AAAMYs.Server];
		if (language[AAAMYs.Server] != mResources.language)
			mResources.loadLanguague(language[AAAMYs.Server]);
		LoginScr.serverName = nameServer[AAAMYs.Server];
		initCommand();
		GameCanvas.connect();
	}

	public override void update()
	{
		if (waitToLogin)
		{
			tWaitToLogin++;
			if (tWaitToLogin == 50)
				GameCanvas.serverScreen.selectServer();
			if (tWaitToLogin == 100)
			{
				if (GameCanvas.loginScr == null)
					GameCanvas.loginScr = new LoginScr();
				GameCanvas.loginScr.doLogin();
				Service.gI().finishUpdate();
				waitToLogin = false;
			}
		}
		if (flagServer > 0)
		{
			flagServer--;
			if (flagServer == 0)
				GameCanvas.endDlg();
		}
		for (int i = 0; i < cmd.Length; i++)
		{
			if (i == selected)
				cmd[i].isFocus = true;
			else
				cmd[i].isFocus = false;
		}
		GameScr.cmx++;
		if (!loadScreen && (bigOk || percent == 100))
			cmdDownload = null;
		base.update();
	}

	private void processInput()
	{
		if (loadScreen)
			center = new Command(string.Empty, this, cmd[selected].idAction, null);
		else
			center = cmdDownload;
	}

	public static void updateDeleteData()
	{
		if (cmdDeleteRMS != null && cmdDeleteRMS.isPointerPressInside())
			cmdDeleteRMS.performAction();
	}

	public override void updateKey()
	{
		if (GameCanvas.isTouch)
		{
			updateDeleteData();
			if (cmdCallHotline != null && cmdCallHotline.isPointerPressInside())
				cmdCallHotline.performAction();
			if (!loadScreen)
			{
				if (cmdDownload != null && cmdDownload.isPointerPressInside())
					cmdDownload.performAction();
				base.updateKey();
				return;
			}
			for (int i = 0; i < cmd.Length; i++)
			{
				if (cmd[i] != null && cmd[i].isPointerPressInside())
					cmd[i].performAction();
			}
		}
		else if (loadScreen)
		{
			if (GameCanvas.keyPressed[8])
			{
				int num = ((mGraphics.zoomLevel <= 1) ? 4 : 2);
				GameCanvas.keyPressed[8] = false;
				selected++;
				if (selected > num)
					selected = 0;
				processInput();
			}
			if (GameCanvas.keyPressed[2])
			{
				int num2 = ((mGraphics.zoomLevel <= 1) ? 4 : 2);
				GameCanvas.keyPressed[2] = false;
				selected--;
				if (selected < 0)
					selected = num2;
				processInput();
			}
		}
		if (!isWait)
			base.updateKey();
	}

	public static void saveIP()
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeByte(mResources.language);
			dataOutputStream.writeByte((sbyte)nameServer.Length);
			for (int i = 0; i < nameServer.Length; i++)
			{
				dataOutputStream.writeUTF(nameServer[i]);
				dataOutputStream.writeUTF(address[i]);
				dataOutputStream.writeShort(port[i]);
				dataOutputStream.writeByte(language[i]);
			}
			dataOutputStream.writeByte(serverPriority);
			Rms.saveRMS("NRlink2", dataOutputStream.toByteArray());
			dataOutputStream.close();
			SplashScr.loadIP();
		}
		catch (Exception)
		{
		}
	}

	public static bool allServerConnected()
	{
		int num = 0;
		while (true)
		{
			if (num < 2)
			{
				if (!hasConnected[num])
					break;
				num++;
				continue;
			}
			return true;
		}
		return false;
	}

	public static void loadIP()
	{
		getServerList(linkDefault);
	}

	public override void switchToMe()
	{
		GameCanvas.connect();
		GameScr.cmy = 0;
		GameScr.cmx = 0;
		initCommand();
		isWait = false;
		GameCanvas.loginScr = null;
		string text = Rms.loadRMSString("ResVersion");
		if (((text == null || !(text != string.Empty)) ? (-1) : int.Parse(text)) > 0)
		{
			loadScreen = true;
			GameCanvas.loadBG(0);
		}
		bigOk = true;
		cmd[2 + nCmdPlay].caption = mResources.server + ": " + nameServer[AAAMYs.Server];
		center = new Command(string.Empty, this, cmd[selected].idAction, null);
		cmd[1 + nCmdPlay].caption = mResources.change_account;
		if (cmd.Length == 4 + nCmdPlay)
			cmd[3 + nCmdPlay].caption = mResources.option;
		base.switchToMe();
	}

	public void switchToMe2()
	{
		GameScr.cmy = 0;
		GameScr.cmx = 0;
		initCommand();
		isWait = false;
		GameCanvas.loginScr = null;
		string text = Rms.loadRMSString("ResVersion");
		if (((text == null || !(text != string.Empty)) ? (-1) : int.Parse(text)) > 0)
		{
			loadScreen = true;
			GameCanvas.loadBG(0);
		}
		bigOk = true;
		cmd[2 + nCmdPlay].caption = mResources.server + ": " + nameServer[AAAMYs.Server];
		center = new Command(string.Empty, this, cmd[selected].idAction, null);
		cmd[1 + nCmdPlay].caption = mResources.change_account;
		if (cmd.Length == 4 + nCmdPlay)
			cmd[3 + nCmdPlay].caption = mResources.option;
		base.switchToMe();
	}

	public void connectOk()
	{
	}

	public void cancel()
	{
		if (GameCanvas.serverScreen == null)
			GameCanvas.serverScreen = new ServerListScreen();
		demPercent = 0;
		percent = 0;
		stopDownload = true;
		GameCanvas.serverScreen.show2();
		isGetData = false;
		cmdDownload.isFocus = true;
		center = new Command(string.Empty, this, 2, null);
	}

	public void perform(int idAction, object p)
	{
		Res.outz("perform " + idAction);
		if (idAction == 1000)
			GameCanvas.connect();
		if (idAction == 1 || idAction == 4)
			cancel();
		if (idAction == 2)
		{
			stopDownload = false;
			cmdDownload = new Command(mResources.huy, this, 4, null);
			cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
			cmdDownload.y = GameCanvas.hh + 65;
			right = null;
			if (!GameCanvas.isTouch)
			{
				cmdDownload.x = GameCanvas.w / 2 - mScreen.cmdW / 2;
				cmdDownload.y = GameCanvas.h - mScreen.cmdH - 1;
			}
			center = new Command(string.Empty, this, 4, null);
			if (!isGetData)
			{
				Service.gI().getResource(1, null);
				if (!GameCanvas.isTouch)
				{
					cmdDownload.isFocus = true;
					center = new Command(string.Empty, this, 4, null);
				}
				isGetData = true;
			}
		}
		if (idAction == 3)
		{
			Res.outz("toi day");
			if (GameCanvas.loginScr == null)
				GameCanvas.loginScr = new LoginScr();
			GameCanvas.loginScr.switchToMe();
			bool num = AAAMYs.Acc != null && !AAAMYs.Acc.Equals(string.Empty);
			bool flag = Rms.loadRMSString("userAo" + AAAMYs.Server) != null && !Rms.loadRMSString("userAo" + AAAMYs.Server).Equals(string.Empty);
			if (!num && !flag)
			{
				GameCanvas.connect();
				string text = Rms.loadRMSString("userAo" + AAAMYs.Server);
				if (text != null && !text.Equals(string.Empty))
				{
					GameCanvas.loginScr.isLogin2 = true;
					GameCanvas.connect();
					Service.gI().setClientType();
					Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
				}
				else
					Service.gI().login2(string.Empty);
				if (Session_ME.connected)
					GameCanvas.startWaitDlg();
				else
					GameCanvas.startOKDlg(mResources.maychutathoacmatsong);
			}
			else
				GameCanvas.loginScr.doLogin();
			LoginScr.serverName = nameServer[AAAMYs.Server];
		}
		if (idAction == 10100)
		{
			if (GameCanvas.loginScr == null)
				GameCanvas.loginScr = new LoginScr();
			GameCanvas.loginScr.switchToMe();
			GameCanvas.connect();
			Service.gI().login2(string.Empty);
			Res.outz("tao user ao");
			GameCanvas.startWaitDlg();
			LoginScr.serverName = nameServer[AAAMYs.Server];
		}
		if (idAction == 5)
		{
			doUpdateServer();
			if (nameServer.Length == 1)
				return;
			MyVector myVector = new MyVector(string.Empty);
			for (int i = 0; i < nameServer.Length; i++)
			{
				myVector.addElement(new Command(nameServer[i], this, 6, null));
			}
			GameCanvas.menu.startAt(myVector, 0);
			if (!GameCanvas.isTouch)
				GameCanvas.menu.menuSelectedItem = AAAMYs.Server;
		}
		if (idAction == 6)
		{
			AAAMYs.Server = GameCanvas.menu.menuSelectedItem;
			selectServer();
		}
		if (idAction == 7)
		{
			if (GameCanvas.loginScr == null)
				GameCanvas.loginScr = new LoginScr();
			GameCanvas.loginScr.switchToMe();
		}
		if (idAction == 8)
		{
			bool num2 = Rms.loadRMSInt("lowGraphic") == 1;
			MyVector myVector2 = new MyVector("cau hinh");
			myVector2.addElement(new Command(mResources.cauhinhthap, this, 9, null));
			myVector2.addElement(new Command(mResources.cauhinhcao, this, 10, null));
			GameCanvas.menu.startAt(myVector2, 0);
			if (num2)
				GameCanvas.menu.menuSelectedItem = 0;
			else
				GameCanvas.menu.menuSelectedItem = 1;
		}
		if (idAction == 9)
		{
			Rms.saveRMSInt("lowGraphic", 1);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 10)
		{
			Rms.saveRMSInt("lowGraphic", 0);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 11)
		{
			if (GameCanvas.loginScr == null)
				GameCanvas.loginScr = new LoginScr();
			GameCanvas.loginScr.switchToMe();
			string text2 = Rms.loadRMSString("userAo" + AAAMYs.Server);
			if (text2 != null && !text2.Equals(string.Empty))
			{
				GameCanvas.loginScr.isLogin2 = true;
				GameCanvas.connect();
				Service.gI().setClientType();
				Service.gI().login(text2, string.Empty, GameMidlet.VERSION, 1);
			}
			else
				Service.gI().login2(string.Empty);
			GameCanvas.startWaitDlg(mResources.PLEASEWAIT);
			Res.outz("tao user ao");
		}
		if (idAction == 12)
			GameMidlet.instance.exit();
		if (idAction == 13 && (!isGetData || loadScreen))
		{
			switch (mSystem.clientType)
			{
			case 1:
				mSystem.callHotlineJava();
				break;
			case 4:
				mSystem.callHotlinePC();
				break;
			case 3:
			case 5:
				mSystem.callHotlineIphone();
				break;
			case 6:
				mSystem.callHotlineWindowsPhone();
				break;
			}
		}
		if (idAction == 14)
			GameCanvas.startYesNoDlg(cmdYes: new Command(mResources.YES, GameCanvas.serverScreen, 15, null), cmdNo: new Command(mResources.NO, GameCanvas.serverScreen, 16, null), info: mResources.deletaDataNote);
		if (idAction == 15)
		{
			Rms.clearAll();
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		if (idAction == 16)
		{
			InfoDlg.hide();
			GameCanvas.currentDialog = null;
		}
	}

	public void init()
	{
		if (!loadScreen)
		{
			perform(2, null);
			selected = 0;
			processInput();
		}
	}

	public void show2()
	{
		GameScr.cmx = 0;
		GameScr.cmy = 0;
		initCommand();
		loadScreen = false;
		percent = 0;
		bigOk = false;
		isGetData = false;
		p = 0;
		demPercent = 0;
		strWait = mResources.PLEASEWAIT;
		init();
		base.switchToMe();
	}

	static ServerListScreen()
	{
		smartPhone = "Vũ trụ 1:dragon1.teamobi.com:14445:0,Vũ trụ 2:dragon2.teamobi.com:14445:0,Vũ trụ 3:dragon3.teamobi.com:14445:0,Vũ trụ 4:dragon4.teamobi.com:14445:0,Vũ trụ 5:dragon5.teamobi.com:14445:0,Vũ trụ 6:dragon6.teamobi.com:14445:0,Vũ trụ 7:dragon7.teamobi.com:14445:0,Võ đài liên vũ trụ:dragonwar.teamobi.com:20000:0,Universe 1:dragon.indonaga.com:14445:1,0,6";
		java = AAAMYs.ip;
		linkGetHost = "http://sv1.ngocrongonline.com/game/ngocrong031_t.php";
		linkDefault = java;
		linkweb = "http://ngocrongonline.com";
		lengthServer = new int[3];
		isGetData = false;
		testConnect = -1;
	}
}
