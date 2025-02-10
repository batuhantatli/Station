using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Extension 
{
    public static T GetRandomElementEnd<T>(this IEnumerable<T> enumerable, int endIndex)
    {
        return enumerable.ElementAt(Random.Range(0, endIndex));
    }
    
    public static void CallWithDelay(this MonoBehaviour mono, Action method, float delay)
    {
        mono.StartCoroutine(CallWithDelayIE(method, delay));
    }
    private static IEnumerator CallWithDelayIE(Action method, float delay)
    {
        yield return new WaitForSeconds(delay);
        method();
    }
}
