using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevelButton : ClickButton
{
    [SerializeField] bool isPrevious;
    Controller cont;

    protected override void Start()
    {
        base.Start();
        cont = FindObjectOfType<Controller>();
    }

    protected override void TaskOnClick()
    {
        base.TaskOnClick();
        cont.ChangeLevel(isPrevious);
    }
}
