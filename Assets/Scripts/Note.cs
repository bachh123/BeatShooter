using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    double timeInstantiated;
    public double assignedTime;

    void Start()
    {
        timeInstantiated = SongsManager.GetAudioSourceTime();
    }

    void Update()
    {
        double timeSinceInstantiated = SongsManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongsManager.Instance.noteTime * 2));

        if (t > 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Hit();
        }
    }

    void Hit()
    {
        ScoreManager.Instance.Hit();
        Destroy(gameObject);
    }
}