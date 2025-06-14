using System.Collections.Generic;
using AppCoreModule.Scripts.Services;
using UnityEngine;

namespace AppCoreModule.Scripts.Contexts
{
    public abstract class BaseContext : MonoBehaviour
    {
        [SerializeField] private List<BaseMonoService> _services;
        
    }
}