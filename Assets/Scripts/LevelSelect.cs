using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public static LevelSelect instance;

    void Awake() {
        instance = this;
    }

    public float selectTime = 2;
    public GameObject[] easy, medium, hard;
    public GameObject winScreen, loseScreen, selectionScreen, currentLevel, pipe;
    public Text timer, choiceText;
    public Image flood;
    public float floodSpeed;
    public bool selecting, waiting;

    public AudioSource music, sfx;
    public AudioClip main, countdown, win, loss;
    public int choice = 0;

    public void SelectLevel(int i) {
        choice = i;
        choiceText.text = (choice == 0 ? "Easy" : choice == 1 ? "Medium" : "Hard");
    }

    public void GoToSelection() {
        selecting = true;
        selectTime = 2;
        Destroy(currentLevel);
        selectionScreen.SetActive(true);
        floodSpeed = 0;
        flood.fillAmount = 0;
        timer.text = "2";
        timer.gameObject.SetActive(true);
    }

    void Update() {
        if (Input.GetKeyDown("c"))
            Time.timeScale = Time.timeScale == 1 ? .5f : 1;
        if (selecting) {
            selectTime -= Time.deltaTime;
            timer.text = "" + (((int)(selectTime * 10))/10f);
            if(selectTime < 0) {
                selecting = false;
                GameObject[] selection = (choice == 0 ? easy : choice == 1 ? medium : hard);
                currentLevel = Instantiate(selection[Random.Range(0, selection.Length)]);
                selectionScreen.SetActive(false);
                pipe.SetActive(true);
                music.clip = countdown;
                music.Play();
                waiting = true;
            }
        }
        else if(floodSpeed > 0 && flood.fillAmount < 1)
            flood.fillAmount += floodSpeed * Time.deltaTime;
        if(waiting && flood.fillAmount > .2){
            waiting = false;
            timer.gameObject.SetActive(false);
            loseScreen.SetActive(true);
            sfx.clip = loss;
            sfx.Play();
            music.clip = main;
            music.Play();
        }
    }

    public void Finish() {
        if(!waiting)
            return;
        waiting = false;
        timer.gameObject.SetActive(false);
        if(floodSpeed > 0) {
            loseScreen.SetActive(true);
            sfx.clip = loss;
        }
        else {
            winScreen.SetActive(true);
            sfx.clip = win;
        }
        sfx.Play();
        music.clip = main;
        music.Play();
    }

    public void Lose() {
        if(!waiting)
            return;
        waiting = false;
        timer.gameObject.SetActive(false);
        loseScreen.SetActive(true);
        sfx.clip = loss;
        sfx.Play();
        music.clip = main;
        music.Play();
    }
}
