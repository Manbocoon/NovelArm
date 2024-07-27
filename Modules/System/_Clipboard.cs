using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using forms = System.Windows.Forms;
using Windows.ApplicationModel.DataTransfer;
using System.Threading;

namespace NovelArm.Modules.System
{
    internal static class _Clipboard
    {
        #region Properties
        internal static ClipboardHistoryItemsResult systemClipboard;
        #endregion

        /// <summary>
        /// 사용자가 Windows 클립보드 기록 설정을 활성화했는지의 여부를 반환합니다.
        /// </summary>
        /// <returns>Windows 클립보드 기록 활성화 여부</returns>
        internal static bool IsHistoryEnabled()
        {
            return Clipboard.IsHistoryEnabled();
        }
        
        /// <summary>
        /// Windows 클립보드 기록을 모두 지웁니다.
        /// </summary>
        internal static void ClearHistory()
        {
            Clipboard.ClearHistory();
        }

        /// <summary>
        /// WinRT API에서 ClipboardHistoryItem의 데이터가 텍스트인지 판단합니다.
        /// </summary>
        /// <param name="cItem">ClipboardHistoryItem 항목</param>
        internal static bool DataIsText(ClipboardHistoryItem cItem)
        {
            if (cItem == null)
                return false;

            return cItem.Content.AvailableFormats.Contains<string>("Text");
        }

        /// <summary>
        /// WinRT API를 통해 클립보드 기록에서 특정 텍스트 데이터를 삭제합니다.
        /// </summary>
        /// <param name="text">데이터를 찾기 위한 텍스트 값</param>
        internal static async void RemoveExactItem(long timeStamp)
        {
            if (timeStamp < 999)           
                return;

            systemClipboard = await Clipboard.GetHistoryItemsAsync();
            foreach (ClipboardHistoryItem item in systemClipboard.Items)
            {
                if (!DataIsText(item))
                    continue;
                
                // Timestamp가 일치하는 특정 클립보드 항목 제거
                if (item.Timestamp.Ticks == timeStamp)
                {
                    Clipboard.DeleteItemFromHistory(item);
                    break;
                }
                
            }
        }


        /// <summary>
        /// WinRT API를 통해 클립보드 내에서 오래된 중복 텍스트 값들을 모두 제거합니다.
        /// </summary>
        /// <param name="textContent">클립보드 특정 항목을 찾기 위한 텍스트 값입니다.</param>
        /// <param name="workReverse">반대로 동작합니다. 중복값을 제거하지 않고 최신 값만 제거합니다.</param>
        internal static async void RemoveDuplicateItems(string textContent, bool workReverse = false)
        {
            if (String.IsNullOrEmpty(textContent))
                return;

            systemClipboard = await Clipboard.GetHistoryItemsAsync();

            List<ClipboardHistoryItem> cbItems = new List<ClipboardHistoryItem>();
            List<long> cbItemTimestamps = new List<long>();
            foreach (ClipboardHistoryItem item in systemClipboard.Items)
            {
                if (!DataIsText(item))
                    continue;

                // Text값이 일치하는 모든 항목들을 저장
                string itemTextData = await item.Content.GetTextAsync(StandardDataFormats.Text);
                if (TextConverter.ChangeNewLine(ref itemTextData) == TextConverter.ChangeNewLine(ref textContent))
                {
                    cbItems.Add(item);
                    cbItemTimestamps.Add(item.Timestamp.Ticks);
                }
            }

            // 겹치는 값이 없다면 취소
            if (cbItems.Count < 2)
                return;

            // 가장 최신의 데이터 얻기
            int latestItemIndex = cbItemTimestamps.IndexOf(cbItemTimestamps.Max());

            // 파라미터에 따라 중복값을 모두 제거하거나, 최신값만 제거
            if (workReverse)
                Clipboard.DeleteItemFromHistory(cbItems[latestItemIndex]);
            else
            {
                cbItems.RemoveAt(latestItemIndex);
                foreach (ClipboardHistoryItem item in cbItems)
                    Clipboard.DeleteItemFromHistory(item);
            }
            
        }

        /// <summary>
        /// WinRT API를 통해 클립보드 특정 항목을 텍스트를 통해 찾고, Timestamp 값을 저장합니다.
        /// </summary>
        /// <param name="text">클립보드 특정 항목을 찾기 위한 텍스트 값입니다.</param>
        internal static async void GetTimestampFromItem(string text)
        {
            if (String.IsNullOrEmpty(text))
                return;

            systemClipboard = await Clipboard.GetHistoryItemsAsync();
            foreach (ClipboardHistoryItem item in systemClipboard.Items)
            {
                if (!DataIsText(item))
                    continue;

                // 특정 항목 찾기
                string itemTextData = await item.Content.GetTextAsync(StandardDataFormats.Text);
                if (itemTextData == text)
                {
                    // TextConverter.lastCbDataTimestamp = item.Timestamp.Ticks;
                    break;
                }
            }
        }


        internal static void OnContentChanged(object sender, object evt)
        {
            if (!TextConverter.cbWorking)
                TextConverter.cbChanged = true;
        }
    }
}
