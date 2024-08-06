using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelArm.Modules
{
    internal static class StringBuilderExt
    {
        /// <summary>
        /// 속도는 미세하게 느리지만, 새로운 객체를 복사하지 않도록 만든 StringBuilder IndexOf() 확장 함수입니다.
        /// </summary>
        /// <param name="stringBuilder">사용할 StringBuilder 객체입니다.</param>
        /// <param name="value">찾을 문자열입니다.</param>
        /// <param name="startIndex">검색을 시작할 색인값입니다.</param>
        /// <param name="ignoreCase">True: 대소문자 검사를 무시합니다.</param>
        /// <returns></returns>
        internal static int IndexOfExt(this StringBuilder stringBuilder, string value, int startIndex = 0, bool ignoreCase = false)
        {
            if (String.IsNullOrEmpty(value))
                return -1;

            int len = value.Length;
            int max = (stringBuilder.Length - len) + 1;
            var v1 = (ignoreCase)
                ? value.ToLower() : value;
            var func1 = (ignoreCase)
                ? new Func<char, char, bool>((x, y) => char.ToLower(x) == y)
                : new Func<char, char, bool>((x, y) => x == y);
            for (int i1 = startIndex; i1 < max; ++i1)
                if (func1(stringBuilder[i1], v1[0]))
                {
                    int i2 = 1;
                    while ((i2 < len) && func1(stringBuilder[i1 + i2], v1[i2]))
                        ++i2;
                    if (i2 == len)
                        return i1;
                }
            return -1;
        }

        /// <summary>
        /// 특정 단어가 나오는 구간의 데이터를 모두 삭제하여 업데이트하는 StringBuilder 확장 함수입니다.
        /// </summary>
        /// <param name="stringBuilder">업데이트할 기존의 StringBuilder 객체</param>
        /// <param name="value">찾을 문자열</param>
        /// <param name="removeType">0: 찾은 값 이전의 데이터를 모두 제거 || 1: 찾은 값까지 모두 제거 || 2: 찾은 값을 포함해 이후의 데이터를 모두 제거</param>
        /// <param name="ignoreCase">True: 대소문자 검사를 무시합니다.</param>
        internal static void RemoveExt(this StringBuilder stringBuilder, string value, byte removeType = 0, bool ignoreCase = false)
        {
            int valueIndex = IndexOfExt(stringBuilder, value, 0, ignoreCase);
            int removeIndex = valueIndex;
            if (removeType == 1)
                removeIndex = valueIndex + value.Length;
            else if (removeType == 2)
            {
                stringBuilder.Remove(valueIndex, stringBuilder.Length - valueIndex);
                return;
            }

            stringBuilder.Remove(0, removeIndex);
        }

        /*
        internal static bool Contains(this StringBuilder haystack, string needle)
        {
            return haystack.IndexOf(needle) != -1;
        }

        internal static int IndexOf(this StringBuilder haystack, string needle)
        {
            if (haystack == null || needle == null)
                throw new ArgumentNullException();
            if (needle.Length == 0)
                return 0;//empty strings are everywhere!
            if (needle.Length == 1)//can't beat just spinning through for it
            {
                char c = needle[0];
                for (int idx = 0; idx != haystack.Length; ++idx)
                    if (haystack[idx] == c)
                        return idx;
                return -1;
            }
            int m = 0;
            int i = 0;
            int[] T = KMPTable(needle);
            while (m + i < haystack.Length)
            {
                if (needle[i] == haystack[m + i])
                {
                    if (i == needle.Length - 1)
                        return m == needle.Length ? -1 : m;//match -1 = failure to find conventional in .NET
                    ++i;
                }
                else
                {
                    m = m + i - T[i];
                    i = T[i] > -1 ? T[i] : 0;
                }
            }
            return -1;
        }

        private static int[] KMPTable(string sought)
        {
            int[] table = new int[sought.Length];
            int pos = 2;
            int cnd = 0;
            table[0] = -1;
            table[1] = 0;
            while (pos < table.Length)
                if (sought[pos - 1] == sought[cnd])
                    table[pos++] = ++cnd;
                else if (cnd > 0)
                    cnd = table[cnd];
                else
                    table[pos++] = 0;
            return table;
        }
        */
    }
}
