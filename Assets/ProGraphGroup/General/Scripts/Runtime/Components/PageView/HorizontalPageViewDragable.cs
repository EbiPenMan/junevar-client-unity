using UnityEngine;
using UnityEngine.UI;

namespace ProGraphGroup.General.Components.PageView
{
    public class HorizontalPageViewDragable : PageViewDragable
    {
        public HorizontalViewMode pageViewMode;
        public HorizontalLayoutGroup LayoutGroup { get; set; }

        public override void Awake()
        {
            LayoutGroup = content.GetComponent<HorizontalLayoutGroup>();
            if (LayoutGroup == null)
                LayoutGroup = content.gameObject.AddComponent<HorizontalLayoutGroup>();
            base.Awake();
        }

        public override void Start()
        {
            base.Start();
            LayoutGroup.childControlHeight = false;
            LayoutGroup.childControlWidth = false;
            for (int i = 0; i < content.childCount; i++)
            {
                RectTransform rect = content.GetChild(i).GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(Scroll.viewport.rect.width, Scroll.viewport.rect.height);
            }
            SwitchPageViewDirection((int)pageViewMode);
        }

        public override void SwitchPageViewDirection(int mode)
        {
            pageViewMode = (HorizontalViewMode)mode;

            switch (pageViewMode)
            {
                case HorizontalViewMode.None:
                    break;
                case HorizontalViewMode.Center:
                    LayoutGroup.childAlignment = TextAnchor.MiddleCenter;
                    RectTransform.anchorMin = new Vector2(0.5f, 0f);
                    RectTransform.anchorMax = new Vector2(0.5f, 1f);
                    RectTransform.pivot = new Vector2(0.5f, 0.5f);
                    break;
                case HorizontalViewMode.Left:
                    LayoutGroup.childAlignment = TextAnchor.MiddleLeft;
                    RectTransform.anchorMin = new Vector2(0f, 0f);
                    RectTransform.anchorMax = new Vector2(0f, 1f);
                    RectTransform.pivot = new Vector2(0f, 0.5f);
                    break;
                case HorizontalViewMode.Right:
                    LayoutGroup.childAlignment = TextAnchor.MiddleRight;
                    RectTransform.anchorMin = new Vector2(1f, 0f);
                    RectTransform.anchorMax = new Vector2(1f, 1f);
                    RectTransform.pivot = new Vector2(1f, 0.5f);
                    break;
                default:
                    Debug.LogError($"PageView Error: Page view mode not set!");
                    break;
            }
        }

        public override void SetClosestItem()
        {
            float delta = 10000f;
            PageViewItem closestItem = null;
            for (int i = 0; i < items.Length; i++)
            {
                if (Mathf.Abs(Scroll.horizontalNormalizedPosition - items[i].NormalizedScrollPosition) < delta)
                {
                    delta = Mathf.Abs(Scroll.horizontalNormalizedPosition - items[i].NormalizedScrollPosition);
                    closestItem = items[i];
                }
            }
            TargetPageIndex = closestItem.Index;
        }
    }


}