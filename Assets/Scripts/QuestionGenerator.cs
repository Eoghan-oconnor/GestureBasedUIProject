using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using TMPro;
using Newtonsoft.Json;
using System;

public class QuestionGenerator : MonoBehaviour
{
    string easyPath, mediumPath, hardPath;
    string easyJson, mediumJson, hardJson;

    // Difficulty based on 1,2,3 (easy, medium, hard) set when class is loaded
    public int qCount = 0;
    public int remainingGuesses = 5;
    public int scoreCount;
    

    IList<Question> easy, medium, hard;
    Question[] easyQuestions, mediumQuestions, hardQuestions;
    Question currentQuestion;


    // Strings that will be displayed on the UI
    public TextMeshProUGUI question, ans1, ans2, ans3, ans4, scoreTxt, guesses, outcomeTxt;


    
    void Start()
    {
        // Initialize Question ILists
        initQuestions();
    }


    void Update()
    {
        guesses.text = remainingGuesses.ToString();
        scoreTxt.text = scoreCount.ToString();
    }

    public void nextQuestion(int choice)
    {
        randomQuestion(choice);
    }

    private void randomQuestion(int choice)
    {
        System.Random rand = new System.Random();

        switch (choice)
        {
            case 1:
                // Easy 
                currentQuestion = easyQuestions[rand.Next(1, easyQuestions.Length)];
                displayQuestion(currentQuestion);
                break;

            case 2:
                // Medium
                currentQuestion = mediumQuestions[rand.Next(1, mediumQuestions.Length)];
                displayQuestion(currentQuestion);
                break;

            case 3:
                // Hard
                currentQuestion = hardQuestions[rand.Next(1, hardQuestions.Length)];
                displayQuestion(currentQuestion);
                break;

        } 
    }



    // Simple method for checking the correct answer
    // Since the answers get mixed up when sent to the text fields
    // a simple way to check the corrrect answer is compare the strings
    public Boolean checkAnswer(string ans)
    {
        if (ans.Contains(currentQuestion.correct_answer))
        {
            outcomeTxt.SetText("Correct Answer");
            
            return true;
        }
        else
        {
            outcomeTxt.SetText("Incorrect Answer");
            
            return false;
        }

        

    }

    private void displayQuestion(Question q)
    {
        // Display Question
        question.SetText(q.question.ToString());

        // Shuffle answers
        List<string> possibleAnswers = new List<string>();
        possibleAnswers.Add(q.incorrect_answers[0]);
        possibleAnswers.Add(q.incorrect_answers[1]);
        possibleAnswers.Add(q.incorrect_answers[2]);
        possibleAnswers.Add(q.correct_answer);

//        Debug.Log(possibleAnswers);

        Helper.Shuffle(possibleAnswers);

        int count = 1;
        foreach (String ans in possibleAnswers)
        {
            
            switch (count)
            {
                case 1:
                    ans1.SetText(ans);
                    break;
                case 2:
                    ans2.SetText(ans);
                    break;
                case 3:
                    ans3.SetText(ans);
                    break;
                case 4:
                    ans4.SetText(ans);
                    break;
                default:
                    break;
            }
            count++;
        }    
    }

    void initQuestions()
    {
        // Method used to read .json files
        // Calls loadQuestions to Deserialize json string

        // Set the string paths to the 3 Json files 
        easyPath = Application.streamingAssetsPath + "/easy.json";
        mediumPath = Application.streamingAssetsPath + "/medium.json";
        hardPath = Application.streamingAssetsPath + "/hard.json";

        // Open and read all json files
        easyJson = File.ReadAllText(easyPath);
        mediumJson = File.ReadAllText(mediumPath);
        hardJson = File.ReadAllText(hardPath);

        Helper.RemoveSpecialChars(easyJson);
        Helper.RemoveSpecialChars(mediumJson);
        Helper.RemoveSpecialChars(hardJson);

        easy = loadQuestions(easyJson);
        medium = loadQuestions(mediumJson);
        hard = loadQuestions(hardJson);

        // Convert ILists to arrays
        easyQuestions = convertToArray(easy);
        mediumQuestions = convertToArray(medium);
        hardQuestions = convertToArray(hard);

        // Passes in the selected difficulty from the previous scene
        nextQuestion(Helper.difficulty);

    }

    public static IList<Question> loadQuestions(string jsonString)
    {
        // Method for Deserializing json string to IList<Question> object

        JsonTextReader reader = new JsonTextReader(new StringReader(jsonString));
        IList<Question> listOfQs = JsonConvert.DeserializeObject<List<Question>>(jsonString);

        return (List<Question>) listOfQs;
    }

    public Question[] convertToArray(IList<Question> questions)
    {
        // Converter for converting IList<Question> to Question[]

        List<Question> temp = (List<Question>)questions;
        Question[] questionsArray = temp.ToArray();

        return questionsArray;
    }

    
}




// Question class based on JSON format of stored questions
[System.Serializable]
public class Question
{
    public string category { get; set; }
    public string type { get; set; }
    public string difficulty { get; set; }
    public string question { get; set; }
    public string correct_answer { get; set; }
    public string[] incorrect_answers { get; set; }
}

