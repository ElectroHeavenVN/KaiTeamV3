using System;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using UnityEngine;

public class Controller : IMessageHandler
{
	protected static Controller me;

	protected static Controller me2;

	public Message messWait;

	public static bool isLoadingData;

	public static bool isConnectOK;

	public static bool isConnectionFail;

	public static bool isDisconnected;

	public static bool isMain;

	private float demCount;

	private int move;

	private int total;

	public static bool isStopReadMessage;

	public static Controller gI()
	{
		if (me == null)
			me = new Controller();
		return me;
	}

	public static Controller gI2()
	{
		if (me2 == null)
			me2 = new Controller();
		return me2;
	}

	public void onConnectOK(bool isMain1)
	{
		isMain = isMain1;
		mSystem.onConnectOK();
	}

	public void onConnectionFail(bool isMain1)
	{
		isMain = isMain1;
		mSystem.onConnectionFail();
	}

	public void onDisconnected(bool isMain1)
	{
		isMain = isMain1;
		mSystem.onDisconnected();
	}

	public void requestItemPlayer(Message msg)
	{
		try
		{
			byte num = msg.reader().readUnsignedByte();
			Item item = GameScr.currentCharViewInfo.arrItemBody[num];
			item.saleCoinLock = msg.reader().readInt();
			item.sys = msg.reader().readByte();
			item.options = new MyVector();
			try
			{
				while (true)
				{
					item.options.addElement(new ItemOption(msg.reader().readByte(), msg.reader().readShort()));
				}
			}
			catch (Exception ex)
			{
				Cout.println("Loi tairequestItemPlayer 1" + ex.ToString());
			}
		}
		catch (Exception ex2)
		{
			Cout.println("Loi tairequestItemPlayer 2" + ex2.ToString());
		}
	}

	public void onMessage(Message msg)
	{
		GameCanvas.debugSession.removeAllElements();
		GameCanvas.debug("SA1", 2);
		try
		{
			Char @char = null;
			Mob mob = null;
			MyVector myVector = new MyVector();
			int num = 0;
			Controller2.readMessage(msg);
			switch (msg.command)
			{
			case -99:
				InfoDlg.hide();
				if (msg.reader().readByte() == 0)
				{
					GameCanvas.panel.vEnemy.removeAllElements();
					int num146 = msg.reader().readUnsignedByte();
					for (int num147 = 0; num147 < num146; num147++)
					{
						Char char11 = new Char();
						char11.charID = msg.reader().readInt();
						char11.head = msg.reader().readShort();
						char11.body = msg.reader().readShort();
						char11.leg = msg.reader().readShort();
						char11.bag = msg.reader().readShort();
						char11.cName = msg.reader().readUTF();
						InfoItem infoItem4 = new InfoItem(msg.reader().readUTF());
						bool flag9 = msg.reader().readBoolean();
						infoItem4.charInfo = char11;
						infoItem4.isOnline = flag9;
						Res.outz("isonline = " + flag9);
						GameCanvas.panel.vEnemy.addElement(infoItem4);
					}
					GameCanvas.panel.setTypeEnemy();
					GameCanvas.panel.show();
				}
				break;
			case -98:
			{
				sbyte b16 = msg.reader().readByte();
				GameCanvas.menu.showMenu = false;
				if (b16 == 0)
					GameCanvas.startYesNoDlg(msg.reader().readUTF(), new Command(mResources.YES, GameCanvas.instance, 888397, msg.reader().readUTF()), new Command(mResources.NO, GameCanvas.instance, 888396, null));
				break;
			}
			case -97:
				Char.myCharz().cNangdong = msg.reader().readInt();
				break;
			case -96:
			{
				sbyte typeTop = msg.reader().readByte();
				GameCanvas.panel.vTop.removeAllElements();
				string topName = msg.reader().readUTF();
				sbyte b11 = msg.reader().readByte();
				for (int num32 = 0; num32 < b11; num32++)
				{
					int rank = msg.reader().readInt();
					int pId = msg.reader().readInt();
					short headID = msg.reader().readShort();
					short body = msg.reader().readShort();
					short leg = msg.reader().readShort();
					string name = msg.reader().readUTF();
					string info = msg.reader().readUTF();
					TopInfo topInfo = new TopInfo();
					topInfo.rank = rank;
					topInfo.headID = headID;
					topInfo.body = body;
					topInfo.leg = leg;
					topInfo.name = name;
					topInfo.info = info;
					topInfo.info2 = msg.reader().readUTF();
					topInfo.pId = pId;
					GameCanvas.panel.vTop.addElement(topInfo);
				}
				GameCanvas.panel.topName = topName;
				GameCanvas.panel.setTypeTop(typeTop);
				GameCanvas.panel.show();
				break;
			}
			case -95:
			{
				sbyte b9 = msg.reader().readByte();
				Res.outz("type= " + b9);
				if (b9 == 0)
				{
					int num18 = msg.reader().readInt();
					short templateId = msg.reader().readShort();
					int num19 = msg.readInt3Byte();
					SoundMn.gI().explode_1();
					if (num18 == Char.myCharz().charID)
					{
						Char.myCharz().mobMe = new Mob(num18, false, false, false, false, false, templateId, 1, num19, 0, num19, (short)(Char.myCharz().cx + ((Char.myCharz().cdir != 1) ? (-40) : 40)), (short)Char.myCharz().cy, 4, 0);
						Char.myCharz().mobMe.isMobMe = true;
						EffecMn.addEff(new Effect(18, Char.myCharz().mobMe.x, Char.myCharz().mobMe.y, 2, 10, -1));
						Char.myCharz().tMobMeBorn = 30;
						GameScr.vMob.addElement(Char.myCharz().mobMe);
					}
					else
					{
						@char = GameScr.findCharInMap(num18);
						if (@char != null)
						{
							Mob mob2 = new Mob(num18, false, false, false, false, false, templateId, 1, num19, 0, num19, (short)@char.cx, (short)@char.cy, 4, 0);
							mob2.isMobMe = true;
							@char.mobMe = mob2;
							GameScr.vMob.addElement(@char.mobMe);
						}
						else if (GameScr.findMobInMap(num18) == null)
						{
							Mob mob3 = new Mob(num18, false, false, false, false, false, templateId, 1, num19, 0, num19, -100, -100, 4, 0);
							mob3.isMobMe = true;
							GameScr.vMob.addElement(mob3);
						}
					}
				}
				if (b9 == 1)
				{
					int num20 = msg.reader().readInt();
					int mobId = msg.reader().readByte();
					Res.outz("mod attack id= " + num20);
					if (num20 == Char.myCharz().charID)
					{
						if (GameScr.findMobInMap(mobId) != null)
							Char.myCharz().mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
					}
					else
					{
						@char = GameScr.findCharInMap(num20);
						if (@char != null && GameScr.findMobInMap(mobId) != null)
							@char.mobMe.attackOtherMob(GameScr.findMobInMap(mobId));
					}
				}
				if (b9 == 2)
				{
					int num21 = msg.reader().readInt();
					int num22 = msg.reader().readInt();
					int num23 = msg.readInt3Byte();
					int cHPNew = msg.readInt3Byte();
					if (num21 == Char.myCharz().charID)
					{
						Res.outz("mob dame= " + num23);
						@char = GameScr.findCharInMap(num22);
						if (@char != null)
						{
							@char.cHPNew = cHPNew;
							if (Char.myCharz().mobMe.isBusyAttackSomeOne)
								@char.doInjure(num23, 0, false, true);
							else
							{
								Char.myCharz().mobMe.dame = num23;
								Char.myCharz().mobMe.setAttack(@char);
							}
						}
					}
					else
					{
						mob = GameScr.findMobInMap(num21);
						if (mob != null)
						{
							if (num22 == Char.myCharz().charID)
							{
								Char.myCharz().cHPNew = cHPNew;
								if (mob.isBusyAttackSomeOne)
									Char.myCharz().doInjure(num23, 0, false, true);
								else
								{
									mob.dame = num23;
									mob.setAttack(Char.myCharz());
								}
							}
							else
							{
								@char = GameScr.findCharInMap(num22);
								if (@char != null)
								{
									@char.cHPNew = cHPNew;
									if (mob.isBusyAttackSomeOne)
										@char.doInjure(num23, 0, false, true);
									else
									{
										mob.dame = num23;
										mob.setAttack(@char);
									}
								}
							}
						}
					}
				}
				if (b9 == 3)
				{
					int num24 = msg.reader().readInt();
					int mobId2 = msg.reader().readInt();
					int hp = msg.readInt3Byte();
					int num25 = msg.readInt3Byte();
					@char = null;
					@char = ((Char.myCharz().charID != num24) ? GameScr.findCharInMap(num24) : Char.myCharz());
					if (@char != null)
					{
						mob = GameScr.findMobInMap(mobId2);
						if (@char.mobMe != null)
							@char.mobMe.attackOtherMob(mob);
						if (mob != null)
						{
							mob.hp = hp;
							if (num25 == 0)
							{
								mob.x = mob.xFirst;
								mob.y = mob.yFirst;
								GameScr.startFlyText(mResources.miss, mob.x, mob.y - mob.h, 0, -2, mFont.MISS);
							}
							else
								GameScr.startFlyText("-" + num25, mob.x, mob.y - mob.h, 0, -2, mFont.ORANGE);
						}
					}
				}
				if (b9 == 4)
					;
				if (b9 == 5)
				{
					int num26 = msg.reader().readInt();
					sbyte b10 = msg.reader().readByte();
					int mobId3 = msg.reader().readInt();
					int num27 = msg.readInt3Byte();
					int hp2 = msg.readInt3Byte();
					@char = null;
					@char = ((num26 != Char.myCharz().charID) ? GameScr.findCharInMap(num26) : Char.myCharz());
					if (@char == null)
						return;
					if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
						@char.setSkillPaint(GameScr.sks[b10], 0);
					else
						@char.setSkillPaint(GameScr.sks[b10], 1);
					Mob mob4 = GameScr.findMobInMap(mobId3);
					if (@char.cx <= mob4.x)
						@char.cdir = 1;
					else
						@char.cdir = -1;
					@char.mobFocus = mob4;
					mob4.hp = hp2;
					GameCanvas.debug("SA83v2", 2);
					if (num27 == 0)
					{
						mob4.x = mob4.xFirst;
						mob4.y = mob4.yFirst;
						GameScr.startFlyText(mResources.miss, mob4.x, mob4.y - mob4.h, 0, -2, mFont.MISS);
					}
					else
						GameScr.startFlyText("-" + num27, mob4.x, mob4.y - mob4.h, 0, -2, mFont.ORANGE);
				}
				if (b9 == 6)
				{
					int num28 = msg.reader().readInt();
					if (num28 == Char.myCharz().charID)
						Char.myCharz().mobMe.startDie();
					else
						GameScr.findCharInMap(num28)?.mobMe.startDie();
				}
				if (b9 != 7)
					break;
				int num29 = msg.reader().readInt();
				if (num29 == Char.myCharz().charID)
				{
					Char.myCharz().mobMe = null;
					for (int num30 = 0; num30 < GameScr.vMob.size(); num30++)
					{
						if (((Mob)GameScr.vMob.elementAt(num30)).mobId == num29)
							GameScr.vMob.removeElementAt(num30);
					}
					break;
				}
				@char = GameScr.findCharInMap(num29);
				for (int num31 = 0; num31 < GameScr.vMob.size(); num31++)
				{
					if (((Mob)GameScr.vMob.elementAt(num31)).mobId == num29)
						GameScr.vMob.removeElementAt(num31);
				}
				if (@char != null)
					@char.mobMe = null;
				break;
			}
			case -94:
				while (msg.reader().available() > 0)
				{
					short num12 = msg.reader().readShort();
					int num13 = msg.reader().readInt();
					for (int num14 = 0; num14 < Char.myCharz().vSkill.size(); num14++)
					{
						Skill skill = (Skill)Char.myCharz().vSkill.elementAt(num14);
						if (skill != null && skill.skillId == num12)
						{
							if (num13 < skill.coolDown)
								skill.lastTimeUseThisSkill = mSystem.currentTimeMillis() - (skill.coolDown - num13);
							Res.outz("1 chieu id= " + skill.template.id + " cooldown= " + num13 + "curr cool down= " + skill.coolDown);
						}
					}
				}
				break;
			case -93:
			{
				short num149 = msg.reader().readShort();
				BgItem.newSmallVersion = new sbyte[num149];
				for (int num150 = 0; num150 < num149; num150++)
				{
					BgItem.newSmallVersion[num150] = msg.reader().readByte();
				}
				break;
			}
			case -92:
				Main.typeClient = msg.reader().readByte();
				Rms.clearAll();
				Rms.saveRMSInt("clienttype", Main.typeClient);
				Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
				GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
				break;
			case -91:
			{
				sbyte b38 = msg.reader().readByte();
				GameCanvas.panel.mapNames = new string[b38];
				GameCanvas.panel.planetNames = new string[b38];
				for (int num103 = 0; num103 < b38; num103++)
				{
					GameCanvas.panel.mapNames[num103] = msg.reader().readUTF();
					GameCanvas.panel.planetNames[num103] = msg.reader().readUTF();
				}
				GameCanvas.panel.setTypeMapTrans();
				GameCanvas.panel.show();
				break;
			}
			case -90:
			{
				sbyte b12 = msg.reader().readByte();
				Res.outz("type = " + b12);
				int num37 = msg.reader().readInt();
				if (b12 != -1)
				{
					short num38 = msg.reader().readShort();
					short num39 = msg.reader().readShort();
					short num40 = msg.reader().readShort();
					sbyte b13 = msg.reader().readByte();
					Res.outz("is Monkey = " + b13);
					if (Char.myCharz().charID == num37)
					{
						Char.myCharz().isMask = true;
						Char.myCharz().isMonkey = b13;
						if (Char.myCharz().isMonkey != 0)
						{
							Char.myCharz().isWaitMonkey = false;
							Char.myCharz().isLockMove = false;
						}
					}
					else if (GameScr.findCharInMap(num37) != null)
					{
						GameScr.findCharInMap(num37).isMask = true;
						GameScr.findCharInMap(num37).isMonkey = b13;
					}
					if (num38 != -1)
					{
						if (num37 == Char.myCharz().charID)
							Char.myCharz().head = num38;
						else if (GameScr.findCharInMap(num37) != null)
						{
							GameScr.findCharInMap(num37).head = num38;
						}
					}
					if (num39 != -1)
					{
						if (num37 == Char.myCharz().charID)
							Char.myCharz().body = num39;
						else if (GameScr.findCharInMap(num37) != null)
						{
							GameScr.findCharInMap(num37).body = num39;
						}
					}
					if (num40 != -1)
					{
						if (num37 == Char.myCharz().charID)
							Char.myCharz().leg = num40;
						else if (GameScr.findCharInMap(num37) != null)
						{
							GameScr.findCharInMap(num37).leg = num40;
						}
					}
				}
				if (b12 == -1)
				{
					if (Char.myCharz().charID == num37)
					{
						Char.myCharz().isMask = false;
						Char.myCharz().isMonkey = 0;
					}
					else if (GameScr.findCharInMap(num37) != null)
					{
						GameScr.findCharInMap(num37).isMask = false;
						GameScr.findCharInMap(num37).isMonkey = 0;
					}
				}
				break;
			}
			case -88:
				GameCanvas.endDlg();
				GameCanvas.serverScreen.switchToMe();
				break;
			case -87:
			{
				Res.outz("GET UPDATE_DATA " + msg.reader().available() + " bytes");
				msg.reader().mark(100000);
				createData(msg.reader(), true);
				msg.reader().reset();
				sbyte[] data = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref data);
				Rms.saveRMS("NRdataVersion", new sbyte[1] { GameScr.vcData });
				LoginScr.isUpdateData = false;
				if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
				{
					Res.outz(GameScr.vsData + "," + GameScr.vsMap + "," + GameScr.vsSkill + "," + GameScr.vsItem);
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
					return;
				}
				break;
			}
			case -86:
			{
				sbyte b21 = msg.reader().readByte();
				Res.outz("server gui ve giao dich action = " + b21);
				if (b21 == 0)
				{
					int playerID = msg.reader().readInt();
					GameScr.gI().giaodich(playerID);
				}
				if (b21 == 1)
				{
					int num66 = msg.reader().readInt();
					Char char4 = GameScr.findCharInMap(num66);
					if (char4 == null)
						return;
					GameCanvas.panel.setTypeGiaoDich(char4);
					GameCanvas.panel.show();
					Service.gI().getPlayerMenu(num66);
				}
				if (b21 == 2)
				{
					sbyte b22 = msg.reader().readByte();
					for (int num67 = 0; num67 < GameCanvas.panel.vMyGD.size(); num67++)
					{
						Item item = (Item)GameCanvas.panel.vMyGD.elementAt(num67);
						if (item.indexUI == b22)
						{
							GameCanvas.panel.vMyGD.removeElement(item);
							break;
						}
					}
				}
				if (b21 == 5)
					;
				if (b21 == 6)
				{
					GameCanvas.panel.isFriendLock = true;
					if (GameCanvas.panel2 != null)
						GameCanvas.panel2.isFriendLock = true;
					GameCanvas.panel.vFriendGD.removeAllElements();
					if (GameCanvas.panel2 != null)
						GameCanvas.panel2.vFriendGD.removeAllElements();
					int friendMoneyGD = msg.reader().readInt();
					sbyte b23 = msg.reader().readByte();
					Res.outz("item size = " + b23);
					for (int num68 = 0; num68 < b23; num68++)
					{
						Item item2 = new Item();
						item2.template = ItemTemplates.get(msg.reader().readShort());
						item2.quantity = msg.reader().readByte();
						int num69 = msg.reader().readUnsignedByte();
						if (num69 != 0)
						{
							item2.itemOption = new ItemOption[num69];
							for (int num70 = 0; num70 < item2.itemOption.Length; num70++)
							{
								int optionTemplateId2 = msg.reader().readUnsignedByte();
								ushort param2 = msg.reader().readUnsignedShort();
								item2.itemOption[num70] = new ItemOption(optionTemplateId2, param2);
								item2.compare = GameCanvas.panel.getCompare(item2);
							}
						}
						if (GameCanvas.panel2 != null)
							GameCanvas.panel2.vFriendGD.addElement(item2);
						else
							GameCanvas.panel.vFriendGD.addElement(item2);
					}
					if (GameCanvas.panel2 != null)
					{
						GameCanvas.panel2.setTabGiaoDich(false);
						GameCanvas.panel2.friendMoneyGD = friendMoneyGD;
					}
					else
					{
						GameCanvas.panel.friendMoneyGD = friendMoneyGD;
						if (GameCanvas.panel.currentTabIndex == 2)
							GameCanvas.panel.setTabGiaoDich(false);
					}
				}
				if (b21 == 7)
				{
					InfoDlg.hide();
					if (GameCanvas.panel.isShow)
						GameCanvas.panel.hide();
				}
				break;
			}
			case -85:
			{
				Res.outz("CAP CHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
				sbyte b24 = msg.reader().readByte();
				if (b24 == 0)
				{
					int num71 = msg.reader().readUnsignedShort();
					Res.outz("lent =" + num71);
					sbyte[] data3 = new sbyte[num71];
					msg.reader().read(ref data3, 0, num71);
					GameScr.imgCapcha = Image.createImage(data3, 0, num71);
					GameScr.gI().keyInput = "-----";
					GameScr.gI().strCapcha = msg.reader().readUTF();
					GameScr.gI().keyCapcha = new int[GameScr.gI().strCapcha.Length];
					GameScr.gI().mobCapcha = new Mob();
					GameScr.gI().right = null;
				}
				if (b24 == 1)
					MobCapcha.isAttack = true;
				if (b24 == 2)
				{
					MobCapcha.explode = true;
					GameScr.gI().right = GameScr.gI().cmdFocus;
				}
				break;
			}
			case -84:
			{
				int index = msg.reader().readUnsignedByte();
				Mob mob6 = null;
				try
				{
					mob6 = (Mob)GameScr.vMob.elementAt(index);
				}
				catch (Exception)
				{
				}
				if (mob6 != null)
					mob6.maxHp = msg.reader().readInt();
				break;
			}
			case -83:
			{
				sbyte b14 = msg.reader().readByte();
				if (b14 == 0)
				{
					int num41 = msg.reader().readShort();
					int bgRID = msg.reader().readShort();
					int num42 = msg.reader().readUnsignedByte();
					int num43 = msg.reader().readInt();
					msg.reader().readUTF();
					int num44 = msg.reader().readShort();
					int num45 = msg.reader().readShort();
					if (msg.reader().readByte() == 1)
						GameScr.gI().isRongNamek = true;
					else
						GameScr.gI().isRongNamek = false;
					GameScr.gI().xR = num44;
					GameScr.gI().yR = num45;
					Res.outz("xR= " + num44 + " yR= " + num45 + " +++++++++++++++++++++++++++++++++++++++");
					if (Char.myCharz().charID == num43)
					{
						GameCanvas.panel.hideNow();
						GameScr.gI().activeRongThanEff(true);
					}
					else if (TileMap.mapID == num41 && TileMap.zoneID == num42)
					{
						GameScr.gI().activeRongThanEff(false);
					}
					else if (mGraphics.zoomLevel > 1)
					{
						GameScr.gI().doiMauTroi();
					}
					GameScr.gI().mapRID = num41;
					GameScr.gI().bgRID = bgRID;
					GameScr.gI().zoneRID = num42;
				}
				if (b14 == 1)
				{
					Res.outz("map RID = " + GameScr.gI().mapRID + " zone RID= " + GameScr.gI().zoneRID);
					Res.outz("map ID = " + TileMap.mapID + " zone ID= " + TileMap.zoneID);
					if (TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID)
						GameScr.gI().hideRongThanEff();
					else
					{
						GameScr.gI().isRongThanXuatHien = false;
						if (GameScr.gI().isRongNamek)
							GameScr.gI().isRongNamek = false;
					}
				}
				if (b14 != 2)
					;
				break;
			}
			case -82:
			{
				sbyte b3 = msg.reader().readByte();
				TileMap.tileIndex = new int[b3][][];
				TileMap.tileType = new int[b3][];
				for (int k = 0; k < b3; k++)
				{
					sbyte b4 = msg.reader().readByte();
					TileMap.tileType[k] = new int[b4];
					TileMap.tileIndex[k] = new int[b4][];
					for (int l = 0; l < b4; l++)
					{
						TileMap.tileType[k][l] = msg.reader().readInt();
						sbyte b5 = msg.reader().readByte();
						TileMap.tileIndex[k][l] = new int[b5];
						for (int m = 0; m < b5; m++)
						{
							TileMap.tileIndex[k][l][m] = msg.reader().readByte();
						}
					}
				}
				break;
			}
			case -81:
			{
				sbyte b58 = msg.reader().readByte();
				if (b58 == 0)
				{
					string src = msg.reader().readUTF();
					string src2 = msg.reader().readUTF();
					GameCanvas.panel.setTypeCombine();
					GameCanvas.panel.combineInfo = mFont.tahoma_7b_blue.splitFontArray(src, Panel.WIDTH_PANEL);
					GameCanvas.panel.combineTopInfo = mFont.tahoma_7.splitFontArray(src2, Panel.WIDTH_PANEL);
					GameCanvas.panel.show();
				}
				if (b58 == 1)
				{
					GameCanvas.panel.vItemCombine.removeAllElements();
					sbyte b59 = msg.reader().readByte();
					for (int num153 = 0; num153 < b59; num153++)
					{
						sbyte b60 = msg.reader().readByte();
						for (int num154 = 0; num154 < Char.myCharz().arrItemBag.Length; num154++)
						{
							Item item3 = Char.myCharz().arrItemBag[num154];
							if (item3 != null && item3.indexUI == b60)
							{
								item3.isSelect = true;
								GameCanvas.panel.vItemCombine.addElement(item3);
							}
						}
					}
					if (GameCanvas.panel.isShow)
						GameCanvas.panel.setTabCombine();
				}
				if (b58 > 1)
				{
					int num155 = 21;
					for (int num156 = 0; num156 < GameScr.vNpc.size(); num156++)
					{
						Npc npc6 = (Npc)GameScr.vNpc.elementAt(num156);
						if (npc6.template.npcTemplateId == num155)
						{
							GameCanvas.panel.xS = npc6.cx - GameScr.cmx;
							GameCanvas.panel.yS = npc6.cy - GameScr.cmy;
							GameCanvas.panel.idNPC = num155;
							break;
						}
					}
				}
				if (b58 == 2)
				{
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(0);
				}
				if (b58 == 3)
				{
					GameCanvas.panel.combineSuccess = 1;
					GameCanvas.panel.setCombineEff(0);
				}
				if (b58 == 4)
				{
					short iconID = msg.reader().readShort();
					GameCanvas.panel.iconID3 = iconID;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(1);
				}
				if (b58 == 5)
				{
					short iconID2 = msg.reader().readShort();
					GameCanvas.panel.iconID3 = iconID2;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(2);
				}
				if (b58 == 6)
				{
					short iconID3 = msg.reader().readShort();
					short iconID4 = msg.reader().readShort();
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(3);
					GameCanvas.panel.iconID1 = iconID3;
					GameCanvas.panel.iconID3 = iconID4;
				}
				break;
			}
			case -80:
			{
				sbyte b17 = msg.reader().readByte();
				InfoDlg.hide();
				if (b17 == 0)
				{
					GameCanvas.panel.vFriend.removeAllElements();
					int num55 = msg.reader().readUnsignedByte();
					for (int num56 = 0; num56 < num55; num56++)
					{
						Char char3 = new Char();
						char3.charID = msg.reader().readInt();
						char3.head = msg.reader().readShort();
						char3.body = msg.reader().readShort();
						char3.leg = msg.reader().readShort();
						char3.bag = msg.reader().readUnsignedByte();
						char3.cName = msg.reader().readUTF();
						bool isOnline = msg.reader().readBoolean();
						InfoItem infoItem = new InfoItem(mResources.power + ": " + msg.reader().readUTF());
						infoItem.charInfo = char3;
						infoItem.isOnline = isOnline;
						GameCanvas.panel.vFriend.addElement(infoItem);
					}
					GameCanvas.panel.setTypeFriend();
					GameCanvas.panel.show();
				}
				if (b17 == 3)
				{
					MyVector vFriend = GameCanvas.panel.vFriend;
					int num57 = msg.reader().readInt();
					Res.outz("online offline id=" + num57);
					for (int num58 = 0; num58 < vFriend.size(); num58++)
					{
						InfoItem infoItem2 = (InfoItem)vFriend.elementAt(num58);
						if (infoItem2.charInfo != null && infoItem2.charInfo.charID == num57)
						{
							Res.outz("online= " + infoItem2.isOnline);
							infoItem2.isOnline = msg.reader().readBoolean();
							break;
						}
					}
				}
				if (b17 != 2)
					break;
				MyVector vFriend2 = GameCanvas.panel.vFriend;
				int num59 = msg.reader().readInt();
				for (int num60 = 0; num60 < vFriend2.size(); num60++)
				{
					InfoItem infoItem3 = (InfoItem)vFriend2.elementAt(num60);
					if (infoItem3.charInfo != null && infoItem3.charInfo.charID == num59)
					{
						vFriend2.removeElement(infoItem3);
						break;
					}
				}
				if (GameCanvas.panel.isShow)
					GameCanvas.panel.setTabFriend();
				break;
			}
			case -79:
			{
				InfoDlg.hide();
				msg.reader().readInt();
				Char charMenu = GameCanvas.panel.charMenu;
				if (charMenu == null)
					return;
				charMenu.cPower = msg.reader().readLong();
				charMenu.currStrLevel = msg.reader().readUTF();
				break;
			}
			case -77:
			{
				short num46 = msg.reader().readShort();
				SmallImage.newSmallVersion = new sbyte[num46];
				SmallImage.maxSmall = num46;
				SmallImage.imgNew = new Small[num46];
				for (int num47 = 0; num47 < num46; num47++)
				{
					SmallImage.newSmallVersion[num47] = msg.reader().readByte();
				}
				break;
			}
			case -76:
			{
				sbyte b52 = msg.reader().readByte();
				if (b52 == 0)
				{
					sbyte b53 = msg.reader().readByte();
					if (b53 <= 0)
						return;
					Char.myCharz().arrArchive = new Archivement[b53];
					for (int num130 = 0; num130 < b53; num130++)
					{
						Char.myCharz().arrArchive[num130] = new Archivement();
						Char.myCharz().arrArchive[num130].info1 = num130 + 1 + ". " + msg.reader().readUTF();
						Char.myCharz().arrArchive[num130].info2 = msg.reader().readUTF();
						Char.myCharz().arrArchive[num130].money = msg.reader().readShort();
						Char.myCharz().arrArchive[num130].isFinish = msg.reader().readBoolean();
						Char.myCharz().arrArchive[num130].isRecieve = msg.reader().readBoolean();
					}
					GameCanvas.panel.setTypeArchivement();
					GameCanvas.panel.show();
				}
				else if (b52 == 1)
				{
					int num131 = msg.reader().readUnsignedByte();
					if (Char.myCharz().arrArchive[num131] != null)
						Char.myCharz().arrArchive[num131].isRecieve = true;
				}
				break;
			}
			case -74:
			{
				if (ServerListScreen.stopDownload)
					return;
				if (!GameCanvas.isGetResourceFromServer())
				{
					Service.gI().getResource(3, null);
					SmallImage.loadBigRMS();
					SplashScr.imgLogo = null;
					if (Rms.loadRMSString("acc") != null || Rms.loadRMSString("userAo" + ServerListScreen.ipSelect) != null)
						LoginScr.isContinueToLogin = true;
					GameCanvas.loginScr = new LoginScr();
					GameCanvas.loginScr.switchToMe();
					return;
				}
				bool flag2 = true;
				sbyte b20 = msg.reader().readByte();
				Res.outz("action = " + b20);
				if (b20 == 0)
				{
					int num62 = msg.reader().readInt();
					string text3 = Rms.loadRMSString("ResVersion");
					int num63 = ((text3 == null || !(text3 != string.Empty)) ? (-1) : int.Parse(text3));
					if (num63 != -1 && num63 == num62)
					{
						Res.outz("login ngay");
						SmallImage.loadBigRMS();
						SplashScr.imgLogo = null;
						ServerListScreen.loadScreen = true;
						if (GameCanvas.currentScreen != GameCanvas.loginScr)
							GameCanvas.serverScreen.switchToMe();
					}
					else
					{
						ServerListScreen.loadScreen = false;
						GameCanvas.serverScreen.show2();
					}
				}
				if (b20 == 1)
				{
					ServerListScreen.strWait = mResources.downloading_data;
					ServerListScreen.nBig = msg.reader().readShort();
					Service.gI().getResource(2, null);
				}
				if (b20 == 2)
					try
					{
						isLoadingData = true;
						GameCanvas.endDlg();
						ServerListScreen.demPercent++;
						ServerListScreen.percent = ServerListScreen.demPercent * 100 / ServerListScreen.nBig;
						string[] array6 = Res.split(msg.reader().readUTF(), "/", 0);
						string filename = "x" + mGraphics.zoomLevel + array6[array6.Length - 1];
						int num64 = msg.reader().readInt();
						sbyte[] data2 = new sbyte[num64];
						msg.reader().read(ref data2, 0, num64);
						Rms.saveRMS(filename, data2);
					}
					catch (Exception)
					{
						GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
					}
				if (b20 == 3 && flag2)
				{
					isLoadingData = false;
					int num65 = msg.reader().readInt();
					Res.outz("last version= " + num65);
					Rms.saveRMSString("ResVersion", num65 + string.Empty);
					Service.gI().getResource(3, null);
					GameCanvas.endDlg();
					SplashScr.imgLogo = null;
					SmallImage.loadBigRMS();
					mSystem.gcc();
					ServerListScreen.bigOk = true;
					ServerListScreen.loadScreen = true;
					GameScr.gI().loadGameScr();
					if (GameCanvas.currentScreen != GameCanvas.loginScr)
						GameCanvas.serverScreen.switchToMe();
				}
				break;
			}
			case -70:
			{
				Res.outz("BIG MESSAGE .......................................");
				GameCanvas.endDlg();
				int avatar = msg.reader().readShort();
				string chat4 = msg.reader().readUTF();
				Npc npc5 = new Npc(-1, 0, 0, 0, 0, 0);
				npc5.avatar = avatar;
				ChatPopup.addBigMessage(chat4, 100000, npc5);
				sbyte b50 = msg.reader().readByte();
				if (b50 == 0)
				{
					ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
					ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 35;
					ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
				}
				if (b50 == 1)
				{
					string p2 = msg.reader().readUTF();
					string caption2 = msg.reader().readUTF();
					ChatPopup.serverChatPopUp.cmdMsg1 = new Command(caption2, ChatPopup.serverChatPopUp, 1000, p2);
					ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 75;
					ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
					ChatPopup.serverChatPopUp.cmdMsg2 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
					ChatPopup.serverChatPopUp.cmdMsg2.x = GameCanvas.w / 2 + 11;
					ChatPopup.serverChatPopUp.cmdMsg2.y = GameCanvas.h - 35;
				}
				break;
			}
			case -69:
				Char.myCharz().cMaxStamina = msg.reader().readShort();
				break;
			case -68:
				Char.myCharz().cStamina = msg.reader().readShort();
				break;
			case -67:
			{
				Res.outz("RECIEVE ICON");
				demCount += 1f;
				int num118 = msg.reader().readInt();
				sbyte[] array10 = null;
				try
				{
					array10 = NinjaUtil.readByteArray(msg);
					Res.outz("request hinh icon = " + num118);
					if (num118 == 3896)
						Res.outz("SIZE CHECK= " + array10.Length);
					SmallImage.imgNew[num118].img = createImage(array10);
				}
				catch (Exception)
				{
					array10 = null;
					SmallImage.imgNew[num118].img = Image.createRGBImage(new int[1], 1, 1, true);
				}
				if (array10 != null && mGraphics.zoomLevel > 1)
					Rms.saveRMS(mGraphics.zoomLevel + "Small" + num118, array10);
				break;
			}
			case -66:
			{
				short id = msg.reader().readShort();
				sbyte[] data4 = NinjaUtil.readByteArray(msg);
				EffectData effDataById = Effect.getEffDataById(id);
				effDataById.readData(data4);
				sbyte[] array7 = NinjaUtil.readByteArray(msg);
				effDataById.img = Image.createImage(array7, 0, array7.Length);
				break;
			}
			case -65:
			{
				Res.outz("TELEPORT ...................................................");
				InfoDlg.hide();
				int num121 = msg.reader().readInt();
				sbyte b47 = msg.reader().readByte();
				if (b47 == 0)
					break;
				if (Char.myCharz().charID == num121)
				{
					isStopReadMessage = true;
					GameScr.lockTick = 500;
					GameScr.gI().center = null;
					if (b47 == 0 || b47 == 1 || b47 == 3)
						Teleport.addTeleport(new Teleport(Char.myCharz().cx, Char.myCharz().cy, Char.myCharz().head, Char.myCharz().cdir, 0, true, (b47 != 1) ? b47 : Char.myCharz().cgender));
					if (b47 == 2)
					{
						GameScr.lockTick = 50;
						Char.myCharz().hide();
					}
				}
				else
				{
					Char char10 = GameScr.findCharInMap(num121);
					if ((b47 == 0 || b47 == 1 || b47 == 3) && char10 != null)
					{
						char10.isUsePlane = true;
						Teleport teleport = new Teleport(char10.cx, char10.cy, char10.head, char10.cdir, 0, false, (b47 != 1) ? b47 : char10.cgender);
						teleport.id = num121;
						Teleport.addTeleport(teleport);
					}
					if (b47 == 2)
						char10.hide();
				}
				break;
			}
			case -64:
			{
				int num120 = msg.reader().readInt();
				int bag = msg.reader().readUnsignedByte();
				if (num120 == Char.myCharz().charID)
					Char.myCharz().bag = bag;
				else if (GameScr.findCharInMap(num120) != null)
				{
					GameScr.findCharInMap(num120).bag = bag;
				}
				break;
			}
			case -63:
			{
				Res.outz("GET BAG");
				int num99 = msg.reader().readUnsignedByte();
				sbyte b37 = msg.reader().readByte();
				ClanImage clanImage3 = new ClanImage();
				clanImage3.ID = num99;
				if (b37 > 0)
				{
					clanImage3.idImage = new short[b37];
					for (int num100 = 0; num100 < b37; num100++)
					{
						clanImage3.idImage[num100] = msg.reader().readShort();
						Res.outz("ID=  " + num99 + " frame= " + clanImage3.idImage[num100]);
					}
					ClanImage.idImages.put(num99 + string.Empty, clanImage3);
				}
				break;
			}
			case -62:
			{
				int num94 = msg.reader().readUnsignedByte();
				sbyte b35 = msg.reader().readByte();
				if (b35 <= 0)
					break;
				ClanImage clanImage2 = ClanImage.getClanImage((sbyte)num94);
				if (clanImage2 == null)
					break;
				clanImage2.idImage = new short[b35];
				for (int num95 = 0; num95 < b35; num95++)
				{
					clanImage2.idImage[num95] = msg.reader().readShort();
					if (clanImage2.idImage[num95] > 0)
						SmallImage.vKeys.addElement(clanImage2.idImage[num95] + string.Empty);
				}
				break;
			}
			case -61:
			{
				int num74 = msg.reader().readInt();
				if (num74 != Char.myCharz().charID)
				{
					if (GameScr.findCharInMap(num74) != null)
					{
						GameScr.findCharInMap(num74).clanID = msg.reader().readInt();
						if (GameScr.findCharInMap(num74).clanID == -2)
							GameScr.findCharInMap(num74).isCopy = true;
					}
				}
				else if (Char.myCharz().clan != null)
				{
					Char.myCharz().clan.ID = msg.reader().readInt();
				}
				break;
			}
			case -60:
			{
				GameCanvas.debug("SA7666", 2);
				int num104 = msg.reader().readInt();
				int num105 = -1;
				if (num104 != Char.myCharz().charID)
				{
					Char char7 = GameScr.findCharInMap(num104);
					if (char7 == null)
						return;
					if (char7.currentMovePoint != null)
					{
						char7.createShadow(char7.cx, char7.cy, 10);
						char7.cx = char7.currentMovePoint.xEnd;
						char7.cy = char7.currentMovePoint.yEnd;
					}
					int num106 = msg.reader().readUnsignedByte();
					Res.outz("player skill ID= " + num106);
					if ((TileMap.tileTypeAtPixel(char7.cx, char7.cy) & 2) == 2)
						char7.setSkillPaint(GameScr.sks[num106], 0);
					else
						char7.setSkillPaint(GameScr.sks[num106], 1);
					sbyte b39 = msg.reader().readByte();
					Res.outz("nAttack = " + b39);
					Char[] array9 = new Char[b39];
					for (num = 0; num < array9.Length; num++)
					{
						num105 = msg.reader().readInt();
						Char char8;
						if (num105 == Char.myCharz().charID)
						{
							char8 = Char.myCharz();
							if (!GameScr.isChangeZone && GameScr.isAutoPlay && GameScr.canAutoPlay && AAAMYs.autodoikhu)
							{
								Service.gI().requestChangeZone(-1, -1);
								GameScr.isChangeZone = true;
							}
						}
						else
							char8 = GameScr.findCharInMap(num105);
						array9[num] = char8;
						if (num == 0)
						{
							if (char7.cx <= char8.cx)
								char7.cdir = 1;
							else
								char7.cdir = -1;
						}
					}
					if (num > 0)
					{
						char7.attChars = new Char[num];
						for (num = 0; num < char7.attChars.Length; num++)
						{
							char7.attChars[num] = array9[num];
						}
						char7.mobFocus = null;
						char7.charFocus = char7.attChars[0];
					}
				}
				else
				{
					msg.reader().readByte();
					msg.reader().readByte();
					num105 = msg.reader().readInt();
				}
				sbyte b40 = msg.reader().readByte();
				Res.outz("isRead continue = " + b40);
				if (b40 != 1)
					break;
				sbyte b41 = msg.reader().readByte();
				Res.outz("type skill = " + b41);
				if (num105 == Char.myCharz().charID)
				{
					bool flag3 = false;
					@char = Char.myCharz();
					int num107 = msg.readInt3Byte();
					Res.outz("dame hit = " + num107);
					@char.isDie = msg.reader().readBoolean();
					if (@char.isDie)
						Char.isLockKey = true;
					Res.outz("isDie=" + @char.isDie + "---------------------------------------");
					flag3 = (@char.isCrit = msg.reader().readBoolean());
					@char.isMob = false;
					num107 = (@char.damHP = num107 + 0);
					if (b41 == 0)
						@char.doInjure(num107, 0, flag3, false);
				}
				else
				{
					@char = GameScr.findCharInMap(num105);
					if (@char == null)
						return;
					bool flag4 = false;
					int num108 = msg.readInt3Byte();
					Res.outz("dame hit= " + num108);
					@char.isDie = msg.reader().readBoolean();
					Res.outz("isDie=" + @char.isDie + "---------------------------------------");
					flag4 = (@char.isCrit = msg.reader().readBoolean());
					@char.isMob = false;
					num108 = (@char.damHP = num108 + 0);
					if (b41 == 0)
						@char.doInjure(num108, 0, flag4, false);
				}
				break;
			}
			case -59:
			{
				sbyte typePK = msg.reader().readByte();
				GameScr.gI().player_vs_player(msg.reader().readInt(), msg.reader().readInt(), msg.reader().readUTF(), typePK);
				break;
			}
			case -57:
			{
				string strInvite = msg.reader().readUTF();
				int clanID = msg.reader().readInt();
				int code = msg.reader().readInt();
				GameScr.gI().clanInvite(strInvite, clanID, code);
				break;
			}
			case -53:
			{
				Res.outz("MY CLAN INFO");
				InfoDlg.hide();
				bool flag = false;
				int num48 = msg.reader().readInt();
				Res.outz("clanId= " + num48);
				if (num48 == -1)
				{
					flag = true;
					Char.myCharz().clan = null;
					ClanMessage.vMessage.removeAllElements();
					if (GameCanvas.panel.member != null)
						GameCanvas.panel.member.removeAllElements();
					if (GameCanvas.panel.myMember != null)
						GameCanvas.panel.myMember.removeAllElements();
					if (GameCanvas.currentScreen == GameScr.gI())
						GameCanvas.panel.setTabClans();
					return;
				}
				GameCanvas.panel.tabIcon = null;
				if (Char.myCharz().clan == null)
					Char.myCharz().clan = new Clan();
				Char.myCharz().clan.ID = num48;
				Char.myCharz().clan.name = msg.reader().readUTF();
				Char.myCharz().clan.slogan = msg.reader().readUTF();
				Char.myCharz().clan.imgID = msg.reader().readUnsignedByte();
				Char.myCharz().clan.powerPoint = msg.reader().readUTF();
				Char.myCharz().clan.leaderName = msg.reader().readUTF();
				Char.myCharz().clan.currMember = msg.reader().readUnsignedByte();
				Char.myCharz().clan.maxMember = msg.reader().readUnsignedByte();
				Char.myCharz().role = msg.reader().readByte();
				Char.myCharz().clan.clanPoint = msg.reader().readInt();
				Char.myCharz().clan.level = msg.reader().readByte();
				GameCanvas.panel.myMember = new MyVector();
				for (int num49 = 0; num49 < Char.myCharz().clan.currMember; num49++)
				{
					Member member = new Member();
					member.ID = msg.reader().readInt();
					member.head = msg.reader().readShort();
					member.leg = msg.reader().readShort();
					member.body = msg.reader().readShort();
					member.name = msg.reader().readUTF();
					member.role = msg.reader().readByte();
					member.powerPoint = msg.reader().readUTF();
					member.donate = msg.reader().readInt();
					member.receive_donate = msg.reader().readInt();
					member.clanPoint = msg.reader().readInt();
					member.curClanPoint = msg.reader().readInt();
					member.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					GameCanvas.panel.myMember.addElement(member);
				}
				int num50 = msg.reader().readUnsignedByte();
				for (int num51 = 0; num51 < num50; num51++)
				{
					readClanMsg(msg, -1);
				}
				if (GameCanvas.panel.isSearchClan || GameCanvas.panel.isViewMember || GameCanvas.panel.isMessage)
					GameCanvas.panel.setTabClans();
				if (flag)
					GameCanvas.panel.setTabClans();
				break;
			}
			case -52:
			{
				sbyte b54 = msg.reader().readByte();
				if (b54 == 0)
				{
					Member member3 = new Member();
					member3.ID = msg.reader().readInt();
					member3.head = msg.reader().readShort();
					member3.leg = msg.reader().readShort();
					member3.body = msg.reader().readShort();
					member3.name = msg.reader().readUTF();
					member3.role = msg.reader().readByte();
					member3.powerPoint = msg.reader().readUTF();
					member3.donate = msg.reader().readInt();
					member3.receive_donate = msg.reader().readInt();
					member3.clanPoint = msg.reader().readInt();
					member3.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					if (GameCanvas.panel.myMember == null)
						GameCanvas.panel.myMember = new MyVector();
					GameCanvas.panel.myMember.addElement(member3);
					GameCanvas.panel.initTabClans();
				}
				if (b54 == 1)
				{
					GameCanvas.panel.myMember.removeElementAt(msg.reader().readByte());
					GameCanvas.panel.currentListLength--;
					GameCanvas.panel.initTabClans();
				}
				if (b54 != 2)
					break;
				Member member4 = new Member();
				member4.ID = msg.reader().readInt();
				member4.head = msg.reader().readShort();
				member4.leg = msg.reader().readShort();
				member4.body = msg.reader().readShort();
				member4.name = msg.reader().readUTF();
				member4.role = msg.reader().readByte();
				member4.powerPoint = msg.reader().readUTF();
				member4.donate = msg.reader().readInt();
				member4.receive_donate = msg.reader().readInt();
				member4.clanPoint = msg.reader().readInt();
				member4.joinTime = NinjaUtil.getDate(msg.reader().readInt());
				for (int num132 = 0; num132 < GameCanvas.panel.myMember.size(); num132++)
				{
					Member member5 = (Member)GameCanvas.panel.myMember.elementAt(num132);
					if (member5.ID == member4.ID)
					{
						if (Char.myCharz().charID == member4.ID)
							Char.myCharz().role = member4.role;
						Member o2 = member4;
						GameCanvas.panel.myMember.removeElement(member5);
						GameCanvas.panel.myMember.insertElementAt(o2, num132);
						return;
					}
				}
				break;
			}
			case -51:
				InfoDlg.hide();
				readClanMsg(msg, 0);
				if (GameCanvas.panel.isMessage && GameCanvas.panel.type == 5)
					GameCanvas.panel.initTabClans();
				break;
			case -50:
			{
				InfoDlg.hide();
				GameCanvas.panel.member = new MyVector();
				sbyte b49 = msg.reader().readByte();
				for (int num128 = 0; num128 < b49; num128++)
				{
					Member member2 = new Member();
					member2.ID = msg.reader().readInt();
					member2.head = msg.reader().readShort();
					member2.leg = msg.reader().readShort();
					member2.body = msg.reader().readShort();
					member2.name = msg.reader().readUTF();
					member2.role = msg.reader().readByte();
					member2.powerPoint = msg.reader().readUTF();
					member2.donate = msg.reader().readInt();
					member2.receive_donate = msg.reader().readInt();
					member2.clanPoint = msg.reader().readInt();
					member2.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					GameCanvas.panel.member.addElement(member2);
				}
				GameCanvas.panel.isViewMember = true;
				GameCanvas.panel.isSearchClan = false;
				GameCanvas.panel.isMessage = false;
				GameCanvas.panel.currentListLength = GameCanvas.panel.member.size() + 2;
				GameCanvas.panel.initTabClans();
				break;
			}
			case -47:
			{
				InfoDlg.hide();
				sbyte b36 = msg.reader().readByte();
				Res.outz("clan = " + b36);
				if (b36 == 0)
				{
					GameCanvas.panel.clanReport = mResources.cannot_find_clan;
					GameCanvas.panel.clans = null;
				}
				else
				{
					GameCanvas.panel.clans = new Clan[b36];
					Res.outz("clan search lent= " + GameCanvas.panel.clans.Length);
					for (int num98 = 0; num98 < GameCanvas.panel.clans.Length; num98++)
					{
						GameCanvas.panel.clans[num98] = new Clan();
						GameCanvas.panel.clans[num98].ID = msg.reader().readInt();
						GameCanvas.panel.clans[num98].name = msg.reader().readUTF();
						GameCanvas.panel.clans[num98].slogan = msg.reader().readUTF();
						GameCanvas.panel.clans[num98].imgID = msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num98].powerPoint = msg.reader().readUTF();
						GameCanvas.panel.clans[num98].leaderName = msg.reader().readUTF();
						GameCanvas.panel.clans[num98].currMember = msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num98].maxMember = msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num98].date = msg.reader().readInt();
					}
				}
				GameCanvas.panel.isSearchClan = true;
				GameCanvas.panel.isViewMember = false;
				GameCanvas.panel.isMessage = false;
				if (GameCanvas.panel.isSearchClan)
					GameCanvas.panel.initTabClans();
				break;
			}
			case -46:
			{
				InfoDlg.hide();
				sbyte b31 = msg.reader().readByte();
				if (b31 == 1 || b31 == 3)
				{
					GameCanvas.endDlg();
					ClanImage.vClanImage.removeAllElements();
					int num80 = msg.reader().readUnsignedByte();
					for (int num81 = 0; num81 < num80; num81++)
					{
						ClanImage clanImage = new ClanImage();
						clanImage.ID = msg.reader().readUnsignedByte();
						clanImage.name = msg.reader().readUTF();
						clanImage.xu = msg.reader().readInt();
						clanImage.luong = msg.reader().readInt();
						if (!ClanImage.isExistClanImage(clanImage.ID))
						{
							ClanImage.addClanImage(clanImage);
							continue;
						}
						ClanImage.getClanImage((sbyte)clanImage.ID).name = clanImage.name;
						ClanImage.getClanImage((sbyte)clanImage.ID).xu = clanImage.xu;
						ClanImage.getClanImage((sbyte)clanImage.ID).luong = clanImage.luong;
					}
					if (Char.myCharz().clan != null)
						GameCanvas.panel.changeIcon();
				}
				if (b31 == 4)
				{
					Char.myCharz().clan.imgID = msg.reader().readUnsignedByte();
					Char.myCharz().clan.slogan = msg.reader().readUTF();
				}
				break;
			}
			case -45:
			{
				sbyte b25 = msg.reader().readByte();
				int num75 = msg.reader().readInt();
				short num76 = msg.reader().readShort();
				Res.outz("skill type= " + b25 + "   player use= " + num75);
				if (b25 == 0)
				{
					Res.outz("id use= " + num75);
					if (Char.myCharz().charID != num75)
					{
						@char = GameScr.findCharInMap(num75);
						if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
							@char.setSkillPaint(GameScr.sks[num76], 0);
						else
						{
							@char.setSkillPaint(GameScr.sks[num76], 1);
							@char.delayFall = 20;
						}
					}
					else
					{
						Char.myCharz().saveLoadPreviousSkill();
						Res.outz("LOAD LAST SKILL");
					}
					sbyte b26 = msg.reader().readByte();
					Res.outz("npc size= " + b26);
					for (int num77 = 0; num77 < b26; num77++)
					{
						sbyte b27 = msg.reader().readByte();
						sbyte b28 = msg.reader().readByte();
						Res.outz("index= " + b27);
						if (num76 >= 42 && num76 <= 48)
						{
							((Mob)GameScr.vMob.elementAt(b27)).isFreez = true;
							((Mob)GameScr.vMob.elementAt(b27)).seconds = b28;
							((Mob)GameScr.vMob.elementAt(b27)).last = (((Mob)GameScr.vMob.elementAt(b27)).cur = mSystem.currentTimeMillis());
						}
					}
					sbyte b29 = msg.reader().readByte();
					for (int num78 = 0; num78 < b29; num78++)
					{
						int num79 = msg.reader().readInt();
						sbyte b30 = msg.reader().readByte();
						Res.outz("player ID= " + num79 + " my ID= " + Char.myCharz().charID);
						if (num76 < 42 || num76 > 48)
							continue;
						if (num79 == Char.myCharz().charID)
						{
							if (!Char.myCharz().isFlyAndCharge && !Char.myCharz().isStandAndCharge)
							{
								GameScr.gI().isFreez = true;
								Char.myCharz().isFreez = true;
								Char.myCharz().freezSeconds = b30;
								Char.myCharz().lastFreez = (Char.myCharz().currFreez = mSystem.currentTimeMillis());
								Char.myCharz().isLockMove = true;
							}
						}
						else
						{
							@char = GameScr.findCharInMap(num79);
							if (@char != null && !@char.isFlyAndCharge && !@char.isStandAndCharge)
							{
								@char.isFreez = true;
								@char.seconds = b30;
								@char.freezSeconds = b30;
								@char.lastFreez = (GameScr.findCharInMap(num79).currFreez = mSystem.currentTimeMillis());
							}
						}
					}
				}
				if (b25 == 1 && num75 != Char.myCharz().charID)
					GameScr.findCharInMap(num75).isCharge = true;
				if (b25 == 3)
				{
					if (num75 == Char.myCharz().charID)
					{
						Char.myCharz().isCharge = false;
						SoundMn.gI().taitaoPause();
						Char.myCharz().saveLoadPreviousSkill();
					}
					else
						GameScr.findCharInMap(num75).isCharge = false;
				}
				if (b25 == 4)
				{
					if (num75 == Char.myCharz().charID)
					{
						Char.myCharz().seconds = msg.reader().readShort() - 1000;
						Char.myCharz().last = mSystem.currentTimeMillis();
						Res.outz("second= " + Char.myCharz().seconds + " last= " + Char.myCharz().last);
					}
					else if (GameScr.findCharInMap(num75) != null)
					{
						switch (GameScr.findCharInMap(num75).cgender)
						{
						case 0:
							GameScr.findCharInMap(num75).useChargeSkill(false);
							break;
						case 1:
							GameScr.findCharInMap(num75).useChargeSkill(true);
							break;
						}
						GameScr.findCharInMap(num75).skillTemplateId = num76;
						GameScr.findCharInMap(num75).isUseSkillAfterCharge = true;
						GameScr.findCharInMap(num75).seconds = msg.reader().readShort();
						GameScr.findCharInMap(num75).last = mSystem.currentTimeMillis();
					}
				}
				if (b25 == 5)
				{
					if (num75 == Char.myCharz().charID)
						Char.myCharz().stopUseChargeSkill();
					else if (GameScr.findCharInMap(num75) != null)
					{
						GameScr.findCharInMap(num75).stopUseChargeSkill();
					}
				}
				if (b25 == 6)
				{
					if (num75 == Char.myCharz().charID)
						Char.myCharz().setAutoSkillPaint(GameScr.sks[num76], 0);
					else if (GameScr.findCharInMap(num75) != null)
					{
						GameScr.findCharInMap(num75).setAutoSkillPaint(GameScr.sks[num76], 0);
						SoundMn.gI().gong();
					}
				}
				if (b25 == 7)
				{
					if (num75 == Char.myCharz().charID)
					{
						Char.myCharz().seconds = msg.reader().readShort();
						Res.outz("second = " + Char.myCharz().seconds);
						Char.myCharz().last = mSystem.currentTimeMillis();
					}
					else if (GameScr.findCharInMap(num75) != null)
					{
						GameScr.findCharInMap(num75).useChargeSkill(true);
						GameScr.findCharInMap(num75).seconds = msg.reader().readShort();
						GameScr.findCharInMap(num75).last = mSystem.currentTimeMillis();
						SoundMn.gI().gong();
					}
				}
				if (b25 == 8 && num75 != Char.myCharz().charID && GameScr.findCharInMap(num75) != null)
					GameScr.findCharInMap(num75).setAutoSkillPaint(GameScr.sks[num76], 0);
				break;
			}
			case -44:
			{
				bool flag8 = false;
				if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
					flag8 = true;
				sbyte b55 = msg.reader().readByte();
				int num137 = msg.reader().readUnsignedByte();
				Char.myCharz().arrItemShop = new Item[num137][];
				GameCanvas.panel.shopTabName = new string[num137 + ((!flag8) ? 1 : 0)][];
				for (int num138 = 0; num138 < GameCanvas.panel.shopTabName.Length; num138++)
				{
					GameCanvas.panel.shopTabName[num138] = new string[2];
				}
				if (b55 == 2)
				{
					GameCanvas.panel.maxPageShop = new int[num137];
					GameCanvas.panel.currPageShop = new int[num137];
				}
				if (!flag8)
					GameCanvas.panel.shopTabName[num137] = mResources.inventory;
				for (int num139 = 0; num139 < num137; num139++)
				{
					string[] array12 = Res.split(msg.reader().readUTF(), "\n", 0);
					if (b55 == 2)
						GameCanvas.panel.maxPageShop[num139] = msg.reader().readUnsignedByte();
					if (array12.Length == 2)
						GameCanvas.panel.shopTabName[num139] = array12;
					if (array12.Length == 1)
					{
						GameCanvas.panel.shopTabName[num139][0] = array12[0];
						GameCanvas.panel.shopTabName[num139][1] = string.Empty;
					}
					int num140 = msg.reader().readUnsignedByte();
					Char.myCharz().arrItemShop[num139] = new Item[num140];
					Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
					if (b55 == 1)
						Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy2;
					for (int num141 = 0; num141 < num140; num141++)
					{
						short num142 = msg.reader().readShort();
						if (num142 == -1)
							continue;
						Char.myCharz().arrItemShop[num139][num141] = new Item();
						Char.myCharz().arrItemShop[num139][num141].template = ItemTemplates.get(num142);
						Res.outz("name " + num139 + " = " + Char.myCharz().arrItemShop[num139][num141].template.name + " id templat= " + Char.myCharz().arrItemShop[num139][num141].template.id);
						if (b55 == 0)
						{
							Char.myCharz().arrItemShop[num139][num141].buyCoin = msg.reader().readInt();
							Char.myCharz().arrItemShop[num139][num141].buyGold = msg.reader().readInt();
						}
						else if (b55 == 1)
						{
							Char.myCharz().arrItemShop[num139][num141].powerRequire = msg.reader().readLong();
						}
						else if (b55 == 2)
						{
							Char.myCharz().arrItemShop[num139][num141].itemId = msg.reader().readShort();
							Char.myCharz().arrItemShop[num139][num141].buyCoin = msg.reader().readInt();
							Char.myCharz().arrItemShop[num139][num141].buyGold = msg.reader().readInt();
							Char.myCharz().arrItemShop[num139][num141].buyType = msg.reader().readByte();
							Char.myCharz().arrItemShop[num139][num141].quantity = msg.reader().readByte();
							Char.myCharz().arrItemShop[num139][num141].isMe = msg.reader().readByte();
						}
						else if (b55 == 3)
						{
							Char.myCharz().arrItemShop[num139][num141].isBuySpec = true;
							Char.myCharz().arrItemShop[num139][num141].iconSpec = msg.reader().readShort();
							Char.myCharz().arrItemShop[num139][num141].buySpec = msg.reader().readInt();
						}
						int num143 = msg.reader().readUnsignedByte();
						if (num143 != 0)
						{
							Char.myCharz().arrItemShop[num139][num141].itemOption = new ItemOption[num143];
							for (int num144 = 0; num144 < Char.myCharz().arrItemShop[num139][num141].itemOption.Length; num144++)
							{
								int optionTemplateId6 = msg.reader().readUnsignedByte();
								ushort param6 = msg.reader().readUnsignedShort();
								Char.myCharz().arrItemShop[num139][num141].itemOption[num144] = new ItemOption(optionTemplateId6, param6);
								Char.myCharz().arrItemShop[num139][num141].compare = GameCanvas.panel.getCompare(Char.myCharz().arrItemShop[num139][num141]);
							}
						}
						sbyte b56 = msg.reader().readByte();
						Char.myCharz().arrItemShop[num139][num141].newItem = ((b56 != 0) ? true : false);
						if (msg.reader().readByte() == 1)
						{
							int headTemp = msg.reader().readShort();
							int bodyTemp = msg.reader().readShort();
							int legTemp = msg.reader().readShort();
							short bagTemp = msg.reader().readShort();
							Char.myCharz().arrItemShop[num139][num141].setPartTemp(headTemp, bodyTemp, legTemp, bagTemp);
						}
					}
				}
				if (flag8)
				{
					if (b55 != 2)
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.tabName[7] = new string[1][] { new string[1] { string.Empty } };
						GameCanvas.panel2.setTypeBodyOnly();
						GameCanvas.panel2.show();
					}
					else
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.setTypeKiGuiOnly();
						GameCanvas.panel2.show();
					}
				}
				GameCanvas.panel.tabName[1] = GameCanvas.panel.shopTabName;
				if (b55 == 2)
				{
					string[][] array13 = GameCanvas.panel.tabName[1];
					if (flag8)
						GameCanvas.panel.tabName[1] = new string[4][]
						{
							array13[0],
							array13[1],
							array13[2],
							array13[3]
						};
					else
						GameCanvas.panel.tabName[1] = new string[5][]
						{
							array13[0],
							array13[1],
							array13[2],
							array13[3],
							array13[4]
						};
				}
				GameCanvas.panel.setTypeShop(b55);
				GameCanvas.panel.show();
				break;
			}
			case -43:
			{
				sbyte itemAction = msg.reader().readByte();
				sbyte where = msg.reader().readByte();
				sbyte index2 = msg.reader().readByte();
				string info4 = msg.reader().readUTF();
				GameCanvas.panel.itemRequest(itemAction, info4, where, index2);
				break;
			}
			case -42:
				Char.myCharz().cHPGoc = msg.readInt3Byte();
				Char.myCharz().cMPGoc = msg.readInt3Byte();
				Char.myCharz().cDamGoc = msg.reader().readInt();
				Char.myCharz().cHPFull = msg.readInt3Byte();
				Char.myCharz().cMPFull = msg.readInt3Byte();
				Char.myCharz().cHP = msg.readInt3Byte();
				Char.myCharz().cMP = msg.readInt3Byte();
				Char.myCharz().cspeed = msg.reader().readByte();
				Char.myCharz().hpFrom1000TiemNang = msg.reader().readByte();
				Char.myCharz().mpFrom1000TiemNang = msg.reader().readByte();
				Char.myCharz().damFrom1000TiemNang = msg.reader().readByte();
				Char.myCharz().cDamFull = msg.reader().readInt();
				Char.myCharz().cDefull = msg.reader().readInt();
				Char.myCharz().cCriticalFull = msg.reader().readByte();
				Char.myCharz().cTiemNang = msg.reader().readLong();
				Char.myCharz().expForOneAdd = msg.reader().readShort();
				Char.myCharz().cDefGoc = msg.reader().readShort();
				Char.myCharz().cCriticalGoc = msg.reader().readByte();
				InfoDlg.hide();
				break;
			case -41:
			{
				sbyte b51 = msg.reader().readByte();
				Char.myCharz().strLevel = new string[b51];
				for (int num129 = 0; num129 < b51; num129++)
				{
					string text5 = msg.reader().readUTF();
					Char.myCharz().strLevel[num129] = text5;
				}
				Res.outz("---   xong  level caption cmd : " + msg.command);
				break;
			}
			case -37:
			{
				sbyte b48 = msg.reader().readByte();
				Res.outz("cAction= " + b48);
				if (b48 != 0)
					break;
				Char.myCharz().head = msg.reader().readShort();
				Char.myCharz().setDefaultPart();
				int num122 = msg.reader().readUnsignedByte();
				Res.outz("num body = " + num122);
				Char.myCharz().arrItemBody = new Item[num122];
				for (int num123 = 0; num123 < num122; num123++)
				{
					short num124 = msg.reader().readShort();
					if (num124 == -1)
						continue;
					Char.myCharz().arrItemBody[num123] = new Item();
					Char.myCharz().arrItemBody[num123].template = ItemTemplates.get(num124);
					int num125 = Char.myCharz().arrItemBody[num123].template.type;
					Char.myCharz().arrItemBody[num123].quantity = msg.reader().readInt();
					Char.myCharz().arrItemBody[num123].info = msg.reader().readUTF();
					Char.myCharz().arrItemBody[num123].content = msg.reader().readUTF();
					int num126 = msg.reader().readUnsignedByte();
					if (num126 != 0)
					{
						Char.myCharz().arrItemBody[num123].itemOption = new ItemOption[num126];
						for (int num127 = 0; num127 < Char.myCharz().arrItemBody[num123].itemOption.Length; num127++)
						{
							int optionTemplateId5 = msg.reader().readUnsignedByte();
							ushort param5 = msg.reader().readUnsignedShort();
							Char.myCharz().arrItemBody[num123].itemOption[num127] = new ItemOption(optionTemplateId5, param5);
						}
					}
					switch (num125)
					{
					case 0:
						Char.myCharz().body = Char.myCharz().arrItemBody[num123].template.part;
						break;
					case 1:
						Char.myCharz().leg = Char.myCharz().arrItemBody[num123].template.part;
						break;
					}
				}
				break;
			}
			case -36:
			{
				sbyte b44 = msg.reader().readByte();
				Res.outz("cAction= " + b44);
				if (b44 == 0)
				{
					int num113 = msg.reader().readUnsignedByte();
					Char.myCharz().arrItemBag = new Item[num113];
					GameScr.hpPotion = 0;
					Res.outz("numC=" + num113);
					for (int num114 = 0; num114 < num113; num114++)
					{
						short num115 = msg.reader().readShort();
						if (num115 == -1)
							continue;
						Char.myCharz().arrItemBag[num114] = new Item();
						Char.myCharz().arrItemBag[num114].template = ItemTemplates.get(num115);
						Char.myCharz().arrItemBag[num114].quantity = msg.reader().readInt();
						Char.myCharz().arrItemBag[num114].info = msg.reader().readUTF();
						Char.myCharz().arrItemBag[num114].content = msg.reader().readUTF();
						Char.myCharz().arrItemBag[num114].indexUI = num114;
						int num116 = msg.reader().readUnsignedByte();
						if (num116 != 0)
						{
							Char.myCharz().arrItemBag[num114].itemOption = new ItemOption[num116];
							for (int num117 = 0; num117 < Char.myCharz().arrItemBag[num114].itemOption.Length; num117++)
							{
								int optionTemplateId4 = msg.reader().readUnsignedByte();
								ushort param4 = msg.reader().readUnsignedShort();
								Char.myCharz().arrItemBag[num114].itemOption[num117] = new ItemOption(optionTemplateId4, param4);
							}
							Char.myCharz().arrItemBag[num114].compare = GameCanvas.panel.getCompare(Char.myCharz().arrItemBag[num114]);
						}
						if (Char.myCharz().arrItemBag[num114].template.type == 11)
							;
						if (Char.myCharz().arrItemBag[num114].template.type == 6)
							GameScr.hpPotion += Char.myCharz().arrItemBag[num114].quantity;
					}
				}
				if (b44 == 2)
				{
					sbyte b45 = msg.reader().readByte();
					sbyte b46 = msg.reader().readByte();
					int quantity = Char.myCharz().arrItemBag[b45].quantity;
					Char.myCharz().arrItemBag[b45].quantity = b46;
					if (Char.myCharz().arrItemBag[b45].quantity < quantity && Char.myCharz().arrItemBag[b45].template.type == 6)
						GameScr.hpPotion -= quantity - Char.myCharz().arrItemBag[b45].quantity;
					if (Char.myCharz().arrItemBag[b45].quantity == 0)
						Char.myCharz().arrItemBag[b45] = null;
				}
				break;
			}
			case -35:
			{
				sbyte b32 = msg.reader().readByte();
				Res.outz("cAction= " + b32);
				if (b32 == 0)
				{
					int num86 = msg.reader().readUnsignedByte();
					Char.myCharz().arrItemBox = new Item[num86];
					GameCanvas.panel.hasUse = 0;
					for (int num87 = 0; num87 < num86; num87++)
					{
						short num88 = msg.reader().readShort();
						if (num88 == -1)
							continue;
						Char.myCharz().arrItemBox[num87] = new Item();
						Char.myCharz().arrItemBox[num87].template = ItemTemplates.get(num88);
						Char.myCharz().arrItemBox[num87].quantity = msg.reader().readInt();
						Char.myCharz().arrItemBox[num87].info = msg.reader().readUTF();
						Char.myCharz().arrItemBox[num87].content = msg.reader().readUTF();
						int num89 = msg.reader().readUnsignedByte();
						if (num89 != 0)
						{
							Char.myCharz().arrItemBox[num87].itemOption = new ItemOption[num89];
							for (int num90 = 0; num90 < Char.myCharz().arrItemBox[num87].itemOption.Length; num90++)
							{
								int optionTemplateId3 = msg.reader().readUnsignedByte();
								ushort param3 = msg.reader().readUnsignedShort();
								Char.myCharz().arrItemBox[num87].itemOption[num90] = new ItemOption(optionTemplateId3, param3);
							}
						}
						GameCanvas.panel.hasUse++;
					}
				}
				if (b32 == 1)
				{
					bool isBoxClan = false;
					try
					{
						if (msg.reader().readByte() == 1)
							isBoxClan = true;
					}
					catch (Exception)
					{
					}
					GameCanvas.panel.setTypeBox();
					GameCanvas.panel.isBoxClan = isBoxClan;
					GameCanvas.panel.show();
				}
				if (b32 == 2)
				{
					sbyte b33 = msg.reader().readByte();
					sbyte num91 = msg.reader().readByte();
					Char.myCharz().arrItemBox[b33].quantity = num91;
					if (Char.myCharz().arrItemBox[b33].quantity == 0)
						Char.myCharz().arrItemBox[b33] = null;
				}
				break;
			}
			case -34:
			{
				sbyte b18 = msg.reader().readByte();
				Res.outz("act= " + b18);
				if (b18 == 0 && GameScr.gI().magicTree != null)
				{
					Res.outz("toi duoc day");
					MagicTree magicTree = GameScr.gI().magicTree;
					magicTree.id = msg.reader().readShort();
					magicTree.name = msg.reader().readUTF();
					magicTree.name = Res.changeString(magicTree.name);
					magicTree.x = msg.reader().readShort();
					magicTree.y = msg.reader().readShort();
					magicTree.level = msg.reader().readByte();
					magicTree.currPeas = msg.reader().readShort();
					magicTree.maxPeas = msg.reader().readShort();
					Res.outz("curr Peas= " + magicTree.currPeas);
					magicTree.strInfo = msg.reader().readUTF();
					magicTree.seconds = msg.reader().readInt();
					magicTree.timeToRecieve = magicTree.seconds;
					sbyte b19 = msg.reader().readByte();
					magicTree.peaPostionX = new int[b19];
					magicTree.peaPostionY = new int[b19];
					for (int num61 = 0; num61 < b19; num61++)
					{
						magicTree.peaPostionX[num61] = msg.reader().readByte();
						magicTree.peaPostionY[num61] = msg.reader().readByte();
					}
					magicTree.isUpdate = msg.reader().readBool();
					magicTree.last = (magicTree.cur = mSystem.currentTimeMillis());
					GameScr.gI().magicTree.isUpdateTree = true;
				}
				if (b18 == 1)
				{
					myVector = new MyVector();
					try
					{
						while (msg.reader().available() > 0)
						{
							myVector.addElement(new Command(msg.reader().readUTF(), GameCanvas.instance, 888392, null));
						}
					}
					catch (Exception ex5)
					{
						Cout.println("Loi MAGIC_TREE " + ex5.ToString());
					}
					GameCanvas.menu.startAt(myVector, 3);
				}
				if (b18 == 2)
				{
					GameScr.gI().magicTree.remainPeas = msg.reader().readShort();
					GameScr.gI().magicTree.seconds = msg.reader().readInt();
					GameScr.gI().magicTree.last = (GameScr.gI().magicTree.cur = mSystem.currentTimeMillis());
					GameScr.gI().magicTree.isUpdateTree = true;
					GameScr.gI().magicTree.isPeasEffect = true;
				}
				break;
			}
			case -32:
			{
				if (GameCanvas.lowGraphic && TileMap.mapID != 51 && TileMap.mapID != 103)
					return;
				short num33 = msg.reader().readShort();
				int num34 = msg.reader().readInt();
				sbyte[] array2 = null;
				Image image = null;
				try
				{
					array2 = new sbyte[num34];
					for (int num35 = 0; num35 < num34; num35++)
					{
						array2[num35] = msg.reader().readByte();
					}
					image = Image.createImage(array2, 0, num34);
					BgItem.imgNew.put(num33 + string.Empty, image);
				}
				catch (Exception)
				{
					array2 = null;
					BgItem.imgNew.put(num33 + string.Empty, Image.createRGBImage(new int[1], 1, 1, true));
				}
				if (array2 != null)
				{
					if (mGraphics.zoomLevel > 1)
						Rms.saveRMS(mGraphics.zoomLevel + "bgItem" + num33, array2);
					BgItemMn.blendcurrBg(num33, image);
				}
				break;
			}
			case -31:
			{
				if (GameCanvas.lowGraphic && TileMap.mapID != 51)
					return;
				TileMap.vItemBg.removeAllElements();
				short num15 = msg.reader().readShort();
				Cout.LogError2("nItem= " + num15);
				for (int num16 = 0; num16 < num15; num16++)
				{
					BgItem bgItem = new BgItem();
					bgItem.id = num16;
					bgItem.idImage = msg.reader().readShort();
					bgItem.layer = msg.reader().readByte();
					bgItem.dx = msg.reader().readShort();
					bgItem.dy = msg.reader().readShort();
					sbyte b8 = msg.reader().readByte();
					bgItem.tileX = new int[b8];
					bgItem.tileY = new int[b8];
					for (int num17 = 0; num17 < b8; num17++)
					{
						bgItem.tileX[num16] = msg.reader().readByte();
						bgItem.tileY[num16] = msg.reader().readByte();
					}
					TileMap.vItemBg.addElement(bgItem);
				}
				break;
			}
			case -30:
				messageSubCommand(msg);
				break;
			case -29:
				messageNotLogin(msg);
				break;
			case -28:
				messageNotMap(msg);
				break;
			case -26:
				ServerListScreen.testConnect = 2;
				GameCanvas.debug("SA2", 2);
				GameCanvas.startOKDlg(msg.reader().readUTF());
				InfoDlg.hide();
				LoginScr.isContinueToLogin = false;
				Char.isLoadingMap = false;
				if (GameCanvas.currentScreen == GameCanvas.loginScr)
					GameCanvas.serverScreen.switchToMe();
				break;
			case -25:
				GameCanvas.debug("SA3", 2);
				GameScr.info1.addInfo(msg.reader().readUTF(), 0);
				break;
			case -24:
				Char.isLoadingMap = true;
				Cout.println("GET MAP INFO");
				GameScr.gI().magicTree = null;
				GameCanvas.isLoading = true;
				GameCanvas.debug("SA75", 2);
				GameScr.resetAllvector();
				GameCanvas.endDlg();
				TileMap.vGo.removeAllElements();
				PopUp.vPopups.removeAllElements();
				mSystem.gcc();
				TileMap.mapID = msg.reader().readUnsignedByte();
				TileMap.planetID = msg.reader().readByte();
				TileMap.tileID = msg.reader().readByte();
				TileMap.bgID = msg.reader().readByte();
				Cout.println("load planet from server: " + TileMap.planetID + "bgType= " + TileMap.bgType + ".............................");
				TileMap.typeMap = msg.reader().readByte();
				TileMap.mapName = msg.reader().readUTF();
				TileMap.zoneID = msg.reader().readByte();
				GameCanvas.debug("SA75x1", 2);
				try
				{
					TileMap.loadMapFromResource(TileMap.mapID);
				}
				catch (Exception)
				{
					Service.gI().requestMaptemplate(TileMap.mapID);
					messWait = msg;
					return;
				}
				loadInfoMap(msg);
				try
				{
					TileMap.isMapDouble = ((msg.reader().readByte() != 0) ? true : false);
				}
				catch (Exception)
				{
				}
				GameScr.cmx = GameScr.cmtoX;
				GameScr.cmy = GameScr.cmtoY;
				break;
			case -22:
				GameCanvas.debug("SA65", 2);
				Char.isLockKey = true;
				Char.ischangingMap = true;
				GameScr.gI().timeStartMap = 0;
				GameScr.gI().timeLengthMap = 0;
				Char.myCharz().mobFocus = null;
				Char.myCharz().npcFocus = null;
				Char.myCharz().charFocus = null;
				Char.myCharz().itemFocus = null;
				Char.myCharz().focus.removeAllElements();
				Char.myCharz().testCharId = -9999;
				Char.myCharz().killCharId = -9999;
				GameCanvas.resetBg();
				GameScr.gI().resetButton();
				GameScr.gI().center = null;
				break;
			case -21:
			{
				GameCanvas.debug("SA60", 2);
				short itemMapID = msg.reader().readShort();
				for (int num145 = 0; num145 < GameScr.vItemMap.size(); num145++)
				{
					if (((ItemMap)GameScr.vItemMap.elementAt(num145)).itemMapID == itemMapID)
					{
						GameScr.vItemMap.removeElementAt(num145);
						break;
					}
				}
				break;
			}
			case -20:
			{
				GameCanvas.debug("SA61", 2);
				Char.myCharz().itemFocus = null;
				short itemMapID = msg.reader().readShort();
				for (int num135 = 0; num135 < GameScr.vItemMap.size(); num135++)
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(num135);
					if (itemMap.itemMapID != itemMapID)
						continue;
					itemMap.setPoint(Char.myCharz().cx, Char.myCharz().cy - 10);
					string text6 = msg.reader().readUTF();
					num = 0;
					try
					{
						num = msg.reader().readShort();
						if (itemMap.template.type == 9)
						{
							num = msg.reader().readShort();
							Char.myCharz().xu += num;
						}
						else if (itemMap.template.type == 10)
						{
							num = msg.reader().readShort();
							Char.myCharz().luong += num;
						}
					}
					catch (Exception)
					{
					}
					if (text6.Equals(string.Empty))
					{
						if (itemMap.template.type == 9)
						{
							GameScr.startFlyText(((num >= 0) ? "+" : string.Empty) + num, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch, 0, -2, mFont.YELLOW);
							SoundMn.gI().getItem();
						}
						else if (itemMap.template.type == 10)
						{
							GameScr.startFlyText(((num >= 0) ? "+" : string.Empty) + num, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch, 0, -2, mFont.GREEN);
							SoundMn.gI().getItem();
						}
						else
						{
							GameScr.info1.addInfo(mResources.you_receive + " " + ((num <= 0) ? string.Empty : (num + " ")) + itemMap.template.name, 0);
							SoundMn.gI().getItem();
						}
						if (num > 0 && Char.myCharz().petFollow != null && Char.myCharz().petFollow.smallID == 4683)
						{
							ServerEffect.addServerEffect(55, Char.myCharz().petFollow.cmx, Char.myCharz().petFollow.cmy, 1);
							ServerEffect.addServerEffect(55, Char.myCharz().cx, Char.myCharz().cy, 1);
						}
					}
					else if (text6.Length == 1)
					{
						Cout.LogError3("strInf.Length =1:  " + text6);
					}
					else
					{
						GameScr.info1.addInfo(text6, 0);
					}
					break;
				}
				break;
			}
			case -19:
			{
				GameCanvas.debug("SA62", 2);
				short itemMapID = msg.reader().readShort();
				@char = GameScr.findCharInMap(msg.reader().readInt());
				for (int num136 = 0; num136 < GameScr.vItemMap.size(); num136++)
				{
					ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(num136);
					if (itemMap2.itemMapID != itemMapID)
						continue;
					if (@char == null)
						return;
					itemMap2.setPoint(@char.cx, @char.cy - 10);
					if (itemMap2.x < @char.cx)
						@char.cdir = -1;
					else if (itemMap2.x > @char.cx)
					{
						@char.cdir = 1;
					}
					break;
				}
				break;
			}
			case -18:
			{
				GameCanvas.debug("SA63", 2);
				int num134 = msg.reader().readByte();
				GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), Char.myCharz().arrItemBag[num134].template.id, Char.myCharz().cx, Char.myCharz().cy, msg.reader().readShort(), msg.reader().readShort()));
				Char.myCharz().arrItemBag[num134] = null;
				break;
			}
			case -14:
				GameCanvas.debug("SA64", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
					return;
				GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), msg.reader().readShort(), @char.cx, @char.cy, msg.reader().readShort(), msg.reader().readShort()));
				break;
			case -4:
			{
				GameCanvas.debug("SA76", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
					return;
				GameCanvas.debug("SA76v1", 2);
				if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
					@char.setSkillPaint(GameScr.sks[msg.reader().readUnsignedByte()], 0);
				else
					@char.setSkillPaint(GameScr.sks[msg.reader().readUnsignedByte()], 1);
				GameCanvas.debug("SA76v2", 2);
				@char.attMobs = new Mob[msg.reader().readByte()];
				for (int num119 = 0; num119 < @char.attMobs.Length; num119++)
				{
					Mob mob7 = (Mob)GameScr.vMob.elementAt(msg.reader().readByte());
					@char.attMobs[num119] = mob7;
					if (num119 == 0)
					{
						if (@char.cx <= mob7.x)
							@char.cdir = 1;
						else
							@char.cdir = -1;
					}
				}
				GameCanvas.debug("SA76v3", 2);
				@char.charFocus = null;
				@char.mobFocus = @char.attMobs[0];
				Char[] array9 = new Char[10];
				num = 0;
				try
				{
					for (num = 0; num < array9.Length; num++)
					{
						int num73 = msg.reader().readInt();
						Char char9 = (array9[num] = ((num73 != Char.myCharz().charID) ? GameScr.findCharInMap(num73) : Char.myCharz()));
						if (num == 0)
						{
							if (@char.cx <= char9.cx)
								@char.cdir = 1;
							else
								@char.cdir = -1;
						}
					}
				}
				catch (Exception ex14)
				{
					Cout.println("Loi PLAYER_ATTACK_N_P " + ex14.ToString());
				}
				GameCanvas.debug("SA76v4", 2);
				if (num > 0)
				{
					@char.attChars = new Char[num];
					for (num = 0; num < @char.attChars.Length; num++)
					{
						@char.attChars[num] = array9[num];
					}
					@char.charFocus = @char.attChars[0];
					@char.mobFocus = null;
				}
				GameCanvas.debug("SA76v5", 2);
				break;
			}
			case 1:
			{
				bool flag7 = msg.reader().readBool();
				Res.outz("isRes= " + flag7);
				if (!flag7)
				{
					GameCanvas.startOKDlg(msg.reader().readUTF());
					break;
				}
				GameCanvas.loginScr.isLogin2 = false;
				Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, string.Empty);
				GameCanvas.endDlg();
				GameCanvas.loginScr.doLogin();
				break;
			}
			case 2:
				Char.isLoadingMap = true;
				LoginScr.isLoggingIn = false;
				if (!GameScr.isLoadAllData)
					GameScr.gI().initSelectChar();
				BgItem.clearHashTable();
				GameCanvas.endDlg();
				CreateCharScr.isCreateChar = true;
				CreateCharScr.gI().switchToMe();
				break;
			case 6:
				GameCanvas.debug("SA70", 2);
				Char.myCharz().xu = msg.reader().readInt();
				Char.myCharz().luong = msg.reader().readInt();
				GameCanvas.endDlg();
				break;
			case 7:
			{
				sbyte type = msg.reader().readByte();
				short id2 = msg.reader().readShort();
				string info3 = msg.reader().readUTF();
				GameCanvas.panel.saleRequest(type, info3, id2);
				break;
			}
			case 11:
			{
				GameCanvas.debug("SA9", 2);
				int num96 = msg.reader().readByte();
				if (msg.reader().readByte() == 1)
					Mob.arrMobTemplate[num96].data.readData2(NinjaUtil.readByteArray(msg));
				else
					Mob.arrMobTemplate[num96].data.readData(NinjaUtil.readByteArray(msg));
				for (int num97 = 0; num97 < GameScr.vMob.size(); num97++)
				{
					mob = (Mob)GameScr.vMob.elementAt(num97);
					if (mob.templateId == num96)
					{
						mob.w = Mob.arrMobTemplate[num96].data.width;
						mob.h = Mob.arrMobTemplate[num96].data.height;
					}
				}
				sbyte[] array8 = NinjaUtil.readByteArray(msg);
				Image img = Image.createImage(array8, 0, array8.Length);
				Mob.arrMobTemplate[num96].data.img = img;
				break;
			}
			case 20:
			{
				GameCanvas.debug("SZ7", 2);
				mob = (Mob)GameScr.vMob.elementAt(msg.reader().readByte());
				int num73 = msg.reader().readInt();
				@char = ((num73 != Char.myCharz().charID) ? GameScr.findCharInMap(num73) : Char.myCharz());
				@char.moveFast = new short[3];
				@char.moveFast[0] = 0;
				@char.moveFast[1] = (short)mob.x;
				@char.moveFast[2] = (short)mob.y;
				break;
			}
			case 24:
			{
				GameCanvas.debug("SA69", 2);
				Char.myCharz().xuInBox = msg.reader().readInt();
				Char.myCharz().arrItemBox = new Item[msg.reader().readUnsignedByte()];
				for (int num84 = 0; num84 < Char.myCharz().arrItemBox.Length; num84++)
				{
					short num85 = msg.reader().readShort();
					if (num85 != -1)
					{
						Char.myCharz().arrItemBox[num84] = new Item();
						Char.myCharz().arrItemBox[num84].typeUI = 4;
						Char.myCharz().arrItemBox[num84].indexUI = num84;
						Char.myCharz().arrItemBox[num84].template = ItemTemplates.get(num85);
						Char.myCharz().arrItemBox[num84].isLock = msg.reader().readBool();
						if (Char.myCharz().arrItemBox[num84].isTypeBody())
							Char.myCharz().arrItemBox[num84].upgrade = msg.reader().readByte();
						Char.myCharz().arrItemBox[num84].isExpires = msg.reader().readBool();
						Char.myCharz().arrItemBox[num84].quantity = msg.reader().readShort();
					}
				}
				break;
			}
			case 27:
			{
				myVector = new MyVector();
				msg.reader().readUTF();
				int num82 = msg.reader().readByte();
				for (int num83 = 0; num83 < num82; num83++)
				{
					myVector.addElement(new Command(msg.reader().readUTF(), p: msg.reader().readShort(), actionListener: GameCanvas.instance, action: 88819));
				}
				GameCanvas.menu.startWithoutCloseButton(myVector, 3);
				break;
			}
			case 29:
				GameCanvas.debug("SA58", 2);
				GameScr.gI().openUIZone(msg);
				break;
			case 32:
			{
				GameCanvas.debug("SA68", 2);
				int num3 = msg.reader().readShort();
				for (int num52 = 0; num52 < GameScr.vNpc.size(); num52++)
				{
					Npc npc3 = (Npc)GameScr.vNpc.elementAt(num52);
					if (npc3.template.npcTemplateId == num3 && npc3.Equals(Char.myCharz().npcFocus))
					{
						string chat2 = msg.reader().readUTF();
						string[] array4 = new string[msg.reader().readByte()];
						for (int num53 = 0; num53 < array4.Length; num53++)
						{
							array4[num53] = msg.reader().readUTF();
						}
						GameScr.gI().createMenu(array4, npc3);
						ChatPopup.addChatPopup(chat2, 100000, npc3);
						return;
					}
				}
				Npc npc4 = new Npc(num3, 0, -100, 100, num3, GameScr.info1.charId[Char.myCharz().cgender][2]);
				Res.outz((Char.myCharz().npcFocus == null) ? "null" : "!null");
				string chat3 = msg.reader().readUTF();
				string[] array5 = new string[msg.reader().readByte()];
				for (int num54 = 0; num54 < array5.Length; num54++)
				{
					array5[num54] = msg.reader().readUTF();
				}
				try
				{
					npc4.avatar = msg.reader().readShort();
				}
				catch (Exception)
				{
				}
				Res.outz((Char.myCharz().npcFocus == null) ? "null" : "!null");
				GameScr.gI().createMenu(array5, npc4);
				ChatPopup.addChatPopup(chat3, 100000, npc4);
				break;
			}
			case 33:
			{
				GameCanvas.debug("SA51", 2);
				InfoDlg.hide();
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				myVector = new MyVector();
				try
				{
					while (true)
					{
						myVector.addElement(new Command(msg.reader().readUTF(), GameCanvas.instance, 88822, null));
					}
				}
				catch (Exception ex3)
				{
					Cout.println("Loi OPEN_UI_MENU " + ex3.ToString());
				}
				if (Char.myCharz().npcFocus == null)
					return;
				for (int num36 = 0; num36 < Char.myCharz().npcFocus.template.menu.Length; num36++)
				{
					string[] array3 = Char.myCharz().npcFocus.template.menu[num36];
					myVector.addElement(new Command(array3[0], GameCanvas.instance, 88820, array3));
				}
				GameCanvas.menu.startAt(myVector, 3);
				break;
			}
			case 38:
			{
				GameCanvas.debug("SA67", 2);
				InfoDlg.hide();
				int num3 = msg.reader().readShort();
				Res.outz("OPEN_UI_SAY ID= " + num3);
				string chat = Res.changeString(msg.reader().readUTF());
				for (int n = 0; n < GameScr.vNpc.size(); n++)
				{
					Npc npc = (Npc)GameScr.vNpc.elementAt(n);
					Res.outz("npc id= " + npc.template.npcTemplateId);
					if (npc.template.npcTemplateId == num3)
					{
						ChatPopup.addChatPopupMultiLine(chat, 100000, npc);
						GameCanvas.panel.hideNow();
						return;
					}
				}
				Npc npc2 = new Npc(num3, 0, 0, 0, num3, GameScr.info1.charId[Char.myCharz().cgender][2]);
				if (npc2.template.npcTemplateId == 5)
					npc2.charID = 5;
				try
				{
					npc2.avatar = msg.reader().readShort();
				}
				catch (Exception)
				{
				}
				ChatPopup.addChatPopupMultiLine(chat, 100000, npc2);
				GameCanvas.panel.hideNow();
				break;
			}
			case 39:
				GameCanvas.debug("SA49", 2);
				GameScr.gI().typeTradeOrder = 2;
				if (GameScr.gI().typeTrade >= 2 && GameScr.gI().typeTradeOrder >= 2)
					InfoDlg.showWait();
				break;
			case 40:
			{
				GameCanvas.debug("SA52", 2);
				GameCanvas.taskTick = 150;
				short taskId = msg.reader().readShort();
				sbyte index3 = msg.reader().readByte();
				string name2 = Res.changeString(msg.reader().readUTF());
				string detail = Res.changeString(msg.reader().readUTF());
				string[] array14 = new string[msg.reader().readByte()];
				string[] array15 = new string[array14.Length];
				GameScr.tasks = new int[array14.Length];
				GameScr.mapTasks = new int[array14.Length];
				short[] array16 = new short[array14.Length];
				short count = -1;
				for (int num151 = 0; num151 < array14.Length; num151++)
				{
					string text7 = Res.changeString(msg.reader().readUTF());
					GameScr.tasks[num151] = msg.reader().readByte();
					GameScr.mapTasks[num151] = msg.reader().readShort();
					string text8 = Res.changeString(msg.reader().readUTF());
					array16[num151] = -1;
					if (!text7.Equals(string.Empty))
					{
						array14[num151] = text7;
						array15[num151] = text8;
					}
				}
				try
				{
					count = msg.reader().readShort();
					for (int num152 = 0; num152 < array14.Length; num152++)
					{
						array16[num152] = msg.reader().readShort();
					}
				}
				catch (Exception ex19)
				{
					Cout.println("Loi TASK_GET " + ex19.ToString());
				}
				Char.myCharz().taskMaint = new Task(taskId, index3, name2, detail, array14, array16, count, array15);
				if (Char.myCharz().npcFocus != null)
					Npc.clearEffTask();
				Char.taskAction(false);
				break;
			}
			case 41:
				GameCanvas.debug("SA53", 2);
				GameCanvas.taskTick = 100;
				Res.outz("TASK NEXT");
				Char.myCharz().taskMaint.index++;
				Char.myCharz().taskMaint.count = 0;
				Npc.clearEffTask();
				Char.taskAction(true);
				break;
			case 43:
				GameCanvas.taskTick = 50;
				GameCanvas.debug("SA55", 2);
				Char.myCharz().taskMaint.count = msg.reader().readShort();
				if (Char.myCharz().npcFocus != null)
					Npc.clearEffTask();
				break;
			case 46:
				GameCanvas.debug("SA5", 2);
				Cout.LogWarning("Controler RESET_POINT  " + Char.ischangingMap);
				Char.isLockKey = false;
				Char.myCharz().setResetPoint(msg.reader().readShort(), msg.reader().readShort());
				break;
			case 47:
				GameCanvas.debug("SA4", 2);
				GameScr.gI().resetButton();
				break;
			case 50:
			{
				sbyte b57 = msg.reader().readByte();
				Panel.vGameInfo.removeAllElements();
				for (int num148 = 0; num148 < b57; num148++)
				{
					GameInfo gameInfo = new GameInfo();
					gameInfo.id = msg.reader().readShort();
					gameInfo.main = msg.reader().readUTF();
					gameInfo.content = msg.reader().readUTF();
					Panel.vGameInfo.addElement(gameInfo);
					gameInfo.hasRead = Rms.loadRMSInt(gameInfo.id + string.Empty) != -1;
				}
				break;
			}
			case 54:
			{
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
					return;
				int num133 = msg.reader().readUnsignedByte();
				if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
					@char.setSkillPaint(GameScr.sks[num133], 0);
				else
					@char.setSkillPaint(GameScr.sks[num133], 1);
				GameCanvas.debug("SA769991v2", 2);
				Mob[] array11 = new Mob[10];
				num = 0;
				try
				{
					GameCanvas.debug("SA769991v3", 2);
					for (num = 0; num < array11.Length; num++)
					{
						GameCanvas.debug("SA769991v4-num" + num, 2);
						Mob mob8 = (array11[num] = (Mob)GameScr.vMob.elementAt(msg.reader().readByte()));
						if (num == 0)
						{
							if (@char.cx <= mob8.x)
								@char.cdir = 1;
							else
								@char.cdir = -1;
						}
						GameCanvas.debug("SA769991v5-num" + num, 2);
					}
				}
				catch (Exception ex15)
				{
					Cout.println("Loi PLAYER_ATTACK_NPC " + ex15.ToString());
				}
				GameCanvas.debug("SA769992", 2);
				if (num > 0)
				{
					@char.attMobs = new Mob[num];
					for (num = 0; num < @char.attMobs.Length; num++)
					{
						@char.attMobs[num] = array11[num];
					}
					@char.charFocus = null;
					@char.mobFocus = @char.attMobs[0];
				}
				break;
			}
			case 56:
			{
				GameCanvas.debug("SXX6", 2);
				@char = null;
				int num73 = msg.reader().readInt();
				if (num73 == Char.myCharz().charID)
				{
					bool flag5 = false;
					@char = Char.myCharz();
					@char.cHP = msg.readInt3Byte();
					int num109 = msg.readInt3Byte();
					Res.outz("dame hit = " + num109);
					if (num109 != 0)
						@char.doInjure();
					int num110 = 0;
					try
					{
						flag5 = msg.reader().readBoolean();
						sbyte b42 = msg.reader().readByte();
						if (b42 != -1)
						{
							Res.outz("hit eff= " + b42);
							EffecMn.addEff(new Effect(b42, @char.cx, @char.cy, 3, 1, -1));
						}
					}
					catch (Exception)
					{
					}
					num109 += num110;
					if (Char.myCharz().cTypePk != 4)
					{
						if (num109 == 0)
							GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS_ME);
						else
							GameScr.startFlyText("-" + num109, @char.cx, @char.cy - @char.ch, 0, -3, flag5 ? mFont.FATAL : mFont.RED);
					}
					break;
				}
				@char = GameScr.findCharInMap(num73);
				if (@char == null)
					return;
				@char.cHP = msg.readInt3Byte();
				bool flag6 = false;
				int num111 = msg.readInt3Byte();
				if (num111 != 0)
					@char.doInjure();
				int num112 = 0;
				try
				{
					flag6 = msg.reader().readBoolean();
					sbyte b43 = msg.reader().readByte();
					if (b43 != -1)
					{
						Res.outz("hit eff= " + b43);
						EffecMn.addEff(new Effect(b43, @char.cx, @char.cy, 3, 1, -1));
					}
				}
				catch (Exception)
				{
				}
				num111 += num112;
				if (@char.cTypePk != 4)
				{
					if (num111 == 0)
						GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS);
					else
						GameScr.startFlyText("-" + num111, @char.cx, @char.cy - @char.ch, 0, -3, flag6 ? mFont.FATAL : mFont.ORANGE);
				}
				break;
			}
			case 57:
			{
				GameCanvas.debug("SZ6", 2);
				MyVector myVector2 = new MyVector();
				myVector2.addElement(new Command(msg.reader().readUTF(), GameCanvas.instance, 88817, null));
				GameCanvas.menu.startAt(myVector2, 3);
				break;
			}
			case 58:
			{
				GameCanvas.debug("SZ7", 2);
				int num73 = msg.reader().readInt();
				Char char5 = ((num73 != Char.myCharz().charID) ? GameScr.findCharInMap(num73) : Char.myCharz());
				char5.moveFast = new short[3];
				char5.moveFast[0] = 0;
				short num101 = msg.reader().readShort();
				short num102 = msg.reader().readShort();
				char5.moveFast[1] = num101;
				char5.moveFast[2] = num102;
				try
				{
					num73 = msg.reader().readInt();
					Char char6 = ((num73 != Char.myCharz().charID) ? GameScr.findCharInMap(num73) : Char.myCharz());
					char6.cx = num101;
					char6.cy = num102;
				}
				catch (Exception ex10)
				{
					Cout.println("Loi MOVE_FAST " + ex10.ToString());
				}
				break;
			}
			case 62:
				GameCanvas.debug("SZ3", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.killCharId = Char.myCharz().charID;
					Char.myCharz().npcFocus = null;
					Char.myCharz().mobFocus = null;
					Char.myCharz().itemFocus = null;
					Char.myCharz().charFocus = @char;
					Char.isManualFocus = true;
					GameScr.info1.addInfo(@char.cName + mResources.CUU_SAT, 0);
				}
				break;
			case 63:
				GameCanvas.debug("SZ4", 2);
				Char.myCharz().killCharId = msg.reader().readInt();
				Char.myCharz().npcFocus = null;
				Char.myCharz().mobFocus = null;
				Char.myCharz().itemFocus = null;
				Char.myCharz().charFocus = GameScr.findCharInMap(Char.myCharz().killCharId);
				Char.isManualFocus = true;
				break;
			case 64:
				GameCanvas.debug("SZ5", 2);
				@char = Char.myCharz();
				try
				{
					@char = GameScr.findCharInMap(msg.reader().readInt());
				}
				catch (Exception ex9)
				{
					Cout.println("Loi CLEAR_CUU_SAT " + ex9.ToString());
				}
				@char.killCharId = -9999;
				break;
			case 65:
			{
				sbyte b34 = msg.reader().readSByte();
				string text4 = msg.reader().readUTF();
				short num93 = msg.reader().readShort();
				if (ItemTime.isExistMessage(b34))
				{
					if (num93 != 0)
						ItemTime.getMessageById(b34).initTimeText(b34, text4, num93);
					else
						GameScr.textTime.removeElement(ItemTime.getMessageById(b34));
				}
				else
				{
					ItemTime itemTime = new ItemTime();
					itemTime.initTimeText(b34, text4, num93);
					GameScr.textTime.addElement(itemTime);
				}
				break;
			}
			case 66:
				readGetImgByName(msg);
				break;
			case 68:
			{
				Res.outz("ADD ITEM TO MAP --------------------------------------");
				GameCanvas.debug("SA6333", 2);
				short itemMapID = msg.reader().readShort();
				short itemTemplateID = msg.reader().readShort();
				int x = msg.reader().readShort();
				int y = msg.reader().readShort();
				int num92 = msg.reader().readInt();
				short r = 0;
				if (num92 == -2)
					r = msg.reader().readShort();
				ItemMap o = new ItemMap(num92, itemMapID, itemTemplateID, x, y, r);
				GameScr.vItemMap.addElement(o);
				break;
			}
			case 69:
				GameCanvas.debug("SA633355", 2);
				Char.myCharz().arrItemBag[msg.reader().readByte()].quantity = msg.reader().readShort();
				break;
			case 81:
				GameCanvas.debug("SXX4", 2);
				((Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte())).isDisable = msg.reader().readBool();
				break;
			case 82:
				GameCanvas.debug("SXX5", 2);
				((Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte())).isDontMove = msg.reader().readBool();
				break;
			case 83:
			{
				GameCanvas.debug("SXX8", 2);
				int num73 = msg.reader().readInt();
				@char = ((num73 != Char.myCharz().charID) ? GameScr.findCharInMap(num73) : Char.myCharz());
				if (@char == null)
					return;
				Mob mobToAttack = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
				if (@char.mobMe != null)
					@char.mobMe.attackOtherMob(mobToAttack);
				break;
			}
			case 84:
			{
				int num73 = msg.reader().readInt();
				if (num73 == Char.myCharz().charID)
					@char = Char.myCharz();
				else
				{
					@char = GameScr.findCharInMap(num73);
					if (@char == null)
						return;
				}
				@char.cHP = @char.cHPFull;
				@char.cMP = @char.cMPFull;
				@char.cx = msg.reader().readShort();
				@char.cy = msg.reader().readShort();
				@char.liveFromDead();
				break;
			}
			case 85:
				GameCanvas.debug("SXX5", 2);
				((Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte())).isFire = msg.reader().readBool();
				break;
			case 86:
			{
				GameCanvas.debug("SXX5", 2);
				Mob mob5 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
				mob5.isIce = msg.reader().readBool();
				if (!mob5.isIce)
					ServerEffect.addServerEffect(77, mob5.x, mob5.y - 9, 1);
				break;
			}
			case 87:
				GameCanvas.debug("SXX5", 2);
				((Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte())).isWind = msg.reader().readBool();
				break;
			case 88:
			{
				string info2 = msg.reader().readUTF();
				short num72 = msg.reader().readShort();
				GameCanvas.inputDlg.show(info2, new Command(mResources.ACCEPT, GameCanvas.instance, 88818, num72), TField.INPUT_TYPE_ANY);
				break;
			}
			case 90:
				GameCanvas.debug("SA577", 2);
				requestItemPlayer(msg);
				break;
			case 92:
			{
				if (GameCanvas.currentScreen == GameScr.instance)
					GameCanvas.endDlg();
				string text = msg.reader().readUTF();
				string text2 = Res.changeString(msg.reader().readUTF());
				string empty = string.Empty;
				Char char2 = null;
				sbyte b15 = 0;
				if (!text.Equals(string.Empty))
				{
					char2 = new Char();
					char2.charID = msg.reader().readInt();
					char2.head = msg.reader().readShort();
					char2.body = msg.reader().readShort();
					char2.bag = msg.reader().readShort();
					char2.leg = msg.reader().readShort();
					b15 = msg.reader().readByte();
					char2.cName = text;
				}
				empty += text2;
				InfoDlg.hide();
				if (text.Equals(string.Empty))
				{
					GameScr.info1.addInfo(empty, 0);
					break;
				}
				GameScr.info2.addInfoWithChar(empty, char2, (b15 == 0) ? true : false);
				if (GameCanvas.panel.isShow && GameCanvas.panel.type == 8)
					GameCanvas.panel.initLogMessage();
				break;
			}
			case 94:
				GameCanvas.debug("SA3", 2);
				GameScr.info1.addInfo(msg.reader().readUTF(), 0);
				break;
			case -107:
			{
				sbyte b7 = msg.reader().readByte();
				if (b7 == 0)
					Char.myCharz().havePet = false;
				if (b7 == 1)
					Char.myCharz().havePet = true;
				if (b7 != 2)
					break;
				InfoDlg.hide();
				Char.myPetz().head = msg.reader().readShort();
				Char.myPetz().setDefaultPart();
				int num4 = msg.reader().readUnsignedByte();
				Res.outz("num body = " + num4);
				Char.myPetz().arrItemBody = new Item[num4];
				for (int num5 = 0; num5 < num4; num5++)
				{
					short num6 = msg.reader().readShort();
					Res.outz("template id= " + num6);
					if (num6 == -1)
						continue;
					Res.outz("1");
					Char.myPetz().arrItemBody[num5] = new Item();
					Char.myPetz().arrItemBody[num5].template = ItemTemplates.get(num6);
					int num7 = Char.myPetz().arrItemBody[num5].template.type;
					Char.myPetz().arrItemBody[num5].quantity = msg.reader().readInt();
					Res.outz("3");
					Char.myPetz().arrItemBody[num5].info = msg.reader().readUTF();
					Char.myPetz().arrItemBody[num5].content = msg.reader().readUTF();
					int num8 = msg.reader().readUnsignedByte();
					Res.outz("option size= " + num8);
					if (num8 != 0)
					{
						Char.myPetz().arrItemBody[num5].itemOption = new ItemOption[num8];
						for (int num9 = 0; num9 < Char.myPetz().arrItemBody[num5].itemOption.Length; num9++)
						{
							int optionTemplateId = msg.reader().readUnsignedByte();
							ushort param = msg.reader().readUnsignedShort();
							Char.myPetz().arrItemBody[num5].itemOption[num9] = new ItemOption(optionTemplateId, param);
						}
					}
					switch (num7)
					{
					case 0:
						Char.myPetz().body = Char.myPetz().arrItemBody[num5].template.part;
						break;
					case 1:
						Char.myPetz().leg = Char.myPetz().arrItemBody[num5].template.part;
						break;
					}
				}
				Char.myPetz().cHP = msg.readInt3Byte();
				Char.myPetz().cHPFull = msg.readInt3Byte();
				Char.myPetz().cMP = msg.readInt3Byte();
				Char.myPetz().cMPFull = msg.readInt3Byte();
				Char.myPetz().cDamFull = msg.readInt3Byte();
				Char.myPetz().cName = msg.reader().readUTF();
				Char.myPetz().currStrLevel = msg.reader().readUTF();
				Char.myPetz().cPower = msg.reader().readLong();
				Char.myPetz().cTiemNang = msg.reader().readLong();
				Char.myPetz().petStatus = msg.reader().readByte();
				Char.myPetz().cStamina = msg.reader().readShort();
				Char.myPetz().cMaxStamina = msg.reader().readShort();
				Char.myPetz().cCriticalFull = msg.reader().readByte();
				Char.myPetz().cDefull = msg.reader().readShort();
				Char.myPetz().arrPetSkill = new Skill[msg.reader().readByte()];
				Res.outz("SKILLENT = " + Char.myPetz().arrPetSkill);
				for (int num10 = 0; num10 < Char.myPetz().arrPetSkill.Length; num10++)
				{
					short num11 = msg.reader().readShort();
					if (num11 != -1)
					{
						Char.myPetz().arrPetSkill[num10] = Skills.get(num11);
						continue;
					}
					Char.myPetz().arrPetSkill[num10] = new Skill();
					Char.myPetz().arrPetSkill[num10].template = null;
					Char.myPetz().arrPetSkill[num10].moreInfo = msg.reader().readUTF();
				}
				break;
			}
			case -112:
			{
				sbyte b6 = msg.reader().readByte();
				if (b6 == 0)
					GameScr.findMobInMap(msg.reader().readByte()).clearBody();
				if (b6 == 1)
					GameScr.findMobInMap(msg.reader().readByte()).setBody(msg.reader().readShort());
				break;
			}
			case 112:
			{
				sbyte b = msg.reader().readByte();
				Res.outz("spec type= " + b);
				if (b == 0)
				{
					Panel.spearcialImage = msg.reader().readShort();
					Panel.specialInfo = msg.reader().readUTF();
				}
				else
				{
					if (b != 1)
						break;
					sbyte b2 = msg.reader().readByte();
					Char.myCharz().infoSpeacialSkill = new string[b2][];
					Char.myCharz().imgSpeacialSkill = new short[b2][];
					GameCanvas.panel.speacialTabName = new string[b2][];
					for (int i = 0; i < b2; i++)
					{
						GameCanvas.panel.speacialTabName[i] = new string[2];
						string[] array = Res.split(msg.reader().readUTF(), "\n", 0);
						if (array.Length == 2)
							GameCanvas.panel.speacialTabName[i] = array;
						if (array.Length == 1)
						{
							GameCanvas.panel.speacialTabName[i][0] = array[0];
							GameCanvas.panel.speacialTabName[i][1] = string.Empty;
						}
						int num2 = msg.reader().readByte();
						Char.myCharz().infoSpeacialSkill[i] = new string[num2];
						Char.myCharz().imgSpeacialSkill[i] = new short[num2];
						for (int j = 0; j < num2; j++)
						{
							Char.myCharz().imgSpeacialSkill[i][j] = msg.reader().readShort();
							Char.myCharz().infoSpeacialSkill[i][j] = msg.reader().readUTF();
						}
					}
					GameCanvas.panel.tabName[25] = GameCanvas.panel.speacialTabName;
					GameCanvas.panel.setTypeSpeacialSkill();
					GameCanvas.panel.show();
				}
				break;
			}
			}
			switch (msg.command)
			{
			case -17:
				GameCanvas.debug("SA88", 2);
				Char.myCharz().meDead = true;
				Char.myCharz().cPk = msg.reader().readByte();
				Char.myCharz().startDie(msg.reader().readShort(), msg.reader().readShort());
				try
				{
					Char.myCharz().cPower = msg.reader().readLong();
					Char.myCharz().applyCharLevelPercent();
				}
				catch (Exception)
				{
					Cout.println("Loi tai ME_DIE " + msg.command);
				}
				Char.myCharz().countKill = 0;
				break;
			case -16:
				GameCanvas.debug("SA90", 2);
				if (Char.myCharz().wdx != 0 || Char.myCharz().wdy != 0)
				{
					Char.myCharz().cx = Char.myCharz().wdx;
					Char.myCharz().cy = Char.myCharz().wdy;
					Char char15 = Char.myCharz();
					Char.myCharz().wdy = 0;
					char15.wdx = 0;
				}
				Char.myCharz().liveFromDead();
				Char.myCharz().isLockMove = false;
				Char.myCharz().meDead = false;
				break;
			case -13:
			{
				GameCanvas.debug("SA82", 2);
				int num175 = msg.reader().readUnsignedByte();
				if (num175 <= GameScr.vMob.size() - 1 && num175 >= 0)
				{
					Mob mob9 = (Mob)GameScr.vMob.elementAt(num175);
					mob9.sys = msg.reader().readByte();
					mob9.levelBoss = msg.reader().readByte();
					if (mob9.levelBoss != 0)
						mob9.typeSuperEff = Res.random(0, 3);
					mob9.x = mob9.xFirst;
					mob9.y = mob9.yFirst;
					mob9.status = 5;
					mob9.injureThenDie = false;
					mob9.hp = msg.reader().readInt();
					mob9.maxHp = mob9.hp;
					ServerEffect.addServerEffect(60, mob9.x, mob9.y, 1);
					break;
				}
				return;
			}
			case -12:
			{
				Res.outz("SERVER SEND MOB DIE");
				GameCanvas.debug("SA85", 2);
				Mob mob9 = null;
				try
				{
					mob9 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
				}
				catch (Exception)
				{
					Cout.println("LOi tai NPC_DIE cmd " + msg.command);
				}
				if (mob9 == null || mob9.status == 0 || mob9.status == 0)
					break;
				mob9.startDie();
				try
				{
					int num171 = msg.readInt3Byte();
					if (msg.reader().readBool())
						GameScr.startFlyText("-" + num171, mob9.x, mob9.y - mob9.h, 0, -2, mFont.FATAL);
					else
						GameScr.startFlyText("-" + num171, mob9.x, mob9.y - mob9.h, 0, -2, mFont.ORANGE);
					sbyte b66 = msg.reader().readByte();
					for (int num172 = 0; num172 < b66; num172++)
					{
						ItemMap itemMap3 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob9.x, mob9.y, msg.reader().readShort(), msg.reader().readShort());
						int num173 = (itemMap3.playerId = msg.reader().readInt());
						Res.outz("playerid= " + num173 + " my id= " + Char.myCharz().charID);
						GameScr.vItemMap.addElement(itemMap3);
						if (Res.abs(itemMap3.y - Char.myCharz().cy) < 24 && Res.abs(itemMap3.x - Char.myCharz().cx) < 24)
							Char.myCharz().charFocus = null;
					}
				}
				catch (Exception ex29)
				{
					Cout.println("LOi tai NPC_DIE " + ex29.ToString() + " cmd " + msg.command);
				}
				break;
			}
			case -11:
			{
				GameCanvas.debug("SA86", 2);
				Mob mob9 = null;
				try
				{
					byte index4 = msg.reader().readUnsignedByte();
					mob9 = (Mob)GameScr.vMob.elementAt(index4);
				}
				catch (Exception)
				{
					Cout.println("Loi tai NPC_ATTACK_ME " + msg.command);
				}
				if (mob9 != null)
				{
					Char.myCharz().isDie = false;
					Char.isLockKey = false;
					int num163 = msg.readInt3Byte();
					int num164;
					try
					{
						num164 = msg.readInt3Byte();
					}
					catch (Exception)
					{
						num164 = 0;
					}
					if (mob9.isBusyAttackSomeOne)
					{
						Char.myCharz().doInjure(num163, num164, false, true);
						break;
					}
					mob9.dame = num163;
					mob9.dameMp = num164;
					mob9.setAttack(Char.myCharz());
				}
				break;
			}
			case -10:
			{
				GameCanvas.debug("SA87", 2);
				Mob mob9 = null;
				try
				{
					mob9 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
				}
				catch (Exception)
				{
				}
				GameCanvas.debug("SA87x1", 2);
				if (mob9 != null)
				{
					GameCanvas.debug("SA87x2", 2);
					@char = GameScr.findCharInMap(msg.reader().readInt());
					if (@char == null)
						return;
					GameCanvas.debug("SA87x3", 2);
					int num167 = msg.readInt3Byte();
					mob9.dame = @char.cHP - num167;
					@char.cHPNew = num167;
					GameCanvas.debug("SA87x4", 2);
					try
					{
						@char.cMP = msg.readInt3Byte();
					}
					catch (Exception)
					{
					}
					GameCanvas.debug("SA87x5", 2);
					if (mob9.isBusyAttackSomeOne)
						@char.doInjure(mob9.dame, 0, false, true);
					else
						mob9.setAttack(@char);
					GameCanvas.debug("SA87x6", 2);
				}
				break;
			}
			case -9:
			{
				GameCanvas.debug("SA83", 2);
				Mob mob9 = null;
				try
				{
					mob9 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
				}
				catch (Exception)
				{
				}
				GameCanvas.debug("SA83v1", 2);
				if (mob9 != null)
				{
					mob9.hp = msg.readInt3Byte();
					int num177 = msg.readInt3Byte();
					if (num177 == 1)
						return;
					bool flag10 = false;
					try
					{
						flag10 = msg.reader().readBoolean();
					}
					catch (Exception)
					{
					}
					sbyte b68 = msg.reader().readByte();
					if (b68 != -1)
						EffecMn.addEff(new Effect(b68, mob9.x, mob9.getY(), 3, 1, -1));
					GameCanvas.debug("SA83v2", 2);
					if (flag10)
						GameScr.startFlyText("-" + num177, mob9.x, mob9.getY() - mob9.getH(), 0, -2, mFont.FATAL);
					else if (num177 == 0)
					{
						mob9.x = mob9.xFirst;
						mob9.y = mob9.yFirst;
						GameScr.startFlyText(mResources.miss, mob9.x, mob9.getY() - mob9.getH(), 0, -2, mFont.MISS);
					}
					else
					{
						GameScr.startFlyText("-" + num177, mob9.x, mob9.getY() - mob9.getH(), 0, -2, mFont.ORANGE);
					}
				}
				GameCanvas.debug("SA83v3", 2);
				break;
			}
			case -8:
				GameCanvas.debug("SA89", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
					return;
				@char.cPk = msg.reader().readByte();
				@char.waitToDie(msg.reader().readShort(), msg.reader().readShort());
				break;
			case -7:
			{
				GameCanvas.debug("SA80", 2);
				int num160 = msg.reader().readInt();
				Cout.println("RECEVED MOVE OF " + num160);
				for (int num161 = 0; num161 < GameScr.vCharInMap.size(); num161++)
				{
					Char char12 = null;
					try
					{
						char12 = (Char)GameScr.vCharInMap.elementAt(num161);
					}
					catch (Exception ex20)
					{
						Cout.println("Loi PLAYER_MOVE " + ex20.ToString());
					}
					if (char12 != null)
					{
						if (char12.charID == num160)
						{
							GameCanvas.debug("SA8x2y" + num161, 2);
							char12.moveTo(msg.reader().readShort(), msg.reader().readShort());
							char12.lastUpdateTime = mSystem.currentTimeMillis();
							break;
						}
						continue;
					}
					break;
				}
				GameCanvas.debug("SA80x3", 2);
				break;
			}
			case -6:
			{
				GameCanvas.debug("SA81", 2);
				int num160 = msg.reader().readInt();
				for (int num178 = 0; num178 < GameScr.vCharInMap.size(); num178++)
				{
					Char char16 = (Char)GameScr.vCharInMap.elementAt(num178);
					if (char16 != null && char16.charID == num160)
					{
						if (!char16.isInvisiblez && !char16.isUsePlane)
							ServerEffect.addServerEffect(60, char16.cx, char16.cy, 1);
						if (!char16.isUsePlane)
							GameScr.vCharInMap.removeElementAt(num178);
						return;
					}
				}
				break;
			}
			case -5:
			{
				GameCanvas.debug("SA79", 2);
				int charID = msg.reader().readInt();
				int num168 = msg.reader().readInt();
				Char char14;
				if (num168 != -100)
				{
					char14 = new Char();
					char14.charID = charID;
					char14.clanID = num168;
				}
				else
				{
					char14 = new Mabu();
					char14.charID = charID;
					char14.clanID = num168;
				}
				if (char14.clanID == -2)
					char14.isCopy = true;
				if (readCharInfo(char14, msg))
				{
					sbyte b64 = msg.reader().readByte();
					if (char14.cy <= 10 && b64 != 0 && b64 != 2)
					{
						Res.outz("nhân vật bay trên trời xuống x= " + char14.cx + " y= " + char14.cy);
						Teleport teleport2 = new Teleport(char14.cx, char14.cy, char14.head, char14.cdir, 1, false, (b64 != 1) ? b64 : char14.cgender);
						teleport2.id = char14.charID;
						char14.isTeleport = true;
						Teleport.addTeleport(teleport2);
					}
					if (b64 == 2)
						char14.show();
					for (int num169 = 0; num169 < GameScr.vMob.size(); num169++)
					{
						Mob mob10 = (Mob)GameScr.vMob.elementAt(num169);
						if (mob10 != null && mob10.isMobMe && mob10.mobId == char14.charID)
						{
							Res.outz("co 1 con quai");
							char14.mobMe = mob10;
							char14.mobMe.x = char14.cx;
							char14.mobMe.y = char14.cy - 40;
							break;
						}
					}
					if (GameScr.findCharInMap(char14.charID) == null)
						GameScr.vCharInMap.addElement(char14);
					char14.isMonkey = msg.reader().readByte();
					short num170 = msg.reader().readShort();
					Res.outz("mount id= " + num170 + "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
					if (num170 != -1)
					{
						char14.isHaveMount = true;
						switch (num170)
						{
						case 396:
							char14.isEventMount = true;
							break;
						case 532:
							char14.isSpeacialMount = true;
							break;
						default:
							if (num170 >= Char.ID_NEW_MOUNT)
								char14.idMount = num170;
							break;
						case 349:
						case 350:
						case 351:
							char14.isMountVip = true;
							break;
						case 346:
						case 347:
						case 348:
							char14.isMountVip = false;
							break;
						}
					}
					else
						char14.isHaveMount = false;
				}
				sbyte b65 = msg.reader().readByte();
				Res.outz("addplayer:   " + b65);
				char14.cFlag = b65;
				char14.isNhapThe = msg.reader().readByte() == 1;
				GameScr.gI().getFlagImage(char14.charID, char14.cFlag);
				break;
			}
			case -75:
			{
				Mob mob9 = null;
				try
				{
					mob9 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
				}
				catch (Exception)
				{
				}
				if (mob9 != null)
				{
					mob9.levelBoss = msg.reader().readByte();
					if (mob9.levelBoss > 0)
						mob9.typeSuperEff = Res.random(0, 3);
				}
				break;
			}
			case 74:
			{
				GameCanvas.debug("SA85", 2);
				Mob mob9 = null;
				try
				{
					mob9 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
				}
				catch (Exception)
				{
					Cout.println("Loi tai NPC CHANGE " + msg.command);
				}
				if (mob9 != null && mob9.status != 0 && mob9.status != 0)
				{
					mob9.status = 0;
					ServerEffect.addServerEffect(60, mob9.x, mob9.y, 1);
					ItemMap itemMap4 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob9.x, mob9.y, msg.reader().readShort(), msg.reader().readShort());
					GameScr.vItemMap.addElement(itemMap4);
					if (Res.abs(itemMap4.y - Char.myCharz().cy) < 24 && Res.abs(itemMap4.x - Char.myCharz().cx) < 24)
						Char.myCharz().charFocus = null;
				}
				break;
			}
			case 66:
				Res.outz("ME DIE XP DOWN NOT IMPLEMENT YET!!!!!!!!!!!!!!!!!!!!!!!!!!");
				break;
			case 45:
			{
				GameCanvas.debug("SA84", 2);
				Mob mob9 = null;
				try
				{
					mob9 = (Mob)GameScr.vMob.elementAt(msg.reader().readUnsignedByte());
				}
				catch (Exception ex27)
				{
					Cout.println("Loi tai NPC_MISS  " + ex27.ToString());
				}
				if (mob9 != null)
				{
					mob9.hp = msg.reader().readInt();
					GameScr.startFlyText(mResources.miss, mob9.x, mob9.y - mob9.h, 0, -2, mFont.MISS);
				}
				break;
			}
			case 44:
			{
				GameCanvas.debug("SA91", 2);
				int num166 = msg.reader().readInt();
				string text9 = msg.reader().readUTF();
				Res.outz("user id= " + num166 + " text= " + text9);
				@char = ((Char.myCharz().charID != num166) ? GameScr.findCharInMap(num166) : Char.myCharz());
				if (@char == null)
					return;
				@char.addInfo(text9);
				break;
			}
			case 19:
				Char.myCharz().countKill = msg.reader().readUnsignedShort();
				Char.myCharz().countKillMax = msg.reader().readUnsignedShort();
				break;
			case 18:
			{
				sbyte b63 = msg.reader().readByte();
				for (int num165 = 0; num165 < b63; num165++)
				{
					int charId = msg.reader().readInt();
					int cx = msg.reader().readShort();
					int cy = msg.reader().readShort();
					int cHPShow = msg.readInt3Byte();
					Char char13 = GameScr.findCharInMap(charId);
					if (char13 != null)
					{
						char13.cx = cx;
						char13.cy = cy;
						char13.cHP = (char13.cHPShow = cHPShow);
						char13.lastUpdateTime = mSystem.currentTimeMillis();
					}
				}
				break;
			}
			case -73:
			{
				sbyte b61 = msg.reader().readByte();
				for (int num159 = 0; num159 < GameScr.vNpc.size(); num159++)
				{
					Npc npc7 = (Npc)GameScr.vNpc.elementAt(num159);
					if (npc7.template.npcTemplateId == b61)
					{
						if (msg.reader().readByte() == 0)
							npc7.isHide = true;
						else
							npc7.isHide = false;
						break;
					}
				}
				break;
			}
			case 95:
			{
				GameCanvas.debug("SA77", 22);
				int num176 = msg.reader().readInt();
				Char.myCharz().xu += num176;
				GameScr.startFlyText((num176 <= 0) ? (string.Empty + num176) : ("+" + num176), Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
				break;
			}
			case 96:
				GameCanvas.debug("SA77a", 22);
				Char.myCharz().taskOrders.addElement(new TaskOrder(msg.reader().readByte(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readByte()));
				break;
			case 97:
			{
				sbyte b67 = msg.reader().readByte();
				for (int num174 = 0; num174 < Char.myCharz().taskOrders.size(); num174++)
				{
					TaskOrder taskOrder = (TaskOrder)Char.myCharz().taskOrders.elementAt(num174);
					if (taskOrder.taskId == b67)
					{
						taskOrder.count = msg.reader().readShort();
						break;
					}
				}
				break;
			}
			case -3:
			{
				GameCanvas.debug("SA78", 2);
				sbyte b62 = msg.reader().readByte();
				int num162 = msg.reader().readInt();
				if (b62 == 0)
					Char.myCharz().cPower += num162;
				if (b62 == 1)
					Char.myCharz().cTiemNang += num162;
				if (b62 == 2)
				{
					Char.myCharz().cPower += num162;
					Char.myCharz().cTiemNang += num162;
				}
				Char.myCharz().applyCharLevelPercent();
				if (Char.myCharz().cTypePk != 3)
				{
					GameScr.startFlyText(((num162 <= 0) ? string.Empty : "+") + num162, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch, 0, -4, mFont.GREEN);
					if (num162 > 0 && Char.myCharz().petFollow != null && Char.myCharz().petFollow.smallID == 5002)
					{
						ServerEffect.addServerEffect(55, Char.myCharz().petFollow.cmx, Char.myCharz().petFollow.cmy, 1);
						ServerEffect.addServerEffect(55, Char.myCharz().cx, Char.myCharz().cy, 1);
					}
				}
				break;
			}
			case -2:
			{
				GameCanvas.debug("SA77", 22);
				int num158 = msg.reader().readInt();
				Char.myCharz().yen += num158;
				GameScr.startFlyText((num158 <= 0) ? (string.Empty + num158) : ("+" + num158), Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
				break;
			}
			case -1:
			{
				GameCanvas.debug("SA77", 222);
				int num157 = msg.reader().readInt();
				Char.myCharz().xu += num157;
				Char.myCharz().yen -= num157;
				GameScr.startFlyText("+" + num157, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
				break;
			}
			}
			GameCanvas.debug("SA92", 2);
		}
		catch (Exception)
		{
		}
		finally
		{
			msg?.cleanup();
		}
	}

	private void createItem(myReader d)
	{
		GameScr.vcItem = d.readByte();
		ItemTemplates.itemTemplates.clear();
		GameScr.gI().iOptionTemplates = new ItemOptionTemplate[d.readUnsignedByte()];
		for (int i = 0; i < GameScr.gI().iOptionTemplates.Length; i++)
		{
			GameScr.gI().iOptionTemplates[i] = new ItemOptionTemplate();
			GameScr.gI().iOptionTemplates[i].id = i;
			GameScr.gI().iOptionTemplates[i].name = d.readUTF();
			GameScr.gI().iOptionTemplates[i].type = d.readByte();
		}
		int num = d.readShort();
		for (int j = 0; j < num; j++)
		{
			ItemTemplates.add(new ItemTemplate((short)j, d.readByte(), d.readByte(), d.readUTF(), d.readUTF(), d.readByte(), d.readInt(), d.readShort(), d.readShort(), d.readBool()));
		}
	}

	private void createSkill(myReader d)
	{
		GameScr.vcSkill = d.readByte();
		GameScr.gI().sOptionTemplates = new SkillOptionTemplate[d.readByte()];
		for (int i = 0; i < GameScr.gI().sOptionTemplates.Length; i++)
		{
			GameScr.gI().sOptionTemplates[i] = new SkillOptionTemplate();
			GameScr.gI().sOptionTemplates[i].id = i;
			GameScr.gI().sOptionTemplates[i].name = d.readUTF();
		}
		GameScr.nClasss = new NClass[d.readByte()];
		for (int j = 0; j < GameScr.nClasss.Length; j++)
		{
			GameScr.nClasss[j] = new NClass();
			GameScr.nClasss[j].classId = j;
			GameScr.nClasss[j].name = d.readUTF();
			GameScr.nClasss[j].skillTemplates = new SkillTemplate[d.readByte()];
			for (int k = 0; k < GameScr.nClasss[j].skillTemplates.Length; k++)
			{
				GameScr.nClasss[j].skillTemplates[k] = new SkillTemplate();
				GameScr.nClasss[j].skillTemplates[k].id = d.readByte();
				GameScr.nClasss[j].skillTemplates[k].name = d.readUTF();
				GameScr.nClasss[j].skillTemplates[k].maxPoint = d.readByte();
				GameScr.nClasss[j].skillTemplates[k].manaUseType = d.readByte();
				GameScr.nClasss[j].skillTemplates[k].type = d.readByte();
				GameScr.nClasss[j].skillTemplates[k].iconId = d.readShort();
				GameScr.nClasss[j].skillTemplates[k].damInfo = d.readUTF();
				int lineWidth = 130;
				if (GameCanvas.w == 128 || GameCanvas.h <= 208)
					lineWidth = 100;
				GameScr.nClasss[j].skillTemplates[k].description = mFont.tahoma_7_green2.splitFontArray(d.readUTF(), lineWidth);
				GameScr.nClasss[j].skillTemplates[k].skills = new Skill[d.readByte()];
				for (int l = 0; l < GameScr.nClasss[j].skillTemplates[k].skills.Length; l++)
				{
					GameScr.nClasss[j].skillTemplates[k].skills[l] = new Skill();
					GameScr.nClasss[j].skillTemplates[k].skills[l].skillId = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].template = GameScr.nClasss[j].skillTemplates[k];
					GameScr.nClasss[j].skillTemplates[k].skills[l].point = d.readByte();
					GameScr.nClasss[j].skillTemplates[k].skills[l].powRequire = d.readLong();
					GameScr.nClasss[j].skillTemplates[k].skills[l].manaUse = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].coolDown = d.readInt();
					GameScr.nClasss[j].skillTemplates[k].skills[l].dx = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].dy = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].maxFight = d.readByte();
					GameScr.nClasss[j].skillTemplates[k].skills[l].damage = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].price = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].moreInfo = d.readUTF();
					Skills.add(GameScr.nClasss[j].skillTemplates[k].skills[l]);
				}
			}
		}
	}

	private void createMap(myReader d)
	{
		GameScr.vcMap = d.readByte();
		TileMap.mapNames = new string[d.readUnsignedByte()];
		for (int i = 0; i < TileMap.mapNames.Length; i++)
		{
			TileMap.mapNames[i] = d.readUTF();
		}
		Npc.arrNpcTemplate = new NpcTemplate[d.readByte()];
		for (sbyte b = 0; b < Npc.arrNpcTemplate.Length; b = (sbyte)(b + 1))
		{
			Npc.arrNpcTemplate[b] = new NpcTemplate();
			Npc.arrNpcTemplate[b].npcTemplateId = b;
			Npc.arrNpcTemplate[b].name = d.readUTF();
			Npc.arrNpcTemplate[b].headId = d.readShort();
			Npc.arrNpcTemplate[b].bodyId = d.readShort();
			Npc.arrNpcTemplate[b].legId = d.readShort();
			Npc.arrNpcTemplate[b].menu = new string[d.readByte()][];
			for (int j = 0; j < Npc.arrNpcTemplate[b].menu.Length; j++)
			{
				Npc.arrNpcTemplate[b].menu[j] = new string[d.readByte()];
				for (int k = 0; k < Npc.arrNpcTemplate[b].menu[j].Length; k++)
				{
					Npc.arrNpcTemplate[b].menu[j][k] = d.readUTF();
				}
			}
		}
		Mob.arrMobTemplate = new MobTemplate[d.readByte()];
		for (sbyte b2 = 0; b2 < Mob.arrMobTemplate.Length; b2 = (sbyte)(b2 + 1))
		{
			Mob.arrMobTemplate[b2] = new MobTemplate();
			Mob.arrMobTemplate[b2].mobTemplateId = b2;
			Mob.arrMobTemplate[b2].type = d.readByte();
			Mob.arrMobTemplate[b2].name = d.readUTF();
			Mob.arrMobTemplate[b2].hp = d.readInt();
			Mob.arrMobTemplate[b2].rangeMove = d.readByte();
			Mob.arrMobTemplate[b2].speed = d.readByte();
			Mob.arrMobTemplate[b2].dartType = d.readByte();
		}
	}

	private void createData(myReader d, bool isSaveRMS)
	{
		GameScr.vcData = d.readByte();
		if (isSaveRMS)
		{
			Rms.saveRMS("NR_dart", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_arrow", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_effect", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_image", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_part", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_skill", NinjaUtil.readByteArray(d));
			Rms.DeleteStorage("NRdata");
		}
	}

	private Image createImage(sbyte[] arr)
	{
		try
		{
			return Image.createImage(arr, 0, arr.Length);
		}
		catch (Exception)
		{
		}
		return null;
	}

	public int[] arrayByte2Int(sbyte[] b)
	{
		int[] array = new int[b.Length];
		for (int i = 0; i < b.Length; i++)
		{
			int num = b[i];
			if (num < 0)
				num += 256;
			array[i] = num;
		}
		return array;
	}

	public void readClanMsg(Message msg, int index)
	{
		try
		{
			ClanMessage clanMessage = new ClanMessage();
			sbyte b = msg.reader().readByte();
			clanMessage.type = b;
			clanMessage.id = msg.reader().readInt();
			clanMessage.playerId = msg.reader().readInt();
			clanMessage.playerName = msg.reader().readUTF();
			clanMessage.role = msg.reader().readByte();
			clanMessage.time = msg.reader().readInt() + 1000000000;
			bool upToTop = false;
			GameScr.isNewClanMessage = false;
			if (b == 0)
			{
				string text = msg.reader().readUTF();
				GameScr.isNewClanMessage = true;
				if (mFont.tahoma_7.getWidth(text) > Panel.WIDTH_PANEL - 60)
					clanMessage.chat = mFont.tahoma_7.splitFontArray(text, Panel.WIDTH_PANEL - 10);
				else
				{
					clanMessage.chat = new string[1];
					clanMessage.chat[0] = text;
				}
				clanMessage.color = msg.reader().readByte();
			}
			else if (b == 1)
			{
				clanMessage.recieve = msg.reader().readByte();
				clanMessage.maxCap = msg.reader().readByte();
				if (upToTop = msg.reader().readByte() == 1)
					GameScr.isNewClanMessage = true;
				if (clanMessage.playerId != Char.myCharz().charID)
				{
					if (clanMessage.recieve < clanMessage.maxCap)
						clanMessage.option = new string[1] { mResources.donate };
					else
						clanMessage.option = null;
				}
				if (GameCanvas.panel.cp != null)
					GameCanvas.panel.updateRequest(clanMessage.recieve, clanMessage.maxCap);
			}
			else if (b == 2 && Char.myCharz().role == 0)
			{
				GameScr.isNewClanMessage = true;
				clanMessage.option = new string[2]
				{
					mResources.CANCEL,
					mResources.receive
				};
			}
			if (GameCanvas.currentScreen != GameScr.instance)
				GameScr.isNewClanMessage = false;
			else if (GameCanvas.panel.isShow && GameCanvas.panel.type == 0 && GameCanvas.panel.currentTabIndex == 3)
			{
				GameScr.isNewClanMessage = false;
			}
			ClanMessage.addMessage(clanMessage, index, upToTop);
		}
		catch (Exception)
		{
			Cout.println("LOI TAI CMD -= " + msg.command);
		}
	}

	public void loadCurrMap(sbyte teleport3)
	{
		Res.outz("is loading map = " + Char.isLoadingMap);
		GameScr.gI().auto = 0;
		GameScr.isChangeZone = false;
		CreateCharScr.instance = null;
		GameScr.info1.isUpdate = false;
		GameScr.info2.isUpdate = false;
		GameScr.lockTick = 0;
		GameCanvas.panel.isShow = false;
		SoundMn.gI().stopAll();
		if (!GameScr.isLoadAllData && !CreateCharScr.isCreateChar)
			GameScr.gI().initSelectChar();
		GameScr.loadCamera(false, (teleport3 != 1) ? (-1) : Char.myCharz().cx, (teleport3 == 0) ? (-1) : 0);
		TileMap.loadMainTile();
		TileMap.loadMap(TileMap.tileID);
		Res.outz("LOAD GAMESCR 2");
		Char.myCharz().cvx = 0;
		Char.myCharz().statusMe = 4;
		Char.myCharz().currentMovePoint = null;
		Char.myCharz().mobFocus = null;
		Char.myCharz().charFocus = null;
		Char.myCharz().npcFocus = null;
		Char.myCharz().itemFocus = null;
		Char.myCharz().skillPaint = null;
		Char.myCharz().setMabuHold(false);
		Char.myCharz().skillPaintRandomPaint = null;
		GameCanvas.clearAllPointerEvent();
		if (Char.myCharz().cy >= TileMap.pxh - 100)
		{
			Char.myCharz().isFlyUp = true;
			Char.myCharz().cx += Res.abs(Res.random(0, 80));
			Service.gI().charMove();
		}
		GameScr.gI().loadGameScr();
		GameCanvas.loadBG(TileMap.bgID);
		Char.isLockKey = false;
		Res.outz("cy= " + Char.myCharz().cy + "---------------------------------------------");
		for (int i = 0; i < Char.myCharz().vEff.size(); i++)
		{
			if (((EffectChar)Char.myCharz().vEff.elementAt(i)).template.type == 10)
			{
				Char.isLockKey = true;
				break;
			}
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
		GameScr.gI().dHP = Char.myCharz().cHP;
		GameScr.gI().dMP = Char.myCharz().cMP;
		Char.ischangingMap = false;
		GameScr.gI().switchToMe();
		if (Char.myCharz().cy <= 10 && teleport3 != 0 && teleport3 != 2)
		{
			Teleport.addTeleport(new Teleport(Char.myCharz().cx, Char.myCharz().cy, Char.myCharz().head, Char.myCharz().cdir, 1, true, (teleport3 != 1) ? teleport3 : Char.myCharz().cgender));
			Char.myCharz().isTeleport = true;
		}
		if (teleport3 == 2)
			Char.myCharz().show();
		if (GameScr.gI().isRongThanXuatHien)
		{
			if (TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID)
				GameScr.gI().callRongThan(GameScr.gI().xR, GameScr.gI().yR);
			if (mGraphics.zoomLevel > 1)
				GameScr.gI().doiMauTroi();
		}
		InfoDlg.hide();
		InfoDlg.show(TileMap.mapName, mResources.zone + " " + TileMap.zoneID, 30);
		GameCanvas.endDlg();
		GameCanvas.isLoading = false;
		Hint.clickMob();
		Hint.clickNpc();
		GameCanvas.debug("SA75x9", 2);
	}

	public void loadInfoMap(Message msg)
	{
		try
		{
			if (mGraphics.zoomLevel == 1)
				SmallImage.clearHastable();
			Char.myCharz().cx = (Char.myCharz().cxSend = (Char.myCharz().cxFocus = msg.reader().readShort()));
			Char.myCharz().cy = (Char.myCharz().cySend = (Char.myCharz().cyFocus = msg.reader().readShort()));
			Char.myCharz().xSd = Char.myCharz().cx;
			Char.myCharz().ySd = Char.myCharz().cy;
			Res.outz("head= " + Char.myCharz().head + " body= " + Char.myCharz().body + " left= " + Char.myCharz().leg + " x= " + Char.myCharz().cx + " y= " + Char.myCharz().cy + " chung toc= " + Char.myCharz().cgender);
			if (Char.myCharz().cx >= 0 && Char.myCharz().cx <= 100)
				Char.myCharz().cdir = 1;
			else if (Char.myCharz().cx >= TileMap.tmw - 100 && Char.myCharz().cx <= TileMap.tmw)
			{
				Char.myCharz().cdir = -1;
			}
			GameCanvas.debug("SA75x4", 2);
			int num = msg.reader().readByte();
			Res.outz("vGo size= " + num);
			if (!GameScr.info1.isDone)
			{
				GameScr.info1.cmx = Char.myCharz().cx - GameScr.cmx;
				GameScr.info1.cmy = Char.myCharz().cy - GameScr.cmy;
			}
			for (int i = 0; i < num; i++)
			{
				Waypoint waypoint = new Waypoint(msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readUTF());
				if ((TileMap.mapID != 21 && TileMap.mapID != 22 && TileMap.mapID != 23) || waypoint.minX < 0 || waypoint.minX <= 24)
					;
			}
			Resources.UnloadUnusedAssets();
			GC.Collect();
			GameCanvas.debug("SA75x5", 2);
			num = msg.reader().readByte();
			Mob.newMob.removeAllElements();
			for (sbyte b = 0; b < num; b = (sbyte)(b + 1))
			{
				Mob mob = new Mob(b, msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readByte(), msg.reader().readByte(), msg.reader().readInt(), msg.reader().readByte(), msg.reader().readInt(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readByte(), msg.reader().readByte());
				mob.xSd = mob.x;
				mob.ySd = mob.y;
				mob.isBoss = msg.reader().readBoolean();
				if (Mob.arrMobTemplate[mob.templateId].type != 0)
				{
					if (b % 3 == 0)
						mob.dir = -1;
					else
						mob.dir = 1;
					mob.x += 10 - b % 20;
				}
				mob.isMobMe = false;
				BigBoss bigBoss = null;
				BachTuoc bachTuoc = null;
				BigBoss2 bigBoss2 = null;
				if (mob.templateId == 70)
					bigBoss = new BigBoss(b, (short)mob.x, (short)mob.y, 70, mob.hp, mob.maxHp, mob.sys);
				if (mob.templateId == 71)
					bachTuoc = new BachTuoc(b, (short)mob.x, (short)mob.y, 71, mob.hp, mob.maxHp, mob.sys);
				if (mob.templateId == 72)
					bigBoss2 = new BigBoss2(b, (short)mob.x, (short)mob.y, 72, mob.hp, mob.maxHp, 3);
				if (bigBoss != null)
					GameScr.vMob.addElement(bigBoss);
				else if (bachTuoc != null)
				{
					GameScr.vMob.addElement(bachTuoc);
				}
				else if (bigBoss2 != null)
				{
					GameScr.vMob.addElement(bigBoss2);
				}
				else
				{
					GameScr.vMob.addElement(mob);
				}
			}
			for (int j = 0; j < Mob.lastMob.size(); j++)
			{
				string text = (string)Mob.lastMob.elementAt(j);
				if (!Mob.isExistNewMob(text))
				{
					Mob.arrMobTemplate[int.Parse(text)].data = null;
					Mob.lastMob.removeElementAt(j);
					j--;
				}
			}
			if (Char.myCharz().mobMe != null && GameScr.findMobInMap(Char.myCharz().mobMe.mobId) == null)
			{
				Char.myCharz().mobMe.getData();
				Char.myCharz().mobMe.x = Char.myCharz().cx;
				Char.myCharz().mobMe.y = Char.myCharz().cy - 40;
				GameScr.vMob.addElement(Char.myCharz().mobMe);
			}
			num = msg.reader().readByte();
			for (byte b2 = 0; b2 < num; b2 = (byte)(b2 + 1))
			{
			}
			GameCanvas.debug("SA75x6", 2);
			num = msg.reader().readByte();
			Res.outz("NPC size= " + num);
			for (int k = 0; k < num; k++)
			{
				sbyte b3 = msg.reader().readByte();
				short cx = msg.reader().readShort();
				short num2 = msg.reader().readShort();
				sbyte b4 = msg.reader().readByte();
				short num3 = msg.reader().readShort();
				if (b4 != 6 && ((Char.myCharz().taskMaint.taskId >= 7 && (Char.myCharz().taskMaint.taskId != 7 || Char.myCharz().taskMaint.index > 1)) || (b4 != 7 && b4 != 8 && b4 != 9)) && (Char.myCharz().taskMaint.taskId >= 6 || b4 != 16))
				{
					if (b4 == 4)
					{
						GameScr.gI().magicTree = new MagicTree(k, b3, cx, num2, b4, num3);
						Service.gI().magicTree(2);
						GameScr.vNpc.addElement(GameScr.gI().magicTree);
					}
					else
					{
						Npc o = new Npc(k, b3, cx, num2 + 3, b4, num3);
						GameScr.vNpc.addElement(o);
					}
				}
			}
			GameCanvas.debug("SA75x7", 2);
			num = msg.reader().readByte();
			Res.outz("item size = " + num);
			for (int l = 0; l < num; l++)
			{
				short itemMapID = msg.reader().readShort();
				short itemTemplateID = msg.reader().readShort();
				int x = msg.reader().readShort();
				int y = msg.reader().readShort();
				int num4 = msg.reader().readInt();
				short r = 0;
				if (num4 == -2)
					r = msg.reader().readShort();
				ItemMap itemMap = new ItemMap(num4, itemMapID, itemTemplateID, x, y, r);
				bool flag = false;
				for (int m = 0; m < GameScr.vItemMap.size(); m++)
				{
					if (((ItemMap)GameScr.vItemMap.elementAt(m)).itemMapID == itemMap.itemMapID)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
					GameScr.vItemMap.addElement(itemMap);
			}
			if (GameCanvas.lowGraphic && (!GameCanvas.lowGraphic || (TileMap.mapID != 51 && TileMap.mapID != 103)))
			{
				short num5 = msg.reader().readShort();
				for (int n = 0; n < num5; n++)
				{
					msg.reader().readShort();
					msg.reader().readShort();
					msg.reader().readShort();
				}
				short num6 = msg.reader().readShort();
				for (int num7 = 0; num7 < num6; num7++)
				{
					msg.reader().readUTF();
					msg.reader().readUTF();
				}
			}
			else
			{
				short num8 = msg.reader().readShort();
				TileMap.vCurrItem.removeAllElements();
				if (mGraphics.zoomLevel == 1)
					BgItem.clearHashTable();
				BgItem.vKeysNew.removeAllElements();
				Res.outz("nItem= " + num8);
				for (int num9 = 0; num9 < num8; num9++)
				{
					short id = msg.reader().readShort();
					short num10 = msg.reader().readShort();
					short num11 = msg.reader().readShort();
					if (TileMap.getBIById(id) == null)
						continue;
					BgItem bIById = TileMap.getBIById(id);
					BgItem bgItem = new BgItem();
					bgItem.id = id;
					bgItem.idImage = bIById.idImage;
					bgItem.dx = bIById.dx;
					bgItem.dy = bIById.dy;
					bgItem.x = num10 * TileMap.size;
					bgItem.y = num11 * TileMap.size;
					bgItem.layer = bIById.layer;
					if (TileMap.isExistMoreOne(bgItem.id))
					{
						bgItem.trans = ((num9 % 2 != 0) ? 2 : 0);
						if (TileMap.mapID == 45)
							bgItem.trans = 0;
					}
					Image image = null;
					if (!BgItem.imgNew.containsKey(bgItem.idImage + string.Empty))
					{
						if (mGraphics.zoomLevel == 1)
						{
							image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
							if (image == null)
							{
								image = Image.createRGBImage(new int[1], 1, 1, true);
								Service.gI().getBgTemplate(bgItem.idImage);
							}
							BgItem.imgNew.put(bgItem.idImage + string.Empty, image);
						}
						else
						{
							bool flag2 = false;
							sbyte[] array = Rms.loadRMS(mGraphics.zoomLevel + "bgItem" + bgItem.idImage);
							if (array != null)
							{
								if (BgItem.newSmallVersion != null)
								{
									Res.outz("Small  last= " + array.Length % 127 + "new Version= " + BgItem.newSmallVersion[bgItem.idImage]);
									if (array.Length % 127 != BgItem.newSmallVersion[bgItem.idImage])
										flag2 = true;
								}
								if (!flag2)
								{
									image = Image.createImage(array, 0, array.Length);
									if (image != null)
										BgItem.imgNew.put(bgItem.idImage + string.Empty, image);
									else
										flag2 = true;
								}
							}
							else
								flag2 = true;
							if (flag2)
							{
								image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage + ".png");
								if (image == null)
								{
									image = Image.createRGBImage(new int[1], 1, 1, true);
									Service.gI().getBgTemplate(bgItem.idImage);
								}
								BgItem.imgNew.put(bgItem.idImage + string.Empty, image);
							}
						}
						BgItem.vKeysLast.addElement(bgItem.idImage + string.Empty);
					}
					if (!BgItem.isExistKeyNews(bgItem.idImage + string.Empty))
						BgItem.vKeysNew.addElement(bgItem.idImage + string.Empty);
					bgItem.changeColor();
					TileMap.vCurrItem.addElement(bgItem);
				}
				for (int num12 = 0; num12 < BgItem.vKeysLast.size(); num12++)
				{
					string text2 = (string)BgItem.vKeysLast.elementAt(num12);
					if (!BgItem.isExistKeyNews(text2))
					{
						BgItem.imgNew.remove(text2);
						if (BgItem.imgNew.containsKey(text2 + "blend" + 1))
							BgItem.imgNew.remove(text2 + "blend" + 1);
						if (BgItem.imgNew.containsKey(text2 + "blend" + 3))
							BgItem.imgNew.remove(text2 + "blend" + 3);
						BgItem.vKeysLast.removeElementAt(num12);
						num12--;
					}
				}
				BackgroudEffect.isFog = false;
				BackgroudEffect.nCloud = 0;
				EffecMn.vEff.removeAllElements();
				BackgroudEffect.vBgEffect.removeAllElements();
				Effect.newEff.removeAllElements();
				short num13 = msg.reader().readShort();
				for (int num14 = 0; num14 < num13; num14++)
				{
					keyValueAction(msg.reader().readUTF(), msg.reader().readUTF());
				}
				for (int num15 = 0; num15 < Effect.lastEff.size(); num15++)
				{
					string text3 = (string)Effect.lastEff.elementAt(num15);
					if (!Effect.isExistNewEff(text3))
					{
						Effect.removeEffData(int.Parse(text3));
						Effect.lastEff.removeElementAt(num15);
						num15--;
					}
				}
			}
			TileMap.bgType = msg.reader().readByte();
			loadCurrMap(msg.reader().readByte());
			Char.isLoadingMap = false;
			GameCanvas.debug("SA75x8", 2);
			Resources.UnloadUnusedAssets();
			GC.Collect();
			Cout.LogError("----------DA CHAY XONG LOAD INFO MAP");
		}
		catch (Exception ex)
		{
			Cout.LogError("LOI TAI LOADMAP INFO " + ex.ToString());
		}
	}

	public void keyValueAction(string key, string value)
	{
		if (key.Equals("eff"))
		{
			if (Panel.graphics > 0)
				return;
			string[] array = Res.split(value, ".", 0);
			int id = int.Parse(array[0]);
			int layer = int.Parse(array[1]);
			int x = int.Parse(array[2]);
			int y = int.Parse(array[3]);
			int loop;
			int loopCount;
			if (array.Length <= 4)
			{
				loop = -1;
				loopCount = 1;
			}
			else
			{
				loop = int.Parse(array[4]);
				loopCount = int.Parse(array[5]);
			}
			Effect effect = new Effect(id, x, y, layer, loop, loopCount);
			if (array.Length > 6)
			{
				effect.typeEff = int.Parse(array[6]);
				if (array.Length > 7)
				{
					effect.indexFrom = int.Parse(array[7]);
					effect.indexTo = int.Parse(array[8]);
				}
			}
			EffecMn.addEff(effect);
		}
		else if (key.Equals("beff") && Panel.graphics <= 1)
		{
			BackgroudEffect.addEffect(int.Parse(value));
		}
	}

	public void messageNotMap(Message msg)
	{
		GameCanvas.debug("SA6", 2);
		try
		{
			switch (msg.reader().readByte())
			{
			case 4:
			{
				GameCanvas.debug("SA8", 2);
				GameCanvas.loginScr.savePass();
				GameScr.isAutoPlay = false;
				GameScr.canAutoPlay = false;
				LoginScr.isUpdateAll = true;
				LoginScr.isUpdateData = true;
				LoginScr.isUpdateMap = true;
				LoginScr.isUpdateSkill = true;
				LoginScr.isUpdateItem = true;
				GameScr.vsData = msg.reader().readByte();
				GameScr.vsMap = msg.reader().readByte();
				GameScr.vsSkill = msg.reader().readByte();
				GameScr.vsItem = msg.reader().readByte();
				msg.reader().readByte();
				if (GameCanvas.loginScr.isLogin2)
				{
					Rms.saveRMSString("acc", string.Empty);
					Rms.saveRMSString("pass", string.Empty);
				}
				else
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, string.Empty);
				if (GameScr.vsData != GameScr.vcData)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateData();
				}
				else
					try
					{
						LoginScr.isUpdateData = false;
					}
					catch (Exception)
					{
						GameScr.vcData = -1;
						Service.gI().updateData();
					}
				if (GameScr.vsMap != GameScr.vcMap)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateMap();
				}
				else
					try
					{
						if (!GameScr.isLoadAllData)
							createMap(new DataInputStream(Rms.loadRMS("NRmap")).r);
						LoginScr.isUpdateMap = false;
					}
					catch (Exception)
					{
						GameScr.vcMap = -1;
						Service.gI().updateMap();
					}
				if (GameScr.vsSkill != GameScr.vcSkill)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateSkill();
				}
				else
					try
					{
						if (!GameScr.isLoadAllData)
							createSkill(new DataInputStream(Rms.loadRMS("NRskill")).r);
						LoginScr.isUpdateSkill = false;
					}
					catch (Exception)
					{
						GameScr.vcSkill = -1;
						Service.gI().updateSkill();
					}
				if (GameScr.vsItem != GameScr.vcItem)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateItem();
				}
				else
					try
					{
						createItem(new DataInputStream(Rms.loadRMS("NRitem")).r);
						LoginScr.isUpdateItem = false;
					}
					catch (Exception)
					{
						GameScr.vcItem = -1;
						Service.gI().updateItem();
					}
				if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
				{
					if (!GameScr.isLoadAllData)
					{
						GameScr.gI().readDart();
						GameScr.gI().readEfect();
						GameScr.gI().readArrow();
						GameScr.gI().readSkill();
					}
					Service.gI().clientOk();
				}
				sbyte b = msg.reader().readByte();
				Res.outz("CAPTION LENT= " + b);
				GameScr.exps = new long[b];
				for (int j = 0; j < GameScr.exps.Length; j++)
				{
					GameScr.exps[j] = msg.reader().readLong();
				}
				break;
			}
			case 6:
			{
				Res.outz("GET UPDATE_MAP " + msg.reader().available() + " bytes");
				msg.reader().mark(100000);
				createMap(msg.reader());
				msg.reader().reset();
				sbyte[] data = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref data);
				Rms.saveRMS("NRmap", data);
				Rms.saveRMS("NRmapVersion", new sbyte[1] { GameScr.vcMap });
				LoginScr.isUpdateMap = false;
				if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
				{
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
				}
				break;
			}
			case 7:
			{
				Res.outz("GET UPDATE_SKILL " + msg.reader().available() + " bytes");
				msg.reader().mark(100000);
				createSkill(msg.reader());
				msg.reader().reset();
				sbyte[] data3 = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref data3);
				Rms.saveRMS("NRskill", data3);
				Rms.saveRMS("NRskillVersion", new sbyte[1] { GameScr.vcSkill });
				LoginScr.isUpdateSkill = false;
				if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
				{
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
				}
				break;
			}
			case 8:
			{
				Res.outz("GET UPDATE_ITEM " + msg.reader().available() + " bytes");
				msg.reader().mark(100000);
				createItem(msg.reader());
				msg.reader().reset();
				sbyte[] data2 = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref data2);
				Rms.saveRMS("NRitem", data2);
				Rms.saveRMS("NRitemVersion", new sbyte[1] { GameScr.vcItem });
				LoginScr.isUpdateItem = false;
				if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
				{
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
				}
				break;
			}
			case 9:
				GameCanvas.debug("SA11", 2);
				break;
			case 10:
				try
				{
					Char.isLoadingMap = true;
					Res.outz("REQUEST MAP TEMPLATE");
					GameCanvas.isLoading = true;
					TileMap.maps = null;
					TileMap.types = null;
					mSystem.gcc();
					GameCanvas.debug("SA99", 2);
					TileMap.tmw = msg.reader().readByte();
					TileMap.tmh = msg.reader().readByte();
					TileMap.maps = new int[TileMap.tmw * TileMap.tmh];
					Res.outz("   M apsize= " + TileMap.tmw * TileMap.tmh);
					for (int i = 0; i < TileMap.maps.Length; i++)
					{
						int num2 = msg.reader().readByte();
						if (num2 < 0)
							num2 += 256;
						TileMap.maps[i] = (ushort)num2;
					}
					TileMap.types = new int[TileMap.maps.Length];
					msg = messWait;
					loadInfoMap(msg);
					AAAeee.LoadWaypointPos();
					try
					{
						TileMap.isMapDouble = msg.reader().readByte() != 0;
					}
					catch (Exception)
					{
					}
				}
				catch (Exception ex2)
				{
					Cout.LogError("LOI TAI CASE REQUEST_MAPTEMPLATE " + ex2.ToString());
				}
				msg.cleanup();
				messWait.cleanup();
				msg = (messWait = null);
				break;
			case 12:
				GameCanvas.debug("SA10", 2);
				break;
			case 16:
				MoneyCharge.gI().switchToMe();
				break;
			case 17:
				GameCanvas.debug("SYB123", 2);
				Char.myCharz().clearTask();
				break;
			case 18:
			{
				GameCanvas.isLoading = false;
				GameCanvas.endDlg();
				int num = msg.reader().readInt();
				GameCanvas.inputDlg.show(mResources.changeNameChar, new Command(mResources.OK, GameCanvas.instance, 88829, num), TField.INPUT_TYPE_ANY);
				break;
			}
			case 36:
				GameScr.typeActive = msg.reader().readByte();
				Res.outz("load Me Active: " + GameScr.typeActive);
				break;
			case 35:
				GameCanvas.endDlg();
				GameScr.gI().resetButton();
				GameScr.info1.addInfo(msg.reader().readUTF(), 0);
				break;
			case 20:
				Char.myCharz().cPk = msg.reader().readByte();
				GameScr.info1.addInfo(mResources.PK_NOW + " " + Char.myCharz().cPk, 0);
				break;
			}
		}
		catch (Exception)
		{
			Cout.LogError("LOI TAI messageNotMap + " + msg.command);
		}
		finally
		{
			msg?.cleanup();
		}
	}

	public void messageNotLogin(Message msg)
	{
		try
		{
			ServerListScreen.getServerList(AAAMYs.ip);
		}
		catch (Exception)
		{
		}
		finally
		{
			msg?.cleanup();
		}
	}

	public void messageSubCommand(Message msg)
	{
		try
		{
			GameCanvas.debug("SA12", 2);
			switch (msg.reader().readByte())
			{
			case 0:
			{
				GameCanvas.debug("SA21", 2);
				Teleport.vTeleport.removeAllElements();
				GameScr.vCharInMap.removeAllElements();
				GameScr.vItemMap.removeAllElements();
				Char.vItemTime.removeAllElements();
				GameScr.loadImg();
				GameScr.currentCharViewInfo = Char.myCharz();
				Char.myCharz().charID = msg.reader().readInt();
				Char.myCharz().ctaskId = msg.reader().readByte();
				Char.myCharz().cgender = msg.reader().readByte();
				Char.myCharz().head = msg.reader().readShort();
				Char.myCharz().cName = msg.reader().readUTF();
				Char.myCharz().cPk = msg.reader().readByte();
				Char.myCharz().cTypePk = msg.reader().readByte();
				Char.myCharz().cPower = msg.reader().readLong();
				Char.myCharz().applyCharLevelPercent();
				Char.myCharz().eff5BuffHp = msg.reader().readShort();
				Char.myCharz().eff5BuffMp = msg.reader().readShort();
				Char.myCharz().nClass = GameScr.nClasss[msg.reader().readByte()];
				Char.myCharz().vSkill.removeAllElements();
				Char.myCharz().vSkillFight.removeAllElements();
				GameScr.gI().dHP = Char.myCharz().cHP;
				GameScr.gI().dMP = Char.myCharz().cMP;
				sbyte b = msg.reader().readByte();
				for (sbyte b4 = 0; b4 < b; b4 = (sbyte)(b4 + 1))
				{
					useSkill(Skills.get(msg.reader().readShort()));
				}
				GameScr.gI().sortSkill();
				GameScr.gI().loadSkillShortcut();
				Char.myCharz().xu = msg.reader().readInt();
				Char.myCharz().yen = msg.reader().readInt();
				Char.myCharz().luong = msg.reader().readInt();
				Char.myCharz().arrItemBody = new Item[msg.reader().readByte()];
				try
				{
					Char.myCharz().setDefaultPart();
					for (int j = 0; j < Char.myCharz().arrItemBody.Length; j++)
					{
						short num3 = msg.reader().readShort();
						if (num3 == -1)
							continue;
						ItemTemplate itemTemplate = ItemTemplates.get(num3);
						int num4 = itemTemplate.type;
						Char.myCharz().arrItemBody[j] = new Item();
						Char.myCharz().arrItemBody[j].template = itemTemplate;
						Char.myCharz().arrItemBody[j].quantity = msg.reader().readInt();
						Char.myCharz().arrItemBody[j].info = msg.reader().readUTF();
						Char.myCharz().arrItemBody[j].content = msg.reader().readUTF();
						int num5 = msg.reader().readUnsignedByte();
						if (num5 != 0)
						{
							Char.myCharz().arrItemBody[j].itemOption = new ItemOption[num5];
							for (int k = 0; k < Char.myCharz().arrItemBody[j].itemOption.Length; k++)
							{
								int optionTemplateId = msg.reader().readUnsignedByte();
								ushort param = msg.reader().readUnsignedShort();
								Char.myCharz().arrItemBody[j].itemOption[k] = new ItemOption(optionTemplateId, param);
							}
						}
						switch (num4)
						{
						case 0:
							Res.outz("toi day =======================================" + Char.myCharz().body);
							Char.myCharz().body = Char.myCharz().arrItemBody[j].template.part;
							break;
						case 1:
							Char.myCharz().leg = Char.myCharz().arrItemBody[j].template.part;
							Res.outz("toi day =======================================" + Char.myCharz().leg);
							break;
						}
					}
				}
				catch (Exception)
				{
				}
				Char.myCharz().arrItemBag = new Item[msg.reader().readByte()];
				GameScr.hpPotion = 0;
				for (int l = 0; l < Char.myCharz().arrItemBag.Length; l++)
				{
					short num6 = msg.reader().readShort();
					if (num6 == -1)
						continue;
					Char.myCharz().arrItemBag[l] = new Item();
					Char.myCharz().arrItemBag[l].template = ItemTemplates.get(num6);
					Char.myCharz().arrItemBag[l].quantity = msg.reader().readInt();
					Char.myCharz().arrItemBag[l].info = msg.reader().readUTF();
					Char.myCharz().arrItemBag[l].content = msg.reader().readUTF();
					Char.myCharz().arrItemBag[l].indexUI = l;
					sbyte b5 = msg.reader().readByte();
					if (b5 != 0)
					{
						Char.myCharz().arrItemBag[l].itemOption = new ItemOption[b5];
						for (int m = 0; m < Char.myCharz().arrItemBag[l].itemOption.Length; m++)
						{
							int optionTemplateId2 = msg.reader().readUnsignedByte();
							ushort param2 = msg.reader().readUnsignedShort();
							Char.myCharz().arrItemBag[l].itemOption[m] = new ItemOption(optionTemplateId2, param2);
							Char.myCharz().arrItemBag[l].getCompare();
						}
					}
					if (Char.myCharz().arrItemBag[l].template.type == 6)
						GameScr.hpPotion += Char.myCharz().arrItemBag[l].quantity;
				}
				Char.myCharz().arrItemBox = new Item[msg.reader().readByte()];
				GameCanvas.panel.hasUse = 0;
				for (int n = 0; n < Char.myCharz().arrItemBox.Length; n++)
				{
					short num7 = msg.reader().readShort();
					if (num7 != -1)
					{
						Char.myCharz().arrItemBox[n] = new Item();
						Char.myCharz().arrItemBox[n].template = ItemTemplates.get(num7);
						Char.myCharz().arrItemBox[n].quantity = msg.reader().readInt();
						Char.myCharz().arrItemBox[n].info = msg.reader().readUTF();
						Char.myCharz().arrItemBox[n].content = msg.reader().readUTF();
						Char.myCharz().arrItemBox[n].itemOption = new ItemOption[msg.reader().readByte()];
						for (int num8 = 0; num8 < Char.myCharz().arrItemBox[n].itemOption.Length; num8++)
						{
							int optionTemplateId3 = msg.reader().readUnsignedByte();
							ushort param3 = msg.reader().readUnsignedShort();
							Char.myCharz().arrItemBox[n].itemOption[num8] = new ItemOption(optionTemplateId3, param3);
							Char.myCharz().arrItemBox[n].getCompare();
						}
						GameCanvas.panel.hasUse++;
					}
				}
				Char.myCharz().statusMe = 4;
				if (Rms.loadRMSInt(Char.myCharz().cName + "vci") < 1)
					GameScr.isViewClanInvite = false;
				else
					GameScr.isViewClanInvite = true;
				short num9 = msg.reader().readShort();
				Char.idHead = new short[num9];
				Char.idAvatar = new short[num9];
				for (int num10 = 0; num10 < num9; num10++)
				{
					Char.idHead[num10] = msg.reader().readShort();
					Char.idAvatar[num10] = msg.reader().readShort();
				}
				for (int num11 = 0; num11 < GameScr.info1.charId.Length; num11++)
				{
					GameScr.info1.charId[num11] = new int[3];
				}
				GameScr.info1.charId[Char.myCharz().cgender][0] = msg.reader().readShort();
				GameScr.info1.charId[Char.myCharz().cgender][1] = msg.reader().readShort();
				GameScr.info1.charId[Char.myCharz().cgender][2] = msg.reader().readShort();
				Char.myCharz().isNhapThe = msg.reader().readByte() == 1;
				Res.outz("NHAP THE= " + Char.myCharz().isNhapThe);
				GameScr.deltaTime = mSystem.currentTimeMillis() - msg.reader().readInt() * 1000L;
				GameScr.isNewMember = msg.reader().readByte();
				Service.gI().updateCaption((sbyte)Char.myCharz().cgender);
				Service.gI().androidPack();
				break;
			}
			case 1:
				GameCanvas.debug("SA13", 2);
				Char.myCharz().nClass = GameScr.nClasss[msg.reader().readByte()];
				Char.myCharz().cTiemNang = msg.reader().readLong();
				Char.myCharz().vSkill.removeAllElements();
				Char.myCharz().vSkillFight.removeAllElements();
				Char.myCharz().myskill = null;
				break;
			case 2:
			{
				GameCanvas.debug("SA14", 2);
				if (Char.myCharz().statusMe != 14 && Char.myCharz().statusMe != 5)
				{
					Char.myCharz().cHP = Char.myCharz().cHPFull;
					Char.myCharz().cMP = Char.myCharz().cMPFull;
					Cout.LogError2(" ME_LOAD_SKILL");
				}
				Char.myCharz().vSkill.removeAllElements();
				Char.myCharz().vSkillFight.removeAllElements();
				sbyte b = msg.reader().readByte();
				for (sbyte b2 = 0; b2 < b; b2 = (sbyte)(b2 + 1))
				{
					useSkill(Skills.get(msg.reader().readShort()));
				}
				GameScr.gI().sortSkill();
				if (GameScr.isPaintInfoMe)
				{
					GameScr.indexRow = -1;
					GameScr.gI().left = (GameScr.gI().center = null);
				}
				break;
			}
			case 4:
				GameCanvas.debug("SA23", 2);
				Char.myCharz().xu = msg.reader().readInt();
				Char.myCharz().luong = msg.reader().readInt();
				Char.myCharz().cHP = msg.readInt3Byte();
				Char.myCharz().cMP = msg.readInt3Byte();
				break;
			case 5:
			{
				GameCanvas.debug("SA24", 2);
				int cHP = Char.myCharz().cHP;
				Char.myCharz().cHP = msg.readInt3Byte();
				if (Char.myCharz().cHP > cHP && Char.myCharz().cTypePk != 4)
				{
					GameScr.startFlyText("+" + (Char.myCharz().cHP - cHP) + " " + mResources.HP, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 20, 0, -1, mFont.HP);
					SoundMn.gI().HP_MPup();
					if (Char.myCharz().petFollow != null && Char.myCharz().petFollow.smallID == 5003)
						MonsterDart.addMonsterDart(Char.myCharz().petFollow.cmx + ((Char.myCharz().petFollow.dir != 1) ? (-10) : 10), Char.myCharz().petFollow.cmy + 10, true, -1, -1, Char.myCharz(), 29);
				}
				if (Char.myCharz().cHP < cHP)
					GameScr.startFlyText("-" + (cHP - Char.myCharz().cHP) + " " + mResources.HP, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 20, 0, -1, mFont.HP);
				GameScr.gI().dHP = Char.myCharz().cHP;
				if (GameScr.isPaintInfoMe)
					;
				break;
			}
			case 6:
			{
				GameCanvas.debug("SA25", 2);
				if (Char.myCharz().statusMe == 14 || Char.myCharz().statusMe == 5)
					break;
				int cMP = Char.myCharz().cMP;
				Char.myCharz().cMP = msg.readInt3Byte();
				if (Char.myCharz().cMP > cMP)
				{
					GameScr.startFlyText("+" + (Char.myCharz().cMP - cMP) + " " + mResources.KI, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 23, 0, -2, mFont.MP);
					SoundMn.gI().HP_MPup();
					if (Char.myCharz().petFollow != null && Char.myCharz().petFollow.smallID == 5001)
						MonsterDart.addMonsterDart(Char.myCharz().petFollow.cmx + ((Char.myCharz().petFollow.dir != 1) ? (-10) : 10), Char.myCharz().petFollow.cmy + 10, true, -1, -1, Char.myCharz(), 29);
				}
				if (Char.myCharz().cMP < cMP)
					GameScr.startFlyText("-" + (cMP - Char.myCharz().cMP) + " " + mResources.KI, Char.myCharz().cx, Char.myCharz().cy - Char.myCharz().ch - 23, 0, -2, mFont.MP);
				Res.outz("curr MP= " + Char.myCharz().cMP);
				GameScr.gI().dMP = Char.myCharz().cMP;
				if (GameScr.isPaintInfoMe)
					;
				break;
			}
			case 7:
			{
				Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.clanID = msg.reader().readInt();
					if (@char.clanID == -2)
						@char.isCopy = true;
					readCharInfo(@char, msg);
				}
				break;
			}
			case 8:
			{
				GameCanvas.debug("SA26", 2);
				Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
					@char.cspeed = msg.reader().readByte();
				break;
			}
			case 9:
			{
				GameCanvas.debug("SA27", 2);
				Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
				}
				break;
			}
			case 10:
			{
				GameCanvas.debug("SA28", 2);
				Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
					@char.eff5BuffHp = msg.reader().readShort();
					@char.eff5BuffMp = msg.reader().readShort();
					@char.wp = msg.reader().readShort();
					if (@char.wp == -1)
						@char.setDefaultWeapon();
				}
				break;
			}
			case 11:
			{
				GameCanvas.debug("SA29", 2);
				Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
					@char.eff5BuffHp = msg.reader().readShort();
					@char.eff5BuffMp = msg.reader().readShort();
					@char.body = msg.reader().readShort();
					if (@char.body == -1)
						@char.setDefaultBody();
				}
				break;
			}
			case 12:
			{
				GameCanvas.debug("SA30", 2);
				Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
					@char.eff5BuffHp = msg.reader().readShort();
					@char.eff5BuffMp = msg.reader().readShort();
					@char.leg = msg.reader().readShort();
					if (@char.leg == -1)
						@char.setDefaultLeg();
				}
				break;
			}
			case 13:
			{
				GameCanvas.debug("SA31", 2);
				Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
					@char.eff5BuffHp = msg.reader().readShort();
					@char.eff5BuffMp = msg.reader().readShort();
				}
				break;
			}
			case 14:
			{
				GameCanvas.debug("SA32", 2);
				Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
					break;
				@char.cHP = msg.readInt3Byte();
				sbyte b3 = msg.reader().readByte();
				Res.outz("player load hp type= " + b3);
				if (b3 == 1)
				{
					ServerEffect.addServerEffect(11, @char, 5);
					ServerEffect.addServerEffect(104, @char, 4);
				}
				try
				{
					@char.cHPFull = msg.readInt3Byte();
					break;
				}
				catch (Exception)
				{
					break;
				}
			}
			case 15:
			{
				GameCanvas.debug("SA33", 2);
				Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.cHP = msg.readInt3Byte();
					@char.cHPFull = msg.readInt3Byte();
					@char.cx = msg.reader().readShort();
					@char.cy = msg.reader().readShort();
					@char.statusMe = 1;
					@char.cp3 = 3;
					ServerEffect.addServerEffect(109, @char, 2);
				}
				break;
			}
			case 19:
				GameCanvas.debug("SA17", 2);
				Char.myCharz().boxSort();
				break;
			case 20:
			{
				GameCanvas.debug("SA18", 2);
				int num2 = msg.reader().readInt();
				Char.myCharz().xu -= num2;
				Char.myCharz().xuInBox += num2;
				break;
			}
			case 21:
			{
				GameCanvas.debug("SA19", 2);
				int num18 = msg.reader().readInt();
				Char.myCharz().xuInBox -= num18;
				Char.myCharz().xu += num18;
				break;
			}
			case 23:
			{
				short num13 = msg.reader().readShort();
				Skill skill = Skills.get(num13);
				useSkill(skill);
				if (num13 != 0 && num13 != 14 && num13 != 28)
					GameScr.info1.addInfo(mResources.LEARN_SKILL + " " + skill.template.name, 0);
				break;
			}
			case 61:
			{
				string text = msg.reader().readUTF();
				sbyte[] data = new sbyte[msg.reader().readInt()];
				msg.reader().read(ref data);
				if (data.Length == 0)
					data = null;
				if (text.Equals("KSkill"))
					GameScr.gI().onKSkill(data);
				else if (text.Equals("OSkill"))
				{
					GameScr.gI().onOSkill(data);
				}
				else if (text.Equals("CSkill"))
				{
					GameScr.gI().onCSkill(data);
				}
				break;
			}
			case 62:
			{
				Res.outz("ME UPDATE SKILL");
				Skill skill2 = Skills.get(msg.reader().readShort());
				for (int num14 = 0; num14 < Char.myCharz().vSkill.size(); num14++)
				{
					if (((Skill)Char.myCharz().vSkill.elementAt(num14)).template.id == skill2.template.id)
					{
						Char.myCharz().vSkill.setElementAt(skill2, num14);
						break;
					}
				}
				for (int num15 = 0; num15 < Char.myCharz().vSkillFight.size(); num15++)
				{
					if (((Skill)Char.myCharz().vSkillFight.elementAt(num15)).template.id == skill2.template.id)
					{
						Char.myCharz().vSkillFight.setElementAt(skill2, num15);
						break;
					}
				}
				for (int num16 = 0; num16 < GameScr.onScreenSkill.Length; num16++)
				{
					if (GameScr.onScreenSkill[num16] != null && GameScr.onScreenSkill[num16].template.id == skill2.template.id)
					{
						GameScr.onScreenSkill[num16] = skill2;
						break;
					}
				}
				for (int num17 = 0; num17 < GameScr.keySkill.Length; num17++)
				{
					if (GameScr.keySkill[num17] != null && GameScr.keySkill[num17].template.id == skill2.template.id)
					{
						GameScr.keySkill[num17] = skill2;
						break;
					}
				}
				if (Char.myCharz().myskill.template.id == skill2.template.id)
					Char.myCharz().myskill = skill2;
				GameScr.info1.addInfo(mResources.hasJustUpgrade1 + skill2.template.name + mResources.hasJustUpgrade2 + skill2.point, 0);
				break;
			}
			case 63:
			{
				sbyte b6 = msg.reader().readByte();
				if (b6 > 0)
				{
					InfoDlg.showWait();
					MyVector vPlayerMenu = GameCanvas.panel.vPlayerMenu;
					for (int num12 = 0; num12 < b6; num12++)
					{
						string caption = msg.reader().readUTF();
						string caption2 = msg.reader().readUTF();
						short menuSelect = msg.reader().readShort();
						Char.myCharz().charFocus.menuSelect = menuSelect;
						Command command = new Command(caption, 11115, Char.myCharz().charFocus);
						command.caption2 = caption2;
						vPlayerMenu.addElement(command);
					}
					InfoDlg.hide();
					GameCanvas.panel.setTabPlayerMenu();
				}
				break;
			}
			case 35:
			{
				GameCanvas.debug("SY3", 2);
				int num = msg.reader().readInt();
				Res.outz("CID = " + num);
				if (TileMap.mapID == 130)
					GameScr.gI().starVS();
				if (num == Char.myCharz().charID)
				{
					Char.myCharz().cTypePk = msg.reader().readByte();
					if (GameScr.gI().isVS() && Char.myCharz().cTypePk != 0)
						GameScr.gI().starVS();
					Res.outz("type pk= " + Char.myCharz().cTypePk);
					Char.myCharz().npcFocus = null;
					if (!GameScr.gI().isMeCanAttackMob(Char.myCharz().mobFocus))
						Char.myCharz().mobFocus = null;
					Char.myCharz().itemFocus = null;
				}
				else
				{
					Char @char = GameScr.findCharInMap(num);
					if (@char != null)
					{
						Res.outz("type pk= " + @char.cTypePk);
						@char.cTypePk = msg.reader().readByte();
						if (@char.isAttacPlayerStatus())
							Char.myCharz().charFocus = @char;
					}
				}
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					Char char2 = GameScr.findCharInMap(i);
					if (char2 != null && char2.cTypePk != 0 && char2.cTypePk == Char.myCharz().cTypePk)
					{
						if (!Char.myCharz().mobFocus.isMobMe)
							Char.myCharz().mobFocus = null;
						Char.myCharz().npcFocus = null;
						Char.myCharz().itemFocus = null;
						break;
					}
				}
				Res.outz("update type pk= ");
				break;
			}
			}
		}
		catch (Exception ex3)
		{
			Cout.println("Loi tai Sub : " + ex3.ToString());
		}
		finally
		{
			msg?.cleanup();
		}
	}

	private void useSkill(Skill skill)
	{
		if (Char.myCharz().myskill == null)
			Char.myCharz().myskill = skill;
		else if (skill.template.Equals(Char.myCharz().myskill.template))
		{
			Char.myCharz().myskill = skill;
		}
		Char.myCharz().vSkill.addElement(skill);
		if ((skill.template.type == 1 || skill.template.type == 4 || skill.template.type == 2 || skill.template.type == 3) && (skill.template.maxPoint == 0 || (skill.template.maxPoint > 0 && skill.point > 0)))
		{
			if (skill.template.id == Char.myCharz().skillTemplateId)
				Service.gI().selectSkill(Char.myCharz().skillTemplateId);
			Char.myCharz().vSkillFight.addElement(skill);
		}
	}

	public bool readCharInfo(Char c, Message msg)
	{
		try
		{
			c.clevel = msg.reader().readByte();
			c.isInvisiblez = msg.reader().readBoolean();
			c.cTypePk = msg.reader().readByte();
			Res.outz("ADD TYPE PK= " + c.cTypePk + " to player " + c.charID + " @@ " + c.cName);
			c.nClass = GameScr.nClasss[msg.reader().readByte()];
			c.cgender = msg.reader().readByte();
			c.head = msg.reader().readShort();
			c.cName = msg.reader().readUTF();
			c.cHP = msg.readInt3Byte();
			c.dHP = c.cHP;
			if (c.cHP == 0)
				c.statusMe = 14;
			c.cHPFull = msg.readInt3Byte();
			if (c.cy >= TileMap.pxh - 100)
				c.isFlyUp = true;
			c.body = msg.reader().readShort();
			c.leg = msg.reader().readShort();
			c.bag = msg.reader().readUnsignedByte();
			Res.outz(" body= " + c.body + " leg= " + c.leg + " bag=" + c.bag + "BAG ==" + c.bag + "*********************************");
			c.isShadown = true;
			msg.reader().readByte();
			if (c.wp == -1)
				c.setDefaultWeapon();
			if (c.body == -1)
				c.setDefaultBody();
			if (c.leg == -1)
				c.setDefaultLeg();
			Res.outz("1");
			c.cx = msg.reader().readShort();
			c.cy = msg.reader().readShort();
			c.xSd = c.cx;
			c.ySd = c.cy;
			c.eff5BuffHp = msg.reader().readShort();
			c.eff5BuffMp = msg.reader().readShort();
			int num = msg.reader().readByte();
			for (int i = 0; i < num; i++)
			{
				EffectChar effectChar = new EffectChar(msg.reader().readByte(), msg.reader().readInt(), msg.reader().readInt(), msg.reader().readShort());
				c.vEff.addElement(effectChar);
				if (effectChar.template.type == 12 || effectChar.template.type == 11)
					c.isInvisiblez = true;
			}
			return true;
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		return false;
	}

	private void readGetImgByName(Message msg)
	{
		try
		{
			string text = msg.reader().readUTF();
			sbyte nFrame = msg.reader().readByte();
			sbyte[] array = null;
			array = NinjaUtil.readByteArray(msg);
			ImgByName.SetImage(text, createImage(array), nFrame);
			if (array != null)
				ImgByName.saveRMS(text, nFrame, array);
		}
		catch (Exception)
		{
		}
	}
}
