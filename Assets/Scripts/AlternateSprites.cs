using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateSprites : MonoBehaviour
{
    [SerializeField]
    List<Sprite> sprites;
    int currentIndex = 0;

    SpriteRenderer spriteRenderer;

    public bool canShow = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator Alternate()
    {
        while (canShow)
        {
            if (currentIndex + 1 < sprites.Count)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }
            spriteRenderer.sprite = sprites[currentIndex];
            yield return new WaitForSeconds(0.3f);
        }
        spriteRenderer.sprite = null;
    }
}
