using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebStore_2021.Infrastructure.Conventions
{
    public class TestControllerModelConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerName == "Home")
            {
                //controller.Actions.Add(new ActionModel());
            }
        }
    }
}
