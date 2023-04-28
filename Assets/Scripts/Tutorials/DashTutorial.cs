using UnityEngine;
using UnityEngine.Events;

public sealed class DashTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _lineTrap;

    [SerializeField] private GameObject _movmentTutorialCircle;

    [SerializeField] private GameObject _abilityJoysitck;

    [SerializeField] private GameObject _dashUI;

    [SerializeField] private GameObject _dashText;

    [SerializeField] private GameObject _dashChargesText;

    public UnityEvent TutorialEnded;

    private Vector3 _safeSpot;

    private int _currentTutorialState;

    private void TeleportPlayerToSafeSpot()
    {
        Main.playerTransform.position = _safeSpot;
    }    

    public void StartTutorial()
    {
        GameObject currentLineTrap = Instantiate(_lineTrap, Vector3.zero, Quaternion.identity, transform);
    
        currentLineTrap.GetComponent<MovmentTutorialTrigger>().PlayerEntered.AddListener(TeleportPlayerToSafeSpot);

        IterateTutorial();

        _abilityJoysitck.SetActive(true);

        _dashUI.SetActive(true);

        _dashText.SetActive(true);
    }

    private void IterateTutorial()
    {
        switch(_currentTutorialState)
        {
            case 0: 
            {
                CreateTutorialCircle(-Main.roomSettings.GetWidth() + 2f, 0f); 
                _safeSpot = new Vector3(Main.roomSettings.GetWidth() - 2f, 0f);   
                break;
            }
            case 1: 
            {
                CreateTutorialCircle(Main.roomSettings.GetWidth() - 2f, 0f); 
                _dashChargesText.SetActive(true);
                _dashText.SetActive(false);
                _safeSpot = new Vector3(-Main.roomSettings.GetWidth() + 2f, 0f);   
                break;
            }
            case 2: EndTutorial(); break;
        }

        _currentTutorialState++;
    }

    private void EndTutorial()
    {
        TutorialEnded.Invoke();

        _dashChargesText.SetActive(false);

        _dashUI.SetActive(false);

        Destroy(gameObject);
    }

    private void CreateTutorialCircle(float x, float y)
    {
        GameObject circle = Instantiate(_movmentTutorialCircle.gameObject, new Vector3(x, y, 0f), Quaternion.identity);

        circle.GetComponent<MovmentTutorialTrigger>().PlayerEntered.AddListener(IterateTutorial);
    } 
}
