using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProGraphGroup.General.Components.MagneticScrollRect
{
    public class HorizontalScrollSnap : ScrollRect, IBeginDragHandler, IEndDragHandler
    {

        [SerializeField]
        public int _lastIndex = 0;
        public List<Transform> _items;

        private float _speed = 10;
        private int _currentIndex;
        private Vector2 _startPos;
        private float _currentStartTime;

        private bool startTouch = false;
        bool isFirstOpened = true;

        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                OnValueChange?.Invoke();
                OnIndexChange?.Invoke(_currentIndex);
            }
        }

        public Action OnValueChange;
        public Action<int> OnIndexChange;

        public void Initialize(List<Transform> items)
        {
            this._items = items;
            var count = items.Count;
            _lastIndex = count - 1;
            if (isFirstOpened && gameObject.activeInHierarchy)
                StartCoroutine(InitializeRoutine(count));
            else
            {
                _lastIndex = count - 1;
                RightToLeft();
                JumpTo(0);
            }
        }

        public IEnumerator InitializeRoutine(int count)
        {
            isFirstOpened = false;
            yield return new WaitForEndOfFrame();
            RightToLeft();
            JumpTo(0);
        }

        void Update()
        {
            UpdatePosition();
        }


        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            OnMouseDown(Input.mousePosition);
        }
        
 
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            OnMouseUp(Input.mousePosition);
        }

        private bool eventTick;


        private void UpdatePosition()
        {
            _currentStartTime += Time.deltaTime;
            if (!startTouch)
            {
                horizontalNormalizedPosition = Mathf.Lerp(horizontalNormalizedPosition,
                ((float)CurrentIndex / _lastIndex), Time.deltaTime * _speed);
            }
        }

        public void SnapTo(int index)
        {
            this.CurrentIndex = index;
        }
        public void JumpTo(int index)
        {
            horizontalNormalizedPosition = ((float)index / _lastIndex);
            this.CurrentIndex = index;
        }

        public void OnMouseUp(Vector2 position)
        {
            Vector2 delta = position - _startPos;
            if (Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
            {
                startTouch = false;
                SnapTo(CurrentIndex);
                return;
            }
            float currentStartTime = (Mathf.Abs(delta.x) / _currentStartTime) / 2000;
            int step = currentStartTime < 1 ? 1 : (int)currentStartTime;
            if (delta.x > 0)
            {
                int destination = this.CurrentIndex;

                if (_currentStartTime < 1f)
                {
                    destination -= step;
                    destination = destination >= 0 ? destination : 0;
                }
                else
                {
                    destination = (int)((horizontalNormalizedPosition * _lastIndex) + 0.25f);
                }
                if (destination >= 0)
                    SnapTo(destination);
            }

            if (delta.x < 0)
            {
                int destination = this.CurrentIndex;

                if (_currentStartTime < 1f)
                {
                    destination += step;
                    destination = destination > _lastIndex ? _lastIndex : destination;
                }
                else
                {
                    destination = 1 + (int)((horizontalNormalizedPosition * _lastIndex) - 0.25f);
                }

                if (destination < _lastIndex + 1)
                    SnapTo(destination);
            }
            startTouch = false;
        }


        public void OnMouseDown(Vector2 position)
        {
            startTouch = true;
            _startPos = position;
            _currentStartTime = 0;
        }

        public void LeftToRight()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].SetSiblingIndex(i);
            }
        }

        public void RightToLeft()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _items[_items.Count - i - 1].SetSiblingIndex(i);
            }
        }

        public void ClearData()
        {
            _items.Clear();
        }

        public void SnapToNextPanelLoop()
        {
            CurrentIndex = (_currentIndex + 1) % (_lastIndex + 1);
            SnapTo(CurrentIndex);
        }
    }
}