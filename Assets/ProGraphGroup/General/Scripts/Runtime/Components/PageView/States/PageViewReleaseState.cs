using UnityEngine;

namespace ProGraphGroup.General.Components.PageView.States
{
    public class PageViewReleaseState : PageViewState
    {
        float normalizedPos = 0f;
        float delta = 0f;
        float step = 0f;
        bool isHorizontal;

        public override string StateName { get => "Release"; }

        public PageViewReleaseState(global::ProGraphGroup.General.Components.PageView.IPageView pageView)
        {
            OnStateEnter(pageView);
        }

        public override void OnStateEnter(global::ProGraphGroup.General.Components.PageView.IPageView pageView)
        {
            if (pageView.PointerData != null && Mathf.Abs(pageView.PointerData.delta.x) > 2f)
            {
                isHorizontal = true;
                if (pageView.PointerData.delta.x < 0)
                {
                    pageView.TargetPageIndex =
                        pageView.GetContent.childCount - 1 == pageView.TargetPageIndex ?
                        pageView.TargetPageIndex :
                        pageView.TargetPageIndex + 1;
                }
                else if (pageView.PointerData.delta.x > 0)
                {
                    pageView.TargetPageIndex =
                        pageView.TargetPageIndex == 0 ?
                        pageView.TargetPageIndex :
                        pageView.TargetPageIndex - 1;
                }
            }
            else if (pageView.PointerData != null && Mathf.Abs(pageView.PointerData.delta.y) > 2f)
            {
                isHorizontal = false;
                if (pageView.PointerData.delta.y < 0)
                {
                    pageView.TargetPageIndex =
                        pageView.GetContent.childCount - 1 == pageView.TargetPageIndex ?
                        pageView.TargetPageIndex :
                        pageView.TargetPageIndex + 1;
                }
                else if (pageView.PointerData.delta.y > 0)

                {
                    pageView.TargetPageIndex =
                        pageView.TargetPageIndex == 0 ?
                        pageView.TargetPageIndex :
                        pageView.TargetPageIndex - 1;
                }
            }
            pageView.PointerData = null;
            normalizedPos = pageView.items[pageView.TargetPageIndex].NormalizedScrollPosition;
            delta = isHorizontal ? pageView.Scroll.horizontalNormalizedPosition - normalizedPos : pageView.Scroll.verticalNormalizedPosition - normalizedPos;
            step = Mathf.Abs(delta) / (pageView.GetScrollToItemSpeed * 60f);
        }

        public override void OnStateExit(global::ProGraphGroup.General.Components.PageView.IPageView pageView)
        {
        }

        public override void OnStateUpdate(global::ProGraphGroup.General.Components.PageView.IPageView pageView)
        {
            if (Mathf.Abs(delta) > 0.0001f)
            {
                if (delta > 0)
                {
                    if (isHorizontal)
                        pageView.Scroll.horizontalNormalizedPosition -= step;
                    else
                        pageView.Scroll.verticalNormalizedPosition -= step;
                }
                else if (delta < 0)
                {
                    if (isHorizontal)
                        pageView.Scroll.horizontalNormalizedPosition += step;
                    else
                        pageView.Scroll.verticalNormalizedPosition += step;
                }
                delta = isHorizontal ? pageView.Scroll.horizontalNormalizedPosition - normalizedPos : pageView.Scroll.verticalNormalizedPosition - normalizedPos;
            }
            else
            {
                if (isHorizontal)
                    pageView.Scroll.horizontalNormalizedPosition = normalizedPos;
                else
                    pageView.Scroll.verticalNormalizedPosition = normalizedPos;
                pageView.State.ChangeState(pageView, new PageViewEntryState(pageView));
            }
        }
    }
}
