using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kz
{
    [AddComponentMenu("")]
    public class ListGameObjectVariable : Variable<List<GameObject>>
    {
        protected override bool ValueEquals(List<GameObject> val1, List<GameObject> val2)
        {
            if (val1.Count != val2.Count) return false;
            for (int i = 0; i < val1.Count; i++)
            {
                if (val1[i] != val2[i]) return false;
            }

            return true;
        }
    }
    
    [System.Serializable]
    public class ListGameObjectReference : VariableReference<ListGameObjectVariable, List<GameObject>>
    {
        public ListGameObjectReference(VarRefMode mode = VarRefMode.EnableConstant)
        {
            SetMode(mode);
        }

        protected override bool isConstantValid
        {
            get { return constantValue != null; }
        }

        public List<GameObject> Value
        {
            get
            {
                return (useConstant)? constantValue : this.GetVariable().Value;
            }
            set
            {
                if (useConstant)
                {
                    constantValue = value;
                }
                else
                {
                    this.GetVariable().Value = value;
                }
            }
        }
    }
}