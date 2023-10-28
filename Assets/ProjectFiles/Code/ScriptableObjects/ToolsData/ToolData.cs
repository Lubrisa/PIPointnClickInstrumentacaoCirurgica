using UnityEngine;

namespace PointnClick
{
    /// <summary>
    /// A surgery type.
    /// </summary>
    public enum OperationType
    {
        Geral,
        Obstetricia,
        Otorrinolaringologia,
        Plastica,
        Toracica,
        Urologia
    };

    [CreateAssetMenu(fileName = "ToolName", menuName = "Data/ToolData")]
    public class ToolData : ScriptableObject
    {
        [field: SerializeField] public string ToolName { get; private set; }
        [field: SerializeField] public OperationData[] OperationsData { get; private set; }
        [field: SerializeField] public Sprite ToolSprite { get; private set; }
    }

    [System.Serializable]
    public class OperationData
    {
        [field: SerializeField] public OperationType OperationType { get; private set; }
        [field: SerializeField] public int[] OperationsDifficulties { get; private set; }
    }
}
