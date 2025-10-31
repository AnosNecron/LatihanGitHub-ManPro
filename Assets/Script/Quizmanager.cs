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
            // Cek apakah ada cukup pilihan jawaban untuk tombol ini
            if (i < kuis.answer.Length)
            {
                answerButton[i].gameObject.SetActive(true);

                // Ganti teks di dalam tombol
                // Kita cari komponen TextMeshPro di dalam tombol
                answerButton[i].GetComponentInChildren<TextMeshProUGUI>().text = kuis.answer[i];

                // Penting: Hapus listener/fungsi lama agar tidak menumpuk
                answerButton[i].onClick.RemoveAllListeners();

                // Salin nilai 'i' ke variabel lokal
                // Ini PENTING agar fungsi di bawah tahu tombol mana (indeks ke berapa)
                // yang sedang di-set
                int indeksTombol = i;

                // Tambahkan fungsi baru saat tombol diklik
                answerButton[i].onClick.AddListener(() => CekJawaban(indeksTombol));
            }
            else
            {
                // Jika pertanyaan ini pilihannya lebih sedikit (misal cuma 2),
                // sembunyikan tombol ekstra
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
            totalScore++;
            totalScore = totalScore + kuis.score;
            
            // (Di sini Anda bisa tambahkan skor)
        }
        else
        {
            Debug.Log("JAWABAN SALAH!");
            totalScore--;
            totalScore = totalScore + kuis.score;
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
        // Tambah indeks untuk pertanyaan selanjutnya
        questionIndex++;

        // Cek apakah pertanyaan sudah habis
        if (totalScore >= 0)
        {
            if (questionIndex < quizdatabase.Count)
            {
                // Jika belum habis, tampilkan pertanyaan baru
                TampilkanPertanyaan();
            }
            else
            {
                questionText.text = "Quiz Complete!";
                // Sembunyikan semua tombol
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
            // Sembunyikan semua tombol
            foreach (Button tombol in answerButton)
            {
                tombol.gameObject.SetActive(false);
            }
            scoreText.text = "Your Score = " + totalScore;
        }
    }
}