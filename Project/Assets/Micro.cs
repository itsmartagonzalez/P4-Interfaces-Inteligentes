using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Micro : MonoBehaviour {
    public AudioSource Mic;
    Button StartStopButton;
    Button PlayButton;
    Text StartStopButtonText;
    Text PlayButtonText;
    private IEnumerator coroutine;
    float startRecordingTime, stopRecordingTime;

    // Start is called before the first frame update
    void Start() {
        Mic = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        
        StartStopButton = GameObject.Find("Start / Stop Mic").GetComponent<Button>();
        StartStopButtonText = StartStopButton.transform.Find("Text").GetComponent<Text>();
        StartStopButton.onClick.AddListener(StartStopClicked);

        PlayButton = GameObject.Find("Play Mic").GetComponent<Button>();
        PlayButtonText = PlayButton.transform.Find("Text").GetComponent<Text>();
        PlayButton.onClick.AddListener(PlayClicked);
        PlayButton.gameObject.SetActive(false);
    }

    public void StartStopClicked() {
        string microName = Microphone.devices[0];
        if (Microphone.IsRecording(microName)) {
            stopRecordingTime = Time.time;
            Microphone.End(microName);
            StartStopButtonText.text = "Start Recording";
            PlayButton.gameObject.SetActive(true);
        } else {
            startRecordingTime = Time.time;
            Mic.clip = Microphone.Start(microName, true, 10, 44100);
            StartStopButtonText.text = "Stop Recording";
            PlayButton.gameObject.SetActive(false);
        }
    }

    public void PlayClicked() {
        Mic.Play();
        StartCoroutine(PlayingText());
    }

    IEnumerator PlayingText() {
        PlayButtonText.text = "Playing"; 
        yield return new WaitForSeconds(stopRecordingTime - startRecordingTime);
        PlayButtonText.text = "Play"; 
    }
}
