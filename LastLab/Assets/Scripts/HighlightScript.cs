using UnityEngine;
using System.Collections.Generic;

public class HighlightScript : MonoBehaviour
{
    [SerializeField] private List<Renderer> renderers; // рендереры объекта
    [SerializeField] private Color highlightColor = Color.white;   // цвет выделения
    [SerializeField] private Color collisionColor = Color.red;     // цвет пересечения
    private List<Material> materials;

    void Awake()
    {
        materials = new List<Material>();
        foreach (var renderer in renderers)
            materials.AddRange(new List<Material>(renderer.materials));
    }

    private void OnMouseEnter() => ToggleHighlight(true);
    private void OnMouseExit() => ToggleHighlight(false);



    private int overlapCount = 0;  // сколько объектов сейчас пересекается с этим
    private bool isSelected = false;

    public void ToggleHighlight(bool val)
    {
        isSelected = val;
        updateColor();
    }

    private void updateColor()
    {
        Color target;
        if (overlapCount > 0)
            target = collisionColor;
        else if (isSelected)
            target = highlightColor;
        else
        {
            // Выключаем emission полностью
            foreach (var material in materials)
                material.DisableKeyword("_EMISSION");
            return;
        }

        foreach (var material in materials)
        {
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Реагируем только на объекты сцены, игнорируем землю
        if (other.GetComponent<ObjectDescription>() == null) return;
        overlapCount++;
        updateColor();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ObjectDescription>() == null) return;
        overlapCount--;
        if (overlapCount < 0) overlapCount = 0;
        updateColor();
    }
}