using UnityEngine;

namespace AppCoreModule.Scripts.Services
{
    public class BaseService : MonoBehaviour, IService
    {
        public virtual void Init()
        {
        }

        public virtual void Dispose()
        {
        }
    }
}