using ProGraphGroup.General.Components.PageView.States;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProGraphGroup.General.Components.PageView
{
    public abstract class PageView : MonoBehaviour, IPageView
    {
        public Transform content;
        public ScrollMode scrollMode;
        public float scrollToItemSpeed = 0.5f;
        [Range(10, 500)] public float stopThreshold = 100f;
        public bool scaleEffect = false;
        public bool fadeEffect = false;


        public PageViewState State { get; set; }
        public ContentSizeFitter SizeFitter { get; set; }
        public RectTransform RectTransform { get; set; }
        public ScrollRect Scroll { get; set; }
        public PageViewItem[] items { get; set; }
        public PointerEventData PointerData { get; set; }
        public int TargetPageIndex { get; set; }

        public Transform GetContent
        {
            get => content;
            set => content = value;
        }

        public float GetScrollToItemSpeed
        {
            get => scrollToItemSpeed;
            set => scrollToItemSpeed = value;
        }

        public abstract void SwitchPageViewDirection(int mode);
        public abstract void SetClosestItem();

        public virtual void Awake()
        {
            Scroll = GetComponent<ScrollRect>();
            SizeFitter = content.GetComponent<ContentSizeFitter>();
            if (SizeFitter == null)
                SizeFitter = content.gameObject.AddComponent<ContentSizeFitter>();
            RectTransform = content.GetComponent<RectTransform>();
            if (RectTransform == null)
                RectTransform = content.gameObject.AddComponent<RectTransform>();
            items = new PageViewItem[content.childCount];
            for (int i = 0; i < content.childCount; i++)
            {
                items[i] = content.GetChild(i).gameObject.AddComponent<PageViewItem>();
                items[i].Index = i;
                items[i].NormalizedScrollPosition = (i / (float) (content.childCount - 1));
            }
        }

        public virtual void Start()
        {
            State = new PageViewEntryState(this);
            SizeFitter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
            SizeFitter.verticalFit = ContentSizeFitter.FitMode.MinSize;
        }

        public virtual void Update()
        {
            State.OnStateUpdate(this);
        }

        public virtual void ScrollToItem(int index)
        {
            TargetPageIndex = index;
            State.ChangeState(this, new PageViewReleaseState(this));
        }


    }
}