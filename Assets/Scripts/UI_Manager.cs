using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI bestScoreText;
    [SerializeField]
    private Image _lifeImage;
    [SerializeField]
    private SpriteRenderer _spaceBG;
    [SerializeField]
    private GameObject _pausePanel;
    [SerializeField]
    private Sprite[] _lifeSprites;
    [SerializeField]
    private TextMeshProUGUI game_over_text;
    private bool _isGameOver = false;

    int _bestScore;
    int _bestScore_Single;
    int _checkScore;
    
    // Start is called before the first frame update
    void Start()
    {
        //AdjustResolution();

        scoreText.text = "Score: 0";
        if (SceneManager.GetActiveScene().name == "Game")
        {
            _bestScore_Single = PlayerPrefs.GetInt("BestScore_Single", 0);
            bestScoreText.text = "Best: " + PlayerPrefs.GetInt("BestScore_Single", 0).ToString();
        }
        else
        {
            _bestScore = PlayerPrefs.GetInt("BestScore", 0);
            bestScoreText.text = "Best: " + PlayerPrefs.GetInt("BestScore", 0).ToString();
        }
        
    }

    private void AdjustResolution()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        Debug.Log("Screen Resolution: " + screenWidth + "x" + screenHeight);
        float spriteWidth = _spaceBG.sprite.bounds.size.x;
        Debug.Log(spriteWidth);

        if (screenWidth<=1920)
        {
            _spaceBG.gameObject.transform.localScale = new(1.09f,1.15f,1);
        }
        else if (screenWidth >=2400)
        {
            _spaceBG.gameObject.transform.localScale = new(1.37f, 1.15f, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyUp(KeyCode.Escape) && !_pausePanel.activeInHierarchy)
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyUp(KeyCode.P) && !_pausePanel.activeInHierarchy)
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyUp(KeyCode.P) && _pausePanel.activeInHierarchy)
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }

    }
    public void UpdateScore(int score)
    {
        _checkScore = score;
        scoreText.text = "Score: "+ score.ToString();
        if(SceneManager.GetActiveScene().name == "Game")
        {
            CheckBestScore(_bestScore_Single);
        }
        else
        {
            CheckBestScore(_bestScore);
        }
        
    }
    public void UpdateLife(int currentLife)
    {
        if (currentLife < 0)
        {
            _lifeImage.sprite = _lifeSprites[0];
        }
        else
        {
            _lifeImage.sprite = _lifeSprites[currentLife];
        }
        
        if (currentLife == 0)
        {
            game_over_text.gameObject.SetActive(true);
            _isGameOver=true;
            StartCoroutine(GameOverFlicker());
            //CheckBestScore();
        }

    }

    private void CheckBestScore(int _bestScore)
    {
        
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (_checkScore >= _bestScore)
            {
                PlayerPrefs.SetInt("BestScore_Single", _checkScore);
                bestScoreText.text = "Best: " + PlayerPrefs.GetInt("BestScore_Single", 0).ToString();
            }
        }
        else
        {
            if (_checkScore >= _bestScore)
            {
                PlayerPrefs.SetInt("BestScore", _checkScore);
                bestScoreText.text = "Best: " + PlayerPrefs.GetInt("BestScore", 0).ToString();
            }
        }
    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            game_over_text.text = "GAME OVER";
            yield return new WaitForSeconds(.5f);
            game_over_text.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Resume()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
