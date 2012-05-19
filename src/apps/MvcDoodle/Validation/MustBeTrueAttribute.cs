using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MvcDoodle {

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MustBeTrueAttribute : ValidationAttribute, IClientValidatable {

        public override bool IsValid(object value) {

            return value != null && value is bool && (bool)value;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context) {

            yield return new ModelClientValidationRule {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "mustbetrue"
            };
        }
    }
}