using UnityEngine;

public class ParticleOnDestroy : MonoBehaviour
{
    public GameObject death_object;
    public bool enableLifetime = true;

    private void OnDestroy()
    {
        if (death_object != null)
        {
            GameObject obj = Instantiate(death_object, transform.position, Quaternion.identity);
            ParticleSystem particles = obj.GetComponent<ParticleSystem>();
            var main = particles.main;
            main.playOnAwake = true;
            particles.Play();
            if (enableLifetime) obj.GetComponent<Lifetime>()?.Activate();
        }
    }
}
