using ScriptableObjectArchitecture;
using UnityEngine;

namespace PointnClick
{
    [CreateAssetMenu(
        fileName = "ToolDataGameEvent",
        menuName = SOArchitecture_Utility.GAME_EVENT + "ToolData",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_EVENTS + 3)]
    public class ToolDataGameEvent : GameEventBase<ToolData>
    {
    }
}
