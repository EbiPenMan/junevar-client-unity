using ProGraphGroup.General.Components.PageView.States;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProGraphGroup.General.Components.PageView
{
    public interface IPageView 
    {
        
         PageViewState State { get; set; }
         ContentSizeFitter SizeFitter { get; set; }
         RectTransform RectTransform { get; set; }
         ScrollRect Scroll { get; set; }
         PageViewItem[] items { get; set; }
         PointerEventData PointerData { get; set; }
         int TargetPageIndex { get; set; }

         Transform GetContent { get; set; }

         float GetScrollToItemSpeed  { get; set; }
             
        void SwitchPageViewDirection(int mode);
        void SetClosestItem();

        void Awake();

        void Start();

        void Update();
        
        void ScrollToItem(int index);
    }
}