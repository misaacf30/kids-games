// Diseñado y desarrollado por Miguel I. Fernandez
// Email: misaacf30@gmail.com
// Por favor, no dudes en contactarme si necesitas ayuda.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LongShort : MonoBehaviour
{
    public Button[] buttons;
    public Sprite[] spritesRow1;
    public Sprite[] spritesRow2;
    public AudioSource[] sounds;    // [0] = short , [1] = long
    public AudioSource[] answerSounds;  // 0 = right, 1 = wrong

    private int correctSoundIndex;
    private string audioName;    // 'short' or 'long'

    // Start is called before the first frame update
    void Start()
    {
        int randomRowIndex = Random.Range(0, 10);
        int randomBottom1 = Random.Range(0, 2);
        int randomBottom2;
        if (randomBottom1 == 0)
            randomBottom2 = 1;
        else
            randomBottom2 = 0;
        buttons[randomBottom1].GetComponent<Image>().sprite = spritesRow1[randomRowIndex];
        buttons[randomBottom2].GetComponent<Image>().sprite = spritesRow2[randomRowIndex];

        correctSoundIndex = Random.Range(0, 2);
        sounds[correctSoundIndex].PlayDelayed(0.5f);
        audioName = sounds[correctSoundIndex].clip.name; // 'short' or 'long'
    }

    public void onCick()
    {
        string clickedSpriteName = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name;
        var button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        var colors = button.colors;
        var disabledColor = colors.disabledColor;
        if (audioName == clickedSpriteName.Substring(0, audioName.Length))
        {
            disabledColor = Color.green;
            answerSounds[0].Play();
        }
        else
        {
            disabledColor = Color.red;
            answerSounds[1].Play();
        }
        colors.disabledColor = disabledColor;
        button.colors = colors;

        Button[] btns = EventSystem.current.currentSelectedGameObject.transform.parent.GetComponentsInChildren<Button>();
        foreach (Button btn in btns)
        {
            btn.interactable = false;
        }
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("PrevSceneIndex"));
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
