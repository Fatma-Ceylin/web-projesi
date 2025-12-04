// site.js - handles onload reveal and scroll reveal using IntersectionObserver

document.addEventListener("DOMContentLoaded", function () {
    // On-load reveal for hero image
    const heroImage = document.querySelector('.hero-image.reveal-right-onload');
    const heroText = document.querySelector('.hero-text');

    if (heroImage) {
        // Wait for image to fully load, then reveal image and text
        if (heroImage.complete) {
            revealHero();
        } else {
            heroImage.addEventListener('load', revealHero);
        }
    }

    function revealHero() {
        heroImage.classList.add('visible');
        // slight delay for text
        setTimeout(() => {
            if (heroText) heroText.classList.add('visible');
        }, 300);
    }

    // IntersectionObserver for scroll-reveal items
    const options = {
        root: null,
        rootMargin: '0px',
        threshold: 0.15
    };

    const observer = new IntersectionObserver((entries, obs) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
                obs.unobserve(entry.target);
            }
        });
    }, options);

    // elements to observe
    document.querySelectorAll('.reveal-on-scroll-left, .reveal-on-scroll-right, .feature-text').forEach(el => {
        observer.observe(el);
    });
});
