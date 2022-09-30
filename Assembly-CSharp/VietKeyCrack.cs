public class VietKeyCrack
{
	public static string ToVietnamese(string str)
	{
		int length = str.Length;
		if (str.Length < 2)
			return str;
		switch (str.Substring(length - 2))
		{
		case "as":
			return str.Substring(0, length - 2) + "á";
		case "ás":
			return str.Substring(0, length - 2) + "as";
		case "af":
			return str.Substring(0, length - 2) + "à";
		case "àf":
			return str.Substring(0, length - 2) + "af";
		case "ar":
			return str.Substring(0, length - 2) + "ả";
		case "ảr":
			return str.Substring(0, length - 2) + "ar";
		case "ax":
			return str.Substring(0, length - 2) + "ã";
		case "ãx":
			return str.Substring(0, length - 2) + "ax";
		case "aj":
			return str.Substring(0, length - 2) + "ạ";
		case "ạj":
			return str.Substring(0, length - 2) + "aj";
		case "aw":
			return str.Substring(0, length - 2) + "ă";
		case "ăw":
			return str.Substring(0, length - 2) + "aw";
		case "ăs":
			return str.Substring(0, length - 2) + "ắ";
		case "ắs":
			return str.Substring(0, length - 2) + "ăs";
		case "ăf":
			return str.Substring(0, length - 2) + "ằ";
		case "ằf":
			return str.Substring(0, length - 2) + "ăf";
		case "ăr":
			return str.Substring(0, length - 2) + "ẳ";
		case "ẳr":
			return str.Substring(0, length - 2) + "ăr";
		case "ăx":
			return str.Substring(0, length - 2) + "ẵ";
		case "ẵ":
			return str.Substring(0, length - 2) + "ăx";
		case "ăj":
			return str.Substring(0, length - 2) + "ặ";
		case "ặj":
			return str.Substring(0, length - 2) + "ăj";
		case "aa":
			return str.Substring(0, length - 2) + "â";
		case "âa":
			return str.Substring(0, length - 2) + "aa";
		case "âs":
			return str.Substring(0, length - 2) + "ấ";
		case "ấs":
			return str.Substring(0, length - 2) + "âs";
		case "âf":
			return str.Substring(0, length - 2) + "ầ";
		case "ầf":
			return str.Substring(0, length - 2) + "âf";
		case "âr":
			return str.Substring(0, length - 2) + "ẩ";
		case "ẩr":
			return str.Substring(0, length - 2) + "âr";
		case "âx":
			return str.Substring(0, length - 2) + "ẫ";
		case "ẫx":
			return str.Substring(0, length - 2) + "âx";
		case "âj":
			return str.Substring(0, length - 2) + "ậ";
		case "ậj":
			return str.Substring(0, length - 2) + "âj";
		case "es":
			return str.Substring(0, length - 2) + "é";
		case "és":
			return str.Substring(0, length - 2) + "es";
		case "ef":
			return str.Substring(0, length - 2) + "è";
		case "èf":
			return str.Substring(0, length - 2) + "ef";
		case "er":
			return str.Substring(0, length - 2) + "ẻ";
		case "ẻr":
			return str.Substring(0, length - 2) + "er";
		case "ex":
			return str.Substring(0, length - 2) + "ẽ";
		case "ẽx":
			return str.Substring(0, length - 2) + "ex";
		case "ej":
			return str.Substring(0, length - 2) + "ẹ";
		case "ẹj":
			return str.Substring(0, length - 2) + "ej";
		case "ee":
			return str.Substring(0, length - 2) + "ê";
		case "êe":
			return str.Substring(0, length - 2) + "ee";
		case "ês":
			return str.Substring(0, length - 2) + "ế";
		case "ếs":
			return str.Substring(0, length - 2) + "ês";
		case "êf":
			return str.Substring(0, length - 2) + "ề";
		case "ềf":
			return str.Substring(0, length - 2) + "êf";
		case "êr":
			return str.Substring(0, length - 2) + "ể";
		case "ểr":
			return str.Substring(0, length - 2) + "êr";
		case "êx":
			return str.Substring(0, length - 2) + "ễ";
		case "ễx":
			return str.Substring(0, length - 2) + "êx";
		case "êj":
			return str.Substring(0, length - 2) + "ệ";
		case "ệj":
			return str.Substring(0, length - 2) + "êj";
		case "is":
			return str.Substring(0, length - 2) + "í";
		case "ís":
			return str.Substring(0, length - 2) + "is";
		case "if":
			return str.Substring(0, length - 2) + "ì";
		case "ìf":
			return str.Substring(0, length - 2) + "if";
		case "ir":
			return str.Substring(0, length - 2) + "ỉ";
		case "ỉr":
			return str.Substring(0, length - 2) + "ir";
		case "ix":
			return str.Substring(0, length - 2) + "ĩ";
		case "ĩx":
			return str.Substring(0, length - 2) + "ix";
		case "ij":
			return str.Substring(0, length - 2) + "ị";
		case "ịj":
			return str.Substring(0, length - 2) + "ij";
		case "os":
			return str.Substring(0, length - 2) + "ó";
		case "ós":
			return str.Substring(0, length - 2) + "os";
		case "of":
			return str.Substring(0, length - 2) + "ò";
		case "òf":
			return str.Substring(0, length - 2) + "of";
		case "or":
			return str.Substring(0, length - 2) + "ỏ";
		case "ỏr":
			return str.Substring(0, length - 2) + "or";
		case "ox":
			return str.Substring(0, length - 2) + "õ";
		case "õx":
			return str.Substring(0, length - 2) + "ox";
		case "oj":
			return str.Substring(0, length - 2) + "ọ";
		case "ọj":
			return str.Substring(0, length - 2) + "oj";
		case "oo":
			return str.Substring(0, length - 2) + "ô";
		case "ôo":
			return str.Substring(0, length - 2) + "oo";
		case "ôs":
			return str.Substring(0, length - 2) + "ố";
		case "ốs":
			return str.Substring(0, length - 2) + "ôs";
		case "ôf":
			return str.Substring(0, length - 2) + "ồ";
		case "ồf":
			return str.Substring(0, length - 2) + "ôf";
		case "ôr":
			return str.Substring(0, length - 2) + "ổ";
		case "ổr":
			return str.Substring(0, length - 2) + "ôr";
		case "ôx":
			return str.Substring(0, length - 2) + "ỗ";
		case "ỗx":
			return str.Substring(0, length - 2) + "ôx";
		case "ôj":
			return str.Substring(0, length - 2) + "ộ";
		case "ộj":
			return str.Substring(0, length - 2) + "ôj";
		case "ow":
			return str.Substring(0, length - 2) + "ơ";
		case "ơw":
			return str.Substring(0, length - 2) + "ow";
		case "ơs":
			return str.Substring(0, length - 2) + "ớ";
		case "ớs":
			return str.Substring(0, length - 2) + "ơs";
		case "ơf":
			return str.Substring(0, length - 2) + "ờ";
		case "ờf":
			return str.Substring(0, length - 2) + "ơf";
		case "ơr":
			return str.Substring(0, length - 2) + "ở";
		case "ởr":
			return str.Substring(0, length - 2) + "ơr";
		case "ơx":
			return str.Substring(0, length - 2) + "ỡ";
		case "ỡx":
			return str.Substring(0, length - 2) + "ơx";
		case "ơj":
			return str.Substring(0, length - 2) + "ợ";
		case "ợj":
			return str.Substring(0, length - 2) + "ơj";
		case "us":
			return str.Substring(0, length - 2) + "ú";
		case "ús":
			return str.Substring(0, length - 2) + "us";
		case "uf":
			return str.Substring(0, length - 2) + "ù";
		case "ùf":
			return str.Substring(0, length - 2) + "uf";
		case "ur":
			return str.Substring(0, length - 2) + "ủ";
		case "ủr":
			return str.Substring(0, length - 2) + "ur";
		case "ux":
			return str.Substring(0, length - 2) + "ũ";
		case "ũx":
			return str.Substring(0, length - 2) + "ux";
		case "uj":
			return str.Substring(0, length - 2) + "ụ";
		case "ụj":
			return str.Substring(0, length - 2) + "uj";
		case "uw":
			return str.Substring(0, length - 2) + "ư";
		case "ưw":
			return str.Substring(0, length - 2) + "uu";
		case "ưs":
			return str.Substring(0, length - 2) + "ứ";
		case "ứs":
			return str.Substring(0, length - 2) + "ưs";
		case "ưf":
			return str.Substring(0, length - 2) + "ừ";
		case "ừf":
			return str.Substring(0, length - 2) + "ưf";
		case "ưr":
			return str.Substring(0, length - 2) + "ử";
		case "ửr":
			return str.Substring(0, length - 2) + "ưr";
		case "ưx":
			return str.Substring(0, length - 2) + "ữ";
		case "ữx":
			return str.Substring(0, length - 2) + "ưx";
		case "ưj":
			return str.Substring(0, length - 2) + "ự";
		case "ựj":
			return str.Substring(0, length - 2) + "ưj";
		case "ys":
			return str.Substring(0, length - 2) + "ý";
		case "ýs":
			return str.Substring(0, length - 2) + "ys";
		case "yf":
			return str.Substring(0, length - 2) + "ỳ";
		case "ỳf":
			return str.Substring(0, length - 2) + "yf";
		case "yr":
			return str.Substring(0, length - 2) + "ỷ";
		case "ỷr":
			return str.Substring(0, length - 2) + "yr";
		case "yx":
			return str.Substring(0, length - 2) + "ỹ";
		case "ỹx":
			return str.Substring(0, length - 2) + "yx";
		case "yj":
			return str.Substring(0, length - 2) + "ỵ";
		case "ỵj":
			return str.Substring(0, length - 2) + "yj";
		case "dd":
			return str.Substring(0, length - 2) + "đ";
		case "đd":
			return str.Substring(0, length - 2) + "dd";
		default:
			return str;
		}
	}
}
