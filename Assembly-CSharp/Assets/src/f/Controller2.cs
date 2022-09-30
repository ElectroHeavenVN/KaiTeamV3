using System;
using Assets.src.g;

namespace Assets.src.f
{
	internal class Controller2
	{
		public static void readMessage(Message msg)
		{
			try
			{
				Res.outz("cmd=" + msg.command);
				switch (msg.command)
				{
				case -126:
				{
					sbyte b23 = msg.reader().readByte();
					Res.outz("type quay= " + b23);
					if (b23 == 1)
					{
						msg.reader().readByte();
						string num34 = msg.reader().readUTF();
						string finish = msg.reader().readUTF();
						GameScr.gI().showWinNumber(num34, finish);
					}
					if (b23 == 0)
						GameScr.gI().showYourNumber(msg.reader().readUTF());
					break;
				}
				case -125:
				{
					ChatTextField.gI().isShow = false;
					string text2 = msg.reader().readUTF();
					Res.outz("titile= " + text2);
					sbyte b30 = msg.reader().readByte();
					ClientInput.gI().setInput(b30, text2);
					for (int num39 = 0; num39 < b30; num39++)
					{
						ClientInput.gI().tf[num39].name = msg.reader().readUTF();
						sbyte b31 = msg.reader().readByte();
						if (b31 == 0)
							ClientInput.gI().tf[num39].setIputType(TField.INPUT_TYPE_NUMERIC);
						if (b31 == 1)
							ClientInput.gI().tf[num39].setIputType(TField.INPUT_TYPE_ANY);
						if (b31 == 2)
							ClientInput.gI().tf[num39].setIputType(TField.INPUT_TYPE_PASSWORD);
					}
					break;
				}
				case -124:
				{
					sbyte b20 = msg.reader().readByte();
					sbyte b21 = msg.reader().readByte();
					if (b21 == 0)
					{
						if (b20 == 2)
						{
							int num25 = msg.reader().readInt();
							if (num25 == Char.myCharz().charID)
								Char.myCharz().removeEffect();
							else if (GameScr.findCharInMap(num25) != null)
							{
								GameScr.findCharInMap(num25).removeEffect();
							}
						}
						int num26 = msg.reader().readUnsignedByte();
						int num27 = msg.reader().readInt();
						if (num26 == 32)
						{
							if (b20 == 1)
							{
								int num28 = msg.reader().readInt();
								if (num27 == Char.myCharz().charID)
								{
									Char.myCharz().holdEffID = num26;
									GameScr.findCharInMap(num28).setHoldChar(Char.myCharz());
								}
								else if (GameScr.findCharInMap(num27) != null && num28 != Char.myCharz().charID)
								{
									GameScr.findCharInMap(num27).holdEffID = num26;
									GameScr.findCharInMap(num28).setHoldChar(GameScr.findCharInMap(num27));
								}
								else if (GameScr.findCharInMap(num27) != null && num28 == Char.myCharz().charID)
								{
									GameScr.findCharInMap(num27).holdEffID = num26;
									Char.myCharz().setHoldChar(GameScr.findCharInMap(num27));
								}
							}
							else if (num27 == Char.myCharz().charID)
							{
								Char.myCharz().removeHoleEff();
							}
							else if (GameScr.findCharInMap(num27) != null)
							{
								GameScr.findCharInMap(num27).removeHoleEff();
							}
						}
						if (num26 == 33)
						{
							if (b20 == 1)
							{
								if (num27 == Char.myCharz().charID)
									Char.myCharz().protectEff = true;
								else if (GameScr.findCharInMap(num27) != null)
								{
									GameScr.findCharInMap(num27).protectEff = true;
								}
							}
							else if (num27 == Char.myCharz().charID)
							{
								Char.myCharz().removeProtectEff();
							}
							else if (GameScr.findCharInMap(num27) != null)
							{
								GameScr.findCharInMap(num27).removeProtectEff();
							}
						}
						if (num26 == 39)
						{
							if (b20 == 1)
							{
								if (num27 == Char.myCharz().charID)
									Char.myCharz().huytSao = true;
								else if (GameScr.findCharInMap(num27) != null)
								{
									GameScr.findCharInMap(num27).huytSao = true;
								}
							}
							else if (num27 == Char.myCharz().charID)
							{
								Char.myCharz().removeHuytSao();
							}
							else if (GameScr.findCharInMap(num27) != null)
							{
								GameScr.findCharInMap(num27).removeHuytSao();
							}
						}
						if (num26 == 40)
						{
							if (b20 == 1)
							{
								if (num27 == Char.myCharz().charID)
									Char.myCharz().blindEff = true;
								else if (GameScr.findCharInMap(num27) != null)
								{
									GameScr.findCharInMap(num27).blindEff = true;
								}
							}
							else if (num27 == Char.myCharz().charID)
							{
								Char.myCharz().removeBlindEff();
							}
							else if (GameScr.findCharInMap(num27) != null)
							{
								GameScr.findCharInMap(num27).removeBlindEff();
							}
						}
						if (num26 == 41)
						{
							if (b20 == 1)
							{
								if (num27 == Char.myCharz().charID)
									Char.myCharz().sleepEff = true;
								else if (GameScr.findCharInMap(num27) != null)
								{
									GameScr.findCharInMap(num27).sleepEff = true;
								}
							}
							else if (num27 == Char.myCharz().charID)
							{
								Char.myCharz().removeSleepEff();
							}
							else if (GameScr.findCharInMap(num27) != null)
							{
								GameScr.findCharInMap(num27).removeSleepEff();
							}
						}
						if (num26 == 42)
						{
							if (b20 == 1)
							{
								if (num27 == Char.myCharz().charID)
									Char.myCharz().stone = true;
							}
							else if (num27 == Char.myCharz().charID)
							{
								Char.myCharz().stone = false;
							}
						}
					}
					if (b21 != 1)
						break;
					int num29 = msg.reader().readUnsignedByte();
					sbyte b22 = msg.reader().readByte();
					Res.outz("modbHoldID= " + b22 + " skillID= " + num29 + "eff ID= " + b20);
					if (num29 == 32)
					{
						if (b20 == 1)
						{
							int num30 = msg.reader().readInt();
							if (num30 == Char.myCharz().charID)
							{
								GameScr.findMobInMap(b22).holdEffID = num29;
								Char.myCharz().setHoldMob(GameScr.findMobInMap(b22));
							}
							else if (GameScr.findCharInMap(num30) != null)
							{
								GameScr.findMobInMap(b22).holdEffID = num29;
								GameScr.findCharInMap(num30).setHoldMob(GameScr.findMobInMap(b22));
							}
						}
						else
							GameScr.findMobInMap(b22).removeHoldEff();
					}
					if (num29 == 40)
					{
						if (b20 == 1)
							GameScr.findMobInMap(b22).blindEff = true;
						else
							GameScr.findMobInMap(b22).removeBlindEff();
					}
					if (num29 == 41)
					{
						if (b20 == 1)
							GameScr.findMobInMap(b22).sleepEff = true;
						else
							GameScr.findMobInMap(b22).removeSleepEff();
					}
					break;
				}
				case -123:
				{
					int charId = msg.reader().readInt();
					if (GameScr.findCharInMap(charId) != null)
						GameScr.findCharInMap(charId).perCentMp = msg.reader().readByte();
					break;
				}
				case -122:
				{
					Npc npc = GameScr.findNPCInMap(msg.reader().readShort());
					sbyte b16 = msg.reader().readByte();
					npc.duahau = new int[b16];
					Res.outz("N DUA HAU= " + b16);
					for (int num20 = 0; num20 < b16; num20++)
					{
						npc.duahau[num20] = msg.reader().readShort();
					}
					npc.setStatus(msg.reader().readByte(), msg.reader().readInt());
					break;
				}
				case -121:
					Service.logMap = mSystem.currentTimeMillis() - Service.curCheckMap;
					Service.gI().sendCheckMap();
					break;
				case -120:
					Service.logController = mSystem.currentTimeMillis() - Service.curCheckController;
					Service.gI().sendCheckController();
					break;
				case -119:
					Char.myCharz().rank = msg.reader().readInt();
					break;
				case -117:
					GameScr.gI().tMabuEff = 0;
					GameScr.gI().percentMabu = msg.reader().readByte();
					if (GameScr.gI().percentMabu == 100)
						GameScr.gI().mabuEff = true;
					if (GameScr.gI().percentMabu == 101)
						Npc.mabuEff = true;
					break;
				case -116:
					GameScr.canAutoPlay = msg.reader().readByte() == 1;
					break;
				case -115:
					Char.myCharz().setPowerInfo(msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort());
					break;
				case -113:
				{
					sbyte[] array7 = new sbyte[5];
					for (int num33 = 0; num33 < 5; num33++)
					{
						array7[num33] = msg.reader().readByte();
						Res.outz("vlue i= " + array7[num33]);
					}
					GameScr.gI().onKSkill(array7);
					GameScr.gI().onOSkill(array7);
					GameScr.gI().onCSkill(array7);
					break;
				}
				case -111:
				{
					short num31 = msg.reader().readShort();
					ImageSource.vSource = new MyVector();
					for (int num32 = 0; num32 < num31; num32++)
					{
						string iD = msg.reader().readUTF();
						sbyte version = msg.reader().readByte();
						ImageSource.vSource.addElement(new ImageSource(iD, version));
					}
					ImageSource.checkRMS();
					ImageSource.saveRMS();
					break;
				}
				case -110:
				{
					sbyte b13 = msg.reader().readByte();
					if (b13 == 1)
					{
						int num14 = msg.reader().readInt();
						sbyte[] array6 = Rms.loadRMS(num14 + string.Empty);
						if (array6 == null)
							Service.gI().sendServerData(1, -1, null);
						else
							Service.gI().sendServerData(1, num14, array6);
					}
					if (b13 == 0)
					{
						int num15 = msg.reader().readInt();
						short num16 = msg.reader().readShort();
						sbyte[] data = new sbyte[num16];
						msg.reader().read(ref data, 0, num16);
						Rms.saveRMS(num15 + string.Empty, data);
					}
					break;
				}
				case -106:
				{
					short num10 = msg.reader().readShort();
					int num11 = msg.reader().readShort();
					if (ItemTime.isExistItem(num10))
					{
						ItemTime.getItemById(num10).initTime(num11);
						break;
					}
					ItemTime o = new ItemTime(num10, num11);
					Char.vItemTime.addElement(o);
					break;
				}
				case -105:
					TransportScr.gI().time = 0;
					TransportScr.gI().maxTime = msg.reader().readShort();
					TransportScr.gI().last = (TransportScr.gI().curr = mSystem.currentTimeMillis());
					TransportScr.gI().type = msg.reader().readByte();
					TransportScr.gI().switchToMe();
					break;
				case -103:
				{
					sbyte b = msg.reader().readByte();
					if (b == 0)
					{
						GameCanvas.panel.vFlag.removeAllElements();
						sbyte b2 = msg.reader().readByte();
						for (int i = 0; i < b2; i++)
						{
							Item item = new Item();
							short num = msg.reader().readShort();
							if (num != -1)
							{
								item.template = ItemTemplates.get(num);
								sbyte b3 = msg.reader().readByte();
								if (b3 != -1)
								{
									item.itemOption = new ItemOption[b3];
									for (int j = 0; j < item.itemOption.Length; j++)
									{
										sbyte b4 = msg.reader().readByte();
										int param = msg.reader().readUnsignedShort();
										if (b4 != -1)
											item.itemOption[j] = new ItemOption(b4, param);
									}
								}
							}
							GameCanvas.panel.vFlag.addElement(item);
						}
						GameCanvas.panel.setTypeFlag();
						GameCanvas.panel.show();
					}
					else if (b == 1)
					{
						int num2 = msg.reader().readInt();
						sbyte b5 = msg.reader().readByte();
						Res.outz("---------------actionFlag1:  " + num2 + " : " + b5);
						if (num2 == Char.myCharz().charID)
							Char.myCharz().cFlag = b5;
						else if (GameScr.findCharInMap(num2) != null)
						{
							GameScr.findCharInMap(num2).cFlag = b5;
						}
						GameScr.gI().getFlagImage(num2, b5);
					}
					else
					{
						if (b != 2)
							break;
						sbyte b6 = msg.reader().readByte();
						int num3 = msg.reader().readShort();
						PKFlag pKFlag = new PKFlag();
						pKFlag.cflag = b6;
						pKFlag.IDimageFlag = num3;
						GameScr.vFlag.addElement(pKFlag);
						for (int k = 0; k < GameScr.vFlag.size(); k++)
						{
							PKFlag pKFlag2 = (PKFlag)GameScr.vFlag.elementAt(k);
							Res.outz("i: " + k + "  cflag: " + pKFlag2.cflag + "   IDimageFlag: " + pKFlag2.IDimageFlag);
						}
						for (int l = 0; l < GameScr.vCharInMap.size(); l++)
						{
							Char @char = (Char)GameScr.vCharInMap.elementAt(l);
							if (@char != null && @char.cFlag == b6)
								@char.flagImage = num3;
						}
						if (Char.myCharz().cFlag == b6)
							Char.myCharz().flagImage = num3;
					}
					break;
				}
				case -102:
				{
					sbyte b14 = msg.reader().readByte();
					if (b14 != 0 && b14 == 1)
					{
						GameCanvas.loginScr.isLogin2 = false;
						Service.gI().login(Rms.loadRMSString("acc"), Rms.loadRMSString("pass"), GameMidlet.VERSION, 0);
						LoginScr.isLoggingIn = true;
					}
					break;
				}
				case -101:
				{
					GameCanvas.loginScr.isLogin2 = true;
					GameCanvas.connect();
					string text3 = msg.reader().readUTF();
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, text3);
					Service.gI().setClientType();
					Service.gI().login(text3, string.Empty, GameMidlet.VERSION, 1);
					break;
				}
				case -100:
				{
					InfoDlg.hide();
					bool flag = false;
					if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
						flag = true;
					sbyte b17 = msg.reader().readByte();
					Res.outz("t Indxe= " + b17);
					GameCanvas.panel.maxPageShop[b17] = msg.reader().readByte();
					GameCanvas.panel.currPageShop[b17] = msg.reader().readByte();
					Res.outz("max page= " + GameCanvas.panel.maxPageShop[b17] + " curr page= " + GameCanvas.panel.currPageShop[b17]);
					int num21 = msg.reader().readUnsignedByte();
					Char.myCharz().arrItemShop[b17] = new Item[num21];
					for (int num22 = 0; num22 < num21; num22++)
					{
						short num23 = msg.reader().readShort();
						if (num23 == -1)
							continue;
						Res.outz("template id= " + num23);
						Char.myCharz().arrItemShop[b17][num22] = new Item();
						Char.myCharz().arrItemShop[b17][num22].template = ItemTemplates.get(num23);
						Char.myCharz().arrItemShop[b17][num22].itemId = msg.reader().readShort();
						Char.myCharz().arrItemShop[b17][num22].buyCoin = msg.reader().readInt();
						Char.myCharz().arrItemShop[b17][num22].buyGold = msg.reader().readInt();
						Char.myCharz().arrItemShop[b17][num22].buyType = msg.reader().readByte();
						Char.myCharz().arrItemShop[b17][num22].quantity = msg.reader().readByte();
						Char.myCharz().arrItemShop[b17][num22].isMe = msg.reader().readByte();
						Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
						sbyte b18 = msg.reader().readByte();
						if (b18 != -1)
						{
							Char.myCharz().arrItemShop[b17][num22].itemOption = new ItemOption[b18];
							for (int num24 = 0; num24 < Char.myCharz().arrItemShop[b17][num22].itemOption.Length; num24++)
							{
								sbyte b19 = msg.reader().readByte();
								int param2 = msg.reader().readUnsignedShort();
								if (b19 != -1)
								{
									Char.myCharz().arrItemShop[b17][num22].itemOption[num24] = new ItemOption(b19, param2);
									Char.myCharz().arrItemShop[b17][num22].compare = GameCanvas.panel.getCompare(Char.myCharz().arrItemShop[b17][num22]);
								}
							}
						}
						if (msg.reader().readByte() == 1)
						{
							int headTemp = msg.reader().readShort();
							int bodyTemp = msg.reader().readShort();
							int legTemp = msg.reader().readShort();
							short bagTemp = msg.reader().readShort();
							Char.myCharz().arrItemShop[b17][num22].setPartTemp(headTemp, bodyTemp, legTemp, bagTemp);
						}
					}
					if (flag)
						GameCanvas.panel2.setTabKiGui();
					GameCanvas.panel.setTabShop();
					Panel panel = GameCanvas.panel;
					Panel panel2 = GameCanvas.panel;
					int num4 = 0;
					panel2.cmtoY = 0;
					panel.cmy = 0;
					break;
				}
				case 48:
					ServerListScreen.ipSelect = msg.reader().readByte();
					GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
					Session_ME.gI().close();
					GameCanvas.endDlg();
					ServerListScreen.waitToLogin = true;
					break;
				case 113:
					EffecMn.addEff(new Effect(loop: msg.reader().readByte(), layer: msg.reader().readByte(), id: msg.reader().readUnsignedByte(), x: msg.reader().readShort(), y: msg.reader().readShort(), loopCount: msg.reader().readShort()));
					break;
				case 93:
				{
					string chatVip = Res.changeString(msg.reader().readUTF());
					GameScr.gI().chatVip(chatVip);
					break;
				}
				case 42:
				{
					GameCanvas.endDlg();
					LoginScr.isContinueToLogin = false;
					Char.isLoadingMap = false;
					sbyte haveName = msg.reader().readByte();
					if (GameCanvas.registerScr == null)
						GameCanvas.registerScr = new RegisterScreen(haveName);
					GameCanvas.registerScr.switchToMe();
					break;
				}
				case 31:
				{
					int num8 = msg.reader().readInt();
					if (msg.reader().readByte() == 1)
					{
						short smallID = msg.reader().readShort();
						sbyte b10 = -1;
						int[] array5 = null;
						short wimg = 0;
						short himg = 0;
						try
						{
							b10 = msg.reader().readByte();
							if (b10 > 0)
							{
								sbyte b11 = msg.reader().readByte();
								array5 = new int[b11];
								for (int num9 = 0; num9 < b11; num9++)
								{
									array5[num9] = msg.reader().readByte();
								}
								wimg = msg.reader().readShort();
								himg = msg.reader().readShort();
							}
						}
						catch (Exception)
						{
						}
						if (num8 == Char.myCharz().charID)
						{
							Char.myCharz().petFollow = new PetFollow();
							Char.myCharz().petFollow.smallID = smallID;
							if (b10 > 0)
								Char.myCharz().petFollow.SetImg(b10, array5, wimg, himg);
							break;
						}
						Char char2 = GameScr.findCharInMap(num8);
						char2.petFollow = new PetFollow();
						char2.petFollow.smallID = smallID;
						if (b10 > 0)
							char2.petFollow.SetImg(b10, array5, wimg, himg);
					}
					else if (num8 == Char.myCharz().charID)
					{
						Char.myCharz().petFollow.remove();
						Char.myCharz().petFollow = null;
					}
					else
					{
						Char char3 = GameScr.findCharInMap(num8);
						char3.petFollow.remove();
						char3.petFollow = null;
					}
					break;
				}
				case 100:
				{
					sbyte b24 = msg.reader().readByte();
					sbyte b25 = msg.reader().readByte();
					Item item2 = null;
					if (b24 == 0)
						item2 = Char.myCharz().arrItemBody[b25];
					if (b24 == 1)
						item2 = Char.myCharz().arrItemBag[b25];
					short num35 = msg.reader().readShort();
					if (num35 == -1)
						break;
					item2.template = ItemTemplates.get(num35);
					item2.quantity = msg.reader().readInt();
					item2.info = msg.reader().readUTF();
					item2.content = msg.reader().readUTF();
					sbyte b26 = msg.reader().readByte();
					if (b26 == 0)
						break;
					item2.itemOption = new ItemOption[b26];
					for (int num36 = 0; num36 < item2.itemOption.Length; num36++)
					{
						sbyte b27 = msg.reader().readByte();
						Res.outz("id o= " + b27);
						int param3 = msg.reader().readUnsignedShort();
						if (b27 != -1)
							item2.itemOption[num36] = new ItemOption(b27, param3);
					}
					break;
				}
				case 101:
				{
					Res.outz("big boss--------------------------------------------------");
					BigBoss bigBoss2 = Mob.getBigBoss();
					if (bigBoss2 == null)
						break;
					sbyte b28 = msg.reader().readByte();
					if (b28 == 0 || b28 == 1 || b28 == 2 || b28 == 4 || b28 == 3)
					{
						if (b28 == 3)
						{
							bigBoss2.xTo = (bigBoss2.xFirst = msg.reader().readShort());
							bigBoss2.yTo = (bigBoss2.yFirst = msg.reader().readShort());
							bigBoss2.setFly();
						}
						else
						{
							sbyte b29 = msg.reader().readByte();
							Res.outz("CHUONG nChar= " + b29);
							Char[] array8 = new Char[b29];
							int[] array9 = new int[b29];
							for (int num37 = 0; num37 < b29; num37++)
							{
								int num38 = msg.reader().readInt();
								Res.outz("char ID=" + num38);
								array8[num37] = null;
								if (num38 != Char.myCharz().charID)
									array8[num37] = GameScr.findCharInMap(num38);
								else
									array8[num37] = Char.myCharz();
								array9[num37] = msg.reader().readInt();
							}
							bigBoss2.setAttack(array8, array9, b28);
						}
					}
					if (b28 == 5)
					{
						bigBoss2.haftBody = true;
						bigBoss2.status = 2;
					}
					if (b28 == 6)
					{
						bigBoss2.getDataB2();
						bigBoss2.x = msg.reader().readShort();
						bigBoss2.y = msg.reader().readShort();
					}
					if (b28 == 7)
						bigBoss2.setAttack(null, null, b28);
					if (b28 == 8)
					{
						bigBoss2.xTo = (bigBoss2.xFirst = msg.reader().readShort());
						bigBoss2.yTo = (bigBoss2.yFirst = msg.reader().readShort());
						bigBoss2.status = 2;
					}
					if (b28 == 9)
					{
						int num4 = -1000;
						bigBoss2.yFirst = -1000;
						num4 = -1000;
						bigBoss2.xFirst = -1000;
						num4 = -1000;
						bigBoss2.yTo = -1000;
						num4 = -1000;
						bigBoss2.xTo = -1000;
						num4 = -1000;
						bigBoss2.y = -1000;
						bigBoss2.x = -1000;
					}
					break;
				}
				case 102:
				{
					sbyte b7 = msg.reader().readByte();
					if (b7 == 0 || b7 == 1 || b7 == 2 || b7 == 6)
					{
						BigBoss2 bigBoss = Mob.getBigBoss2();
						if (bigBoss == null)
							break;
						if (b7 == 6)
						{
							int num4 = -1000;
							bigBoss.yFirst = -1000;
							num4 = -1000;
							bigBoss.xFirst = -1000;
							num4 = -1000;
							bigBoss.yTo = -1000;
							num4 = -1000;
							bigBoss.xTo = -1000;
							num4 = -1000;
							bigBoss.y = -1000;
							bigBoss.x = -1000;
							break;
						}
						sbyte b8 = msg.reader().readByte();
						Char[] array = new Char[b8];
						int[] array2 = new int[b8];
						for (int m = 0; m < b8; m++)
						{
							int num5 = msg.reader().readInt();
							array[m] = null;
							if (num5 != Char.myCharz().charID)
								array[m] = GameScr.findCharInMap(num5);
							else
								array[m] = Char.myCharz();
							array2[m] = msg.reader().readInt();
						}
						bigBoss.setAttack(array, array2, b7);
					}
					if (b7 != 3 && b7 != 4 && b7 != 5 && b7 != 7)
						break;
					BachTuoc bachTuoc = Mob.getBachTuoc();
					if (bachTuoc == null)
						break;
					if (b7 == 7)
					{
						int num4 = -1000;
						bachTuoc.yFirst = -1000;
						num4 = -1000;
						bachTuoc.xFirst = -1000;
						num4 = -1000;
						bachTuoc.yTo = -1000;
						num4 = -1000;
						bachTuoc.xTo = -1000;
						num4 = -1000;
						bachTuoc.y = -1000;
						bachTuoc.x = -1000;
						break;
					}
					if (b7 == 3 || b7 == 4)
					{
						sbyte b9 = msg.reader().readByte();
						Char[] array3 = new Char[b9];
						int[] array4 = new int[b9];
						for (int n = 0; n < b9; n++)
						{
							int num6 = msg.reader().readInt();
							array3[n] = null;
							if (num6 != Char.myCharz().charID)
								array3[n] = GameScr.findCharInMap(num6);
							else
								array3[n] = Char.myCharz();
							array4[n] = msg.reader().readInt();
						}
						bachTuoc.setAttack(array3, array4, b7);
					}
					if (b7 == 5)
						bachTuoc.move(msg.reader().readShort());
					break;
				}
				case 51:
				{
					Mabu mabu = (Mabu)GameScr.findCharInMap(msg.reader().readInt());
					sbyte id2 = msg.reader().readByte();
					short x = msg.reader().readShort();
					short y = msg.reader().readShort();
					sbyte b32 = msg.reader().readByte();
					Char[] array10 = new Char[b32];
					int[] array11 = new int[b32];
					for (int num40 = 0; num40 < b32; num40++)
					{
						int num41 = msg.reader().readInt();
						Res.outz("char ID=" + num41);
						array10[num40] = null;
						if (num41 != Char.myCharz().charID)
							array10[num40] = GameScr.findCharInMap(num41);
						else
							array10[num40] = Char.myCharz();
						array11[num40] = msg.reader().readInt();
					}
					mabu.setSkill(id2, x, y, array10, array11);
					break;
				}
				case 52:
				{
					sbyte b15 = msg.reader().readByte();
					if (b15 == 1)
					{
						int num18 = msg.reader().readInt();
						if (num18 == Char.myCharz().charID)
						{
							Char.myCharz().setMabuHold(true);
							Char.myCharz().cx = msg.reader().readShort();
							Char.myCharz().cy = msg.reader().readShort();
						}
						else
						{
							Char char5 = GameScr.findCharInMap(num18);
							if (char5 != null)
							{
								char5.setMabuHold(true);
								char5.cx = msg.reader().readShort();
								char5.cy = msg.reader().readShort();
							}
						}
					}
					if (b15 == 0)
					{
						int num19 = msg.reader().readInt();
						if (num19 == Char.myCharz().charID)
							Char.myCharz().setMabuHold(false);
						else
							GameScr.findCharInMap(num19)?.setMabuHold(false);
					}
					if (b15 == 2)
					{
						int charId2 = msg.reader().readInt();
						int id = msg.reader().readInt();
						((Mabu)GameScr.findCharInMap(charId2)).eat(id);
					}
					if (b15 == 3)
						GameScr.mabuPercent = msg.reader().readByte();
					break;
				}
				case 121:
					mSystem.publicID = msg.reader().readUTF();
					mSystem.strAdmob = msg.reader().readUTF();
					Res.outz("SHOW AD public ID= " + mSystem.publicID);
					mSystem.createAdmob();
					break;
				case 122:
				{
					short num17 = msg.reader().readShort();
					Res.outz("second login = " + num17);
					LoginScr.timeLogin = num17;
					LoginScr.currTimeLogin = (LoginScr.lastTimeLogin = mSystem.currentTimeMillis());
					GameCanvas.endDlg();
					break;
				}
				case 123:
				{
					Res.outz("SET POSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSss");
					int num13 = msg.reader().readInt();
					short xPos = msg.reader().readShort();
					short yPos = msg.reader().readShort();
					sbyte b12 = msg.reader().readByte();
					Char char4 = null;
					if (num13 == Char.myCharz().charID)
						char4 = Char.myCharz();
					else if (GameScr.findCharInMap(num13) != null)
					{
						char4 = GameScr.findCharInMap(num13);
					}
					if (char4 != null)
					{
						ServerEffect.addServerEffect((b12 != 0) ? 173 : 60, char4, 1);
						char4.setPos(xPos, yPos, b12);
					}
					break;
				}
				case 124:
				{
					short num12 = msg.reader().readShort();
					string text = msg.reader().readUTF();
					Res.outz("noi chuyen = " + text + "npc ID= " + num12);
					GameScr.findNPCInMap(num12)?.addInfo(text);
					break;
				}
				case 125:
				{
					sbyte fusion = msg.reader().readByte();
					int num7 = msg.reader().readInt();
					if (num7 == Char.myCharz().charID)
						Char.myCharz().setFusion(fusion);
					else if (GameScr.findCharInMap(num7) != null)
					{
						GameScr.findCharInMap(num7).setFusion(fusion);
					}
					break;
				}
				case -89:
					GameCanvas.open3Hour = msg.reader().readByte() == 1;
					break;
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
