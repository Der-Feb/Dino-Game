using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        frame = (frame + 1) % sprites.Length;

        if(frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }

        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }

}
