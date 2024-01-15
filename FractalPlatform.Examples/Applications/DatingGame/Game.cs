using System;
using System.Collections.Generic;

namespace FractalPlatform.Examples.Applications.DatingGame
{
    public class AnswerQuestion
    {
        public string From { get; set; }

        public string Question { get; set; }

        public string To { get; set; }

        public string Answer { get; set; }
    }

    public class Game
    {
        public uint ID { get; set; }

        public GameStatus Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpiredParticipants { get; set; }

        public DateTime ExpiredAnswers { get; set; }
        
        public DateTime ExpiredChooses { get; set; }

        public List<Participant> Boys { get; set; }
        
        public List<Participant> Girls { get; set; }

        public List<AnswerQuestion> AnswerQuestions { get; set; }
    }
}
