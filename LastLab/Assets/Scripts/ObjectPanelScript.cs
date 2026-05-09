using System.Data.Common;
using System.Windows.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPanelScript : MonoBehaviour
{
    [SerializeField] TMP_InputField objectName;
    [SerializeField] Slider rotationSlider;  // слайдер, min=0, max=360
    ObjectDescription selectedObject;
    Vector3 rotationOnSelect;                // ПОЛНОЕ вращение в момент выбора объекта
    bool isAdjusting = false;                // флаг для предотвращения рекурсии


    private void Start()
    {
        rotationSlider.onValueChanged.AddListener(onRotationSliderChanged);
    }
    public void clearPanel()
    {
        objectName.text = "";
        selectedObject = null;
    }

    // Вызывается кнопкой "Обновить имя"
    public void updateName()
    {
        if (selectedObject != null)
        {
            ICommand command = new RenameCommand(selectedObject, selectedObject.objectName, objectName.text);
            CommandInvoker.ExeqteCommand(command);
        }
    }

    // Вызывается кнопкой "Удалить"
    public void deleteObject()
    {
        ICommand command = new DelCommand(selectedObject.gameObject);
        CommandInvoker.ExeqteCommand(command);
    }

    // Вызывается в setObject():
    public void setObject(ObjectDescription od)
    {
        selectedObject = od;
        objectName.text = selectedObject.objectName;

        // Сохраняем ПОЛНОЕ вращение
        rotationOnSelect = od.transform.eulerAngles;

        // Синхронизируем слайдер с текущим Y-углом объекта
        float currentY = rotationOnSelect.y;
        rotationSlider.SetValueWithoutNotify(currentY);
    }

    // Вызывается слайдером через onValueChanged — каждый кадр во время перетаскивания
    public void onRotationSliderChanged(float value)
    {
        if (selectedObject != null && !isAdjusting)
        {
            // Сохраняем исходное вращение по X и Z
            Vector3 currentRot = selectedObject.transform.eulerAngles;
            selectedObject.transform.eulerAngles = new Vector3(currentRot.x, value, currentRot.z);
        }
    }

    // Вызывается кнопкой "Применить вращение" (или событием onPointerUp слайдера)
    public void applyRotation()
    {
        if (selectedObject == null) return;

        Vector3 newRotation = selectedObject.transform.eulerAngles;

        // Создаем команду с ПОЛНЫМ вращением
        ICommand command = new RotateCommand(selectedObject, rotationOnSelect, newRotation);
        CommandInvoker.ExeqteCommand(command);

        rotationOnSelect = newRotation;  // обновляем базовую точку для следующей отмены
    }
}