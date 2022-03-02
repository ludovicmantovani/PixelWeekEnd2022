using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebrifManager : MonoBehaviour
{
    public Text maxScoreText;
    public Text trueScoreText;
    public Text falseScoreText;
    public Text totalScoreText;

    int bonus = 500;
    int mallus = -1000;


    int trueArt, falseArt, totalArt, maxScore;
    void Start()
    {
        trueArt = PlayerPrefs.GetInt("MatisseTrue");
        falseArt = PlayerPrefs.GetInt("MatisseFalse");
        //totalArt = PlayerPrefs.GetInt("Total");

        if (!PlayerPrefs.HasKey("BestScore"))
            PlayerPrefs.SetInt("BestScore", 0);
        maxScore = PlayerPrefs.GetInt("BestScore");

        int b_calc = bonus * trueArt;
        int m_calc = mallus * falseArt;
        maxScoreText.text = "Meilleur score : " + maxScore.ToString();
        trueScoreText.text = "Tableaux de Matisse volés : " + trueArt.ToString() + " x " + bonus.ToString() + " = " + b_calc.ToString();
        falseScoreText.text = "Autres tableaux volés : " + falseArt.ToString() + " x " + mallus.ToString() + " = " + m_calc.ToString();
        int t_calc = b_calc + m_calc;
        totalScoreText.text = "Total : " + t_calc.ToString();

        if (t_calc > maxScore)
        {
            PlayerPrefs.SetInt("BestScore", t_calc);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
