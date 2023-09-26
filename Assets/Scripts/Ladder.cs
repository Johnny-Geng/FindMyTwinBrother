using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] Transform TopPosition;
    [SerializeField] Transform BotPosition;
    enum PlayerPosition
    {
        None, Top, Bot, Mid
    }

    PlayerPosition ps = PlayerPosition.None;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        float fromTop = Vector2.Distance(pc.transform.position, TopPosition.position);
        float fromBot = Vector2.Distance(pc.transform.position, BotPosition.position);
        if (fromTop < fromBot)
        {
            ps = PlayerPosition.Top;
        }
        else if (fromBot < fromTop)
        {
            ps = PlayerPosition.Bot;
        }
        else
        {
            ps = PlayerPosition.Mid;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (!pc.isClimbing)
        {
            // climp up
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) &&
                (ps == PlayerPosition.Mid || ps == PlayerPosition.Bot))
            {
                StartCoroutine(Climb(pc, other, TopPosition.position));
            }
            // climp down
            else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) &&
                     (ps == PlayerPosition.Mid || ps == PlayerPosition.Top))
            {
                StartCoroutine(Climb(pc, other, BotPosition.position));
            }
        }
    }

    IEnumerator Climb(PlayerController pc, Collider2D collider, Vector2 target)
    {
        pc.isClimbing = true;
        pc.rb.gravityScale = 0;
        collider.enabled = false;
        pc.rb.velocity = new Vector2(0, 0);
        while (Vector2.Distance(pc.transform.position, target) > 1)
        {
            pc.transform.position = Vector2.MoveTowards(pc.transform.position, target, pc.climbSpeed * Time.deltaTime);
            yield return null;
        }
        pc.isClimbing = false;
        pc.rb.gravityScale = 1;
        collider.enabled = true;
    }
}
