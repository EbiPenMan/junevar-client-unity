using ProGraphGroup.General.Components.PageView.States;
using UnityEngine;
using UnityEngine.UI;

namespace ProGraphGroup.General.Components.PageView
{
    public class VerticalPageView : PageView
    {
        public VerticalViewMode pageViewMode;
        public VerticalLayoutGroup LayoutGroup { get; set; }

        public override void Awake()
        {
            Scroll = GetComponent<ScrollRect>();
            RectTransform = content.GetComponent<RectTransform>();
            items = new PageViewItem[content.childCount];
            for (int i = 0; i < content.childCount; i++)
            {
                items[i] = content.GetChild(i).gameObject.AddComponent<PageViewItem>();
                items[i].Index = i;
                items[i].NormalizedScrollPosition = (i / (float)(content.childCount - 1));
            }
        }

        public override void Start()
        {
            State = new PageViewEntryState(this);
        }

        public override void SetClosestItem()
        {
            float delta = 10000f;
            PageViewItem closestItem = null;
            for (int i = 0; i < items.Length; i++)
            {
                if (Mathf.Abs(Scroll.verticalNormalizedPosition - items[i].NormalizedScrollPosition) < delta)
                {
                    delta = Mathf.Abs(Scroll.verticalNormalizedPosition - items[i].NormalizedScrollPosition);
                    closestItem = items[i];
                }
            }
            TargetPageIndex = closestItem.Index;
        }

        public override void SwitchPageViewDirection(int mode)
        {
        }
    }

    public enum VerticalViewMode
    {
        None = 0,
        Middle = 1,
        Top = 2,
        Bottom = 3
    }
}