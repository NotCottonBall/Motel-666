using UnityEngine;

public class AnomalyAffectedObject : MonoBehaviour
{
  public enum ObjectType
  {
    Unknown = 0,
    TV,
    Sofa,
    Book,
    Light
  }

  [SerializeField] private ObjectType m_Type = ObjectType.Unknown;
}
