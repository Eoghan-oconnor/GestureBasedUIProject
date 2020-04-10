using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class SpeechRecognitionEngine : MonoBehaviour
{
    public string[] keywords = new string[] { "First", "Second", "Third", "Fourth", "Play", "Settings", "Pause"};
    public ConfidenceLevel confidence = ConfidenceLevel.Low;
    public float speed = 1;

    public Text results;
    public Image target;

    protected PhraseRecognizer recognizer;
    protected string word = " ";

    public GameObject pauseMenuUI;

    public bool IsGamePaused = false;


    
    private void Start()
    {
        if (keywords != null)
        {
            recognizer = new KeywordRecognizer(keywords, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }

        pauseMenuUI = GameObject.FindGameObjectWithTag("Pause");

        if(pauseMenuUI == null){
            Debug.Log("*****");
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
            case "First":
                break;
            case "Second":
                break;
            case "Third":
                break;
            case "Fourth":
                break;
            case "Play":
                Pause();
                break;
            case "Settings":
                break;
            case "Pause":
                //Debug.Log("true");
                Pause();
                break;
            

        }

        //target.transform.position = new Vector3(x, y, 0);
    }

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }  

    void Pause(){
        if(IsGamePaused){

            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            IsGamePaused = false;
           
        } else {

            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            IsGamePaused = true;
            
        }
    }

    
}
