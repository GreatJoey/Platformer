using UnityEngine;
using UnityEngine.UI;

public class SpeedDisplay : MonoBehaviour
{
    public TestMovement movement;
    public Text current_speed;

    void Start()
    {
        GameObject graphic = new GameObject("SpeedGraphic");
        Canvas canvas = graphic.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        graphic.AddComponent<CanvasScaler>();
        graphic.AddComponent<GraphicRaycaster>();

        GameObject text_object = new GameObject("SpeedText");
        text_object.transform.SetParent(graphic.transform);

        current_speed = text_object.AddComponent<Text>();
        current_speed.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        current_speed.fontSize = 24;
        current_speed.alignment = TextAnchor.UpperCenter;
        current_speed.color = Color.white;

        RectTransform rect = current_speed.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 1f);
        rect.anchorMax = new Vector2(0.5f, 1f);
        rect.pivot = new Vector2(0.5f, 1f);
        rect.anchoredPosition = new Vector2(0, -10);
        rect.sizeDelta = new Vector2(200, 50);
    }
    void Update()
    {
        if (movement != null)
        {

            current_speed.text = movement.speed.ToString("F1");
        }
    }
}
