﻿using System;
using System.Globalization;
using System.Web.Mvc;

namespace BusinessLight.Mvc.Binders
{
    public class DoubleModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object result = null;

            // Don't do this here!
            // It might do bindingContext.ModelState.AddModelError
            // and there is no RemoveModelError!
            // 
            // result = base.BindModel(controllerContext, bindingContext);

            var modelName = bindingContext.ModelName;
            var attemptedValue =
                bindingContext.ValueProvider.GetValue(modelName).AttemptedValue;

            // Depending on CultureInfo, the NumberDecimalSeparator can be "," or "."
            // Both "." and "," should be accepted, but aren't.
            var wantedSeperator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
            var alternateSeperator = wantedSeperator == "," ? "." : ",";

            if (attemptedValue.IndexOf(wantedSeperator) == -1 && attemptedValue.IndexOf(alternateSeperator) != -1)
            {
                attemptedValue = attemptedValue.Replace(alternateSeperator, wantedSeperator);
            }

            try
            {
                if (bindingContext.ModelMetadata.IsNullableValueType && string.IsNullOrWhiteSpace(attemptedValue))
                {
                    return null;
                }

                result = double.Parse(attemptedValue, NumberStyles.Any);
            }
            catch (FormatException e)
            {
                bindingContext.ModelState.AddModelError(modelName, e);
            }

            return result;
        }
    }
}
