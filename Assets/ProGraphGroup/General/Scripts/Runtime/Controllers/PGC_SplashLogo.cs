using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace ProGraphGroup.General.Controllers
{
    [Serializable]
    public class PGC_SplashLogo : MonoBehaviour
    {
        [TitleGroup("General")] public Image BackgroundImage;
        [TitleGroup("General")] public Button SkipButton;

        [TitleGroup("Video")] public CanvasGroup VideoCanvasGroup;
        [TitleGroup("Video")] public RawImage VideoRawImage;
        [TitleGroup("Video")] public VideoPlayer VideoPlayerComp;

        [TitleGroup("Logo")] public CanvasGroup LogoCanvasGroup;
        [TitleGroup("Logo")] public RawImage LogoRawImage;

        [Space] [Space] [LabelText("Item List")]
        public List<ItemClass> ItemClasses;

        private int currentIndex = 0;
        private Func<bool> onDone;

        private void Start()
        {
            Init(() =>
            {
                Debug.Log("OnDone");
                return true;
            });
        }

        public void Init(Func<bool> onDone)
        {
            this.onDone = onDone;
            StartItem();
        }

        public async UniTask StartItem()
        {
            if (currentIndex >= ItemClasses.Count)
            {
                onDone.Invoke();
            }

            ItemClass currentItem = ItemClasses[currentIndex];
            await ShowItem(currentItem);
            currentIndex++;
            StartItem();
        }

        private async UniTask ShowItem(ItemClass item)
        {
            
            BackgroundImage.color = item.BackgroundDefaultColor;
            
            if (item.ItemType == ItemTypeEnum.Video)
            {
                LogoCanvasGroup.gameObject.SetActive(false);
                VideoCanvasGroup.gameObject.SetActive(true);
            }
            else if (item.ItemType == ItemTypeEnum.Logo)
            {
                LogoCanvasGroup.gameObject.SetActive(true);
                VideoCanvasGroup.gameObject.SetActive(false);
            }
            
            
            if (item.BackgroundFadeInExecuteOrder == FadeExecuteOrderEnum.BeforeDelay)
            {
                Debug.Log("Start Fade In BeforeDelay" + DateTime.Now);
                await BackgroundImage.DOColor(item.BackgroundFadeInColor, item.BackgroundFadeInDuration)
                    .AsyncWaitForCompletion();
                Debug.Log("End Fade In BeforeDelay" + DateTime.Now);
            }

            if (item.BackgroundFadeInExecuteOrder == FadeExecuteOrderEnum.WithDelay)
            {
                Debug.Log("Start Fade In WithDelay" + DateTime.Now);
                BackgroundImage.DOColor(item.BackgroundFadeInColor, item.BackgroundFadeInDuration);
            }

            Debug.Log("Start StartDelay" + DateTime.Now);
            await UniTask.Delay(TimeSpan.FromSeconds(item.ItemStartDelay), ignoreTimeScale: false);
            Debug.Log("End StartDelay" + DateTime.Now);

            if (item.BackgroundFadeInExecuteOrder == FadeExecuteOrderEnum.AfterDelay)
            {
                Debug.Log("Start Fade In AfterDelay" + DateTime.Now);
                await BackgroundImage.DOColor(item.BackgroundFadeInColor, item.BackgroundFadeInDuration)
                    .AsyncWaitForCompletion();
                Debug.Log("End Fade In AfterDelay" + DateTime.Now);
            }

            if (item.BackgroundFadeInExecuteOrder == FadeExecuteOrderEnum.WithItemShow)
            {
                Debug.Log("Start Fade In WithItemShow" + DateTime.Now);
                BackgroundImage.DOColor(item.BackgroundFadeInColor, item.BackgroundFadeInDuration);
            }

            //----------------------------

            if (item.ItemType == ItemTypeEnum.Video)
            {
                if (item.ItemRectTransform != null)
                {
                    RectTransform rawImageRec = VideoRawImage.GetComponent<RectTransform>();
                    rawImageRec.anchorMin = item.ItemRectTransform.anchorMin;
                    rawImageRec.anchorMax = item.ItemRectTransform.anchorMax;
                    rawImageRec.anchoredPosition = item.ItemRectTransform.anchoredPosition;
                    rawImageRec.sizeDelta = item.ItemRectTransform.sizeDelta;
                }

                VideoPlayerComp.aspectRatio = item.ItemVideoAspectRatio;
            }
            else if (item.ItemType == ItemTypeEnum.Logo)
            {
                if (item.ItemRectTransform != null)
                {
                    RectTransform rawImageRec = LogoRawImage.GetComponent<RectTransform>();
                    rawImageRec.anchorMin = item.ItemRectTransform.anchorMin;
                    rawImageRec.anchorMax = item.ItemRectTransform.anchorMax;
                    rawImageRec.anchoredPosition = item.ItemRectTransform.anchoredPosition;
                    rawImageRec.sizeDelta = item.ItemRectTransform.sizeDelta;
                }
            }

            //----------------------------


            //----------------------------

            if (item.ItemType == ItemTypeEnum.Video)
            {
                VideoPlayerComp.clip = item.ItemVideo;
                VideoPlayerComp.Play();
                VideoCanvasGroup.DOFade(1, item.ItemFadeInDuration);
                Debug.Log("Start Video" + DateTime.Now);
                await UniTask.Delay(TimeSpan.FromSeconds(item.ItemVideo.length), ignoreTimeScale: false);
                VideoCanvasGroup.DOFade(0, item.ItemFadeOutDuration);
            }
            else if (item.ItemType == ItemTypeEnum.Logo)
            {
                LogoRawImage.texture = item.ItemLogo;
                LogoCanvasGroup.DOFade(1, item.ItemFadeInDuration);
                Debug.Log("Start Logo" + DateTime.Now);
                await UniTask.Delay(TimeSpan.FromSeconds(item.ItemLogoDuration), ignoreTimeScale: false);
                LogoCanvasGroup.DOFade(0, item.ItemFadeOutDuration);
            }

            //----------------------------

            if (item.BackgroundFadeOutExecuteOrder == FadeExecuteOrderEnum.BeforeDelay)
            {
                Debug.Log("Start Fade Out BeforeDelay" + DateTime.Now);
                await BackgroundImage.DOColor(item.BackgroundFadeOutColor, item.BackgroundFadeOutDuration)
                    .AsyncWaitForCompletion();
                Debug.Log("End Fade Out BeforeDelay" + DateTime.Now);
            }

            if (item.BackgroundFadeOutExecuteOrder == FadeExecuteOrderEnum.WithDelay)
            {
                Debug.Log("Start Fade Out WithDelay" + DateTime.Now);
                BackgroundImage.DOColor(item.BackgroundFadeOutColor, item.BackgroundFadeOutDuration);
            }

            Debug.Log("Start EndDelay" + DateTime.Now);
            await UniTask.Delay(TimeSpan.FromSeconds(item.ItemEndDelay), ignoreTimeScale: false);

            if (item.BackgroundFadeOutExecuteOrder == FadeExecuteOrderEnum.AfterDelay)
            {
                Debug.Log("Start Fade Out AfterDelay" + DateTime.Now);
                await BackgroundImage.DOColor(item.BackgroundFadeOutColor, item.BackgroundFadeOutDuration)
                    .AsyncWaitForCompletion();
                Debug.Log("End Fade Out AfterDelay" + DateTime.Now);
            }
        }

    }

    [Serializable]
    public class ItemClass
    {
        [LabelText("Type")] public ItemTypeEnum ItemType;
        [LabelText("DefaultColor")] public Color BackgroundDefaultColor;

        public bool SkipButton;

        
        
        [FoldoutGroup("Background Fade In")] [LabelText("Color")]
        public Color BackgroundFadeInColor;

        [FoldoutGroup("Background Fade In")] [LabelText("Duration")]
        public float BackgroundFadeInDuration;

        [FoldoutGroup("Background Fade In")] [LabelText("ExecuteOrder")]
        public FadeExecuteOrderEnum BackgroundFadeInExecuteOrder;


        [FoldoutGroup("Item Config")]
        [LabelText("Video")]
        [ShowIf("ItemType", ItemTypeEnum.Video)]
        [PreviewField(ObjectFieldAlignment.Left)]
        public VideoClip ItemVideo;

        [FoldoutGroup("Item Config")] [LabelText("Mute")] [ShowIf("ItemType", ItemTypeEnum.Video)]
        public bool ItemVideoMute;

        [FoldoutGroup("Item Config")] [LabelText("Volume")] [ShowIf("ItemType", ItemTypeEnum.Video)] [Range(0, 1)]
        public int ItemVideoVolume = 1;


        [FoldoutGroup("Item Config")]
        [LabelText("Logo")]
        [ShowIf("ItemType", ItemTypeEnum.Logo)]
        [PreviewField(ObjectFieldAlignment.Left)]
        public Texture ItemLogo;

        [FoldoutGroup("Item Config")] [LabelText("Duration")] [ShowIf("ItemType", ItemTypeEnum.Logo)]
        public float ItemLogoDuration = 5;


        [FoldoutGroup("Item Config/Size Config")] [LabelText("AspectRatio")] [ShowIf("ItemType", ItemTypeEnum.Video)]
        public VideoAspectRatio ItemVideoAspectRatio = VideoAspectRatio.NoScaling;

        [FoldoutGroup("Item Config/Size Config")]
        [LabelText("RectTransform")]
        [InfoBox(
            "For use different transform for each item.\nCreate empty game object in canvas node and link to this property.")]
        public RectTransform ItemRectTransform;


        [FoldoutGroup("Item Config")] [LabelText("StartDelay")]
        public float ItemStartDelay;

        [FoldoutGroup("Item Config")] [LabelText("FadeInDuration")]
        public float ItemFadeInDuration;

        [FoldoutGroup("Item Config")] [LabelText("FadeOutDuration")]
        public float ItemFadeOutDuration;

        [FoldoutGroup("Item Config")] [LabelText("EndDelay")]
        public float ItemEndDelay;

        [FoldoutGroup("Background Fade Out")] [LabelText("Color")]
        public Color BackgroundFadeOutColor;

        [FoldoutGroup("Background Fade Out")] [LabelText("Duration")]
        public float BackgroundFadeOutDuration;

        [FoldoutGroup("Background Fade Out")] [LabelText("ExecuteOrder")]
        public FadeExecuteOrderEnum BackgroundFadeOutExecuteOrder;
    }


    [Serializable]
    public enum ItemTypeEnum
    {
        Video,
        Logo
    }

    [Serializable]
    public enum FadeExecuteOrderEnum
    {
        BeforeDelay,
        WithDelay,
        AfterDelay,
        WithItemShow,
    }
}