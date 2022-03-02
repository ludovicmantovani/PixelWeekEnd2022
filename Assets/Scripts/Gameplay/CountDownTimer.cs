using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    #region PRIVATE VARIABLE
    // Game manager pour lancer la fin de partie
    [SerializeField] private GameManager _gameManager;
    // Temps de la partie (en secondes)
    [SerializeField] private float _timer = 2f * 60f; // deux minutes

    // Flag indiquant si le compte à rebour est lancé
    private bool _isRunning = false;
    // Texte d'affichage
    private Text _countdownText;
    #endregion

    #region BUILTIN METHOD
    void Start()
    {
        // Récupération du texte d'affichage
        _countdownText = GetComponentInChildren<Text>();
        // Lancement du compte à rebour
        _isRunning = true;
    }

    void Update()
    {
        // Test si le compte à rebour est lancé
        if (_isRunning)
        {
            if (_timer > 0)
            {
                // Décrémentation du temps restant
                _timer -= Time.deltaTime;

                if (_countdownText)
                {
                    if (_timer <= 10f)
                    {
                        // Texte en rouge
                        _countdownText.color = Color.red;
                    }

                    // Calcul et affichage du temps restant
                    int min = Mathf.FloorToInt(_timer / 60);
                    int sec = Mathf.FloorToInt(_timer % 60);
                    _countdownText.text = min.ToString("00") + ":" + sec.ToString("00");
                }
            }
            else
            {
                _isRunning = false;
                _timer = 0f;
                _countdownText.text = "00:00";
                if (_gameManager)
                    _gameManager.FinalCountDownHook();
            }
        }
    }
    #endregion
}
