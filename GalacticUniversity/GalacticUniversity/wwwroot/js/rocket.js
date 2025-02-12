// Add this to a new JS file (e.g., rocket.js)

// Select the rocket element
const rocket = document.querySelector('.fas.fa-rocket');

// Add a "bounce" effect when clicked
rocket.addEventListener('click', function () {
    rocket.classList.add('bounce'); // Add bounce effect class
    setTimeout(() => {
        rocket.classList.remove('bounce'); // Remove it after animation ends
    }, 1000);
});

// Adding bouncing effect with CSS
const style = document.createElement('style');
style.innerHTML = `
    .fas.fa-rocket.bounce {
        animation: bounce 0.3s ease-out;
    }

    @keyframes bounce {
        0% { transform: translateY(0); }
        25% { transform: translateY(-10px); }
        50% { transform: translateY(0); }
        75% { transform: translateY(-5px); }
        100% { transform: translateY(0); }
    }
`;
document.head.appendChild(style);
