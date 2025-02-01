function initDatepicker(id, additionalOptions = {}) {
    const $elm = document.getElementById(id);

    if (!$elm) return;

    const options = {
        autohide: true,
        format: "dd/mm/yyyy",
        orientation: "top",
        weekStart: 1,
        language: "vi",
        // clearBtn: true,
        // todayBtn: true,
        // todayBtnMode: 1,
    };

    new Datepicker($elm, {...options, ...additionalOptions});
}
