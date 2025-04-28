using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using StarterAssets;
using UnityEngine;
using Random = UnityEngine.Random;

public class Duch : MonoBehaviour
{
    public Transform lookPoint;
    private AudioSource _source;
    private Animator _animator;
    public FirstPersonController _firstPersonController;
    public AudioClip breath;
    public DialogSO dialogSo;
    public GameObject knife;
    private bool hPoint;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }

    public AudioClip clip;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Test22.Instance.Tester1();
            hPoint = true;
            // _animator.SetTrigger("FirstMeet");    
            // _animator.SetBool("HPoint",hPoint);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            GameManager.Instance.PlayerAudioSource.clip = clip;
            GameManager.Instance.PlayerAudioSource.Play();
            Test22.Instance.Tester1();
            MoveBehind();
            // HPoint();
        }

        if (Input.GetMouseButtonDown(0) && readyForNextDialog)
        {
            Dialog( (() =>
            {
                _firstPersonController.enabled = false;
                _firstPersonController.GetComponent<Stun>().Hit();
            }));
        }
    }

    public void HPoint()
    {
        _firstPersonController.enabled = false;
        _source.Play();
    }

    public void HPointImpactEnd()
    {
        Test22.Instance.Tester2();
        DialogOpener();
    }

    public void DialogOpener()
    {
    }

    public float moveSpeed;
    public GameObject mesh;

    public void MoveBehind()
    {
        // _source.Play();
        t = true;
        _animator.SetBool("WalkDrunk", true);
        StartCoroutine(PlayWalkAudio());
        transform.LookAt(_firstPersonController.transform);
        transform.DOMove(_firstPersonController.transform.position, moveSpeed).SetEase(Ease.Linear).OnComplete((() =>
        {
            mesh.SetActive(false);
            knife.GetComponent<AudioSource>().Play();
            knife.GetComponent<Animator>().SetBool("Pull", true);
            Test22.Instance.Tester2();
            // _firstPersonController.enabled = true;
            t = false;
            this.CallWithDelay((() =>
            {
                transform.DOLocalMoveX(-2, .2f).SetRelative().OnComplete((() =>
                {
                    _source.clip = breath;
                    _source.Play();
                }));
            }), 2f);

            this.CallWithDelay(
                (() => { transform.DOLocalMoveX(4, 2).SetRelative().OnComplete((() => { Dialog(); })); }), 5f);
        }));
        // transform.DOLocalMoveX(-1, moveSpeed / 3).SetRelative().OnComplete((() =>
        // {
        //     transform.DOLocalMoveX(2, moveSpeed / 3).SetRelative().OnComplete((() =>
        //     {
        //         transform.DOLocalMoveX(-1, moveSpeed / 3).SetRelative().OnComplete((() =>
        //         {
        //     
        //         }));
        //     }));
        // }));
        _firstPersonController.enabled = false;
    }


    public List<AudioClip> walkClips = new List<AudioClip>();


    public AudioClip RandomWalkclips()
    {
        var g = walkClips[Random.Range(0, walkClips.Count)];
        return g;
    }

    public bool t;
    public float walkAuidoClipDelay;

    public IEnumerator PlayWalkAudio()
    {
        while (t)
        {
            _source.clip = RandomWalkclips();
            _source.Play();
            yield return new WaitForSeconds(walkAuidoClipDelay);
        }
    }

    public Action endAction;
    public bool readyForNextDialog;
    public int dialogIndex;
    public int currentDialogIndex;

    public void Dialog(Action OnEndDialog = null)
    {
        if (dialogSo != null)
        {
            if (OnEndDialog != null)
            {
                endAction = OnEndDialog;
            }

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
            }
        }
    }

    public bool NextDialogPart()
    {
        return false;
    }
}