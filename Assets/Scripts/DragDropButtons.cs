// Diseñado y desarrollado por Miguel I. Fernandez
// Email: misaacf30@gmail.com
// Por favor, no dudes en contactarme si necesitas ayuda.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DragDropButtons : MonoBehaviour
{
    public GameObject DropSlot1;
    public GameObject DropSlot2;
    public AudioSource[] questions;
    public AudioSource[] answerSounds;       // [0] = right, [1] = wrong

    int correctAnswer;

    private void Start()
    {
        int randomIndex = Random.Range(0, questions.Length);
        questions[randomIndex].PlayDelayed(0.5f);
        correctAnswer = int.Parse(questions[randomIndex].clip.name);
    }

    public void CheckAnswer()
    {
        int? num1 = null;
        int? num2 = null;
        int? answer = null;

        if (DropSlot1.GetComponentInChildren<Text>() != null)
            num1 = int.Parse((DropSlot1.GetComponentInChildren<Text>().text).Substring(0,1));
        if (DropSlot2.GetComponentInChildren<Text>() != null)
            num2 = int.Parse((DropSlot2.GetComponentInChildren<Text>().text).Substring(0,1));

        if (num1 == null && num2 == null)
        {
            answerSounds[1].Play();
            return;
        }
        else if (num1 != null && num2 != null)
            answer = num1 * 10 + num2;
        else if (num1 != null && num2 == null)
            answer = num1;
        else if (num2 != null && num1 == null)
            answer = num2;

        if(answer == correctAnswer)
        {
            answerSounds[0].Play();
            this.GetComponent<Button>().interactable = false;
        }

        else
        {
            answerSounds[1].Play();
        }
        
        if (answer != null)
            Debug.Log(answer);

        
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
