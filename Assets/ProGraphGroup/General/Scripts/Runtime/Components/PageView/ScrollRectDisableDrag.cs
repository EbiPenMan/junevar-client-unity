using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProGraphGroup.General.Components.PageView
{
    public class ScrollRectDisableDrag : ScrollRect
    {
        public override void OnBeginDrag(PointerEventData eventData) { }
        public override void OnDrag(PointerEventData eventData) { }
        public override void OnEndDrag(PointerEventData eventData) { }
    }
}