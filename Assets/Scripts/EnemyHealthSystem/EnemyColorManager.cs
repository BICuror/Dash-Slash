using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EnemyColorManager : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _spriteRenderers;

    [SerializeField] private TrailRenderer[] _trailRenderers;

    [SerializeField] private ParticleSystem[] _particleSystems;

    public void SetMaterial(Material material)
    {
        foreach(SpriteRenderer renderer in _spriteRenderers)
        {
            renderer.material = material;
        }

        foreach(TrailRenderer renderer in _trailRenderers)
        {
            renderer.material = material;
        }

        foreach(ParticleSystem system in _particleSystems)
        {
            var particleSystemRenderer = system.GetComponent<Renderer>();

            particleSystemRenderer.material = material;
        }
    }

    public void SetColor(Color color)
    {
        foreach(SpriteRenderer renderer in _spriteRenderers)
        {
            renderer.color = color;
        }

       
        foreach(TrailRenderer renderer in _trailRenderers)
        {
            renderer.startColor = color;

            renderer.endColor = color;
        }

        foreach(ParticleSystem system in _particleSystems)
        {
            ParticleSystem.MainModule main = system.main;
        
            main.startColor = color;
        }
    }

    public void AddRenderer(SpriteRenderer renderer) => _spriteRenderers.Add(renderer);

    public void RemoveRenderer(SpriteRenderer renderer) => _spriteRenderers.Remove(renderer);
}
