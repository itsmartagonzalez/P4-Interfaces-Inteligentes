using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Micro : MonoBehaviour {
    public AudioSource Mic;
    Button StartStopButton;
    Button PlayButton;
    Text StartStopButtonText;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start() {
        Mic = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        StartStopButton = GameObject.Find("Start / Stop Mic").GetComponent<Button>();
        StartStopButtonText = StartStopButton.transform.Find("Text").GetComponent<Text>();
        StartStopButton.onClick.AddListener(StartStopClicked);

        PlayButton = GameObject.Find("Play Mic").GetComponent<Button>();
        PlayButton.onClick.AddListener(PlayClicked);
        PlayButton.gameObject.SetActive(false);
    }

    public void StartStopClicked() {
        string microName = Microphone.devices[0];
        if (Microphone.IsRecording(microName)) {
            Microphone.End(microName);
            StartStopButtonText.text = "Start Recording";
            PlayButton.gameObject.SetActive(true);
        } else {
            Mic.clip = Microphone.Start(microName, true, 10, 44100);
            StartStopButtonText.text = "Stop Recording";
            PlayButton.gameObject.SetActive(false);
        }
    }

    public void PlayClicked() {
        Mic.Play();        
    }
}
