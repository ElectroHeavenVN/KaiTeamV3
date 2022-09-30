using System;

public class SoundMn
{
	public class MediaPlayer
	{
	}

	public class SoundPool
	{
	}

	public class AssetManager
	{
	}

	public static SoundMn gIz;

	public static bool isSound;

	public static int volume;

	private static int MAX_VOLUME;

	public static MediaPlayer[] music;

	public static SoundPool[] sound;

	public static int[] soundID;

	public static int AIR_SHIP;

	public static int RAIN;

	public static int TAITAONANGLUONG;

	public static int GET_ITEM;

	public static int MOVE;

	public static int LOW_PUNCH;

	public static int LOW_KICK;

	public static int FLY;

	public static int JUMP;

	public static int PANEL_OPEN;

	public static int BUTTON_CLOSE;

	public static int BUTTON_CLICK;

	public static int MEDIUM_PUNCH;

	public static int MEDIUM_KICK;

	public static int PANEL_CLICK;

	public static int EAT_PEAN;

	public static int OPEN_DIALOG;

	public static int NORMAL_KAME;

	public static int NAMEK_KAME;

	public static int XAYDA_KAME;

	public static int EXPLODE_1;

	public static int EXPLODE_2;

	public static int TRAIDAT_KAME;

	public static int HP_UP;

	public static int THAIDUONGHASAN;

	public static int HOISINH;

	public static int GONG;

	public static int KHICHAY;

	public static int BIG_EXPLODE;

	public static int NAMEK_LAZER;

	public static int NAMEK_CHARGE;

	public bool freePool;

	public int poolCount;

	public static int cout;

	public static void init(AssetManager ac)
	{
		Sound.setActivity(ac);
	}

	public static SoundMn gI()
	{
		if (gIz == null)
			gIz = new SoundMn();
		return gIz;
	}

	public void loadSound(int mapID)
	{
		Sound.init(new int[3] { AIR_SHIP, RAIN, TAITAONANGLUONG }, new int[28]
		{
			GET_ITEM, MOVE, LOW_PUNCH, LOW_KICK, FLY, JUMP, PANEL_OPEN, BUTTON_CLOSE, BUTTON_CLICK, MEDIUM_PUNCH,
			MEDIUM_KICK, PANEL_OPEN, EAT_PEAN, OPEN_DIALOG, NORMAL_KAME, NAMEK_KAME, XAYDA_KAME, EXPLODE_1, EXPLODE_2, TRAIDAT_KAME,
			HP_UP, THAIDUONGHASAN, HOISINH, GONG, KHICHAY, BIG_EXPLODE, NAMEK_LAZER, NAMEK_CHARGE
		});
	}

	public void getSoundOption()
	{
		if (GameCanvas.loginScr.isLogin2 && Char.myCharz().taskMaint != null && Char.myCharz().taskMaint.taskId >= 2)
		{
			Panel.strTool = new string[10]
			{
				"Menu hack-mod - 8SAO.CLUB",
				mResources.quayso,
				mResources.gameInfo,
				mResources.change_flag,
				mResources.change_zone,
				mResources.chat_world,
				mResources.account,
				mResources.option,
				mResources.change_account,
				mResources.REGISTOPROTECT
			};
			if (Char.myCharz().havePet)
				Panel.strTool = new string[11]
				{
					"Menu hack-mod - 8SAO.CLUB",
					mResources.quayso,
					mResources.gameInfo,
					mResources.pet,
					mResources.change_flag,
					mResources.change_zone,
					mResources.chat_world,
					mResources.account,
					mResources.option,
					mResources.change_account,
					mResources.REGISTOPROTECT
				};
		}
		else
		{
			Panel.strTool = new string[9]
			{
				"Menu hack-mod - 8SAO.CLUB",
				mResources.quayso,
				mResources.gameInfo,
				mResources.change_flag,
				mResources.change_zone,
				mResources.chat_world,
				mResources.account,
				mResources.option,
				mResources.change_account
			};
			if (Char.myCharz().havePet)
				Panel.strTool = new string[10]
				{
					"Menu hack-mod - 8SAO.CLUB",
					mResources.quayso,
					mResources.gameInfo,
					mResources.pet,
					mResources.change_flag,
					mResources.change_zone,
					mResources.chat_world,
					mResources.account,
					mResources.option,
					mResources.change_account
				};
		}
	}

	public void getStrOption()
	{
		if (Main.isPC)
			Panel.strCauhinh = new string[2]
			{
				(!GameCanvas.isPlaySound) ? mResources.turnOnSound : mResources.turnOffSound,
				(mGraphics.zoomLevel <= 1) ? mResources.x2Screen : mResources.x1Screen
			};
		else
			Panel.strCauhinh = new string[2]
			{
				(!GameCanvas.isPlaySound) ? mResources.turnOnSound : mResources.turnOffSound,
				(GameScr.isAnalog != 0) ? mResources.turnOffAnalog : mResources.turnOnAnalog
			};
	}

	public void HP_MPup()
	{
		Sound.playSound(HP_UP, 0.5f);
	}

	public void charPunch(bool isKick, float volumn)
	{
		if (!Char.myCharz().me)
			volume /= 2;
		if (volumn <= 0f)
			volumn = 0.01f;
		int num = Res.random(0, 3);
		if (isKick)
			Sound.playSound((num != 0) ? MEDIUM_KICK : LOW_KICK, 0.1f);
		else
			Sound.playSound((num != 0) ? MEDIUM_PUNCH : LOW_PUNCH, 0.1f);
		poolCount++;
	}

	public void thaiduonghasan()
	{
		Sound.playSound(THAIDUONGHASAN, 0.5f);
		poolCount++;
	}

	public void rain()
	{
		Sound.playMus(RAIN, 0.3f, true);
	}

	public void gongName()
	{
		Sound.playSound(NAMEK_CHARGE, 0.3f);
		poolCount++;
	}

	public void gong()
	{
		Sound.playSound(GONG, 0.2f);
		poolCount++;
	}

	public void getItem()
	{
		Sound.playSound(GET_ITEM, 0.3f);
		poolCount++;
	}

	public void soundToolOption()
	{
		GameCanvas.isPlaySound = !GameCanvas.isPlaySound;
		if (GameCanvas.isPlaySound)
		{
			Panel.strCauhinh[0] = mResources.turnOffSound;
			gI().loadSound(TileMap.mapID);
			Rms.saveRMSInt("isPlaySound", 1);
		}
		else
		{
			Panel.strCauhinh[0] = mResources.turnOnSound;
			gI().closeSound();
			Rms.saveRMSInt("isPlaySound", 0);
		}
	}

	public void update()
	{
	}

	public void closeSound()
	{
		Sound.stopAll = true;
		stopAll();
	}

	public void openSound()
	{
		if (Sound.music == null)
			loadSound(0);
		Sound.stopAll = false;
	}

	public void bigeExlode()
	{
		Sound.playSound(BIG_EXPLODE, 0.5f);
		poolCount++;
	}

	public void explode_1()
	{
		Sound.playSound(EXPLODE_1, 0.5f);
		poolCount++;
	}

	public void explode_2()
	{
		Sound.playSound(EXPLODE_1, 0.5f);
		poolCount++;
	}

	public void traidatKame()
	{
		Sound.playSound(TRAIDAT_KAME, 1f);
		poolCount++;
	}

	public void namekKame()
	{
		Sound.playSound(NAMEK_KAME, 0.3f);
		poolCount++;
	}

	public void nameLazer()
	{
		Sound.playSound(NAMEK_LAZER, 0.3f);
		poolCount++;
	}

	public void xaydaKame()
	{
		Sound.playSound(XAYDA_KAME, 0.3f);
		poolCount++;
	}

	public void mobKame(int type)
	{
		int id = XAYDA_KAME;
		if (type == 13)
			id = NORMAL_KAME;
		Sound.playSound(id, 0.1f);
		poolCount++;
	}

	public void charRun(float volumn)
	{
		if (!Char.myCharz().me)
		{
			volume /= 2;
			if (volumn <= 0f)
				volumn = 0.01f;
		}
		if (GameCanvas.gameTick % 8 == 0)
		{
			Sound.playSound(MOVE, volumn);
			poolCount++;
		}
	}

	public void monkeyRun(float volumn)
	{
		if (GameCanvas.gameTick % 8 == 0)
		{
			Sound.playSound(KHICHAY, 0.2f);
			poolCount++;
		}
	}

	public void charFall()
	{
		Sound.playSound(MOVE, 0.1f);
		poolCount++;
	}

	public void charJump()
	{
		Sound.playSound(MOVE, 0.2f);
		poolCount++;
	}

	public void panelOpen()
	{
		Sound.playSound(PANEL_OPEN, 0.5f);
		poolCount++;
	}

	public void buttonClose()
	{
		Sound.playSound(BUTTON_CLOSE, 0.5f);
		poolCount++;
	}

	public void buttonClick()
	{
		Sound.playSound(BUTTON_CLICK, 0.5f);
		poolCount++;
	}

	public void stopMove()
	{
	}

	public void charFly()
	{
		Sound.playSound(FLY, 0.2f);
		poolCount++;
	}

	public void stopFly()
	{
	}

	public void openMenu()
	{
		Sound.playSound(BUTTON_CLOSE, 0.5f);
		poolCount++;
	}

	public void panelClick()
	{
		Sound.playSound(PANEL_CLICK, 0.5f);
		poolCount++;
	}

	public void eatPeans()
	{
		Sound.playSound(EAT_PEAN, 0.5f);
		poolCount++;
	}

	public void openDialog()
	{
		Sound.playSound(OPEN_DIALOG, 0.5f);
	}

	public void hoisinh()
	{
		Sound.playSound(HOISINH, 0.5f);
		poolCount++;
	}

	public void taitao()
	{
		Sound.playMus(TAITAONANGLUONG, 0.5f, true);
	}

	public void taitaoPause()
	{
	}

	public bool isPlayRain()
	{
		try
		{
			return Sound.isPlayingSound();
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool isPlayAirShip()
	{
		return false;
	}

	public void airShip()
	{
		cout++;
		if (cout % 2 == 0)
			Sound.playMus(AIR_SHIP, 0.3f, false);
	}

	public void pauseAirShip()
	{
	}

	public void resumeAirShip()
	{
	}

	public void stopAll()
	{
		Sound.stopAllz();
	}

	public void backToRegister()
	{
		Session_ME.gI().close();
		GameCanvas.panel.hide();
		GameCanvas.loginScr.actRegister();
		GameCanvas.loginScr.switchToMe();
	}

	static SoundMn()
	{
		isSound = true;
		MAX_VOLUME = 10;
		RAIN = 1;
		TAITAONANGLUONG = 2;
		MOVE = 1;
		LOW_PUNCH = 2;
		LOW_KICK = 3;
		FLY = 4;
		JUMP = 5;
		PANEL_OPEN = 6;
		BUTTON_CLOSE = 7;
		BUTTON_CLICK = 8;
		MEDIUM_PUNCH = 9;
		MEDIUM_KICK = 10;
		PANEL_CLICK = 11;
		EAT_PEAN = 12;
		OPEN_DIALOG = 13;
		NORMAL_KAME = 14;
		NAMEK_KAME = 15;
		XAYDA_KAME = 16;
		EXPLODE_1 = 17;
		EXPLODE_2 = 18;
		TRAIDAT_KAME = 19;
		HP_UP = 20;
		THAIDUONGHASAN = 21;
		HOISINH = 22;
		GONG = 23;
		KHICHAY = 24;
		BIG_EXPLODE = 25;
		NAMEK_LAZER = 26;
		NAMEK_CHARGE = 27;
		cout = 1;
	}
}
