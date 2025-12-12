using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HARD.CORE.DAT
{
    public abstract class BaseDA<MyClass, Type1, Type2>
    {

        public abstract T Get<T>(Type1 clave, Type2 activo);

        public abstract MyClass Post(MyClass elemento);

        public abstract bool Put(MyClass elemento);

        public abstract bool Delete(Type1 clave);


        public List<Dictionary<string, object>> DictionariesFromReader(IDataReader reader)
        {
            List<Dictionary<string, object>> dictionaries = new List<Dictionary<string, object>>();
            while (reader.Read())
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue);
                dictionaries.Add(dictionary);
            }
            return (dictionaries);
        }

        public Dictionary<string, object> DictionaryFromReader(IDataReader reader)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            while (reader.Read())
            {
                dictionary = Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue);
            }
            return (dictionary);
        }
    }
}
