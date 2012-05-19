/// <reference path="jquery-1.4.4-vsdoc.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />
(function () {

    //NumericLessThanAttribute client side validator
    $.validator.addMethod("numericlessthan", function (value, element, params) {

        var otherValue = $(params.element).val();
        return isNaN(value) && isNaN(otherValue) || (params.allowequality === "True" ? parseFloat(value) <= parseFloat(otherValue) : parseFloat(value) < parseFloat(otherValue));

    }, "");

    $.validator.unobtrusive.adapters.add("numericlessthan", ["other", "allowequality"], function (options) {

        var prefix = options.element.name.substr(0, options.element.name.lastIndexOf('.') + 1)
        other = options.params.other,
		fullOtherName = appendModelPrefix(other, prefix),
		element = $(options.form).find(":input[name='" + fullOtherName + "']")[0];

        options.rules["numericlessthan"] = {
            allowequality: options.params.allowequality,
            element: element
        };

        if (options.message) {
            options.messages['numericlessthan'] = options.message;
        }

    });

    //MustBeTrueAttribute client side validator
    $.validator.unobtrusive.adapters.addBool("mustbetrue", "required");

    //helpers
    function appendModelPrefix(value, prefix) {
        if (value.indexOf('*.') === 0) {
            value = value.replace('*.', prefix);
        }
        return value;
    }

}());