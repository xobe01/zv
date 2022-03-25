using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    [SerializeField] InputField questionNumber;
    [SerializeField] Text questionText;
    [SerializeField] AnswerButtonController[] answerBoxes;
    [SerializeField] GameObject LearnedCheck;

    bool isChecked;

    void Start()
    {          
        NextQuestion();
    }

    void NextQuestion()
    {
        int current = DataStorage.CurrentQuestion;
        questionNumber.text = (current + 1).ToString();
        questionText.text = DataStorage.GetQuestion(current);
        for (int i = 0; i < answerBoxes.Length; i++)
        {
            if (i < DataStorage.GetAnswers(current).Count)
            {
                if(!DataStorage.GetIsMulti(DataStorage.CurrentQuestion)) answerBoxes[i].SetToSingle();
                answerBoxes[i].SetText(DataStorage.GetAnswers(current)[i]);
            }
            else answerBoxes[i].Disable();
        }
        if (DataStorage.GetIsLearned(current)) LearnedCheck.SetActive(true);
    }

    public void ChangeLevel(bool isPrevious)
    {
        if(DataStorage.IsRandom) DataStorage.CurrentQuestion = Random.Range(0, DataStorage.LineCount);
        else DataStorage.CurrentQuestion += isPrevious ? -1 : 1;
        if (DataStorage.CurrentQuestion == -1) DataStorage.CurrentQuestion = DataStorage.LineCount - 1;
        else if (DataStorage.CurrentQuestion == DataStorage.LineCount) DataStorage.CurrentQuestion = 0;
        SceneManager.LoadScene(0);
    }

    public void ChangeLevelByNumber(string number)
    {
        DataStorage.CurrentQuestion = Mathf.Clamp(int.Parse(number)-1, 0, DataStorage.LineCount - 1);
        SceneManager.LoadScene(0);
    }

    public void SetRandom()
    {
        DataStorage.IsRandom = !DataStorage.IsRandom;
    }

    public void CheckAnswer()
    {
        if (!isChecked)
        {
            bool isGood = false;
            foreach(AnswerButtonController a in answerBoxes) 
                if(a.IsSelected)
                {
                    isGood = true;
                    break;
                }
            if (isGood)
            {
                for (int i = 0; i < DataStorage.GetAnswers(DataStorage.CurrentQuestion).Count; i++)
                {
                    answerBoxes[i].CheckBox(DataStorage.GetGoodAnswers(DataStorage.CurrentQuestion).Contains(i), answerBoxes[i].IsSelected);
                }
                isChecked = true;
            }            
        }
        else ChangeLevel(false);
    }

    public void AnswerSelected()
    {
        if (!DataStorage.GetIsMulti(DataStorage.CurrentQuestion))
        {
            CheckAnswer();
        }        
    }

    public void Learned()
    {
        bool islearned = DataStorage.GetIsLearned(DataStorage.CurrentQuestion);
        DataStorage.SetIsLearned(DataStorage.CurrentQuestion, !islearned);
        LearnedCheck.SetActive(!islearned);
    }
}
