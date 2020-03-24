using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONParser : MonoBehaviour
{
    string easyPath, mediumPath, hardPath;
    string easyJson, mediumJson, hardJson;
    
    void Start()
    {

        // Set the dtring paths to the 3 Json files 
        easyPath = Application.streamingAssetsPath + "/Questions/easy.json";
        mediumPath = Application.streamingAssetsPath + "/Questions/medium.json";
        hardPath = Application.streamingAssetsPath + "/Questions/hard.json";

        // Open and read all json files
        easyJson = File.ReadAllText(easyPath);
        mediumJson = File.ReadAllText(mediumPath);
        hardJson = File.ReadAllText(hardPath);

        QuestionList easyQuestions = JsonUtility.FromJson<QuestionList>(easyJson);
        QuestionList mediumQuestions = JsonUtility.FromJson<QuestionList>(easyJson);
        QuestionList hardQuestions = JsonUtility.FromJson<QuestionList>(easyJson);




    }

    
}

[System.Serializable]
public class QuestionList
{
    public Question[] questions;
}

[System.Serializable]
public class Question
{

    public string category;
    public string type;
    public string difficulty;
    public string question;
    public string correct_answer;
    public string[] incorrect_answers;
}

