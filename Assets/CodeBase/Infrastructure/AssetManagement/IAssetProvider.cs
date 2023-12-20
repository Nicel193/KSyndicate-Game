using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
    }
}