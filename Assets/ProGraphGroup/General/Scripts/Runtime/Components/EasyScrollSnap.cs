using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProGraphGroup.General.Components
{
    public class EasyScrollSnap : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private float _speed = 10;
        [SerializeField] private RectTransform _ignorRect;
        [SerializeField] private bool _reverseIgnor;
        public List<Transform> _items = new List<Transform>();
        private int _currentIndex;
        private Vector2 _startPos;
        private float _currentStartTime;
        public int _lastIndex;
        private RectTransform _container;

        public bool disableDragClick = false;


        private bool startTouch = false;
        static bool isFirstOpened = true;

        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                OnValueChange?.Invoke();
            }
        }

        public Action OnValueChange;

        void Start()
        {
            _container = _scrollRect.viewport;
        }

        private void OnEnable()
        {
            //   OnMouseUp(Vector2.zero);
        }

        public void Initialize(int count)
        {
            if (isFirstOpened)
                StartCoroutine(InitializeRoutine(count));
            else
            {
                bool isLeftToRight = true;
                _lastIndex = count - 1;
                int index = isLeftToRight ? 0 : _lastIndex;
                if (isLeftToRight)
                {
                    LeftToRight();
                    JumpTo(index);
                }
                else
                {
                    RightToLeft();
                    JumpTo(index);
                }
            }
        }

        public void Initialize(int count, int targetIndex)
        {
            if (isFirstOpened)
                StartCoroutine(InitializeRoutine(count));
            else
            {
                bool isLeftToRight = true;
                _lastIndex = count - 1;
                int index = isLeftToRight ? targetIndex : _lastIndex - targetIndex;
                if (isLeftToRight)
                {
                    LeftToRight();
                    JumpTo(index);
                }
                else
                {
                    RightToLeft();
                    JumpTo(index);
                }
            }
        }

        public IEnumerator InitializeRoutine(int count)
        {
            isFirstOpened = false;
            yield return new WaitForEndOfFrame();
            bool isLeftToRight = true;
            _lastIndex = count - 1;
            int index = isLeftToRight ? 0 : _lastIndex;
            if (isLeftToRight)
            {
                LeftToRight();
                JumpTo(index);
            }
            else
            {
                RightToLeft();
                JumpTo(index);
            }
        }

        void Update()
        {
//            if (UIManager.instance.Pages["GlobalLoading"].gameObject.activeInHierarchy)
//                return;

            if (!disableDragClick)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    bool isInIgnor = false;
                    try
                    {
                        isInIgnor = RectTransformUtility.RectangleContainsScreenPoint(_ignorRect, Input.mousePosition,
                            Camera.main);
                    }
                    catch
                    {
                    }

                    isInIgnor = _reverseIgnor ? !isInIgnor : isInIgnor;
                    if (!isInIgnor)
                        OnMouseDown(Input.mousePosition);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    OnMouseUp(Input.mousePosition);
                }
            }

            UpdatePosition();
        }


        private bool eventTick;


        private void UpdatePosition()
        {
            _currentStartTime += Time.deltaTime;
            if (!startTouch)
            {
                _scrollRect.horizontalNormalizedPosition = Mathf.Lerp(_scrollRect.horizontalNormalizedPosition,
                    ((float) CurrentIndex / _lastIndex), Time.deltaTime * _speed);
            }
        }

        public void SnapTo(int index)
        {
            int count = Mathf.Abs(index - CurrentIndex);
            //            if (count == 1)
            //                SoundManager.Instance.PlayEffect(SoundEffectType.scroll);
            //            else
            //            {
            //                SoundManager.Instance.PlayEffect(SoundEffectType.scroll);
            //                SoundManager.Instance.PlayEffect(SoundEffectType.scroll, 0.05f);
            //            }

            this.CurrentIndex = index;
        }

        public void JumpTo(int index)
        {
            _scrollRect.horizontalNormalizedPosition = ((float) index / _lastIndex);
            this.CurrentIndex = index;
        }

        public void OnMouseUp(Vector2 position)
        {
            Vector2 delta = position - _startPos;
            if (Mathf.Abs(delta.x) < Mathf.Abs(delta.y) || Mathf.Abs(delta.x) < 30)
            {
                startTouch = false;
                SnapTo(CurrentIndex);
                return;
            }

            float currentStartTime = (Mathf.Abs(delta.x) / _currentStartTime) / 2000;
            int step = currentStartTime < 1 ? 1 : (int) currentStartTime;
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
                    destination = (int) ((_scrollRect.horizontalNormalizedPosition * _lastIndex) + 0.25f);
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
                    destination = 1 + (int) ((_scrollRect.horizontalNormalizedPosition * _lastIndex) - 0.25f);
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
            CurrentIndex = _lastIndex >= 0 ? (_currentIndex + 1) % (_lastIndex + 1) : -1;
            if (CurrentIndex != -1)
                SnapTo(CurrentIndex);
        }
    }
}