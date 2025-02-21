using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class DialogManager : Singleton<DialogManager>
{
    public GameObject readyForNextImage;
    public TMP_Text dialogText;
    public float eachLetterDelay;
    public float nextDialogDelay;

    public IEnumerator SetText(string text, Action onComplete )
    {
        readyForNextImage.SetActive(false);
        dialogText.text = null;
        foreach (var VARIABLE in text)
        {
            dialogText.text += VARIABLE;
            if (VARIABLE.ToString() == " ")
            {
                yield return new WaitForEndOfFrame();

            }
            else
            {
                yield return new WaitForSeconds(eachLetterDelay);
                
            }
        }

        yield return new WaitForSeconds(nextDialogDelay);
        readyForNextImage.SetActive(true);
        onComplete?.Invoke();
    }

    public void ClearText()
    {
        dialogText.text = "";
        readyForNextImage.SetActive(false);
    }

}
