using System.Collections;
using UnityEngine;

namespace ProtoTD
{
    public interface ICoroutineHandler
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
    }
}
