namespace ProGraphGroup.General.Components.PageView.States
{
    public class PageViewDraggingState : PageViewState
    {
        public override string StateName { get => "Dragging"; }

        public PageViewDraggingState(global::ProGraphGroup.General.Components.PageView.IPageView pageView)
        {
            OnStateEnter(pageView);
        }

        public override void OnStateEnter(global::ProGraphGroup.General.Components.PageView.IPageView pageView)
        {
        }

        public override void OnStateExit(global::ProGraphGroup.General.Components.PageView.IPageView pageView)
        {
        }

        public override void OnStateUpdate(global::ProGraphGroup.General.Components.PageView.IPageView pageView)
        {
        }
    }
}
