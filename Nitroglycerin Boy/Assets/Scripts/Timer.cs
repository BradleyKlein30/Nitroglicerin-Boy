using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    [Header("Text")]
    public Text timerText;
    private char characterSplitter = ':';

    [Header("Time Variables")]
    public float goldSeconds;
    public float silverSecondsMin;
    public float silverMinutesMax;
    public float bronzeMinutesMin;

    public Image pointer;
    public RawImage goldMedal;
    public RawImage silverMedal;
    public RawImage bronzeMedal;

    private float startTime;
    private bool finished = false;

    private float t;
    private float seconds;
    private float minutes;
    private float hours;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (finished) return;

        t = Time.time - startTime;
        seconds = (t % 60);
        minutes = ((int)(t / 60) % 60);
        hours = (int)(t / 3600);

        timerText.text
            = hours.ToString("00") + characterSplitter
            + minutes.ToString("00") + characterSplitter
            + seconds.ToString("00");
    }

    public void Finish()
    {
        finished = true;
        timerText.color = Color.yellow;
    }

    public void Win()
    {
        pointer.enabled = false;

        if((seconds <= goldSeconds))
        {
            Debug.Log("Gold Medal");
            goldMedal.enabled = true;
        }

        if ((minutes < silverMinutesMax) && (seconds > silverSecondsMin))
        {
            Debug.Log("Silver Medal");
            silverMedal.enabled = true;
        }

        if ((minutes >= bronzeMinutesMin))
        {
            Debug.Log("Bronze Medal");
            bronzeMedal.enabled = true;
        }
    }
}
