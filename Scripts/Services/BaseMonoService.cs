using UnityEngine;

namespace AppCoreModule.Scripts.Services
{
    public class BaseMonoService : MonoBehaviour, IService
    {
        public virtual void Init()
        {
        }

        public virtual void Dispose()
        {
        }
    }
}