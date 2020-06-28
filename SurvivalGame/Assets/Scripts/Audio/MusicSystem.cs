using Extensions.Generics.Singleton;
using System.Collections;
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
            musicList[i].start();
        }

        musicList[currentMusic].setParameterByName("FADE music", 1f);
    }

    public void SetBattleValue(bool inBattle = true)
    {
        musicList[currentMusic].setParameterByName("BATTLE", (inBattle) ? 100f : 0f);
    }

    public void NewEnvironment(Environment environment)
    {
        int toPlayNumber = (int)environment;
        if (toPlayNumber == currentMusic) return;


        StartCoroutine(MusicFade(currentMusic, 0, 1, 2));   //Fade out
        StartCoroutine(MusicFade(toPlayNumber, 1, 0, 1));   //Fade in

        currentMusic = toPlayNumber;
    }

    IEnumerator MusicFade(int index, float toValue, float fromValue, float time)
    {
        float elapsedTime = 0;
        float cValue = fromValue;

        while (elapsedTime < time)
        {
            cValue = Mathf.Lerp(fromValue, toValue, (elapsedTime / time));
            musicList[index].setParameterByName("FADE music", cValue);            
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public enum Environment
    {
        Menu = 0,
        Starting,
        Jungle,
        Snow
    }
}
