using System;
using System.Linq;
using ModestTree;

namespace Zenject
{
  public class SignalSubscription : IDisposable, IPoolable<Action<object>, SignalDeclaration>
  {
    public class Pool : PoolableMemoryPool<Action<object>, SignalDeclaration, SignalSubscription>
    {
    }

    private readonly Pool _pool;

    public BindingId SignalId { get; private set; }

    private Action<object> _callback;
    private SignalDeclaration _declaration;

    public SignalSubscription(Pool pool)
    {
      _pool = pool;

      SetDefaults();
    }

    public void Dispose()
    {
      // Allow calling this twice since signals automatically unsubscribe in SignalBus.LateDispose
      // and so this causes issues if users also unsubscribe in a MonoBehaviour OnDestroy on a
      // root game object
      if (!_pool.InactiveItems.Contains(this))
        _pool.Despawn(this);
    }

    public void OnSpawned(
      Action<object> callback, SignalDeclaration declaration)
    {
      Assert.IsNull(_callback);
      _callback = callback;
      _declaration = declaration;
      // Cache this in case OnDeclarationDespawned is called
      SignalId = declaration.BindingId;

      declaration.Add(this);
    }

    public void OnDespawned()
    {
      if (_declaration != null)
        _declaration.Remove(this);

      SetDefaults();
    }

    // See comment in SignalDeclaration for why this exists
    public void OnDeclarationDespawned()
    {
      _declaration = null;
    }

    public void Invoke(object signal)
    {
      _callback(signal);
    }

    private void SetDefaults()
    {
      _callback = null;
      _declaration = null;
      SignalId = new BindingId();
    }
  }
}