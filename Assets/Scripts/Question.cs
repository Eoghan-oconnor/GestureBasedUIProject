using System;

namespace JSONQuestionParser
{
    public class Question
    {
        public Question(String category, String type, String difficulty, String question, String correct_answer, String[] wrong_answers)
        {
            this.category = category;
            this.type = type;
            this.difficulty = difficulty;
            this.question = question;
            this.correct_answer = correct_answer;
            this.incorrect_answers = incorrect_answers;
        }

        public String category { get; set; }
        public String type { get; set; }
        public String difficulty { get; set; }
        public String question { get; set; }
        public String correct_answer { get; set; }
        public String[] incorrect_answers { get; set; }
    }
}
