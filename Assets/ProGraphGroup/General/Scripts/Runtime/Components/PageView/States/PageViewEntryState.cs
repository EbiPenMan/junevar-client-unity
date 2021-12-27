namespace ProGraphGroup.General.Components.PageView.States
{
    public class PageViewEntryState : PageViewState
    {
        public override string StateName { get => "Entry"; }

        public PageViewEntryState(global::ProGraphGroup.General.Components.PageView.IPageView pageView)
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
