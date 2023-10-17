using UnityEngine;

namespace PointnClick
{
    public enum OperationType
    {
        A,
        B,
        C,
        D,
        E,
        F
    };

    [CreateAssetMenu(fileName = "ToolName", menuName = "Data/ToolData")]
    public class ToolData : ScriptableObject
    {
        [SerializeField] private OperationType m_operationType;

        [SerializeField] private Sprite m_toolSprite;

        public OperationType GetOperationType { get { return m_operationType; } }
        public Sprite ToolSprite { get { return m_toolSprite; } }
    }
}
