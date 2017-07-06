$.validator.addMethod("mustbegreaterthan", function (value, element, params) {
    var result = checkIsValidDate(value);
    if (!resul.isValid) {
        return false;
    }
    var checkResult = checkIsValidDate(params);
    return result.value > checkResult.value;
});

$.validator.unobtrusive.adapters.add("mustbegreaterthan", ["minimumdate"], function (options) {
    options["mustbegreaterthan"] = options.params.minimumdate;
    options.messages["mustbegreaterthan"] = options.message;
});

function checkIsValidDate(valueToCheck) {
    var d = new Date(valueToCheck);
    if (Object.prototype.toString.call(d) === "[object Date]") {
        if (isNaN(d.getTime())) {
            return { isValid: false };
        } else {
            return { isValid: true, value: d };
        }
    } else {
        return { isValid: false };
    }
}