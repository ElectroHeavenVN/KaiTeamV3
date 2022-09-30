using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using LibraryPhPro;
using UnityEngine;

public class AAAMYs
{
	public static long timeAk;

	public static bool sanboss;

	public static List<string> listboss;

	public static bool isAutoLogin;

	public static bool xindau;

	public static bool thudau;

	public static bool chodau;

	public static bool catdau;

	public static bool isOpenCskb;

	public static bool hs;

	public static bool isGMT;

	public static int IDCharGMT;

	public static int IDMObGMT;

	public static bool isgmtchar;

	public static bool isgmtmob;

	public static bool isXoaMap;

	public static bool isAutoFire;

	public static bool isMiniMap;

	public static bool iskepkhu;

	public static int khujoin;

	public static int numberplayer;

	public static int idmapkhu;

	public static List<string> listTOOL;

	public static int TestMini;

	public static bool isAutoCskb;

	public static Thread threadUseMd;

	public static bool isUseMd;

	public static bool isUDTvaAntiMatsong;

	public static bool isCoolMob;

	public static bool isAutoUseMD;

	public static long timeKillMe;

	public static int LastZone;

	public static Thread threadAutoLogin;

	public static int iditem;

	public static int daban;

	public static bool autobmdo;

	public static bool bayNhanh;

	public static string statuspick;

	public static bool autopickkhitansat;

	public static bool pickts2;

	public static bool pickme;

	public static bool pickonlygem;

	public static bool pickall;

	public static int indexPick;

	public static long timepick;

	public static bool isNhat;

	public static int TimeTaiXiu;

	public static long timeTru;

	public static string Acc;

	public static string Pass;

	public static string Id;

	public static int Server;

	public static Thread threadCatDau;

	public static Thread threadXinDau;

	public static Thread threadChoDau;

	public static Thread threadThuDau;

	public static Thread threadAutoFire;

	public static long Settimepick;

	public static long TimeAkTime;

	public static long TimeSearchitem;

	public static bool autopickplay;

	public static bool tsplay22222;

	public static List<int> listmob;

	public static bool tdlt1;

	public static int speedts;

	public static bool tiengviet;

	public static List<int> updekok;

	public static int tocdochay;

	public static bool isupkok;

	public static List<int> vitrix;

	public static List<int> vitriy;

	public static bool iscvt;

	public static string ndchat;

	public static bool atc;

	public static string ip;

	public static bool autodoikhu;

	public static bool firtMove;

	public static bool isupt77;

	public static bool isPemNoTn;

	public static bool isautofiret77;

	public static int xT77;

	public static bool cccc;

	public static bool t77dungim;

	public static int cccc2;

	public static long timecheckt77;

	public static long timeCheckTn;

	public static bool deoratnt77;

	public static bool casieuquai;

	public static long timeskillllllauto;

	public static bool skillat;

	public static int skills;

	public static long atuseskilltime;

	public static bool isupss;

	public static bool isautocn;

	public static long timeCtg;

	public static long TimeAutoCtg;

	public static string Ndctg;

	public static bool isautoctg;

	public static int currCtg;

	public static int Abfhp;

	public static bool Autobuffffff;

	public static int Abfki;

	public static int abf;

	public static bool buffftime;

	public static int timebuff;

	public static int abfhpdt;

	public static int abfkidt;

	public static bool buffdt;

	public static bool isAutoDoanhTrai;

	public static string chatvipptb;

	public static long timetb;

	public static sbyte action;

	public static int itemid;

	public static int money;

	public static int quaintly;

	public static sbyte moneyType;

	public static bool iskigui;

	public static bool isautogx;

	public static long timeusegx;

	public static long timeusecn;

	public static bool isautobh;

	public static long timeusebh;

	public static bool isshowplayer;

	public static void SendAttackToCharFocus()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(Char.myCharz().charFocus);
		Service.gI().sendPlayerAttack(new MyVector(), myVector, 2);
	}

	public static void SendAttackToMobFocus()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(Char.myCharz().mobFocus);
		Service.gI().sendPlayerAttack(myVector, new MyVector(), 1);
	}

	public static void Update()
	{
		AutoGiapXen();
		if (iskigui && GameCanvas.gameTick % 40 == 0)
			Service.gI().kigui(action, itemid, moneyType, money, quaintly);
		AutoDoanhTrai();
		if (isautoctg)
			AutoCTG();
		if (isupss && GameCanvas.gameTick % 70 == 0)
		{
			if (Char.myCharz().cgender == 2)
				aaAutoup.UpSSXayda();
			else if (Char.myCharz().cgender == 0)
			{
				aaAutoup.UpSSTD();
			}
		}
		if (isupt77)
		{
			if (GameCanvas.gameTick % 30 == 0)
				GotoDongNamKarin();
			if (TileMap.mapID == 111)
			{
				AKToT77();
				CheckTn(0);
				BugT77DungIm();
			}
			else
			{
				t77dungim = false;
				isautofiret77 = false;
				firtMove = false;
				deoratnt77 = false;
				timeCheckTn = mSystem.currentTimeMillis();
			}
		}
		if (isupt77 && GameCanvas.gameTick % 20 == 0)
			AutoT77();
		if (tocdochay != 0)
			Char.myCharz().cspeed = tocdochay;
		if (GameCanvas.gameTick % 50 == 0 && Char.myCharz().meDead && hs)
			Service.gI().wakeUpFromDead();
		if (GameCanvas.gameTick % 14 == 0 && Char.myCharz().havePet)
			Service.gI().petInfo();
		if (GameCanvas.gameTick % 150 == 0 && Char.myCharz().havePet && isUDTvaAntiMatsong)
			try
			{
				if (Char.myPetz().cStamina * 100 / Char.myPetz().cMaxStamina < 2 && !Char.myCharz().doUsePotion())
					GameScr.info1.addInfo(mResources.HP_EMPTY, 0);
			}
			catch
			{
			}
		if (isUDTvaAntiMatsong && mSystem.currentTimeMillis() - timeKillMe > 150000L)
		{
			AttackMyself();
			timeKillMe = mSystem.currentTimeMillis();
		}
		AutoBanDo();
		InitListTool();
		AutoZone();
		LockFocus();
		if (!ItemTime.isExistItem(2758) && GameCanvas.gameTick % 350 == 0 && !isUseMd && isAutoUseMD)
		{
			isUseMd = true;
			threadUseMd = new Thread((ThreadStart)delegate
			{
				AutoUseMayDo();
			});
			threadUseMd.IsBackground = true;
			threadUseMd.Start();
		}
		if (isOpenCskb && GameCanvas.gameTick % 6 == 0)
			AutoOpenCSKB();
		if (pickme)
			PickMyItem();
		if (pickonlygem)
			PickGem();
		if (pickall)
			PickAll();
		if (skillat && mSystem.currentTimeMillis() - timeskillllllauto > atuseskilltime)
		{
			timeskillllllauto = mSystem.currentTimeMillis();
			GameScr.gI().doUseSkill(GameScr.onScreenSkill[skills], true);
		}
		AutoChat();
		if (mSystem.currentTimeMillis() - timetb > 400000L)
		{
			timetb = mSystem.currentTimeMillis();
			GameScr.gI().chatVip(chatvipptb);
		}
	}

	public static void AutoFire()
	{
		while (isAutoFire)
		{
			if (mSystem.currentTimeMillis() - timeAk > ((TimeAkTime == 0L) ? (Char.myCharz().myskill.coolDown + 250) : TimeAkTime))
			{
				if (Char.myCharz().mobFocus != null)
					SendAttackToMobFocus();
				else if (Char.myCharz().charFocus != null)
				{
					SendAttackToCharFocus();
				}
				timeAk = mSystem.currentTimeMillis();
			}
			Thread.Sleep(20);
		}
	}

	static AAAMYs()
	{
		updekok = new List<int>();
		vitrix = new List<int>();
		vitriy = new List<int>();
		ip = "Vũ Trụ 1:112.213.94.23:14445:0,Vũ Trụ 2:210.211.109.199:14445:0,Vũ Trụ 3:112.213.85.88:14445:0,Vũ Trụ 4:27.0.12.164:14445:0,Vũ Trụ 5:27.0.12.16:14445:0,Vũ Trụ 6:27.0.12.173:14445:0,Vũ Trụ 7:112.213.94.223:14445:0,Vũ Trụ 8:27.0.14.77:14445:0,Vũ Trụ 9:112.213.85.53:14445:0,Võ đài Liên Trụ:27.0.12.173:20000:0,Naga:54.179.255.27:14446:2,Universe 1:54.179.255.27:14445:1,0,5";
		Settimepick = 600L;
		listmob = new List<int>();
		speedts = 15;
		statuspick = "Tắt.";
		listboss = new List<string>();
		listTOOL = new List<string>();
		TestMini = 50;
	}

	public static void empty_method_0()
	{
	}

	public static bool Chat(string text)
	{
		switch (text)
		{
		case "akigui":
			if (money == 0)
				GameScr.info1.addInfo("Vui lòng kí gửi 1 lần", 0);
			else
			{
				iskigui = !iskigui;
				if (iskigui)
					GameScr.info1.addInfo("Đã Bật Auto kí gửi", 0);
				else
					GameScr.info1.addInfo("Đã Tắt Auto kí gửi", 0);
			}
			return true;
		case "abf":
			Autobuffffff = !Autobuffffff;
			new Thread((ThreadStart)delegate
			{
				while (Autobuffffff)
				{
					if (Char.myCharz().cHP < Abfhp || Char.myCharz().cMP < Abfki)
					{
						GameScr.gI().doUseHP();
						Thread.Sleep(1000);
					}
				}
			}).Start();
			if (Autobuffffff)
				GameScr.info1.addInfo("Đã Bật Auto Buff Đậu Theo Chỉ Số", 0);
			else
				GameScr.info1.addInfo("Đã Tắt Auto Buff Đậu Theo Chỉ Số", 0);
			return true;
		case "abfdt":
			buffdt = !buffdt;
			if (abfkidt == 0)
			{
				GameScr.info1.addInfo("Chưa cài buff KI", 0);
				ChatTextField.gI().isShow = false;
			}
			else if (abfhpdt == 0)
			{
				GameScr.info1.addInfo("Chưa cài buff HP", 0);
				ChatTextField.gI().isShow = false;
			}
			else
			{
				new Thread((ThreadStart)delegate
				{
					while (Autobuffffff)
					{
						if (Char.myCharz().cHP < Abfhp || Char.myCharz().cMP < Abfki)
						{
							GameScr.gI().doUseHP();
							Thread.Sleep(1000);
						}
					}
				}).Start();
			}
			if (buffdt)
				GameScr.info1.addInfo("Đã Bật Auto Buff Đậu Theo Chỉ Số Đệ Tử", 0);
			else
				GameScr.info1.addInfo("Đã Tắt Auto Buff Đậu Theo Chỉ Số Đệ Tử", 0);
			return true;
		default:
			try
			{
				if (text.Contains("go"))
				{
					int index = int.Parse(text.Replace("go", ""));
					Char @char = (Char)GameScr.vCharInMap.elementAt(index);
					TeleportTo(@char.cx, @char.cy);
					return true;
				}
				if (text.Contains("abfhpdt"))
				{
					abfhpdt = int.Parse(text.Replace("abfhpdt", ""));
					GameScr.info1.addInfo("Buff đậu khi HP đệ dưới " + abfhpdt, 0);
					return true;
				}
				if (text.Contains("abfkidt"))
				{
					abfkidt = int.Parse(text.Replace("abfkidt", ""));
					GameScr.info1.addInfo("Buff đậu khi KI đệ dưới " + abfkidt, 0);
					return true;
				}
				if (text.Contains("abfhp"))
				{
					Abfhp = int.Parse(text.Replace("abfhp", ""));
					GameScr.info1.addInfo("Buff đậu khi HP dưới " + Abfhp, 0);
					return true;
				}
				if (text.Contains("abfki"))
				{
					Abfki = int.Parse(text.Replace("abfki", ""));
					GameScr.info1.addInfo("Buff đậu khi KI dưới " + Abfki, 0);
					return true;
				}
				if (text.Contains("goto "))
				{
					Char.myCharz().cx = int.Parse(text.Split(' ')[1]);
					Char.myCharz().cy = int.Parse(text.Split(' ')[2]);
					Service.gI().charMove();
					return true;
				}
				if (text.Contains("sm"))
				{
					Char.myCharz().cPower = long.Parse(text.Replace("sm", ""));
					return true;
				}
				if (text.Contains("tn"))
				{
					Char.myCharz().cTiemNang = long.Parse(text.Replace("tn", ""));
					return true;
				}
				if (text.Contains("vang"))
				{
					Char.myCharz().xu = int.Parse(text.Replace("vang", ""));
					return true;
				}
				if (text.Contains("ngoc"))
				{
					Char.myCharz().luong = int.Parse(text.Replace("ngoc", ""));
					return true;
				}
				if (text.Contains("actg "))
				{
					TimeAutoCtg = long.Parse(text.Replace("actg ", ""));
					string text2 = File.ReadAllText("Data\\ndctg.txt");
					if (isautoctg)
					{
						GameScr.info1.addInfo("Đã Tắt Auto Chat thế giới!", 0);
						isautoctg = false;
					}
					else if (text2 == "")
					{
						GameScr.info1.addInfo("Chưa Có Nội Dung Chat thế giới!", 0);
					}
					else
					{
						isautoctg = true;
						GameScr.info1.addInfo("Đã Bật Auto Chat thế giới " + TimeAutoCtg + "mili giây", 0);
					}
					return true;
				}
				if (text.Contains("td"))
				{
					Time.timeScale = int.Parse(text.Replace("td", ""));
					return true;
				}
				if (text.Contains("ts"))
				{
					speedts = int.Parse(text.Replace("ts", ""));
					GameScr.info1.addInfo("Delay tàn sát: " + speedts, 0);
					return true;
				}
				if (text.Contains("autos "))
				{
					skills = int.Parse(text.Split(' ')[1]) - 1;
					atuseskilltime = long.Parse(text.Split(' ')[2]) * 1000L;
					GameScr.info1.addInfo("Đã " + "cài tự dùng skill " + (skills + 1) + " " + atuseskilltime / 1000L + " giây.", 0);
					return true;
				}
				if (text.Contains("s"))
				{
					tocdochay = int.Parse(text.Replace("s", ""));
					GameScr.info1.addInfo("Tốc Độ Chạy " + tocdochay + "!", 0);
					return true;
				}
				if (text.Contains("l"))
				{
					Char.myCharz().cx = Char.myCharz().cx + int.Parse(text.Replace("l", ""));
					Service.gI().charMove();
					return true;
				}
				if (text.Contains("d"))
				{
					Char.myCharz().cy = Char.myCharz().cy + int.Parse(text.Replace("d", ""));
					Service.gI().charMove();
					return true;
				}
				if (text.Contains("u"))
				{
					Char.myCharz().cy = Char.myCharz().cy - int.Parse(text.Replace("u", ""));
					Service.gI().charMove();
					return true;
				}
				if (text.Contains("r"))
				{
					Char.myCharz().cx = Char.myCharz().cx - int.Parse(text.Replace("r", ""));
					Service.gI().charMove();
					return true;
				}
				if (text.Contains("npc"))
				{
					Service.gI().openMenu(int.Parse(text.Replace("npc", "")));
					return true;
				}
				if (text.Contains("ak"))
					try
					{
						TimeAkTime = long.Parse(text.Replace("ak", ""));
						try
						{
							threadAutoFire.Abort();
						}
						catch
						{
						}
						SetAutoPickMode();
						timeAk = 0L;
						isAutoFire = true;
						threadAutoFire = new Thread((ThreadStart)delegate
						{
							AutoFire();
						});
						threadAutoFire.Start();
						GameScr.info1.addInfo("Bạn đã bật tự đánh: " + TimeAkTime + "mili giây.", 0);
						return true;
					}
					catch
					{
					}
				if (text.Contains("co"))
				{
					Service.gI().getFlag(1, (sbyte)int.Parse(text.Replace("co", "")));
					return true;
				}
				if (text.Contains("pick"))
					try
					{
						Settimepick = long.Parse(text.Replace("pick", ""));
						GameScr.info1.addInfo("Bạn đã chỉnh thời gian nhặt là: " + Settimepick + "mili giây.", 0);
						return true;
					}
					catch
					{
					}
				if (text.Contains("zone"))
					try
					{
						int num = int.Parse(text.Replace("zone", ""));
						idmapkhu = TileMap.mapID;
						iskepkhu = !iskepkhu;
						khujoin = num;
						GameScr.info1.addInfo("Auto vào khu " + (iskepkhu ? (khujoin + " bật.") : "Tắt"), 0);
						return true;
					}
					catch
					{
					}
				if (text.Contains("k"))
					try
					{
						int zoneId = int.Parse(text.Replace("k", ""));
						Service.gI().requestChangeZone(zoneId, -1);
						return true;
					}
					catch
					{
					}
				if (text.Contains("csb"))
				{
					Service.gI().requestMapSelect(int.Parse(text.Replace("csb", "")));
					return true;
				}
			}
			catch
			{
			}
			switch (text)
			{
			case "banphimao":
				if (GameScr.isAnalog == 0)
				{
					GameScr.isAnalog = 1;
					GameScr.info1.addInfo("Đã " + ((GameScr.isAnalog == 1) ? "bật" : "tắt") + " bàn phím ảo", 0);
					ChatTextField.gI().isShow = false;
				}
				else if (GameScr.isAnalog == 1)
				{
					GameScr.isAnalog = 0;
					GameScr.info1.addInfo("Đã " + ((GameScr.isAnalog == 1) ? "bật" : "tắt") + " bàn phím ảo", 0);
					ChatTextField.gI().isShow = false;
				}
				return true;
			case "ak":
				try
				{
					threadAutoFire.Abort();
				}
				catch
				{
				}
				TimeAkTime = 0L;
				timeAk = 0L;
				isAutoFire = !isAutoFire;
				threadAutoFire = new Thread((ThreadStart)delegate
				{
					AutoFire();
				});
				threadAutoFire.Start();
				GameScr.info1.addInfo("Đã " + (isAutoFire ? "bật" : "tắt") + " tự động đánh.", 0);
				return true;
			case "tiengviet":
				tiengviet = !tiengviet;
				GameScr.info1.addInfo("Đã " + (tiengviet ? "bật" : "tắt") + " chat tiếng việt!", 0);
				return true;
			case "atc":
			{
				string text3 = File.ReadAllText("Data\\ndchat.txt");
				if (atc)
				{
					GameScr.info1.addInfo("Đã Tắt Auto Chat!", 0);
					atc = false;
				}
				else if (text3 == "")
				{
					GameScr.info1.addInfo("Chưa Có Nội Dung Chat!", 0);
				}
				else
				{
					atc = true;
					GameScr.info1.addInfo("Đã Bật Auto Chat!", 0);
				}
				return true;
			}
			case "night":
				if (mGraphics.zoomLevel > 1 && !GameCanvas.lowGraphic)
					GameScr.gI().doiMauTroi();
				return true;
			case "tvt":
				vitrix.Add(Char.myCharz().cx);
				vitriy.Add(Char.myCharz().cy);
				GameScr.info1.addInfo("Đã thêm vị trí " + vitrix.Count, 0);
				return true;
			case "xvt":
				vitrix.Clear();
				vitriy.Clear();
				GameScr.info1.addInfo("Đã xóa vị trí", 0);
				return true;
			case "cvt":
				iscvt = !iscvt;
				if (vitrix.Count == 0)
				{
					GameScr.info1.addInfo("Vui lòng thêm vị trí", 0);
					ChatTextField.gI().isShow = false;
				}
				else
				{
					GameScr.info1.addInfo("Đã " + (iscvt ? "bật" : "tắt") + " tự đổi vị trí", 0);
					new Thread((ThreadStart)delegate
					{
						int num2 = 0;
						while (iscvt)
						{
							Char.myCharz().cx = vitrix.ToArray()[num2];
							Char.myCharz().cy = vitriy.ToArray()[num2];
							num2++;
							if (num2 == vitrix.Count)
								num2 = 0;
							Thread.Sleep(3000);
						}
					}).Start();
				}
				return true;
			case "upkok":
				updekok.Clear();
				isupkok = !isupkok;
				GameScr.info1.addInfo("Đã " + (isupkok ? "bật" : "tắt") + " up đệ kok.", 0);
				new Thread((ThreadStart)delegate
				{
					updekok.Add(Char.myCharz().cx + 20);
					updekok.Add(Char.myCharz().cx - 20);
					int num3 = 0;
					while (isupkok)
					{
						Char.myCharz().cy -= 2;
						Service.gI().charMove();
						Char.myCharz().cy += 2;
						Service.gI().charMove();
						num3++;
						if (num3 == updekok.Count)
							num3 = 0;
						Thread.Sleep(2000);
					}
				}).Start();
				return true;
			case "tl":
				Service.gI().openMenu(38);
				return true;
			case "add":
				if (Char.myCharz().mobFocus == null)
				{
					GameScr.info1.addInfo("Bạn phải chỉ vào quái.", 0);
					ChatTextField.gI().isShow = false;
				}
				else if (listmob.Contains(Char.myCharz().mobFocus.mobId))
				{
					GameScr.info1.addInfo("Đã có trong danh sách.", 0);
					ChatTextField.gI().isShow = false;
				}
				else
				{
					listmob.Add(Char.myCharz().mobFocus.mobId);
					GameScr.info1.addInfo("Đã thêm " + Char.myCharz().mobFocus.mobId, 0);
				}
				return true;
			case "rcvt":
				listmob.Clear();
				GameScr.info1.addInfo("Đã xóa danh sách tàn săt.", 0);
				return true;
			case "vts":
				tdlt1 = !tdlt1;
				GameScr.info1.addInfo("TDLT " + (tdlt1 ? "v1" : "v2"), 0);
				return true;
			case "dongbang":
				if (Application.runInBackground)
				{
					Application.runInBackground = false;
					GameScr.info1.addInfo("Đã bật đóng băng game.", 0);
				}
				else
				{
					GameScr.info1.addInfo("Đã tắt đóng băng game.", 0);
					Application.runInBackground = true;
				}
				break;
			}
			switch (text)
			{
			case "bando":
				autobmdo = !autobmdo;
				GameScr.info1.addInfo("Đã " + (autobmdo ? "bật" : "tắt") + " auto bán đồ.", 0);
				return true;
			case "copy":
				if (Char.myCharz().charFocus != null)
				{
					Char.myCharz().head = Char.myCharz().charFocus.head;
					Char.myCharz().leg = Char.myCharz().charFocus.leg;
					Char.myCharz().body = Char.myCharz().charFocus.body;
				}
				if (Char.myCharz().mobFocus != null)
				{
					Char.myCharz().cp1 = Char.myCharz().mobFocus.p1;
					Char.myCharz().cp2 = Char.myCharz().mobFocus.p1;
					Char.myCharz().cp3 = Char.myCharz().mobFocus.p1;
				}
				if (Char.myCharz().npcFocus != null)
				{
					Char.myCharz().head = Char.myCharz().npcFocus.head;
					Char.myCharz().leg = Char.myCharz().npcFocus.leg;
					Char.myCharz().body = Char.myCharz().npcFocus.body;
				}
				GameScr.info1.addInfo("PhoToCopy hehe.", 0);
				return true;
			case "gmt":
				isGMT = !isGMT;
				isgmtchar = false;
				isgmtmob = false;
				GameScr.info1.addInfo("Đã " + (isGMT ? "bật" : "tắt") + " auto giữ mục tiêu.", 0);
				if (Char.myCharz().charFocus != null)
				{
					isgmtchar = true;
					IDCharGMT = Char.myCharz().charFocus.charID;
				}
				if (Char.myCharz().mobFocus != null)
				{
					isgmtmob = true;
					IDMObGMT = Char.myCharz().mobFocus.mobId;
				}
				return true;
			case "mocskb":
				isOpenCskb = true;
				return true;
			case "abh":
				GameScr.info1.addInfo("Đã dùng bổ huyết.", 0);
				AnBoHuyet();
				return true;
			case "acn":
				GameScr.info1.addInfo("Đã dùng cuồng nộ.", 0);
				AnBoHuyet();
				return true;
			case "ahs":
				hs = !hs;
				if (hs)
					GameScr.info1.addInfo("Đã bật Auto Hồi sinh", 0);
				else
					GameScr.info1.addInfo("Đã tắt Auto Hồi sinh", 0);
				return true;
			case "autocn":
				isautocn = !isautocn;
				GameScr.info1.addInfo("Auto dùng cuồng nộ " + (isautocn ? "Bật" : "tắt"), 0);
				return true;
			default:
				Service.gI().chat(text);
				return false;
			}
		}
	}

	public static void AutoOpenCSKB()
	{
		sbyte b = 0;
		while (true)
		{
			if (b >= Char.myCharz().arrItemBag.Length)
				return;
			if (Char.myCharz().arrItemBag[b].template.id == 380 && Char.myCharz().arrItemBag[b].quantity < 3)
				break;
			if (Char.myCharz().arrItemBag[b].template.id == 380)
				try
				{
					Service.gI().useItem(0, 1, b, -1);
					return;
				}
				catch
				{
				}
			b = (sbyte)(b + 1);
		}
		isOpenCskb = false;
	}

	public static void UseCapsule()
	{
		try
		{
			sbyte b = 0;
			while (true)
			{
				if (b < Char.myCharz().arrItemBag.Length)
				{
					if (Char.myCharz().arrItemBag[b].template.id == 193 || Char.myCharz().arrItemBag[b].template.id == 194)
						break;
					b = (sbyte)(b + 1);
					continue;
				}
				return;
			}
			Service.gI().useItem(0, 1, b, -1);
		}
		catch
		{
		}
	}

	public static void UsePorata()
	{
		try
		{
			sbyte b = 0;
			while (true)
			{
				if (b < Char.myCharz().arrItemBag.Length)
				{
					if (Char.myCharz().arrItemBag[b].template.id == 454)
						break;
					b = (sbyte)(b + 1);
					continue;
				}
				return;
			}
			Service.gI().useItem(0, 1, b, -1);
		}
		catch
		{
		}
	}

	public static void unused_method_3()
	{
		try
		{
			GameScr.info1.addInfo("Đã dùng bổ khí!", 0);
			sbyte b = 0;
			while (true)
			{
				if (b < Char.myCharz().arrItemBag.Length)
				{
					if (Char.myCharz().arrItemBag[b].template.id == 383)
						break;
					b = (sbyte)(b + 1);
					continue;
				}
				return;
			}
			Service.gI().useItem(0, 1, b, -1);
		}
		catch
		{
		}
	}

	public static void AnBoHuyet()
	{
		try
		{
			GameScr.info1.addInfo("Đã dùng bổ huyết!", 0);
			sbyte b = 0;
			while (true)
			{
				if (b < Char.myCharz().arrItemBag.Length)
				{
					if (Char.myCharz().arrItemBag[b].template.id == 382)
						break;
					b = (sbyte)(b + 1);
					continue;
				}
				return;
			}
			Service.gI().useItem(0, 1, b, -1);
		}
		catch
		{
		}
	}

	public static void AnGiapXen()
	{
		try
		{
			GameScr.info1.addInfo("Đã dùng giáp sên!", 0);
			sbyte b = 0;
			while (true)
			{
				if (b < Char.myCharz().arrItemBag.Length)
				{
					if (Char.myCharz().arrItemBag[b].template.id == 384)
						break;
					b = (sbyte)(b + 1);
					continue;
				}
				return;
			}
			Service.gI().useItem(0, 1, b, -1);
		}
		catch
		{
		}
	}

	public static void unused_method_0()
	{
		try
		{
			GameScr.info1.addInfo("Đã dùng ẩn danh!", 0);
			sbyte b = 0;
			while (true)
			{
				if (b < Char.myCharz().arrItemBag.Length)
				{
					if (Char.myCharz().arrItemBag[b].template.id == 385)
						break;
					b = (sbyte)(b + 1);
					continue;
				}
				return;
			}
			Service.gI().useItem(0, 1, b, -1);
		}
		catch
		{
		}
	}

	public static void AnCuongNo()
	{
		try
		{
			sbyte b = 0;
			while (true)
			{
				if (b < Char.myCharz().arrItemBag.Length)
				{
					if (Char.myCharz().arrItemBag[b].template.id == 381)
						break;
					b = (sbyte)(b + 1);
					continue;
				}
				return;
			}
			Service.gI().useItem(0, 1, b, -1);
		}
		catch
		{
		}
	}

	public static void LockFocus()
	{
		if (!isGMT)
			return;
		if (isgmtchar)
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				Char @char = (Char)GameScr.vCharInMap.elementAt(i);
				if (@char.charID == IDCharGMT)
				{
					Char.myCharz().charFocus = @char;
					Char.myCharz().mobFocus = null;
				}
			}
		}
		if (!isgmtmob)
			return;
		for (int j = 0; j < GameScr.vMob.size(); j++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(j);
			if (mob.mobId == IDMObGMT)
			{
				Char.myCharz().charFocus = null;
				Char.myCharz().mobFocus = mob;
			}
		}
	}

	public static void Paint(mGraphics g)
	{
		g.drawImage(mResources.img8sao, GameCanvas.w / 2, 26, StaticObj.BOTTOM_HCENTER);
		if (isMiniMap)
			PaintMiniMap(g, GameCanvas.w - TileMap.pxw / 11 - 10, 90);
		if (iskepkhu)
			mFont.tahoma_7_red.drawString(g, "Đang tự động vào khu " + khujoin + " Số người: " + numberplayer, GameCanvas.w / 2, GameCanvas.h / 2, mFont.CENTER);
		if (sanboss)
			try
			{
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					Char @char = (Char)GameScr.vCharInMap.elementAt(i);
					if (@char.cTypePk == 5)
					{
						g.setColor(Color.red);
						g.drawLine(Char.myCharz().cx - GameScr.cmx, Char.myCharz().cy - GameScr.cmy + 10, @char.cx - GameScr.cmx, @char.cy - GameScr.cmy + 9);
					}
				}
				int num = 153;
				foreach (string item in listboss.AsEnumerable().Reverse())
				{
					string[] array = item.Split('-');
					DateTime value = Convert.ToDateTime(array[2]);
					TimeSpan timeSpan = DateTime.Now.Subtract(value);
					int num2 = (int)timeSpan.TotalSeconds;
					((!array[1].Trim().Contains(TileMap.mapName)) ? mFont.number_yellow : mFont.number_red).drawString(g, array[0].Replace("BOSS ", "") + array[1].Replace("zona", "khu") + " - " + ((num2 < 60) ? (num2 + "s") : (timeSpan.Minutes + "p")) + " trước", 500, GameCanvas.h - num, mFont.RIGHT, mFont.tahoma_7_grey);
					num -= 9;
				}
			}
			catch
			{
			}
		if (GameScr.isAutoPlay && GameScr.canAutoPlay)
			mFont.number_green.drawString(g, "Đang Auto Luyện tập " + (tdlt1 ? "V1" : "V2") + " Vơ\u0301i Delay : " + speedts, 25, GameCanvas.h - 100, 0);
		if (!GameScr.isAutoPlay && GameScr.canAutoPlay)
			mFont.number_green.drawString(g, "Đánh quái để tiếp tục TDLT!", 25, GameCanvas.h - 100, 0);
		if (!isshowplayer)
			return;
		try
		{
			mFont.number_orange.drawString(g, "-Chat goX để dịch tới! Vd: go0", 25, GameCanvas.h - 210, 0);
			int num3 = 200;
			for (int j = 0; j < GameScr.vCharInMap.size(); j++)
			{
				Char char2 = (Char)GameScr.vCharInMap.elementAt(j);
				((char2.cTypePk != 5) ? mFont.number_green : mFont.number_red).drawString(g, j + "-" + char2.cName + " " + char2.cHP * 100 / char2.cHPFull + "%", 25, GameCanvas.h - num3, mFont.LEFT, mFont.tahoma_7_grey);
				num3 -= 9;
			}
		}
		catch
		{
		}
	}

	public static void PaintMiniMap(mGraphics g, int x, int y)
	{
		try
		{
			int num = 11;
			g.fillRect(x, y, TileMap.pxw / 11, TileMap.pxh / 11, 0, 110);
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				Char @char = (Char)GameScr.vCharInMap.elementAt(i);
				Char char2 = Char.myCharz();
				mFont.tahoma_7b_yellowSmall2.drawString(g, "*", x + char2.cx / num, y + char2.cy / num, 0);
				if (@char.cTypePk >= 5)
					mFont.tahoma_7_red.drawString(g, "*", x + @char.cx / num, y + @char.cy / num, 0);
				else
					mFont.tahoma_7_blue1Small.drawString(g, "*", x + @char.cx / num, y + @char.cy / num, 0);
			}
			g.setColor(Color.blue);
			g.drawRect(x, y, TileMap.pxw / num, TileMap.pxh / num);
			for (int j = 0; j < TileMap.pxw; j += TestMini)
			{
				mFont.number_red.drawString(g, "-", x + j / num, y + AAAeee.GetYGround(j) / num, 0);
			}
		}
		catch
		{
		}
	}

	public static void unused_method_1(mGraphics mGraphics_0)
	{
	}

	public static void AutoZone()
	{
		if (!iskepkhu || GameCanvas.gameTick % 7 != 0)
			return;
		Service.gI().openUIZone();
		if (idmapkhu != TileMap.mapID)
		{
			iskepkhu = false;
			GameScr.info1.addInfo("Auto vào khu tắt", 0);
			return;
		}
		if (khujoin == TileMap.zoneID)
		{
			iskepkhu = false;
			GameScr.info1.addInfo("Auto vào khu tắt", 0);
			return;
		}
		try
		{
			numberplayer = GameScr.gI().numPlayer[khujoin];
			if (numberplayer < 12)
				Service.gI().requestChangeZone(khujoin, -1);
		}
		catch
		{
			Service.gI().openUIZone();
			numberplayer = GameScr.gI().numPlayer[khujoin];
			if (numberplayer < 12)
				Service.gI().requestChangeZone(khujoin, -1);
		}
	}

	public static void InitListTool()
	{
		listTOOL.Clear();
		string text = "Trạng thái:";
		listTOOL.Add("Tự động nhặt.;" + text + " đang " + statuspick);
		listTOOL.Add("Hiện map thu nhỏ.;" + text + " đang " + (isMiniMap ? "bật." : "tắt."));
		listTOOL.Add("Hiện người trong khu.;" + text + " đang " + (isshowplayer ? "bật." : "tắt."));
		listTOOL.Add("Chế độ săn boss.;" + text + " đang " + (sanboss ? "bật." : "tắt."));
		listTOOL.Add("Tự bơm đậu up đệ và tự đánh.;" + text + " đang " + (isUDTvaAntiMatsong ? "bật." : "tắt."));
		listTOOL.Add("Ẩn map.;" + text + " đang " + (isXoaMap ? "bật." : "tắt."));
		listTOOL.Add("Đóng băng quái.;" + text + " đang " + (isCoolMob ? "bật." : "tắt."));
		listTOOL.Add("Tự động dùng máy dò.;" + text + " đang " + (isAutoUseMD ? "bật." : "tắt."));
		listTOOL.Add("Tự động đăng nhập lại khi mất sóng.;" + text + " đang " + (isAutoLogin ? "bật." : "tắt."));
		listTOOL.Add("Tự cho đậu.;" + text + " đang " + (chodau ? "bật." : "tắt."));
		listTOOL.Add("Tự xin đậu.;" + text + " đang " + (xindau ? "bật." : "tắt."));
		listTOOL.Add("Tự thu hoạch đậu.;" + text + " đang " + (thudau ? "bật." : "tắt."));
		listTOOL.Add("Tự cất đậu vào rương.;" + text + " đang " + (catdau ? "bật." : "tắt."));
		listTOOL.Add("Tự động hồi sinh.;" + text + " đang " + (hs ? "bật." : "tắt."));
		listTOOL.Add("Tự động đổi khu khi tự động luyện tập ;" + text + " đang " + (autodoikhu ? "bật." : "tắt."));
		listTOOL.Add("Đánh cả siêu quái TDLT ;" + text + " đang " + (casieuquai ? "bật." : "tắt."));
		listTOOL.Add("Kiểu TDLT ;" + (tdlt1 ? " dịch chuyển tới quái." : "chạy bộ tới quái."));
		listTOOL.Add("Tự động up tàu 77.;" + text + " đang " + (isupt77 ? "bật." : "tắt."));
		listTOOL.Add("Tự động dùng skill.;" + text + " đang " + (skillat ? "bật." : "tắt."));
		listTOOL.Add("Chat tiếng việt.;" + text + " đang " + (tiengviet ? "bật." : "tắt."));
		listTOOL.Add("Up sơ sinh tự động.;" + text + " đang " + (isupss ? "bật." : "tắt."));
		listTOOL.Add("Up đệ kok.;" + text + " đang " + (isupkok ? "bật." : "tắt."));
		listTOOL.Add("Bàn phím ảo.;" + text + " đang " + ((GameScr.isAnalog == 1) ? "bật" : "tắt"));
		listTOOL.Add("Auto mở doanh trại.;" + text + " đang " + (isAutoDoanhTrai ? "bật." : "tắt."));
		listTOOL.Add("Auto chat.;" + text + " đang " + (atc ? "bật." : "tắt."));
		listTOOL.Add("Auto dùng cuồng nộ.;" + text + " đang " + (isautocn ? "bật." : "tắt."));
		listTOOL.Add("Auto dùng bổ huyết.;" + text + " đang " + (isautobh ? "bật." : "tắt."));
		listTOOL.Add("Auto dùng giáp xên.;" + text + " đang " + (isautogx ? "bật." : "tắt."));
		int count = listTOOL.Count;
		Char.myCharz().arrToolAuto = new aaToolInfo[count];
		for (int i = 0; i < count; i++)
		{
			string text2 = listTOOL.ElementAt(i);
			Char.myCharz().arrToolAuto[i] = new aaToolInfo();
			Char.myCharz().arrToolAuto[i].Line1 = i + ". " + text2.Split(';')[0];
			Char.myCharz().arrToolAuto[i].Line2 = text2.Split(';')[1];
		}
	}

	public static void OnMenuHackModSelected(int index)
	{
		switch (index)
		{
		case 0:
			if (indexPick >= 5)
			{
				indexPick = -1;
				pickts2 = false;
				pickme = false;
				pickall = false;
				pickonlygem = false;
				autopickkhitansat = false;
				statuspick = ":Tắt";
			}
			SetAutoPickMode(indexPick);
			break;
		case 1:
			isMiniMap = !isMiniMap;
			break;
		case 2:
			isshowplayer = !isshowplayer;
			break;
		case 3:
			sanboss = !sanboss;
			break;
		case 4:
			isUDTvaAntiMatsong = !isUDTvaAntiMatsong;
			break;
		case 5:
			isXoaMap = !isXoaMap;
			break;
		case 6:
			isCoolMob = !isCoolMob;
			break;
		case 7:
			isAutoUseMD = !isAutoUseMD;
			break;
		case 8:
			isAutoLogin = !isAutoLogin;
			break;
		case 9:
			try
			{
				threadChoDau.Abort();
			}
			catch
			{
			}
			chodau = !chodau;
			if (chodau)
			{
				threadChoDau = new Thread((ThreadStart)delegate
				{
					AutoChoDau();
				});
				threadChoDau.IsBackground = true;
				threadChoDau.Start();
			}
			break;
		case 10:
			try
			{
				threadChoDau.Abort();
			}
			catch
			{
			}
			xindau = !xindau;
			if (xindau)
			{
				threadXinDau = new Thread((ThreadStart)delegate
				{
					AutoXinDau();
				});
				threadXinDau.IsBackground = true;
				threadXinDau.Start();
			}
			break;
		case 11:
			try
			{
				threadThuDau.Abort();
			}
			catch
			{
			}
			thudau = !thudau;
			if (thudau)
			{
				threadThuDau = new Thread((ThreadStart)delegate
				{
					AutoThuDau();
				});
				threadThuDau.IsBackground = true;
				threadThuDau.Start();
			}
			break;
		case 12:
			try
			{
				threadCatDau.Abort();
			}
			catch
			{
			}
			catdau = !catdau;
			if (catdau)
			{
				threadCatDau = new Thread((ThreadStart)delegate
				{
					AutoCatDauVaoRuong();
				});
				threadCatDau.IsBackground = true;
				threadCatDau.Start();
			}
			break;
		case 13:
			hs = !hs;
			break;
		case 14:
			autodoikhu = !autodoikhu;
			break;
		case 15:
			casieuquai = !casieuquai;
			break;
		case 16:
			tdlt1 = !tdlt1;
			break;
		case 17:
			isupt77 = !isupt77;
			break;
		case 18:
			if (atuseskilltime == 0L)
				GameScr.info1.addInfo("Vui lòng cài đặt auto skill.", 0);
			else
				skillat = !skillat;
			break;
		case 19:
			tiengviet = !tiengviet;
			break;
		case 20:
			isupss = !isupss;
			break;
		case 21:
			updekok.Clear();
			isupkok = !isupkok;
			GameScr.info1.addInfo("Đã " + (isupkok ? "bật" : "tắt") + " up đệ kok.", 0);
			if (!isupkok)
				break;
			new Thread((ThreadStart)delegate
			{
				updekok.Add(Char.myCharz().cx + 20);
				updekok.Add(Char.myCharz().cx - 20);
				int num = 0;
				while (isupkok)
				{
					Char.myCharz().cy -= 2;
					Service.gI().charMove();
					Char.myCharz().cy += 2;
					Service.gI().charMove();
					num++;
					if (num == updekok.Count)
						num = 0;
					Thread.Sleep(2000);
				}
			}).Start();
			break;
		case 22:
			if (GameScr.isAnalog == 0)
			{
				GameScr.isAnalog = 1;
				GameScr.info1.addInfo("Đã " + ((GameScr.isAnalog == 1) ? "bật" : "tắt") + " bàn phím ảo", 0);
				ChatTextField.gI().isShow = false;
			}
			else if (GameScr.isAnalog == 1)
			{
				GameScr.isAnalog = 0;
				GameScr.info1.addInfo("Đã " + ((GameScr.isAnalog == 1) ? "bật" : "tắt") + " bàn phím ảo", 0);
				ChatTextField.gI().isShow = false;
			}
			break;
		case 23:
			isAutoDoanhTrai = !isAutoDoanhTrai;
			break;
		case 24:
		{
			string text = File.ReadAllText("Data\\ndchat.txt");
			if (atc)
			{
				GameScr.info1.addInfo("Đã Tắt Auto Chat!", 0);
				atc = false;
			}
			else if (text == "")
			{
				GameScr.info1.addInfo("Chưa Có Nội Dung Chat!", 0);
			}
			else
			{
				atc = true;
				GameScr.info1.addInfo("Đã Bật Auto Chat!", 0);
			}
			break;
		}
		case 25:
			isautocn = !isautocn;
			GameScr.info1.addInfo("Đã " + (isautocn ? "Bật" : "Tắt") + " Auto Dùng cuồng nộ!", 0);
			break;
		case 26:
			isautobh = !isautobh;
			GameScr.info1.addInfo("Đã " + (isautobh ? "Bật" : "Tắt") + " Auto Dùng bổ huyết!", 0);
			break;
		case 27:
			isautogx = !isautogx;
			GameScr.info1.addInfo("Đã " + (isautogx ? "Bật" : "Tắt") + " Auto Dùng giáp xên!", 0);
			break;
		}
	}

	public static void AutoUseMayDo()
	{
		GameScr.info1.addInfo("Sẽ tự dùng máy dò sau 150 giây.", 0);
		Thread.Sleep(150000);
		GameScr.info1.addInfo("Đã dùng máy dò.", 0);
		sbyte b = 0;
		while (b < Char.myCharz().arrItemBag.Length)
		{
			if (Char.myCharz().arrItemBag[b].template.id != 379)
			{
				b = (sbyte)(b + 1);
				continue;
			}
			Service.gI().useItem(0, 1, b, -1);
			break;
		}
		isUseMd = false;
		threadUseMd.Abort();
	}

	public static void AttackMyself()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(Char.myCharz());
		Service.gI().sendPlayerAttack(new MyVector(), myVector, -1);
	}

	public static void UpdateKey(int keyPress)
	{
		if (keyPress == 116)
		{
			GameScr.canAutoPlay = !GameScr.canAutoPlay;
			if (GameScr.canAutoPlay)
				GameScr.isAutoPlay = true;
			GameScr.info1.addInfo("Đã " + (GameScr.canAutoPlay ? "Bật" : "Tắt") + " Tự Động Luyện Tập", 0);
		}
		if (keyPress == 109)
		{
			GameScr.khuuuu = 555;
			Service.gI().openUIZone();
		}
		if (keyPress == 120)
			PhPro.Chat("xm");
		if (keyPress == 122)
		{
			string text = Directory.GetCurrentDirectory() + "\\HinhAnh\\";
			int i;
			for (i = 0; File.Exists("HinhAnh\\L2Image-" + i + ".png"); i++)
			{
			}
			string text2 = "L2Image-" + i + ".png";
			if (!Directory.Exists(text))
				Directory.CreateDirectory(text);
			Application.CaptureScreenshot(text + text2);
		}
		if (keyPress == 100 && isAutoDoanhTrai)
		{
			isAutoDoanhTrai = false;
			GameScr.info1.addInfo("Đã tắt auto doanh trại", 0);
		}
		if (keyPress == 102)
			UsePorata();
		if (keyPress == 99)
			UseCapsule();
		if (keyPress == 106)
			AAAeee.LoadMap(0);
		if (keyPress == 107)
			AAAeee.LoadMap(2);
		if (keyPress == 108)
			AAAeee.LoadMap(1);
	}

	public static void AutoBanDo()
	{
		if (!autobmdo || GameCanvas.gameTick % 30 != 0)
			return;
		if (daban >= 200)
		{
			autobmdo = false;
			GameScr.info1.addInfo("Đã " + (autobmdo ? "bật" : "tắt") + " auto bán đồ.", 0);
			return;
		}
		Service.gI().buyItem(0, iditem, 0);
		for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
		{
			if (Char.myCharz().arrItemBag[i].template.id == iditem)
			{
				Service.gI().saleItem(1, 1, (short)i);
				daban++;
			}
		}
	}

	public static void SetAutoPickMode(int int_0)
	{
		indexPick++;
		if (indexPick == 0)
		{
			pickts2 = false;
			pickme = true;
			pickall = false;
			pickonlygem = false;
			autopickkhitansat = false;
			statuspick = "chỉ nhặt của mình " + Settimepick + "mili";
		}
		else if (indexPick == 1)
		{
			pickts2 = false;
			pickall = true;
			pickonlygem = false;
			autopickkhitansat = false;
			pickme = false;
			statuspick = "nhặt tất cả " + Settimepick + "mili";
		}
		else if (indexPick == 2)
		{
			pickts2 = false;
			pickonlygem = true;
			pickme = false;
			autopickkhitansat = false;
			pickall = false;
			statuspick = "chỉ nhặt ngọc xanh " + Settimepick + "mili";
		}
		else if (indexPick == 3)
		{
			pickts2 = false;
			autopickkhitansat = true;
			pickall = false;
			pickme = false;
			pickonlygem = false;
			statuspick = "nhặt all khi tàn sát.";
		}
		else if (indexPick == 4)
		{
			pickts2 = true;
			autopickkhitansat = false;
			pickall = false;
			pickme = false;
			pickonlygem = false;
			statuspick = "nhặt đồ và vàng tàn sát.";
		}
		else if (indexPick == 5)
		{
			pickts2 = false;
			autopickkhitansat = false;
			pickall = false;
			pickme = false;
			pickonlygem = false;
			statuspick = "đang tắt.";
			indexPick = -1;
		}
	}

	public static void PickMyItem()
	{
		try
		{
			if (mSystem.currentTimeMillis() - timepick <= Settimepick)
				return;
			for (int i = 0; i < GameScr.vItemMap.size(); i++)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
				int num = Math.abs(Char.myCharz().cx - itemMap.x);
				if ((itemMap.playerId == Char.myCharz().charID || itemMap.playerId == -1) && num <= 70 && itemMap != null)
				{
					timepick = mSystem.currentTimeMillis();
					Char.myCharz().itemFocus = itemMap;
					Service.gI().pickItem(Char.myCharz().itemFocus.itemMapID);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public static void PickAll()
	{
		try
		{
			if (mSystem.currentTimeMillis() - timepick <= Settimepick)
				return;
			timepick = mSystem.currentTimeMillis();
			int[] array = new int[4] { -1, -1, -1, -1 };
			if (Char.myCharz().itemFocus == null)
			{
				for (int i = 0; i < GameScr.vItemMap.size(); i++)
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
					int num = Math.abs(Char.myCharz().cx - itemMap.x);
					int num2 = Math.abs(Char.myCharz().cy - itemMap.y);
					int num3 = ((num <= num2) ? num2 : num);
					if (num <= 48 && num2 <= 48 && (Char.myCharz().itemFocus == null || num3 < array[3]))
					{
						Char.myCharz().itemFocus = itemMap;
						array[3] = num3;
					}
				}
			}
			else if (Char.myCharz().itemFocus.template.id != 673 && Char.myCharz().itemFocus != null)
			{
				Service.gI().pickItem(Char.myCharz().itemFocus.itemMapID);
			}
		}
		catch (Exception)
		{
		}
	}

	public static void PickGem()
	{
		try
		{
			if (mSystem.currentTimeMillis() - timepick <= Settimepick)
				return;
			for (int i = 0; i < GameScr.vItemMap.size(); i++)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
				int num = Math.abs(Char.myCharz().cx - itemMap.x);
				if (itemMap.template.id == 77 && num <= 70 && itemMap != null)
				{
					timepick = mSystem.currentTimeMillis();
					Service.gI().pickItem(itemMap.itemMapID);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public static string unused_method_2()
	{
		string result = "0.0.0.0";
		IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
		foreach (IPAddress iPAddress in addressList)
		{
			if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
			{
				result = iPAddress.ToString();
				break;
			}
		}
		return result;
	}

	public static void Start()
	{
		Time.timeScale = 1.4f;
		string[] array = File.ReadAllText("Data\\Login").Split('|');
		Acc = array[2];
		Id = array[0];
		if (array[1] == "Naga")
			Server = 10;
		else if (array[1] == "1 Star")
		{
			Server = 11;
		}
		else
		{
			Server = int.Parse(array[1].Replace("Server ", "")) - 1;
		}
		Pass = array[3];
		tocdochay = 7;
		if (!File.Exists("Dragonboy_vn_187_Data\\dataq"))
			File.WriteAllText("Dragonboy_vn_187_Data\\dataq", ":v");
		if (File.ReadAllText("Dragonboy_vn_187_Data\\dataq") != DateTime.Now.ToString("MM/dd/yyyy"))
		{
			File.WriteAllText("Dragonboy_vn_187_Data\\dataq", DateTime.Now.ToString("MM/dd/yyyy"));
			Application.OpenURL("https://8sao.club/");
		}
		try
		{
			Thread thread = new Thread((ThreadStart)delegate
			{
				ServicePointManager.ServerCertificateValidationCallback = (object object_0, X509Certificate x509Certificate_0, X509Chain x509Chain_0, SslPolicyErrors sslPolicyErrors_0) => true;
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
				chatvipptb = new WebClient().DownloadString("https://8sao.club/tool/chat.php");
			});
			thread.IsBackground = true;
			thread.Start();
		}
		catch
		{
		}
	}

	public static void AutoCatDauVaoRuong()
	{
		while (catdau)
		{
			for (sbyte b = 0; b < Char.myCharz().arrItemBag.Length; b = (sbyte)(b + 1))
			{
				if (Char.myCharz().arrItemBag[b].template.type == 6)
				{
					try
					{
						Service.gI().getItem(1, b);
					}
					catch
					{
						continue;
					}
					break;
				}
			}
			Thread.Sleep(20000);
		}
	}

	public static void AutoXinDau()
	{
		while (xindau)
		{
			Service.gI().clanMessage(1, null, -1);
			Thread.Sleep(30000);
		}
	}

	public static void AutoChoDau()
	{
		while (chodau)
		{
			try
			{
				Service.gI().openUIZone();
				ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(0);
				for (int i = 0; i <= 5; i++)
				{
					Service.gI().clanDonate(clanMessage.id);
					Thread.Sleep(100);
				}
			}
			catch
			{
			}
			Thread.Sleep(10000);
		}
	}

	public static void AutoThuDau()
	{
		while (thudau)
		{
			Service.gI().openMenu(4);
			Service.gI().menu(4, 0, 0);
			Thread.Sleep(30000);
		}
	}

	public static void SetAutoPickMode()
	{
		if (indexPick == 0)
		{
			pickts2 = false;
			pickme = true;
			pickall = false;
			pickonlygem = false;
			autopickkhitansat = false;
			statuspick = "chỉ nhặt của mình " + Settimepick + "mili";
		}
		else if (indexPick == 1)
		{
			pickts2 = false;
			pickall = true;
			pickonlygem = false;
			autopickkhitansat = false;
			pickme = false;
			statuspick = "nhặt tất cả " + Settimepick + "mili";
		}
		else if (indexPick == 2)
		{
			pickts2 = false;
			pickonlygem = true;
			pickme = false;
			autopickkhitansat = false;
			pickall = false;
			statuspick = "chỉ nhặt ngọc xanh " + Settimepick + "mili";
		}
		else if (indexPick == 3)
		{
			pickts2 = false;
			autopickkhitansat = true;
			pickall = false;
			pickme = false;
			pickonlygem = false;
			statuspick = "nhặt all khi tàn sát.";
		}
		else if (indexPick == 4)
		{
			pickts2 = true;
			autopickkhitansat = false;
			pickall = false;
			pickme = false;
			pickonlygem = false;
			statuspick = "nhặt đồ và vàng tàn sát.";
		}
		else if (indexPick == 5)
		{
			pickts2 = false;
			autopickkhitansat = false;
			pickall = false;
			pickme = false;
			pickonlygem = false;
			statuspick = "đang tắt.";
			indexPick = -1;
		}
	}

	public static void PickItemWhenTanSat()
	{
		int num = 0;
		while (true)
		{
			if (num < GameScr.vItemMap.size())
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(num);
				if (Math.abs(Char.myCharz().cx - itemMap.x) <= 170 && (itemMap.playerId == Char.myCharz().charID || itemMap.playerId == -1))
				{
					if (mSystem.currentTimeMillis() - 700L < TimeSearchitem && TimeSearchitem != 0L)
						return;
					autopickplay = true;
					Char.myCharz().itemFocus = itemMap;
					TeleportTo(Char.myCharz().itemFocus.x, Char.myCharz().itemFocus.y);
					Service.gI().charMove();
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					if (Char.myCharz().itemFocus.template.id != 673)
						break;
				}
				num++;
				continue;
			}
			autopickplay = false;
			return;
		}
		TimeSearchitem = mSystem.currentTimeMillis();
		Service.gI().pickItem(Char.myCharz().itemFocus.itemMapID);
	}

	public static void PickItemWhenTanSat2()
	{
		int num = 0;
		while (true)
		{
			if (num < GameScr.vItemMap.size())
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(num);
				if (Math.abs(Char.myCharz().cx - itemMap.x) <= 170 && (itemMap.playerId == Char.myCharz().charID || itemMap.playerId == -1) && (itemMap.template.type == 0 || itemMap.template.type == 2 || itemMap.template.type == 3 || itemMap.template.type == 4 || itemMap.template.type == 5 || itemMap.template.type == 9))
				{
					if (mSystem.currentTimeMillis() - 700L < TimeSearchitem && TimeSearchitem != 0L)
						return;
					tsplay22222 = true;
					Char.myCharz().itemFocus = itemMap;
					TeleportTo(Char.myCharz().itemFocus.x, Char.myCharz().itemFocus.y);
					Service.gI().charMove();
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
					if (Char.myCharz().itemFocus.template.id != 673)
						break;
				}
				num++;
				continue;
			}
			tsplay22222 = false;
			return;
		}
		TimeSearchitem = mSystem.currentTimeMillis();
		Service.gI().pickItem(Char.myCharz().itemFocus.itemMapID);
	}

	public static void SelectMobToAttack()
	{
		int num = 9999;
		Mob mob = null;
		if (listmob.Count != 0)
		{
			foreach (int item in listmob)
			{
				Mob mob2 = (Mob)GameScr.vMob.elementAt(item);
				if (mob2.status != 0 && mob2.status != 1 && mob2.hp > 0 && !mob2.isMobMe && mob2.x < num && ((!casieuquai) ? (!mob2.checkIsBoss()) : (mob2.x < num)))
				{
					num = mob2.x;
					mob = mob2;
				}
			}
		}
		else
		{
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob3 = (Mob)GameScr.vMob.elementAt(i);
				if (mob3.status != 0 && mob3.status != 1 && mob3.hp > 0 && !mob3.isMobMe && mob3.x < num && ((!casieuquai) ? (!mob3.checkIsBoss()) : (mob3.x < num)))
				{
					num = mob3.x;
					mob = mob3;
				}
			}
		}
		if (mob == null)
			return;
		if (tdlt1)
		{
			Char.myCharz().cx = mob.x;
			Char.myCharz().cy = mob.y;
			Char.myCharz().mobFocus = mob;
			Service.gI().charMove();
			return;
		}
		if (Math.abs(Char.myCharz().cx - mob.x) <= 170)
		{
			TeleportTo(mob.x, mob.y);
			Service.gI().charMove();
		}
		Char.myCharz().mobFocus = mob;
	}

	public static void TeleportTo(int x, int y)
	{
		Char.myCharz().cx = x;
		Char.myCharz().cy = y;
		Service.gI().charMove();
		Char.myCharz().cy = y + 1;
		Service.gI().charMove();
		Char.myCharz().cy = y;
		Service.gI().charMove();
	}

	public static void AutoT77()
	{
		if (TileMap.mapID != 111 || Char.myCharz().meDead)
			return;
		if (Char.myCharz().cFlag != 8)
			Service.gI().getFlag(1, 8);
		Char @char = (Char)GameScr.vCharInMap.elementAt(0);
		if (Char.myCharz().cMP <= Char.myCharz().cMPFull * 2 / 100 && !Char.myCharz().doUsePotion())
		{
			TeleportTo(@char.cx, @char.cy);
			return;
		}
		if (deoratnt77)
		{
			TeleportTo(@char.cx, @char.cy);
			return;
		}
		if (!firtMove)
		{
			firtMove = true;
			TeleportTo(762, 336);
		}
		if (@char.cTypePk != 5)
			return;
		if (@char.cy == 336)
		{
			if (t77dungim)
				isautofiret77 = true;
			else
				isautofiret77 = false;
		}
		Char.myCharz().charFocus = @char;
		if (Math.abs(Char.myCharz().cx - @char.cx) < 30)
		{
			TeleportTo(Char.myCharz().cx - 50, Char.myCharz().cy);
			xT77 = ((Char)GameScr.vCharInMap.elementAt(0)).cx;
		}
	}

	public static void AKToT77()
	{
		if (mSystem.currentTimeMillis() - timeAk > ((TimeAkTime == 0L) ? (Char.myCharz().myskill.coolDown + 250) : TimeAkTime))
		{
			if (Char.myCharz().charFocus != null && Char.myCharz().charFocus.cy == 336)
				SendAttackToCharFocus();
			timeAk = mSystem.currentTimeMillis();
		}
	}

	public static void BugT77DungIm()
	{
		Char @char = (Char)GameScr.vCharInMap.elementAt(0);
		if (@char.cTypePk != 5)
			return;
		if (!cccc)
		{
			cccc = true;
			xT77 = @char.cx;
		}
		else if (cccc2 <= 5)
		{
			if (mSystem.currentTimeMillis() - timecheckt77 > 500L)
			{
				timecheckt77 = mSystem.currentTimeMillis();
				cccc2++;
			}
		}
		else if (xT77 == @char.cx && @char.cy == 336)
		{
			t77dungim = true;
			cccc = false;
			cccc2 = 0;
		}
		else
		{
			cccc = false;
			cccc2 = 0;
			t77dungim = false;
		}
	}

	public static void CheckTn(int typeCheck)
	{
		if (t77dungim)
		{
			if (typeCheck == 1)
			{
				deoratnt77 = false;
				timeCheckTn = mSystem.currentTimeMillis();
			}
			else if (mSystem.currentTimeMillis() - timeCheckTn > 6000L && timeCheckTn != 0L)
			{
				deoratnt77 = true;
			}
		}
		else
		{
			deoratnt77 = false;
			timeCheckTn = mSystem.currentTimeMillis();
		}
	}

	public static void GotoDongNamKarin()
	{
		if (TileMap.mapID == 24)
			ChangeMap(0);
		if (TileMap.mapID == 23 || TileMap.mapID == 21 || TileMap.mapID == 22)
		{
			Service.gI().confirmMenu(12, 0);
			Service.gI().openMenu(4);
			Service.gI().menu(4, 0, 0);
			Service.gI().pickItem(-1);
			ChangeMap(0);
		}
		if (TileMap.mapID == 14)
			ChangeMap(0);
		if (TileMap.mapID == 15)
			ChangeMap(1);
		if (TileMap.mapID == 16)
			ChangeMap(2);
		if (TileMap.mapID == 26)
		{
			Service.gI().openMenu(12);
			Service.gI().confirmMenu(12, 0);
			return;
		}
		if (TileMap.mapID == 25)
		{
			Service.gI().openMenu(11);
			Service.gI().confirmMenu(11, 0);
			return;
		}
		if (TileMap.mapID == 2)
			ChangeMap(0);
		if (TileMap.mapID == 1)
			ChangeMap(2);
		if (TileMap.mapID == 47)
			ChangeMap(2);
		if (TileMap.mapID == 0)
			ChangeMap(0);
		if (TileMap.mapID == 2)
			ChangeMap(0);
		if (TileMap.mapID == 9)
			ChangeMap(2);
		if (TileMap.mapID == 8)
		{
			if (Char.myCharz().cx < 600)
			{
				Char.myCharz().currentMovePoint = new MovePoint(610, 288);
				return;
			}
			ChangeMap(1);
		}
		if (TileMap.mapID == 7)
			ChangeMap(0);
	}

	public static void ChangeMap(int index)
	{
		switch (index)
		{
		case 0:
			((Waypoint)TileMap.vGo.elementAt(0)).popup.doClick(1);
			break;
		case 1:
			((Waypoint)TileMap.vGo.elementAt(1)).popup.doClick(1);
			break;
		case 2:
			((Waypoint)TileMap.vGo.elementAt(2)).popup.doClick(1);
			break;
		case 3:
			((Waypoint)TileMap.vGo.elementAt(3)).popup.doClick(1);
			break;
		}
	}

	public static void AutoChat()
	{
		if (atc && GameCanvas.gameTick % 100 == 0)
			try
			{
				string text = File.ReadAllText("Data\\ndchat.txt");
				Service.gI().chat(text);
			}
			catch
			{
			}
	}

	public static void AutoCTG()
	{
		if (mSystem.currentTimeMillis() - timeCtg > TimeAutoCtg)
			try
			{
				string text = File.ReadAllText("Data\\ndctg.txt");
				timeCtg = mSystem.currentTimeMillis();
				Service.gI().chatGlobal(text + " " + currCtg);
				currCtg++;
			}
			catch
			{
			}
	}

	public static void AutoDoanhTrai()
	{
		if (isAutoDoanhTrai && GameCanvas.gameTick % 6 == 0 && TileMap.mapID == 27)
		{
			Service.gI().openMenu(25);
			Service.gI().confirmMenu(25, 0);
		}
		if (TileMap.mapID != 27 && isAutoDoanhTrai)
		{
			GameScr.info1.addInfo("VUi lòng đứng ở map bambo", 0);
			isAutoDoanhTrai = false;
		}
	}

	public static void AutoGiapXen()
	{
		AutoBoHuyet();
		AutoCuongNo();
		if (mSystem.currentTimeMillis() - timeusegx > 630000L && isautogx)
		{
			timeusegx = mSystem.currentTimeMillis();
			AnGiapXen();
		}
	}

	public static void AutoCuongNo()
	{
		if (mSystem.currentTimeMillis() - timeusecn > 630000L && isautocn)
		{
			timeusecn = mSystem.currentTimeMillis();
			AnCuongNo();
		}
	}

	public static void AutoBoHuyet()
	{
		if (mSystem.currentTimeMillis() - timeusebh > 630000L && isautobh)
		{
			timeusebh = mSystem.currentTimeMillis();
			AnBoHuyet();
		}
	}
}
