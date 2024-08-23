using UnityEngine;

public class HeatManager : MonoBehaviour
{
    public float heat = 0f;
    public float maxHeat = 10f;
    public float minHeat = -10f;
    public bool allowOverheat = true;
    public bool overheated = false;
    public float overheatStartTime = 5f;
    public float overheatTime = 0f;

    void Update()
    {
        if (allowOverheat && overheated) overheatTime -= Time.deltaTime;
        if (overheatTime <= 0) overheated = false;
    }

    public void Heat(float h)
    {
        heat += h;
        Clamp();
        if (heat == maxHeat)
        {
            heat = maxHeat;
            if (allowOverheat)
            {
                overheated = true;
                overheatTime = overheatStartTime;
            }
        }
    }

    public void Set(float h)
    {
        heat = h;
        Clamp();
    }

    public void ForceOverheat()
    {
        overheated = true;
        overheatTime = overheatStartTime; 
    }

    public void ForceDeOverheat()
    {
        overheated = false;
        overheatTime = 0f; 
    }

    private void Clamp()
    {
        heat = Mathf.Clamp(heat, minHeat, maxHeat);
    }
}