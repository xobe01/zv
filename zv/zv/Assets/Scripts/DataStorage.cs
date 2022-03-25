using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataStorage
{
    static SerializableData data;
    const int lineCount = 3052;
    static int counter;
    static bool[] isMulti;
    static List<int>[] goodAnswers;
    static List<string>[] answers;
    static string[] questions;

    public static int CurrentQuestion
    {
        get { return data.currentQuestion; }
        set 
        {
            data.currentQuestion = value;
            Save();
        }
    }

    public static int LineCount
    {
        get { return lineCount; }
    }

    public static bool IsRandom
    {
        get { return data.isRandom; }
        set
        {
            data.isRandom = value;
            Save();
        }
    }

    [RuntimeInitializeOnLoadMethod]
    static void AppStartFunctions()
    {
        ReadData();
    }

    static void ReadData()
    {
        data = SaveAndLoad.LoadData(true);
        counter = 0;
        isMulti = new bool[lineCount];
        goodAnswers = new List<int>[lineCount];
        questions = new string[lineCount];
        answers = new List<string>[lineCount];
        string line;
        TextAsset txt = Resources.Load<TextAsset>("data2");
        System.IO.StreamReader file = new System.IO.StreamReader(new System.IO.MemoryStream(txt.bytes));
        while ((line = file.ReadLine()) != null)
        {
            ProcessLine(line);
            counter++;
        }
        file.Close();
    }

    static void ProcessLine(string line)
    {
        string[] parts = line.Split('$');
        bool multi = parts[0][0] == 'M';
        isMulti[counter] = multi;
        goodAnswers[counter] = new List<int>();
        answers[counter] = new List<string>();
        if (multi)
        {
            for (int i = 0; i < Mathf.Round((float)parts[1].Length / 2); i++)
            {
                goodAnswers[counter].Add(ConvertToInt(parts[1][i * 2]));
            }
        }
        else goodAnswers[counter].Add(ConvertToInt(parts[1][0]));
        questions[counter] = parts[2];
        int partsCounter = 3;
        while (partsCounter < parts.Length && parts[partsCounter] != "")
        {
            answers[counter].Add(parts[partsCounter]);
            partsCounter++;
        }
    }

    static int ConvertToInt(char c)
    {
        switch (c)
        {
            case 'D':
                return 0;
            case 'E':
                return 1;
            case 'F':
                return 2;
            case 'G':
                return 3;
            case 'H':
                return 4;
            case 'I':
                return 5;
        }
        return -1;
    }

    public static void Save()
    {
        SaveAndLoad.SaveData(data);
    }

    public static string GetQuestion(int index)
    {
        return questions[index];
    }

    public static List<string> GetAnswers(int index)
    {
        return answers[index];
    }

    public static List<int> GetGoodAnswers(int index)
    {
        return goodAnswers[index];
    }

    public static bool GetIsMulti(int index)
    {
        return isMulti[index];
    }

    public static bool GetIsLearned(int index)
    {
        return data.isLearned[index];
    }

    public static void SetIsLearned(int index, bool value)
    {
        data.isLearned[index] = value;
        Save();
    }
}
