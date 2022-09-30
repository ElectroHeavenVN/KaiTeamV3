public class Skill
{
	public const sbyte ATT_STAND = 0;

	public const sbyte ATT_FLY = 1;

	public const sbyte SKILL_AUTO_USE = 0;

	public const sbyte SKILL_CLICK_USE_ATTACK = 1;

	public const sbyte SKILL_CLICK_USE_BUFF = 2;

	public const sbyte SKILL_CLICK_NPC = 3;

	public const sbyte SKILL_CLICK_LIVE = 4;

	public SkillTemplate template;

	public short skillId;

	public int point;

	public long powRequire;

	public int coolDown;

	public long lastTimeUseThisSkill;

	public int dx;

	public int dy;

	public int maxFight;

	public int manaUse;

	public SkillOption[] options;

	public bool paintCanNotUseSkill;

	public short damage;

	public string moreInfo;

	public short price;

	public string strTimeReplay()
	{
		if (coolDown % 1000 == 0)
			return coolDown / 1000 + string.Empty;
		int num = coolDown % 1000;
		return coolDown / 1000 + "." + ((num % 100 != 0) ? (num / 10) : (num / 100));
	}

	public void paint(int x, int y, mGraphics g)
	{
		SmallImage.drawSmallImage(g, template.iconId, x, y, 0, StaticObj.VCENTER_HCENTER);
		long num = mSystem.currentTimeMillis();
		long num2 = coolDown + (lastTimeUseThisSkill - mSystem.currentTimeMillis());
		long num3 = num - lastTimeUseThisSkill;
		if (num3 < coolDown)
		{
			g.setColor(2721889, 0.7f);
			if (paintCanNotUseSkill && GameCanvas.gameTick % 6 > 2)
				g.setColor(876862);
			int num4 = (int)(num3 * 20L / coolDown);
			g.fillRect(x - 10, y - 10 + num4, 20, 20 - num4);
			mFont.tahoma_7b_dark.drawString(g, string.Concat((int)num2 / 1000), x, y - 5, mFont.CENTER);
		}
		else
			paintCanNotUseSkill = false;
	}
}
