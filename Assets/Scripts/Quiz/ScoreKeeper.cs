using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswer  = 0;
    int questionSeen = 0;

   public int GetCorrectAnswers()
   {
       return correctAnswer;
   }

   public void IncrementCorrectAnswers()
   {
       correctAnswer++;
   } 

   public int GetQuestionSeen()
   {
       return questionSeen;
   }

   public void IncrementQuestionsSeen()
   {
       questionSeen++;
   }

   public int CalculateScore()
   {
       return Mathf.RoundToInt(correctAnswer / (float)questionSeen * 100);
   }
}
