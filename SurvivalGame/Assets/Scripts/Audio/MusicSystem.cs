using Extensions.Generics.Singleton;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : GenericSingleton<MusicSystem, MusicSystem>
{
    [FMODUnity.EventRef]
    [SerializeField] private List<string> songsByNumber = new List<string>();
    private int currentMusic = 0;
    private List<FMOD.Studio.EventInstance> musicList = new List<FMOD.Studio.EventInstance>();

    protected override void Awake()
    {
        for (int i = 0; i < songsByNumber.Count; i++)
        {
            musicList.Add(FMODUnity.RuntimeManager.CreateInstance(songsByNumber[i]));
        }

        musicList[currentMusic].start();
        musicList[currentMusic].setParameterByName("FADE music", 1f);
    }

    public void SetBattleValue(bool inBattle = true)
    {
        musicList[currentMusic].setParameterByName("BATTLE", (inBattle) ? 100f : 0f);
    }

    public void NewEnvironment(Environment environment)
    {
        int toPlayNumber = (int)environment;

        musicList[currentMusic].setParameterByName("FADE music", 0f);
        musicList[toPlayNumber].start();
        musicList[toPlayNumber].setParameterByName("FADE music", 1f);

        currentMusic = toPlayNumber;
    }

    public enum Environment
    {
        Menu = 0,
        Starting,
        Jungle,
        Snow
    }
}
