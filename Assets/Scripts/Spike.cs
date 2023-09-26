using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        ReducePlayerShape(pc);
    }

    private void ReducePlayerShape(PlayerController pc)
    {
        var currentScale = pc.transform.localScale;
        currentScale.x -= 1;
        currentScale.y -= 1;
        pc.transform.localScale = currentScale;

        pc.currentShapeSize -= 1;

        Destroy(gameObject);
    }
}
