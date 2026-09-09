using System;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntitySocial : IInitializable, IDisposable
{
    private readonly VisionHitbox _visionHitbox;
    private readonly SocialModule _social;

    public EntitySocial(VisionHitbox visionHitbox, SocialModule social)
    {
        _visionHitbox = visionHitbox;
        _social = social;
    }

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