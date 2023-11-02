using UnityEngine;

namespace PointnClick
{
    /// <summary>
    /// A surgery type.
    /// </summary>
    public enum OperationType
    {
        Dierese,
        Hemostasia,
        Exerese,
        Sintese
    };

    [CreateAssetMenu(fileName = "ToolName", menuName = "Data/ToolData")]
    public class ToolData : ScriptableObject
    {
        [field: SerializeField] public string ToolName { get; private set; }
        [field: SerializeField] public OperationType[] Operations { get; private set; }
        [field: SerializeField] public Sprite ToolSprite { get; private set; }
    }
}