using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class QTEManager : MonoBehaviour
{
  private Key[] m_QTERandomKeys = { Key.W, Key.A, Key.S, Key.D };

  public void QTERun(float timePerkey, int keysAmount, Action<bool> OnFinish)
  {
    StartCoroutine(QTERoutine(timePerkey, keysAmount, OnFinish));
  }

  IEnumerator QTERoutine(float timerPerKey, int keysAmount, Action<bool> OnFinish)
  {
    for (int i = 0; i < keysAmount; i++)
    {
      Key chosenKey = m_QTERandomKeys[UnityEngine.Random.Range(0, m_QTERandomKeys.Length)];
      float timeLeft = timerPerKey;
      bool correctKey = false;

      while (timeLeft > 0 && !correctKey)
      {
        correctKey = Keyboard.current[chosenKey].wasPressedThisFrame;

        timeLeft -= Time.deltaTime;
        yield return null;
      }

      if (!correctKey)
      {
        OnFinish(false);
        yield break;
      }
    }
    OnFinish(false);
  }
}
