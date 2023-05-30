using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZandomLevelGenerator.Customizables
{
    public abstract class GeneratorTask
    {
        public IEnumerator Run()
        {
            throw new System.NotImplementedException();
        }
    }
}
