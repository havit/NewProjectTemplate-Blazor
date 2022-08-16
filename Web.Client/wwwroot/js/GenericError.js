export function copyToClipboard(text, genericErrorDotnetObjectReference) {
	navigator.clipboard.writeText(text).then(function () {
		genericErrorDotnetObjectReference.invokeMethodAsync('GenericError_HandleCopiedToClipboard');
	});
}
