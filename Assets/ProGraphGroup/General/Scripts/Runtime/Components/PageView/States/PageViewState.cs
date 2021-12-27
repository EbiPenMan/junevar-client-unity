namespace ProGraphGroup.General.Components.PageView.States
{
    public abstract class PageViewState
    {
        public abstract string StateName { get; }
        public abstract void OnStateEnter(global::ProGraphGroup.General.Components.PageView.IPageView pageView);
        public abstract void OnStateExit(global::ProGraphGroup.General.Components.PageView.IPageView pageView);
        public abstract void OnStateUpdate(global::ProGraphGroup.General.Components.PageView.IPageView pageView);
        public virtual void ChangeState(global::ProGraphGroup.General.Components.PageView.IPageView pageView, PageViewState newState)
        {
            pageView.State.OnStateExit(pageView);
            pageView.State = newState;
        }
    }
}