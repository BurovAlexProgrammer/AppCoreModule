using UnityEngine;

namespace AppCoreModule.Scripts.Services
{
    public interface IService
    {
        public virtual void Init()
        {
            Debug.Log("Init");
        }

        public void Dispose()
        {
            Debug.Log("Dispose");
        }
    }
}