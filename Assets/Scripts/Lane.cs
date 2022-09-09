using UnityEngine;
using MidiParser;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Lane : MonoBehaviour
{
    public static Lane Instance;
    public int noteType;
    public GameObject notePrefab;
    public int laneX;

    public List<double> timeStamps = new List<double>();

    int spawnIndex = 0;

    void Start()
    {
        Instance = this;
    }

    public void SetTimeStamps(double time, int note)
    {
        if (note == noteType)
        {
            timeStamps.Add(time / 1000);
        }
    }

    private void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (SongsManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongsManager.Instance.noteTime)
            {
                var laneZ = timeStamps[spawnIndex] * SongsManager.velocity;
                var note = Instantiate(notePrefab, new Vector3(Random.Range(-2f, 2f), 1, (float)laneZ), Quaternion.identity);
                note.transform.parent = gameObject.transform;
                note.GetComponent<Note>().assignedTime = timeStamps[spawnIndex];
                spawnIndex++;
            }
        }
    }
}