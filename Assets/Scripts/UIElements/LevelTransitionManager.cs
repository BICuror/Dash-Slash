using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class LevelTransitionManager : MonoBehaviour
{
    [SerializeField] private Animator _transitionAnimator;

    private string _sceneNameToLoad;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(_sceneNameToLoad);

        Time.timeScale = 1f;
    }

    public void LoadScene(string sceneName)
    {
        _sceneNameToLoad = sceneName;

        gameObject.SetActive(true); 

        _transitionAnimator.Play("FadeIn");
    } 

    public void DisableManager() => gameObject.SetActive(false); 
}
