using EasyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string test;
    [FMODUnity.EventRef]
    [SerializeField] private List<string> songsByNumber = new List<string>();
    [SerializeField] private int toPlayNumber;
    private int currentMusic = 0;
    private List<FMOD.Studio.EventInstance> musicList = new List<FMOD.Studio.EventInstance>();

    [Button]
    private void CreateMusic()
    {
        for (int i = 0; i < songsByNumber.Count; i++)
        {
            musicList.Add(FMODUnity.RuntimeManager.CreateInstance(songsByNumber[i]));
        }

        musicList[currentMusic].start();
        musicList[currentMusic].setParameterByName("FADE music", 1f);
    }

    [Button]
    private void BattleValue()
    {
        musicList[currentMusic].setParameterByName("BATTLE", 100f);
    }

    [Button]
    private void NonBattleValue()
    {
        musicList[currentMusic].setParameterByName("BATTLE", 0f);
    }

    [Button]
    private void PlaySong()
    {
        musicList[currentMusic].setParameterByName("FADE music", 0f);
        musicList[toPlayNumber].start();
        musicList[toPlayNumber].setParameterByName("FADE music", 1f);

        currentMusic = toPlayNumber;
    }
}
