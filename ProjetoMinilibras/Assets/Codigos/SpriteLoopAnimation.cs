using UnityEngine;
using UnityEngine.UI;

public class SpriteLoopAnimation : MonoBehaviour
{
    public Sprite[] frames;          // Imagens do tipo sprite
    public float frameRate = 10f;    // Quadros por segundo (velocidade da animação)

    private int currentFrame;
    private float timer;

    void Update()
    {
        if (frames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= 1f / frameRate)
        {
            currentFrame = (currentFrame + 1) % frames.Length;
            GetComponent<Image>().sprite = frames[currentFrame];
            timer = 0f;
        }
    }
}