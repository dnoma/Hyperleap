using Cainos.PixelArtPlatformer_VillageProps;
using DG.Tweening;
using System;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    public GameObject powerupPrefab; // Assign a powerup prefab in Inspector
    private bool isHit = false;

    public float upY = .1f, downY = .05f;
    public float timeupY = .1f, timedownY = .05f;

    void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("Question block Colliding Player");

        if (collision.gameObject.CompareTag("Player") && !isHit)
        {
            if (collision.transform.position.y < transform.position.y)
            {
                isHit = true;
                // Spawn powerup
                GameObject powerup = Instantiate(powerupPrefab, transform.position + Vector3.up, Quaternion.identity);
                Debug.Log("Powerup Released!");
                AnimateTile();

                try {
                    GetComponentInChildren<Chest>().Open();
                }
                catch
                {

                }
                
            }
        }
    }

    private void AnimateTile()
    {
        //var upPostion = transform.position;
        //upPostion.y += upY;

        //var downPostion = transform.position;
        //upPostion .y += downY;

        var orignal = transform.position.y;

        var sequence = DOTween.Sequence ();

        sequence.Append(transform.DOMoveY((orignal + upY), timeupY));
        sequence.Append(transform.DOMoveY((orignal + downY), timedownY));
        sequence.Append(transform.DOMoveY(orignal, timedownY));

        sequence.Play();
    }

    private void Update()
    {
        if (isHit)
        {

        }
    }
}