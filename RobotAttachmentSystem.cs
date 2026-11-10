using UnityEngine;
using Valve.VR.InteractionSystem;

public class RobotAttachmentSystem : MonoBehaviour
{
    public RobotMountPoint[] mountPoints;

    // Проверяем, можно ли установить деталь.
    public void CheckForAutoAttach(RobotPart part)
    {
        if (part == null || part.IsAttached)
            return;

        Interactable interactable = part.GetComponent<Interactable>();

        if (interactable == null || interactable.attachedToHand == null)
            return;

        foreach (RobotMountPoint point in mountPoints)
        {
            if (point == null || !point.CanAccept(part))
                continue;

            float distance = Vector3.Distance(
                part.transform.position,
                point.transform.position
            );

            if (distance <= point.attachDistance)
            {
                TryAttach(part, point);
                return;
            }
        }
    }

    // Устанавливаем деталь.
    private void TryAttach(RobotPart part, RobotMountPoint point)
    {
        Interactable interactable = part.GetComponent<Interactable>();

        // Освобождаем деталь из руки.
        interactable.attachedToHand.DetachObject(part.gameObject, false);

        // Регистрируем установленную деталь.
        if (!point.TryReserve(part))
            return;

        // Отключаем VR-взаимодействие.
        interactable.enabled = false;

        Throwable throwable = part.GetComponent<Throwable>();

        if (throwable != null)
            throwable.enabled = false;

        // Отключаем физические столкновения.
        Rigidbody rb = part.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.detectCollisions = false;
        }

        // Отключаем все коллайдеры детали.
        Collider[] colliders = part.GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
            col.enabled = false;

        // Прикрепляем деталь к роботу.
        part.transform.SetParent(point.transform);

        part.transform.localPosition = Vector3.zero;
        part.transform.localRotation = Quaternion.identity;
    }
}