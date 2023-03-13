using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject go;
    public Text txt;
    public float duration;
    public float lastShow;
    public Vector3 motion;

    public void Show()
    {
        active = true;
        lastShow = Time.time;
        go.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateFloatingtext()
    {
        if (!active)
            return;

        if (Time.time - lastShow > duration)
            Hide();

        go.transform.position += motion * Time.deltaTime;

    }
}
