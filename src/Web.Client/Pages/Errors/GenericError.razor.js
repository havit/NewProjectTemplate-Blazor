export function copyToClipboard(text, genericErrorDotnetObjectReference) {
	navigator.clipboard.writeText(text).then(function () {
		genericErrorDotnetObjectReference.invokeMethodAsync('GenericError_HandleCopiedToClipboard');
	});
}

export function getTraceID() {
	if (window.appInsights && window.appInsights.context && window.appInsights.context.telemetryTrace) {
		return window.appInsights.context.telemetryTrace.traceID;
	}
}