using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ProGraphGroup.General.Components
{
    public class ScrollSnap : MonoBehaviour
    {
        public RectTransform Content => _scrollRect.content;
        public RectTransform ViewPort => _scrollRect.viewport;

        public float SpeedScrollMaxValueTime = 0.3f;
        [SerializeField]
        private ScrollRect _scrollRect;
        [SerializeField]
        private float _speed = 10;
        [SerializeField]
        private RectTransform _validRect;
        [SerializeField]
        private RectTransform _sizeComparer;

        public int _currentIndex;
        private Vector2 _startPos;
        public int _lastIndex;

        private bool startTouch = false;
        static bool isFirstOpened = true;
        public GameObject[] items;
        public Text[] itemText;
        private float itemHeight;

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


        public void Initialize(int count)
        {
            _count = count;
            if (isFirstOpened)
                StartCoroutine(InitializeRoutine(count));
            else
            {
                _lastIndex = count - 1;
            }

            items = Content.GetComponentsInChildren<GameObject>();
            itemText = new Text[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                itemText[i] = items[i].GetComponentInChildren<Text>();
            }
            itemHeight = items[0].GetComponent<RectTransform>().rect.height;
        }

        public IEnumerator InitializeRoutine(int count)
        {
            isFirstOpened = false;
            yield return new WaitForEndOfFrame();
            _lastIndex = count - 1;

        }
        void Update()
        {
            if (items == null || items.Length == 0)
                return;

            for (int i = 0; i < _count - 1; i++)
            {
                float size = 1 - (Vector2.Distance(items[i].transform.position, _sizeComparer.position) / 1.75f);
                Color color = itemText[i].color;
                itemText[i].color = new Color(color.r, color.g, color.b, size);
                items[i].transform.localScale = new Vector2(size, size);
            }

            if (Input.GetMouseButtonDown(0))
            {
                bool isInValid = false;
                try
                {
                    isInValid = RectTransformUtility.RectangleContainsScreenPoint(_validRect, Input.mousePosition, Camera.main);
                }
                catch
                {

                }
                if (isInValid)
                    OnMouseDown(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                OnMouseUp(Input.mousePosition);
            }
            UpdatePosition();
        }


        private bool eventTick;
        private int _count;

        private void UpdatePosition()
        {

            if (!startTouch)
            {
                float verticalNormalizedPosition = _scrollRect.verticalNormalizedPosition;
                _scrollRect.verticalNormalizedPosition = Mathf.Lerp(verticalNormalizedPosition,
                    ((float)CurrentIndex / _lastIndex), Time.deltaTime * _speed);
            }
        }

        public void SnapTo(int index)
        {
            int soundEffectCount = Mathf.Abs(index - CurrentIndex);
            //if (soundEffectCount == 1)
            //    SoundManager.Instance.PlayEffect(SoundEffectType.scroll);
            //else
            //{
            //    SoundManager.Instance.PlayEffect(SoundEffectType.scroll);
            //    SoundManager.Instance.PlayEffect(SoundEffectType.scroll, 0.05f);
            //}
            index = index < 0 ? 0 : index > _count - 1 ? _count - 1 : index;
            this.CurrentIndex = index;
        }
        float startTime;
        public void OnMouseUp(Vector2 position)
        {
            if (!startTouch)
                return;

            Vector2 delta = position - _startPos;
            int deltaPerViewY = Time.time - startTime < SpeedScrollMaxValueTime ? (int)(Mathf.Abs(delta.y) / ViewPort.rect.height) : 0;
            float contecntHeight = _scrollRect.content.rect.height;
            float itemHeightNormalized = (itemHeight / contecntHeight) / 2;
            if (Mathf.Abs(delta.y) < Mathf.Abs(delta.x) || Mathf.Abs(delta.y) < 30)
            {
                startTouch = false;
                SnapTo(CurrentIndex);
                return;
            }
            if (delta.y > 0)
            {
                int destination = (int)(((_scrollRect.verticalNormalizedPosition + itemHeightNormalized) * _lastIndex) - deltaPerViewY);
                SnapTo(destination);
            }

            if (delta.y < 0)
            {
                int destination = 1 + (int)(((_scrollRect.verticalNormalizedPosition - itemHeightNormalized) * _lastIndex) + deltaPerViewY);
                SnapTo(destination);
            }
            startTouch = false;
        }


        public void OnMouseDown(Vector2 position)
        {
            startTime = Time.time;
            startTouch = true;
            _startPos = position;
        }
    }
}
