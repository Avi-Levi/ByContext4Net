// Copyright 2011 Avi Levi

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//  http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ByContext.ModelBinders
{
    public class DynamicMethodModelBinderFactory : IModelBinderFactory
    {
        private bool injectNonPublic;

        public IModelBinder Create(Type modelType)
        {
            var injectors = new Dictionary<string, Action<object, object>>();

            foreach (var pi in modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p=>p.GetSetMethod(this.injectNonPublic) != null))
            {
                var injector = this.CreatePropertyInjector(pi);
                injectors.Add(pi.Name, injector);
            }

            return new DynamicMethodModelBinder(injectors);
        }

        private Action<object, object> CreatePropertyInjector(PropertyInfo property)
        {
            var dynamicMethod = new DynamicMethod(GetAnonymousMethodName(), typeof(void), new[] { typeof(object), typeof(object) }, true);

            ILGenerator il = dynamicMethod.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            EmitUnboxOrCast(il, property.DeclaringType);

            il.Emit(OpCodes.Ldarg_1);
            EmitUnboxOrCast(il, property.PropertyType);

            EmitMethodCall(il, property.GetSetMethod(injectNonPublic));
            il.Emit(OpCodes.Ret);

            return (Action<object, object>)dynamicMethod.CreateDelegate(typeof(Action<object, object>));
        }
        private void EmitUnboxOrCast(ILGenerator il, Type type)
        {
            OpCode opCode = type.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass;
            il.Emit(opCode, type);
        }
        private void EmitMethodCall(ILGenerator il, MethodInfo method)
        {
            OpCode opCode = method.IsFinal ? OpCodes.Call : OpCodes.Callvirt;
            il.Emit(opCode, method);
        }
        private string GetAnonymousMethodName()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}