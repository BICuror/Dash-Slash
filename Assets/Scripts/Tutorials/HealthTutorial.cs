using UnityEngine;
using UnityEngine.Events;

public sealed class HealthTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _abilityJoystick;

    [SerializeField] private GameObject _healthUI;

    [SerializeField] private GameObject _hitTutorialLaser;

    [SerializeField] private GameObject _healthInfoText;

    [SerializeField] private GameObject _hitInfoText;

    [SerializeField] private GameObject _movmentTutorialCircle;

    private int _currentTutorialState;

    public UnityEvent TutorialEnded;

    public void StartTutorial()
    {
        _healthUI.SetActive(true);

        _abilityJoystick.SetActive(false);

        _hitInfoText.SetActive(true);

        IterateTutorial();
    }

    private void IterateTutorial()
    {
        switch(_currentTutorialState)
        {
            case 0: 
            {
                CreateTutorialCircle(-Main.roomSettings.GetWidth() + 2f, 0f);
                Instantiate(_hitTutorialLaser, Vector3.zero, Quaternion.identity);
            } break;
            case 1: 
            {
                CreateTutorialCircle(Main.roomSettings.GetWidth() - 2f, 0f);
                _healthInfoText.SetActive(true);
                _hitInfoText.SetActive(false);
                Instantiate(_hitTutorialLaser, Vector3.zero, Quaternion.identity);
            } break;
            case 2: EndTutorial(); break;
        }
        _currentTutorialState++;
    }

    private void EndTutorial()
    {
        TutorialEnded.Invoke();

        _healthUI.SetActive(false);

        Destroy(gameObject);
    }

    private void CreateTutorialCircle(float x, float y)
    {
        GameObject circle = Instantiate(_movmentTutorialCircle.gameObject, new Vector3(x, y, 0f), Quaternion.identity);

        circle.GetComponent<MovmentTutorialTrigger>().PlayerEntered.AddListener(IterateTutorial);
    } 
}
