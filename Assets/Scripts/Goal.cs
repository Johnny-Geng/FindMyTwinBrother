using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField]
    Shape.ShapeType myShapeType = Shape.ShapeType.None;

    [SerializeField]
    int myShapeSize = 1;

    AlternateSprites alternateSprites;

    bool canGoNextLevel = false;
    private void Start()
    {
        alternateSprites = GetComponentInChildren<AlternateSprites>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (myShapeType == pc.currentShape && myShapeSize == pc.currentShapeSize)
        {
            if (alternateSprites != null)
            {
                alternateSprites.canShow = true;
                canGoNextLevel = true;
                StartCoroutine(alternateSprites.Alternate());
                StartCoroutine(TrackSpace());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        if (alternateSprites != null)
        {
            canGoNextLevel = false;
            alternateSprites.canShow = false;
        }
    }

    IEnumerator TrackSpace()
    {
        while (canGoNextLevel)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                GameManager.instance.LoadNextLevel();
                yield break;
            }
            yield return null;
        }
    }
}
