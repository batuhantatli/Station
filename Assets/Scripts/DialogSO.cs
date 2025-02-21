using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DailogSO", menuName = "DialogManager/DialogSO", order = 1)]
public class DialogSO : ScriptableObject
{
    [Serializable]
    public struct Dialog
    {
        public List<string> dialogPart;
    }

    public List<Dialog> dialogs;
}
