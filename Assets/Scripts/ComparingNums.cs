// Diseñado y desarrollado por Miguel I. Fernandez
// Email: misaacf30@gmail.com
// Por favor, no dudes en contactarme si necesitas ayuda.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ComparingNums : MonoBehaviour
{
    public Text number1;
    public Text number2;
    public int maxNumber = 40;
    public AudioSource[] sounds;
    public AudioSource[] answerSounds;  // 0 = right, 1 = wrong

    private string soundName;
    private int answer;

    // Start is called before the first frame update
    void Start()
    {
        int num1 = Random.Range(0, maxNumber + 1);

        int min = num1 - 9;
        if (min <= 0)
            min = 1;
        int max = num1 + 9;
        if (max >= maxNumber)
            max = maxNumber;
        
        int num2 = Random.Range(min, max + 1);
        while(num2 == num1)
        {
            num2 = Random.Range(min, max + 1);
        }

        number1.text = num1.ToString();
        number2.text = num2.ToString();

        int randomAudioIndex = Random.Range(0, 2);
        sounds[randomAudioIndex].PlayDelayed(0.5f);
        soundName = sounds[randomAudioIndex].clip.name;    // greater or less
        
        if(soundName == "greater")
        {
            if(num1 > num2)
                answer = num1;
            else
                answer = num2;
        }
        else
        {
            if(num1 < num2)
                answer = num1;
            else
                answer = num2;
        }
    }

    public void OnClick()
    {
        string clickedButtonName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        var button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        var colors = button.colors;
        var disabledColor = colors.disabledColor;
        if (clickedButtonName == answer.ToString())
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
