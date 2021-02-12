#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      05.02.2021
#endregion

using System;
using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebUI
{
    public class PersonConverter : JsonCreationConverter<Person>
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //throw new NotImplementedException();
        }

        protected override Person Create(Type objectType, JObject jObject)
        {
            if (FieldExists("Aufgabe", jObject))
            {
                return new Spieler();
            }
            else if (FieldExists("Gehalt", jObject))
            {
                return new Trainer();
            }
            else
            {
                return new Spieler();
            }
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName.ToLower()] != null;
        }
    }

    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        /// <summary>
        /// Create an instance of objectType, based properties in the JSON object
        /// </summary>
        /// <param name="objectType">type of object expected</param>
        /// <param name="jObject">
        /// contents of JSON object that will be deserialized
        /// </param>
        /// <returns></returns>
        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                         object existingValue,
                                         JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            T target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
    }
}
