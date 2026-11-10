using UnityEngine;

public enum RobotPartType
{
    Head,
    Body
}

public class RobotPart : MonoBehaviour
{
    [Header("Информация о детали")]
    public RobotPartType partType = RobotPartType.Body;

    [Header("Система крепления")]
    public RobotAttachmentSystem attachmentSystem;

    [HideInInspector]
    public RobotMountPoint currentMountPoint;

    public bool IsAttached => currentMountPoint != null;

    private void Update()
    {
        if (IsAttached || attachmentSystem == null)
            return;

        attachmentSystem.CheckForAutoAttach(this);
    }
}