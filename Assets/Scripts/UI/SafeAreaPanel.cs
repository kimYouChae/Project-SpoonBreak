using UnityEngine;

/// <summary>
/// 화면의 안전 영역에 맞춰 UI 컨테이너를 배치한다.
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(RectTransform))]
public sealed class SafeAreaPanel : MonoBehaviour
{
    // 안전 영역을 적용할 UI 컨테이너.
    private RectTransform panel;
    private Rect lastSafeArea;
    private Vector2Int lastScreenSize;

    private void Awake()
    {
        panel = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        ApplySafeArea();
    }

    private void Update()
    {
        Vector2Int screenSize =
            new Vector2Int(Screen.width, Screen.height);

        if (Screen.safeArea != lastSafeArea ||
            screenSize != lastScreenSize)
        {
            ApplySafeArea();
        }
    }

    private void ApplySafeArea()
    {
        if (Screen.width <= 0 || Screen.height <= 0)
        {
            return;
        }

        Rect safeArea = Screen.safeArea;
        Vector2 screenSize =
            new Vector2(Screen.width, Screen.height);

        // 전체 화면 기준 좌표를 정규화된 앵커로 변환한다.
        panel.anchorMin = new Vector2(
            safeArea.xMin / screenSize.x,
            safeArea.yMin / screenSize.y);
        panel.anchorMax = new Vector2(
            safeArea.xMax / screenSize.x,
            safeArea.yMax / screenSize.y);
        panel.offsetMin = Vector2.zero;
        panel.offsetMax = Vector2.zero;

        lastSafeArea = safeArea;
        lastScreenSize =
            new Vector2Int(Screen.width, Screen.height);
    }
}
