using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npcs : MonoBehaviour, IInteractable
{
    public bool isInteracting;
    public Transform cameraLookPoint;
    public Transform cameraPos;
    public float interactDistance;
    private PlayerInteractor _playerInteractor;
    public DialogSO dialogSo;
    public int dialogIndex = 0;

    private int currentDialogIndex = 0;
    public Action endAction;
 
    public Transform GetCameraLookPoint()
    {
        return cameraLookPoint;
    }

    public bool readyForNextDialog;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && readyForNextDialog && isInteracting)
        {
            Dialog();
        }
    }

    public void Dialog(Action OnEndDialog = null)
    {
        if (dialogSo != null)
        {
            if (OnEndDialog != null)
            {
                endAction = OnEndDialog;
                
            }
            isInteracting = true;
            readyForNextDialog = false;
            
            if (dialogSo.dialogs[dialogIndex].dialogPart.Count > currentDialogIndex)
            {
                var text = dialogSo.dialogs[dialogIndex].dialogPart[currentDialogIndex];
                StartCoroutine(DialogManager.Instance.SetText(text, (() =>
                {
                    readyForNextDialog = true;
                    currentDialogIndex++;
                })));
            }
            else if (NextDialogPart())
            {
                dialogIndex++;
                currentDialogIndex = 0;
            }
            else
            {
                DialogManager.Instance.ClearText();
                endAction?.Invoke();
                endAction = null;
                currentDialogIndex = 0;
                isInteracting = false;
            } 
        }

    }

    public bool NextDialogPart()
    {
        return false;
    }

    public void SetPlayerInteractor(PlayerInteractor playerInteractor)
    {
        _playerInteractor = playerInteractor;
    }

    public bool SetDistance(Transform player)
    {
        if (Vector3.Distance(transform.position, player.position) <= interactDistance)
        {
            return true;
        }

        return false;
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }

    public float InteractTime()
    {
        return 0;
    }

    public float InteractDistance()
    {
        return interactDistance;
    }
}