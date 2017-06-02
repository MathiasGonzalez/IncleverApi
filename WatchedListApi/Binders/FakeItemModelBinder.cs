using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using IncleverApi.Models;

namespace IncleverApi.Binders
{
    public class FakeItemModelBinder : ModelBinderProvider, IModelBinder
    {

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {

            if (bindingContext.ModelType != typeof(FakeItem))
            {
                return false;

            }

            bindingContext.Model = new FakeItem() { Description = "FakeItemModelBinder" };

            return true;

        }       

        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return this;
        }
    }
}