using UnityEngine;

public class GameManager : MonoBehaviour
{
    // a game-manager singleton for the application
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = .1f;
    public float gameSpeed { get; private set; }

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

    private void Oestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        gameSpeed = initialGameSpeed;
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
    }
}
