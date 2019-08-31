using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadingController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name == "Ending")
        {
            Fade("out", string.Empty);
        }
    }

    public void Fade(string direction, string newScene)
    {
        if (_animator != null)
        {
            _animator.SetBool("CanFade", true);
            if (direction == "In" || direction == "in")
            {
                _animator.SetBool("FadeIn", true);
            }
            else if (direction == "out" || direction == "Out")
            {
                _animator.SetBool("FadeIn", false);
            }

            if (newScene != string.Empty)
            {
                StartCoroutine(ChangeScene(newScene));
            }

            Invoke("ResetAnimator", 1f);
        }
    }

    public void ResetAnimator()
    {
        _animator.SetBool("CanFade", false);
    }

    private IEnumerator ChangeScene(string newScene)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(newScene);
    }
}
