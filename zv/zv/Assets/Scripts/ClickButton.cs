using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    //protected AudioController audioCont;
    //AudioClip clickSound;
    protected Button button;

    protected virtual void Start()
    {
        //audioCont = FindObjectOfType<AudioController>();
        //clickSound = Resources.Load("Click") as AudioClip;
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    virtual protected void TaskOnClick()
    {
        //audioCont.PlaySound(clickSound);
    }
}
