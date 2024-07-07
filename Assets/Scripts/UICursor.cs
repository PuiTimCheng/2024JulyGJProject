using UnityEngine;
using UnityEngine.UI;

public class UICursor : MonoBehaviour
{
    public Sprite catchCursor;
    public Sprite releaseCursor;
    public Image _image;
    private RectTransform _uiCursor;

    private void Awake()
    {
        _uiCursor = GetComponent<RectTransform>();
        _image.sprite = releaseCursor;

        Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _uiCursor.position = cursorPos;

        if (Input.GetMouseButtonDown(0))
        {
            _image.sprite = catchCursor;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _image.sprite = releaseCursor;
        }
    }
}