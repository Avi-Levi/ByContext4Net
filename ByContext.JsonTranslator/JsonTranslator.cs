﻿using System;
using ByContext.StringToValueTranslator;
using Newtonsoft.Json.Linq;

namespace ByContext.JsonTranslator
 {
     public class JsonTranslator : IStringToValueTranslator
     {
         private readonly Type _objectType;
 
         public JsonTranslator(Type objectType)
         {
             _objectType = objectType;
         }
 
         public object Translate(string value)
         {
             var reader = JObject.Parse(value);
             return reader.ToObject(_objectType);
         }
     }
 }