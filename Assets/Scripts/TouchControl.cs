using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public MoveDirection direction = MoveDirection.None;
    public bool check = false;
    private Vector2 downPos = Vector2.zero;

    public void OnDrag(PointerEventData eventData) {
        if(check) {
            if(eventData.position.x - downPos.x >= 100) {
                direction = MoveDirection.Right;
                check = false;
            }
            else if(eventData.position.x - downPos.x <= -100) {
                direction = MoveDirection.Left;
                check = false;
            }
            else if(eventData.position.y - downPos.y >= 100) {
                direction = MoveDirection.Up;
                check = false;
            }
            else if(eventData.position.y - downPos.y <= -100) {
                direction = MoveDirection.Down;
                check = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        check = true;
        downPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData) {
        downPos = Vector2.zero;
        direction = MoveDirection.None;
        check = false;
    }
}
