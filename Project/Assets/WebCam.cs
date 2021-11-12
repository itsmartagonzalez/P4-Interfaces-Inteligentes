using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCam : MonoBehaviour {

    WebCamTexture webCam;
    public RawImage display;
    Button StartStopButton;
    Button PlayPauseButton;
    Text StartStopButtonText;
    Text PlayPauseButtonText;
    List<Texture> stories = new List<Texture>();
    int storyCounter;

    // Start is called before the first frame update
    void Start() {
        display = GetComponent<RawImage>();
        StartStopButton = GameObject.Find("Start / Stop Cam").GetComponent<Button>();
        StartStopButtonText = StartStopButton.transform.Find("Text").GetComponent<Text>();
        StartStopButton.onClick.AddListener(StartStopClicked);

        PlayPauseButton = GameObject.Find("Play / Pause Cam").GetComponent<Button>();
        PlayPauseButtonText = PlayPauseButton.transform.Find("Text").GetComponent<Text>();
        PlayPauseButton.onClick.AddListener(PlayPauseClicked);
        PlayPauseButton.gameObject.SetActive(false);
    }

    public void StartStopClicked() {
        if (webCam != null) {
            display.texture = null;
            webCam.Stop();
            webCam = null;
            StartStopButtonText.text = "Play";
            PlayPauseButton.gameObject.SetActive(false);
            showStories();
        } else {
            StartCoroutine(PlayFor10Seconds());
        }
    }

    public void PlayPauseClicked() {
        if (webCam.isPlaying) {
            webCam.Pause();
            PlayPauseButtonText.text = "Play";
        } else {
            webCam.Play();
            PlayPauseButtonText.text = "Pause";
        }
    }

    IEnumerator PlayFor10Seconds() {
        webCam = new WebCamTexture(WebCamTexture.devices[0].name);
        display.texture = webCam;
        webCam.Play();
        StartStopButtonText.text = "Stop";
        PlayPauseButton.gameObject.SetActive(true);
        PlayPauseButtonText.text = "Pause";
        yield return new WaitForSeconds(10);
        stories.Add(display.texture);
        display.texture = null;
        webCam = null;
        PlayFor10Seconds();
    }

    public void showStories() {
        display.texture = stories.ElementAt(0);
    }
}
