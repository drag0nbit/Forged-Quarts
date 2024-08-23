using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public int team = 0;

    public bool Compare(int t)
    {
        return team == t;
    }

    public void Set(int t)
    {
        team = t;
    }
}