$(document).ready(function () {
    function isString(value) {
        return typeof value !== 'string' || value.trim() === ''
    }
    function isValidUrlPath(path) {
        const pattern = /^\/[a-zA-Z0-9_\-\/]*$/;

        return pattern.test(path);
    }

    function isValidToastLifeTime(value) {
        return typeof value === "number" && value >= 1000 && value <= 30000
    }

    function getElement(id) {
        if (isString(id)) 
            return null;

        var elm = $(`#${id}`);

        if (!elm.length) {
            console.warn(`No element found with ID ${id}.`);
            return null;
        }
        return elm;
    }

    $(document.body).on("delete-elm", function (evt) {
        var elm = getElement(evt.detail.value);
        if (elm) {
            elm.remove();
            console.log(`Element with ID ${evt.detail.value} removed.`);
        }
    });

    $(document.body).on("add-toast", function (evt) {
        initDismisses()
        console.log("Reinitialize all Dismisses.");

        var toDismissElm = getElement(evt.detail.toastId)
        var toastLifeTime = isValidToastLifeTime(evt.detail.toastLifeTime)
            ? evt.detail.toastLifeTime
            : 5000
                
        if (toDismissElm) {
            var dismissButton = toDismissElm.find("button[data-dismiss-target]")
            if (dismissButton.length) {
                setTimeout(function () {
                    dismissButton.click();
                    setTimeout(function () {
                        toDismissElm.remove();
                        if (isValidUrlPath(evt.detail.redirectUrl))
                            window.location.href = evt.detail.redirectUrl;
                        console.log(`Element with ID ${evt.detail.toastId} removed.`);
                    }, 400)
                }, toastLifeTime)
            }
        }
    });
});