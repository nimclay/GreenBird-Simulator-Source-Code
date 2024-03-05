using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{

    public ParticleSystem[] WeatherEffects;

    private int eventNum;
    private float startTime;
    private float Duration;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeather();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectWeather(){
        eventNum = Random.Range(0,WeatherEffects.Length);
        startTime = Random.Range(120,300);
        Duration = Random.Range(40,120);
        StartCoroutine(StartWeather());
    }
    public IEnumerator StartWeather(){
        Debug.Log(startTime);
        yield return new WaitForSeconds(startTime);
        WeatherEffects[eventNum].Play();
        yield return new WaitForSeconds(Duration);
        WeatherEffects[eventNum].Stop();
        SelectWeather();

    }
}
