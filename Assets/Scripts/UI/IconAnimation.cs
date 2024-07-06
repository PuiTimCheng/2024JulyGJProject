using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace UI
{
    public class IconAnimation : MonoBehaviour
    {
        public MMF_Player audioEffect;
        public GameObject singleIcon;
        public RectTransform endPosition;

        public Vector3 controlPointOffsetMin;
        public Vector3 controlPointOffsetMax;
        public RectTransform canvas;

        public float minSpeed;
        public float maxSpeed;

        public Camera cam;

        public void StartIconAnimation(int quantity, Vector3 worldPosition)
        {
            audioEffect.PlayFeedbacks();
            // 将世界坐标转换为屏幕坐标
            Vector2 screenPosition = cam.WorldToScreenPoint(worldPosition);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas, screenPosition, cam, out var canvasPosition);

            var iconRectTransform = endPosition; // Icon 的 RectTransform。
            var starRectTransform = iconRectTransform.parent as RectTransform; // StarUI 的 RectTransform。
            var canvasRectTransform = canvas; // Canvas 的 RectTransform。

            var iconWorldPosition = starRectTransform.TransformPoint(iconRectTransform.anchoredPosition);

            var iconScreenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, iconWorldPosition);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, iconScreenPosition,
                Camera.main, out var iconCanvasPosition);

            for (var i = 0; i < quantity; i++)
            {
                var icon = Instantiate(singleIcon, canvasPosition, Quaternion.identity, canvas);
                icon.GetComponent<RectTransform>().position = canvasPosition;
                StartCoroutine(MoveIcon(icon, canvasPosition, iconCanvasPosition));
            }
        }


        private IEnumerator MoveIcon(GameObject icon, Vector3 start, Vector3 end)
        {
            // Randomize duration for each icon
            var duration = Random.Range(minSpeed, maxSpeed); // Adjust the range as needed
            float elapsed = 0;

            // Randomize control points for each icon
            var controlPoint1 = start + Vector3.Lerp(controlPointOffsetMin, controlPointOffsetMax, Random.value);
            var controlPoint2 = end + Vector3.Lerp(controlPointOffsetMin, controlPointOffsetMax, Random.value);

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float normalizedTime = elapsed / duration;

                // Apply non-linear time interpolation for speed variation
                float adjustedTime = AdjustedTime(normalizedTime);

                Vector3 position = CalculateBezierPoint(adjustedTime, start, controlPoint1, controlPoint2, end);
                icon.transform.localPosition = position;

                yield return null;
            }

            Destroy(icon); 
        }

        float AdjustedTime(float t)
        {
            return t * t * (3f - 2f * t);
        }

        // Method to calculate a point on a Bezier curve
        Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            var u = 1 - t;
            var tt = t * t;
            var uu = u * u;
            var uuu = uu * u;
            var ttt = tt * t;

            Vector3 p = uuu * p0; // First term
            p += 3 * uu * t * p1; // Second term
            p += 3 * u * tt * p2; // Third term
            p += ttt * p3; // Fourth term

            return p;
        }
    }
}