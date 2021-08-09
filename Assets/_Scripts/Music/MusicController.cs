using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    private SongSelector songSelector;
    

    public AudioSource song;

    
    [SerializeField] [HideInInspector] private int currentIndex = 0;

    public int samplesNumber;
    public int bandsNumber; 
    public float decreaseRatio;

    public static float songLength;
    public static float songPosition;
    public static float songPercentage;

    public static float amplitude, amplitudeBuffer;
    private float amplitudeHighest;
    private float[] samples;

    private float[] freqBand;
    private float[] bandBuffer;

    private float[] freqBandHigest;
    private float[] bufferDecrease;

    public static float[] audioBand;
    public static float[] audioBandBuffer;
    

    private void Start ()
    {
        GameObject songSelectorObject = GameObject.FindGameObjectWithTag("SongSelector");

        if (songSelectorObject != null)
        {
            songSelector = songSelectorObject.GetComponent<SongSelector>();
        }

        if (songSelector == null)
        {
            Debug.Log("cannot find 'MusicController' script");
        }
        

        if (song == null) song = gameObject.AddComponent<AudioSource>();
        
        samples = new float[samplesNumber];
        bufferDecrease = new float[bandsNumber];

        freqBand = new float[bandsNumber];
        bandBuffer = new float[bandsNumber];
        freqBandHigest = new float[bandsNumber];
        
        audioBand = new float[bandsNumber];
        audioBandBuffer = new float[bandsNumber];
            
        song = GetComponent<AudioSource>();
        
        song.clip = songSelector.clips[songSelector.songIndex];
        songLength = song.clip.length;
        
        song.Play();
    }
	
    private void Update ()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
        SongProgression();
    }

    private void GetSpectrumAudioSource()
    {
        song.GetSpectrumData(samples, 0, window: FFTWindow.Blackman);
    }

    private void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < bandsNumber; i++)
        {
            float average = 0;
            
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == bandsNumber - 1 )
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBand[i] = average * 10;
        }
    }

    private void BandBuffer()
    {
        for (int i = 0; i < bandsNumber; ++i)
        {
            if (freqBand[i] > bandBuffer[i])
            {
                bandBuffer[i] = freqBand[i];
                bufferDecrease[i] = 0.005f;
            }

            if (freqBand[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= decreaseRatio;
            }
        }
    }

    private void CreateAudioBands()
    {
        for (int i = 0; i < bandsNumber; i++)
        {
            if (freqBand[i] > freqBandHigest[i])
            {
                freqBandHigest[i] = freqBand[i];
            }

            audioBand[i] = (freqBand[i] / freqBandHigest[i]);
            audioBandBuffer[i] = (bandBuffer[i] / freqBandHigest[i]);
        }
    }

    private void GetAmplitude()
    {
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;

        for (int i = 0; i < bandsNumber; i++)
        {
            currentAmplitude = audioBand[i];
            currentAmplitudeBuffer = audioBandBuffer[i];
        }

        if (currentAmplitude > amplitudeHighest)
        {
            amplitudeHighest = currentAmplitude;
        }

        amplitude = currentAmplitude / amplitudeHighest;
        amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
    }

    private void SongProgression()
    {
        songPosition = song.time;
        
        songPercentage = (songPosition / songLength) * 100;
    }
}