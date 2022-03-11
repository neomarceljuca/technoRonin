using UnityEngine;
using UnityEngine.EventSystems;
public class OnPointerDownJump : MonoBehaviour, IPointerDownHandler// required interface when using the OnPointerDown method.
{
    public Player player;

    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerDown(PointerEventData eventData)
    {
        player.Jump();
    }
}