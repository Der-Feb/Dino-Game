using UnityEngine;


public class DayNightCycle : MonoBehaviour
{
    public static DayNightCycle Instance { get; private set; }

    public Camera mainCamera;

    private Color dayColor = Color.white;
    private Color nightColor = new Color(51f / 255f, 51f / 255f, 51f / 255f);

    [HideInInspector]
    public bool isNight = false;

    public void Awake()
    {
        Instance = this;
    }

    public void SetNight()
    {
        mainCamera.backgroundColor = nightColor;
        isNight = true;
    }

    public void SetDay()
    {
        mainCamera.backgroundColor = dayColor;
        isNight = false;
    }
}
