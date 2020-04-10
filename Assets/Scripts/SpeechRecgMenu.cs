using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;
using TMPro;

public class SpeechRecgMenu : MonoBehaviour
{
    // Variables
    public string[] keywords = new string[] { "Play", "Settings", "Easy", "Average", "Hard", "Pause" };
    public ConfidenceLevel confidence = ConfidenceLevel.Low;
    public TextMeshProUGUI results;
    protected PhraseRecognizer recognizer;
    protected string word = "";

    
    private void Start()
    {
        // intailizes Keyword Recognizer
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
    }

    private void Update()
    {
        switch (word)
        {
            case "Play":
                SceneManager.LoadScene("LevelDifficulty");
                break;
                case "Settings":
                //Debug.Log("Settings");
                //SceneManager.LoadScene("Settings");
                break;
            case "Easy":
                SceneManager.LoadScene("GameScene");
                Helper.difficulty = 1;
                Debug.Log(Helper.difficulty);
                break;
            case "Average":
                //Debug.Log("average");
                SceneManager.LoadScene("GameScene");
                Helper.difficulty = 2;
                Debug.Log(Helper.difficulty);
                break;
            case "Hard":
                //  Debug.Log("hard");
                SceneManager.LoadScene("GameScene");
                Helper.difficulty = 3;
                Debug.Log(Helper.difficulty);
                break;
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
