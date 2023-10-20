using UnityEngine;

namespace PointnClick
{
    /// <summary>
    /// A surgery type.
    /// </summary>
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
        [SerializeField] private OperationType[] m_operationsTypes;

        [SerializeField] private Sprite m_toolSprite;

        public OperationType[] OperationsTypes { get { return m_operationsTypes; } }
        public Sprite ToolSprite { get { return m_toolSprite; } }
    }
}
