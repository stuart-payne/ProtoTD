using System.Collections;
using UnityEngine;

public interface ICoroutineHandler
{
    Coroutine StartCoroutine(IEnumerator coroutine);
    void StopCoroutine(Coroutine coroutine);
}
