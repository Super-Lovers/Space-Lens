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
        _audioController = GetComponent<AudioController>();
        if (_isDialoguePlaying)
        {
            StartCoroutine(PlayDialogue(_currentDialogueIndex, 2));
        } else
        {
            NextDialogue();
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
        for (int i = fromIndex; i <= toIncludingIndex; i++)
        {
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
            _fadingController.Fade("in", "MainScene");
        }
    }

    private void NextDialogue()
    {
        _currentDialogueIndex++;
        LoadText(_textField, _dialogue[_currentDialogueIndex].Sentence, plainTextLoadSpeed, _dialogue[_currentDialogueIndex].ClearScreen);
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
                field.text = field.text.Substring(0, field.text.Length - 1);
            }
            yield return new WaitForSecondsRealtime(speed);
        }

        if (field.text.Length == text.Length)
        {
            _audioController.AudioSource.loop = false;
            _audioController.AudioSource.Stop();
            _isItLoading = false;
        }
    }
}
