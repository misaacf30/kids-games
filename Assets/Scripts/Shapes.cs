// Diseñado y desarrollado por Miguel I. Fernandez
// Email: misaacf30@gmail.com
// Por favor, no dudes en contactarme si necesitas ayuda.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Shapes : MonoBehaviour
{
    public Button[] buttons;
    public Sprite[] images;
    public AudioSource[] shapesSounds;    // [0] = short , [1] = long
    public AudioSource[] answerSounds;  // 0 = right, 1 = wrong

    private string correctAnswerName;

    // Start is called before the first frame update
    void Start()
    {
        int randomImage1 = Random.Range(0, images.Length);
        int randomImage2 = Random.Range(0, images.Length);
        while (randomImage2 == randomImage1)
            randomImage2 = Random.Range(0, images.Length);

        buttons[0].GetComponent<Image>().sprite = images[randomImage1];
        buttons[1].GetComponent<Image>().sprite = images[randomImage2];

        int correctIndex = Random.Range(0, buttons.Length);
        correctAnswerName = buttons[correctIndex].GetComponent<Image>().sprite.name;
        foreach (AudioSource sound in shapesSounds)
        {
            if (sound.clip.name == correctAnswerName)
                sound.PlayDelayed(0.5f);
        }
    }

    public void OnClick()
    {
        string clickedSpriteName = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name;
        var button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        var colors = button.colors;
        var disabledColor = colors.disabledColor;
        if (correctAnswerName == clickedSpriteName)
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