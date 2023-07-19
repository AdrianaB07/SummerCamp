
const dialogOpeners = document.querySelectorAll('[data-dialogtarget]');
const dialogDelete = document.querySelectorAll('[data-dialogdelete]');
const dialogClosers = document.querySelectorAll('[data-dialogclose]');

for (let i = 0; i < dialogOpeners.length; i++) {
    const opener = dialogOpeners[i];
    const thisDialog =
        document.querySelector(
            `#${opener.getAttribute('data-dialogtarget')}`);

    opener.addEventListener('click', function () {
        thisDialog.showModal();
    });

}

for (let i = 0; i < dialogDelete.length; i++) {
    const closer = dialogDelete[i];
    const corrDialog =
        document.querySelector(
            `#${closer.getAttribute('data-dialogclose')}`);

    closer.addEventListener('click', function () {
        const deleteUrl = button.getAttribute('href');
        window.location.href = deleteUrl;
    });
}

for (let i = 0; i < dialogClosers.length; i++) {
    const closer = dialogClosers[i];
    const corrDialog =
        document.querySelector(
            `#${closer.getAttribute('data-dialogclose')}`);

    closer.addEventListener('click', function () {
        corrDialog.close();
    });

}

