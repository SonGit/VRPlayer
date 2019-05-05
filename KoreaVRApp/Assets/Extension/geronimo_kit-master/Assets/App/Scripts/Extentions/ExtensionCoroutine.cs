using System;
using UnityEngine;
using System.Collections;

public class ExtensionCoroutine : Singleton<ExtensionCoroutine>
{
    Coroutine lastRoutine = null;

    public IEnumerator StartExtendedCoroutine(IEnumerator enumerator)
    {
        yield return StartCoroutine(enumerator);
    }

    public IEnumerator StartExtendedCoroutine(IEnumerator enumerator, Action cb)
    {
        yield return StartCoroutine(enumerator);

        if (cb != null)
        {
            cb();
        }
    }

    public void StartExtendedCoroutineNoWait(IEnumerator enumerator)
    {
        lastRoutine = StartCoroutine(enumerator);
    }

    public void StartExtendedCoroutineNoWait(IEnumerator enumerator, Action cb)
    {
        lastRoutine = StartCoroutine(enumerator);
    }

    public void StopExtendedCoroutine()
    {
        if (lastRoutine != null)
            StopCoroutine(lastRoutine);
    }
}