using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchCanvas : MonoBehaviour
{
    public GameObject CanvasOff;
    public GameObject CanvasOn;
    public GameObject FirstObjectSelected;

    public void ChangeCanvas()
    {
        CanvasOff.SetActive(true);
        CanvasOn.SetActive(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(FirstObjectSelected, null);
    }
}
