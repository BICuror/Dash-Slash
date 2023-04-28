using UnityEngine;
using UnityEngine.Events;

public sealed class MovmentTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _movmentText;

    [SerializeField] private GameObject _movmentTutorialCircle;

    public UnityEvent TutorialEnded;

    private int _currentTutorialState;

    private void Start()
    {
        _movmentText.SetActive(true);

        IterateTutorial();
    }
    
    private void IterateTutorial()
    {
        switch(_currentTutorialState)
        {
            case 0: CreateTutorialCircle(Main.roomSettings.GetWidth() - 2f, 0f); break;
            case 1: CreateTutorialCircle(0f, -Main.roomSettings.GetHeight() + 2f); break;
            case 2: CreateTutorialCircle(-Main.roomSettings.GetWidth() + 2f, 0f); break;
            case 3: CreateTutorialCircle(0f, Main.roomSettings.GetHeight() - 2f); break;
            case 4: CreateTutorialCircle(Main.roomSettings.GetWidth() - 2f, 0f); break;
            case 5: EndTutorial(); break; 
        }

        _currentTutorialState++;
    }

    private void EndTutorial()
    {
        TutorialEnded.Invoke();

        _movmentText.SetActive(false);

        Destroy(gameObject);
    }

    private void CreateTutorialCircle(float x, float y)
    {
        GameObject circle = Instantiate(_movmentTutorialCircle.gameObject, new Vector3(x, y, 0f), Quaternion.identity);

        circle.GetComponent<MovmentTutorialTrigger>().PlayerEntered.AddListener(IterateTutorial);
    } 
}
