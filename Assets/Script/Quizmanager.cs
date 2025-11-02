using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI; 
using TMPro; 


public class QuizManager : MonoBehaviour
{
    public List<QuestionControl> quizdatabase;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI scoreText;
    public List<Button> answerButton;
    private int questionIndex = 0;
    private int totalScore = 0;

    void Start()
    {
        questionIndex = 0;
        totalScore = 0;
        UpdateScoreUI();
        TampilkanPertanyaan();
    }

    void TampilkanPertanyaan()
    {
        QuestionControl kuis = quizdatabase[questionIndex];
        questionText.text = kuis.question;
        for (int i = 0; i < answerButton.Count; i++)
        {
            if (i < kuis.answer.Length)
            {
                answerButton[i].gameObject.SetActive(true);
                answerButton[i].GetComponentInChildren<TextMeshProUGUI>().text = kuis.answer[i];
                answerButton[i].onClick.RemoveAllListeners();
                int indeksTombol = i;
                answerButton[i].onClick.AddListener(() => CekJawaban(indeksTombol));
            }
            else
            {
                answerButton[i].gameObject.SetActive(false);
            }
        }
    }

    void CekJawaban(int indeksPilihan)
{
    QuestionControl kuis = quizdatabase[questionIndex];
    
    if (indeksPilihan == kuis.rightans)
    {
        Debug.Log("JAWABAN BENAR!");
        totalScore += kuis.score; // Tambah skor jika benar
    }
    else
    {
        Debug.Log("JAWABAN SALAH!");
        totalScore -= 5; // Kurangi 5 poin jika salah
        if (totalScore < 0)
            totalScore = 0; // Pastikan skor tidak negatif
    }
    
    UpdateScoreUI();
    LanjutKePertanyaanBerikutnya();
}

    void UpdateScoreUI()
    {
        if(scoreText != null)
        {
            scoreText.text = "Score = " + totalScore; 
        }
    }

    void LanjutKePertanyaanBerikutnya()
    {
        questionIndex++;

        if (totalScore >= 0)
        {
            if (questionIndex < quizdatabase.Count)
            {
                TampilkanPertanyaan();
            }
            else
            {
                questionText.text = "Quiz Complete!";
                foreach (Button tombol in answerButton)
                {
                    tombol.gameObject.SetActive(false);
                }
                scoreText.text = "Your Score = " + totalScore;
            }
        }
        else
        {
            questionText.text = "You Lose";
            foreach (Button tombol in answerButton)
            {
                tombol.gameObject.SetActive(false);
            }
            scoreText.text = "Your Score = " + totalScore;
        }
    }
}
