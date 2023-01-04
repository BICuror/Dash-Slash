using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "CircleOfFire", menuName = "ScriptableObjects/Perks/CircleOfFire")]

public sealed class CircleOfFire : PerkBasis
{
    [SerializeField] private GameObject _fireCirclePrefab;

    [Range(0f, 100f)] [SerializeField] private float _appearingChanse;

    public override void Obtain() => Main.enemyList.EnemyDied.AddListener(TrySpawnCircleOfFire);
    public override void Remove() => Main.enemyList.EnemyDied.RemoveListener(TrySpawnCircleOfFire);
    
    private void TrySpawnCircleOfFire(Vector3 position)
    {
        if (_appearingChanse > Random.Range(0, 100))
        {
            Instantiate(_fireCirclePrefab, position, Quaternion.identity);
        }
    }

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        textField.SetText("Oftenly spawns " + $"{"FIRE".AddColor(Main.colorManager.GetDefenceColor())}" + " zones when enemy dies");
    }
}
