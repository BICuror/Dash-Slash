using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaterialFactory : MonoBehaviour
{
    [SerializeField] private Material dronePanelMaterial;

    [SerializeField] private Material transmutationPanelMaterial;

    [SerializeField] private Material reversedTransmutationPanelMaterial;

    [SerializeField] private Material[] particleMateral;

    public enum ColorType
    {
        None,
        Attack,
        Repair,
        Area
    }

    private Material[] defaultShineMaterials;

    public Material GetPanelMaterial(DroneType type, int panelType)
    {
        Material panelMaterial;

        if (panelType == 0) panelMaterial = dronePanelMaterial;
        else if (panelType == 1) panelMaterial = reversedTransmutationPanelMaterial;
        else panelMaterial = transmutationPanelMaterial;

        Material newMaterial = new Material(panelMaterial);

        Color mainColor = new Color();

        switch(type)
        {
            case DroneType.Single: mainColor = Main.colorManager.GetSingleColor(); break;
            case DroneType.Defence: mainColor = Main.colorManager.GetDefenceColor(); break;
            case DroneType.Area: mainColor = Main.colorManager.GetAreaColor(); break;
        }

        Color additionalColor = CalculateAdditionalColor(mainColor);

        newMaterial.SetColor("MainColor", mainColor);
        newMaterial.SetColor("AdditiveColor", additionalColor);
        if (panelType == 0) newMaterial.SetFloat("MainOffset", Random.Range(0f, 10f));

        return newMaterial;
    }

    private Color CalculateAdditionalColor(Color mainColor)
    {
        Color additionalColor = new Color();

        additionalColor = mainColor;

        return additionalColor;
    }

    public Material GetParticleMaterial(DroneType droneType)
    {
        return particleMateral[(int)droneType];
    }
}
