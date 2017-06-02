using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nustache;
using Nustache.Core;
using PrimaryHub.Models;
using System.IO;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;

namespace PrimaryHub
{
    public class Class1
    {
        public static string Compilar(Item Item, bool generar = true)
        {
            var t = Render.FileToString($"{AppDomain.CurrentDomain.RelativeSearchPath}\\Templates\\Entity.mustache", Item);

            if (generar && !string.IsNullOrEmpty(Item.Nombre))
            {
                using (StreamWriter sw = new StreamWriter($"C:\\{Item.Nombre}.cs"))
                {
                    sw.Write(t);
                }
            }

            return t;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////
        // json schema
        //////////////////////////////////////////////////////////////////////////////////////////////
        public static JSchema getSchema(Type type)
        {
            JSchemaGenerator generator = new JSchemaGenerator();

            JSchema schema = generator.Generate(type);

            var binPath = AppDomain.CurrentDomain.RelativeSearchPath;

            string typeName = type.Name;

            using (StreamWriter file = File.CreateText($"c:\\Generated\\{typeName}.json"))
            using (JsonTextWriter writer = new JsonTextWriter(file))
                schema.WriteTo(writer);
            schema.Title = typeName;


            Item item = Class1.itemByJsonSchema(schema);
            // 
            #region generar entidad cs
            var t = Class1.getCSharpClass(schema);

            using (StreamWriter sw = new StreamWriter($"C:\\Generated\\{typeName}.cs"))
            {
                sw.Write(t);
            }
            #endregion
            #region generar entidad ts
            var ts = getTsClass(schema);
            using (StreamWriter sw = new StreamWriter($"C:\\Generated\\{typeName}.ts"))
            {
                sw.Write(ts);
            }
            #endregion  
            return schema;
        }

        static Item itemByJsonSchema(JSchema schema)
        {

            Item item = new Item();
            item.Nombre = schema.Title;
            item.propiedades = new List<Propiedad>();
            schema.Properties.AsEnumerable().ToList().ForEach(x =>
            {
                item.propiedades.Add(new Propiedad()
                {
                    nombre = x.Key,
                    tipo = Class1.tipoByType(x.Value)

                });

            });
            return item;
        }
        static string tipoByType(JSchema type)
        {
            string result = "";
            string[] tipos = type.Type.ToString().Split(',');
            result = tipos[0];
            result = result.TrimEnd();
            if (result == "Array")
            {
                result = "List";
                result = result + @"~" + type.Items.First().Type.ToString().Split(',')[0] + @"*";
            }
            return result;
        }
        static string TstipoByType(JSchema type)
        {
            string result = "";
            string[] tipos = type.Type.ToString().Split(',');
            result = tipos[0];
            result = result.TrimEnd();
            if (result == "Array")
            {
                result = result + @"~" + type.Items.First().Type.ToString().Split(',')[0] + @"*";
                if (tipos.Length > 1)
                {
                    result += $"|{tipos[1]}";
                   // result += Class1.TstipoByType(type.Items.First());
                }
            }
            return result;
        }

        //cs
        static string getCSharpClass(JSchema schema)
        {
            Item item = Class1.CSItemByJsonSchema(schema);
            var t = Render.FileToString($"{AppDomain.CurrentDomain.RelativeSearchPath}\\Templates\\Entity.mustache", item);
            t = t.Replace('~', '<');
            t = t.Replace('*', '>');
            return t;
        }
        static Item CSItemByJsonSchema(JSchema schema)
        {

            Item item = new Item();
            item.Nombre = schema.Title;
            item.propiedades = new List<Propiedad>();
            schema.Properties.AsEnumerable().ToList().ForEach(x =>
            {
                item.propiedades.Add(new Propiedad()
                {
                    nombre = x.Key,
                    tipo = Class1.tipoByType(x.Value)

                });

            });
            return item;
        }
        //ts
        static string getTsClass(JSchema schema)
        {
            Item item = Class1.TsItemByJsonSchema(schema);
            var t = Render.FileToString($"{AppDomain.CurrentDomain.RelativeSearchPath}\\Templates\\TsEntity.mustache", item);
            t = t.Replace('~', '<');
            t = t.Replace('*', '>');
            return t;
        }
        static Item TsItemByJsonSchema(JSchema schema)
        {

            Item item = new Item();
            item.Nombre = schema.Title;
            item.propiedades = new List<Propiedad>();
            schema.Properties.AsEnumerable().ToList().ForEach(x =>
            {
                item.propiedades.Add(new Propiedad()
                {
                    nombre = x.Key,
                    tipo = Class1.TstipoByType(x.Value)

                });

            });
            return item;
        }
    }
}
