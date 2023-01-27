using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "ExplodingCorpses", menuName = "ScriptableObjects/Perks/ExplodingCorpses")]

public sealed class ExplodingCorpsesPerk : PerkBasis
{
    [SerializeField] GameObject _missle;

    [SerializeField] private float _missleSpeed;
    
    [SerializeField] private float _missleDamage;

    public override void Obtain() => Main.enemyList.EnemyDied.AddListener(SpawnCorpse);
    public override void Remove() => Main.enemyList.EnemyDied.RemoveListener(SpawnCorpse);

    public void SpawnCorpse(Vector3 position)
    {
        float additionalAngle = Random.Range(0f, 360f);

        float angle = 360 / GetLevel(); 

        for (int i = 0; i < GetLevel(); i++)
        {
            GameObject currentBullet = Instantiate(_missle, position, Quaternion.identity);
        
            currentBullet.transform.rotation = Quaternion.Euler(0f, 0f, additionalAngle + angle * i);
                
            Bullet currentBulletScript = currentBullet.GetComponent<Bullet>();

            currentBulletScript.Setup(_missleDamage, _missleSpeed, Main.enemyList);
                
            currentBulletScript.SetupHoming(360f);

            currentBulletScript.SetupPenetrating();
        }
    }

    public override void SetDescription(TextMeshProUGUI textField) 
    {
        string descripion = "";

        switch (GetLevel())
        {
            case 1: descripion = "Corpses explode with 1 bullet dealing"; break;
            case 2: descripion = "Corpses explode with 2 bullet dealing"; break;
            case 3: descripion = "Corpses explode with 3 bullet dealing"; break; 
        }
        
        textField.SetText(
            descripion +
            $"{" AREA".AddColor(Main.colorManager.GetAreaColor())}" +
            " damage");

    }

    public override void SetUpgradeDescription(TextMeshProUGUI textField)
    {
        string descripion = "";

        switch (GetLevel())
        {
            case 1: descripion = "Corpses explode with 1 > 2 bullet dealing"; break;
            case 2: descripion = "Corpses explode with 2 > 3 bullet dealing"; break; 
        }

        textField.SetText(
            descripion +
            $"{" AREA".AddColor(Main.colorManager.GetAreaColor())}" +
            " damage");
    }
}
