using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class HandleController : MonoBehaviour, IPointerDownHandler
{
    [Header("Rotation Settings")]
    public float pullAngle = -60f;     // Pull Towards Down 
    public float pullDuration = 0.15f;
    public float returnDuration = 0.2f;

    [Header("Reference")]
    public SlotManager slotManager;

    private Quaternion startRotation; // Store The original rotation of Handle
    private bool isBusy = false;

    void Start()
    {
        startRotation = transform.localRotation;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isBusy) return;
        StartCoroutine(PullHandle());
    }

    IEnumerator PullHandle()
    {
        isBusy = true;

        // Pull Down (Rotate)
        yield return RotateHandle(startRotation,
            Quaternion.Euler(pullAngle,0,0),
            pullDuration);

        // Start Spin
        slotManager.OnSpinClicked();

        // Return Up
        yield return RotateHandle(transform.localRotation,
            startRotation,
            returnDuration);

        isBusy = false;
    }

    IEnumerator RotateHandle(Quaternion from, Quaternion to, float time)
    {
        float t = 0f;
        while (t < time)
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(from, to, t / time);
            yield return null;
        }
        transform.localRotation = to;
    }
}