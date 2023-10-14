const cardSections = document.querySelectorAll('.card-section');

window.addEventListener('scroll', () => {
    for (const cardSection of cardSections) {
        const boundingClientRect = cardSection.getBoundingClientRect();

        if (boundingClientRect.top >= 0 && boundingClientRect.bottom <= window.innerHeight) {
            cardSection.classList.add('in-viewport');
        } else {
            cardSection.classList.remove('in-viewport');
        }
    }
});