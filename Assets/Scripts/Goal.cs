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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (myShapeType == pc.currentShape && myShapeSize == pc.currentShapeSize)
        {
            pc.rb.constraints = RigidbodyConstraints2D.FreezePosition;
            Invoke(nameof(LoadNextLevel),2);
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
