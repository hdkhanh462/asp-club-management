$(document).ready(function () {
    function getElement(id) {
        if (typeof id !== 'string' || id.trim() === '') {
            //console.error('Invalid ID:', id);
            return null;
        }

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

    $(document.body).on("init-dismisses", function (evt) {
        initDismisses()
        console.log("Reinitialize all Dismisses.");

        var toDismissElm = getElement(evt.detail.toDismissId)
        if (toDismissElm) {
            var dismissButton = toDismissElm.find("button[data-dismiss-target]")
            if (dismissButton.length) {
                setTimeout(function () {
                    dismissButton.click();
                    console.log(`Element with ID ${evt.detail.toDismissId} dismissed.`);
                    setTimeout(function () {
                        toDismissElm.remove();
                        console.log(`Element with ID ${evt.detail.toDismissId} removed.`);
                    }, 400)
                }, 5000)
            }
        }
    });
});