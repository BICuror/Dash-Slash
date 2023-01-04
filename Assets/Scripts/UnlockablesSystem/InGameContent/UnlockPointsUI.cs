using UnityEngine;
using TMPro;

public sealed class UnlockPointsUI : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [SerializeField] private TextMeshProUGUI _pointsField;

    [SerializeField] private UnlockPointsCounter _pointCounter;

    private void Awake()
    {
        _pointCounter.PointsUpdated.AddListener(StartAnimation);

        UpdateCount();

        gameObject.SetActive(false);
    }   

    private void StartAnimation()
    {
        _anim.gameObject.SetActive(true);

        _anim.Play("UpdateUnlockPointCounter");
    } 

    public void UpdateCount()
    {
        _pointsField.text = _pointCounter.GetPoints().ToString();
    }
}
