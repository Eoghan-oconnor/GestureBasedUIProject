using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Web;

public static class Helper
{
    public static int difficulty;
    public static GameObject pausemenuUI;

    public static bool IsGamePaused = false;
    
    // Used to shuffle correct answer with incorrect answers
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    public static void RestartGame()
    {
        Application.LoadLevel(0);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static string RemoveSpecialChars(this string data)
    {
        return HttpUtility.HtmlDecode(data);
    }
}
