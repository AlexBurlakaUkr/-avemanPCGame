using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EggsCounterText : MonoBehaviour
{
    private int count = 0;
    void Start()
    {
        GlobalEventManager.OnEggCounter.AddListener(EggCount);
    }
    private void EggCount()
    {
        count++;
        GetComponent<TMP_Text>().text = $":{count}";
    }
}
