(function () {
 
 var templates = { 
    "name": "dnn",
    "context": "htmlmixed",
    "templates": [
    {
        "name": "@foreach",
        "description": "iterate over collection",
        "template": "@foreach(var ${item} in ${collection}){\n\t <${tag}>@${item}</${tag}>	\n}"
    }, {
        "name": "@for",
        "description": "iterate over array with temporary variable",
        "template": "for (var ${index} = 0; ${index} < ${array}.length; ${index}++) {\n\tvar ${array_element} = ${array}[${index}];\n\t${cursor}\n}"
    }
    ]
};
CodeMirror.templatesHint.addTemplates(templates);
})();