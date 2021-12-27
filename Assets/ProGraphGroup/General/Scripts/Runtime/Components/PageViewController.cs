using UnityEngine;
using UnityEngine.EventSystems;

namespace ProGraphGroup.General.Components
{
    public class PageViewController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler
    {
        public enum Direction
        {
            Horizontal,
            Vertical
        }

        public Direction direction;

        public RectTransform content;

        public int pageCount;

        public float pageWidth = 100;

        public float elasticity = 0.1f;

        [Range(0.1f, 0.5f), Tooltip("滑动结束判断更改Page的阈值")]
        public float threshold = 0.4f;

        public float velocityThreshold = 30f;

        public int page;

        public float contentOffset;

        float currentVelocity;

        float value;

        float targetValue;

        bool isDrag = false;

        bool isDamping = true;

        public void OnBeginDrag(PointerEventData eventData)
        {
            isDrag = true;
            currentVelocity = 0;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (direction == Direction.Horizontal)
            {
                value -= eventData.delta.x;
                currentVelocity = -eventData.delta.x / Time.deltaTime;
            }
            else if (direction == Direction.Vertical)
            {
                value += eventData.delta.y;
                currentVelocity = eventData.delta.y / Time.deltaTime;
            }

            UpdateContentPosition();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDrag = false;

            if (direction == Direction.Horizontal)
            {
                currentVelocity = -eventData.delta.x / Time.deltaTime;
            }
            else
            {
                currentVelocity = eventData.delta.y / Time.deltaTime;
            }
            isDamping = true;
            targetValue = CalculateTargetValue();
        }

        void LateUpdate()
        {
            Reposition();
        }

        void Reposition()
        {
            if (!isDrag && isDamping)
            {
                value = Mathf.SmoothDamp(value, targetValue, ref currentVelocity, elasticity);
                if (Mathf.Abs(currentVelocity) < 1)
                {
                    currentVelocity = 0;
                }
                if (Mathf.Abs(targetValue - value) < 0.1)
                {
                    value = targetValue;
                    isDamping = false;
                }

                UpdateContentPosition();
            }
        }

        void UpdateContentPosition()
        {
            Vector2 vector = content.anchoredPosition;
            if (direction == Direction.Horizontal)
            {
                vector.x = contentOffset - value;
            }
            else
            {
                vector.y = value - contentOffset;
            }
            content.anchoredPosition = vector;
        }

        float CalculateTargetValue()
        {
            float pageIndex = value / pageWidth;

            float delta = pageIndex - Mathf.Floor(pageIndex);

            if (Mathf.Abs(currentVelocity) > velocityThreshold)
            {
                if (currentVelocity > 0)
                {
                    if (delta > threshold)
                    {
                        pageIndex = Mathf.Ceil(pageIndex);
                    }
                    else
                    {
                        pageIndex = Mathf.Floor(pageIndex);
                    }
                }
                else
                {
                    if (delta > threshold)
                    {
                        pageIndex = Mathf.Floor(pageIndex);
                    }
                    else
                    {
                        pageIndex = Mathf.Ceil(pageIndex);
                    }
                }
            }
            else
            {
                pageIndex = Mathf.Round(pageIndex);
            }

            if (pageCount > 0)
            {
                pageIndex = Mathf.Clamp(pageIndex, 0, pageCount - 1);
            }
            return pageIndex * pageWidth;
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            isDamping = false;
            currentVelocity = 0;
        }
    }
}