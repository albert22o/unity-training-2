using System.Windows.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    [SerializeField] LayerMask ground;   // слой земли
    [SerializeField] LayerMask objects;  // слой объектов
    [SerializeField] ObjectPanelScript objectPanel;

    public GameObject selectedObject = null;
    bool drag = false;
    Vector2 mPos;
    bool cursorOnUI = false;

    Vector3 startingPosition;

    void updatePanel()
    {
        if (selectedObject != null)
            objectPanel.setObject(selectedObject.GetComponent<ObjectDescription>());
    }

    void clearPanel()
    {
        objectPanel.clearPanel();
    }

    GameObject castRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(mPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, objects))
            return hit.transform.gameObject;
        return null;
    }

    public void select(GameObject go)
    {
        if (selectedObject == go) return;
        if (selectedObject != null) deselect();

        selectedObject = go;
        selectedObject.GetComponent<HighlightScript>().ToggleHighlight(true);
        updatePanel();
        startingPosition = selectedObject.transform.position; // Лаб.3
    }

    public void deselect()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<HighlightScript>().ToggleHighlight(false);
            selectedObject = null;
        }
    }

    private void mouseDown()
    {
        GameObject go = castRay();
        if (go != null)
        {
            select(go);
            drag = true;
        }
        else
        {
            clearPanel();
            deselect();
        }
    }

    private void mouseUp()
    {
        drag = false;
        if (selectedObject != null)
        {
            ICommand command = new MoveCommand(selectedObject, startingPosition, selectedObject.transform.position);
            CommandInvoker.ExeqteCommand(command);
            startingPosition = selectedObject.transform.position;
        }
    }

    public void OnClick(InputValue button)
    {
        if (cursorOnUI) return;
        bool pressed = button.isPressed;
        if (pressed) mouseDown();
        else mouseUp();
    }

    public void OnPoint(InputValue mousePosition)
    {
        mPos = mousePosition.Get<Vector2>();
        if (drag == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(mPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, ground))
                selectedObject.transform.position = hit.point;
        }
    }

    private void Update()
    {
        cursorOnUI = EventSystem.current.IsPointerOverGameObject();
    }
}