using System;
using UnityEngine;

namespace AppCoreModule.Scripts.Services
{
    public interface IService : IDisposable
    {
        public void Init();

        public void Dispose();
    }
}