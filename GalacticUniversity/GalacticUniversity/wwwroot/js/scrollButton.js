document.addEventListener('DOMContentLoaded', function () {
    const button = document.querySelector('.button');

    // Ensure the button exists before proceeding
    if (!button) return;

    button.style.opacity = '0'; // Start hidden
    button.style.transition = 'opacity 0.5s ease';

    // Show button after scrolling 100px
    window.addEventListener('scroll', function () {
        if (window.scrollY > 100) {
            button.style.opacity = '1';
        } else {
            button.style.opacity = '0';
        }
    });

    // Scroll to top when button is clicked
    button.addEventListener('click', function () {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
    });
});
