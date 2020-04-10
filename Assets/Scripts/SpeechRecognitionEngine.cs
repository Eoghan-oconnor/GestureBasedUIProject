using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;
using TMPro;

public class SpeechRecognitionEngine : MonoBehaviour
{
    public string[] keywords = new string[] { "First", "Second", "Third", "Fourth", "Play", "Settings", "Pause", "Yes", "No"};
    public ConfidenceLevel confidence = ConfidenceLevel.Low;
    public float speed = 1;

    public TextMeshProUGUI results;
    public int scoreInt;
    private string finalAnswer;

    protected PhraseRecognizer recognizer;
    protected string word = " ";

    public GameObject canvas;
    public GameObject answerPanel;
    public GameObject pausePanel;
    public GameObject outcomePanel;
    public GameObject gameOverPanel;
    private QuestionGenerator qGen;
    
    


    private void Start()
    {
        // Gets the instance of QuestionGenorator class
        qGen = canvas.GetComponent<QuestionGenerator>();
        
        
        if (keywords != null)
        {
            recognizer = new KeywordRecognizer(keywords, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }

        
    }


    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        results.text = "You said: <b>" + word + "</b>";
        Debug.Log(word);

        switch (word)
        {
            case "First":
                answerPanel.SetActive(true);
                Debug.Log(qGen.ans1.text.ToString());
                finalAnswer = qGen.ans1.text.ToString();
                break;
            case "Second":
                answerPanel.SetActive(true);
                Debug.Log(qGen.ans2.text.ToString());
                finalAnswer = qGen.ans2.text.ToString();
                break;
            case "Third":
                answerPanel.SetActive(true);
                Debug.Log(qGen.ans3.text.ToString());
                finalAnswer = qGen.ans3.text.ToString();
                break;
            case "Fourth":
                answerPanel.SetActive(true);
                Debug.Log(qGen.ans4.text.ToString());
                finalAnswer = qGen.ans4.text.ToString();
                break;
            case "Play":
                ContinueGame();
                break;
            case "Settings":
                break;
            case "Pause":
                PauseGame();
                break;
            case "Yes":
                if (answerPanel.activeSelf)
                {
                    runCheck(finalAnswer);
                }
                break;
            case "No":
                answerPanel.SetActive(false);
                break;
            case "Restart":
                Helper.RestartGame();
                break;
            case "Quit":
                Helper.QuitGame();
                break;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    private void Update()
    {

        if (qGen.remainingGuesses <= 0)
        {
            gameOverPanel.SetActive(true);

        }

       
    }

    private void runCheck(string ans)
    {
        answerPanel.SetActive(false);
        outcomePanel.SetActive(true);
        if (qGen.checkAnswer(ans))
        {
            // The answer was correct
            qGen.scoreCount += 10;
            qGen.nextQuestion(Helper.difficulty);

            Invoke("", 1);
            outcomePanel.SetActive(false);
        }
        else
        {
            qGen.remainingGuesses--;
            Debug.Log("RemainingGuesses = "+ qGen.remainingGuesses);
            Invoke("", 1);
            outcomePanel.SetActive(false);
        }
    }

    

   

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }
}
