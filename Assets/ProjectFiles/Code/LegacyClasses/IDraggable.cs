using UnityEngine.EventSystems;

namespace PointnClick
{
    public interface IDraggable
    {
        public void OnMouseDown();

        public void OnMouseDrag();

        public void OnMouseUp();
    }
}
