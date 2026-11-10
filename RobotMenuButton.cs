using UnityEngine;

public class RobotMenuButton : MonoBehaviour
{
    public RobotMountPoint[] mountPoints;
    public GameObject menu;

    // Вызывается при нажатии кнопки.
    public void CheckAndOpenMenu()
    {
        foreach (RobotMountPoint point in mountPoints)
        {
            if (point == null || !point.IsOccupied)
                return;
        }

        menu.SetActive(true);
    }
}

