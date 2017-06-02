using Newtonsoft.Json;
using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using IncleverApi.Models;

namespace IncleverApi.Binders
{
    public class GenericModelBinder : ModelBinderProvider, IModelBinder
    {

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {

            //if (actionContext.ActionDescriptor.ActionName == "GenericPost")
            //{
            //    var x = actionContext.Request.RequestUri.OriginalString.Split("?".ToCharArray(), StringSplitOptions.None);
            //    GenericMessage par = new GenericMessage();
            //    JsonConvert.PopulateObject(x[1], par);
            //    actionContext.ActionArguments.Add("action", par.action);
            //    bindingContext.Model = par;
            //    return true;
            //}
            //if (actionContext.ActionDescriptor.ActionName == "GenericGet")
            //{
            //    var x = actionContext.Request.RequestUri.OriginalString.Split("?".ToCharArray(), StringSplitOptions.None);
            //    GenericMessage par = new GenericMessage();
            //    JsonConvert.PopulateObject(x[1], par);
            //    actionContext.ActionArguments.Add("action", par.action);
            //    bindingContext.Model = par;
            //    return true;
            //}

            if (bindingContext.ModelType == typeof(FakeItem))
            {
                return false;
            }
            if (actionContext.Request.Method.Method == "GET")
            {
                object data;

                if (actionContext.Request.Properties.TryGetValue("DATA", out data))
                {
                    bindingContext.Model = System.Activator.CreateInstance(bindingContext.ModelType);
                    if (data != null && ((string)data) != String.Empty)
                    {
                        JsonConvert.PopulateObject((string)data, bindingContext.Model);
                    }
                }

            }
            else if (actionContext.Request.Method.Method == "POST")
            {
                var x = actionContext.Request.Content.ReadAsStringAsync().Result;
                //bindingContext.ModelType;
                bindingContext.Model = Activator.CreateInstance(bindingContext.ModelType);
                if (x.Length > 1)
                {
                    JsonConvert.PopulateObject(x, bindingContext.Model);
                }
            }
            return true;

        }

        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return this;
        }
        public object Instancia(string typo)
        {
            string _dll = AppDomain.CurrentDomain.RelativeSearchPath + @"\Parametros.dll";
            var parssembly = System.Reflection.Assembly.LoadFile(_dll);
            Type typ = parssembly.GetType(typo);
            return Activator.CreateInstance(typ);
        }
    }
}