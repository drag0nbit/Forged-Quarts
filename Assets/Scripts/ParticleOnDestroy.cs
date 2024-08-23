using UnityEngine;

public class ParticleOnDestroy : MonoBehaviour
{
    public ParticleSystem death_particle;

    private void OnDestroy()
    {
        if (death_particle != null)
        {
            ParticleSystem particleInstance = Instantiate(death_particle, transform.position, Quaternion.identity);
            var main = particleInstance.main;
            main.playOnAwake = true;
            particleInstance.Play();
        }
    }
}
