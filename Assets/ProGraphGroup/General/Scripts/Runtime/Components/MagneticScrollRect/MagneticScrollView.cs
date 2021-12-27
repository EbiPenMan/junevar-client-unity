using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProGraphGroup.General.Components.MagneticScrollRect
{
    public class MagneticScrollView : MonoBehaviour, IEndDragHandler, IBeginDragHandler
    {
        public Sprite resultPage;
        public ToggleGroup toggleGroup;
        public Transform highlight;
        public float snapThreshold = 60f;
        public float snapSpeedInSeconds = 0.2f;
        public Action<Sprite> OnSelectedItemChanged { get; set; }
        private ScrollRect scrollRect;
        //private VerticalLayoutGroup layout;
        private bool isDragging;
        private bool isSnapping;
        private bool isSnapped;
        private int activeToggleIndex = -1;

        private void Awake()
        {
            scrollRect = GetComponent<ScrollRect>();
            //layout = scrollRect.content.GetComponent<VerticalLayoutGroup>();
            scrollRect.onValueChanged.AddListener(v2 => { OnScrollRectValueChanged(v2); });
        }

        private void OnEnable()
        {
            var paddingValue = Mathf.FloorToInt(highlight.GetComponent<RectTransform>().sizeDelta.y);
            //layout.padding.top = paddingValue;
            //layout.padding.bottom = paddingValue;
            //SnapToClosestItem();
        }

        private void Update()
        {
            if (isSnapped == false && isSnapping == false && isDragging == false && scrollRect.velocity.y < snapThreshold)
            {
                scrollRect.StopMovement();
                Debug.Log($"Snapping to closest item");
                SnapToClosestItem();
            }
        }

        private void OnScrollRectValueChanged(Vector2 v2)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
            Debug.Log($"Drag Ended On Scroll Rect!");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isSnapped = false;
            isDragging = true;
            Debug.Log($"Drag Started On Scroll Rect!");
        }

        private void SnapToClosestItem()
        {
            Transform closestItem = null;
            Transform currentItem = null;
            int closestIndex = -1;
            float delta = 0;
            float closestDistance = 1080;
            float centerPoint = highlight.position.y;
            Debug.Log($"viewport.y {centerPoint}");
            for (int i = 0; i < scrollRect.content.childCount; i++)
            {
                currentItem = scrollRect.content.GetChild(i);
                delta = currentItem.position.y - centerPoint;
                if (Mathf.Abs(delta) < Mathf.Abs(closestDistance))
                {
                    closestItem = currentItem;
                    closestIndex = i;
                    closestDistance = delta;
                }
            }
            Debug.Log($"Closest index : {closestIndex} , delta {closestDistance}");
            StartCoroutine(LerpToItem(closestItem, closestItem.position.y, closestDistance));
        }

        private IEnumerator LerpToItem(Transform closestItem, float targetPos, float delta)
        {
            isSnapping = true;
            float step = delta / snapSpeedInSeconds;
            Debug.Log($"delta : {delta} , target pos : {targetPos}");
            scrollRect.content.position += new Vector3(0, delta, 0);
            //while (Mathf.Abs(scrollRect.content.position.y - targetPos) > 0.1f)
            //{
            //    Debug.Log($"Target Pos : {targetPos}, Content Pos : {scrollRect.content.position.y}");
            //    scrollRect.content.position += new Vector3(0, step * Time.deltaTime, 0);
            //    yield return null;
            //}
            Debug.Log($"Snap lerp finished.");
            isSnapped = true;
            yield return null;
            OnSelectedItemChanged?.Invoke(closestItem.GetComponent<Sprite>());
            isSnapping = false;
            yield break;
        }

    }
}