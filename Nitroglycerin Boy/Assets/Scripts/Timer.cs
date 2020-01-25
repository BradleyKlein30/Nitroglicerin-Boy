using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Timer : MonoBehaviour
{
    [Header("Text")]
    public Text timerText;
    private char characterSplitter = ':';

    [Header("Time Variables")]
    public float goldSecondsLimit;
    public float silverSecondsLimit;

    [Header("UI")]
    public Image pointer;
    public RawImage goldMedal;
    public RawImage silverMedal;
    public RawImage bronzeMedal;
    public GameObject retryButton;
    public GameObject exitToHUBButton;

    private float startTime;
    private bool finished = false;

    private float t;
    private float seconds;
    private float minutes;
    private float hours;

    [Header("AUDIOCLIPS")]
    public AudioClip victory;

    [Header("COMPONENTS")]
    public AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (finished) return;

        t = Time.time - startTime;
        seconds = (int)(t % 60);
        minutes = (int)(t / 60) % 60;
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
        exitToHUBButton.SetActive(true);
        retryButton.SetActive(true);
        GameObject.Find("First Person Player/Main Camera").GetComponent<MouseLook>().enabled = false;
        GameObject.Find("First Person Player/Main Camera/Right Arm").GetComponent<ProyectileSpawner>().enabled = false;
        Cursor.lockState = CursorLockMode.None;

        if ((t <= goldSecondsLimit))
        {
            Debug.Log("Gold Medal");
            goldMedal.enabled = true;
            audio.clip = victory;
            audio.pitch = 2;
            audio.Play();
        }
        if ((t <= silverSecondsLimit) && (t > goldSecondsLimit))
        {
            Debug.Log("Silver Medal");
            silverMedal.enabled = true;
            audio.clip = victory;
            audio.pitch = 1;
            audio.Play();
        }
        if ((t >= (silverSecondsLimit + 1)))
        {
            Debug.Log("Bronze Medal");
            bronzeMedal.enabled = true;
            audio.clip = victory;
            audio.pitch = 0.5f;
            audio.Play();
        }
    }
}
