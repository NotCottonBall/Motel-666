using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour
{
  private Key[] m_QTERandomKeys = { Key.W, Key.A, Key.S, Key.D };

  [SerializeField] private TextMeshProUGUI m_KeyPressText;
  [SerializeField] private Slider m_TimerSlider;
  [SerializeField] private GameObject m_QTEUI;

  public static QTEManager Instance;
  void Awake()
  {
    Instance = this;
  }
  void Start()
  {
    ToggleQTEMenu(false);
  }

  // This is the QTE where a random selected key pops up and the player has to click it
  // in a certain period of time while not pressing a wrong key.
  public void QTERunKeyMashing(float timePerKey, int keysAmount, Action<bool> OnFinish)
  {
    PlayerController.Instance.TogglePlayerMovement(false);
    ToggleQTEMenu(true);
    StartCoroutine(QTERoutineKeyMashing(timePerKey, keysAmount, OnFinish));
  }

  IEnumerator QTERoutineKeyMashing(float timerPerKey, int keysAmount, Action<bool> OnFinish)
  {
    for (int i = 0; i < keysAmount; i++)
    {
      Key chosenKey = m_QTERandomKeys[UnityEngine.Random.Range(0, m_QTERandomKeys.Length)];
      m_KeyPressText.text = chosenKey.ToString();

      float timeLeft = timerPerKey;
      bool correctKey = false;

      while (timeLeft > 0 && !correctKey)
      {
        foreach (Key k in m_QTERandomKeys)
        {
          if (chosenKey == k && Keyboard.current[k].wasPressedThisFrame)
            correctKey = true;
          else if (chosenKey != k && Keyboard.current[k].wasPressedThisFrame)
          {
            ToggleQTEMenu(false);
            PlayerController.Instance.TogglePlayerMovement(true);
            OnFinish(false);
            yield break;
          }
        }

        timeLeft -= Time.deltaTime;
        m_TimerSlider.value = timeLeft / timerPerKey;

        yield return null;
      }

      if (!correctKey)
      {
        PlayerController.Instance.TogglePlayerMovement(true);
        OnFinish(false);
        yield break;
      }
    }
    ToggleQTEMenu(false);
    PlayerController.Instance.TogglePlayerMovement(true);
    OnFinish(true);
  }

  // Functions
  void ToggleQTEMenu(bool value)
  {
    if (value == false)
    {
      m_KeyPressText.text = "_";
      m_TimerSlider.value = 1;
      m_QTEUI.SetActive(false);
    }
    else
      m_QTEUI.SetActive(true);
  }
}
