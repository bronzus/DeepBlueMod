using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace kz.uitls
{
    [Serializable]
    public class InjectVariables
    {
        [Serializable]
        public class BaseInjectVariable
        {
            public string key;
        }

        [Serializable]
        public class InjectVariable<T>:BaseInjectVariable
        {
            public T value = default(T);
        }
        
        public List<InjectVariable<bool>> boolVars = new();
        public List<InjectVariable<int>> intVars = new();
        public List<InjectVariable<float>> floatVars = new();
        public List<InjectVariable<string>> stringVars = new();
        public List<InjectVariable<Vector2>> vector2Vars = new();
        public List<InjectVariable<Vector3>> vector3Vars = new();
        public List<InjectVariable<Quaternion>> quaternionVars = new();
        public List<InjectVariable<GameObject>> gameObjectVars = new();
        public List<InjectVariable<List<bool>>> boolListVars= new();
        public List<InjectVariable<List<int>>> intListVars= new();
        public List<InjectVariable<List<float>>> floatListVars= new();
        public List<InjectVariable<List<string>>> stringListVars= new();
        public List<InjectVariable<List<Vector2>>> vector2ListVars= new();
        public List<InjectVariable<List<Vector3>>> vector3ListVars= new();
        public List<InjectVariable<List<Quaternion>>> quaternionListVars= new();
        public List<InjectVariable<List<GameObject>>> gameObjectListVars= new();
    }
}