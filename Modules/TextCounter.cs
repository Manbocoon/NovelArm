using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace NovelArm.Modules
{
    internal class TextCounter : IDisposable
    {
        #region Properties - Convert Methods
        internal static readonly bool REMOVE_NEWLINE = true;
        internal static bool REMOVE_BLANKS { get; set; } = true;
        internal static bool REMOVE_MARKS { get; set; } = true;
        internal static bool PROCESS_SPECIALMARKS { get; set; } = true;

        private static readonly string PUNCTUATION_MARKS = ".,!?'\"";
        #endregion

        public void Dispose() { }

        internal static int CalculateDraft(StringBuilder draft)
        {
            if (REMOVE_NEWLINE)
                draft.Replace("\r", null).Replace("\n", null);
            
            if (REMOVE_BLANKS)
                draft.Replace(" ", null).Replace("\t", null);

            if (REMOVE_MARKS)
                foreach (char mark in PUNCTUATION_MARKS)
                    draft.Replace(mark.ToString(), null);

            /* 노벨피아식 특수문자 처리
                    부등호 ><는 4글자로, &는 5글자로 계산됨.
                     ≪︎《︎『︎〔︎ 등은 각 2글자씩 인정됨.
            */
            if (PROCESS_SPECIALMARKS)
            {
                draft.Replace("<", "<<<<").Replace(">", ">>>>").Replace("&", "&&&&&").Replace("≪︎", "≪︎≪︎").Replace("《︎", "《︎《︎").Replace("『︎", "『︎『︎").Replace("〔︎", "〔︎〔︎");
            }

            return draft.Length;
        }




    }
}
