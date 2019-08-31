using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NarratorController : MonoBehaviour
{
    [SerializeField]
    private List<Dialogue> _dialogue = new List<Dialogue>();
    [SerializeField]
    private TextMeshProUGUI _textField = null;

    #region Dialogue references
    // Because once the game starts, NextDialogue increases
    // it to 1 when it should be 0.
    private int _currentDialogueIndex = -1;
    private bool _isItLoading = true;
    [SerializeField]
    private bool _isDialoguePlaying = true;
    private float plainTextLoadSpeed = 0.000001f;
    #endregion

    private AudioController _audioController = null;

    #region Dependencies
    [Space(10)]
    [SerializeField]
    private FadingController _fadingController = null;
    #endregion

    private void Start()
    {
        _fadingController = FindObjectOfType<FadingController>();
        _audioController = GetComponent<AudioController>();
        if (_isDialoguePlaying)
        {
            StartCoroutine(PlayDialogue(_currentDialogueIndex, _dialogue.Count - 1));
        } else
        {
            NextDialogue();
        }

        if (SceneManager.GetActiveScene().name == "Ending")
        {
            PlayerController.Instance.isGameCompleted = true;
        }
    }

    private void Update()
    {
        if (_isItLoading == true && _isDialoguePlaying == false)
        {
            if (Input.GetMouseButtonDown(0) && _currentDialogueIndex < _dialogue.Count - 1)
            {
                NextDialogue();
            }
            else if (Input.GetMouseButtonDown(1) && _currentDialogueIndex > 0)
            {
                PreviousDialogue();
            }
        }
    }

    private IEnumerator PlayDialogue(int fromIndex, int toIncludingIndex)
    {
        _currentDialogueIndex = fromIndex;
        for (int i = fromIndex; i < toIncludingIndex; i++)
        {
            yield return new WaitForSeconds(2);
            _currentDialogueIndex = i;
            NextDialogue();
            yield return new WaitForSeconds(_dialogue[_currentDialogueIndex].Delay);
        }

        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Intro")
        {
            if (_currentDialogueIndex == _dialogue.Count - 1)
            {
                _isDialoguePlaying = false;
                _fadingController.Fade("in", "MainScene");
            }
        } else if (currentScene == "Ending")
        {
            _fadingController.ResetAnimator();
            _fadingController.Fade("in", "MainScene");
            PlayerController.Instance.isGameCompleted = true;
        }
    }

    private void NextDialogue()
    {
        _currentDialogueIndex++;
        LoadText(_textField, _dialogue[_currentDialogueIndex].Sentence, plainTextLoadSpeed, _dialogue[_currentDialogueIndex].ClearScreen);

        Invoke("ToggleObjects", _dialogue[_currentDialogueIndex].Delay);
    }

    private void ToggleObjects()
    {
        foreach (GameObject obj in _dialogue[_currentDialogueIndex].ObjectToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }

        foreach (GameObject obj in _dialogue[_currentDialogueIndex].ObjectToDeactivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    private void PreviousDialogue()
    {
        _currentDialogueIndex--;
        LoadText(_textField, _dialogue[_currentDialogueIndex].Sentence, plainTextLoadSpeed, _dialogue[_currentDialogueIndex].ClearScreen);
    }


    private void LoadText(TextMeshProUGUI field, string text, float speed, bool clearScreen)
    {
        if (clearScreen)
        {
            _textField.text = string.Empty;
        } else
        {
            _textField.text += "\n";
        }
        _isItLoading = true;
        StartCoroutine(LoadTextCo(field, text, speed));
    }

    private IEnumerator LoadTextCo(TextMeshProUGUI field, string text, float speed)
    {
        AudioManager.Instance.AudioController.LoopSound("Text Displaying");
        AudioManager.Instance.AudioController.AudioSource.loop = true;
        string followingCharacter = "_";
        for (int i = 0; i < text.Length; i++)
        {
            if (i > 0)
            {
                field.text = field.text.Substring(0, field.text.Length - 1);
                field.text += text[i] + followingCharacter;
            }
            else if (i == 0)
            {
                field.text += text[i] + followingCharacter;
            }

            if (i == text.Length - 1)
            {
AudioManager.Instance.AudioController.AudioSource.loop = false;
                AudioManager.Instance.AudioController.AudioSource.Stop();
                field.text = field.text.Substring(0, field.text.Length - 1);
            }
            yield return new WaitForSeconds(speed);
        }
    }
}
