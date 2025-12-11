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



// site.js dosyanıza ekleyin
$(document).ready(function() {
    const $stars = $('.rating-stars .star');
    const $ratingInput = $('#RatingValue');

    // Hover Efekti
    $stars.on('mouseenter', function() {
        const index = $(this).data('index'); 
        $stars.removeClass('hover');
        $stars.each(function() {
            if ($(this).data('index') <= index) {
                $(this).addClass('hover');
            }
        });
    }).on('mouseleave', function() {
        $stars.removeClass('hover');
    });

    // Tıklama Olayı
    $stars.on('click', function() {
        const index = $(this).data('index'); 
        const rating = index + 1; 
        
        $ratingInput.val(rating); // Değeri gizli inputa atar

        $stars.removeClass('selected');
        
        $stars.each(function() {
            if ($(this).data('index') <= index) {
                $(this).addClass('selected');
            }
        });
    });
});