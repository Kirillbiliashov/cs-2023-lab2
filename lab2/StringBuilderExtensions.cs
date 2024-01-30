using System;
using System.Text;

namespace lab2
{
	public static class StringBuilderExtensions
	{

		public static int IndexOf(this StringBuilder sb, string str, int fromIdx)
		{
			for (int i = fromIdx; i < sb.Length; i++)
			{
				if (sb[i].ToString() == str)
				{
					return i;
				}
			}
			return -1;
		}

		public static string Substring(this StringBuilder sb, int fromIdx, int? toIdx = null)
		{
			if (toIdx == null)
			{
				toIdx = sb.Length;
			}
			if (toIdx < fromIdx)
			{
				throw new ArgumentOutOfRangeException(nameof(toIdx), "Provide valid idx range");
			}
			var newSb = new StringBuilder();
			for (int i = fromIdx; i < toIdx; i++)
			{
				newSb.Append(sb[i]);
			}
			return newSb.ToString();
		}

		public static void Replace(this StringBuilder sb, int fromIdx, int toIdx, string newStr)
		{
			sb.Remove(fromIdx, toIdx - fromIdx);
			sb.Insert(fromIdx, newStr);
		}
	}
}

