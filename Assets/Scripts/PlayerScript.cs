using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public string teamName;
    public Color color;

    public float scoreMultiphier = 8;
    public float collisionPenalty = 20.0f;
    public float respawnPenalty = 50.0f;

    public float score;
    bool calcScore;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = color;
    }

    // Update is called once per frame
    void UpdateScore()
    {
        score += Time.deltaTime * scoreMultiphier;
    }

    private void Update()
    {
        if (calcScore)
        {
            UpdateScore();
        }
    }

    public void StartScoreCount()
    {
        calcScore = true;
    }

    public void StopScoreCount()
    {
        calcScore = false;
    }

    public void PelalizeScoreOnCollision()
    {
        if (calcScore)
        {
            score -= collisionPenalty;
        }
    }

    public void PelalizeScoreOnRespawn() {
        if (calcScore) {
            score -= respawnPenalty;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")  {
            if (calcScore) {
                PelalizeScoreOnCollision();
            }
        }
    }
}
