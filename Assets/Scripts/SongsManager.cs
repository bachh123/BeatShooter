using UnityEngine;
using MidiParser;
using System.Collections.Generic;

public class SongsManager : MonoBehaviour
{
    public static SongsManager Instance;
    public AudioSource audioSource;
    public Lane[] lanes;
    public string fileLocation = "Unity.mid";
    public float songDelayInSeconds = 0.1f;

    public float noteTime = 1;

    public static float velocity;

    public static MidiFile midiFile;

    void Start()
    {
        Instance = this;
        ReadFile();
    }

    private void ReadFile()
    {
        midiFile = new MidiFile(Application.dataPath + "/Songs/" + fileLocation);

        var ticksPerQuarterNote = midiFile.TicksPerQuarterNote;

        double timeRate = 500 / ticksPerQuarterNote;

        foreach (var track in midiFile.Tracks)
        {
            foreach (var midiEvent in track.MidiEvents)
            {
                if (midiEvent.MidiEventType == MidiEventType.NoteOn)
                {
                    double time = midiEvent.Time * timeRate;
                    int note = midiEvent.Note;
                    velocity = midiEvent.Velocity;
                    SetTimeStamps(time, note);
                }
            }
        }

        Invoke(nameof(StartSong), songDelayInSeconds);
    }

    void SetTimeStamps(double timeStamps, int noteStamps)
    {
        foreach (var lane in lanes) lane.SetTimeStamps(timeStamps, noteStamps);
    }

    public void StartSong()
    {
        audioSource.Play();
    }

    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }
}