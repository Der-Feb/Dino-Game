using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum SoundType
{
    Jump, 
    Die,
    Score
}

public class GameManager : MonoBehaviour
{
    // a game-manager singleton for the application
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = .1f;
    public float gameSpeed { get; private set; }

    private Player player;
    private Spawner spawner;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiScoreText;
    public Button resetButton;

    [HideInInspector]
    public float score;

    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioClip jumpSound;
    public AudioClip dieSound;
    public AudioClip scoreSound;

    public float nextScoreMilestone = 100f;
    public float nextCycleScore = 200f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // destroy if another exists
            DestroyImmediate(Instance);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        spawner = FindAnyObjectByType<Spawner>();

        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsByType<Obstacle>();
        foreach(var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialGameSpeed;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        gameOverText.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);

        score = 0;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

        gameOverText.gameObject.SetActive(true);
        resetButton.gameObject.SetActive(true);

        UpdateHiScore();
        PlaySound(SoundType.Die);
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;

        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");

        if(score >= nextScoreMilestone)
        {
            PlaySound(SoundType.Score);
            nextScoreMilestone += nextScoreMilestone;
        }

        if(score >= nextCycleScore)
        {
            if(!DayNightCycle.Instance.isNight)
            {
                DayNightCycle.Instance.SetNight();
            }
            else
            {
                DayNightCycle.Instance.SetDay();  
            }

            nextCycleScore += nextCycleScore;
        }
    }

    private void UpdateHiScore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if(score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        hiScoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }

    public void PlaySound(SoundType type)
    {
        switch(type)
        {
            case SoundType.Jump:
                sfxSource.PlayOneShot(jumpSound);
                break;

            case SoundType.Die:
                sfxSource.PlayOneShot(dieSound);
                break;

            case SoundType.Score:
                sfxSource.PlayOneShot(scoreSound);
                break;
        }
    }
}
