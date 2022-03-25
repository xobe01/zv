using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButtonController : ClickButton
{
    [SerializeField] GameObject circleStroke;
    [SerializeField] GameObject circle;
    [SerializeField] Text text;

    RawImage box;
    Controller cont;
    bool isSelected;
    bool isSingle;

    protected override void Start()
    {
        base.Start();
        cont = FindObjectOfType<Controller>();
        box = GetComponent<RawImage>();
    }

    public bool IsSelected
    {
        get { return isSelected; }
    }

    protected override void TaskOnClick()
    {
        base.TaskOnClick();
        isSelected = !isSelected;
        if(!isSingle) circle.SetActive(isSelected);
        else cont.AnswerSelected();
    }

    public void Disable()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void SetToSingle()
    {
        isSingle = true;
        circleStroke.SetActive(false);  
    }

    public void CheckBox(bool isGood, bool isSelected)
    {
        box.color = isGood ? Color.green : Color.red;
        button.interactable = false;
    }

    public void SetText(string answer)
    {
        text.text = answer;
    }
}
