using UnityEngine;

public class RobotMountPoint : MonoBehaviour
{
    [Header("Настройки точки крепления")]

    public RobotPartType acceptedPartType = RobotPartType.Body;

    [Tooltip("Расстояние, на котором деталь автоматически крепится")]
    public float attachDistance = 0.15f;

    [SerializeField]
    private RobotPart installedPart;

    public bool IsOccupied => installedPart != null;

    public RobotPart InstalledPart => installedPart;

    public bool CanAccept(RobotPart part)
    {
        if (part == null || IsOccupied || part.IsAttached)
            return false;

        return part.partType == acceptedPartType;
    }

    public bool TryReserve(RobotPart part)
    {
        if (!CanAccept(part))
            return false;

        installedPart = part;
        part.currentMountPoint = this;

        return true;
    }

    public void ReleasePart()
    {
        if (installedPart == null)
            return;

        installedPart.currentMountPoint = null;
        installedPart = null;
    }
}