using System;
using UnityEngine;

public class EnvironmentZone : MonoBehaviour
{
    [SerializeField] private MusicSystem.Environment environmentSong;
    [Space()]
    [SerializeField] private Color fogColor;
    [SerializeField] private float fogDensity;
    [Space()]
    [SerializeField] private Color lightColor;
    [SerializeField] private float lightIntensity;


    [Header("References: ")]
    [SerializeField] private Light directionalLight; 

    private Action<MusicSystem.Environment> OnEnterZone;

    private void Awake()
    {
        MusicSystem ms = MusicSystem.Instance;

        OnEnterZone += ms.NewEnvironment;
        OnEnterZone += x =>
        {
            directionalLight.color = lightColor;
            directionalLight.intensity = lightIntensity;
        };
        OnEnterZone += x =>
        {
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogDensity;
        };
    }

    private void OnDestroy()
    {
        OnEnterZone = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        Debug.Log("Entered zone: " + environmentSong);
        OnEnterZone?.Invoke(environmentSong);
    }
}
