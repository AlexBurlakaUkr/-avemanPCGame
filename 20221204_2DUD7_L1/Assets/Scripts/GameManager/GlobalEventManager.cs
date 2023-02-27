using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent OnEggCounter = new UnityEvent(); 
    public static UnityEvent OnPlayerDeath = new UnityEvent(); 
    public static void SendEggCount() => OnEggCounter.Invoke();
    public static void SendPlayerDeath() => OnPlayerDeath.Invoke();
}
