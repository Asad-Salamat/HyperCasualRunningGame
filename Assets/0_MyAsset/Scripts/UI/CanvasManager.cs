using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager i;

    public InputCanvasController inputCanvas;
    public SettingsCanvasController settingsCanvas;
    public StartCanvasController startCanvas;
    public ClearCanvasController clearCanvas;
    public FailCanvasController failCanvas;
    public GameObject progressSlider;
    public Image filler;

    //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
    void Awake()
    {
        i = this;
    }

    void Start()
    {
        CloseAllPage();
        startCanvas.gameObject.SetActive(true);
        Debug.LogWarning($"You may see 4 errors above this comment, but these issues seem Unity2021 bugs.\n( If error comments don't exist, please ignore this comment. )");
    }

    //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
    public void OpenSettingsCanvas()
    {
        SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        CloseAllPage();
        settingsCanvas.gameObject.SetActive(true);
        EffectViewerManager.i.SetViewersActive(true);
    }

    public void CloseSettingsCanvas()
    {
        CloseAllPage();
        startCanvas.gameObject.SetActive(true);
        EffectViewerManager.i.SetViewersActive(false);
    }

    public void OpenClearCanvas(int level)
    {
        CloseAllPage();
        clearCanvas.gameObject.SetActive(true);
        clearCanvas.SetData(level);
    }

    public void OpenFailCanvas()
    {
        CloseAllPage();
        failCanvas.gameObject.SetActive(true);
    }

    void CloseAllPage()
    {
        settingsCanvas.gameObject.SetActive(false);
        startCanvas.gameObject.SetActive(false);
        clearCanvas.gameObject.SetActive(false);
        failCanvas.gameObject.SetActive(false);
    }

    public void UpdateFiller(float value)
    {
        //float previousValue
        filler.DOFillAmount(value, .3f).SetEase(Ease.Linear);
    }
}
