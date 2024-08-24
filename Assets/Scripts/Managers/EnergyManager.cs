using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public float energy = 10f;
    public float maxEnergy = 10f;

    public void Charge(float e)
    {
        energy += e;
        Clamp();
    }

    public void Set(float e)
    {
        energy = e;
        Clamp();
    }

    private void Clamp()
    {
        energy = Mathf.Clamp(energy, 0, maxEnergy);
    }
}