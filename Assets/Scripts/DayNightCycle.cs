using System.Collections;
using UnityEngine;


public class DayNightCycle : MonoBehaviour
{
    public static DayNightCycle Instance { get; private set; }

    public Camera mainCamera;

    public Color dayColor = Color.white;
    public Color nightColor = new Color(51f / 255f, 51f / 255f, 51f / 255f);

    [HideInInspector] public bool isNight = false;
    private float transitionDuration = 2f;
    private Coroutine currentTransition;

    public void Awake() => Instance = this;

    public void ToggleCycle()
    {
        isNight = !isNight;
        Color targetColor = isNight ? nightColor : dayColor;

        if(currentTransition != null) StopCoroutine(currentTransition);
        currentTransition = StartCoroutine(LerpColor(targetColor));
    }

    private IEnumerator LerpColor(Color targetColor)
    {
        float time = 0;
        Color startColor = mainCamera.backgroundColor;

        while(time < transitionDuration)
        {
            mainCamera.backgroundColor = Color.Lerp(startColor, targetColor, time / transitionDuration);
            time += Time.deltaTime;
            yield return null;
        }

        mainCamera.backgroundColor = targetColor;
    }
}
