using UnityEngine;

public enum AnomalyType
{
  Unknown = 0,
  SignsOfHarm,
  RoomActivity,
  OccupantBehaviour,
  BodyTransformation,
  UnidentifiedBeing
}

public class Anomaly : MonoBehaviour
{
  [SerializeField] private AnomalyType m_Type = AnomalyType.Unknown;
}
