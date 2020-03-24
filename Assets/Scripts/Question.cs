using UnityEngine;

[System.Serializable]
public class Question
{

    public String category;
    public String type;
    public String difficulty;
    public String question;
    public String correct_answer;
    public String[] incorrect_answers;

   public static Question CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Question>(jsonString);
    }

}

