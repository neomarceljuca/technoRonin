using UnityEngine;
using UnityEngine.EventSystems;
public class OnPointerDownAttack : MonoBehaviour, IPointerDownHandler// required interface when using the OnPointerDown method.
{
    public Player player;
    public CooldownBar myCDBar;

    //Do this when the mouse is clicked over the selectable object this script is attached to.
    public void OnPointerDown(PointerEventData eventData)
    {
        player.Attack();
        myCDBar.resetCooldown();
    }
}