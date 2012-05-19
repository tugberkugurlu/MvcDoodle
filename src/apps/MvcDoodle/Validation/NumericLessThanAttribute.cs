using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MvcDoodle {

    //http://nickstips.wordpress.com/2011/11/05/asp-net-mvc-lessthan-and-greaterthan-validation-attributes/
    [AttributeUsage(AttributeTargets.Property)]
    public class NumericLessThanAttribute : ValidationAttribute, IClientValidatable {

        private const string _lessThanErrorMessage = "{0} must be less than {1}.";
        private const string _lessThanOrEqualToErrorMessage = "{0} must be less than or equal to {1}.";
        private const string _cannotFindPropertyErrorMessage = "Could not find a property named {0}.";
        private const string _isNotANumericValueErrorMessage = "{0} is not a numeric value.";
        private bool _allowEquality;

        public string OtherProperty { get; private set; }

        public bool AllowEquality {
            get {
                return _allowEquality;
            }
            set {
                _allowEquality = value;

                // Set the error message based on whether or not
                // equality is allowed
                this.ErrorMessage = (value ? _lessThanOrEqualToErrorMessage : _lessThanErrorMessage);
            }
        }

        public NumericLessThanAttribute(string otherProperty)
            : base(_lessThanErrorMessage) {

            if (string.IsNullOrEmpty(otherProperty))
                throw new ArgumentNullException("otherProperty");

            OtherProperty = otherProperty;
        }

        public override string FormatErrorMessage(string name) {

            return string.Format(
                CultureInfo.CurrentCulture, ErrorMessageString, name, OtherProperty
            );
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

            var otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);

            if (otherPropertyInfo == null) {

                return new ValidationResult(
                    string.Format(
                        CultureInfo.CurrentCulture, _cannotFindPropertyErrorMessage, OtherProperty
                    )
                );
            }

            var otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            decimal decValue, decOtherPropertyValue;

            // Check to ensure the validating property is numeric
            if (!decimal.TryParse(value.ToString(), out decValue)) {

                return new ValidationResult(
                    string.Format(
                        CultureInfo.CurrentCulture, _isNotANumericValueErrorMessage, validationContext.DisplayName
                    )
                );
            }

            // Check to ensure the other property is numeric
            if (!decimal.TryParse(otherPropertyValue.ToString(), out decOtherPropertyValue)) {

                return new ValidationResult(
                    string.Format(
                        CultureInfo.CurrentCulture, _isNotANumericValueErrorMessage, OtherProperty
                    )
                );
            }

            // Check for equality
            if (AllowEquality && decValue == decOtherPropertyValue) {
                return null;
            }
                // Check to see if the value is greater than the other property value
            else if (decValue > decOtherPropertyValue) {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            //returning null means that it is valid
            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context) {

            yield return new ModelClientValidationNumericLessThanRule(
                FormatErrorMessage(string.IsNullOrEmpty(metadata.DisplayName) ? metadata.PropertyName : metadata.DisplayName),
                formatPropertyForClientValidation(OtherProperty),
                this.AllowEquality
            );
        }

        //private helpers
        private string formatPropertyForClientValidation(string property) {

            if (string.IsNullOrEmpty(property))
                throw new ArgumentNullException("property");

            return "*." + property;
        }
    }
}