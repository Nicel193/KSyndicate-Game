using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase
{
    public interface IInstantiateTool : IService
    {
        GameObject InstantiateRegistered(string prefabPath, Vector3 at);
        GameObject InstantiateRegistered(string prefabPath);
        Task<GameObject> InstantiateByAddress(string prefabAddress);
    }
}