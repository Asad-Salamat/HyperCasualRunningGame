using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ClearCanvasController : MonoBehaviour
{

    [SerializeField] private TMP_Text percentageText;
    [SerializeField] private Image filler;
    public void OnBtnPush_next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetData(float level)
    {
        float percentage = level / 5;
        percentageText.text = percentage * 100 + "%";
        filler.DOFillAmount(percentage, .5f).SetEase(Ease.InFlash);
        if (level >= 5)
        {
            //activate new character....
        }
    }
}
