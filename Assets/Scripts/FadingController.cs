using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadingController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Fade(string direction, string newScene)
    {
        _animator.SetBool("CanFade", true);
        if (direction == "In" || direction == "in")
        {
            _animator.SetBool("FadeIn", true);
        } else if (direction == "out" || direction == "out")
        {
            _animator.SetBool("FadeIn", false);
        }

        StartCoroutine(ChangeScene(newScene));
    }

    private IEnumerator ChangeScene(string newScene)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(newScene);
    }
}
