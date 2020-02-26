using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// https://www.newtonsoft.com/json/help/html/ReadJson.htm

namespace JSONQuestionParser
{
    class MainClass
    {
        

        public static void Main(string[] args)
        {
            // Read in the json file
            List<Question> eq = (List<Question>) LoadQuestions();
            Question[] easy = eq.ToArray();

        
            Console.WriteLine("-------------- Question --------------");
            easy[21].question.Replace("&quot;", "'");
            Console.WriteLine(easy[21].question);


            Console.WriteLine("---------- Possible answers ----------");
            List<String> possibleAnswer = new List<String>();
            possibleAnswer.Add(eq[20].correct_answer);


            possibleAnswer.Add(eq[20].incorrect_answers[0]);
            possibleAnswer.Add(eq[20].incorrect_answers[1]);
            possibleAnswer.Add(eq[20].incorrect_answers[2]);

            int count = 0;

            foreach(string a in possibleAnswer)
            {
                count++;
                Console.WriteLine(count+". "+a);
            }
            

            Console.ReadLine();
            
        }

        public static IList<Question> LoadQuestions()
        {
            StreamReader json = File.OpenText("/Users/seanmoylan/Desktop/4th_Year/GestureBasedUI/JSONQuestionParser/JSONQuestionParser/easy.json");
            String jb = json.ReadToEnd();

            // Create a Jsonreader and read 
            JsonTextReader reader = new JsonTextReader(new StringReader(jb));
            IList<Question> fg = JsonConvert.DeserializeObject<List<Question>>(jb);
            
            return fg;
        }
    }
}
