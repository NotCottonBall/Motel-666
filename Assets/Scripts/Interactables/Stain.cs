using UnityEngine;

public enum StainType
{
  Unknown = 0,
  Regular,
  Water
}

public class Stain : MonoBehaviour
{
  [Header("Stain Properties")]
  [SerializeField] private StainType m_Type = StainType.Unknown;

  void Awake()
  {
    if (m_Type == StainType.Unknown)
    {
      Debug.LogError("Stain Type Was Not Set!!!");
    }
  }

  // SetGets
  public StainType GetStainType()
  {
    return m_Type;
  }
}
