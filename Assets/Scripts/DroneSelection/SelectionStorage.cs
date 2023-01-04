using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionStorage : MonoBehaviour
{
    [SerializeField] private List<PerkData> _allPerks;
    [SerializeField] private List<PerkData> _perksToUpgrade = new List<PerkData>();
    [SerializeField] private List<DroneBasis> _allDrones;

    private void Awake() 
    {
        for (int i = 0; i < _allPerks.Count; i++) 
        {
            _allPerks[i].Perk.SetToDefault(); 
        }
    } 

    public void AddDrone(DroneBasis drone) => _allDrones.Add(drone);

    public List<DroneBasis> GetAllDrones()
    {
        List<DroneBasis> newList = new List<DroneBasis>(_allDrones);

        return newList;
    }

    public PerkData GetRandomPerk()
    {
        PerkData randomPerk = _allPerks[Random.Range(0, _allPerks.Count)];

        _allPerks.Remove(randomPerk);

        return randomPerk;
    }

    public List<PerkData> GetAllPerks()
    {
        List<PerkData> allPerks = new List<PerkData>(_allPerks);
    
        return allPerks;
    }
    
    public void ReturnPerk(PerkData perkData) => _allPerks.Add(perkData);

    public PerkData GetRandomUpgradablePerk()
    {
        PerkData perk = _perksToUpgrade[Random.Range(0, _perksToUpgrade.Count)];

        _perksToUpgrade.Remove(perk);

        return perk;
    }

    public void ReturnUpgradablePerk(PerkData perkData) => _perksToUpgrade.Add(perkData);

    public bool HasUpgradablePerks() => _perksToUpgrade.Count > 0; 
}
