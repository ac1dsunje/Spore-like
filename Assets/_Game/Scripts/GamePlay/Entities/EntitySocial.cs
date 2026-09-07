using System;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntitySocial : IInitializable, IDisposable
{
    [Inject] private VisionHitbox _visionHitbox;
    [Inject] private SocialModule _social; 

    public void Initialize()
    {
        _visionHitbox.OnSocialEntityEntered += EnterEntity;
        _visionHitbox.OnSocialEntityLeft += ExitEntity;
    }

    private void EnterEntity(ISocial entity)
    {
        _social.EnterEntity(entity);
    }

    private void ExitEntity(ISocial entity)
    {
        _social.ExitEntity(entity);
    }

    public void Dispose()
    {
        _visionHitbox.OnSocialEntityEntered -= EnterEntity;
        _visionHitbox.OnSocialEntityLeft -= ExitEntity;
    }
}
}