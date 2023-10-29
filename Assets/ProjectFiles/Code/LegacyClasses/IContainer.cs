using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PointnClick
{
    public interface IContainer
    {
        public void AddTool(ToolController tool);

        public void RemoveTool(ToolController tool);

        public Vector2 GenerateCoordinates(Vector2 startPosition, Vector2 deltas, int rowsQuantity, int index);

        public void ResetToolsPosition();
    }
}
