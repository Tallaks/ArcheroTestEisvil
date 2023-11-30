using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Data.Providers
{
  public interface IAsyncConfigProvider<T> : IConfigProvider<T> where T : ScriptableObject
  {
    UniTask<T> GetAsync(string id);
  }
}