using UnityEngine.UI;
using UnityEngine;
using TMPro;

public sealed class DefeatMenu : MonoBehaviour
{
    [SerializeField] private GameObject _defeatPanelObject;


    [Header("StatisticsTextFields")]
    [SerializeField] private TextMeshProUGUI _wavesTextField;
    [SerializeField] private TextMeshProUGUI _killedEnemiesTextField;
    [SerializeField] private TextMeshProUGUI _droneTextField;
    [SerializeField] private TextMeshProUGUI _perksTextField;


    [Header("KillerInfoLinks")]

    [SerializeField] private Image _killerIcon;
    [SerializeField] private TextMeshProUGUI _killerNameTextField;
    [SerializeField] private TextMeshProUGUI _killerDescriptionTextField;



    public void OpenDefeatPanel(EnemyProfile profile)
    {
        _defeatPanelObject.SetActive(true);

        Main.cameraEffects.SetActiveAdditionalEffects(false);

        Time.timeScale = 0f;

        Main.timeManager.DefaultTimeScale = 0f;

        SetStatistic();
        SetKillerInformation(profile);
    }

    private void SetKillerInformation(EnemyProfile profile)
    {
        _killerIcon.sprite = profile.Icon;
        _killerDescriptionTextField.text = profile.Description;
        _killerNameTextField.text = profile.Name;
    }

    private void SetStatistic()
    {
        _wavesTextField.text = Main.arenaManager.GetCurrentWave().ToString();

        _killedEnemiesTextField.text = Main.enemyList.GetAmountOfKilledEnemies().ToString();

        _droneTextField.text = Main.droneContainer.GetAmountOfDrones().ToString();

        _perksTextField.text = Main.perkInventory.GetPerksAmount().ToString();
    }
}
